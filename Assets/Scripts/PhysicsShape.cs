using Unity.Mathematics;

namespace BehnamPhysicsEngine
{
    public abstract class PhysicsShape
    {
        #region --------------------interface
        protected PhysicsShape(float4x4 transform) => _transform = transform;

        public virtual bool IsCollidingWith(Circle circle) => false;

        public virtual bool IsCollidingWith(AABB AABB) => false;

        public virtual bool IsCollidingWith(Plane plane) => false;
         
        public float3 Position { get { return _transform.c3.xyz; } set { _transform.c3 = new float4(value, 1.0f); } }

        public quaternion Rotation { get { return new quaternion(_transform); } set { _transform = new float4x4(value, Position); } } 
        #endregion

        #region --------------------details
        protected float4x4 _transform;
        #endregion
    }
}
