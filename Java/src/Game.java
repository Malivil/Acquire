/* **DON'T CHANGE THIS HEADER**
 $Author: Mike Lebson $
 $Revision: 1.45 $
 $Date: 2007/05/14 21:34:11 $
 $Source: /cvs/repo/Acquire/src/Game.java,v $
*/

import java.util.*;
import javax.swing.JOptionPane;

public class Game
{
    //Set to true to enable debugging messages.
    private static boolean DEBUG = false;

    //*PRIVATE FIELDS*//

    private static ArrayList<Player> myPlayers = new ArrayList<Player>();
    private static Player myActivePlayer;
    private static boolean isOver;
    private static AcquireFrame myAF;
    private static ArrayList<Company> myEndCompanies;
    private static PlayerStatusPanel[] myPSPs;

    //*CONSTRUCTOR*//

    public Game(ArrayList<Player> p, AcquireFrame AF)
    {
        myAF = AF;
        myPlayers = p;
        myActivePlayer = myPlayers.get(0);
        isOver = false;

        for(Player pl : myPlayers)
        {
            myAF.getGridPanel().takeSquare(pl);
        }

        myAF.getGridPanel().updateHeldSquares(myActivePlayer);
        myAF.fillPlayersPanel();
        myPSPs = myAF.getPSPs();
        myPSPs[0].setHighlighted(true);
        myPSPs[0].updateUI();
        LogMaster.log("It is " + myActivePlayer + "'s turn first.");
    }

    //*GET METHODS*//

    public static Player getActivePlayer()
    {
        return(myActivePlayer);
    }

    public static ArrayList<Player> getPlayers()
    {
        return(myPlayers);
    }

    //*OTHER METHODS*//

    public static void nextTurn()
    {
        int ctr = 0;

        myAF.getGridPanel().takeSquare(myActivePlayer);
        myActivePlayer.canPlaceSquare(true);
        myPlayers.add(myPlayers.remove(0));
        myActivePlayer = myPlayers.get(0);
        myAF.getGridPanel().updateHeldSquares(myActivePlayer);
        myActivePlayer.setNumBuysLeft(3);
        myAF.fillPlayersPanel();

        for(PlayerStatusPanel psp = myPSPs[ctr];ctr < myPSPs.length; ctr++)
        {
            Player p = myPSPs[ctr].getPlayer();

            if(p != null)
            {
                if(myPSPs[ctr].getPlayer().toString().equals(myActivePlayer.toString()))
                {
                    myPSPs[ctr].setHighlighted(true);             
                }
                else
                {
                    myPSPs[ctr].setHighlighted(false);
                }

                if(p.getSquares().isEmpty())
                {
                    p.canPlaceSquare(false);
                }
            }
            myPSPs[ctr].updateUI();
        }

        endTurnCheck();
        LogMaster.clearDynamicContent(true, true);
        LogMaster.log("It is now " + myActivePlayer.toString() + "'s turn.");
    }

    public static void endGameCheck()
    {
        ArrayList<Company> activeComps = GridPanel.getActiveComps();
        Object isEnd = 0;
        int temp = -1;

        if(DEBUG)
        {
            for(int ctr = 0; ctr < activeComps.size(); ctr++)
            {
                System.out.println("Size: " + activeComps.get(ctr).getSize() + " ");
            }
        }

        for(int ctr = 0; ctr < activeComps.size(); ctr++)
        {
            if(activeComps.get(ctr).getSize() >= 11)
            {
                if(!isEnd.equals(false))
                {
                    isEnd = true;
                }
                activeComps.get(ctr).isSafe(true);
            }
            else
            {
                isEnd = false;
            }

            if(activeComps.get(ctr).getSize() >= 41)
            {
                temp = ctr;
            }
        }

        if((temp != -1) && (activeComps.get(temp).getSize() >= 41))
        {
            isEnd = true;
        }

        if(DEBUG)
        {
            System.out.println("isEnd? " + isEnd);            
        }

        if(isEnd.equals(true) && !isOver)
        {
            myEndCompanies = activeComps;
            myAF.endGameButton.setEnabled(true);
        }
        else
        {
            myAF.endGameButton.setEnabled(false);
        }

        myAF.endGameButton.updateUI();
    }

    public static void endGame()
    {
        ArrayList<Company> comps = GridPanel.getComps();
        Player winner = null, smallest, highest;

        for(int pCtr = 0; pCtr < myPlayers.size(); pCtr++)
        {
            if(DEBUG)
            {
                System.out.println("endGame selling all shares from " + myPlayers.get(pCtr).toString());
            }

            for(int cCtr = 0; cCtr < comps.size(); cCtr++)
            {
                int playerShares = myPlayers.get(pCtr).getShares(comps.get(cCtr));

                if(playerShares > 0)
                {
                    myPlayers.get(pCtr).sellShares(comps.get(cCtr), playerShares, true, comps.get(cCtr).getSize());
                }
            }
        }

        smallest = myPlayers.get(0);
        highest = myPlayers.get(0);

        for(int ctr = 0;ctr < myPlayers.size(); ctr++)
        {
            if(DEBUG)
            {
                System.out.println("endGame traversing players. Player size: " + myPlayers.size());
            }

            if(myPlayers.get(ctr).getMoney() > highest.getMoney())
            {
                highest = myPlayers.get(ctr);
            }

            if(myPlayers.get(ctr).getMoney() < smallest.getMoney())
            {
                smallest = myPlayers.get(ctr);
            }
        }

        if(highest.getMoney() == smallest.getMoney())
        {
            winner = null;
        }
        else
        {
            winner = highest;
        }

        if(winner != null)
        {
            LogMaster.log(winner.toString() + " has won the game with $" + winner.getMoney() + "!");
            isOver = true;
        }
        else if(!isOver)
        {
            LogMaster.log("There was a tie! All players have $" + myPlayers.get(0).getMoney() + "!");
            isOver = true;
        }

        myAF.getGridPanel().endGame();
        myAF.endGameButton.setEnabled(false);
        myAF.endGameButton.updateUI();
        myAF.endTurnButton.setEnabled(false);
        myAF.endTurnButton.updateUI();
        myAF.updatePSPs();
    }

    public static void endTurnCheck()
    {
        if(myActivePlayer.canPlaceSquare())
        {
            myAF.endTurnButton.setEnabled(false);
            myAF.endGameButton.requestFocusInWindow();
        }
        else
        {
            if(DEBUG)
            {
                System.out.println(myAF.getGridPanel().isOver());
            }

            if(!myAF.getGridPanel().isOver())
            {
                myAF.endTurnButton.setEnabled(true);
                myAF.endTurnButton.requestFocusInWindow();
            }
       }
        myAF.endTurnButton.updateUI();
    }
}