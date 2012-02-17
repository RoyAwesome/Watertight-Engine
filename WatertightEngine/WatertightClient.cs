using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Watertight.Mods;
using OpenTK;
using Watertight.Renderer;
using System.Diagnostics;
using OpenTK.Graphics.OpenGL;
using System.Threading;
using LuaInterface;

namespace Watertight
{
    class WatertightClient : Client
    {
        

        public void Start(int rate)
        {
            Watertight.SetGame(this);

            LuaHelper.Init(new Lua());
            ModManager.LoadMods();
            ModManager.EnableMods();


            GameWindow window = new GameWindow();
            window.Visible = true;


           
            BatchVertexRenderer renderer = new GL11BatchVertexRenderer();

            window.KeyPress += new EventHandler<KeyPressEventArgs>(window_KeyPress);

            int expectedRate = (int)((1f / rate) * 1000);
            float dt = 0;
            while (window.Exists)
            {
                
                Stopwatch watch = new Stopwatch();
                watch.Start();
                window.ProcessEvents();
                GL.Clear(ClearBufferMask.ColorBufferBit);
                GL.ClearColor(0, 0, 0, 1);

                foreach (Mod m in ModManager.Mods())
                {
                    m.OnTick(dt);
                }

                renderer.Begin();

                foreach (Mod m in ModManager.Mods())
                {
                    m.OnRender(dt, renderer);                    
                }
                renderer.End();


                renderer.Draw();


                window.Context.SwapBuffers();
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


        void window_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'c')
            {
                Mod m = ModManager.GetMod("FileSystemMod");
                Console.WriteLine(">\tGot Mod " + m.GetName());
                m.DoMain();
            }
        }


        public string GetName()
        {
            return Watertight.ImplName + " Client";
        }

        public string GetVersion()
        {
            return Watertight.Version;
        }


        public Platform GetPlatform()
        {
            return Platform.Client;
        }


        
    }
}
