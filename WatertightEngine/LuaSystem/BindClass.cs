using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Watertight
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    class BindClass : Attribute
    {
        string className = "";
        string classTable = "";

        public string Name
        {
            get { return className; }
        }

        public string Table
        {
            get { return classTable; }
        }

        public BindClass(string table, string name)
        {
            this.className = name;
            this.classTable = table;
        }

        public BindClass(string name) : this("_G", name)
        {
            
        }


    }
}
