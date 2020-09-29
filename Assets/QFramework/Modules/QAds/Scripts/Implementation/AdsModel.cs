using System;
using QFramework.Modules.QAds.Scripts.Api;
using UnityEngine.Advertisements;

namespace QFramework.Modules.QAds.Scripts.Implementation
{
    public class AdsModel : IAdsModel
    {
        const string GameId = "3813825";

        Interstitial interstitial;
        Rewarded rewarded;

        public bool IsAdsEnabled { get; }
        public bool IsRewardedReady => rewarded.IsReady;

        public void Init()
        {
            // MobileAds.Initialize(status => Debug.Log("[AdsModel] initialization status: " + status));

            interstitial = new Interstitial();
            rewarded = new Rewarded();
            Advertisement.Initialize(GameId);
        }

        public void ShowInterstitial()
        {
            interstitial.Show();
        }

        public void ShowRewarded(Action onSuccess, Action onFailure = null)
        {
            rewarded.Show(onSuccess, onFailure);
        }
    }
}