using DailyRewardSytem.TimeChecking;
using UnityEngine;

namespace DailyRewardSytem {
    public class RewardButtonMenager : MonoBehaviour
    {
        public DailyRewardConfig DailyRewardConfig;

        public GameObject ParentWithGridLayout;
        public GameObject RewardBoxPrefab;

        private int readyToClaimRewardIndex = -1;

        private RetentionRewardTimeSystem rewardTimeSystem;

        protected void Awake()
        {
            rewardTimeSystem = new RetentionRewardTimeSystem("daily-reward", new CurrentDateProvider());

            //tym mozna sprawdzić jak działa
            // FakeDateProvider fakeDateProvider = new FakeDateProvider(2, 2, 2);
            // rewardTimeSystem = new RetentionRewardTimeSystem("daily-reward", fakeDateProvider);

            //tym mozna zresetowac
            //rewardTimeSystem.ClearAllData();

            RetentionRewardCalendarData data = rewardTimeSystem.RewardCalendarData;
            //debug
            // RetentionRewardCalendarData data = new RetentionRewardCalendarData(
            //     3, 
            //     true
            //     );

            for (int i = 0; i < DailyRewardConfig.retentionCalendarData.Length; i++)
            {
                GameObject rewardBoxObject = Instantiate(RewardBoxPrefab, ParentWithGridLayout.transform);
                RewardBox rewardBox = rewardBoxObject.GetComponent<RewardBox>();

                rewardBox.SetDay(i + 1);
                rewardBox.SetAmount(DailyRewardConfig.retentionCalendarData[i].amount);
                rewardBox.SetRewardSprite(DailyRewardConfig.retentionCalendarData[i].reward.sprite);

                if (i < data.RewardsClaimed)
                    rewardBox.ChangeState(RewardBoxState.Claimed);
                else if (i == data.RewardsClaimed)
                {
                    if (data.CanNextRewardBeClaimed)
                    {
                        rewardBox.ChangeState(RewardBoxState.RewardAvailable);
                        rewardBox.Button.onClick.AddListener(ClaimReward);
                        readyToClaimRewardIndex = i;
                    }
                    else
                        rewardBox.ChangeState(RewardBoxState.Inactive);
                }
                else
                    rewardBox.ChangeState(RewardBoxState.Inactive);
            }
        }

        private void ClaimReward()
        {
            DailyRewardConfig.retentionCalendarData[readyToClaimRewardIndex].Claim();
            rewardTimeSystem.Claim();
        }
    }
}