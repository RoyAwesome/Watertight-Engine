using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace Watertight.Renderer
{
    class GL11BatchVertexRenderer : BatchVertexRenderer
    {
        int displayList = 0;

        public GL11BatchVertexRenderer()
        {
            displayList = GL.GenLists(1);
        }

        protected override void doFlush()
        {
            GL.NewList(displayList, ListMode.Compile);
            GL.PushMatrix();
            GL.Begin(PrimitiveType.Triangles);
            for (int i = 0; i < numVerticies; i++)
            {
                int index = i * 4;
                if (useColors) GL.Color3(colorBuffer[index], colorBuffer[index + 1], colorBuffer[index + 2]);
                if (useNormals) GL.Normal3(normalBuffer[index], normalBuffer[index + 1], normalBuffer[index + 2]);
                if (useTexture) GL.TexCoord2(uvBuffer[(i*2)], normalBuffer[(i * 2) + 1]);
                GL.Vertex4(vertexBuffer[index], vertexBuffer[index + 1], vertexBuffer[index + 2], vertexBuffer[index + 3]);
            }
            GL.End();
            GL.PopMatrix();
            GL.EndList();
        }

        protected override void doDraw()
        {
            GL.CallList(displayList);
        }
    }
}
