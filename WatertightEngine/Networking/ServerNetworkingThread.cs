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

        public void Init(int port)
        {
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
