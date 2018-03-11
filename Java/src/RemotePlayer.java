/* **DON'T CHANGE THIS HEADER**
 $Author: Mike Lebson $
 $Revision: 1.8 $
 $Date: 2007/03/16 15:38:02 $
 $Source: /cvs/repo/Acquire/src/RemotePlayer.java,v $
*/

public class RemotePlayer extends Player
{
    //*PRIVATE FIELDS*//

    private String myIP;

    //*CONSTRUCTORS*//

    public RemotePlayer(String name, String ip)
    {
        super(name);
        myIP = ip;
    }

    //*SET METHODS*//

    public void setIP(String ip)
    {
        myIP = ip;
    }

    //*GET METHODS*//

    public String getIP()
    {
        return(myIP);
    }

    public int getType()
    {
        return(Player.REMOTE_PLAYER);
    }
}