/* **DON'T CHANGE THIS HEADER**
 $Author: Mike Lebson $
 $Revision: 1.22 $
 $Date: 2007/04/30 16:07:26 $
 $Source: /cvs/repo/Acquire/src/AcquireSetupFrame.java,v $
*/

import java.util.*;
import javax.swing.JOptionPane;

public class AcquireSetupFrame extends javax.swing.JFrame
{
    //*PRIVATE FIELDS*//

    private static ArrayList<Player> myPlayers = new ArrayList<Player>();
    private static ArrayList<PlayerSetupPanel> myNotReadyPlayers = new ArrayList<PlayerSetupPanel>();
    private static AcquireFrame myFrame;
    private static javax.swing.JButton goButton;
    private PlayerSetupPanel player1, player2, player3, player4, player5, player6, player7, player8;
    private String[] names = {"Squall", "Obi-wan Kenobi", "Kerrigan", "Star Fox", "Bastila Shan", "Link", "Spartan 117", "Astaroth", "Andariel",  "Mario"};
    private boolean isRunnable = true;

    //*CONSTRUCTOR*//

    public AcquireSetupFrame()
    {
        initComponents();
        myFrame = new AcquireFrame();
    }

    //*OTHER METHODS*//

    // <editor-fold defaultstate="collapsed" desc=" initComponents() ">
    private void initComponents()
    {
        player1 = new PlayerSetupPanel(1);
        player5 = new PlayerSetupPanel(5);
        player2 = new PlayerSetupPanel(2);
        player6 = new PlayerSetupPanel(6);
        player3 = new PlayerSetupPanel(3);
        player7 = new PlayerSetupPanel(7);
        player4 = new PlayerSetupPanel(4);
        player8 = new PlayerSetupPanel(8);
        goButton = new javax.swing.JButton();

        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);
        setTitle("Acquire Setup");

        player1.setBorder(javax.swing.BorderFactory.createLineBorder(new java.awt.Color(0, 0, 0)));
        player2.setBorder(javax.swing.BorderFactory.createLineBorder(new java.awt.Color(0, 0, 0)));
        player3.setBorder(javax.swing.BorderFactory.createLineBorder(new java.awt.Color(0, 0, 0)));
        player4.setBorder(javax.swing.BorderFactory.createLineBorder(new java.awt.Color(0, 0, 0)));
        player5.setBorder(javax.swing.BorderFactory.createLineBorder(new java.awt.Color(0, 0, 0)));
        player6.setBorder(javax.swing.BorderFactory.createLineBorder(new java.awt.Color(0, 0, 0)));
        player7.setBorder(javax.swing.BorderFactory.createLineBorder(new java.awt.Color(0, 0, 0)));
        player8.setBorder(javax.swing.BorderFactory.createLineBorder(new java.awt.Color(0, 0, 0)));

        goButton.setText("Go!");
        goButton.addKeyListener(new java.awt.event.KeyAdapter() {
            public void keyPressed(java.awt.event.KeyEvent evt) {
                goButtonKeyPressed(evt);
            }
        });

        goButton.addMouseListener(new java.awt.event.MouseAdapter() {
            public void mouseReleased(java.awt.event.MouseEvent evt) {
                goButtonMouseReleased(evt);
            }
        });

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(goButton, javax.swing.GroupLayout.PREFERRED_SIZE, 183, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addGroup(layout.createSequentialGroup()
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                            .addComponent(player2, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(player1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(player5, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(player6, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)))
                    .addGroup(layout.createSequentialGroup()
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(player3, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(player4, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(player8, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(player7, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))))
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                    .addComponent(player1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(player5, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(player2, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(player6, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(player3, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(player7, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(player8, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(player4, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(goButton, javax.swing.GroupLayout.DEFAULT_SIZE, 27, Short.MAX_VALUE)
                .addContainerGap())
        );

        pack();
    }// </editor-fold>

    private void goButtonKeyPressed(java.awt.event.KeyEvent evt)
    {
        if(evt.getKeyCode() == evt.VK_ENTER)
        {
            goButtonMouseReleased(null);
        }
    }

    private void goButtonMouseReleased(java.awt.event.MouseEvent evt)
    {
        myPlayers.clear();
        myNotReadyPlayers.clear();

        if(player1.getPlayer() != null)
        {
            checkPlayerIP(player1);
        }

        if(player2.getPlayer() != null && isRunnable)
        {
            checkPlayerIP(player2);
        }

        if(player3.getPlayer() != null && isRunnable)
        {
            checkPlayerIP(player3);
        }

        if(player4.getPlayer() != null && isRunnable)
        {
            checkPlayerIP(player4);
        }

        if(player5.getPlayer() != null && isRunnable)
        {
            checkPlayerIP(player5);
        }

        if(player6.getPlayer() != null && isRunnable)
        {
            checkPlayerIP(player6);
        }

        if(player7.getPlayer() != null && isRunnable)
        {
            checkPlayerIP(player7);
        }

        if(player8.getPlayer() != null && isRunnable)
        {
            checkPlayerIP(player8);
        }

        if(!isRunnable)
        {
            isRunnable = true;
            return;
        }

        if(myPlayers.size() < 2)
        {
            JOptionPane.showMessageDialog(null, "There aren't enough players to start", "Not enough players", JOptionPane.INFORMATION_MESSAGE);
            return;
        }

        if(myNotReadyPlayers.size() > 0)
        {
            JOptionPane.showMessageDialog(null, "Not all of the players are ready", "Not all ready", JOptionPane.INFORMATION_MESSAGE);
            return;
        }

        Game g = new Game(myPlayers, myFrame);
        myFrame.setVisible(true);

        dispose();
    }

    private void checkPlayerIP(PlayerSetupPanel p)
    {
        if((p.getPlayerType() == Player.REMOTE_PLAYER) && (p.getIP().equals("IP Address")))
        {
            JOptionPane.showMessageDialog(null, "There is a remote player without an IP entered.\nPlease fix this before continuing.", "Need an IP", JOptionPane.WARNING_MESSAGE);
            isRunnable = false;
        }
        else
        {
            checkPlayer(p);
        }
    }

    private void checkPlayer(PlayerSetupPanel p)
    {
        if(p.isActive())
        {
            if(p.getPlayer().getType() == Player.LOCAL_PLAYER)
            {
                if(p.getPlayerName().equals("Player Name"))
                {
                    p.setPlayerName("Player" + p.getPlayerNum());
                }
            }
            else if(p.getPlayer().getType() == Player.AI_PLAYER)
            {
                int pos = (int)(Math.round(Math.random() * 9));

                while(names[pos] == null)
                {
                    pos = (int)(Math.round(Math.random() * 9));
                }

                p.setPlayerName(names[pos]);
                names[pos] = null;
            }
            else
            {
                if(p.getPlayerName().equals("Player Name"))
                {
                    p.setPlayerName("RemotePlayer" + p.getPlayerNum());
                }
            }

            if(p.isReady())
            {
                myPlayers.add(p.getPlayer());
            
                if(myNotReadyPlayers.contains(p))
                {
                    myNotReadyPlayers.remove(p);
                }
            }
            else
            {
                myNotReadyPlayers.add(p);
            }
        }
    }

    //*MAIN METHOD*//

    public static void main(String args[])
    {
        java.awt.EventQueue.invokeLater(new Runnable()
        {
            public void run()
            {
                new AcquireSetupFrame().setVisible(true);
            }
        });
    }

    //*GET METHODS*//

    public static AcquireFrame getAF()
    {
        return(myFrame);
    }

    public static javax.swing.JButton getGO()
    {
        return(goButton);
    }
}
