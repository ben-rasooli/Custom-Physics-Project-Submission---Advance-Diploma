using Unity.Mathematics;

namespace BehnamPhysicsEngine
{
    public abstract class PhysicsShape
    {
        #region --------------------interface
        protected PhysicsShape(float4x4 transform) => _transform = transform;

        public float2 Position { get { return _transform.c3.xy; } set { _transform.c3 = new float4(value, 0.0f, 1.0f); } }

        public quaternion Rotation { get { return new quaternion(_transform); } set { _transform = new float4x4(value, new float3(Position, 0.0f)); } }

        public virtual bool IsCollidingWith(Circle circle) => false;

        public virtual bool IsCollidingWith(AABB AABB) => false;

        public virtual bool IsCollidingWith(Plane plane) => false;
        #endregion

        #region --------------------details
        protected float4x4 _transform;
        #endregion
    }
}
