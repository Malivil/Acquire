using System;
using System.Windows.Forms;
using Acquire.Frames;

namespace Acquire
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Create a new setup frame to get the players list
            AcquireSetupFrame asfFrame = new AcquireSetupFrame();
            Application.Run(asfFrame);

            // If we have players and we're supposed to start, start the game
            if (asfFrame.IsStarting && asfFrame.Players.Count > 1)
            {
                Application.Run(new AcquireFrame(asfFrame.Players));
            }
        }
    }
}
