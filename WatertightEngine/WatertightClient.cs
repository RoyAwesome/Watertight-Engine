using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using NLua;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Watertight.Mods;
using Watertight.Renderer;
using Watertight.Renderer.Shaders;
using Watertight.Networking;
using Lidgren.Network;
using Watertight.Resources;

namespace Watertight
{
    class WatertightClient : Engine
    {     
        BatchVertexRenderer renderer;
        GameWindow window;

        public override void Start(int rate)
        {
            base.Start(rate);

            window = new GameWindow();
            window.Visible = true;

          
            Uri shader = new Uri("shader://FileSystemMod/effects/basic30.effect");
            renderer = new GL30BatchVertexRenderer();
            /*
            renderer.ActiveShader = sh;
            renderer.ActiveShader["Proj"] = Matrix4.Identity;
            renderer.ActiveShader["View"] = Matrix4.Identity;
            */
            window.KeyPress += new EventHandler<KeyPressEventArgs>(window_KeyPress);

            int port = 2861;

            Util.Msg("Starting Client");
            NetPeerConfiguration config = new NetPeerConfiguration(Watertight.ImplName + Watertight.Version);
            config.Port = port + 1;
            config.UseMessageRecycling = true;

            NetClient client = new NetClient(config);
            client.Start();

            NetOutgoingMessage connectMessage = client.CreateMessage();
            ConnectPacket connect = new ConnectPacket();
            connect.Encode(ref connectMessage);

            client.Connect("localhost", port, connectMessage);


            Simulation.LuaComponent lc = Simulation.EntityComponentDictionary.NewComponent("TestLuaComponent");      

            foreach (Mod m in ModManager.Mods())
            {
                m.ResourceLoad();
            }

            RunGameLoop();

        }

        public override void Tick(float dt)
        {
            window.ProcessEvents();

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(0, 0, 0, 1);


            foreach (Mod m in ModManager.Mods())
            {
                m.OnTick(dt);
            }


            foreach (Mod m in ModManager.Mods())
            {
                m.PreRender(renderer);
            }

            renderer.Begin();

            foreach (Mod m in ModManager.Mods())
            {
                m.OnRender(dt, renderer);
            }
            renderer.End();


            renderer.Draw();

            window.Context.SwapBuffers();

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


        public override string GetName()
        {
            return Watertight.ImplName + " Client";
        }

        public override Platform GetPlatform()
        {
            return Platform.Client;
        }

        public void AddToNetworkProcessQueue(NetworkedTask method)
        {
            throw new NotImplementedException();
        }

    }
}
