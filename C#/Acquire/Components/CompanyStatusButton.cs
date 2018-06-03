using System;
using System.Drawing;
using System.Windows.Forms;
using Acquire.Models;

namespace Acquire.Components
{
    public partial class CompanyStatusButton : UserControl
    {
        #region Public Member Variables

        /// <summary>
        /// The image associated with this company
        /// </summary>
        public Image ImageIcon { get; set; }

        /// <summary>
        /// The company associated with this status button
        /// </summary>
        public Company Company { get; set; }

        #endregion

        /// <summary>
        /// Initialize this company status button
        /// </summary>
        public CompanyStatusButton()
        {
            InitializeComponent();
        }

        #region Event Handlers

        /// <summary>
        /// Paints the custom content onto this user control
        /// </summary>
        /// 
        /// <param name="args">The arguments for the Paint event</param>
        protected override void OnPaint(PaintEventArgs args)
        {
            // Only paint if the company and the image have been set
            if (Company == null || ImageIcon == null)
            {
                return;
            }

            // Build a string representing the size and price of this company
            string size = $"Size: {Company.Size}";
            string price = Company.GetPrice().ToString();

            if (price.Equals("0"))
            {
                price = "-";
            }

            // If this company is size 11 or greater, it is safe
            if (Company.Size >= 11)
            {
                // Set the company to safe
                Company.IsSafe = true;

                // Update the size string
                size += " *SAFE*";
            }
            // Otherwise it isn't safe
            else
            {
                Company.IsSafe = false;
            }

            // Set this user control image to the stored image
            CompanyImage.Image = ImageIcon;
            // Update the name label
            NameLabel.Text = Company.Name;
            // Update the size label
            SizeLabel.Text = size;
            // Update the shares label
            SharesLabel.Text = $@"Shares left: {Company.Shares}";
            // Update the price label
            PriceLabel.Text = $@"Price: {price}";
        }

        /// <summary>
        /// Changes the user control's color to show it is being highlighted
        /// </summary>
        /// 
        /// <param name="oSender">The object sending this event</param>
        /// <param name="eArgs">The arguments for this event</param>
        private void CompanyStatusButton_MouseEnter(object oSender, EventArgs eArgs)
        {
            BackColor = Color.LightGray;
        }

        /// <summary>
        /// Changes this user control's color back to it's original color
        /// </summary>
        /// 
        /// <param name="oSender">The object sending this event</param>
        /// <param name="eArgs">The arguments for this event</param>
        private void CompanyStatusButton_MouseLeave(object oSender, EventArgs eArgs)
        {
            BackColor = Color.White;
        }

        /// <summary>
        /// Overrides the click event so that the parent component knows this has been clicked
        /// </summary>
        /// 
        /// <param name="oSender">The object sending this event</param>
        /// <param name="eArgs">The arguments for this event</param>
        private void CompanyStatusButton_Click(object oSender, EventArgs eArgs)
        {
            OnClick(eArgs);
        }

        #endregion
    }
}