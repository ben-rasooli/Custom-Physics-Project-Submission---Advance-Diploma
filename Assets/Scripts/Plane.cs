using System;
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
            Normal = _transform.c1.xy;
        }

        public override bool IsCollidingWith(Circle circle, out float2 penetration)
        {
            float distanceToCircleCenter = getDistanceTo(circle.Position);
            penetration = (circle.Radius - distanceToCircleCenter) * -Normal;
            return math.abs(distanceToCircleCenter) <= circle.Radius;
        }

        public override bool IsCollidingWith(AABB AABB, out float2 penetration)
        {
            penetration = float2.zero;

            List<bool> sides = new List<bool> { false, false };

            foreach (var corner in AABB.Corners)
            {
                var result = InWhichSideOfPlaneIs(corner);

                if (result == PlaneSides.Front)
                    sides[0] = true;
                else if (result == PlaneSides.Back)
                    sides[1] = true;
            }

            if (sides[0] && sides[1])
            {
                penetration = getpenetrationWithAABB(AABB.Corners);
                return true;
            }

            return false;
        }

        public float2 Normal { get; private set; }
        #endregion

        #region --------------------details
        float _offsetToWorldCenter;

        float getDistanceTo(float2 pointPosition)
        {
            return math.dot(pointPosition, Normal) + _offsetToWorldCenter;
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

        float2 getpenetrationWithAABB(List<float2> Corners)
        {
            float2 result = float2.zero;

            foreach (var corner in Corners)
                if (InWhichSideOfPlaneIs(corner) == PlaneSides.Back)
                {
                    float2 closestPointOnPlane = getClosestPointOnPlane(corner);
                    result = math.distance(closestPointOnPlane, corner) * -Normal;
                    break;
                }

            return result;
        }

        float2 getClosestPointOnPlane(float2 pointPosition)
        {
            return pointPosition - Normal * getDistanceTo(pointPosition);
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
