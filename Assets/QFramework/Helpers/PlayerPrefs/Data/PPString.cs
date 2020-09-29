namespace QFramework.Helpers.PlayerPrefs.Data {
    public class PPString: PPBase<string>
    {
        public PPString( string key ) : base(key) { }

        public override string Value {
            get {
                if ( !UnityEngine.PlayerPrefs.HasKey(key) )
                    return "";

                return UnityEngine.PlayerPrefs.GetString(key);
            }

            set {
                UnityEngine.PlayerPrefs.SetString(key, value);
            }
        }
    }
}
