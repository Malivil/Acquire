/* **DON'T CHANGE THIS HEADER**
 $Author: Colin $
 $Revision: 1.49 $
 $Date: 2007/05/17 00:45:22 $
 $Source: /cvs/repo/Acquire/src/Player.java,v $
*/

import java.util.ArrayList;
import java.util.NoSuchElementException;
import javax.swing.JOptionPane;

public class Player
{
    //*PUBLIC FIELDS*//

    public static final int LOCAL_PLAYER = 1, REMOTE_PLAYER = 2, AI_PLAYER = 3;

    //*PRIVATE FIELDS*//

    
    private boolean DEBUG = false;
    
    private static int numBuysLeft = 3;
    private String myName;
    private int myMoney = 6000;
    private boolean isWaiting = false, canPlaceSquare = true;
    private boolean isAmerMajorityHolder = false, isAmerMinorityHolder = false, 
                    isLuxMajorityHolder = false, isLuxMinorityHolder = false,
                    isTowMajorityHolder = false, isTowMinorityHolder = false,
                    isImpeMajorityHolder = false, isImpeMinorityHolder = false,
                    isContMajorityHolder = false, isContMinorityHolder = false,
                    isFestMajorityHolder = false, isFestMinorityHolder = false,
                    isWorMajorityHolder = false, isWorMinorityHolder = false;
    private int towShares = 0, luxShares = 0, amerShares = 0, impShares = 0;
    private int contShares = 0;


    private int festShares = 0, worShares = 0;
    private ArrayList<Square> mySquares = new ArrayList<Square>();
    private String[] shareS = {"share", "shares"};
    
    //*CONSTRUCTOR*//

    public Player(String name)
    {
        myName = name;
    }

    //*OTHER METHODS*//

    public void buyShare(Company c, boolean openingShare)
    {
        if(!canPlaceSquare || openingShare)
        {
            if(c.getShares() > 0)
            {
                if(c.getPrice() <= myMoney)
                {
                    myMoney -= c.buyShare(this);
                    int temp = getShares(c);

                    setShares(c, ++temp);
                    if(!openingShare)
                    {
                        LogMaster.dynLog(this + " bought " + LogMaster.dynamicContent(this.toString() + c.toString() + "Buy", LogMaster.STANDARD_NUM_PROG, 1, "Buy")
                                      + " " + LogMaster.dynamicContent(this.toString() + c.toString() + "ShareS", shareS, 1, "ShareS") + " of " + c + ".");
                    }
                    else
                    {
                        LogMaster.log(this + " was given one free share of " + c);
                    }
                    c.updateMajMinHolders(this);                    
                }
                else
                {
                    LogMaster.log("Not enough money to buy a share.");
                }
            }
            else
            {
                LogMaster.log("Not enough shares to buy.");
            }
        }
        else
        {
            LogMaster.log("Place a square first!");
        }
    }

    public void sellShare(Company c, boolean isMerging, int companySize)
    {
        int oldMoney = myMoney;
        if(!canPlaceSquare || isMerging)
        {
            if(getShares(c) > 0)
            {
                myMoney += c.sellShare(companySize);
                int temp = getShares(c);

                LogMaster.dynLog(this + " sold " + LogMaster.dynamicContent(this.toString() + c.toString() + "Sold", LogMaster.STANDARD_NUM_PROG, 1, "Sold")
                              + " " + LogMaster.dynamicContent(this.toString() + c.toString() + "ShareS", shareS, 1, "ShareS") + " of " + c + ".");

                setShares(c, --temp);
            }
            else
            {
                LogMaster.log("You don't have any shares of " + c + " to sell.");
            }
        }
        else
        {
            LogMaster.log("Place a square first!");
        }
        
        if(DEBUG)
        {
            LogMaster.debugLog(this + " sold a share for  " + String.valueOf(oldMoney - myMoney) + " money.");
        }
    }

    public void tradeShares(Company deadComp, Company liveComp, int num)
    {
        if((getShares(deadComp) / 2) > liveComp.getShares())
        {
            setShares(liveComp, (getShares(liveComp) + num));
            setShares(deadComp, (getShares(deadComp) - (num * 2)));

            LogMaster.log(this + " traded " + (num * 2) + " shares of " + deadComp + " for " + num + " shares of " + liveComp);
        }
        else
        {
            LogMaster.log(this + " traded " + getShares(deadComp) + " shares of " + deadComp + " for " + (getShares(deadComp) / 2) + " shares of " + liveComp);

            setShares(liveComp, (getShares(liveComp) + (getShares(deadComp) / 2)));
            setShares(deadComp, 0);
        }
    }

    public void sellShares(Company c, int num, boolean isMerging, int companySize)
    {
        for(int ctr = 0; ctr < num; ctr++)
        {
            sellShare(c, isMerging, companySize);
        }
    }

    public void giveMoney(int m)
    {
        myMoney += m;
        
        if(DEBUG)
        {
            LogMaster.debugLog(this + " was given " + m + " money.");
        }
    }

    public void status()
    {
        System.out.println("Name: " + myName + " Money: $" + myMoney);
        System.out.println("Number of shares in each company:");
        System.out.println("American: " + amerShares);
        System.out.println("Luxor: " + luxShares);
        System.out.println("Tower: " + towShares);
        System.out.println("Imperial: " + impShares);
        System.out.println("Continental: " + contShares);
        System.out.println("Festival: " + festShares);
        System.out.println("Worldwide: " + worShares);
    }

    public void Query()
    {
        isWaiting = true;
    }

    public void replyToQuery()
    {
        isWaiting = false;
        Game.nextTurn();
    }

    //*SET METHODS*//

    public void setName(String n)
    {
        myName = n;
    }

    public void setShares(Company c, int sh)
    {
        if(c.toString().equals("Tower"))
        {
            towShares = sh;
        }
        else if(c.toString().equals("American"))
        {
            amerShares = sh;
        }
        else if(c.toString().equals("Festival"))
        {
            festShares = sh;
        }
        else if(c.toString().equals("Continental"))
        {
            contShares = sh;
        }
        else if(c.toString().equals("Worldwide"))
        {
            worShares = sh;
        }
        else if(c.toString().equals("Imperial"))
        {
            impShares = sh;
        }
        else if(c.toString().equals("Luxor"))
        {
            luxShares = sh;
        }
        else
        {
            throw(new NoSuchElementException());
        }
    }
    
    public void setMajorityHolder(String cName, boolean mh)
    {
        if(cName.equals("Tower"))
        {
            isTowMajorityHolder = mh;
        }
        else if(cName.equals("American"))
        {
            isAmerMajorityHolder = mh;
        }
        else if(cName.equals("Festival"))
        {
            isFestMajorityHolder = mh;
        }
        else if(cName.equals("Continental"))
        {
            isContMajorityHolder = mh;
        }
        else if(cName.equals("Worldwide"))
        {
            isWorMajorityHolder = mh;
        }
        else if(cName.equals("Imperial"))
        {
            isImpeMajorityHolder = mh;
        }
        else if(cName.equals("Luxor"))
        {
            isLuxMajorityHolder = mh;
        }
        else
        {
            throw(new NoSuchElementException());
        } 
    }
    
    public void setMinorityHolder(String cName, boolean mh)
    {
        if(cName.equals("Tower"))
        {
            isTowMinorityHolder = mh;
        }
        else if(cName.equals("American"))
        {
            isAmerMinorityHolder = mh;
        }
        else if(cName.equals("Festival"))
        {
            isFestMinorityHolder = mh;
        }
        else if(cName.equals("Continental"))
        {
            isContMinorityHolder = mh;
        }
        else if(cName.equals("Worldwide"))
        {
            isWorMinorityHolder = mh;
        }
        else if(cName.equals("Imperial"))
        {
            isImpeMinorityHolder = mh;
        }
        else if(cName.equals("Luxor"))
        {
            isLuxMinorityHolder = mh;
        }
        else
        {
            throw(new NoSuchElementException());
        } 
    }

    public void setSquares(ArrayList<Square> sq)
    {
        mySquares = sq;
    }

    public void setNumBuysLeft(int nbl)
    {
        numBuysLeft = nbl;
    }

    //*GET METHODS*//

    public String toString()
    {
        return(myName);
    }

    public int getMoney()
    {
        return(myMoney);
    }

    public int getShares(String cName)
    {
        if(cName.equals("Tower"))
        {
            return(towShares);
        }
        else if(cName.equals("American"))
        {
            return(amerShares);
        }
        else if(cName.equals("Festival"))
        {
            return(festShares);
        }
        else if(cName.equals("Continental"))
        {
            return(contShares);
        }
        else if(cName.equals("Worldwide"))
        {
            return(worShares);
        }
        else if(cName.equals("Imperial"))
        {
            return(impShares);
        }
        else if(cName.equals("Luxor"))
        {
            return(luxShares);
        }
        else
        {
            throw(new NoSuchElementException());
        } 
    }

    public int getShares(Company c)
    {
        if(c.toString().equals("Tower"))
        {
            return(towShares);
        }
        else if(c.toString().equals("American"))
        {
            return(amerShares);
        }
        else if(c.toString().equals("Festival"))
        {
            return(festShares);
        }
        else if(c.toString().equals("Continental"))
        {
            return(contShares);
        }
        else if(c.toString().equals("Worldwide"))
        {
            return(worShares);
        }
        else if(c.toString().equals("Imperial"))
        {
            return(impShares);
        }
        else if(c.toString().equals("Luxor"))
        {
            return(luxShares);
        }
        else
        {
            throw(new NoSuchElementException());
        }   
    }

    public int getType()
    {
        return(1);
    }

    public ArrayList<Square> getSquares()
    {
        return(mySquares);
    }

    public int getNumBuysLeft()
    {
        return(numBuysLeft);
    }

    public String getIP()
    {
        return(null);
    }

    //*BOOLEAN METHODS*//

    public boolean isWaiting()
    {
        return(isWaiting);
    }

    public boolean canPlaceSquare()
    {
        return(canPlaceSquare);
    }

    public void canPlaceSquare(boolean b)
    {
        canPlaceSquare = b;
    }

    public boolean isMajorityHolder(String cName)
    {
        if(cName.equals("Tower"))
        {
            return isTowMajorityHolder;
        }
        else if(cName.equals("American"))
        {
            return isAmerMajorityHolder;
        }
        else if(cName.equals("Festival"))
        {
            return isFestMajorityHolder;
        }
        else if(cName.equals("Continental"))
        {
            return isContMajorityHolder;
        }
        else if(cName.equals("Worldwide"))
        {
            return isWorMajorityHolder;
        }
        else if(cName.equals("Imperial"))
        {
            return isImpeMajorityHolder;
        }
        else if(cName.equals("Luxor"))
        {
            return isLuxMajorityHolder;
        }
        else
        {
            throw(new NoSuchElementException());
        } 
    }
    
    public boolean isMinorityHolder(String cName)
    {
        if(cName.equals("Tower"))
        {
            return isTowMinorityHolder;
        }
        else if(cName.equals("American"))
        {
            return isAmerMinorityHolder;
        }
        else if(cName.equals("Festival"))
        {
            return isFestMinorityHolder;
        }
        else if(cName.equals("Continental"))
        {
            return isContMinorityHolder;
        }
        else if(cName.equals("Worldwide"))
        {
            return isWorMinorityHolder;
        }
        else if(cName.equals("Imperial"))
        {
            return isImpeMinorityHolder;
        }
        else if(cName.equals("Luxor"))
        {
            return isLuxMinorityHolder;
        }
        else
        {
            throw(new NoSuchElementException());
        } 
    }
}