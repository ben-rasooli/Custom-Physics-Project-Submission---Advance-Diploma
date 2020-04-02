using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;

namespace BehnamPhysicsEngine
{
    public class PhysicsScene
    {
        #region --------------------interface
        public static float2 Gravity;

        public PhysicsScene(float fixedDeltaTime, float2 gravity)
        {
            _fixedDeltaTime = fixedDeltaTime;
            Gravity = gravity;
        }

        public void OnUpdate(float deltaTime)
        {
            // this variable is used to make the physics simulation frame-independent
            float accumulatedTime = 0.0f;
            accumulatedTime += deltaTime;

            while (accumulatedTime >= _fixedDeltaTime)
            {
                accumulatedTime -= _fixedDeltaTime;
                _physicsBodies.ForEach(pb => pb.OnFixedUpdate(_fixedDeltaTime));
                detectCollisions();
            }
        }

        public void Add(PhysicsShape physicsShape) => _physicsShapes.Add(physicsShape);

        public void Add(PhysicsBody physicsBody) => _physicsBodies.Add(physicsBody);
        #endregion

        #region --------------------details
        float _fixedDeltaTime;
        List<PhysicsShape> _physicsShapes = new List<PhysicsShape>();
        List<PhysicsBody> _physicsBodies = new List<PhysicsBody>();

        void detectCollisions()
        {
            var physicsShapesInPairs = _physicsShapes.Combinations(2);

            foreach (var pair in physicsShapesInPairs)
            {
                // this variable is used to remove overlapping between shapes
                float2 penetration;

                // it first checks for collision then it notifies the colliding physicsBody(s).
                // Before notifying it needs to know what is colliding because the collision
                // response is different for moving-object to moving-object and
                // moving-object to static-object
                if (pair.ElementAt(0).IsCollidingWith(pair.ElementAt(1), out penetration))
                {
                    if (pair.ElementAt(0) is Plane)
                    {
                        _physicsBodies.First(pb => pb.PhysicsShape == pair.ElementAt(1))
                           .OnCollision((Plane)pair.ElementAt(0), penetration);
                    }
                    else if (pair.ElementAt(1) is Plane)
                    {
                        _physicsBodies.First(pb => pb.PhysicsShape == pair.ElementAt(0))
                               .OnCollision((Plane)pair.ElementAt(1), penetration);
                    }
                    else // both are physics bodies
                    {
                        var firstPhysicsBody = _physicsBodies.First(pb => pb.PhysicsShape == pair.ElementAt(0));
                        var secondPhysicsBody = _physicsBodies.First(pb => pb.PhysicsShape == pair.ElementAt(1));

                        firstPhysicsBody.OnCollision(secondPhysicsBody, penetration);
                    }
                }
            }
        }

        #endregion
    }
}
