using UnityEngine;

namespace ExternalTools._2DMovements.Scripts.Helpers
{
    public static class Bezier
    {
        public static Vector3 GetPoint(Vector3 point0, Vector3 point1, Vector3 point2, Vector3 point3, float dt)
        {
            dt = Mathf.Clamp01(dt);
            float oneMinusT = 1f - dt;
            return
                oneMinusT * oneMinusT * oneMinusT * point0 +
                3f * oneMinusT * oneMinusT * dt * point1 +
                3f * oneMinusT * dt * dt * point2 +
                dt * dt * dt * point3;
        }

        public static Vector3 GetFirstDerivative(Vector3 point0, Vector3 point1, Vector3 point2, Vector3 point3, float dt)
        {
            dt = Mathf.Clamp01(dt);
            float oneMinusT = 1f - dt;
            return
                3f * oneMinusT * oneMinusT * (point1 - point0) +
                6f * oneMinusT * dt * (point2 - point1) +
                3f * dt * dt * (point3 - point2);
        }
    }
}