using Unity.Mathematics;

namespace BehnamPhysicsEngine
{
    public class AABB : PhysicsShape
    {
        #region --------------------interface
        public AABB(float2 exdend, float4x4 transform) : base(transform)
        {
            _extend = exdend;
        }

        public override bool IsCollidingWith(Circle otherPhysicsShape)
        {
            return false;
        }

        public override bool IsCollidingWith(AABB otherPhysicsShape)
        {
            return false;
        }
        #endregion

        #region --------------------details
        float2 _extend;
        #endregion
    }
}
