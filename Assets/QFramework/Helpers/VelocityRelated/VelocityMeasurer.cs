using UnityEngine;

namespace QFramework.Helpers.VelocityRelated
{
    public class VelocityMeasurer : MonoBehaviour
    {
        Vector3 previousPosition;
        Vector3 velocity;

        public Vector3 Velocity => velocity;
        public float velocitySqrMagnitude;

        protected virtual Vector3 Position => transform.position;

        protected virtual void Update()
        {
            velocity = previousPosition.Equals(Vector3.zero) ? Vector3.zero : Position - previousPosition;
            previousPosition = Position;
            velocitySqrMagnitude = Velocity.sqrMagnitude;
        }
    }
}