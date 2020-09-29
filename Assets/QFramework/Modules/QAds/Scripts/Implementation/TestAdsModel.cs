using System;
using QFramework.Modules.QAds.Scripts.Api;
using QFramework.Modules.QAds.Scripts.Config;
using QFramework.Modules.QAds.Scripts.View;
using UnityEngine;

namespace QFramework.Modules.QAds.Scripts.Implementation
{
    public class TestAdsModel : IAdsModel
    {
        public bool IsAdsEnabled => true;
        public bool IsRewardedReady => true;

        public void Init()
        {
            Debug.Log("[TestAdsModel] test ads initialized");
        }

        public void ShowInterstitial()
        {
            ShowRewarded(null, null);
        }

        public void ShowRewarded(Action onSuccess, Action onFailure = null)
        {
            if (AdsConfig.instance.TestAds)
                GameObject.Instantiate(AdsConfig.instance.testAdsView).GetComponent<TestAdsView>()
                    .ShowRewarded(onSuccess, onFailure);
        }
    }
}