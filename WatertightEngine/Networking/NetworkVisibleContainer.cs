using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Watertight.Networking
{
    class NetworkVisibleContainer
    {
        static Dictionary<Guid, NetworkableType> trackedObjects = new Dictionary<Guid, NetworkableType>();


        public static void RegisterNetworkableType(Guid networkID, NetworkableType type)
        {
            if (trackedObjects.ContainsKey(networkID)) GameConsole.ConsoleMessage("[NET] Warning: Trying to register networked object twice!");
            trackedObjects[networkID] = type;
        }

        public static NetworkableType GetNetworkedObject(Guid id)
        {
            if (!trackedObjects.ContainsKey(id))
            {
                GameConsole.ConsoleMessage("[NET] Warning: Tried to get a networked object that doesn't exist!");
                return null;
            }
            return trackedObjects[id];
        }

    }
}
