using QFramework.GameModule.QContinue;
using QFramework.Modules.Purchases;
using QFramework.Modules.QAds.Scripts.Api;
using QFramework.Modules.QConfig.Scripts;
using QFramework.Modules.QEconomy;
// using QFramework.Modules.QExperience.Scripts;
using UnityEngine;

namespace QFramework.Modules
{
    public static class Base
    {
        [RuntimeInitializeOnLoadMethod]
        static void Init()
        {
            Ads.Init();
            Configs.Init();
            // Experience.Init();
            Purchasing.Init();
            Economy.Init(Configs.GetConfig<EconomyConfig>()?.economyData);
            Continue.Init(Configs.GetConfig<ContinueConfig>()?.continueData);
        }
    }
}