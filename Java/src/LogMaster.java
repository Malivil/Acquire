/* **DON'T CHANGE THIS HEADER**
 $Author: Myles $
 $Revision: 1.12 $
 $Date: 2007/05/14 16:31:37 $
 $Source: /cvs/repo/Acquire/src/LogMaster.java,v $
 */

import java.util.*;
import javax.swing.JTextArea;
import javax.swing.text.BadLocationException;
import javax.swing.text.Element;

public class LogMaster
{
    //*PRIVATE FIELDS*//

    private static JTextArea myTextArea;
    private static String lastLog = "";
    private static String lastType = "";
    private static String lastLogType = "";
    private static String lastDynamic[] = new String[2];
    private static Map<String, Integer> storedNums =  new HashMap<String, Integer>();
    private static Map<String, String[]> storedStrs =  new HashMap<String, String[]>();
    private static boolean isBeingReplaced = false;

    // <editor-fold defaultstate="collapsed" desc=" Standard progression array ">
    public final static String[] STANDARD_NUM_PROG = {"a", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30"};
    //</editor-fold>

    //*OTHER METHODS*//

    public static void setTextArea(JTextArea textArea)
    {
        myTextArea = textArea;
    }

    public static void log(String s)
    {
        if(!s.equals(lastLog))
        {
            myTextArea.append("\n>" + s);
            myTextArea.setCaretPosition(myTextArea.getText().length());
            lastLog = s;
            lastLogType = "Norm";
        }
    }

    public static void dynLog(String s)
    {
        if(isBeingReplaced && lastLogType.equals("Dyn"))
        {
            int lineCharCountStart = 0;
            int lineCharCountEnd = 0;

            //The following code is partially borrow from the Java Almanac
            //at http://jug.org.ua

            // Get paragraph element
            Element paragraph = myTextArea.getDocument().getDefaultRootElement();

            // Get number of content elements
            int contentCount = paragraph.getElementCount();

            // Get index ranges for each content element.
            // Each content element represents one line.
            // Each line includes the terminating newline.
            for(int i = 0; i < contentCount; i++)
            {
                Element e = paragraph.getElement(i);
                int rangeStart = e.getStartOffset();
                int rangeEnd = e.getEndOffset();

                try
                {
                    String line = myTextArea.getText(rangeStart, rangeEnd - rangeStart);

                    if(i < (contentCount - 1))
                    {
                        lineCharCountStart += line.length();
                    }

                    lineCharCountEnd += line.length();
                }
                catch (BadLocationException ex)
                {
                }
            }

            myTextArea.replaceRange(">" + s, lineCharCountStart, lineCharCountEnd - 1);
            isBeingReplaced = false;
            lastLogType = "Dyn";
        }
        else
        {
            myTextArea.append("\n>" + s);
        }

        myTextArea.setCaretPosition(myTextArea.getText().length());
        lastLog = s;
        lastLogType = "Dyn";
    }
    
    public static void log(Player p, String s)
    {
        myTextArea.append("\n" + p + ": " + s);
        myTextArea.setCaretPosition(myTextArea.getText().length());
        lastLog = s;
        lastLogType = "Norm";
    }

    public static void debugLog(String s)
    {
        myTextArea.append("\n!->" + s);
        myTextArea.setCaretPosition(myTextArea.getText().length());
        lastLog = s;
        lastLogType = "Norm";
    }

    public static String dynamicContent(String k, int start, int inc, String type)
    {
        String ret = "";

        if(!storedNums.containsKey(k))
        {
            storedNums.put(k, start);
            ret += start;
        }
        else
        {
            String lastDyn = "";

            if(type.equals("ShareS"))
            {
                lastDyn = lastDynamic[0];
            }
            else
            {
                lastDyn = lastDynamic[1];
            }

            if(!k.equals(lastDyn) || !lastLogType.equals("Dyn"))
            {
                storedNums.clear();
                storedNums.put(k, start);
                ret += start;
            }
            else
            {
                storedNums.put(k, storedNums.get(k) + inc);
                ret += storedNums.get(k);
                isBeingReplaced = true;
            }
        }

        if(type.equals("ShareS"))
        {
            lastDynamic[0] = k;
        }
        else
        {
            lastDynamic[1] = k;
        }

        if(!type.equals("ShareS"))
        {
            lastType = type;
        }

        return(ret);
    }

    public static String dynamicContent(String k, String[] vals, int inc, String type)
    {
        String ret = "";

        if(!storedStrs.containsKey(k))
        {
            storedStrs.put(k, vals);
            ret = vals[0];
        }
        else
        {
            String lastDyn = "";

            if(type.equals("ShareS"))
            {
                lastDyn = lastDynamic[0];
            }
            else
            {
                lastDyn = lastDynamic[1];
            }

            if(!k.equals(lastDyn) || !lastLogType.equals("Dyn"))
            {
                storedStrs.clear();
                storedStrs.put(k, vals);
                ret = vals[0];
            }
            else
            {
                if(storedStrs.get(k).length <= inc)
                {
                    inc = storedStrs.get(k).length - 1;
                }

                if(inc > 0)
                {
                    String[] temp = new String[storedStrs.get(k).length - inc];

                    for(int i =0; i < temp.length; i++)
                    {
                        temp[i] = storedStrs.get(k)[i + inc];
                    }

                    storedStrs.put(k, temp);
                }

                ret = storedStrs.get(k)[0];
                isBeingReplaced = true;
            }
        }

        if(type.equals("ShareS"))
        {
            lastDynamic[0] = k;
        }
        else
        {
            lastDynamic[1] = k;
        }

        if(!type.equals("ShareS"))
        {
            lastType = type;
        }

        return(ret);
    }

    public static void clearDynamicContent(boolean strings, boolean nums)
    {
        if(strings)
        {
            storedStrs.clear();
        }

        if(nums)
        {
            storedNums.clear();
        }

        lastDynamic = new String[2];
        isBeingReplaced = false;
    }
}