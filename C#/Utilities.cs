﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Acquire.Enums;
using Acquire.NetworkModels;
using Newtonsoft.Json;
using SocketMessaging;

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

        public static T GetMessageFromConnection<T>(Connection connection)
            => JsonConvert.DeserializeObject<T>(GetMessageFromConnection(connection));

        public static string GetMessageFromConnection(Connection connection)
        {
            string message = connection.ReceiveMessageString();
            if (connection.Mode != MessageMode.PrefixedLength)
            {
                return message;
            }

            // Since we are using PrefixedLength, remove the length from the beginning of the string
            return connection.MessageEncoding.GetString(connection.MessageEncoding.GetBytes(message).Skip(4).ToArray());
        }

        public static string GetNetworkMessageString(AcquireNetworkModel model, IPEndPoint endpoint, MessageType type)
            => JsonConvert.SerializeObject(new NetworkMessage(model, endpoint, type));

        public static void SendMessageToConnection(Connection connection, string message)
        {
            if (connection.Mode != MessageMode.PrefixedLength)
            {
                throw new NotSupportedException("Only PrefixedLength is currently supported");
            }

            connection.Send($"{message.Length:0000}{message}");
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
