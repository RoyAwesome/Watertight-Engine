using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace Watertight.Renderer
{
    interface IMesh
    {
        Vector3[] Vertices
        {
            get;
        }

        int[] Indices
        {
            get;
        }

        Vector3[] Normals
        {
            get;
        }

        Vector2[] UV1
        {
            get;
        }

        Vector2[] UV2
        {
            get;
        }

    }
}
