using Unity.Mathematics;

namespace BehnamPhysicsEngine
{
    public class Circle : PhysicsShape
    {
        #region --------------------interface
        public Circle(float radius, float4x4 transform) : base(transform) => _radius = radius;

        public float Radius => _radius;

        public override bool IsCollidingWith(Circle circle)
        {
            float2 otherPosition = circle.Position.xy;
            float sqrDistanceBetweenTwoCircles = math.distancesq(Position.xy, otherPosition);
            float sqrSumOfRadiuses = math.pow(Radius + circle.Radius, 2);

            return sqrDistanceBetweenTwoCircles <= sqrSumOfRadiuses;
        }

        public override bool IsCollidingWith(AABB AABB)
        {
            float2 AABBMin = AABB.Min;
            float2 AABBMax = AABB.Max;
            float2 clampedPosition = math.clamp(Position.xy, AABBMin, AABBMax);

            float sqrDistance = math.distancesq(clampedPosition, Position.xy);
            float sqrRadius = math.pow(Radius, 2);

            return sqrDistance <= sqrRadius;
        }

        public override bool IsCollidingWith(Plane plane)
        {
            return plane.IsCollidingWith(this);
        }
        #endregion

        #region --------------------details
        float _radius;
        #endregion
    }
}
