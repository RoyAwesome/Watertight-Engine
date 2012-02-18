using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Watertight
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    sealed class ConsoleCommandAttribute : Attribute
    {
       
        readonly string CommandName;
        public readonly bool Silent;

       
        public ConsoleCommandAttribute(string name, bool silent = false)
        {
            this.CommandName = name;
            this.Silent = silent;
           
        }

        public string Name
        {
            get { return CommandName; }
        }

        public string HelpText
        {
            get;
            set;
        }
    }

   
}
