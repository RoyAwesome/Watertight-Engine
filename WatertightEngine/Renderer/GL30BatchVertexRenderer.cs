using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace Watertight.Renderer
{
    class GL30BatchVertexRenderer : BatchVertexRenderer
    {
        const int SIZE_FLOAT = 4;

        int vao;
        int vbos = -1;



        public GL30BatchVertexRenderer()
        {
            GL.GenVertexArrays(1, out vao);
            GL.BindVertexArray(vao);
        }


        protected override void doFlush()
        {
            GL.BindVertexArray(vao);
            int size = numVerticies * 4 * sizeof(float);
            if (useColors) size += numVerticies * 4 * sizeof(float);
            if (useNormals) size += numVerticies * 4 * sizeof(float);
            if (useTexture) size += numVerticies * 2 * sizeof(float);
            IntPtr sizePtr =(IntPtr)size;

            int offset = 0;
           
            GL.GenBuffers(1, out vbos);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbos);
            GL.BufferData(BufferTarget.ArrayBuffer, sizePtr, IntPtr.Zero, BufferUsageHint.StaticDraw);

            GL.BufferSubData<float>(BufferTarget.ArrayBuffer, (IntPtr)offset, (IntPtr)(vertexBuffer.Count * sizeof(float)), vertexBuffer.ToArray());
            GL.EnableVertexAttribArray(0);
            ActiveShader.EnableAttribute("a_Vertex", 4, VertexAttribPointerType.Float, 0, offset);
         
            offset += numVerticies * 4 * sizeof(float);
           
            if (useColors)
            {
                GL.BufferSubData<float>(BufferTarget.ArrayBuffer, (IntPtr)offset, (IntPtr)(colorBuffer.Count * sizeof(float)), colorBuffer.ToArray());
                GL.EnableVertexAttribArray(1);
                ActiveShader.EnableAttribute("a_Color", 4, VertexAttribPointerType.Float, 0, offset);
                offset += numVerticies * 4 * sizeof(float);
               
            }
            if (useNormals)
            {
                
                GL.BufferSubData<float>(BufferTarget.ArrayBuffer, (IntPtr)offset, (IntPtr)(normalBuffer.Count * sizeof(float)), normalBuffer.ToArray());
                GL.EnableVertexAttribArray(2);
                ActiveShader.EnableAttribute("a_Normal", 4, VertexAttribPointerType.Float, 0, offset);

                offset += numVerticies * 4 * sizeof(float);
            }
            if (useTexture)
            {
                GL.BufferSubData<float>(BufferTarget.ArrayBuffer, (IntPtr)offset, (IntPtr)(uvBuffer.Count * sizeof(float)), uvBuffer.ToArray());
                GL.EnableVertexAttribArray(3);
                ActiveShader.EnableAttribute("a_TexCoord", 2, VertexAttribPointerType.Float, 0, offset);
                offset += numVerticies * 2 * sizeof(float);
            }

            
        }

        protected override void doDraw()
        {
            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbos);

            ActiveShader.Assign();
            GL.DrawArrays(PrimitiveType.Triangles, 0, numVerticies);
        }


    }
}
