using System;
using QFramework.Helpers.PlayerPrefs.Data;
using QFramework.Modules.QAds.Scripts.Config;
using QFramework.Modules.QAds.Scripts.Implementation;

namespace QFramework.Modules.QAds.Scripts.Api
{
    public static class Ads
    {
        static readonly PPBool adsDisabled = new PPBool("adsEnabled");

        static IAdsModel adsModel;
        static int interstitialShowCounter;
        public static bool AdsDisabled => adsDisabled.Value;
        public static bool IsRewardedReady => adsModel.IsRewardedReady;

        public static void Init()
        {
//            if (Application.isEditor)
//                adsModel = new TestAdsModel();
//            else
            adsModel = new AdsModel();

            adsModel.Init();
        }

        public static void ShowInterstitial()
        {
            if (adsDisabled.Value.Equals(true))
                return;

            if (AdsConfig.instance != null && interstitialShowCounter > AdsConfig.instance.interstitialAdInterval)
                interstitialShowCounter++;
            else
            {
                adsModel.ShowInterstitial();
                interstitialShowCounter = 0;
            }
        }

        public static void ShowRewarded(Action onSuccess, Action onFailure = null)
        {
            if (adsDisabled.Value)
                onSuccess?.Invoke();
            else
                adsModel.ShowRewarded(onSuccess, onFailure);
        }

        public static void ShowBanner() { }

        public static void DisableAds()
        {
            adsDisabled.Value = true;
        }
    }
}