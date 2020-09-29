using System;
using QFramework.Helpers.DataHelpers.MinMax;
using UnityEngine;

namespace QFramework.Helpers.DataHelpers {
    [Serializable]
    public class Data<T> where T : IComparable
    {
        [SerializeField] protected T valueCache;
        [SerializeField] protected MinMax<T> minMax;

        public event Action<T> OnValueChanged;

        public T Value
        {
            get => valueCache;
            set
            {
                valueCache = value;

                if (HasMinMax())
                {
                    if (value.CompareTo(minMax.Min) < 0)
                        valueCache = minMax.Min;

                    if (value.CompareTo(minMax.Max) > 0)
                        valueCache = minMax.Max;
                }

                OnValueChanged?.Invoke(valueCache);
            }
        }

        public MinMax<T> MinMax
        {
            get => minMax;
            set => minMax = value;
        }

        public bool HasMinMax()
        {
            return minMax != null;
        }
    }
}