﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Watertight.Filesystem
{
    public interface Resource 
    {
        Uri Path
        {
            get;
            set;
        }

    }
}
