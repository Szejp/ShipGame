using UnityEngine;

namespace QFramework.External_Tools
{
    public class Vibrations
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        private static readonly AndroidJavaObject Vibrator =
            new AndroidJavaClass("com.unity3d.player.UnityPlayer") // Get the Unity Player.
                .GetStatic<AndroidJavaObject>("currentActivity") // Get the Current Activity from the Unity Player.
                .Call<AndroidJavaObject>("getSystemService",
                    "vibrator"); // Then get the Vibration Service from the Current Activity.
#endif
        
        [RuntimeInitializeOnLoadMethod]
        public static void Init()
        {
            // Trick Unity into giving the App vibration permission when it builds.
            // This check will always be false, but the compiler doesn't know that.
            if (Application.isEditor) Handheld.Vibrate();
        }

        public static void Vibrate(long milliseconds)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (!Application.isEditor)
                Vibrator.Call("vibrate", milliseconds);
#endif
        }

        public static void Vibrate(long[] pattern, int repeat)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (!Application.isEditor)
                Vibrator.Call("vibrate", pattern, repeat);
#endif
        }
    }
}