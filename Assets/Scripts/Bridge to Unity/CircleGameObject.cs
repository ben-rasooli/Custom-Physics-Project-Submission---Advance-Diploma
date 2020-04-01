using UnityEngine;

namespace BehnamPhysicsEngine
{
    public class CircleGameObject : PhysicsShapeGameObject, IPhysicsShape
    {
        public void Init(PhysicsScene physicsScene)
        {
            var theCollider = transform.GetComponent<CircleCollider2D>();
            float radius = theCollider.bounds.extents.x;
            Destroy(theCollider);
            Shape = new Circle(transform.localToWorldFloat4x4Matrix(), radius);
            physicsScene.Add(Shape);
        }
    }
}
