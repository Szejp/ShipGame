using System;

namespace DailyRewardSytem.TimeChecking {
    public static class NextDayChecker
    {
        public static bool CheckIfAtLeastOneDayPassed(DateTime dayBefore, DateTime dayAfrer)
        {
            if (dayBefore == null)
                return true;
            if (dayBefore.Year < dayAfrer.Year)
                return true;
            else if (dayBefore.Year > dayAfrer.Year)
                return false;

            if (dayBefore.Month < dayAfrer.Month)
                return true;
            else if (dayBefore.Month > dayAfrer.Month)
                return false;
            if (dayBefore.Day < dayAfrer.Day)
                return true;
            return false;
        }
    }
}
