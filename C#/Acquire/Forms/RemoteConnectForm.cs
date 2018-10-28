using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class RemoteConnectForm : Form
    {
        #region Public Variables

        /// <summary>
        /// The list of players for the fully connected game
        /// </summary>
        public List<IPlayer> Players { get; private set; }

        #endregion

        #region Events

        public delegate void OnPlayerNameChanged(string playerId, string newName);
        [Browsable(true)]
        public event OnPlayerNameChanged PlayerNameChanged;

        #endregion

        #region Private Member Variables

        // The client used to connect to the remote host
        private TcpClient client;

        // The endpoint that the remote host is being hosted on
        private readonly IPEndPoint remoteEndpoint;

        // The list of players coming from this client
        private readonly List<Player> localPlayers;

        #endregion

        /// <summary>
        /// Creates an instance of the form used to initiate the connection to a remote server
        /// </summary>
        ///
        /// <param name="endpoint">The remove endpoint to connect to</param>
        /// <param name="players">The list of players loaded into this local game instance</param>
        public RemoteConnectForm(IPEndPoint endpoint, IEnumerable<IPlayer> players)
        {
            remoteEndpoint = endpoint;
            localPlayers = new List<Player>(players.Cast<Player>());

            InitializeComponent();
            Utilities.AddCopyCapability(RemoteStatusBox);
            InitiateConnection();
        }

        /// <summary>
        /// Opens the connection to the server and begins listening for events
        /// </summary>
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

        /// <summary>
        /// Gets the <see cref="TcpClient"/> instance used by this connection form
        /// </summary>
        ///
        /// <returns>The client instance used by this connection form</returns>
        public TcpClient GetClient()
        {
            if (DialogResult != DialogResult.OK)
            {
                return null;
            }

            return client;
        }

        #region Event Handlers

        /// <summary>
        /// Handles receving messages from the remote server
        /// </summary>
        ///
        /// <param name="sender">Event sender - Unused</param>
        /// <param name="e">Event arguments - Unused</param>
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

        /// <summary>
        /// Handles disconnection events
        /// </summary>
        ///
        /// <param name="sender">Event sender - Unused</param>
        /// <param name="e">Event arguments - Unused</param>
        private void Client_Disconnected(object sender, EventArgs e)
        {
            AddRemoteStatusMessage($"Lost connection from {remoteEndpoint.Address}:{remoteEndpoint.Port}");
        }

        /// <summary>
        /// Handles canceling the connection
        /// </summary>
        ///
        /// <param name="sender">Event sender - Unused</param>
        /// <param name="e">Event arguments - Unused</param>
        private void CancelConnectButton_Click(object sender, EventArgs e)
        {
            client?.Close();
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Prepares the client for starting the game with remote connections
        /// </summary>
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

        /// <summary>
        /// Handles a received connect messaeg and sends a player list to the remote server
        /// </summary>
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

        /// <summary>
        /// Handles receiving the remote player list
        /// </summary>
        ///
        /// <param name="message">The message containing player data</param>
        private void HandlePlayersListMessage(NetworkMessage message)
        {
            AddRemoteStatusMessage("Received list of players");
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
            Players = players.Players.ToList<IPlayer>();
        }

        /// <summary>
        /// Handles renaming each player based on the recommendation from the server
        /// </summary>
        ///
        /// <param name="renames">Object containing list of player rename actions to be taken</param>
        private void HandlePlayerRename(PlayerRename renames)
        {
            foreach (PlayerRenameItem rename in renames.PlayerRenames)
            {
                Player foundPlayer = localPlayers.FirstOrDefault(p => p.PlayerId == rename.PlayerId);
                if (foundPlayer == null)
                {
                    AddRemoteStatusMessage($"No player with ID = {rename.PlayerId}, Name = {rename.OriginalName} found in local list");
                    continue;
                }

                AddRemoteStatusMessage($"Renaming {rename.OriginalName} to {rename.NewName} due to duplicate on remote server");
                foundPlayer.Name = rename.NewName;
                PlayerNameChanged?.Invoke(rename.PlayerId, rename.NewName);
            }
        }

        /// <summary>
        /// Sends the given <paramref name="data"/> as a message of the specified <paramref name="type"/>
        /// </summary>
        ///
        /// <param name="type">The type of message being sent</param>
        /// <param name="data">The data to send within the message</param>
        private void SendMessage(MessageType type, AcquireNetworkModel data = null)
        {
            Utilities.SendMessageToConnection(client, data, type);
        }

        /// <summary>
        /// Adds the given <paramref name="message"/> to the <see cref="RemoteStatusBox"/> on the UI
        /// </summary>
        ///
        /// <param name="message">The message to display</param>
        private void AddRemoteStatusMessage(string message)
        {
            Utilities.InvokeOnControl(RemoteStatusBox, () => RemoteStatusBox.Items.Add(message));
        }

        #endregion
    }
}
