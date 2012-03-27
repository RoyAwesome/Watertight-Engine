using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Watertight.Renderer.Shaders;
using Watertight.Filesystem;
using Newtonsoft.Json;
using System.IO;

namespace Watertight.Resources
{
    class ShaderLoader : ResourceFactory<Shader>
    {
        public override Shader getResource(Uri path)
        {
            EffectFile effect;
            using (StreamReader s = FileSystem.GetFileStream(path))
            using (JsonTextReader r = new JsonTextReader(s))
            {
                JsonSerializer ser = new JsonSerializer();
                effect = ser.Deserialize<EffectFile>(r);
            }




            string filename = path.ToString();
            filename = filename.Substring(0, filename.LastIndexOf('/')+1);

            Uri vert = new Uri(filename + effect.Shaders[OpenTK.Graphics.OpenGL.ShaderType.VertexShader]);
            Uri frag = new Uri(filename + effect.Shaders[OpenTK.Graphics.OpenGL.ShaderType.FragmentShader]);
            int v = ShaderHelper.CompileShader(FileSystem.GetFileStream(vert).BaseStream, OpenTK.Graphics.OpenGL.ShaderType.VertexShader);
            int f = ShaderHelper.CompileShader(FileSystem.GetFileStream(frag).BaseStream, OpenTK.Graphics.OpenGL.ShaderType.FragmentShader);

            return new Shader(new int[] { v, f });

        }

        public override Shader getResource(System.IO.StreamReader stream)
        {
            throw new MethodAccessException("Cannot construct shader from one file.  Use URI path!");
        }
    }

    class Shader : BaseShader, Resource
    {

        public Shader(int[] ShaderSources)
            : base(ShaderSources)
        {

        }


        public Uri Path
        {
            get;
            set;
        }
    }
}
