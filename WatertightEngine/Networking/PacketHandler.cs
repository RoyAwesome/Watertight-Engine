using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Watertight.Networking
{
    class HandlerType : Attribute
    {
        Type packetType;

        public Type PacketType
        {
            get { return packetType; }
        }

        public HandlerType(Type packetType)
        {
            this.packetType = packetType;
        }
    }

    interface PacketHandler
    {
        void HandlePacket(Packet packet);
    }
}
