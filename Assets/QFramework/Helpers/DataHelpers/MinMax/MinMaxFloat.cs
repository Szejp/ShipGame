using System;
using UnityEngine;

namespace QFramework.Helpers.DataHelpers.MinMax
{
    [Serializable]
    public class MinMaxFloat : MinMax<float>
    {
        public Vector2 values = Vector2.zero;

        public override float Max
        {
            get => values.y;
            set => values = new Vector2(values.x, value);
        }

        public override float Min
        {
            get => values.x;
            set => values = new Vector2(value, values.y);
        }

        public MinMaxFloat(float min, float max)
        {
            Min = min;
            Max = max;
        }
    }
}