using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Watertight.Simulation
{
    public abstract class EntityComponent
    {

        public Entity Owner
        {
            get;
            internal set;
        }

        /// <summary>
        /// Called after the component has been constructed and attached.  Use this to Initialize the component
        /// </summary>
        public abstract void Awake();

        /// <summary>
        /// Called every frame of the simulation loop
        /// </summary>
        /// <param name="dt">time since last update</param>
        public abstract void Tick(float dt);

        /// <summary>
        /// Called before the Engine renders a frame.  Useful for setting render state
        /// </summary>
        public virtual void PreRender() { }

        /// <summary>
        /// Called when the game renders a frame
        /// </summary>
        public virtual void Render() { }

        /// <summary>
        /// Called after The engine renders a frame.  Useful for setting the render state
        /// </summary>
        public virtual void PostRender() { }

    }
}
