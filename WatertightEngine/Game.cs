using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Watertight
{
    public enum Platform
    {
        Client,
        Server,
    }

    interface Game
    {
        string GetName();

        string GetVersion();

        Platform GetPlatform();

        void Start(int rate);
    }
}
