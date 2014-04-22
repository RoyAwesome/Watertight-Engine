using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLua;
using Watertight.Filesystem;

namespace Watertight.LuaSystem
{
    class LuaFile : Resource
    {
        public string mod = "";
        public string Lua = "";

        public void DoFile(Lua vm)
        {
            try
            {
                vm.DoString(Lua, System.IO.Path.GetFileName(Path.LocalPath));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                if (e.InnerException != null)
                {
                    Console.WriteLine(e.InnerException.Message);
                    Console.WriteLine(e.InnerException.StackTrace);
                }
            }
        }


        public Uri Path
        {
            get;           
            set;         
        }
    }
}
