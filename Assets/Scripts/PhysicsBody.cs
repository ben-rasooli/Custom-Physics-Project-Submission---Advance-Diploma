using Unity.Mathematics;

namespace BehnamPhysicsEngine
{
    public class PhysicsBody
    {
        #region --------------------interface
        public PhysicsBody(PhysicsShape physicsShape, float mass)
        {
            PhysicsShape = physicsShape;
            Mass = mass;
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            AddForce(PhysicsScene.Gravity * Mass * fixedDeltaTime);
            PhysicsShape.Position += Velocity * fixedDeltaTime;
        }

        public void AddForce(float2 force)
        {
            float2 acceleration = force / Mass;
            Velocity += acceleration;
        }

        public PhysicsShape PhysicsShape { get; private set; }

        public float2 Velocity { get; private set; }

        public float Mass { get; private set; }
        #endregion
    }
}