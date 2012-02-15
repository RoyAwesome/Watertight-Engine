using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Watertight.Renderer
{
    class BatcherException : Exception
    {
        public BatcherException(string message)
            : base(message)
        {

        }
    }
}
