using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace Watertight.Renderer.Shaders.ShaderVariables
{
    class FloatShaderVariable : ShaderVariable
    {
        readonly float value;

        public FloatShaderVariable(int program, string name, float value)
            : base(program, name)
        {
            this.value = value;
        }


        public override void Assign()
        {
            GL.Uniform1(location, value);
        }
    }
}
