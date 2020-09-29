using QFramework.Modules.QConfig.Scripts;
using UnityEngine;

namespace QFramework.Helpers.DataHelpers.ScriptableData
{
    [CreateAssetMenu(fileName = "GamesCountDeactivatorConfig", menuName = "Game/Config/UI/GamesCountDeactivatorConfig")]
    public class GamesCountDeactivatorConfig : ConfigBase
    {
        public int gamesCount;
    }
}