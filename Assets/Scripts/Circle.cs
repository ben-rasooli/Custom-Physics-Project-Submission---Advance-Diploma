using Unity.Mathematics;

namespace BehnamPhysicsEngine
{
    public class Circle : PhysicsShape
    {
        #region --------------------interface
        public Circle(float radius, float4x4 transform) : base(transform) => _radius = radius;

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
        float _radius;
        #endregion
    }
}
