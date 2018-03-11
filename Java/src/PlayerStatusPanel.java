/* **DON'T CHANGE THIS HEADER**
 $Author: Mike Lebson $
 $Revision: 1.25 $
 $Date: 2007/05/14 20:45:41 $
 $Source: /cvs/repo/Acquire/src/PlayerStatusPanel.java,v $
*/

import java.awt.Color;
import java.awt.Graphics;
import javax.swing.JPanel;

public class PlayerStatusPanel extends JPanel
{
    //*PRIVATE FIELDS*//

    private Player myPlayer = null;
    private boolean isHighlighted = false;

    //*CONSTRUCTOR*//

    public PlayerStatusPanel()
    {
    }

    //*GET METHODS*//
    
    public Player getPlayer()
    {
        return myPlayer;
    }
    
    //*SET METHODS*//
    
    public void setPlayer(Player p)
    {
        myPlayer = p;
    }
    
    public void setHighlighted(boolean highlight)
    {
        isHighlighted = highlight;
    }
    
    //*OTHER METHODS*//

    public void paintComponent(Graphics g)
    {
        String isAmerHolder = "", isLuxHolder = "", isTowHolder = "",
               isImpeHolder = "", isContHolder = "", isFestHolder = "",
               isWorldHolder = "";

        if(myPlayer == null)
        {
            setEnabled(false);
        }
        else
        {
            g.clearRect(0, 0, getWidth(), getHeight());

            if(isHighlighted)
            {
                g.setColor(Color.RED);
                g.drawRect(0, 0, getWidth() - 1, getHeight() - 1);
            }

            if(myPlayer.getShares("American") == 0)
            {
                isAmerHolder = "";
            }
            else
            {
                if(myPlayer.isMajorityHolder("American"))
                {
                    isAmerHolder += "(+)";
                }
                if (myPlayer.isMinorityHolder("American"))
                {
                    isAmerHolder += "(-)";
                }
            }

            if(myPlayer.getShares("Luxor") == 0)
            {
                isLuxHolder = "";
            }
            else
            {
                if(myPlayer.isMajorityHolder("Luxor"))
                {
                    isLuxHolder += "(+)";
                }
                if (myPlayer.isMinorityHolder("Luxor"))
                {
                    isLuxHolder += "(-)";
                }
            }

            if(myPlayer.getShares("Tower") == 0)
            {
                isTowHolder = "";
            }
            else
            {
                if(myPlayer.isMajorityHolder("Tower"))
                {
                    isTowHolder += "(+)";
                }
                if (myPlayer.isMinorityHolder("Tower"))
                {
                    isTowHolder += "(-)";
                }
            }

            if(myPlayer.getShares("Imperial") == 0)
            {
                isImpeHolder = "";
            }
            else
            {
                if(myPlayer.isMajorityHolder("Imperial"))
                {
                    isImpeHolder += "(+)";
                }
                if (myPlayer.isMinorityHolder("Imperial"))
                {
                    isImpeHolder += "(-)";
                }
            }

            if(myPlayer.getShares("Continental") == 0)
            {
                isContHolder = "";
            }
            else
            {
                if(myPlayer.isMajorityHolder("Continental"))
                {
                    isContHolder += "(+)";
                }
                if (myPlayer.isMinorityHolder("Continental"))
                {
                    isContHolder += "(-)";
                }
            }

            if(myPlayer.getShares("Festival") == 0)
            {
                isFestHolder = "";
            }
            else
            {
                if(myPlayer.isMajorityHolder("Festival"))
                {
                    isFestHolder += "(+)";
                }
                if (myPlayer.isMinorityHolder("Festival"))
                {
                    isFestHolder += "(-)";
                }
            }

            if(myPlayer.getShares("Worldwide") == 0)
            {
                isWorldHolder = "";
            }
            else
            {
                if(myPlayer.isMajorityHolder("Worldwide"))
                {
                    isWorldHolder += "(+)";
                }
                if (myPlayer.isMinorityHolder("Worldwide"))
                {
                    isWorldHolder += "(-)";
                }
            }

            g.drawString(myPlayer.toString(), 5, 15);
            g.drawString("$" + myPlayer.getMoney(), 5, 26);
            g.drawString("Amer: " + myPlayer.getShares("American") + isAmerHolder, 5, 37);
            g.drawString("Lux: " + myPlayer.getShares("Luxor") + isLuxHolder, 5, 48);
            g.drawString("Tow: " + myPlayer.getShares("Tower") + isTowHolder, 5, 59);
            g.drawString("Impe: " + myPlayer.getShares("Imperial") + isImpeHolder, 5, 70);
            g.drawString("Cont: " + myPlayer.getShares("Continental") + isContHolder, 5, 81);
            g.drawString("Fest: " + myPlayer.getShares("Festival") + isFestHolder, 5, 92);
            g.drawString("World: " + myPlayer.getShares("Worldwide") + isWorldHolder, 5, 103);
        }
    }
}