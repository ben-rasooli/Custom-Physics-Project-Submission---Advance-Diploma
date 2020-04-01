using UnityEngine;

namespace BehnamPhysicsEngine
{
    public class PhysicsShapeGameObject : MonoBehaviour
    {
        public PhysicsShape Shape { get; protected set; }

        void Update()
        {
            var position = Shape.Position;
            transform.position = new Vector3(position.x, position.y, 0);
        }
    }
}
