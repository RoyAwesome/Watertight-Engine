using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Watertight
{
    static class EngineCommands
    {
        [ConsoleCommand("Msg")]
        public static void Msg(ConCommandArgs args)
        {
           
            GameConsole.ConsoleMessage(args.ArgString);
        }

        [ConsoleCommand("Quit")]
        public static void Quit(ConCommandArgs args)
        {
            Watertight.GetGame().Shutdown();
        }
          
    }
}
