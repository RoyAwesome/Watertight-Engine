using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Lidgren.Network;

namespace Watertight.Networking
{
   
    class PacketType : Attribute
    {
        byte id = 0;
        public byte ID
        {
            get { return id; }
        }

        public PacketType(byte id)
        {
            this.id = id;
        }

    }

    abstract class Packet
    {
        byte packetID = 0;

        public Packet()
        {
            object[] attribs = this.GetType().GetCustomAttributes(typeof(PacketType), true);
            if (attribs.Length <= 0) throw new Exception("Packet constructed without a PacketType attribute!");
            this.ID = (attribs[0] as PacketType).ID;
        }

        public byte ID
        {
            get { return packetID; }
            set
            {
                if (packetID != 0) return; //If we are already set, ignore the value
                packetID = value;
            }
        }
        public void Encode(ref NetOutgoingMessage message)
        {
            message.Write(ID);
            Console.WriteLine("Wrote packet: " + this.GetType() + " ID:  " + ID);
            WritePacket(ref message);

        }

        protected abstract void WritePacket(ref NetOutgoingMessage message);


        public void Decode(NetIncomingMessage message)
        {
            ID = message.ReadByte();
            ReadPacket(message);
        }


        protected abstract void ReadPacket(NetIncomingMessage message);
    }
}
