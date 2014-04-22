using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLua;

namespace Watertight.Simulation
{
    class EntityComponentDictionary
    {
        private struct LuaComponentDescriptor
        {
            public string ClassName;
            public LuaFunction AwakeFunction;
        }

        static Dictionary<string, LuaComponentDescriptor> ComponentDictionary = new Dictionary<string, LuaComponentDescriptor>();

        [BindFunction("_G", "RegisterComponent")]
        public static void RegisterComponent(string Name, LuaTable tab)
        {
            Util.Msg("Registring Component " + Name);

            if(ComponentDictionary.ContainsKey(Name))
            {
                Util.Msg("Warning: Lua Component " + Name + " Already registered! Skipping");
                return;
            }

            LuaComponentDescriptor descriptor = new LuaComponentDescriptor();

            descriptor.ClassName = Name;
            descriptor.AwakeFunction = tab["Awake"] as LuaFunction;

            ComponentDictionary[Name] = descriptor;
        }

    }
}
