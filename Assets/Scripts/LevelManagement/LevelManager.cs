using System;
using QFramework.Helpers;
using UnityEngine;
using UnityEngine.Assertions;

namespace LevelManagement
{
    public class LevelManager
    {
        readonly LevelManagerConfig config;
        
        public int CurrentLevel
        {
            get
            {
                if (config.CurrentLevel.Equals(0))
                    config.CurrentLevel = 1;
                
                return config.CurrentLevel;
            }
            set => config.CurrentLevel = value;
        }

        public int PresetCount
        {
            get
            {
                var returnValue = (int) config.presetsPerLevel.Evaluate(CurrentLevel);
                Assert.AreNotEqual(returnValue, 0);
                return returnValue;
            }
        }

        public static event Action<int> OnLevelChanged;

        public LevelManager(LevelManagerConfig config)
        {
            this.config = config;
        }

        public void SetLevel(int level)
        {
            CurrentLevel = Mathf.Max(0, level);
            DebugHelper.DebugLog("[LevelManager] current level changed, level: " + CurrentLevel);
            OnLevelChanged?.Invoke(CurrentLevel);
        }
    }
}