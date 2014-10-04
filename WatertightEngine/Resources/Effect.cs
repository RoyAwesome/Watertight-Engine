using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Watertight.Renderer.Shaders;
using Watertight.Filesystem;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Watertight.Resources
{
    class EffectLoader : ResourceFactory<Effect>
    {      
        public override Effect GetResource(Stream stream)
        {
            JObject jo = null;

            using(StreamReader reader = new StreamReader(stream))
            {
                jo = JObject.Parse(reader.ReadToEnd());
            }

            string vtxpath = (string)jo["Shaders"]["VertexShader"];
            TextFile vertexText = FileSystem.LoadResource<TextFile>(new Uri(vtxpath));

            string fragmentpath = (string)jo["Shaders"]["FragmentShader"];
            TextFile fragmentText = FileSystem.LoadResource<TextFile>(new Uri(fragmentpath));

            int v = ShaderHelper.CompileShaderSource(vertexText.Text, OpenTK.Graphics.OpenGL.ShaderType.VertexShader);
            int f = ShaderHelper.CompileShaderSource(fragmentText.Text, OpenTK.Graphics.OpenGL.ShaderType.FragmentShader);

            Effect ef = new Effect(new int[] { v, f });

            //Load the uniforms
            JToken uniforms = jo["Uniforms"];

            if(uniforms != null)
            {
                foreach(JProperty property in uniforms)
                {
                    string name = property.Name;

                    string value = (string)property.Value;

                    float valf;
                    if (float.TryParse(value, out valf))
                    {
                        ef.SetUniform(name, valf);
                    }
                    else if(value.StartsWith("texture"))
                    {
                        Texture t = FileSystem.LoadResource<Texture>(new Uri(name));
                        ef.SetUniform(name, t);
                    }
                }
            }

            return ef;

        }
    }

    class Effect : Material, Resource
    {

        public Effect(int[] ShaderSources)
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
