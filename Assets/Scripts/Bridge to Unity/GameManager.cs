using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace BehnamPhysicsEngine
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] GameObject _entities;

        void Awake()
        {
            _physicsScene = new PhysicsScene(0.002f, new float2(0, -9.81f));
            _entities.GetComponentsInChildren<IPhysicsShape>().ToList().ForEach(ps => ps.Init(_physicsScene));
            _entities.GetComponentsInChildren<PhysicsBodyComponent>().ToList().ForEach(pb => pb.Init(_physicsScene));
        }

        void Update()
        {
            _physicsScene.OnUpdate(Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space))
                _physicsScene.ApplyForce();
        }

        PhysicsScene _physicsScene;
    }

    internal static class ExtensionMethods
    {
        public static float4x4 localToWorldFloat4x4Matrix(this Transform transform)
        {
            var m = transform.localToWorldMatrix;

            return new float4x4(
                m[0, 0], m[0, 1], m[0, 2], m[0, 3],
                m[1, 0], m[1, 1], m[1, 2], m[1, 3],
                m[2, 0], m[2, 1], m[2, 2], m[2, 3],
                m[3, 0], m[3, 1], m[3, 2], m[3, 3]);
        }

        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> elements, int k)
        {
            return k == 0 ? new[] { new T[0] } :
              elements.SelectMany((e, i) =>
                elements.Skip(i + 1).Combinations(k - 1).Select(c => (new[] { e }).Concat(c)));
        }
    }
}
