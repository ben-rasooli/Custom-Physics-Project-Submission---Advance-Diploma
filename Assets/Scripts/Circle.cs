using Unity.Mathematics;

namespace BehnamPhysicsEngine
{
    public class Circle : PhysicsShape
    {
        #region --------------------interface
        public Circle(float4x4 transform, float radius) : base(transform) => _radius = radius;

        public float Radius => _radius;

        public override bool IsCollidingWith(Circle otherCircle, out float2 penetration)
        {
            float distanceBetweenTwoCircles = math.distance(Position, otherCircle.Position);
            float sumOfRadiuses = Radius + otherCircle.Radius;
            penetration = (sumOfRadiuses - distanceBetweenTwoCircles) * math.normalize(otherCircle.Position - Position);
            return distanceBetweenTwoCircles <= sumOfRadiuses;
        }

        public override bool IsCollidingWith(AABB AABB, out float2 penetration)
        {
            float2 closestPointToCircleCenter = math.clamp(Position, AABB.Min, AABB.Max);
            float distanceToClosestPoint = math.distance(closestPointToCircleCenter, Position);
            float2 vectorToClosestPoint = closestPointToCircleCenter - Position;
            penetration = vectorToClosestPoint - (math.normalizesafe(vectorToClosestPoint) * Radius);
            return distanceToClosestPoint <= Radius;
        }

        public override bool IsCollidingWith(Plane plane, out float2 penetration)
        {
            return plane.IsCollidingWith(this, out penetration);
        }
        #endregion

        #region --------------------details
        float _radius;
        #endregion
    }
}
