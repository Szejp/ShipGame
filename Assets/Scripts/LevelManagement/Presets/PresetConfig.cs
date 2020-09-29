using UnityEngine;

namespace LevelManagement.Presets
{
    [CreateAssetMenu(fileName = "PresetConfig", menuName = "Game/Config/LevelManagement/PresetConfig")]
    public class PresetConfig : ScriptableObject
    {
        public Preset preset;
    }
}