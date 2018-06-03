using System.Drawing;
using System.Windows.Forms;

namespace Acquire.Frames
{
    public partial class AboutFrame : Form
    {
        /// <summary>
        /// Creates a new About frame and loads in the logo image
        /// </summary>
        public AboutFrame()
        {
            InitializeComponent();

            LogoBox.Image = Image.FromFile(@"Images/Acquire_Logo.png");
        }
    }
}
