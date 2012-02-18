using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Watertight.Networking
{
    class NetworkVisibleContainer
    {
        static Dictionary<Guid, NetworkableBase> trackedObjects = new Dictionary<Guid, NetworkableBase>();


        public static void RegisterNetworkableType(Guid networkID, NetworkableBase type)
        {
            if (trackedObjects.ContainsKey(networkID)) GameConsole.ConsoleMessage("[NET] Warning: Trying to register networked object twice!");
            trackedObjects[networkID] = type;
        }

        public static NetworkableBase GetNetworkedObject(Guid id)
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
