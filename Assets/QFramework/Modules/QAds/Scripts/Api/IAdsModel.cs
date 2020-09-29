using System;

namespace QFramework.Modules.QAds.Scripts.Api
{
    public interface IAdsModel
    {
        bool IsAdsEnabled { get; }
        bool IsRewardedReady { get; }

        void Init();
        void ShowInterstitial();
        void ShowRewarded(Action onSuccess, Action onFailure = null);
    }
}