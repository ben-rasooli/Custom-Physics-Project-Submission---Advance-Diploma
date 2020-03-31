using UnityEngine;

namespace BehnamPhysicsEngine
{
    public class PlaneGameObject : MonoBehaviour
    {
        void Awake()
        {
            var shape = new Plane(transform.localToWorldFloat4x4Matrix());
            FindObjectOfType<GameManager>().physicsScene.Add(shape);
        }
    }
}
