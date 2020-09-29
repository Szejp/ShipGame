using System;

namespace QFramework.System.Analytics
{
    public static class AnalyticsConsts
    {
        public static class UserProperties
        {
            public const string FirstRunTime = "first_run_time";

            public static class FirstRunTimeOfADay
            {
                public enum FirstRunTimeOfADayEnum
                {
                    Morning,
                    Afternoon,
                    Evening
                }

                public static FirstRunTimeOfADayEnum GetCurrentTimeOfADay()
                {
                    var hour = DateTime.Now.Hour;

                    if (hour < 12)
                        return FirstRunTimeOfADayEnum.Morning;
                    if (hour < 18)
                        return FirstRunTimeOfADayEnum.Afternoon;

                    return FirstRunTimeOfADayEnum.Evening;
                }
            }
        }
    }
}