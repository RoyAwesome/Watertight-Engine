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

        [BindFunction("_G", "Msg")]
        public static void Msg(string message)
        {
            GameConsole.ConsoleMessage(message);
        }


        public static NLua.LuaTable GetMetatable(this NLua.LuaTable tab)
        {
            var mt = LuaHelper.LuaVM.GetFunction("getmetatable").SafeCall(tab)[0] as NLua.LuaTable;
            return mt;
        }

        public static object[] SafeCall(this NLua.LuaFunction func, params object[] param)
        {
            try
            {
                return func.Call(param);
            }
            catch (NLua.Exceptions.LuaScriptException e)
            {
                Msg("[Error] " + e.Message);
                return null;
            }

        }

        public static object CreateNew(this Type t, params object[] args)
        {
            Type[] ConstructorTypes = args.Select(x => x.GetType()).ToArray();

            return t.GetConstructor(ConstructorTypes).Invoke(args);
        }
    }
}
