using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace Watertight.Networking
{

    [HandlerType(typeof(PingPacket))]
    class PingPacketHandler : PacketHandler
    {


        public void HandlePacket(Packet packet)
        {
            throw new NotImplementedException();
        }
    }


    [PacketType(1)]
    class PingPacket : Packet
    {


        protected override void WritePacket(ref NetOutgoingMessage message)
        {
           
        }

        protected override void ReadPacket(NetIncomingMessage message)
        {
           
        }
    }
}
