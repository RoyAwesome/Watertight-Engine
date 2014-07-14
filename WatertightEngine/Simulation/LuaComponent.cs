using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLua;
using Watertight.LuaSystem;
namespace Watertight.Simulation
{
    public class LuaComponent : EntityComponent
    {

        LuaTable table;
      
        public LuaComponent(LuaTable baseTable)
        {
            this.table = baseTable;            
        }
      
        public override void Awake()
        {            
            var lt = table.GetMetatable()["Awake"] as LuaFunction;
            if (lt == null) return;

            lt.SafeCall(table);

        }

        public override void Tick(float dt)
        {
            var lt = table.GetMetatable()["Tick"] as LuaFunction;
            if (lt == null) return;

            lt.SafeCall(table, dt);
        }
    }
}
