using System;

namespace QFramework.Helpers.DataHelpers.MinMax
{
    [Serializable]
    public class MinMaxInt : MinMax<int>
    {
        int min;
        int max;
        
        public override int Min    
        {
            get => min;
            set => min = value;
        }

        public override int Max
        {
            get => max;
            set => max = value;
        }
    }
}