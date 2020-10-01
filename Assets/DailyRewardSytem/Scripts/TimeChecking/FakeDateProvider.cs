using System;

namespace DailyRewardSytem.TimeChecking {
    public class FakeDateProvider : DateProvider
    {


        private DateTime dateTime;

        public FakeDateProvider(int month, int day, int hour)
        {
            dateTime = new DateTime(2020, month, day, hour, 0, 0);
        }
    
        public void SetDate(int month, int day, int hour)
        {
            dateTime = new DateTime(2020, month, day, hour, 0, 0);
        } 
        public void SetDate(DateTime dateTime)
        {
            this.dateTime = dateTime;
        }
    

        public DateTime ProvideDate()
        {
            return dateTime;
        }
    }
}
