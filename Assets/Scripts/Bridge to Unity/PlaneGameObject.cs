using UnityEngine;

namespace BehnamPhysicsEngine
{
    public class PlaneGameObject : PhysicsShapeGameObject, IPhysicsShape
    {
        public void Init(PhysicsScene physicsScene)
        {
            Shape = new Plane(transform.localToWorldFloat4x4Matrix());
            physicsScene.Add(Shape);
        }
    }
}
