using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Watertight
{
    static class Watertight
    {
        private static Game instance = null;

        public static void SetGame(Game game)
        {
            if (instance != null) throw new ArgumentException("Cannot set game twice!");
            instance = game;
        }

        public static Game GetGame()
        {
            return instance;
        }

    }
}
