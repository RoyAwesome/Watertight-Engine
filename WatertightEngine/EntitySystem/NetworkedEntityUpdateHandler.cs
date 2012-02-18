using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Watertight.Networking;
using Lidgren.Network;

namespace Watertight.EntitySystem
{
    [NetworkHandler(30)]
    class NetworkedEntityUpdateHandler : MessageHandler
    {
        public NetworkedEntityUpdateHandler(NetServer server, short id) : base(server, id)
        {

        }
        public override void ProcessMessage(NetIncomingMessage message)
        {
            packetID = message.ReadInt16();
            Guid id = new Guid(message.ReadBytes(16));
            NetworkableBase obj = NetworkVisibleContainer.GetNetworkedObject(id);
            if (obj == null) return;
            obj.RecieveMessage(message);

        }

        public override void SendMessage(object outbound)
        {
            NetworkableBase b = outbound as NetworkableBase;
            NetOutgoingMessage message = server.CreateMessage();
            message.Write(packetID);
            message.Write(b.NetworkID.ToByteArray());
            b.SendMessage(message);

        }
    }
}
