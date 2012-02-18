using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Watertight
{
    static class Util
    {
        public static Type[] TypesWithAttribute(Type attribute)
        {
            List<Type> types = new List<Type>();
            Assembly asm = Assembly.GetEntryAssembly();
            foreach (Type t in asm.GetTypes())
            {
                foreach (Attribute attrib in t.GetCustomAttributes(attribute.GetType(), false))
                {
                    types.Add(t);
                }
            }
            return types.ToArray();

        }
    }
}
