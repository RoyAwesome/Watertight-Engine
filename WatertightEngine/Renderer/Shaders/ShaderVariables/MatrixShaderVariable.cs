using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace Watertight.Renderer.Shaders.ShaderVariables
{
    class MatrixShaderVariable : ShaderVariable
    {
        Matrix4 value;

        public MatrixShaderVariable(int program, string name, Matrix4 value)
            : base(program, name)
        {
            this.value = value;
        }


        public override void Assign()
        {
            GL.UniformMatrix4(location, false, ref value);
        }
    }
}
