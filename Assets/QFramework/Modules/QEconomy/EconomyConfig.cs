using QFramework.Modules.QConfig.Scripts;
using QFramework.Modules.QEconomy.Api;
using UnityEngine;

namespace QFramework.Modules.QEconomy
{
    [CreateAssetMenu(fileName = "EconomyConfig", menuName = "Q/Config/EconomyConfig")]
    public class EconomyConfig : ConfigBase
    {
        public EconomyData economyData;
    }
}