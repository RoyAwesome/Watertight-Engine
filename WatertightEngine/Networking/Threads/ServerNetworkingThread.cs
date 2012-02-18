using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using Watertight.EntitySystem;

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
            GameConsole.ConsoleMessage("Starting Server");
            NetPeerConfiguration config = new NetPeerConfiguration(Watertight.ImplName + Watertight.Version);
            config.Port = port;
            config.UseMessageRecycling = true;


            server = new NetServer(config);
            server.Start();
            packetHandlers[30] = new NetworkedEntityUpdateHandler(server, 30);

            
        }

        public void Run()
        {
            while (true)
            {
                NetIncomingMessage message = server.ReadMessage();
            }
        }

       
    }
}
