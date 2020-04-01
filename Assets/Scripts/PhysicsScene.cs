using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;

namespace BehnamPhysicsEngine
{
    public class PhysicsScene
    {
        #region --------------------interface
        public PhysicsScene(float fixedDeltaTime, float2 gravity)
        {
            _fixedDeltaTime = fixedDeltaTime;
            Gravity = gravity;
        }

        public void OnUpdate(float deltaTime)
        {
            float accumulatedTime = 0.0f;
            accumulatedTime += deltaTime;

            while (accumulatedTime >= _fixedDeltaTime)
            {
                accumulatedTime -= _fixedDeltaTime;

                _physicsBodies.ForEach(pb => pb.OnFixedUpdate(_fixedDeltaTime));

                var physicsShapesPairs = _physicsShapes.Combinations(2);

                foreach (var pair in physicsShapesPairs)
                    if (pair.ElementAt(0).IsCollidingWith(pair.ElementAt(1)))
                        UnityEngine.Debug.Log($"{pair.ElementAt(0).ToString()} collides with {pair.ElementAt(0).ToString()}");
            }
        }

        public static float2 Gravity;

        public void Add(PhysicsShape physicsShape)
        {
            _physicsShapes.Add(physicsShape);
        }

        public void Add(PhysicsBody physicsBody) => _physicsBodies.Add(physicsBody);

        public void ApplyForce()
        {
            _physicsBodies[0].AddForce(new float2(2.0f, 20.0f));
        }
        #endregion

        #region --------------------details
        float _fixedDeltaTime;
        List<PhysicsShape> _physicsShapes = new List<PhysicsShape>();
        List<PhysicsBody> _physicsBodies = new List<PhysicsBody>();
        #endregion
    }
}
