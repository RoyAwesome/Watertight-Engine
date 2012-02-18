using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Dynamic;

namespace Watertight
{
    static class ConsoleVariables
    {
        static Dictionary<string, string> CVars = new Dictionary<string, string>();

        public static string GetVar(string Ident)
        {
            Ident = Ident.ToLower();
            if (!CVars.ContainsKey(Ident)) return "";
            return CVars[Ident];
        }

        public static void SetVar(string Ident, string value)
        {
            CVars[Ident.ToLower()] = value;
        }

        [ConsoleCommand("Set")]
        public static void SetVar(ConCommandArgs args)
        {
            if (args.args.Length == 2)
            {
                GameConsole.ConsoleMessage(args.args[0].ToLower() + ": " + GetVar(args.args[0]));
            }
            else if (args.args.Length == 3)
            {
              
                SetVar(args.args[0], args.args[1]);
                GameConsole.ConsoleMessage(args.args[0].ToLower() +" = "+ args.args[1]);
            }
            else
            {
                GameConsole.ConsoleMessage("Usage: Set [Ident] [Value]");
            }
        }

        public static void InitVariables()
        {
            using (StreamReader file = new StreamReader("Settings/Variables.txt"))
            {
                while (!file.EndOfStream)
                {
                    GameConsole.DoCommand(file.ReadLine());
                }
            }
        }

    }
}
