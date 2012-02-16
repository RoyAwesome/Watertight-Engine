using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Watertight.Mods;
using System.Diagnostics;
using System.Threading;
using LuaInterface;

namespace Watertight
{
    class WatertightServer : Game
    {




        public void Start(int rate)
        {
            
            Watertight.SetGame(this);

            LuaHelper.Init(new Lua());
            ModManager.LoadMods();
            ModManager.EnableMods();


           
            Mod mod = ModManager.GetMod("FileSystemMod");

            int expectedRate = (int)((1f / rate) * 1000);
           
            while (true)
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();

                mod.OnTick(0);

                watch.Stop();
                if (watch.ElapsedMilliseconds < expectedRate) Thread.Sleep(expectedRate - (int)watch.ElapsedMilliseconds);

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


        
    }
}
