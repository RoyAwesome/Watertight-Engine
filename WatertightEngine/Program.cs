#define CLIENT

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LuaInterface;
using System.Reflection;
using OpenTK;
using OpenTK.Platform;
using OpenTK.Graphics;
using System.Diagnostics;
using System.Threading;

using Watertight.Renderer;
using Watertight.Filesystem;
using System.IO;
using Watertight.Mods;
using Watertight.LuaSystem;

using Newtonsoft.Json;

using OpenTK.Input;


namespace Watertight
{
    class Program
    {
     
        static void Main(string[] args)
        {
#if CLIENT
            Game game = new WatertightClient();
#else
            Game game = new WatertightServer();
#endif    

            game.Start();    
   
        }
    }
}
