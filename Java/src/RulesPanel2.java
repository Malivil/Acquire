/* **DON'T CHANGE THIS HEADER**
 $Author: Mike Lebson $
 $Revision: 1.2 $
 $Date: 2007/05/14 21:18:43 $
 $Source: /cvs/repo/Acquire/src/RulesPanel2.java,v $
*/

public class RulesPanel2 extends javax.swing.JPanel
{
    //*CONTRUCTOR*//

    public RulesPanel2()
    {
        initComponents();
    }

    //*OTHER METHODS*//

    /** This method is called from within the constructor to
     * initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is
     * always regenerated by the Form Editor.
     */
    // <editor-fold defaultstate="collapsed" desc=" Generated Code ">//GEN-BEGIN:initComponents
    private void initComponents() {
        rulesTitle = new javax.swing.JLabel();
        rules1a = new javax.swing.JLabel();
        rules1b = new javax.swing.JLabel();
        rules2a = new javax.swing.JLabel();
        rules2b = new javax.swing.JLabel();
        rules3a = new javax.swing.JLabel();
        rules3b = new javax.swing.JLabel();
        rules4 = new javax.swing.JLabel();
        rules5a = new javax.swing.JLabel();
        rules5b = new javax.swing.JLabel();
        rules6 = new javax.swing.JLabel();

        setMaximumSize(new java.awt.Dimension(575, 325));
        setMinimumSize(new java.awt.Dimension(575, 325));
        rulesTitle.setFont(new java.awt.Font("Tahoma", 0, 18));
        rulesTitle.setText("More Rules of Acquire");

        rules1a.setFont(new java.awt.Font("Tahoma", 0, 12));
        rules1a.setText("When two companies come into contact with eachother,");

        rules1b.setFont(new java.awt.Font("Tahoma", 0, 12));
        rules1b.setText("whichever company is larger takes over the smaller company(ies).");

        rules2a.setFont(new java.awt.Font("Tahoma", 0, 12));
        rules2a.setText("When two companies of the same size come into contact with eachother,");

        rules2b.setFont(new java.awt.Font("Tahoma", 0, 12));
        rules2b.setText("the current player decides which company is expanded.");

        rules3a.setFont(new java.awt.Font("Tahoma", 0, 12));
        rules3a.setText("A player is given a bonus when a company that they have the");

        rules3b.setFont(new java.awt.Font("Tahoma", 0, 12));
        rules3b.setText(" most or 2nd most amount of shares in is sold.");

        rules4.setFont(new java.awt.Font("Tahoma", 0, 12));
        rules4.setText("The player panel will show a (+) and/or a (-) if you are eligible for the bonus(es).");

        rules5a.setFont(new java.awt.Font("Tahoma", 0, 12));
        rules5a.setText("The game can be ended when there is only one company on the board and it is safe");

        rules5b.setFont(new java.awt.Font("Tahoma", 0, 12));
        rules5b.setText("or if there is a company of size 41 or greater on the board.");

        rules6.setFont(new java.awt.Font("Tahoma", 0, 12));
        rules6.setText("At the end of the game, whichever player has the most money when all shares are sold wins.");

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(this);
        this.setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                .addContainerGap(37, Short.MAX_VALUE)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addGap(10, 10, 10)
                        .addComponent(rules2b))
                    .addComponent(rules2a)
                    .addGroup(layout.createSequentialGroup()
                        .addGap(10, 10, 10)
                        .addComponent(rules1b))
                    .addComponent(rules3a)
                    .addGroup(layout.createSequentialGroup()
                        .addGap(10, 10, 10)
                        .addComponent(rules3b))
                    .addComponent(rules4)
                    .addGroup(layout.createSequentialGroup()
                        .addGap(10, 10, 10)
                        .addComponent(rules5b))
                    .addComponent(rules5a)
                    .addComponent(rules6)
                    .addComponent(rules1a))
                .addGap(23, 23, 23))
            .addGroup(layout.createSequentialGroup()
                .addGap(189, 189, 189)
                .addComponent(rulesTitle)
                .addContainerGap(213, Short.MAX_VALUE))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(rulesTitle)
                .addGap(33, 33, 33)
                .addComponent(rules1a)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rules1b)
                .addGap(4, 4, 4)
                .addComponent(rules2a)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rules2b)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rules3a)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rules3b)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rules4)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rules5a)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rules5b)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rules6)
                .addGap(57, 57, 57))
        );
    }// </editor-fold>//GEN-END:initComponents

    //*PRE-GENERATED FEILDS*//

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JLabel rules1a;
    private javax.swing.JLabel rules1b;
    private javax.swing.JLabel rules2a;
    private javax.swing.JLabel rules2b;
    private javax.swing.JLabel rules3a;
    private javax.swing.JLabel rules3b;
    private javax.swing.JLabel rules4;
    private javax.swing.JLabel rules5a;
    private javax.swing.JLabel rules5b;
    private javax.swing.JLabel rules6;
    private javax.swing.JLabel rulesTitle;
    // End of variables declaration//GEN-END:variables
}