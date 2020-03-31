using Unity.Mathematics;

namespace BehnamPhysicsEngine
{
    public class PhysicsBody
    {
        #region --------------------interface
        public PhysicsBody(PhysicsShape physicsShape, float mass)
        {
            _physicsShape = physicsShape;
            _mass = mass;
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            AddForce(PhysicsScene.Gravity * _mass * fixedDeltaTime);
            _physicsShape.Position += _velocity * fixedDeltaTime;
        }

        public void AddForce(float2 force)
        {
            float2 acceleration = force / _mass;
            _velocity += acceleration;
        }
        #endregion

        #region --------------------details
        float2 _velocity;
        PhysicsShape _physicsShape;
        float _mass;
        #endregion
    }
}