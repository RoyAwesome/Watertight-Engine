using System.Diagnostics;
using System.Threading;
using LuaInterface;
using Watertight.Mods;
using System;
using System.Collections.Generic;
using Watertight.Networking;
using System.Collections.Concurrent;
using Lidgren.Network;

namespace Watertight
{
    

    class WatertightServer : Server
    {

        int rate;


        List<NetworkedTask> networkTasks = new List<NetworkedTask>();

      
        public void Start(int rate)
        {
            
            Watertight.SetGame(this);

            LuaHelper.Init(new Lua());
            ModManager.LoadMods();
            ModManager.EnableMods();

            GameConsole.Initialize();

            this.rate = rate;
           
            int rateInMillies = (int)((1f / rate) * 1000);
            float dt = 0;
            GameConsole.ConsoleMessage("Starting Server");
            NetPeerConfiguration config = new NetPeerConfiguration(Watertight.ImplName + Watertight.Version);
            config.Port = 2861;
            config.UseMessageRecycling = true;

            NetServer server = new NetServer(config);
            
            server.Start();

            PacketManager.GetPacket(0);

            while (true)
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();

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

                watch.Stop();
                if (watch.ElapsedMilliseconds < rateInMillies)
                {
                    Thread.Sleep(rateInMillies - (int)watch.ElapsedMilliseconds);
                    dt = 1f / rate;
                }
                else
                {
                    GameConsole.ConsoleMessage("[WARN] Thread took longer than " + rateInMillies + "ms!");                    
                    dt = watch.ElapsedMilliseconds;
                }

            }
        }




        public string GetName()
        {
            return Watertight.ImplName + " Server";
        }

        public string GetVersion()
        {
            return Watertight.Version;
        }

        public Platform GetPlatform()
        {
            return Platform.Server;
        }
        

        public void CreateWorld(string WorldName)
        {
            throw new System.NotImplementedException();
        }


        public void Shutdown()
        {
            Environment.Exit(0);
        }


        public int GetRate()
        {
            throw new NotImplementedException();
        }


      
    }
}
