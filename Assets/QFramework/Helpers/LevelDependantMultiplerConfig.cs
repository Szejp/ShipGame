using QFramework.Helpers.DataHelpers;
using QFramework.Modules.QConfig.Scripts;
using UnityEngine;

namespace QFramework.Helpers
{
    [CreateAssetMenu(fileName = "MultiplicaterConfig", menuName = "Game/Config/LevelManagement/LevelDependantMultiplerConfig")]
    public class LevelDependantMultiplerConfig : ConfigBase
    {
        public ExtendedLerpFloat presetsPerLevel;
        public float space = 5f;

        public int GetNumbers(int level)
        {
            return (int)presetsPerLevel.Evaluate(level);
        }
    }
}