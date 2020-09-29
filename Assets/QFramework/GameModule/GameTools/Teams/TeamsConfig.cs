using System.Collections.Generic;
using QFramework.Modules.QConfig.Scripts;
using UnityEngine;

namespace QFramework.GameModule.GameTools.Teams
{
    [CreateAssetMenu(fileName = "TeamsConfig", menuName = "Game/Config/Teams/TeamsConfig")]
    public class TeamsConfig : ConfigBase
    {
        [Tooltip("Each team id should be divided by ',' character")]
        public List<string> teams;
    }
}