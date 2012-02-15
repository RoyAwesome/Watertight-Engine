using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LuaInterface;

namespace Watertight.LuaSystem
{
    abstract class Hookable
    {
        Dictionary<string, LuaFunction> hooks = new Dictionary<string, LuaFunction>();

        public void RegisterHook(string name, LuaFunction function)
        {
            Console.WriteLine("\t Hook Registered: " + name);
            hooks[name] = function;
        }

        public object[] CallHook(string name, params object[] arguments)
        {
            //Empty, just return
            if (!hooks.ContainsKey(name)) return new object[]{ };
            return hooks[name].Call(arguments);
        }
    }
}
