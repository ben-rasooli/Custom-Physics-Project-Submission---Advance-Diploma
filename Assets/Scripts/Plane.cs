using System.Collections.Generic;
using Unity.Mathematics;

namespace BehnamPhysicsEngine
{
    public class Plane : PhysicsShape
    {
        #region --------------------interface
        public Plane(float4x4 transform) : base(transform)
        {
            _offsetToWorldCenter = -math.dot(Position, _transform.c1.xy);
            _normal = _transform.c1.xy;
        }

        public override bool IsCollidingWith(Circle circle)
        {
            float distanceToCircleCenter = getDistanceTo(circle.Position);
            return math.abs(distanceToCircleCenter) <= circle.Radius;
        }

        public override bool IsCollidingWith(AABB AABB)
        {
            List<float2> AABBCorners = new List<float2>
            {
                AABB.Max,
                new float2(AABB.Min.x, AABB.Max.y),
                AABB.Min,
                new float2(AABB.Max.x, AABB.Min.y)
            };

            List<bool> sides = new List<bool> { false, false };

            foreach (var corner in AABBCorners)
            {
                var result = InWhichSideOfPlaneIs(corner);

                if (result == PlaneSides.Front)
                    sides[0] = true;
                else if (result == PlaneSides.Back)
                    sides[1] = true;
            }

            if (sides[0] && sides[1])
                return true;

            return false;
        }
        #endregion

        #region --------------------details
        float _offsetToWorldCenter;
        float2 _normal;

        float getDistanceTo(float2 pointPosition)
        {
            return math.dot(pointPosition, _normal) + _offsetToWorldCenter;
        }

        float2 getClosestPointOnPlane(float2 pointPosition)
        {
            return _offsetToWorldCenter - _normal * getDistanceTo(pointPosition);
        }

        PlaneSides InWhichSideOfPlaneIs(float2 pointPosition)
        {
            float distanceToPoint = getDistanceTo(pointPosition);

            if (distanceToPoint < 0)
                return PlaneSides.Back;
            if (distanceToPoint > 0)
                return PlaneSides.Front;
            return PlaneSides.Intersects;
        }

        enum PlaneSides
        {
            Front,
            Intersects,
            Back
        }
        #endregion
    }
}