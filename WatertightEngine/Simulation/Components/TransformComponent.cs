using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace Watertight.Simulation
{
    public class TransformComponent : EntityComponent
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;
    

        public Matrix4 Transform
        {
            get
            {  
                return Matrix4.CreateTranslation(Position) * Matrix4.Rotate(Rotation) * Matrix4.CreateScale(Scale);
            }
        }

        internal TransformComponent()
        {
            Position = new Vector3(0, 0, 0);
            Rotation = Quaternion.Identity;
            Scale = new Vector3(1, 1, 1);
        }

        public Vector3 GetForwardVector()
        {
            return Vector3.Transform(Vector3.UnitX, Rotation);
        }


        public override void Awake()
        {
           
        }

        public override void Tick(float dt)
        {
            
        }

    }
}
