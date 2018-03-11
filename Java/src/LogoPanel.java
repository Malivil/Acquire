/* **DON'T CHANGE THIS HEADER**
 $Author: Mike Lebson $
 $Revision: 1.3 $
 $Date: 2007/05/03 13:58:49 $
 $Source: /cvs/repo/Acquire/src/LogoPanel.java,v $
*/

import java.awt.*;

public class LogoPanel extends javax.swing.JButton
{
    //*PRIVATE FIELDS*//

    private Toolkit toolkit = Toolkit.getDefaultToolkit();
    private Image logo = toolkit.getImage(ClassLoader.getSystemResource("acquire_logo.GIF"));

    //*CONSTRUCTOR*//

    public LogoPanel()
    {
    }

    //*OTHER METHODS*//

    public void paintComponent(Graphics g)
    {
        g.clearRect(0, 0, 230, 100);
        g.drawImage(logo, 0, -2, null);
        updateUI();
    }
}
