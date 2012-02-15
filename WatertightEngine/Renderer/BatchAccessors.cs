using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Math;

namespace Watertight.Renderer
{
    public abstract partial class BatchVertexRenderer
    {
        public void AddVertex(float x, float y, float z, float w)
        {
            vertexBuffer.Add(x);
            vertexBuffer.Add(y);
            vertexBuffer.Add(z);
            vertexBuffer.Add(w);

            numVerticies++;
        }
        public void AddVertex(float x, float y, float z)
        {
            AddVertex(x, y, z, 1.0f);
        }
        public void AddVertex(float x, float y)
        {
            AddVertex(x, y, 0.0f, 1.0f);
        }

        public void AddColor(float r, float g, float b)
        {
            AddColor(r, g, b, 1.0f);
        }
        public void AddColor(float r, float g, float b, float a)
        {
            if (!useColors) useColors = true;
            colorBuffer.Add(r);
            colorBuffer.Add(g);
            colorBuffer.Add(b);
            colorBuffer.Add(a);
        }

        public void AddNormal(float x, float y, float z, float w)
        {
            if (!useNormals) useNormals = true;
            normalBuffer.Add(x);
            normalBuffer.Add(y);
            normalBuffer.Add(z);
            normalBuffer.Add(w);
        }
        public void AddNormal(float x, float y, float z)
        {
            AddNormal(x, y, z, 1.0f);
        }

        public void AddNormal(Vector3 vertex)
        {
            AddVertex(vertex.X, vertex.Y, vertex.Z);
        }
        public void AddNormal(Vector4 vertex)
        {
            AddVertex(vertex.X, vertex.Y, vertex.Z, vertex.W);
        }

        public void AddTexCoord(float u, float v)
        {
            if (!useTexture) useTexture = true;
            uvBuffer.Add(u);
            uvBuffer.Add(v);
        }

    }
}
