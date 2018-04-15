using System;
using System.Net;
using System.Windows.Forms;
using Acquire.Enums;
using Acquire.NetworkModels;
using Newtonsoft.Json;
using SocketMessaging;
using TcpClient = SocketMessaging.TcpClient;

namespace Acquire.Forms
{
    public partial class RemoteConnectForm : Form
    {
        private TcpClient client;

        public RemoteConnectForm(IPEndPoint endpoint)
        {
            InitializeComponent();
            InitiateConnection(endpoint);
        }

        private void InitiateConnection(IPEndPoint endpoint)
        {
            RemoteStatusBox.Items.Add($"Connecting to {endpoint.Address}:{endpoint.Port}...");
            client = TcpClient.Connect(endpoint.Address, endpoint.Port);
            client.SetDelimiter('|');
            client.ReceivedMessage += Client_ReceivedMessage;
        }

        public TcpClient GetClient()
        {
            if (DialogResult != DialogResult.OK)
            {
                return null;
            }

            return client;
        }

        #region Event Handlers

        private void Client_ReceivedMessage(object sender, EventArgs e)
        {
            string result = client.ReceiveMessageString();
            NetworkMessage message = JsonConvert.DeserializeObject<NetworkMessage>(result);
            switch (message.MessageType) {
                case MessageType.Connect:
                    RemoteStatusBox.Items.Add("Connected! Requesting player list...");
                    // TODO: Request player list
                    break;
                case MessageType.PlayerListResponse:
                    RemoteStatusBox.Items.Add("Received list of players");
                    // TODO: Do something with the player list
                    // TODO: Start the game?
                    break;
                default:
                    RemoteStatusBox.Items.Add($"ERROR: Received unknown message type! `{message.MessageType}`");
                    break;
            }

        }

        private void CancelConnectButton_Click(object sender, EventArgs e)
        {
            client.Close();
        }

        #endregion
    }
}
