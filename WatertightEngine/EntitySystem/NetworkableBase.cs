using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace Watertight.Networking
{
    abstract class NetworkableBase
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

        public NetworkableBase() : this(Guid.Empty)
        {
                        
        }

        public NetworkableBase(Guid id)
        {
            NetworkID = id;
        }

        public abstract void RecieveMessage(NetIncomingMessage message);
       

        public abstract void SendMessage(NetOutgoingMessage message);


        public abstract bool ShouldSendMessage();
    }
}
