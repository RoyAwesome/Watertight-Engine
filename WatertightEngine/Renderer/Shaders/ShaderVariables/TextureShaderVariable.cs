using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace Watertight.Renderer.Shaders.ShaderVariables
{
    class TextureShaderVariable : ShaderVariable
    {
        readonly int textureID = 0;

        public TextureShaderVariable(int program, string name, int texture) : base(program, name)
        {
            this.textureID = texture;
        }

        public void Bind(int unit)
        {
            GL.BindTexture(TextureTarget.Texture2D, textureID);
            GL.ActiveTexture((TextureUnit)((int)TextureUnit.Texture0 + unit));
        }

        public override void Assign()
        {
            
        }
    }
}
