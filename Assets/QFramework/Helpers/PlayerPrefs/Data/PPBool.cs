namespace QFramework.Helpers.PlayerPrefs.Data {
    public class PPBool : PPBase<bool>
    {
        bool defaultValue;
        
        public PPBool(string key) : base(key) { }

        public override bool Value
        {
            get
            {
                if (!UnityEngine.PlayerPrefs.HasKey(key))
                    return defaultValue;

                return UnityEngine.PlayerPrefs.GetInt(key).Equals(1);
            }

            set
            {
                var result = value ? 1 : 0;
                UnityEngine.PlayerPrefs.SetInt(key, result);
            }
        }

        public void SetDefaultValue(bool defaultValue)
        {
            this.defaultValue = defaultValue;
        }
    }
}
