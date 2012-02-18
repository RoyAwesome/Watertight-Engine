using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Watertight.Networking.Packet;
using Lidgren.Network;

namespace Watertight.Networking
{
    abstract class MessageHandler
    {
        protected short packetID;

        protected NetServer server;

        public MessageHandler(NetServer server, short id)
        {
            this.server = server;
            this.packetID = id;
        }

        public abstract void ProcessMessage(NetIncomingMessage message);

        public abstract void SendMessage(object outbound);
       

    }
}
