using Firebase.Analytics;

namespace QFramework.System.Analytics
{
    public static class Analytics
    {
        public static void LogEvent(string eventName)
        {
            FirebaseAnalytics.LogEvent(eventName);
        }

        public static void SetUserProperty(string propertyName, string value)
        {
            FirebaseAnalytics.SetUserProperty(propertyName, value);
        }
    }
}