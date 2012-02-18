using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Watertight.Networking
{
    interface NetworkingThread
    {
        void Init(int port);

        void Run();        
    }
}
