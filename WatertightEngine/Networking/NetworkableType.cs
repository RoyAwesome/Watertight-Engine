using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace Watertight.Networking
{
    class NetworkableType
    {
        Guid networkid = Guid.Empty;

        public Guid NetworkID
        {
            get { return networkid; }
            set
            {
                if (networkid != Guid.Empty) throw new ArgumentException("Network ID Already set");
                networkid = value;
                NetworkVisibleContainer.RegisterNetworkableType(networkid, this);
            }
        }

        public NetworkableType() : this(Guid.Empty)
        {
                        
        }

        public NetworkableType(Guid id)
        {
            NetworkID = id;
        }

        public abstract void RecieveMessage(NetIncomingMessage message);
       

        public abstract void SendMessage(NetOutgoingMessage message);


        public abstract bool ShouldSendMessage();
    }
}
