using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using Watertight.Renderer.Shaders.ShaderVariables;
using OpenTK;
using System.Dynamic;
using Watertight.Resources;

namespace Watertight.Renderer.Shaders
{
    public class Material : IDisposable
    {
        const bool validate = true;

        int program = -1;
        Dictionary<string, ShaderVariable> variables = new Dictionary<string, ShaderVariable>();
        Dictionary<string, TextureShaderVariable> textures = new Dictionary<string, TextureShaderVariable>();

        int maxTextures = 0;

        int[] shaders;

        public Material(int[] shaders)
        {
            Compile(shaders);

        }

        public Material(string VertexShader, string FragmentShader)
        {
            int vshader = ShaderHelper.CompileShader(VertexShader, ShaderType.VertexShader);
            int fshader = ShaderHelper.CompileShader(FragmentShader, ShaderType.FragmentShader);
            int[] shaders = new int[] { vshader, fshader };

            Compile(shaders);

        }

        protected void Compile(int[] shaders)
        {
            this.shaders = shaders;
            program = GL.CreateProgram();

            foreach (int s in shaders)
            {
                GL.AttachShader(program, s);
            }
            
         
            GL.LinkProgram(program);


            int status = 0;

            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out status);
            
            if (status != (int)All.True)
            {
                string error = GL.GetProgramInfoLog(program);
                Console.WriteLine("Error linking shader: \n " + error);
                foreach (int s in shaders)
                {
                    GL.DeleteShader(s);
                }
                GL.DeleteProgram(program);
                program = -1;
            }

            if (validate)
            {
                GL.ValidateProgram(program);
                GL.GetProgram(program, GetProgramParameterName.ValidateStatus, out status);
                if (status != (int)All.True)
                {
                    Console.WriteLine("Error Valdating Shader: \n " + GL.GetProgramInfoLog(program));
                }
                GL.GetProgram(program, GetProgramParameterName.AttachedShaders, out status);
                Console.WriteLine("Attached Shaders: " + status);
                GL.GetProgram(program, GetProgramParameterName.ActiveAttributes, out status);
                Console.WriteLine("Active Attributes: " + status);
                for (int i = 0; i < status; i++)
                {
                    int size = 0;
                    ActiveAttribType type = ActiveAttribType.Float;
                    Console.WriteLine("\t" + GL.GetActiveAttrib(program, i, out size, out type) + " " + size + " " + type);
                }

                GL.GetProgram(program, GetProgramParameterName.ActiveUniforms, out status);
                Console.WriteLine("Active Uniforms: " + status);
                for (int i = 0; i < status; i++)
                {
                    int size = 0;
                    ActiveUniformType type = ActiveUniformType.Bool;
                    Console.WriteLine("\t" + GL.GetActiveUniform(program, i, out size, out type) + " " + size + " " + type);
                }


            }

            GL.GetInteger(GetPName.MaxCombinedTextureImageUnits, out maxTextures);
            GameConsole.ConsoleMessage("Max TExtures: " + maxTextures);
        }

        public dynamic this[string param]
        {
            set
            {
                Type t = value.GetType();
                if (t == typeof(int)) SetUniform(param, (int)value);
                if (t == typeof(float)) SetUniform(param, (float)value);
                if (t == typeof(Vector3)) SetUniform(param, (Vector3)value);
                if (t == typeof(Vector2)) SetUniform(param, (Vector2)value);
                if (t == typeof(Vector4)) SetUniform(param, (Vector4)value);
                if (t == typeof(Matrix4)) SetUniform(param, (Matrix4)value);
                if (t == typeof(Texture)) SetUniform(param, (Texture)value);
            }

        }

        #region Destructor
        ~Material()
        {
            DeleteShaders();
        }

        private void DeleteShaders()
        {
            foreach (int i in shaders)
            {
                GL.DeleteShader(i);
            }
            GL.DeleteProgram(program);
        }

        public void Dispose()
        {
            DeleteShaders();
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Accessors
        public void SetUniform(string name, int value)
        {
            variables[name] = new IntShaderVariable(this.program, name, value);
        }
        public void SetUniform(string name, float value)
        {
            variables[name] = new FloatShaderVariable(this.program, name, value);
        }
        public void SetUniform(string name, Matrix4 value)
        {
            variables[name] = new MatrixShaderVariable(this.program, name, value);
        }
        public void SetUniform(string name, Vector3 value)
        {
            variables[name] = new Vector3ShaderVariable(this.program, name, value);
        }
        public void SetUniform(string name, Vector2 value)
        {
            variables[name] = new Vector2ShaderVariable(this.program, name, value);
        }
        public void SetUniform(string name, Vector4 value)
        {
            variables[name] = new Vector4ShaderVariable(this.program, name, value);
        }

        public void SetUniform(string name, Texture texture)
        {
            textures[name] = new TextureShaderVariable(this.program, name, texture.TextureID);
        }

        public void EnableAttribute(string name, int size, VertexAttribPointerType type, int stride, int offset)
        {
            variables[name] = new AttributeShaderVariable(this.program, name, size, type, stride, offset);
        }

        
        #endregion


        public void Assign()
        {
            GL.UseProgram(this.program);
            foreach (ShaderVariable var in variables.Values)
            {
                var.Assign();
            }
            int i = 0;
            foreach (TextureShaderVariable tex in textures.Values)
            {
                tex.Bind(i);
                i++;
                if (i >= maxTextures)
                {
                    throw new Exception("Bound more textures than you can support! ");
                }
            }
        }

    }
}
