using System.Collections.Generic;
using Unity.Mathematics;

namespace BehnamPhysicsEngine
{
    public class Plane : PhysicsShape
    {
        #region --------------------interface
        public Plane(float4x4 transform) : base(transform)
        {
            _offsetToWorldCenter = -math.dot(Position, _transform.c1.xyz);
        }

        public override bool IsCollidingWith(Circle circle)
        {
            float distanceToCircleCenter = getDistanceTo(circle.Position);
            return math.abs(distanceToCircleCenter) <= circle.Radius;
        }

        public override bool IsCollidingWith(AABB AABB)
        {
            List<float3> AABBCorners = new List<float3>
            {
                new float3(AABB.Max,0.0f),
                new float3(AABB.Min.x, AABB.Max.y, 0.0f),
                new float3(AABB.Min, 0.0f),
                new float3(AABB.Max.x, AABB.Min.y, 0.0f)
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

        float getDistanceTo(float3 pointPosition)
        {
            float3 planeNormal = _transform.c1.xyz;
            return math.dot(pointPosition, planeNormal) + _offsetToWorldCenter;
        }

        float3 getClosestPointOnPlane(float3 pointPosition)
        {
            float3 planeNormal = _transform.c1.xyz;
            return _offsetToWorldCenter - planeNormal * getDistanceTo(pointPosition);
        }

        PlaneSides InWhichSideOfPlaneIs(float3 pointPosition)
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