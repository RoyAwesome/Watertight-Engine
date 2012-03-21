using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace Watertight.Renderer.Shaders.ShaderVariables
{
    class Vector4ShaderVariable : ShaderVariable
    {
        readonly Vector4 value;

        public Vector4ShaderVariable(int program, string name, Vector4 value)
            : base(program, name)
        {
            this.value = value;
        }


        public override void Assign()
        {
            GL.Uniform4(location, value);
        }
    }
}
