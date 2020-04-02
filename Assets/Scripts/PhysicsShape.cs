using System;
using Unity.Mathematics;

namespace BehnamPhysicsEngine
{
    public abstract class PhysicsShape
    {
        #region --------------------interface
        protected PhysicsShape(float4x4 transform) => _transform = transform;

        // it returns and set the position(fourth) column of the transformation matrix
        public float2 Position { get { return _transform.c3.xy; } set { _transform.c3 = new float4(value, 0.0f, 1.0f); } }

        public virtual bool IsCollidingWith(Circle circle, out float2 penetration)
        {
            penetration = float2.zero;
            return false;
        }

        public virtual bool IsCollidingWith(AABB AABB, out float2 penetration)
        {
            penetration = float2.zero;
            return false;
        }

        public virtual bool IsCollidingWith(Plane plane, out float2 penetration)
        {
            penetration = float2.zero;
            return false;
        }

        public bool IsCollidingWith(PhysicsShape shape, out float2 penetration)
        {
            if (shape.GetType() == typeof(Circle))
                return IsCollidingWith((Circle)shape, out penetration);
            if (shape.GetType() == typeof(AABB))
                return IsCollidingWith((AABB)shape, out penetration);
            if (shape.GetType() == typeof(Plane))
                return IsCollidingWith((Plane)shape, out penetration);
            else
            {
                penetration = float2.zero;
                return false;
            }
        }
        #endregion

        #region --------------------details
        protected float4x4 _transform;
        #endregion
    }
}
