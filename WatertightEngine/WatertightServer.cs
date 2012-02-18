using System.Diagnostics;
using System.Threading;
using LuaInterface;
using Watertight.Mods;
using System;
using Watertight.Networking;

namespace Watertight
{
    class WatertightServer : Server
    {

        int rate;


        Thread server;
        ServerNetworkingThread serverthread;
        

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
            serverthread = new ServerNetworkingThread(2861);
            server =  new Thread(new ThreadStart(serverthread.Init));
            server.Start();
            while (true)
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();

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
