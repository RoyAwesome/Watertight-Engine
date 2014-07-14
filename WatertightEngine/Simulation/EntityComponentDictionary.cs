using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLua;

namespace Watertight.Simulation
{
    class EntityComponentDictionary
    {


        static Dictionary<string, LuaTable> ComponentDictionary = new Dictionary<string, LuaTable>();


        static Dictionary<string, Type> EngineComponents = new Dictionary<string, Type>()
        {
            {"Transform", typeof(TransformComponent)},
            {"Renderer", typeof(RenderComponent)},
            {"Camera", typeof(CameraComponent)},
        };

        [BindFunction("_G", "RegisterComponent")]
        public static void RegisterComponent(string Name, LuaTable tab)
        {
            
            Util.Msg("Registring Component " + Name);

            if(ComponentDictionary.ContainsKey(Name))
            {
                Util.Msg("Warning: Lua Component " + Name + " Already registered! Skipping");
                return;
            }

         
          
            ComponentDictionary[Name] = tab;
        }

        public static EntityComponent NewComponent(string Name)
        {
            if(EngineComponents.ContainsKey(Name))
            {
                return NewEngineComponent(Name);
            }
            if (!ComponentDictionary.ContainsKey(Name))
            {
                Util.Msg("Warning: Component named " + Name + " was not registered");
                return null;
            }

            var tab = ComponentDictionary[Name];   

            var newobj = LuaHelper.LuaVM.GetFunction("Instantiate").Call(tab)[0] as LuaTable;

            return new LuaComponent(newobj);

        }

        private static EntityComponent NewEngineComponent(string name)
        {
            var type = EngineComponents[name];

            return type.GetConstructor(Type.EmptyTypes).Invoke(new object[] { }) as EntityComponent;

        }
        

    }
}
