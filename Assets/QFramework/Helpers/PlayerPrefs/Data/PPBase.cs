namespace QFramework.Helpers.PlayerPrefs.Data {
    public abstract class PPBase<T>
    {
        public abstract T Value { get; set; }

        protected string key;

        public PPBase(string key)
        {
            this.key = key;
        }
    }
}
