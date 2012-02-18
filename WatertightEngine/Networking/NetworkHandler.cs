using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Watertight.Networking
{
    class NetworkHandler : Attribute
    {
        short packetID;

        public short PacketID
        {
            get { return packetID; }
        }


        public NetworkHandler(short id)
        {
            this.packetID = id;
        }
    }
}
