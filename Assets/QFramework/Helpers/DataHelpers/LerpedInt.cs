using System;
using System.Linq;
using UnityEngine;

namespace QFramework.Helpers.DataHelpers
{
    [Serializable]
    public class ExtendedLerpFloat
    {
        public Vector2[] values;

        public float First => values.First().y;

        public float Last => values.Last().y;

        public float Evaluate(int x)
        {
            if (values == null || values.Length <= 0) return 0f;

            var firstPoint = values.First();
            if (x < firstPoint.x) return firstPoint.y;

            for (int i = 1; i < values.Length; i++)
            {
                var point = values[i];
                if (x < point.x)
                {
                    var prevPoint = values[i - 1];
                    float t = (x - prevPoint.x) / (point.x - prevPoint.x);
                    return Mathf.Lerp(prevPoint.y, point.y, t);
                }
            }

            return values.Last().y;
        }

        public void Multiply(float ratio)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = new Vector2(values[i].x, values[i].y * ratio);
            }
        }
    }
}