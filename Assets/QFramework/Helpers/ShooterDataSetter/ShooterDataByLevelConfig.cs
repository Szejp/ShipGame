using System;
using System.Linq;
using QFramework.Modules.QConfig.Scripts;
using QFramework.Shooting.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityExtensions = ExternalTools.Scripts.UnityExtensions;

namespace QFramework.Helpers.ShooterDataSetter
{
    [CreateAssetMenu(fileName = "ShooterDataByLevelConfig", menuName = "Game/Config/Shooting/ShooterDataByLevelConfig")]
    public class ShooterDataByLevelConfig : ConfigBase
    {
        public ShootingDataByLevel[] shootingDatasByLevels;

        public ShootingDataByLevel GetShootingDataByLevel(int level)
        {
            var matchingResults =
                shootingDatasByLevels.Where(p => level >= p.levelsRange.x && level <= p.levelsRange.y);

            return matchingResults.Any() ? UnityExtensions.RandomOne(matchingResults) : null;
        }
    }

    [Serializable]
    public class ShootingDataByLevel : ShootingData
    {
        [MinMaxSlider(0, 100)]
        public Vector2Int levelsRange = new Vector2Int(0, 200); 
    }
}