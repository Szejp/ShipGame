using QFramework.Modules.QConfig.Scripts;
using UnityEngine;

namespace QFramework.Modules
{
    public static class BaseModule
    {
        [RuntimeInitializeOnLoadMethod]
        public static void Init()
        {
            Configs.Init();
            Debug.Log("Base module init");
        }
    }
}