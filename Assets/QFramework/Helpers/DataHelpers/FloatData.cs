using System;
using QFramework.Helpers.DataHelpers.MinMax;
using UnityEngine;

namespace QFramework.Helpers.DataHelpers
{
    [Serializable]
    public class FloatData : Data<float>
    {
        [SerializeField] protected MinMaxFloat minMaxFloat;

        public MinMaxFloat MinMaxFloat
        {
            get => minMaxFloat;
            set => minMaxFloat = value;
        }

        FloatData(float value)
        {
            valueCache = value;
            minMax = minMaxFloat;
        }
    }
}