using UnityEngine;

namespace QFramework.Helpers
{
    public static class DebugHelper
    {
        public static void DebugLog(string log)
        {
            if (Debug.isDebugBuild)
                Debug.Log(log);
        }

        public static void DebugFormat(string log)
        {
            if(Debug.isDebugBuild)
                Debug.LogFormat(log);
        }
    }
}