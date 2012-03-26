

using System;
using System.IO;
using Watertight.Renderer;
using Watertight.Renderer.Shaders;
using OpenTK;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using System.Threading;
namespace Watertight
{
    class Program
    {
     
        static void Main(string[] args)
        {


            
            if (!Directory.Exists("bin/")) Directory.CreateDirectory("bin/");
            AppDomain.CurrentDomain.AppendPrivatePath("bin/");
#if CLIENT
            Game game = new WatertightClient();
#else
            Game game = new WatertightServer();
#endif    

            game.Start(30);    
   
        }
    }
}
