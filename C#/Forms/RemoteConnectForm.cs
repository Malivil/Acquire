using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Acquire.Enums;
using Acquire.Models;
using Acquire.Models.Interfaces;
using Acquire.NetworkModels;
using SocketMessaging;
using TcpClient = SocketMessaging.TcpClient;

namespace Acquire.Forms
{
    /// <summary>
    /// TODO: Comment this whole class
    /// </summary>
    public partial class RemoteConnectForm : Form
    {
        #region Public Variables

        /// <summary>
        /// The list of players for the fully connected game
        /// </summary>
        public List<Player> Players { get; private set; }

        #endregion

        #region Private Member Variables

        // The client used to connect to the remote host
        private TcpClient client;

        // The endpoint that the remote host is being hosted on
        private readonly IPEndPoint remoteEndpoint;

        // The list of players coming from this client
        private readonly List<Player> localPlayers;

        #endregion

        public RemoteConnectForm(IPEndPoint endpoint, IEnumerable<IPlayer> players)
        {
            remoteEndpoint = endpoint;
            localPlayers = new List<Player>(players.Cast<Player>());

            InitializeComponent();
            InitiateConnection();
        }

        private void InitiateConnection()
        {
            AddRemoteStatusMessage($"Connecting to {remoteEndpoint.Address}:{remoteEndpoint.Port}...");
            try
            {
                client = TcpClient.Connect(remoteEndpoint.Address, remoteEndpoint.Port);
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
                    HandleConnectMessage();
                    break;
                case MessageType.Disconnect:
                    AddRemoteStatusMessage($"ERROR: {(message.Data as Disconnect)?.Message}. Disconnected from the host");
                    CancelConnectButton_Click(null, EventArgs.Empty);
                    break;
                case MessageType.PlayerListResponse:
                    HandlePlayersListMessage(message);
                    break;
                case MessageType.PlayerRename:
                    HandlePlayerRename(message.Data as PlayerRename);
                    break;
                case MessageType.GameStart:
                    HandlePlayersListMessage(message);
                    StartGame();
                    break;
                default:
                    AddRemoteStatusMessage($"ERROR: Received unknown message type! `{message.MessageType}`");
                    break;
            }
        }

        private void Client_Disconnected(object sender, EventArgs e)
        {
            AddRemoteStatusMessage($"Lost connection from {remoteEndpoint.Address}:{remoteEndpoint.Port}");
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
            Close();
        }

        private void HandleConnectMessage()
        {
            AddRemoteStatusMessage("Connected!");
            // Send player list to the server
            PlayerList playersMessage = new PlayerList
            {
                Players = localPlayers.Where(p => p.Type != PlayerType.Remote).ToList()
            };
            SendMessage(MessageType.PlayerListResponse, playersMessage);
        }

        private void HandlePlayersListMessage(NetworkMessage message)
        {
            AddRemoteStatusMessage("Received list of players");
            // TODO: Fix this always being null
            // BUG: #3
            if (!(message.Data is PlayerList players))
            {
                AddRemoteStatusMessage("ERROR: Failed to receive player list");
                return;
            }

            AddRemoteStatusMessage("Current player counts:");
            AddRemoteStatusMessage($"Local players: {players.LocalCount}");
            AddRemoteStatusMessage($"Remote players: {players.RemoteCount}");
            AddRemoteStatusMessage($"Ai players: {players.AICount}");

            // Save the list of players to use for the game when we start
            Players = players.Players;
        }

        private void HandlePlayerRename(PlayerRename player)
        {
            // TODO:
        }

        private void SendMessage(MessageType type, AcquireNetworkModel data = null)
        {
            Utilities.SendMessageToConnection(client, data, type);
        }

        private void AddRemoteStatusMessage(string message)
        {
            Utilities.InvokeOnControl(RemoteStatusBox, () => RemoteStatusBox.Items.Add(message));
        }

        #endregion
    }
}
