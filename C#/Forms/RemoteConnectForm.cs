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
        private readonly IPEndPoint endpoint;

        public RemoteConnectForm(IPEndPoint endpoint)
        {
            this.endpoint = endpoint;

            InitializeComponent();
            InitiateConnection();
        }

        private void InitiateConnection()
        {
            AddRemoteStatusMessage($"Connecting to {endpoint.Address}:{endpoint.Port}...");
            try
            {
                client = TcpClient.Connect(endpoint.Address, endpoint.Port);
                client.ReceivedMessage += Client_ReceivedMessage;
                client.Disconnected += Client_Disconnected;
                client.SetMode(MessageMode.PrefixedLength);
            }
            catch (Exception ex)
            {
                AddRemoteStatusMessage($"ERROR: Connection to host failed: {ex.Message}");
            }
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
            NetworkMessage message = Utilities.GetMessageFromConnection<NetworkMessage>(client);
            switch (message.MessageType) {
                case MessageType.Connect:
                    // Just sit waiting until the server sends us data
                    AddRemoteStatusMessage("Connected!");
                    break;
                case MessageType.PlayerListResponse:
                    AddRemoteStatusMessage("Received list of players");
                    PlayerList players = message.Data as PlayerList;
                    // TODO: Do something with the player list
                    // Start the game
                    StartGame();
                    break;
                default:
                    AddRemoteStatusMessage($"ERROR: Received unknown message type! `{message.MessageType}`");
                    break;
            }
        }

        private void Client_Disconnected(object sender, EventArgs e)
        {
            AddRemoteStatusMessage($"Lost connection from {endpoint.Address}:{endpoint.Port}");
        }

        private void CancelConnectButton_Click(object sender, EventArgs e)
        {
            client?.Close();
        }

        #endregion

        #region Helpers

        private void StartGame()
        {
            AddRemoteStatusMessage("Starting game...");
            // Stop listening for messages with this handler
            // The client will be passed on to handle the game messages
            client.ReceivedMessage -= Client_ReceivedMessage;
            client.Disconnected -= Client_Disconnected;
            DialogResult = DialogResult.OK;
        }

        private void SendMessage(MessageType type)
        {
            NetworkMessage message = new NetworkMessage(null, endpoint, type);
            Utilities.SendMessageToConnection(client, JsonConvert.SerializeObject(message));
        }

        private void AddRemoteStatusMessage(string message)
        {
            Utilities.InvokeOnControl(RemoteStatusBox, () => RemoteStatusBox.Items.Add(message));
        }

        #endregion
    }
}
