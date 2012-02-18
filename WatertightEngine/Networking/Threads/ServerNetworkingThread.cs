using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace Watertight.Networking
{
    class ServerNetworkingThread : NetworkingThread
    {

        NetServer server;

        Dictionary<int, MessageHandler> packetHandlers = new Dictionary<int, MessageHandler>();

        int port;

        public ServerNetworkingThread(int port)
        {
            this.port = port;
        }

        public void Init()
        {
            foreach (Type t in Util.TypesWithAttribute(typeof(NetworkHandler)))
            {

                int id = (t.GetCustomAttributes(typeof(NetworkHandler), true)[0] as NetworkHandler).PacketID;
                MessageHandler handler = (MessageHandler)t.GetConstructor(Type.EmptyTypes).Invoke(null);
                packetHandlers[id] = handler;
                GameConsole.ConsoleMessage("Registering new handler for packet [" + id + "]: " + handler.ToString());
            }

            NetPeerConfiguration config = new NetPeerConfiguration(Watertight.ImplName + Watertight.Version);
            config.Port = port;
            config.UseMessageRecycling = true;


            server = new NetServer(config);
            server.Start();

            
        }

        public void Run()
        {
            throw new NotImplementedException();
        }

       
    }
}
