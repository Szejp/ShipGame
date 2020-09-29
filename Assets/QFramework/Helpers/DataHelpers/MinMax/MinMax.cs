using System;

namespace QFramework.Helpers.DataHelpers.MinMax
{
    /// <summary>
    /// Simple container for Min and Max value.
    /// </summary>
    [Serializable]
    public abstract class MinMax<T>
    {
        /// <summary>
        /// Minimum value.
        /// </summary>
        public virtual T Min { get; set; } 

        /// <summary>
        /// Maximum value.
        /// </summary>
        public abstract T Max { get; set; } 
    }
}