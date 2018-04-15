﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace Acquire
{
    public static class Utilities
    {
        #region Logging

        public static void SaveLogFile(ListBox logBox)
        {
            // Create a new save dialog with the defaults
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                DefaultExt = "txt",
                Filter = @"Text files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            // Show it until the user closes it, only process if they say "OK"
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                // Get all the text from the log box as a single string
                string output = string.Join(string.Empty, logBox.Items.Cast<string>());

                // Write it out to the chosen file
                using (StreamWriter writer = new StreamWriter(saveDialog.FileName))
                {
                    writer.Write(output);
                    writer.Close();
                }
            }
        }

        #endregion

        #region Networking

        // From https://stackoverflow.com/a/31208009
        public static IPEndPoint ParseIPEndPoint(string text)
        {
            if (Uri.TryCreate(text, UriKind.Absolute, out Uri uri))
            {
                return new IPEndPoint(IPAddress.Parse(uri.Host), uri.Port < 0 ? 0 : uri.Port);
            }

            if (Uri.TryCreate($"tcp://{text}", UriKind.Absolute, out uri))
            {
                return new IPEndPoint(IPAddress.Parse(uri.Host), uri.Port < 0 ? 0 : uri.Port);
            }

            if (Uri.TryCreate($"tcp://[{text}]", UriKind.Absolute, out uri))
            {
                return new IPEndPoint(IPAddress.Parse(uri.Host), uri.Port < 0 ? 0 : uri.Port);
            }

            return null;
        }

        #endregion

        #region UI

        public static void InvokeOnControl(Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((MethodInvoker)delegate { action(); });
            }
            else
            {
                action();
            }
        }

        #endregion
    }
}
