#define CLIENT

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using LuaInterface;
using Newtonsoft.Json;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK.Platform;
using Watertight.Filesystem;
using Watertight.LuaSystem;
using Watertight.Mods;
using Watertight.Renderer;


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

            game.Start(60);    
   
        }
    }
}
