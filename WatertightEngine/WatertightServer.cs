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




        public void Start()
        {
            
            Watertight.SetGame(this);

            LuaHelper.Init(new Lua());
            ModManager.LoadMods();
            ModManager.EnableMods();


           
            Mod mod = ModManager.GetMod("FileSystemMod");


            while (true)
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();

                mod.OnTick(0);

                watch.Stop();
                if (watch.ElapsedMilliseconds < 16) Thread.Sleep(16 - (int)watch.ElapsedMilliseconds);

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
