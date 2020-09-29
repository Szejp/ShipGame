using System;
using Config;
using Game.Scripts.Spawning;
using LevelManagement;
using LevelManagement.Presets;
using QFramework.Helpers.Spawning;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.LevelManagement
{
    namespace LevelManagement.SpawnStrategies
    {
        public class SimpleSpawnStrategy : MonoBehaviour
        {
            [SerializeField] SpawnStrategyConfig config;
            [SerializeField] GameConfig gameConfig;

            static bool isSpawning;

            Spawner spawner;
            LevelManager levelManager;
            float playerHeight;
            float lastSize;
            float spawnedheight;
            float lastFuelSpawnHeight;
            int spawnedCount;

            float LastHeight { get; set; }

            public static void EnableSpawning(bool isEnabled)
            {
                isSpawning = isEnabled;
            }

            void Awake()
            {
                spawner = GameContext.Spawner;
                levelManager = GameContext.LevelManager;
            }

            void Update()
            {
                playerHeight += config.playerSpeed * Time.deltaTime;

                if (!isSpawning)
                    return;

                if (playerHeight >= Mathf.Max(0, LastHeight - config.distanceBetweenPresets - lastSize))
                {
                    if (spawnedCount >= levelManager.PresetCount)
                    {
                        Spawn(config.GetPortalContainer());
                        EnableSpawning(false);
                    }
                    else if (ShouldSpawnFuelPreset())
                    {
                        SpawnCustomPreset(p => p.additionalTypes.isFuel);
                        LastHeight += spawnedheight;
                        lastFuelSpawnHeight = LastHeight;
                    }
                    else if (ShouldSpawnCustomPreset())
                    {
                        SpawnCustomPreset();
                        LastHeight += spawnedheight;
                    }
                }
            }

            void SpawnCustomPreset(Func<Preset, bool> presetSelector = null)
            {
                Spawn(config.GetPreset(levelManager.CurrentLevel, presetSelector));
                spawnedCount++;
            }

            void Spawn(Container container)
            {
                var spawned = spawner.Spawn(container) as Container;

                if (spawned != null)
                {
                    lastSize = spawned.GetYSize();
                    spawnedheight = lastSize + config.distanceBetweenPresets;
                    spawned.transform.position = new Vector3(0, LastHeight + spawnedheight, 0);
                }
            }

            void OnDrawGizmos()
            {
                Gizmos.DrawCube(Vector3.one * 4 + Vector3.up * (LastHeight - 4),
                    Vector3.one * 4 + Vector3.up * (lastSize - 4));
            }

            bool ShouldSpawnCustomPreset()
            {
                var variable = Random.Range(0, 1f);
                return variable < config.customPresetSpawnChance;
            }

            bool ShouldSpawnFuelPreset()
            {
                return playerHeight - lastFuelSpawnHeight > gameConfig.distanceBetweenFuels;
            }
        }
    }
}