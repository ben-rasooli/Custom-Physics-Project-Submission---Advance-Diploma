using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

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
            Debug.Log(deltaTime);
            float accumulatedTime = 0.0f;
            accumulatedTime += deltaTime;

            while (accumulatedTime >= _fixedDeltaTime)
            {
                _physicsBodies.ForEach(pb => pb.OnFixedUpdate(_fixedDeltaTime));
                accumulatedTime -= _fixedDeltaTime;

                // check collisions for each game entity
            }
        }

        public static float2 Gravity;

        public void Add(PhysicsShape gameEntity) => _gameEntities.Add(gameEntity);

        public void Add(PhysicsBody physicsBody) => _physicsBodies.Add(physicsBody);
        #endregion

        #region --------------------details
        float _fixedDeltaTime;
        List<PhysicsShape> _gameEntities = new List<PhysicsShape>();
        List<PhysicsBody> _physicsBodies = new List<PhysicsBody>();
        #endregion
    }
}
