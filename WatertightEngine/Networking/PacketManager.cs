using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Lidgren.Network;

namespace Watertight.Networking
{
    static class PacketManager
    {
        static Dictionary<int, Type> packetTable = new Dictionary<int, Type>();
        static Dictionary<Type, object> packetHandlers = new Dictionary<Type, object>();

        static PacketManager()
        {
            Assembly asm = Assembly.GetCallingAssembly();
            foreach (Type t in asm.GetTypes())
            {
                if(t.IsAssignableFrom(typeof(Packet)))
                {
                    Console.WriteLine("Got a packet " + t);
                     foreach (Attribute attrib in t.GetCustomAttributes(typeof(PacketType), false))
                     {
                         Console.WriteLine("Assigning Packet: " + attrib);
                         PacketType type = attrib as PacketType;
                         packetTable[type.ID] = t;
                     }
                }
                /*
                    if (attrib is HandlerType)
                    {
                        HandlerType handler = attrib as HandlerType;
                        packetHandlers[handler.PacketType] = t.GetConstructor(Type.EmptyTypes).Invoke(null);
                    }
                }
                */
            }


        }

        public static Packet HandleMessage(NetIncomingMessage msg)
        {
            byte header = msg.PeekByte();
            Console.WriteLine(header);
            Packet p = GetPacket(msg.PeekByte());

            p.Decode(msg);

            return p;
        }


        public static Packet GetPacket(byte id)
        {
            if (!packetTable.ContainsKey(id))
            {
                GameConsole.ConsoleMessage("Warning: Got Unknown Packet id: " + id);
                return null;
            }
            Packet p = packetTable[id].GetConstructor(Type.EmptyTypes).Invoke(null) as Packet;
            p.ID = id;
            return p;
        }


    }
}
