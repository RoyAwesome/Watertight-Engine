using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using OpenTK.Graphics.OpenGL;
using System.IO;

namespace Watertight.Resources
{
    [JsonObject(MemberSerialization.OptOut)]
    class EffectFile
    {
        public Dictionary<ShaderType, string> Shaders = new Dictionary<ShaderType, string>();


    }
}
