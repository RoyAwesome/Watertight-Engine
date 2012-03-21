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
        public static int CompileShader(string file, ShaderType type)
        {
            string source = "";
            using( FileStream shader = File.Open(file, FileMode.Open))
            using (TextReader reader = new StreamReader(shader))
            {
                source = reader.ReadToEnd();   
            }

            int s = GL.CreateShader(type);
            GL.ShaderSource(s, source);
            GL.CompileShader(s);
            int[] p = { 0 };
            GL.GetShader(s, ShaderParameter.CompileStatus, p);
            if (p[0] != (int)OpenTK.Graphics.Boolean.True)
            {
                string info = GL.GetShaderInfoLog(s);
                Console.WriteLine("[ERROR] Error while compiling shader: \n " + info);
                GL.DeleteShader(s);
                s = -1;
            }
            
            return s;
        }




    }
}
