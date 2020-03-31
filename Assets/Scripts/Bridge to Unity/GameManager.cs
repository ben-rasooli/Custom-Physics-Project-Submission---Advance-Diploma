using Unity.Mathematics;
using UnityEngine;

namespace BehnamPhysicsEngine
{
    public class GameManager : MonoBehaviour
    {
        public PhysicsScene physicsScene;

        void Awake()
        {
            physicsScene = new PhysicsScene(0.002f, new float2(0, -9.81f));
        }

        void Update()
        {
            physicsScene.OnUpdate(Time.deltaTime);
        }
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
    }
}
