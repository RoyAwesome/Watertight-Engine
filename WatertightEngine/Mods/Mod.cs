using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Watertight.Filesystem;
using Watertight.LuaSystem;
using LuaInterface;
using Watertight.Renderer;

namespace Watertight.Mods
{
    class Mod : Hookable
    {
        ModDescriptor descriptor;
        
        string modFile;

        public string ModFile
        {
            get { return modFile; }
            set { modFile = value; }
        }
        public  ModDescriptor Descriptor
        {
            get { return descriptor; }
            set { descriptor = value; }
        }

        public void Init()
        {
            CallHook("Init");
        }

        public void OnTick(float dt)
        {
            CallHook("OnTick", dt);
        }

        public void OnRender(float dt, BatchVertexRenderer renderer)
        {
            CallHook("OnRender", dt, renderer);
        }


        public string GetFile()
        {
            return modFile;
        }

        public string GetName()
        {
            return descriptor.Name;
        }

        public void DoMain()
        {
            Uri uri = new Uri("script://" + descriptor.Name + "/" + descriptor.ServerMain);
            LuaFile f = FileSystem.LoadResource<LuaFile>(uri);
            f.DoFile(LuaHelper.LuaVM);
        }

    }
}
