using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Watertight.Simulation
{
    public class Entity
    {
        Dictionary<string, EntityComponent> Components = new Dictionary<string, EntityComponent>();

        public TransformComponent GetTransform()
        {
            return Components["Transform"] as TransformComponent;
        }

        public string Name
        {
            get;
            set;
        }

        public Entity()
        {
            Components["Transform"] = new TransformComponent();
            Name = "Test";
        }

        public EntityComponent AddComponent(string name)
        {
            if(Components.ContainsKey(name))
            {
                Util.Msg("[WARN] Entity already contains " + name + ".  Ignoring");
                return Components[name];
            }

            var cmp = EntityComponentDictionary.NewComponent(name);
            cmp.Owner = this;

            Components[name] = cmp;

            cmp.Awake();

            return cmp;
        }

        public EntityComponent GetComponent(string name)
        {
            if (!Components.ContainsKey(name)) return null;
            return Components[name];
        }

    }

}
