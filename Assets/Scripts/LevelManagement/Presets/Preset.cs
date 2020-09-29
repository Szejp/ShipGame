using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LevelManagement.Presets
{
    [Serializable]
    public class Preset
    {
        public Container container;

        [TableColumnWidth(100, Resizable = false)]
        public bool disabled;

        [TableColumnWidth(100, Resizable = false)]
        public bool spawnThisOnly;

        [TableColumnWidth(100, Resizable = false)]
        public bool singleSpawn;

        [MinMaxSlider(0, 100)] public Vector2Int levelsRange = new Vector2Int(0, 200);
        public AdditionalPresetTypes additionalTypes;

        [HideInInspector] public int pickedCount;

        public float GetSpawnWeight()
        {
            return 1 / (float) (pickedCount + 1);
        }
    }
}