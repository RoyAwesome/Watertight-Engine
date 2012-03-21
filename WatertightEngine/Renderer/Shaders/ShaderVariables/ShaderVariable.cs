using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace Watertight.Renderer.Shaders.ShaderVariables
{
    abstract class ShaderVariable
    {
        protected readonly int program;
        protected readonly int location;

        public ShaderVariable(int program, string name)
        {
            this.program = program;
            
            GL.UseProgram(program);
            if (this is AttributeShaderVariable) this.location = GL.GetAttribLocation(program, name);
            else this.location = GL.GetUniformLocation(program, name);

            

        }

        public abstract void Assign();



    }
}
