/* **DON'T CHANGE THIS HEADER**
 $Author: Mike Lebson $
 $Revision: 1.7 $
 $Date: 2007/05/05 15:09:49 $
 $Source: /cvs/repo/Acquire/src/HandPanel.java,v $
*/

import java.awt.*;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;
import java.util.ArrayList;
import javax.swing.JButton;
import javax.swing.JOptionPane;

public class HandPanel extends JButton implements MouseListener
{
    //*PRIVATE FIELDS*//

    private ArrayList<Square> mySquares;
    private int sizeBag;
    private GridPanel myGridPanel;

    //*CONSTRUCTOR*//

    public HandPanel(GridPanel gp)
    {   
        myGridPanel = gp;
    	sizeBag = 0;
    	mySquares = new ArrayList<Square>();
        addMouseListener(this);
    }

    //*SET METHODS*//

    public void setHand(ArrayList<Square> s, int bagLeft)
    {
        sizeBag = bagLeft;
        mySquares = s;
        repaint();
    }

    //*OTHER METHODS*//

    public void paintComponent(Graphics g)
    {
        g.clearRect(0, 0, 900, 50);
        g.drawString("Number of squares left in bag: " + sizeBag, 520, 15);

        Font f = new Font("plain", 1, 16);

        int x = 10;

        for(Square s : mySquares)
        {
            int charVal;

            if(s.canBePlaced())
            {
                g.setColor(Color.BLACK);
            }
            else
            {
                g.setColor(Color.WHITE);
            }

            g.fillRect(x, 10, 40, 30);

            g.setColor(Color.WHITE);
            g.setFont(f);

            if(s.toString().length() > 2)
            {
                charVal = (Character.getNumericValue(s.toString().charAt(1)) * 10) + Character.getNumericValue(s.toString().charAt(2));
            }
            else
            {
                charVal = Character.getNumericValue(s.toString().charAt(1));
            }

            if(charVal > 9)
            {
                if(s.toString().charAt(0) == 'I')
                {
                    g.drawString(s.toString(), x+8, 30);
                }
                else
                {
                    g.drawString(s.toString(), x+5, 30);
                }
            }
            else
            {
                if(s.toString().charAt(0) == 'I')
                {
                    g.drawString(s.toString(), x+13, 30);
                }
                else
                {
                    g.drawString(s.toString(), x+10, 30);
                }
            }

            x += 50;
        }
    }

    //*MOUSE METHODS*//

    public void mouseClicked(MouseEvent e) 
    {
    }

    public void mousePressed(MouseEvent e) 
    {
    }

    public void mouseReleased(MouseEvent e) 
    {
        int x = e.getX(); int y = e.getY();
        int ctr = 999;

        if(y >= 10 && y <= 40)
        {
            if(x >= 10 && x <= 50)
            {
                ctr = 0;
            }
            else if(x >= 60 && x <= 100)
            {
                ctr = 1;
            }
            else if(x >= 110 && x <= 150)
            {
                ctr = 2;
            }
            else if(x >= 160 && x <= 200)
            {
                ctr = 3;
            }
            else if(x >= 210 && x <= 250)
            {
                ctr = 4;
            }
            else if(x >= 260 && x <= 300)
            {
                ctr = 5;
            }
            else if(x >= 310 && x <= 350)
            {
                ctr = 6;
            }
            else if(x >= 360 && x <= 400)
            {
                ctr = 7;
            }
            else if(x >= 410 && x <= 450)
            {
                ctr = 8;
            }
            else if(x >= 460 && x <= 500)
            {
                ctr = 9;
            }

            if(ctr != 999 && !mySquares.get(ctr).canBePlaced())
            {
                Object o = JOptionPane.showConfirmDialog(null, "Would you like to discard " + mySquares.get(ctr).toString() + "?", "Drop it like it's hot?", JOptionPane.YES_NO_OPTION, JOptionPane.INFORMATION_MESSAGE);
                String answer = null;

                if(o != null)
                {
                    answer = o.toString();
                }

                if(answer != null)
                {
                    if(answer.equals("0"))
                    {
                        LogMaster.log("You discarded " + mySquares.get(ctr));
                        mySquares.get(ctr).isHighlighted(false);
                        myGridPanel.repaint();
                        mySquares.remove(ctr);
                        repaint();
                        Game.getActivePlayer().setSquares(mySquares);

                        if(mySquares.isEmpty())
                        {
                            Game.getActivePlayer().canPlaceSquare(false);
                            AcquireFrame.endTurnButton.setEnabled(true);
                        }
                    }
                }
            }
        }
    }

    public void mouseEntered(MouseEvent e) 
    {
    }

    public void mouseExited(MouseEvent e) 
    {
    }
}