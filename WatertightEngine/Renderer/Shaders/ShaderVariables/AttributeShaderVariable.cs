using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace Watertight.Renderer.Shaders.ShaderVariables
{
    class AttributeShaderVariable : ShaderVariable
    {
        int size;
        VertexAttribPointerType type;
        int offset;


        public AttributeShaderVariable(int program, string name, int size, VertexAttribPointerType type, int stride, int offset)
            : base(program, name)
        {
            this.size = size;
            this.type = type;
            this.offset = offset;
        }

        public override void Assign()
        {
            GL.EnableVertexAttribArray(location);
            GL.VertexAttribPointer(location, size, type, false, 0, offset);
        }
    }
}
