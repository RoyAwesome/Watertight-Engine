using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace Watertight.Renderer
{
    class Mesh : IMesh
    {

        Vector3[] verts;
        public Vector3[] Vertices
        {
            get { return verts; }
            set { verts = value; }
        }

        int[] ind;
        public int[] Indices
        {
            get { return ind; }
            set { ind = value; }
        }

        Vector3[] normals;
        public Vector3[] Normals
        {
            get { return normals; }
            set { normals = value; }
        }

        OpenTK.Graphics.Color4[] colors;
        OpenTK.Graphics.Color4[] Colors
        {
            get { return colors; }
            set { colors = value; }
        }

        Vector2[] uv1;
        public Vector2[] UV1
        {
            get { return uv1; }
            set { uv1 = value; }
        }

        Vector2[] uv2;
        public Vector2[] UV2
        {
            get { return uv2; }
            set { uv2 = value; }
        }


        public void RecalculateNormals()
        {

            List<Vector3> normals = new List<Vector3>(Vertices.Length);
            //Initialize the normals with a bunch of Vec3(0,0,0)
            for (int i = 0; i < Vertices.Length; i++)
            {
                normals.Add(new Vector3(0, 0, 0));
            }

            //Go through and calculate each triangle's normal
            for (int i = 0; i < Indices.Length; i += 3)
            {
                Vector3 a = Vertices[Indices[i]];
                Vector3 b = Vertices[Indices[i] + 1];
                Vector3 c = Vertices[Indices[i] + 2];

                Vector3 normal = Vector3.Cross((a - b), (b - c));

                //Add the triangle's normal to each vertex
                normals[Indices[i]] += normal;
                normals[Indices[i + 1]] += normal;
                normals[Indices[i + 2]] += normal;
            }

            //Make each normal a unit vector
            for(int i=0; i < normals.Count; i++)
            {
                normals[i].Normalize();
            }

            Normals = normals.ToArray();

        }

    }
}
