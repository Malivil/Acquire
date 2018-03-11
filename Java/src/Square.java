/* **DON'T CHANGE THIS HEADER**
 $Author: Mike Lebson $
 $Revision: 1.16 $
 $Date: 2007/05/06 18:01:09 $
 $Source: /cvs/repo/Acquire/src/Square.java,v $
*/

import java.awt.Color;
import java.util.ArrayList;

public class Square
{
    //*PRIVATE FIELDS*//

    private final String myID;
    private Color myColor;
    private int myState;
    private GridPanel myParent;
    private int myX, myY;
    private Square myLeftSq, myRightSq, myTopSq, myBottomSq;
    private boolean isPlaced = false, isCompany = false, canBePlaced = true, isHighlighted = false;
    private Company myCompany = null;

    //*PUBLIC FIELDS*//

    public static final int STATE_DEAD = 0;

    //*CONSTRUCTOR*//
    
    public Square(String i, GridPanel mp) 
    {
        myID = i;
        myParent = mp;
        setState(1);
    }

    //*OTHER METHODS*//

    //Sets references to the squares that surround *this*
    public void setBounds(Square leftSq, Square rightSq, Square topSq, Square bottomSq)
    { 
        myLeftSq = leftSq;
        myRightSq = rightSq;
        myTopSq = topSq;
        myBottomSq = bottomSq;
    }

    public String toString()
    {
        return myID;
    }

    public void checkSquare()
    {
        //Number of safe companies that surround this square.
        int numSafeComps = 0;
        //ArrayList of the companies that surround this square.
        ArrayList<Company> surroundingComps = new ArrayList<Company>();

        if(getLeft() != null && getLeft().isCompany() && getLeft().getCompany().isSafe())
        {
            surroundingComps.add(getLeft().getCompany());
            numSafeComps += 1;
        }

        if(getRight() != null && getRight().isCompany() && getRight().getCompany().isSafe())
        {
            if(surroundingComps.indexOf(getRight().getCompany()) < 0)
            {
                surroundingComps.add(getRight().getCompany());
                numSafeComps += 1;
            }
        }

        if(getTop() != null && getTop().isCompany() && getTop().getCompany().isSafe())
        {
            if(surroundingComps.indexOf(getTop().getCompany()) < 0)
            {
                surroundingComps.add(getTop().getCompany());
                numSafeComps += 1;
            }
        }

        if(getBottom() != null && getBottom().isCompany() && getBottom().getCompany().isSafe())
        {
            if(surroundingComps.indexOf(getBottom().getCompany()) < 0)
            {
                surroundingComps.add(getBottom().getCompany());
                numSafeComps += 1;
            }
        }

        if(numSafeComps >= 2)
        {
            setState(STATE_DEAD);
        }
    }
    
    //*SET METHODS*//

    public void setState(int i)
    {
        myState = i;
        switch(i)
        {
            case STATE_DEAD: //Can never be placed
                myColor = Color.WHITE;
                canBePlaced = false;
                isPlaced = true ;
                isCompany = false;
                break;            
            case 1: //Blank
                myColor = Color.GRAY;
                isPlaced = false;
                isCompany = false;
                canBePlaced = true;
                break;
            case 2: //Option
                myColor = Color.GREEN;
                isCompany = false;
                isPlaced = false;
                canBePlaced = true;
                break;
            case 3: //Placed
                myColor = Color.BLACK;
                isCompany = false;
                isPlaced = true;
                break;
            case 4: //Luxor
                myColor = new Color(227, 43, 79);
                isCompany = true;
                isPlaced = false;
                break;
            case 5: //Tower
                myColor = new Color(254, 206, 18);
                isCompany = true;
                isPlaced = false;
                break;
            case 6: //Festival
                myColor = new Color(4, 180, 76);
                isCompany = true;
                isPlaced = false;
                break;
            case 7: //Worldwide
                myColor = new Color(142, 97, 40);
                isCompany = true;
                isPlaced = false;
                break;
            case 8: //American
                myColor = new Color(13, 39, 123);
                isCompany = true;
                isPlaced = false;
                break;
            case 9: //Continental
                myColor = new Color(20, 164, 204);
                isCompany = true;
                isPlaced = false;
                break;
            case 10: //Imperial
                myColor = new Color(220, 28, 116);
                isCompany = true;
                isPlaced = false;
                break;
        }   
        myParent.repaint();
    }

    public void setX(int x)
    {
        myX = x;
    }

    public void setY(int y)
    {
        myY = y;
    }

    public void setCompany(Company c)
    {
        myCompany = c;
        setState(myCompany.getID());
    }
    
    //*GET METHODS*//

    public int getState()
    {
        return myState;
    }

    public int getX()
    {
        return myX;
    }

    public int getY()
    {
        return myY;
    }

    public Company getCompany()
    {
        return myCompany;
    }

    public Square getLeft()
    {
        return myLeftSq;
    }

    public Square getRight()
    {
        return myRightSq;
    }

    public Square getTop()
    {
        return myTopSq;
    }

    public Square getBottom()
    {
        return myBottomSq;
    }

    public Color getColor()
    {
        return myColor;
    }
    
    //*BOOLEAN METHODS*//

    public boolean isPlaced()
    {
        return isPlaced;
    }

    public boolean isCompany()
    {
        return isCompany;
    }

    public boolean canBePlaced()
    {
        return(canBePlaced);
    }

    public void canBePlaced(boolean b)
    {
        canBePlaced = b;
    }

    public boolean isHighlighted()
    {
        return(isHighlighted);
    }

    public void isHighlighted(boolean h)
    {
        isHighlighted = h;
    }
}