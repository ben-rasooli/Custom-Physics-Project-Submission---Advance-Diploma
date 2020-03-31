using UnityEngine;

namespace BehnamPhysicsEngine
{
    public class CircleGameObject : MonoBehaviour, IPhysicsShape
    {
        public PhysicsShape Shape { get => _shape; }

        void Awake()
        {
            var theCollider = GetComponent<CircleCollider2D>();
            float radius = theCollider.bounds.extents.x;
            Destroy(theCollider);
            _shape = new Circle(transform.localToWorldFloat4x4Matrix(), radius);
            FindObjectOfType<GameManager>().physicsScene.Add(_shape);
        }

        void Update()
        {
            var position = _shape.Position;
            transform.position = new Vector3(position.x, position.y, 0);
        }

        Circle _shape;
    }
}
