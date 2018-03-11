/* **DON'T CHANGE THIS HEADER**
 $Author: Mike Lebson $
 $Revision: 1.8 $
 $Date: 2007/03/16 15:38:01 $
 $Source: /cvs/repo/Acquire/src/LocalPlayer.java,v $
*/

public class LocalPlayer extends Player
{
    //*CONSTRUCTOR*//

    public LocalPlayer(String name)
    {
        super(name);
    }

    //*GET METHOD*//

    public int getType()
    {
        return(Player.LOCAL_PLAYER);
    }
}