using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Watertight.Networking
{
    static class PacketManager
    {
        static Dictionary<int, Type> packetTable = new Dictionary<int, Type>();
        static Dictionary<Type, object> packetHandlers = new Dictionary<Type, object>();

        static PacketManager()
        {
            Assembly asm = Assembly.GetEntryAssembly();
            foreach (Type t in asm.GetTypes())
            {
                foreach (Attribute attrib in t.GetCustomAttributes(false))
                {
                    if (attrib is PacketType)
                    {
                        PacketType packetdef = attrib as PacketType;
                        packetTable[packetdef.ID] = t;
                    }
                    if (attrib is HandlerType)
                    {
                        HandlerType handler = attrib as HandlerType;
                        packetHandlers[handler.PacketType] = t.GetConstructor(Type.EmptyTypes).Invoke(null);
                    }
                }

            }


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

        public static void HandlePacket(Packet p)
        {
            if(!packetHandlers.ContainsKey(p.GetType())) 
            {
                GameConsole.ConsoleMessage("Warning: Got Packet " + p.ID + " but can't handle it!");
                return;
            }
            PacketHandler handler = packetHandlers[p.GetType()] as PacketHandler; 
            handler.HandlePacket(p);

        }

    }
}
