using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Watertight.Renderer.Shaders;
using Watertight.Filesystem;

namespace Watertight.Resources
{
    class Shader : BaseShader, Resource
    {
        public Uri Path
        {
            get;
            set;
        }
    }
}
