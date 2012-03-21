using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace Watertight.Renderer.Shaders.ShaderVariables
{
    class Vector2ShaderVariable : ShaderVariable
    {
        readonly Vector2 value;

        public Vector2ShaderVariable(int program, string name, Vector2 value)
            : base(program, name)
        {
            this.value = value;
        }


        public override void Assign()
        {
            GL.Uniform2(location, value);
        }
    }
}
