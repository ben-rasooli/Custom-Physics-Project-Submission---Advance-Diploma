using UnityEngine;

namespace BehnamPhysicsEngine
{
    public class PhysicsBodyComponent : MonoBehaviour
    {
        void Start()
        {
            PhysicsShape shape = GetComponent<IPhysicsShape>().Shape;
            var  physicsBody = new PhysicsBody(shape, 1.0f);
            FindObjectOfType<GameManager>().physicsScene.Add(physicsBody);
        }
    }
}
