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

        public float3 Position { get { return _transform.c3.xyz; } set { _transform.c3 = new float4(value, 1.0f); } }

        public quaternion Rotation { get { return new quaternion(_transform); } set { _transform = new float4x4(value, Position); } }
        #endregion

        #region --------------------details
        float4x4 _transform;
        float3 _velocity;
        float _mass;
        #endregion
    }
}