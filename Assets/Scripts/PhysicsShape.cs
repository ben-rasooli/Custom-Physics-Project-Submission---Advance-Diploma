using Unity.Mathematics;

namespace BehnamPhysicsEngine
{
    public abstract class PhysicsShape
    {
        #region --------------------interface
        protected PhysicsShape(float4x4 transform) => _transform = transform;

        public virtual bool IsCollidingWith(Circle otherPhysicsShape) => false;

        public virtual bool IsCollidingWith(AABB otherPhysicsShape) => false;
        #endregion

        #region --------------------details
        float4x4 _transform;
        #endregion
    }
}
