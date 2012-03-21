using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace Watertight.Renderer.Shaders.ShaderVariables
{
    class IntShaderVariable : ShaderVariable
    {
        readonly int value;
        public IntShaderVariable(int program, String name, int value) : base(program, name)
        {
            this.value = value;
        }



        public override void Assign()
        {
            GL.Uniform1(location, value);
        }
    }
}
