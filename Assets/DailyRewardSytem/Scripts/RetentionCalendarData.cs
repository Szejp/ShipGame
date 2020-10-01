using UnityEngine;

namespace DailyRewardSytem {
    [CreateAssetMenu(fileName = "RetentionCalendarData", menuName = "DailyRewardSystem/RetentionCalendarData")]
    public class RetentionCalendarData: ScriptableObject
    {
        public Reward reward;
        public int amount;

        public void Claim()
        {
            Debug.Log("You claimed reward in amount of " + amount);
        }
    }
}
