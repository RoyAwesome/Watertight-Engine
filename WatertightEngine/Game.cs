using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Watertight
{
    public delegate void NetworkedTask();

    public enum Platform
    {
        Client,
        Server,
    }

    interface Game
    {
        /// <summary>
        /// Gets the name of the Watertight Game
        /// </summary>
        /// <returns></returns>
        string GetName();

        /// <summary>
        /// Gets the version of the Watertight Engine
        /// </summary>
        /// <returns></returns>
        string GetVersion();


        /// <summary>
        /// Returns the Context that the server is in
        /// </summary>
        /// <returns></returns>
        Platform GetPlatform();


        /// <summary>
        /// Starts the engine.  
        /// </summary>
        /// <param name="rate">Ticks per second</param>
        void Start(int rate);
        /// <summary>
        /// Get the expected Ticks Per Second
        /// </summary>
        /// <returns></returns>
        int GetRate();

        /// <summary>
        /// Shuts down the engine
        /// </summary>
        void Shutdown();

    }
}
