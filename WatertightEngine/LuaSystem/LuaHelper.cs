using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LuaInterface;
using System.Reflection;
using System.IO;
using OpenTK;

namespace Watertight
{
    static class LuaHelper
    {
        static Lua vm;
        public static Lua LuaVM
        {
            get { return vm; }
        }

        public static void Init(Lua lstate)
        {
            vm = lstate;
            RegisterLuaFunctions();
            BindClasses();
            EnableSandbox();
            PushGlobals();
        }

        private static void PushGlobals()
        {
            if (Watertight.GetPlatform() == Platform.Server)
            {
                LuaVM.DoString("CLIENT = false;");
                LuaVM.DoString("SERVER = true;");
            }
            else
            {
                LuaVM.DoString("CLIENT = true;");
                LuaVM.DoString("SERVER = false;");
            }

            LuaHelper.LuaVM.DoString("luanet.load_assembly(\"OpenTK\")");
            LuaVM.DoString("Math = {}");
            LuaVM.DoString("Math.Matrix4 = luanet.import_type('" + typeof(Matrix4).FullName + "')");
            LuaVM.DoString("Math.Vector3 = luanet.import_type('" + typeof(Vector3).FullName + "')");
            LuaVM.DoString("Math.Vector3 = luanet.import_type('" + typeof(Vector2).FullName + "')");
        }

        private static void EnableSandbox()
        {
            if (File.Exists("sandbox.lua"))
                LuaVM.DoFile("sandbox.lua");
            else
            {
                Console.WriteLine("----------WARNING--------");
                Console.WriteLine(">Sanbox file not found, this makes running mods dangerous");
                Console.WriteLine(">Find the sandbox.lua file and fix it!");

            }
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
            GameConsole.ConsoleMessage(s.ToString());
        }



    }
}
