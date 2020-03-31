using Unity.Mathematics;
using UnityEngine;

namespace BehnamPhysicsEngine
{
    public class AABBGameObject : MonoBehaviour, IPhysicsShape
    {
        public PhysicsShape Shape => _shape;

        void Awake()
        {
            var theCollider = GetComponent<BoxCollider2D>();
            Vector2 extents = theCollider.bounds.extents;
            Destroy(theCollider);
            _shape = new AABB(transform.localToWorldFloat4x4Matrix(),new float2(extents.x, extents.y));
            FindObjectOfType<GameManager>().physicsScene.Add(_shape);
        }

        AABB _shape;
    }
}
