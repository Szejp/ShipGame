using System;
using System.Globalization;
using UnityEngine;

namespace DailyRewardSytem.TimeChecking {
    public class RetentionRewardTimeSystem
    {
        private string key;
        private const string PREFIX = "RRTS$-%";
    
        private const string REWARDS_CLAIMED = "claimed";
        private const string LAST_CLAIM = "$&";

        private DateProvider dateProvider;
    
        public RetentionRewardCalendarData RewardCalendarData
        {
            get
            {
                return new RetentionRewardCalendarData(
                    PlayerPrefs.GetInt(PREFIX + key + REWARDS_CLAIMED, 0), 
                    NextDayChecker.CheckIfAtLeastOneDayPassed(getLastestClaimDate(), dateProvider.ProvideDate())
                );
            }
        }

        private DateTime getLastestClaimDate()
        {
            const string FMT = "O";
            string strDate = PlayerPrefs.GetString(PREFIX + key + LAST_CLAIM);
            DateTime dateTime = DateTime.MinValue;
            try
            {
                dateTime = DateTime.ParseExact(strDate, FMT, CultureInfo.InvariantCulture);
            }
            catch (System.FormatException e)
            {
            }

            return dateTime;
        }

        public RetentionRewardTimeSystem(string key, DateProvider dateProvider)
        {
            this.key = key;
            this.dateProvider = dateProvider;
        }
    
        public void ClearAllData()
        {
            PlayerPrefs.DeleteKey(PREFIX + key + LAST_CLAIM);
            PlayerPrefs.DeleteKey(PREFIX + key + REWARDS_CLAIMED);
        }

        public bool Claim()
        {
            if(!NextDayChecker.CheckIfAtLeastOneDayPassed(getLastestClaimDate(), dateProvider.ProvideDate()))
            {
                return false;
            }
        
            int numberOfClaims = PlayerPrefs.GetInt(PREFIX + key + REWARDS_CLAIMED, 0);
            numberOfClaims++;
            PlayerPrefs.SetInt(PREFIX + key + REWARDS_CLAIMED, numberOfClaims);
        
            const string FMT = "O";
            string strDate = dateProvider.ProvideDate().ToString(FMT);
            PlayerPrefs.SetString(PREFIX + key + LAST_CLAIM, strDate);
        
            return true;
        }

    

    }

    public struct RetentionRewardCalendarData
    {
        public int RewardsClaimed;
        public bool CanNextRewardBeClaimed;

        public RetentionRewardCalendarData(int rewardsClaimed, bool canNextRewardBeClaimed)
        {
            RewardsClaimed = rewardsClaimed;
            CanNextRewardBeClaimed = canNextRewardBeClaimed;
        }
    }
}