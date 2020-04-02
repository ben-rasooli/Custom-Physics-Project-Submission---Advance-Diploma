using Unity.Mathematics;

namespace BehnamPhysicsEngine
{
    public class PhysicsBody
    {
        private float2 position;
        #region --------------------interface
        public PhysicsBody(PhysicsShape physicsShape, float mass)
        {
            PhysicsShape = physicsShape;
            Mass = mass;
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            AddForce(PhysicsScene.Gravity * Mass * fixedDeltaTime);
            Position += Velocity * fixedDeltaTime;
        }

        public void AddForce(float2 force)
        {
            float2 acceleration = force / Mass;
            Velocity += acceleration;
        }

        public void OnCollision(PhysicsBody otherPhysicsBody, float2 penetration)
        {
            separateBodies(otherPhysicsBody, penetration);

            float2 collisionNormal = math.normalize(otherPhysicsBody.Position - Position);
            float2 relativeVelocity = otherPhysicsBody.Velocity - Velocity;
            float elasticity = 1;
            // the following forula is just what it is. I don't understand it but it works!
            float impulseMagnitude = math.dot(-(1 + elasticity) * relativeVelocity, collisionNormal) / math.dot(collisionNormal, collisionNormal * ((1 / Mass) + (1 / otherPhysicsBody.Mass)));
            float2 bounceForce = collisionNormal * impulseMagnitude;

            applySeparationForce(otherPhysicsBody, bounceForce);
        }

        public void OnCollision(Plane plane, float2 penetration)
        {
            // separate shapes
            Position -= penetration;

            // bounce the body back
            float2 bounceForce = Velocity - 2 * math.dot(Velocity, plane.Normal) * plane.Normal;
            Velocity = float2.zero;
            AddForce(bounceForce);
        }

        public float2 Position {
            get => PhysicsShape.Position;
            private set => PhysicsShape.Position = value; }

        public PhysicsShape PhysicsShape { get; private set; }

        public float2 Velocity { get; private set; }

        public float Mass { get; private set; }
        #endregion

        #region --------------------details
        void separateBodies(PhysicsBody otherPhysicsBody, float2 penetration)
        {
            Position -= penetration * 0.5f;
            otherPhysicsBody.Position += penetration * 0.5f;
        }

        void applySeparationForce(PhysicsBody otherPhysicsBody, float2 bounceForce)
        {
            AddForce(-bounceForce);
            otherPhysicsBody.AddForce(bounceForce);
        }
        #endregion
    }
}