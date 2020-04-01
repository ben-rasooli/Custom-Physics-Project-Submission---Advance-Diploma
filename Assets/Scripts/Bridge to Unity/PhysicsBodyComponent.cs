using UnityEngine;

namespace BehnamPhysicsEngine
{
    public class PhysicsBodyComponent : MonoBehaviour
    {
        [SerializeField] float _mass;

        public void Init(PhysicsScene physicsScene)
        {
            PhysicsShape shape = GetComponent<PhysicsShapeGameObject>().Shape;
            var  physicsBody = new PhysicsBody(shape, _mass);
            physicsScene.Add(physicsBody);
        }
    }
}
