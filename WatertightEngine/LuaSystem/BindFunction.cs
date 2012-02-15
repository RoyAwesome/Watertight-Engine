using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Watertight
{
    [AttributeUsage(AttributeTargets.Method)]
    class BindFunction : Attribute
    {
        private string functionTable;
        private string functionName;
      

        public string FunctionName
        {
            get { return functionName; }
        }

        public string FunctionTable
        {
            get { return functionTable; }
        }

        public BindFunction(string table, string name )
        {
            this.functionName = name;
            this.functionTable = table;
        }

    }
}
