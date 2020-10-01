using UnityEngine;

namespace DailyRewardSytem {
    [CreateAssetMenu(fileName = "DailyRewardConfig", menuName = "DailyRewardSystem/DailyRewardConfig")]
    public class DailyRewardConfig : ScriptableObject
    {
        public RetentionCalendarData[] retentionCalendarData;
    }
}
