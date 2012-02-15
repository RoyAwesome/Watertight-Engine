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
            int size = numVerticies * 4 * SIZE_FLOAT;
            if (useColors) size += numVerticies * 4 * SIZE_FLOAT;
            if (useNormals) size += numVerticies * 4 * SIZE_FLOAT;
            if (useTexture) size += numVerticies * 2 * SIZE_FLOAT;
            IntPtr sizePtr = new IntPtr(size);

            int offset = 0;


            GL.GenBuffers(1, out vbos);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbos);

            GL.BufferSubData<float>(BufferTarget.ArrayBuffer, new IntPtr(offset), sizePtr, vertexBuffer.ToArray());
            offset += numVerticies * 4 * SIZE_FLOAT;

            if (useColors)
            {
                GL.BufferSubData<float>(BufferTarget.ArrayBuffer, new IntPtr(offset), sizePtr, colorBuffer.ToArray());
                offset += numVerticies * 4 * SIZE_FLOAT;
            }
            if (useNormals)
            {
                GL.BufferSubData<float>(BufferTarget.ArrayBuffer, new IntPtr(offset), sizePtr, normalBuffer.ToArray());
                offset += numVerticies * 4 * SIZE_FLOAT;
            }
            if (useTexture)
            {
                GL.BufferSubData<float>(BufferTarget.ArrayBuffer, new IntPtr(offset), sizePtr, uvBuffer.ToArray());
                offset += numVerticies * 2 * SIZE_FLOAT;
            }


        }

        protected override void doDraw()
        {
            GL.BindVertexArray(vao);
            GL.DrawArrays(BeginMode.Triangles, 0, numVerticies);
        }


    }
}
