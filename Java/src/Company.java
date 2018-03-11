/* **DON'T CHANGE THIS HEADER**
 $Author: Colin $
 $Revision: 1.35 $
 $Date: 2007/05/17 00:45:22 $
 $Source: /cvs/repo/Acquire/src/Company.java,v $
*/

import java.util.*;
import java.awt.Image;

public class Company extends ArrayList<Square>
{
    //Set to true to display debugging messages
    private boolean DEBUG = false;

    //*PRIVATE FIELDS*//

    private String myName;
    private boolean isCompCondemned, isPlaced = false, isSafe = false;
    private int myID, myQuality, myShares = 30, mySize = 0;
    private Image myImage;
    private ArrayList<Player> myMajorityHolders, myMinorityHolders;
    private boolean isMerging = false;

    //*CONSTRUCTOR*//

    public Company(String n, int i, int q)
    {
        myName = n;
        myID = i;
        isCompCondemned = false;
        myQuality = q;
        myMajorityHolders = new ArrayList<Player>();
        myMinorityHolders = new ArrayList<Player>();
    }

    //*OTHER METHODS*//

    public void condemn()
    {
        isCompCondemned = true;
    }

    public void unCondemn()
    {
        isCompCondemned = false;
    }

    //Buys a single share from the company and returns the price
    public int buyShare(Player p)
    {      
        int price;

        if(myShares > 0)
        {
            myShares--;
            price = getPrice();
        }
        else
        {
            price = 0;
        }

        return(price);
    }

    //Sells a single share back to the company and returns the price
    public int sellShare(int companySize)
    {
        int temp = mySize;
        int price = 0;

        mySize = companySize;
        myShares++;
        price = getPrice();
        mySize = temp;

        return(price);
    }

    public String toString()
    {
        return(myName);
    }

    public void giveMajMinBonus(int compSize)
    {
        int temp = mySize;
        mySize = compSize;

        if(DEBUG)
        {
            System.out.print("Majority Leaders before merge: ");
            printHolders(myMajorityHolders);
        }
        
        for(Player pl : myMajorityHolders)
        {
            if(DEBUG)
            {
                LogMaster.debugLog(this + " at size " + mySize + " giving majority leader " + pl + " " + getPrice() + " * 10 / " + myMajorityHolders.size());
            }
            
            pl.giveMoney((getPrice() * 10) / myMajorityHolders.size());
        }
        
        if(DEBUG)
        {
            System.out.print("Minority Leaders before merge: ");
            printHolders(myMinorityHolders);
        }
        
        for(Player pl : myMinorityHolders)
        {
            if(DEBUG)
            {
                LogMaster.debugLog(this + " at size " + mySize + " giving minority leader " + pl + " " + getPrice() + " * 5 / " + myMinorityHolders.size());
            }
            
            pl.giveMoney((getPrice() * 5) / myMinorityHolders.size());
        }

        mySize = temp;
        myMajorityHolders.clear();
        myMinorityHolders.clear();
    }

    public void updateMajMinHolders(Player p)
    {
        ArrayList<Player> oldMajorityHolders = new ArrayList<Player>(), oldMinorityHolders = new ArrayList<Player>();
        oldMajorityHolders.addAll(myMajorityHolders);
        oldMinorityHolders.addAll(myMinorityHolders);
        
        if(DEBUG)
        {
            System.out.println("---------------------");
            System.out.print(myName + " Before Maj: ");
            printHolders(myMajorityHolders);
            System.out.print(myName + " Before Min: ");
            printHolders(myMinorityHolders);
        }

        ArrayList<Player> temp = new ArrayList<Player>();
        temp.addAll(myMajorityHolders);
        temp.addAll(myMinorityHolders);

        if(!temp.contains(p))
        {
            temp.add(p);
        }

        if(DEBUG)
        {
            System.out.print("Contenders: ");
            printHolders(temp);
        }

        for(Player pl : temp)
        {
            pl.setMajorityHolder(myName, false);
            pl.setMinorityHolder(myName, false);
        }

        myMajorityHolders.clear();
        myMinorityHolders.clear();

        int currentMajorityShares;
        int currentMinorityShares;

        for(Player pl : temp)
        {
            if(DEBUG)
            {
                System.out.print(myName + " During Maj: ");
                printHolders(myMajorityHolders);
                System.out.print(myName + " During Min: ");
                printHolders(myMinorityHolders);
            }      

            if(!myMajorityHolders.isEmpty())
            {
                 currentMajorityShares = myMajorityHolders.get(0).getShares(myName);
            }
            else
            {
                 currentMajorityShares = 0;
            }

            if(!myMinorityHolders.isEmpty())
            {
                 currentMinorityShares = myMinorityHolders.get(0).getShares(myName);
            }
            else
            {
                 currentMinorityShares = 0;
            }

            if(pl.getShares(myName) >= currentMajorityShares)
            {
                if(pl.getShares(myName) > currentMajorityShares)
                {
                    myMinorityHolders.clear();
                    myMinorityHolders.addAll(myMajorityHolders);
                    myMajorityHolders.clear();
                }

                if(!myMajorityHolders.contains(pl))
                {
                    myMajorityHolders.add(pl);
                }
            }
            else if(pl.getShares(myName) >= currentMinorityShares
                 && pl.getShares(myName) < currentMajorityShares)
            {
                if(pl.getShares(myName) > currentMinorityShares)
                {
                    myMinorityHolders.clear();
                }

                if(!myMinorityHolders.contains(pl))
                {
                    myMinorityHolders.add(pl);
                }
            }

            if(DEBUG)
            {
                System.out.print(myName + " During Maj: ");
                printHolders(myMajorityHolders);
                System.out.print(myName + " During Min: ");
                printHolders(myMinorityHolders);
            }         
        }

        if(myMinorityHolders.isEmpty() && !myMajorityHolders.isEmpty())
        {
            myMinorityHolders.addAll(myMajorityHolders);
        }

        for(Player pl : myMajorityHolders)
        {
            pl.setMajorityHolder(myName, true);
            if(!oldMajorityHolders.contains(pl))
            {
                LogMaster.log(pl + " is now a majority leader of " + this + ".");
            }
        }

        for(Player pl : myMinorityHolders)
        {
            pl.setMinorityHolder(myName, true);
            if(!oldMinorityHolders.contains(pl))
            {
                LogMaster.log(pl + " is now a minority leader of " + this + ".");
            }
        }

        if(DEBUG)
        {
            System.out.print(myName + " After Maj: ");
            printHolders(myMajorityHolders);
            System.out.print(myName + " After Min: ");
            printHolders(myMinorityHolders);
        }   
    }

    //*SET METHODS*//

    public void setImage(Image im)
    {
        myImage = im;
    }

    public void setSize(int s)
    {
        mySize = s;
    }

    //*GET METHODS*//

    public int getSize()
    {
        return(mySize);
    }

    //Returns the price of 1 share of stock.
    public int getPrice() 
    {
        int price = 0;

        if(mySize < 6)
        {
            if(mySize == 0)
            {
                price = 0;
            }
            else
            {
                price = 100 * (mySize + myQuality);
            }
        }
        else if(6 <= mySize && mySize <= 10)
        {
            price = 600 + (myQuality * 100);
        }
        else if((11 <= mySize && mySize <= 20))
        {
            price = 700 + (myQuality * 100);
        }
        else if((21 <= mySize && mySize <= 30))
        {
            price = 800 + (myQuality * 100);
        }
        else if((31 <= mySize && mySize <= 40))
        {
            price = 900 + (myQuality * 100);
        }
        else if(41 <= mySize)
        {
            price = 1000 + (myQuality * 100);
        }

        return price;
    }

    public int getShares() 
    {
        return myShares;
    }

    public Image getImage()
    {
        return myImage;
    }

    public int getID()
    {
        return myID;
    }

    public String getInfo()
    {
        return(myName + " - ID: " + myID + " Shares: " + myShares +
               " Price per share: " + getPrice() +
               " Majority Holder: " + myMajorityHolders +
               " Minority Holder: " + myMinorityHolders +
               " Condemed?: " + isCompCondemned);
    }

    public String getMajorityHolders()
    {
        String s = "";
        int i = 0;

        for(;i < myMajorityHolders.size() - 1;i++)
        {
            s += myMajorityHolders.get(i).toString() + ", ";
        }

        s += myMajorityHolders.get(i).toString();

        return s;
    }

    public String getMinorityHolders()
    {
        String s = "";
        int i = 0;

        for(;i < myMinorityHolders.size() - 1;i++)
        {
            s += myMinorityHolders.get(i).toString() + ", ";
        }

        s += myMinorityHolders.get(i).toString();

        return s;
    }

    //*BOOLEAN METHODS*//

    public void isPlaced(boolean b)
    {
        isPlaced = b;
    }

    public boolean isPlaced()
    {
        return(isPlaced);
    }

    public void isSafe(boolean b)
    {
        isSafe = b;
    }

    public boolean isSafe()
    {
        return(isSafe);
    }

    public boolean isCondemned()
    {
        return(isCompCondemned);
    }

    //*DEBUG METHODS*//

    private void printHolders(ArrayList<Player> myHolders)
    {
        String print = "[";

        if(myHolders.size() > 0)
        {
            for(int ctr = 0; ctr < myHolders.size(); ctr++)
            {
                print += myHolders.get(ctr).toString() + "(" + myHolders.get(ctr).getShares(myName) + "), ";
            }

            print = print.substring(0, print.length() - 2);
        }
        print += "]";

        System.out.println(print);
    }
}