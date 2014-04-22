using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Watertight.CSMod
{
    public interface ManagedMod
    {
        void OnLoad();

        void OnEnable();

        void OnTick(float dt);

        string GetName();
    }
}
