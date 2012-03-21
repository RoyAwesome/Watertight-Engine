using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace Watertight.Renderer.Shaders.ShaderVariables
{
    class Vector3ShaderVariable : ShaderVariable
    {
        readonly Vector3 value;

        public Vector3ShaderVariable(int program, string name, Vector3 value)
            : base(program, name)
        {
            this.value = value;
        }


        public override void Assign()
        {
            GL.Uniform3(location, value);
        }
    }
}
