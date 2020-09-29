using System;
using System.Collections.Generic;
using System.Linq;
using QFramework.Helpers;
using UnityEditor;
using UnityEngine;

namespace LevelManagement.Presets
{
    [CreateAssetMenu(fileName = "PresetManager", menuName = "Game/Config/LevelManagement/PresetManager")]
    public class PresetsManager : ScriptableObject
    {
        public string presetsPath;
        public string containersPath;

        public static PresetsManager instance;

        public Container portalContainer;
        public List<Preset> presets;

        public PresetsManager()
        {
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged += OnPlayModeChangedHandler;
            instance = this;
#endif
        }

        ~PresetsManager()
        {
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged -= OnPlayModeChangedHandler;
#endif
        }

        [ContextMenu("LoadPresets")]
        public void LoadAll()
        {
#if UNITY_EDITOR
            var prefabs = AssetsLoader.LoadAllFromPath<PresetConfig>(presetsPath, "*.asset");
            var loadedPresets = prefabs.Select(p => ((PresetConfig) p).preset);
            presets = loadedPresets.Where(p => !p.disabled).ToList();
            var selectedPresets = presets.Where(p => p.spawnThisOnly).ToList();
            if (selectedPresets.Any())
                presets = selectedPresets;
            Debug.Log("[PresetManager] refreshed presets");
#endif
        }

        public List<Container> GetContainersFromPath()
        {
#if UNITY_EDITOR
            var containers = AssetsLoader.LoadAllFromPath<Container>(containersPath, "*.asset");
            return containers;
#endif
            return null;
        }

        public List<Preset> GetMatchingPresetsList(int level)
        {
            return presets.Where(p => level >= (p.levelsRange).x && level <= (p.levelsRange).y).ToList();
        }

#if UNITY_EDITOR
        void OnPlayModeChangedHandler(PlayModeStateChange playMode)
        {
            if (playMode.Equals(PlayModeStateChange.EnteredPlayMode) && !String.IsNullOrEmpty(containersPath))
            {
                Debug.Log("[PresetManager] Loading presets");
                LoadAll();
            }
        }
#endif
    }
}