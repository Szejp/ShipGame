// using Game.Scripts.Core;

using QFramework.Shooting.Scripts;
using UnityEngine;

namespace QFramework.Helpers.ShooterDataSetter
{
    public class ShooterDataByLevelSetter : MonoBehaviour
    {
        [SerializeField] ShootingSystem shootingSystem;
        [SerializeField] ShooterDataByLevelConfig config;

        void Awake()
        {
//            if (shootingSystem != null)
//                shootingSystem = GetComponent<ShootingSystem>();
//
//            var shooterData = config.GetShootingDataByLevel(GameContext.LevelManager.CurrentLevel);
//            if (shooterData != null)
//                shootingSystem.SetShootingData(config.GetShootingDataByLevel(GameContext.LevelManager.CurrentLevel));
        }
    }
}