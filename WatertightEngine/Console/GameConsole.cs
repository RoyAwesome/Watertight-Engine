using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Watertight
{

    struct ConsoleCommand
    {
        public MethodInfo cmd;
        public bool Silent;
    }

    class ConMessage
    {
        public string text;
        public float TTL;

        public ConMessage(string text, float TTL)
        {
            this.text = text;
            this.TTL = TTL;
        }

        public ConMessage(string text)
            : this(text, GameConsole.DefaultTextLife)
        {
        }

        public override string ToString()
        {
            return text;
        }
    }


    /// <summary>
    /// Arguement container for console commands
    /// </summary>
    public class ConCommandArgs
    {
        public string[] args;
        public string ArgString
        {
         get { return string.Join(" ", args); }   
        }
        public string CommandName;
    }



    class GameConsole
    {
        const bool StdOutput = true;
        public const float DefaultTextLife = 5.0f;

        static Dictionary<string, ConsoleCommand> commands = new Dictionary<string, ConsoleCommand>();

        static List<ConMessage> AllMessages = new List<ConMessage>();

        static public bool DispAllMessages = false;

        static private bool ConsoleActive = false;
        static public bool Active
        {
            get { return ConsoleActive; }
        }

        public static string[] LivingMessages
        {
            get { return AllMessages.Where(x => x.TTL > 0).Select(x => x.text).ToArray(); }
        }
       
        static public void Initialize()
        {
            FillDictionary();
            
            //mainScreen.Desktop.Children.Add(gui);
        }

 
        [ConsoleCommand("ls")]
        private static void Ls(ConCommandArgs args)
        {
            foreach (KeyValuePair<string, ConsoleCommand> kv in commands)
            {
                ConsoleMessage(kv.Key);
            }
        }

        [ConsoleCommand("ActivateConsole")]
        private static void ToggleConsole(ConCommandArgs args)
        {
           
        }


        #region InputParsing
      
        public static void DoCommand(string InString)
        {
            if (InString == string.Empty) return; //Make sure we don't react to empty strings
            

           
            string[] input = ParseInput(InString);
            string cmdString = input[0].ToLower();
            if (!commands.ContainsKey(cmdString))
            {
                ConsoleMessage("Unknown Console Command: " + input[0]);
                return;
            }
            ConsoleCommand command = commands[cmdString];
            if (!command.Silent)
            {
                ConsoleMessage("> " + InString);
            }
            ConCommandArgs args = new ConCommandArgs();
            args.CommandName = input[0];
            args.args = new string[input.Length];
            for (int i = 1; i < input.Length; i++)
            {
                args.args[i - 1] = input[i];
            }
            InvokeCommand(cmdString, args);


        }


        private static string[] ParseInput(string instring)
        {
            char[] trimChars = new char[] { ' ', '\t', '\r' };
            
            List<string> strings = new List<string>();
            
            int stringPos = 0;
            int lastEnd = 0;
            bool quote = false;
            while (stringPos < instring.Length)
            {
                char Cur = instring[stringPos];
                //Check for space
                if (Cur == ' ' && !quote)
                {
                    string text = instring.Substring(lastEnd, (stringPos - lastEnd));
                   
                    text = text.Trim(trimChars);
                    strings.Add(text);
                    lastEnd = stringPos;
                }
               
                if (Cur == '\"' && quote)
                {
                    string text = instring.Substring(lastEnd, (stringPos - lastEnd));
                    Console.WriteLine("Got EndQuote, Text: " + text);
                    strings.Add(text.Trim(trimChars).Replace("\"",""));
                    quote = false;
                }

                if(Cur == '\"' && !quote)
                {
                    
                    lastEnd = stringPos;
                    quote = true;
                }
                //Check for end of string
                if (stringPos == instring.Length - 1 && lastEnd != stringPos)
                {

                    string text = instring.Substring(lastEnd);

                    text = text.Trim(trimChars);
                    strings.Add(text);
                    lastEnd = stringPos;
                }
                stringPos++;
                
            }


            return strings.ToArray();
        }




        #endregion

        #region Output
        public static void ConsoleMessage(string text)
        {
            string prefix = (Watertight.GetPlatform() == Platform.Client) ? "[Client] " : "[Server] ";
            text = prefix + text;
            if (StdOutput) System.Console.WriteLine(text);
            AllMessages.Add(new ConMessage(text));
        }

        private static string CollapseString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (ConMessage s in AllMessages)
            {
                if (DispAllMessages) builder.AppendLine(s.text);
                if (!DispAllMessages && s.TTL > 0) builder.AppendLine(s.text);
            }

            return builder.ToString();
        }


        public static void Update(float time)
        {
            foreach (ConMessage msg in AllMessages)
            {
                if (msg.TTL > 0) msg.TTL -= time;
            }
            
        }


        #endregion

        #region ConsoleMethodInvoking
        private static void InvokeCommand(string commandname, ConCommandArgs args)
        {
            MethodInfo command = commands[commandname.ToLower()].cmd;
            command.Invoke(null, new[] { args });
        }


        public static void FillDictionary()
        {

            Assembly asm = Assembly.GetCallingAssembly();
            foreach (Type t in asm.GetTypes())
            {
                foreach (MethodInfo method in t.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod))
                {
                    object[] attributes = method.GetCustomAttributes(typeof(ConsoleCommandAttribute), false);
                    if (attributes.Length > 0)
                    {
                        foreach (Attribute atrb in attributes)
                        {
                            ConsoleCommandAttribute cc = atrb as ConsoleCommandAttribute;
                            ConsoleCommand Command = new ConsoleCommand();
                            Command.cmd = method;
                            Command.Silent = cc.Silent;
                
                            commands.Add(cc.Name.ToLower(), Command);
                            ConsoleMessage("Added: " + cc.Name);
                        }

                    }
                }

            }


        }

     

        #endregion

  

  

 

 
  
    }
}
