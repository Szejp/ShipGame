namespace QFramework.Helpers.PlayerPrefs.Data {
    public class PPInt : PPBase<int>
    {
        int defaultValue;
    
        public PPInt(string key) : base(key) { }

        public PPInt(string key, int defaultValue) : base(key)
        {
            this.defaultValue = defaultValue;
        }
    
        public override int Value
        {
            get
            {
                if (!UnityEngine.PlayerPrefs.HasKey(key))
                    return defaultValue;

                return UnityEngine.PlayerPrefs.GetInt(key);
            }

            set
            {
                UnityEngine.PlayerPrefs.SetInt(key, value);
            }
        }
    }
}
