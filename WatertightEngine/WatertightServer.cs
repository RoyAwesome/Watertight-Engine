using System.Diagnostics;
using System.Threading;
using NLua;
using Watertight.Mods;
using System;
using System.Collections.Generic;
using Watertight.Networking;
using System.Collections.Concurrent;
using Lidgren.Network;

namespace Watertight
{
    

    class WatertightServer : Engine
    {

   
        List<NetworkedTask> networkTasks = new List<NetworkedTask>();
        NetServer server;

        public override void Start(int rate)
        {
            
            int rateInMillies = (int)((1f / rate) * 1000);
            float dt = 0;
            GameConsole.ConsoleMessage("Starting Server");
            NetPeerConfiguration config = new NetPeerConfiguration(Watertight.ImplName + Watertight.Version);
            config.Port = 2861;
            config.UseMessageRecycling = true;

            server = new NetServer(config);
            
            server.Start();

            PacketManager.GetPacket(0);

            RunGameLoop();
        }




        public override string GetName()
        {
            return Watertight.ImplName + " Server";
        }


        public override Platform GetPlatform()
        {
            return Platform.Server;
        }

        public override void Tick(float dt)
        {
            NetIncomingMessage message = null;
            while ((message = server.ReadMessage()) != null)
            {
                switch (message.MessageType)
                {
                    case NetIncomingMessageType.VerboseDebugMessage:
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.ErrorMessage:
                        Console.WriteLine(message.ReadString());
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        NetConnectionStatus status = (NetConnectionStatus)message.ReadByte();
                        if (status == NetConnectionStatus.Connected)
                        {
                            //
                            // A new player just connected!
                            //
                            Console.WriteLine(NetUtility.ToHexString(message.SenderConnection.RemoteUniqueIdentifier) + " connected!");

                            Packet p = PacketManager.HandleMessage(message.SenderConnection.RemoteHailMessage);
                            GameConsole.ConsoleMessage("Username: " + (p as ConnectPacket).Name);

                        }
                        if (status == NetConnectionStatus.Disconnected)
                        {
                            GameConsole.ConsoleMessage(NetUtility.ToHexString(message.SenderConnection.RemoteUniqueIdentifier) + " connected!");
                        }
                        break;
                    case NetIncomingMessageType.Data:
                        GameConsole.ConsoleMessage("Got a data message");

                        break;
                }
            }

            foreach (Mod m in ModManager.Mods())
            {
                m.OnTick(dt);
            }
        }
    }
}
