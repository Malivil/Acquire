/* **DON'T CHANGE THIS HEADER**
 $Author: Colin $
 $Revision: 1.9 $
 $Date: 2007/04/23 01:13:28 $
 $Source: /cvs/repo/Acquire/src/AIPlayer.java,v $
*/

public class AIPlayer extends Player
{

    //*CONSTRUCTORS*//

    public AIPlayer()
    {
        super("Computer");
    }

    public AIPlayer(String n)
    {
        super(n);
    }

    //*GET METHODS*//

    public int getType()
    {
        return(AI_PLAYER);
    }
}
