using System.Collections.Generic;
using Acquire;
using Acquire.Enums;
using Acquire.Models;
using Acquire.NetworkModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AcquireTests
{
    [TestClass]
    public class AcquireTests
    {
        [TestMethod]
        public void Utilities_GetNetworkMessageString_GetTypeFromString()
        {
            string request = Utilities.GetNetworkMessageString(new PlayerList
            {
                Players = new List<Player>
                {
                    new Player("l", PlayerType.Local, "a3eef493ff134fa688e01f947f267060", false)
                }
            }, MessageType.PlayerListRequest);
            NetworkMessage result = Utilities.GetTypeFromString<NetworkMessage>(request);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Data, typeof(PlayerList));
        }
    }
}
