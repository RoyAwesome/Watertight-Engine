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
        bool running = true;
     

        public Program()
        {
           
        }

       

        public void Run()
        {
            LuaHelper.Init();
            ModManager.LoadMods();
            ModManager.EnableMods();


            GameWindow window = new GameWindow();
            window.Visible = true;


            Mod mod = ModManager.GetMod("FileSystemMod");

            BatchVertexRenderer renderer = new GL11BatchVertexRenderer();

            window.KeyPress += new EventHandler<KeyPressEventArgs>(window_KeyPress);

            while (window.Exists)
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                window.ProcessEvents();
                GL.Clear(ClearBufferMask.ColorBufferBit);
                GL.ClearColor(0, 0, 0, 1);

                mod.OnRender(0, renderer);

                renderer.Draw();


                window.Context.SwapBuffers();
                watch.Stop();
                if (watch.ElapsedMilliseconds < 16) Thread.Sleep(16 - (int)watch.ElapsedMilliseconds);
            }



        }

        void window_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'c')
            {
                Mod m = ModManager.GetMod("FileSystemMod");
                Console.WriteLine(">\tGot Mod " + m.GetName());
                m.DoMain();
            }
        }



        static void Main(string[] args)
        {
                     
            Program p = new Program();
            p.Run();

        }
    }
}
