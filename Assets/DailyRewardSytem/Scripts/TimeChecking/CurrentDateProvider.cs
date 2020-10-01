using System;

namespace DailyRewardSytem.TimeChecking {
    public class CurrentDateProvider : DateProvider
    {
        public DateTime ProvideDate()
        {
            return DateTime.Now;
        }
    }
}
