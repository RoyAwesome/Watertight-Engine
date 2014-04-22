using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLua;
using NLua.Exceptions;

namespace Watertight.LuaSystem
{
    abstract class Hookable
    {
        static object[] empty = { };

        Dictionary<string, LuaFunction> hooks = new Dictionary<string, LuaFunction>();

        public void RegisterHook(string name, LuaFunction function)
        {
            Console.WriteLine("\t Hook Registered: " + name);
            hooks[name] = function;
        }

        protected object[] CallHook(string name, params object[] arguments)
        {
            //if empty, just return
            if (!hooks.ContainsKey(name)) return empty;
            try
            {
                return hooks[name].Call(arguments);
            }
            catch (LuaException e)
            {
                Console.WriteLine(e.Message);
                if (e.InnerException != null) Console.WriteLine(e.InnerException.Message);
                return empty;
            }
        }
    }
}
