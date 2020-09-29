using QFramework.Helpers.DataHelpers;
using QFramework.Helpers.PlayerPrefs.Data;
using QFramework.Modules.QConfig.Scripts;
using UnityEngine;

namespace LevelManagement
{
    [CreateAssetMenu(fileName = "LevelManagerConfig", menuName = "Game/Config/LevelManagement/LevelManagerConfig")]
    public class LevelManagerConfig : ConfigBase
    {
        [SerializeField]
        int currentLevel;
        
        public ExtendedLerpFloat presetsPerLevel;

        public int CurrentLevel
        {
            get => currentLevel;
            set
            {
                CurrentLevelPP = value;
                currentLevel = value;
            }
        }

        PPInt currentLevelPP;

        public int CurrentLevelPP
        {
            get
            {
                if(currentLevelPP == null)
                    currentLevelPP = new PPInt("CurrentLevel");
                
                return currentLevelPP.Value;
            }
            set
            {
                if(currentLevelPP == null)
                    currentLevelPP = new PPInt("CurrentLevel");
                
                currentLevelPP.Value = value;
            }
        }
    }
}