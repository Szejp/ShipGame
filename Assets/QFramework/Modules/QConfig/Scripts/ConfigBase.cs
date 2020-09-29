using System;
#if Firebase_config
using Firebase.RemoteConfig;
#endif
using UnityEngine;

namespace QFramework.Modules.QConfig.Scripts {
    public class ConfigBase : ScriptableObject
    {
        [SerializeField] string id;

        public string Id
        {
            get
            {
                if (id == string.Empty)
                    id = name;
                
                return id;
            }
        }

        public event Action OnUpdated;
        
#if Firebase_config

        public virtual void Setup()
        {
            TryGetRemoteConfigValues();
        }

        void DeserialiseJson(string json)
        {
            if (string.IsNullOrEmpty(json))
                return;

            JsonUtility.FromJsonOverwrite(json, this);
            OnUpdated?.Invoke();
        }

        void TryGetRemoteConfigValues()
        {
            var configValue = FirebaseRemoteConfig.GetValue(Id);
            try
            {
                if (configValue.Source.Equals(ValueSource.DefaultValue))
                {
                    Debug.LogWarning("[Remote config] Trying to get default value from the remote config");
                    return;
                }

                DeserialiseJson(configValue.StringValue);
                Debug.Log("[Remote config] deserialised successfuly");
            }
            catch (ArgumentException e)
            {
                Debug.LogWarning(e.Message);
            }
        }
#endif
    }
}

