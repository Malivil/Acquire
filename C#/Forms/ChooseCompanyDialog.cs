using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Acquire.Models;

namespace Acquire.Forms
{
    public partial class ChooseCompanyDialog : Form
    {
        #region Public Variables

        /// <summary>
        /// The company that has been chose.
        /// Made public so that it can be retrieved after the window is closed.
        /// </summary>
        public Company ChosenCompany { get; set; }

        #endregion

        #region Private Member Variables

        // The list of companies that the player is choosing from
        private readonly List<Company> companies;

        #endregion

        /// <summary>
        /// Creates a new dialog box with a dropdown list of the companies in companies
        /// </summary>
        /// 
        /// <param name="companies">The list of companies for the player to choose from</param>
        public ChooseCompanyDialog(List<Company> companies)
        {
            InitializeComponent();

            // If the list is null for some reason, bail
            if (companies == null)
            {
                return;
            }

            // Otherwise keep it
            this.companies = companies;

            // Add each company to the list
            foreach (Company coCompany in companies)
            {
                CompanyBox.Items.Add(coCompany.GetName());
            }

            // Make sure the first one is selected by default
            CompanyBox.SelectedIndex = 0;
        }

        #region Event Handlers

        /// <summary>
        /// Closes the window
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void CancelButton_Click(object sender, EventArgs args)
        {
            Close();
        }

        /// <summary>
        /// Finds the chosen company and stores it to be gotten later
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void OKButton_Click(object sender, EventArgs args)
        {
            for (int i = 0; i < companies.Count && ChosenCompany == null; i++)
            {
                if (companies[i].GetName().Equals(CompanyBox.Items[CompanyBox.SelectedIndex]))
                {
                    ChosenCompany = companies[i];
                }
            }

            Close();
        }

        #endregion
    }
}
