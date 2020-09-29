using QFramework.Helpers.DataHelpers.MinMax;
using UnityEngine;

namespace Utils
{
    [System.Serializable]
    public class VectorMinMax : MinMax<Vector3>
    {
        [SerializeField] Vector3 min;
        [SerializeField] Vector3 max;

        public override Vector3 Min
        {
            get => min;
            set => min = value;
        }

        public override Vector3 Max
        {
            get => max;
            set => max = value;
        }
    }
}