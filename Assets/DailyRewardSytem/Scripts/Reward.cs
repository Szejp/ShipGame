using UnityEngine;

namespace DailyRewardSytem {
    // [CreateAssetMenu(fileName = "DailyReward", menuName = "DailyRewardSystem/DailyReward")]
    public abstract class Reward : ScriptableObject
    {
        public Sprite sprite;
        
        public abstract void Claim();
    }
}
