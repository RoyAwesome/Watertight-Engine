using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Watertight.Filesystem
{
    abstract class Resource 
    {
        Uri path;
        public Uri Path
        {
            get { return path; }
            set { path = value; }
        }

    }
}
