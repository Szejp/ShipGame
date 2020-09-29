#if Firebase_config

using System;
using System.Linq;
using System.Threading.Tasks;
using Firebase.RemoteConfig;
using UnityEngine;

namespace QFramework.Modules.QConfig.Scripts
{
    public static class Configs
    {
        static ConfigBase[] configs;
        static bool isInitialized;

        public static T GetConfig<T>() where T : ConfigBase
        {
            if (!isInitialized)
                Init();

            var id = typeof(T).Name;
            
            if(configs== null)
                Debug.LogError("[Configs] configs array is null!");

            try
            {
                var config = configs.Where(p => p.Id != null).FirstOrDefault(p => p.Id.Equals(id)) as T;
                if (config == null)
                    Debug.LogError(
                        "[Configs] Config you were searching for is null. Make sure it is in the resources folder: " + id);

                return config;
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }

            return null;
        }

        public static void Init()
        {
            configs = Resources.LoadAll<ConfigBase>(string.Empty);
            Debug.Log("[Configs] loaded configs: " + string.Join("\n", configs.Select(p => p.Id))); 
            isInitialized = true;
        }

        public static Task FetchRemoteConfig()
        {
            return FirebaseRemoteConfig.FetchAsync(TimeSpan.Zero)
                .ContinueWith(task => FirebaseRemoteConfig.ActivateFetched());
        }

        public static void RefreshConfigsData()
        {
            if (!isInitialized)
                Init();

            try
            {
                foreach (var c in configs)
                    c.Setup();

                Debug.Log("[ConfigsInitializer] Initialised");
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}

#endif