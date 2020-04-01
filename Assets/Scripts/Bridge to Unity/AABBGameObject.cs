using Unity.Mathematics;
using UnityEngine;

namespace BehnamPhysicsEngine
{
    public class AABBGameObject : PhysicsShapeGameObject, IPhysicsShape
    {
        public void Init(PhysicsScene physicsScene)
        {
            var theCollider = GetComponent<BoxCollider2D>();
            Vector2 extents = theCollider.bounds.extents;
            Destroy(theCollider);
            Shape = new AABB(transform.localToWorldFloat4x4Matrix(),new float2(extents.x, extents.y));
            physicsScene.Add(Shape);
        }
    }
}
