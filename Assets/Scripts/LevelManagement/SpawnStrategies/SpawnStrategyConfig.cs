using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.LevelManagement;
using QFramework.Modules.QConfig.Scripts;
using UnityEngine;

namespace Game.Scripts.Spawning
{
    [CreateAssetMenu(fileName = "SpawnStrategyConfig", menuName = "Game/Config/Spawning/SpawnStrategyConfig",
        order = 0)]
    public class SpawnStrategyConfig : ConfigBase
    {
        [SerializeField] PresetsManager presetManager;
        [SerializeField] Container defaultElement;

        public float distanceBetweenPresets = 2;
        public float startingHeight = 10;
        public float playerSpeed = 2;
        [Range(0, 1)] public float customPresetSpawnChance;
        [Range(0, 1)] public float defaultElementSpawnChance = .3f;

        Dictionary<int, List<Preset>> cachedPresets = new Dictionary<int, List<Preset>>();
        List<Preset> currentPresetsList;

        int shootersCount;
        

        public Container GetPreset(int level, Func<Preset, bool> selector)
        {
            if (!cachedPresets.ContainsKey(level))
                cachedPresets.Add(level, presetManager.GetMatchingPresetsList(level));

            currentPresetsList = cachedPresets[level];
            shootersCount = currentPresetsList.Count(p => p.additionalTypes.isShooter);

            if (currentPresetsList == null || currentPresetsList.Count.Equals(0))
                return null;

            if (selector != null)
                currentPresetsList = currentPresetsList.Where(selector).ToList();

            var presetToSpawn = currentPresetsList.WeightedRandomOne(p => GetWeight(p));
            presetToSpawn.pickedCount++;
            
            if (presetToSpawn.singleSpawn)
                currentPresetsList.Remove(presetToSpawn);

            return presetToSpawn.container;
        }

        public Container GetDefaultElement()
        {
            return defaultElement;
        }

        public Container GetPortalContainer()
        {
            return presetManager.portalContainer;
        }

        float GetWeight(Preset p)
        {
            var weight = p.GetSpawnWeight();
            if (p.additionalTypes.isShooter)
                weight = GetWeightModifiedNormalised(weight, shootersCount);
            else
                weight = GetWeightModifiedNormalised(weight, currentPresetsList.Count - shootersCount);

            return weight;
        }

        float GetWeightModifiedNormalised(float weight, float shootersCountRelatedModifier)
        {
            return weight * shootersCountRelatedModifier / currentPresetsList.Count;
        }
    }
}