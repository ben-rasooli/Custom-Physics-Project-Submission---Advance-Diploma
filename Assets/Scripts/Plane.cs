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
            float2 circleToAPointOnPlaneVector = circle.Position.xy - Position.xy;
            float projectionOfTheVectorOnThePlanesNormalMagnitude = math.dot(circleToAPointOnPlaneVector, math.normalize(_transform.c1.xy));

            return math.abs(projectionOfTheVectorOnThePlanesNormalMagnitude) <= circle.Radius;
        }

        public override bool IsCollidingWith(AABB AABB)
        {
            return false;
        }
        #endregion

        #region --------------------details
        float _offsetToWorldCenter;
        #endregion
    }
}
