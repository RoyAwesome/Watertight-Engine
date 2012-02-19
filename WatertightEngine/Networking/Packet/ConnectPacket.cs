using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Watertight.Networking
{
    [HandlerType(typeof(ConnectPacket))]
    class ConnectPacketHandler : PacketHandler
    {

        public void HandlePacket(Packet packet)
        {
            ConnectPacket cnt = packet as ConnectPacket;
            GameConsole.ConsoleMessage("Player: " + cnt.Name + " Connected!");
        }
    }

    [PacketType(2)]
    class ConnectPacket : Packet
    {

        public string Name = "RoyAwesome";


        protected override void WritePacket(ref Lidgren.Network.NetOutgoingMessage message)
        {
            message.Write(Name);
        }

        protected override void ReadPacket(Lidgren.Network.NetIncomingMessage message)
        {
            Name = message.ReadString();
        }
    }
}
