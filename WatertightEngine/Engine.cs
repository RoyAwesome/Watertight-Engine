using NLua;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Watertight.Mods;

namespace Watertight
{
    public abstract class Engine : Game
    {

        protected int Rate { get; set; }

        public string GetVersion()
        {
            return Watertight.Version;
        }

        public void Shutdown()
        {
            Environment.Exit(0);
        }


        public abstract string GetName();


        public abstract Platform GetPlatform();
       

        public virtual void Start(int rate)
        {
            Watertight.SetGame(this);
            GameConsole.Initialize();

            LuaHelper.Init(new Lua());
            ModManager.LoadMods();
            ModManager.EnableMods();

            this.Rate = rate;


        }

        protected void RunGameLoop()
        {

            int rateInMillies = (int)((1f / Rate) * 1000);
            float dt = 0;
            while (true)
            {

                Stopwatch watch = new Stopwatch();
                watch.Start();

                Tick(dt);

                watch.Stop();
                if (watch.ElapsedMilliseconds < rateInMillies)
                {
                    Thread.Sleep(rateInMillies - (int)watch.ElapsedMilliseconds);
                    dt = 1f / Rate;
                }
                else
                {
                    GameConsole.ConsoleMessage("[WARN] Thread took longer than " + rateInMillies + "ms!");
                    dt = watch.ElapsedMilliseconds;
                }
            }

        }

        public abstract void Tick(float dt);
      

        public int GetRate()
        {
            return Rate;
        }

       
    }
}
