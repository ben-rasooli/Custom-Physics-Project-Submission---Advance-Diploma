using Unity.Mathematics;

namespace BehnamPhysicsEngine
{
    public class PhysicsBody
    {
        #region --------------------interface
        public PhysicsBody(float4x4 transform, float mass)
        {
            _transform = transform;
            _mass = mass;
        }

        public void AddForce(float3 force)
        {

        }

        public float4x4 Transform() => _transform;
        #endregion

        #region --------------------details
        float4x4 _transform;
        float3 _velocity;
        float _mass;
        #endregion
    }
}