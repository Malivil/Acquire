/* **DON'T CHANGE THIS HEADER**
 $Author: Mike Lebson $
 $Revision: 1.91 $
 $Date: 2007/05/15 00:07:31 $
 $Source: /cvs/repo/Acquire/src/GridPanel.java,v $
*/

import java.awt.*;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;
import java.util.*;
import javax.swing.*;

public class GridPanel extends JPanel implements MouseListener
{
    //Set to true to enable debugging messages
    private boolean DEBUG = false;
    private boolean GAME_TEST = false;

    //*PRIVATE FIELDS*//

    private static Square[][] mySquares;
    private static ArrayList<Company> myCompanies;
    private static ArrayList<Company> myActiveComps;
    private Image towIm, festIm, amerIm, luxIm, worIm, contIm, impIm;
    private static Company lux, tow, fest, world, amer, cont, imp;
    private static MergeDialogFrame myMDF;
    private HashMap<String, Company> myCompanyNameMap;
    private static ArrayList<Square> myBag, mySquaresHeld;
    private HandPanel myHandPanel;
    private boolean isOver;

    //*CONSTRUCTOR*//

    public GridPanel()
    {
        mySquares = new Square[9][12];
        myCompanies = new ArrayList<Company>();
        myActiveComps = new ArrayList<Company>();
        lux = new Company("Luxor", 4, 0);
        tow = new Company("Tower", 5, 0);
        fest = new Company("Festival", 6, 1);
        world = new Company("Worldwide", 7, 1);
        amer = new Company("American", 8, 2);
        cont = new Company("Continental", 9, 2);
        imp = new Company("Imperial", 10, 2);
        myMDF = null;
        myCompanyNameMap = new HashMap<String, Company>();
        myBag = new ArrayList<Square>();
        mySquaresHeld = new ArrayList<Square>();
        isOver = false;

        Toolkit toolkit = Toolkit.getDefaultToolkit();
        towIm = toolkit.getImage(
            ClassLoader.getSystemResource("tow_icon.GIF"));
        luxIm = toolkit.getImage(
            ClassLoader.getSystemResource("lux_icon.GIF"));
        amerIm = toolkit.getImage(
            ClassLoader.getSystemResource("amer_icon.GIF"));
        festIm = toolkit.getImage(
            ClassLoader.getSystemResource("fest_icon.GIF"));
        worIm = toolkit.getImage(
            ClassLoader.getSystemResource("wor_icon.gif"));
        impIm = toolkit.getImage(
            ClassLoader.getSystemResource("imp_icon.GIF"));
        contIm = toolkit.getImage(
            ClassLoader.getSystemResource("cont_icon.GIF"));

        lux.setImage(luxIm);
        tow.setImage(towIm);
        fest.setImage(festIm);
        world.setImage(worIm);
        amer.setImage(amerIm);
        cont.setImage(contIm);
        imp.setImage(impIm);

        String letters = "ABCDEFGHI";
        String s;

        for(int i = 0; i < 9; i++)
        {
            for(int k = 0; k < 12; k++)
            {
                s = letters.charAt(i) + String.valueOf(k + 1);
                mySquares[i][k] = new Square(s, this);
                mySquares[i][k].setState(1);
            }
        }

        Square left = null, right = null, top = null, bottom = null;

        for(int i = 0; i < 9; i++)
        {
            for(int k = 0; k < 12; k++)
            {
                if(i != 0)
                {
                    left = mySquares[i-1][k];
                }
                else
                {
                    left = null;
                }

                if(i != 8)
                {
                    right = mySquares[i+1][k];
                }
                else
                {
                    right = null;
                }

                if(k != 0)
                {
                    top = mySquares[i][k-1];
                }
                else
                {
                    top = null;
                }

                if(k != 11)
                {
                    bottom = mySquares[i][k+1];
                }
                else
                {
                    bottom = null;
                }

                mySquares[i][k].setBounds(left, right, top, bottom);
            }
        }

        for(int i = 0; i < 9; i++)
        {
            for(int k = 0; k < 12; k++)
            {
                if(!arrayHasSquare(myBag, mySquares[i][k]))
                {
                    myBag.add(mySquares[i][k]);
                }
            }
        }

        shuffleBag();

        myCompanies.add(lux);
        myCompanies.add(tow);
        myCompanies.add(fest);
        myCompanies.add(world);
        myCompanies.add(amer);
        myCompanies.add(cont);
        myCompanies.add(imp);
        updateCompanyMap();

        addMouseListener(this);
    }

    //*OTHER METHODS*//

    public void paintComponent(Graphics g)
    {
        Font f = new Font("plain", 1, 16);

        for(int i = 0, x = 10; i < 9; i++, x+= 50)
        {
            for(int y = 10, k = 0; k < 12; k++, y+= 40)
            {
                int charVal;
                Square csq = mySquares[i][k];

                g.setColor(csq.getColor());
                g.clearRect(x, y, 45, 35);
                g.fillRect(x, y, 40, 30);

                if(csq.isHighlighted())
                {
                    if(DEBUG)
                    {
                        System.out.println(csq.toString());
                    }

                    g.setColor(Color.BLACK);
                    g.drawRect(x, y, 41, 31);
                    g.drawRect(x+1, y+1, 39, 29);
                }

                csq.setX(x);
                csq.setY(y);

                g.setColor(Color.WHITE);
                g.setFont(f);

            if(csq.toString().length() > 2)
            {
                charVal = (Character.getNumericValue(csq.toString().charAt(1)) * 10) + Character.getNumericValue(csq.toString().charAt(2));
            }
            else
            {
                charVal = Character.getNumericValue(csq.toString().charAt(1));
            }

            if(charVal > 9)
            {
                if(csq.toString().charAt(0) == 'I')
                {
                    g.drawString(csq.toString(), x+8, y+20);
                }
                else
                {
                    g.drawString(csq.toString(), x+5, y+20);
                }
            }
            else
            {
                if(csq.toString().charAt(0) == 'I')
                {
                    g.drawString(csq.toString(), x+13, y+20);
                }
                else
                {
                    g.drawString(csq.toString(), x+10, y+20);
                }
            }
            }
        }
    }

    public Object placeSquare(Square sq)
    {
        if(sq.canBePlaced())
        {
            if(sq.getState() != Square.STATE_DEAD)
            {
                if((sq.getLeft() == null || sq.getLeft().getState() < 3) &&
                   (sq.getRight() == null || sq.getRight().getState() < 3) &&
                   (sq.getTop() == null || sq.getTop().getState() < 3) &&
                   (sq.getBottom() == null || sq.getBottom().getState() < 3))
                {
                    sq.setState(3);
                    LogMaster.log(Game.getActivePlayer() + " placed a square at " + sq.toString() + ".");
                    sq.isHighlighted(false);
                    repaint();
                    return(1);
                }
                else
                {
                    ArrayList<Square> a = new ArrayList<Square>();

                    if(sq.getLeft() != null && (sq.getLeft().isPlaced() || sq.getLeft().isCompany()))
                    {
                        a.add(sq.getLeft());
                    }

                    if(sq.getRight() != null && (sq.getRight().isPlaced() || sq.getRight().isCompany()))
                    {
                        a.add(sq.getRight());
                    }

                    if(sq.getTop() != null && (sq.getTop().isPlaced() || sq.getTop().isCompany()))
                    {
                        a.add(sq.getTop());
                    }

                    if(sq.getBottom() != null && (sq.getBottom().isPlaced() || sq.getBottom().isCompany()))
                    {
                        a.add(sq.getBottom());
                    }

                    return(merge(a, sq));
                }
            }
            else
            {
                LogMaster.log("Two or more of the companies you are trying to merge are safe.");
                return(null);
            }
        }
        else
        {
            return(1);
        }
    }

    private Company merge(ArrayList<Square> a, Square instigator)
    {
        ArrayList<Company> options = new ArrayList<Company>();
        Company newCompany = null;
        ArrayList<Company> destroyedCompanies = new ArrayList<Company>();
        Square csq;
        ArrayList<Company> newlyDead = new ArrayList<Company>();
        ArrayList<Integer> newlyDeadSizes = new ArrayList<Integer>();
        int numOptions;
        boolean virgin = false;

        while(!a.isEmpty())
        {
            csq = a.remove(0);

            if(csq.isCompany())
            {
                if(newCompany == null)
                {
                    newCompany = csq.getCompany();
                    options.clear();
                    options.add(newCompany);
                    newlyDead.add(newCompany);
                    newlyDeadSizes.add(newCompany.getSize());
                }
                else if(csq.getCompany().getSize() > newCompany.getSize())
                {
                    newCompany = csq.getCompany();
                    destroyedCompanies.addAll(options);
                    options.clear();
                    options.add(newCompany);
                    newlyDead.add(newCompany);
                    newlyDeadSizes.add(newCompany.getSize());
                }
                else if(csq.getCompany().getSize() < newCompany.getSize())
                {
                    newlyDead.add(csq.getCompany());
                    destroyedCompanies.add(csq.getCompany());
                    newlyDeadSizes.add(csq.getCompany().getSize());
                }
                else if(csq.getCompany().getSize() == newCompany.getSize())
                {
                    if(!options.contains(newCompany))
                    {
                        options.add(newCompany);
                    }

                    if(!options.contains(csq.getCompany()))
                    {
                        options.add(csq.getCompany());
                    }

                    newlyDead.add(newCompany);
                    newlyDeadSizes.add(newCompany.getSize());
                    newlyDead.add(csq.getCompany());
                    newlyDeadSizes.add(csq.getCompany().getSize());
                }
            }
        }

        numOptions = options.size();

        if(numOptions == 0)
        {
            if(myCompanies.isEmpty())
            {
                LogMaster.log("This square can't be placed because no new companies can be created.");
                return null;
            }

            options.addAll(myCompanies);
            virgin = true;
        }

        if(numOptions == 1)
        {
            newCompany = options.get(0);
        }
        else
        {
            String[] sa = changeToNameArray(options);
            String s = null;
            Object o;

            if(DEBUG)
            {
                for(int ctr = 0; ctr < sa.length;ctr++)
                {
                    System.out.println(sa[ctr] + " ");
                }
            }

            o = JOptionPane.showInputDialog(null, "Choose a company to create/expand", "Company creation/merge", JOptionPane.QUESTION_MESSAGE, null, sa, sa[0]);

            if(o != null)
            {
                s = o.toString();
            }

            if(s != null)
            {
                newCompany = myCompanyNameMap.get(s);
                newCompany.condemn();
            }
            else
            {
               return null;
            }
        }

        if(DEBUG)
        {
            System.out.println(newCompany.toString());
            for(int ctr = 0; ctr < newlyDead.size(); ctr++)
            {
                System.out.println(" " + newlyDead.get(ctr).toString());
            }
        }

        if(newlyDead.size() > 0)
        {
            for(int compCtr = 0; compCtr < newlyDead.size(); compCtr++)
            {
				Company comp = newlyDead.get(compCtr);
                int compSize = newlyDeadSizes.get(compCtr);

                if(DEBUG)
                {
                    System.out.println(comp.toString().equals(newCompany.toString()));
                }

                if(!comp.toString().equals(newCompany.toString()))
                {
                    ArrayList<Player> activePlayers = Game.getPlayers();

                    for(int playerCtr = 0; playerCtr < activePlayers.size(); playerCtr++)
                    {
						Player player = activePlayers.get(playerCtr);

                        if(DEBUG)
                        {
                            System.out.println(player.toString() + ": " + player.getShares(comp));
                        }

                        if(player.getShares(comp) > 0)
                        {
                            LogMaster.log(Game.getActivePlayer() + " has destroyed " + comp.toString() + " to expand " + newCompany.toString() + ".");
                            myMDF = new MergeDialogFrame(newCompany, comp, compSize, player);
                            myMDF.setVisible(true);
                        }
                    }
                }
            }
        }

        LogMaster.log(Game.getActivePlayer() + " placed a square at " + instigator.toString() + ".");
        instigator.isHighlighted(false);
        repaint();

        if(!virgin)
        {
            destroyedCompanies.addAll(options);
        }

        destroyedCompanies.remove(newCompany);

        if(DEBUG)
        {
            System.out.println("destroyedCompanies is " + destroyedCompanies);
        }

        for(int i = 0; i < destroyedCompanies.size(); i++)
        {
            destroyedCompanies.get(i).unCondemn();
            destroyedCompanies.get(i).isPlaced(false);
            destroyedCompanies.get(i).setSize(0);
        }

        updateCompanies();

        if(DEBUG)
        {
            System.out.println("companies is " + myCompanies);
        }

        return(link(instigator, newCompany));
    }

    private Company link(Square sq, Company newCompany)
    {
        newCompany.add(sq);
        newCompany.setSize(newCompany.getSize() + 1);
        Player active = Game.getActivePlayer();
        boolean isNew = true;

        for(int ctr = 0; ctr < myActiveComps.size(); ctr++)
        {
            if(myActiveComps.get(ctr).toString().equals(newCompany.toString()))
            {
                isNew = false;
            }
        }

        if(isNew)
        {
            myActiveComps.add(newCompany);
        }

        if(DEBUG)
        {
            System.out.println("activeComps is " + myActiveComps);
        }

        if(!newCompany.isPlaced())
        {
            active.giveMoney(newCompany.getPrice());
            active.buyShare(newCompany, true);
            LogMaster.log(active + " has formed the " + newCompany + " company.");
            AcquireFrame.getCSB(newCompany).updateUI();
            newCompany.isPlaced(true);
        }

        Game.endGameCheck();
        sq.setCompany(newCompany);

        if((sq.getLeft() != null) && (sq.getLeft().getCompany() != newCompany) && (sq.getLeft().getState() > 2))
        {
            newCompany = link(sq.getLeft(), newCompany);
        }

        if((sq.getRight() != null) && (sq.getRight().getCompany() != newCompany) && (sq.getRight().getState() > 2))
        {
            newCompany = link(sq.getRight(), newCompany);
        }

        if((sq.getTop() != null) && (sq.getTop().getCompany() != newCompany) && (sq.getTop().getState() > 2))
        {
            newCompany = link(sq.getTop(), newCompany);
        }

        if((sq.getBottom() != null) && (sq.getBottom().getCompany() != newCompany) && (sq.getBottom().getState() > 2))
        {
            newCompany = link(sq.getBottom(), newCompany);
        }

        return newCompany;
    }

    private void updateCompanyMap()
    {
        for(int i = 0; i < myCompanies.size(); i++)
        {
            myCompanyNameMap.put(myCompanies.get(i).toString(), myCompanies.get(i));
        }
    }

    private String[] changeToNameArray(ArrayList<Company> c)
    {
        int s = c.size();
        String[] sa = new String[s];

        for(int i = 0; i < s; i++)
        {
            sa[i] = c.get(i).toString();
        }

        return sa;
    }

    private void updateCompanies()
    {
        myCompanies.clear();

        if(!lux.isCondemned())
        {
            myCompanies.add(lux);
        }

        if(!tow.isCondemned())
        {
            myCompanies.add(tow);
        }

        if(!fest.isCondemned())
        {
            myCompanies.add(fest);
        }

        if(!world.isCondemned())
        {
            myCompanies.add(world);
        }

        if(!amer.isCondemned())
        {
            myCompanies.add(amer);
        }

        if(!cont.isCondemned())
        {
            myCompanies.add(cont);
        }

        if(!imp.isCondemned())
        {
            myCompanies.add(imp);
        }
    }

    private void checkSquares()
    {
        for(int ctr = 0; ctr < 9; ctr++)
        {
            for(int ctr2 = 0; ctr2 < 12; ctr2++)
            {
                mySquares[ctr][ctr2].checkSquare();
            }
        }

        repaint();
    }

    public void takeSquare(Player p)
    {
        ArrayList<Square> sq = p.getSquares();

        while(sq.size() < 10)
        {
            if(!myBag.isEmpty())
            {
                if(myBag.get(0).canBePlaced())
                {
                    sq.add(myBag.remove(0));
                }
                else
                {
                    myBag.remove(0);
                }
            }
            else
            {
                break;
            }
        }
        
        p.setSquares(sq);
        shuffleBag();
    }

    private void shuffleBag()
    {
        if(!myBag.isEmpty())
        {
            Random randNumGen = new Random();
            int n = randNumGen.nextInt(myBag.size()), k = randNumGen.nextInt(myBag.size());

            for(int i = 0; i < 100; i++)
            {
                if(DEBUG)
                {
                    System.out.println("n " + n + "  <->  " + "k " + k);
                }

                Square temp = myBag.get(n);
                myBag.set(n, myBag.get(k));
                myBag.set(k, temp);

                n = randNumGen.nextInt(myBag.size());
                k = randNumGen.nextInt(myBag.size());
            }
        }
    }

    public void updateHeldSquares(Player p)
    {
        for(int i = 0;i < 9;i++)
        {
            for(int k = 0;k < 12; k++)
            {
                mySquares[i][k].isHighlighted(false);
            }
        }

        mySquaresHeld.clear();
        mySquaresHeld.addAll(p.getSquares());
        myHandPanel.setHand(mySquaresHeld, myBag.size());

        if(DEBUG)
        {
            printAllSquares(p);
        }

        for(Square e : mySquaresHeld)
        {
            for(int i = 0;i < 9;i++)
            {
                for(int k = 0;k < 12;k++)
                {
                    if(e.toString().equals(mySquares[i][k].toString()))
                    {
                        mySquares[i][k].isHighlighted(true);
                    }
                }
            }

            if(DEBUG)
            {
                System.out.println(e.toString());
                System.out.println(e.isHighlighted());
            }
        }

        repaint();
    }

    public void endGame()
    {
        for(int i = 0; i < 9;i++)
        {
            for(int k = 0;k < 12;k++)
            {
                mySquares[i][k].canBePlaced(false);
            }
        }
        isOver = true;
    }

    //*MOUSE METHODS*//

    public void mouseClicked(MouseEvent e) 
    {
    }

    public void mousePressed(MouseEvent e) 
    {
    }

    public void mouseReleased(MouseEvent e) 
    {
        Player p = Game.getActivePlayer();

        if(p.canPlaceSquare() || GAME_TEST)
        {
            int x = e.getX(), y = e.getY(), k = 0;
            Square sq;
            Company temp = null;

            for(int i = 0; i < 9; i++)
            {
               sq = mySquares[i][k];
               if(x >= sq.getX() && x < sq.getX() + 40)
                {
                    for(;k < 12; k++)
                    {
                        sq = mySquares[i][k];
                        if((y >= sq.getY() && y < sq.getY() + 30) && ((arrayHasSquare(mySquaresHeld, sq)) || GAME_TEST))
                        {
                                if(sq.canBePlaced())
                                {
                                    if(placeSquare(sq) != null)
                                    {
                                        p.canPlaceSquare(false);
                                        Game.endTurnCheck();
                                        ArrayList<Square> tempAr = p.getSquares();
                                        ArrayList<Square> tempAr2 = new ArrayList<Square>();

                                        for(int ctr = 0; ctr < tempAr.size(); ctr++)
                                        {
                                            if(!tempAr.get(ctr).toString().equals(sq.toString()))
                                            {
                                                tempAr2.add(tempAr.get(ctr));
                                            }
                                        }

                                        if(DEBUG)
                                        {
                                            printAllSquares(p);
                                        }

                                        p.setSquares(tempAr2);
                                        myHandPanel.setHand(tempAr2, myBag.size());
                                        myHandPanel.updateUI();

                                        if(DEBUG)
                                        {
                                            printAllSquares(p);
                                        }

                                        sq.canBePlaced(false);
                                    }
                                }
                                else
                                {
                                    if(isOver)
                                    {
                                        LogMaster.log("You cannot place a square, the game is over");
                                    }
                                    else
                                    {
                                        LogMaster.log("Cannot place a square there.");
                                    }
                                }
                                temp = sq.getCompany();
                                break;
                        }
                    }
                }
                k = 0;
            }

            if(temp != null)
            {
                AcquireFrame.getCSB(temp).updateUI();
            }

            AcquireFrame.getPlayersPanel().updateUI();
        }
        else
        {
            LogMaster.log("You have already placed a square this turn.");
        }

        checkSquares();
    }

    public void mouseEntered(MouseEvent e) 
    {
    }

    public void mouseExited(MouseEvent e) 
    {
    }

    //*GET METHODS*//

    public static MergeDialogFrame getMDF()
    {
        return(myMDF);
    }

    public Company getCompany(String name)
    {
        if(name.equals(lux.toString()))
        {
            return(lux);
        }
        else if(name.equals(tow.toString()))
        {
            return(tow);
        }
        else if(name.equals(fest.toString()))
        {
            return(fest);
        }
        else if(name.equals(world.toString()))
        {
            return(world);
        }
        else if(name.equals(amer.toString()))
        {
            return(amer);
        }
        else if(name.equals(cont.toString()))
        {
            return(cont);
        }
        else if(name.equals(imp.toString()))
        {
            return(imp);
        }
        else
        {
            throw(new NoSuchElementException());
        }   
    }

    public static ArrayList<Company> getComps()
    {
        ArrayList<Company> temp = new ArrayList<Company>();

        temp.add(tow);
        temp.add(lux);
        temp.add(world);
        temp.add(amer);
        temp.add(cont);
        temp.add(imp);
        temp.add(fest);

        return(temp);
    }

    public static ArrayList<Company> getActiveComps()
    {
        ArrayList<Company> temp = getComps();
        ArrayList<Company> temp2 = new ArrayList<Company>();

        for(int ctr = 0; ctr < temp.size(); ctr++)
        {
            if(temp.get(ctr).getSize() > 0)
            {
                temp2.add(temp.get(ctr));
            }
        }

        return(temp2);
    }

    //*SET METHODS*//

    public void setHandPanel(HandPanel hp)
    {
        myHandPanel = hp;
    }

    //*BOOLEAN METHODS*//

    private boolean arrayHasSquare(ArrayList<Square> al, Square sq)
    {
        boolean hasSquare = false;

        for(Square s : al)
        {
            if(DEBUG)
            {
                System.out.println(sq.toString() + " ?= " + s.toString() + " = " + s.toString().equals(sq.toString()));
            }

            if(!hasSquare)
            {
                if(s.toString().equals(sq.toString()))
                {
                    hasSquare = true;
                }
            }
        }

        return(hasSquare);
    }

    public boolean isOver()
    {
        return(isOver);
    }

    //*DEBUG METHODS*//

    private void printAllSquares(Player p)
    {
        System.out.println(p.toString() + " has " + p.getSquares().size());

        for(int ctr = 0; ctr < p.getSquares().size();ctr++)
        {
            System.out.print(p.getSquares().get(ctr).toString() + " ");
        }

        System.out.println();
    }
}
