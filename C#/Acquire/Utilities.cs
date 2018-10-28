using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
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

        /// <summary>
        /// Save all the data in the <paramref name="logBox"/> to a file using a save dialog
        /// </summary>
        ///
        /// <param name="logBox">The <see cref="ListBox"/> of messages to save</param>
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

        /// <summary>
        /// Parses the given <paramref name="text"/> into an <see cref="IPEndPoint"/>. Supports multiple forms.
        /// </summary>
        ///
        /// <remarks>
        /// From https://stackoverflow.com/a/31208009
        /// </remarks>
        ///
        /// <param name="text">The text form of the address to parse</param>
        ///
        /// <returns>An <see cref="IPEndPoint"/> parsed from the given <paramref name="text"/></returns>
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

        /// <summary>
        /// Gets an object of type <typeparamref name="T"/> by reading data coming in from the given <paramref name="connection"/>
        /// </summary>
        ///
        /// <typeparam name="T">The type of object being parsed from the <paramref name="connection"/></typeparam>
        ///
        /// <param name="connection">The connection containing the data to read and parse</param>
        ///
        /// <returns>An object of type <typeparamref name="T"/> parsed from the given <paramref name="connection"/></returns>
        public static T GetMessageFromConnection<T>(Connection connection)
            => GetTypeFromString<T>(GetMessageFromConnection(connection));

        /// <summary>
        /// Reads a message string from the <paramref name="connection"/>. Only supports <see cref="MessageMode.PrefixedLength"/> mode currently
        /// </summary>
        ///
        /// <param name="connection">The connection containing the data to read</param>
        ///
        /// <returns>A string message read from <paramref name="connection"/></returns>
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

        /// <summary>
        /// Creates a <see cref="NetworkMessage"/> from the given <paramref name="model"/> and <paramref name="type"/> and serializes it to JSON
        /// </summary>
        ///
        /// <param name="model">The data to serialize inside the message</param>
        /// <param name="type">The type of message to serialize</param>
        ///
        /// <returns>A JSON string representing a new <see cref="NetworkMessage"/> containing the given <paramref name="model"/> and <paramref name="type"/></returns>
        public static string GetNetworkMessageString(AcquireNetworkModel model, MessageType type)
            => JsonConvert.SerializeObject(new NetworkMessage(model, type), new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.Auto});

        /// <summary>
        /// Sends a JSON message made from the provided <paramref name="model"/> and <paramref name="type"/> to the given <paramref name="connection"/>
        /// </summary>
        ///
        /// <param name="connection">The connection to send the generated message to</param>
        /// <param name="model">The data to serialize inside the message</param>
        /// <param name="type">The type of message to serialize</param>
        public static void SendMessageToConnection(Connection connection, AcquireNetworkModel model, MessageType type)
        {
            SendMessageToConnection(connection, GetNetworkMessageString(model, type));
        }


        /// <summary>
        /// Sends a <see cref="message"/> to the given <paramref name="connection"/>. Sends using <see cref="MessageMode.PrefixedLength"/>
        /// </summary>
        ///
        /// <param name="connection">The connection to send the <paramref name="message"/> to</param>
        /// <param name="message">The data being sent</param>
        public static void SendMessageToConnection(Connection connection, string message)
        {
            if (connection.Mode != MessageMode.PrefixedLength)
            {
                throw new NotSupportedException("Only PrefixedLength is currently supported");
            }

            connection.Send($"{message.Length:0000}{message}");
        }

        #endregion

        #region Serialization

        /// <summary>
        /// Parses the given JSON <paramref name="message"/> into a new object of type <typeparamref name="T"/>
        /// </summary>
        ///
        /// <typeparam name="T">The type of object being parsed from the <paramref name="message"/></typeparam>
        ///
        /// <param name="message">The JSON message to parse</param>
        ///
        /// <returns>A new object of type <typeparamref name="T"/> parsed from the given JSON <paramref name="message"/></returns>
        public static T GetTypeFromString<T>(string message)
            => JsonConvert.DeserializeObject<T>(message, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

        #endregion

        #region UI

        /// <summary>
        /// Invokes an <paramref name="action"/> for a <paramref name="control"/> either directly or through a <see cref="MethodInvoker"/> if that <paramref name="control"/> is on a different thread
        /// </summary>
        ///
        /// <param name="control">The control which holds the context of the <paramref name="action"/> being executed</param>
        /// <param name="action">The action to execute in the correct thread context</param>
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

        #region Players

        /// <summary>
        /// Generates a unique form of the given <paramref name="name"/> based on the list of <paramref name="otherNames"/> provided.
        /// <para>
        /// Appends a number in parentheses to make a name unique (eg. "Name" becomes "Name (2)" or "Name (3)" if "Name (2)" is also taken already)
        /// </para>
        /// </summary>
        ///
        /// <param name="name">The name to make unique</param>
        /// <param name="otherNames">The list of other names that are already used</param>
        ///
        /// <returns>A unique form of the given <paramref name="name"/> based on the list of <paramref name="otherNames"/> provided.</returns>
        public static string GetUniqueName(string name, List<string> otherNames)
        {
            // Trim any existing numbers off the end of the name
            string originalName = name.Clone().ToString();
            Regex regex = new Regex("\\w* \\(\\d\\)+");
            if (regex.IsMatch(originalName))
            {
                originalName = originalName.Substring(0, originalName.LastIndexOf('(') - 1);
            }
            string newName = originalName.Clone().ToString();

            // Find a number to add to the end to make it unique
            for (int i = 2; otherNames.Contains(newName, StringComparer.OrdinalIgnoreCase); i++)
            {
                newName = $"{originalName} ({i})";
            }

            return newName;
        }

        #endregion
    }
}
