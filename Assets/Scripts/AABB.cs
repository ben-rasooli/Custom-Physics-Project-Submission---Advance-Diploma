using System.Collections.Generic;
using System.Linq;
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

        public List<float2> Corners =>
            new List<float2> { Max, new float2(Min.x, Max.y), Min, new float2(Max.x, Min.y) };

        public override bool IsCollidingWith(AABB otherAABB, out float2 penetration)
        {
            penetration = float2.zero;
            float2 myMin = Min;
            float2 myMax = Max;
            float2 otherMin = otherAABB.Min;
            float2 otherMax = otherAABB.Max;

            //any gap
            if (myMax.x < otherMin.x || otherMax.x < myMin.x ||
                myMax.y < otherMin.y || otherMax.y < myMin.y)
                return false;

            penetration = getpenetration(otherAABB);
            return true;
        }

        public override bool IsCollidingWith(Circle circle, out float2 penetration)
        {
            return circle.IsCollidingWith(this, out penetration);
        }

        public override bool IsCollidingWith(Plane plane, out float2 penetration)
        {
            return plane.IsCollidingWith(this, out penetration);
        }

        bool IsOverlappingWith(float2 pointPosition)
        {
            return math.min(Min, pointPosition).Equals(Min) && math.max(Max, pointPosition).Equals(Max);
        }
        #endregion

        #region --------------------details
        float2 _extents;

        // this return the overlap amount by first finding the two overlapping corners,
        // then calculate the vector between them and finally get only the smallest amount between x and y
        float2 getpenetration(AABB otherAABB)
        {
            float2 firstOverlappingCorner = otherAABB.Corners.FirstOrDefault(IsOverlappingWith);
            float2 secondOverlappingCorner = Corners.FirstOrDefault(otherAABB.IsOverlappingWith);
            float2 overlappingBoxArea = secondOverlappingCorner - firstOverlappingCorner;

            if (math.abs(overlappingBoxArea.x) < math.abs(overlappingBoxArea.y))
                overlappingBoxArea.y = 0;
            else
                overlappingBoxArea.x = 0;

            return overlappingBoxArea;
        }
        #endregion
    }
}
