using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LuaInterface;
using System.Reflection;

namespace Watertight
{
    static class LuaHelper
    {
        public static Lua LuaVM;

        static LuaHelper()
        {
            LuaVM = new Lua();
            RegisterLuaFunctions();
            BindClasses();
            EnableSandbox();            
        }

        public static void Init()
        {

        }

        private static void EnableSandbox()
        {
            LuaVM.DoFile("sandbox.lua");
        }

        private static void RegisterLuaFunctions()
        {
            Assembly asm = Assembly.GetEntryAssembly();
            foreach (Type t in asm.GetTypes())
            {
                foreach (MethodInfo method in t.GetMethods())
                {
                    foreach (Attribute attrib in Attribute.GetCustomAttributes(method))
                    {

                        if (!(attrib is BindFunction)) continue;
                        BindFunction function = attrib as BindFunction;
                        if (LuaHelper.LuaVM.GetTable(function.FunctionTable) == null) LuaVM.DoString(function.FunctionTable + " = {} ");
                        string functionName = function.FunctionTable + "." + function.FunctionName;
                        if (method.IsStatic)
                        {
                            Console.WriteLine("Registering static function " + functionName);
                            LuaHelper.LuaVM.RegisterFunction(functionName, null, method);
                        }
                        

                    }

                }
            }

        }

        private static void BindClasses()
        {

            Assembly asm = Assembly.GetEntryAssembly();
            LuaHelper.LuaVM.DoString("luanet.load_assembly(\"Watertight\")");
            foreach (Type t in asm.GetTypes())
            {
                foreach (Attribute attrib in Attribute.GetCustomAttributes(t))
                {
                    if (!(attrib is BindClass)) continue;
                    BindClass c = attrib as BindClass;
                    if (LuaHelper.LuaVM.GetTable(c.Table) == null) LuaVM.DoString(c.Table + " = {}");
                    string Lua = c.Table + "." + c.Name + "= luanet.import_type('" + t.FullName + "')";
                    Console.WriteLine("> " + Lua);
                    LuaHelper.LuaVM.DoString(Lua);

                }
            }

        }

        [BindFunction("_G", "print")]
        public static void Print(object s)
        {
            Console.WriteLine(s.ToString());
        }



    }
}
