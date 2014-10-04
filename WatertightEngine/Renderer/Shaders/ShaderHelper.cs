using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using System.IO;

namespace Watertight.Renderer.Shaders
{
    class ShaderHelper
    {

        public static int CompileShaderSource(string source, ShaderType type)
        {
            int s = GL.CreateShader(type);
            GL.ShaderSource(s, source);
            GL.CompileShader(s);
            int[] p = { 0 };
            GL.GetShader(s, ShaderParameter.CompileStatus, p);
            if (p[0] != (int)OpenTK.Graphics.OpenGL.Boolean.True)
            {
                string info = GL.GetShaderInfoLog(s);
                Console.WriteLine("[ERROR] Error while compiling shader: \n " + info);
                GL.DeleteShader(s);
                s = -1;
            }

            return s;
        }

        public static int CompileShader(string file, ShaderType type)
        {
            int s = -1;

            using (FileStream stream = File.Open(file, FileMode.Open))
            {
                s = CompileShader(stream, type);
            }

            return s;
        }


        public static int CompileShader(Stream stream, ShaderType type)
        {
            string source = "";

            using (TextReader reader = new StreamReader(stream))
            {
                source = reader.ReadToEnd();
            }

            return CompileShaderSource(source, type);          
        }


    }
}
