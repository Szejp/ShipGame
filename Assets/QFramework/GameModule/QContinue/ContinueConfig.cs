using QFramework.Modules.QConfig.Scripts;
using UnityEngine;

namespace QFramework.GameModule.QContinue
{
    [CreateAssetMenu(fileName = "ContinueConfig", menuName = "Q/Config/ContinueConfig")]
    public class ContinueConfig : ConfigBase
    {
        public ContinueData continueData;
    }
}