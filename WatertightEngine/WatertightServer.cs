using System.Diagnostics;
using System.Threading;
using LuaInterface;
using Watertight.Mods;
using System;

namespace Watertight
{
    class WatertightServer : Server
    {




        public void Start(int rate)
        {
            
            Watertight.SetGame(this);

            LuaHelper.Init(new Lua());
            ModManager.LoadMods();
            ModManager.EnableMods();


           
            Mod mod = ModManager.GetMod("FileSystemMod");

            int expectedRate = (int)((1f / rate) * 1000);
            float dt = 0;
            
            while (true)
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();

                foreach (Mod m in ModManager.Mods())
                {
                    m.OnTick(dt);
                }

                watch.Stop();
                if (watch.ElapsedMilliseconds < expectedRate)
                {
                    Thread.Sleep(expectedRate - (int)watch.ElapsedMilliseconds);
                    dt = 1f / rate;
                }
                else
                {
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
    }
}
