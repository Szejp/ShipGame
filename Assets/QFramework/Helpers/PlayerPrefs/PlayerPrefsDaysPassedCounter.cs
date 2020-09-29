using System;

namespace QFramework.Helpers.PlayerPrefs
{
    public static class PlayerPrefsDaysPassedCounter
    {
        public static bool HasDate(string datePrefKey)
        {
            return UnityEngine.PlayerPrefs.HasKey(datePrefKey);
        }

        public static DateTime InitializeStartDate(string datePrefKey)
        {
            var startDate = DateTime.Now; //save the start date ->
            UnityEngine.PlayerPrefs.SetString(datePrefKey, startDate.ToString());
            return startDate;
        }

        public static DateTime GetStartDate(string datePrefKey)
        {
            DateTime startDate;
            if (HasDate(datePrefKey))
                startDate = Convert.ToDateTime(UnityEngine.PlayerPrefs.GetString(datePrefKey));
            else
                startDate = InitializeStartDate(datePrefKey);

            return startDate;
        }

        public static int GetDaysPassed(string datePrefKey)
        {
            var today = DateTime.Now;
            var startDate = GetStartDate(datePrefKey);
            TimeSpan elapsed = today.Subtract(startDate);
            var days = (int)elapsed.TotalDays;
            return days;
        }
    }
}