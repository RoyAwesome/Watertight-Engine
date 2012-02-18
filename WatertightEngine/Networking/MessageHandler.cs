using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace Watertight.Networking
{
    abstract class MessageHandler
    {
        short packetID;

        NetServer server;

        public void SetServer(NetServer server)
        {
            this.server = server;
        }

        public void ProcessMessage(NetIncomingMessage message)
        {
            packetID = message.ReadInt16();
            Guid id = new Guid(message.ReadBytes(16));
            NetworkableType obj = NetworkVisibleContainer.GetNetworkedObject(id);
            if (obj == null) return;
            obj.RecieveMessage(message);


        }

        public abstract void HandleMessage(NetIncomingMessage message);


        public void SendMessage<E>(E outbound) where E : NetworkableType
        {
            NetOutgoingMessage message = server.CreateMessage();
            message.Write(packetID);
            message.Write(outbound.NetworkID.ToByteArray());
            outbound.SendMessage(message);

        }


       

    }
}
