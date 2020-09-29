using LevelManagement.Presets;
using UnityEngine;

namespace LevelManagement
{
    [System.Serializable]
    public class Level
    {
        public int id;
        public Preset[] presets;

        public Preset GetRandomPreset()
        {
            return presets[Random.Range(0, presets.Length)];
        }
    }
}