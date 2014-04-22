using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Watertight.Mods;

namespace Watertight
{
    abstract class WatertightEngine : Game
    {
        protected ModManager modmanager;

        int rate;


       
       
        public virtual void Start(int rate)
        {
            this.rate = rate;

            modmanager = new ModManager();

            modmanager.LoadMods();

            Watertight.SetGame(this);

            

            GameConsole.Initialize();
        }

        public virtual int GetRate()
        {
            return rate;
        }

        public virtual void Shutdown()
        {
            Environment.Exit(0);
        }

        public abstract string GetName();

        public abstract string GetVersion();

        public abstract Platform GetPlatform();
    }
}
