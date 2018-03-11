/* **DON'T CHANGE THIS HEADER**
 $Author: Mike Lebson $
 $Revision: 1.20 $
 $Date: 2007/05/14 20:45:41 $
 $Source: /cvs/repo/Acquire/src/CompanyStatusButton.java,v $
*/

import java.awt.Graphics;
import java.awt.Image;
import javax.swing.*;

public class CompanyStatusButton extends JButton
{
    //*PRIVATE FIELDS*//

    private Image myImage;
    private Company myCompany;
    private boolean hasBeenLogged = false;

    //*CONSTRUCTOR*//

    public CompanyStatusButton(Company c, Image im)
    {
        myImage = im;
        myCompany = c;
        setSize(100, 150);
    }

    //*OTHER METHODS*//

    public void paintComponent(Graphics g)
    {
        String mySize = "Size: " + myCompany.getSize(), price;
        int myPrice = myCompany.getPrice();

        if(myPrice == 0)
        {
            price = "-";
        }
        else
        {
            price = Integer.toString(myPrice);
        }

        if(myCompany.getSize() >= 11)
        {
            myCompany.isSafe(true);
            mySize += " *SAFE*";
            if(!hasBeenLogged)
            {
                LogMaster.log(myCompany + " is now safe.");
                hasBeenLogged = true;
            }
        }
        else
        {
            myCompany.isSafe(false);
        }

        g.clearRect(0, 0, 150, 150);
        g.drawImage(myImage, 20, 50, null);
        g.drawString(myCompany.toString(), 3, 15);
        g.drawString(mySize, 3, 25);
        g.drawString("Shares left: " + myCompany.getShares(), 3, 35);
        g.drawString("Price: " + price, 3, 45);
        updateUI();
    }

    //*GET METHODS*//

    public Company getCompany()
    {
        return(myCompany);
    }
    
    public Image getImage()
    {
        return(myImage);
    }
}
