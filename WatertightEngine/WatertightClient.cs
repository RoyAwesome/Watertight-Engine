using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using LuaInterface;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Watertight.Mods;
using Watertight.Renderer;

namespace Watertight
{
    class WatertightClient : Client
    {
        int rate;

        public void Start(int rate)
        {
            Watertight.SetGame(this);

            LuaHelper.Init(new Lua());
            ModManager.LoadMods();
            ModManager.EnableMods();

            GameConsole.Initialize();


            GameWindow window = new GameWindow();
            window.Visible = true;

            this.rate = rate;
           
            BatchVertexRenderer renderer = new GL11BatchVertexRenderer();

            window.KeyPress += new EventHandler<KeyPressEventArgs>(window_KeyPress);

            int rateInMillies = (int)((1f / rate) * 1000);
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
                if (watch.ElapsedMilliseconds < rateInMillies)
                {
                    Thread.Sleep(rateInMillies - (int)watch.ElapsedMilliseconds);
                    dt = 1f / rate;
                }
                else
                {
                    GameConsole.ConsoleMessage("[WARN] Thread took longer than " + rateInMillies + "ms!");
                    dt = watch.ElapsedMilliseconds;
                }
            }

        }


        void window_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'c')
            {
                Mod m = ModManager.GetMod("FileSystemMod");
                GameConsole.ConsoleMessage(">\tGot Mod " + m.GetName());
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





        public void Shutdown()
        {
            Environment.Exit(0);
        }


        public int GetRate()
        {
            return rate;
        }
    }
}
