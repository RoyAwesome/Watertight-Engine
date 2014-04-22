using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace Watertight.Mods.EngineMod
{
    [Export(typeof(Mod))]
    [ExportMetadata("Name", "Watertight Mod")]
    [ExportMetadata("Author", "Roy Awesome")]
    [ExportMetadata("Version", "1")]
    class WatertightMod : Mod
    {
        public override void Init()
        {
            
        }

        public override void OnTick(float dt)
        {
            
        }

        public override void OnRender(float dt, Renderer.BatchVertexRenderer renderer)
        {
            
        }

        public override void PreRender(Renderer.BatchVertexRenderer renderer)
        {
            
        }

        public override void ResourceLoad()
        {
           
        }
    }
}
