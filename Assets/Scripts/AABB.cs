using Unity.Mathematics;

namespace BehnamPhysicsEngine
{
    public class AABB : PhysicsShape
    {
        #region --------------------interface
        public AABB(float4x4 transform, float2 exdend) : base(transform)
        {
            _extents = exdend;
        }

        public float2 Min => Position.xy - _extents;

        public float2 Max => Position.xy + _extents;

        public override bool IsCollidingWith(AABB AABB)
        {
            float2 myMin = Min;
            float2 myMax = Max;
            float2 otherMin = AABB.Min;
            float2 otherMax = AABB.Max;

            //any gap
            if (myMax.x < otherMin.x || otherMax.x < myMin.x ||
                myMax.y < otherMin.y || otherMax.y < myMin.y)
                return false;

            return true;
        }
        
        public override bool IsCollidingWith(Circle circle)
        {
            return circle.IsCollidingWith(this);
        }

        public override bool IsCollidingWith(Plane plane)
        {
            return plane.IsCollidingWith(this);
        }
        #endregion

        #region --------------------details
        float2 _extents;
        #endregion
    }
}
