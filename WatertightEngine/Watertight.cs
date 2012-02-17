using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Watertight
{
    static class Watertight
    {
        internal static string ImplName = "Watertight Engine";
        internal static string Version = "1.0.0";


        private static Game instance = null;
       
        public static void SetGame(Game game)
        {
            if (instance != null) throw new ArgumentException("Cannot set game twice!");
            instance = game;
        }
        [BindFunction("_G", "GetGame")]
        public static Game GetGame()
        {
            return instance;
        }

        public static Platform GetPlatform()
        {
            return GetGame().GetPlatform();
        }
    }
}
