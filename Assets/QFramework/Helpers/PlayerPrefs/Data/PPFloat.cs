namespace QFramework.Helpers.PlayerPrefs.Data
{
    public class PPFloat : PPBase<float>
    {
        float defaultValue;
        
        public PPFloat(string key) : base(key) { }

        public PPFloat(string key, float defaultValue) : base(key)
        {
            this.defaultValue = defaultValue;
        }
    
        public override float Value
        {
            get
            {
                if (!UnityEngine.PlayerPrefs.HasKey(key))
                    return defaultValue;

                return UnityEngine.PlayerPrefs.GetFloat(key);
            }

            set
            {
                UnityEngine.PlayerPrefs.SetFloat(key, value);
            }
        }
    }
}