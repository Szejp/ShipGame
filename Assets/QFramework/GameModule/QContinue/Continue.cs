using System;
using QFramework.Modules.QAds.Scripts.Api;
using QFramework.Modules.QEconomy;
using QFramework.Modules.QEconomy.Api;
using QFramework.Modules.QEconomy.Api.Currency;
using UnityEngine;

namespace QFramework.GameModule.QContinue
{
    public static class Continue
    {
        static Currency continueCost;
        static ContinueData continueData;
        static int currentLevel;

        public static event Action OnContinueSucceed;

        public static int CurrentLevel
        {
            get => currentLevel;
            set
            {
                currentLevel = value;
                RefreshContinueCost();
            }
        }

        public static bool CanContinue => Ads.IsRewardedReady; //Economy.HasCurrency(continueCost.Id, continueCost.Amount);

        public static void Init(ContinueData continueData)
        {
            if (continueData.Equals(null))
                return;
            
            Continue.continueData = continueData;
            RefreshContinueCost();
        }

        public static void ForceContinueSuccess()
        {
            OnContinueSucceed?.Invoke();
        }

        public static bool TryContinue()
        {
            bool canContinue = CanContinue;

            if (canContinue)
            {
                Economy.RemoveCurrency(continueCost.Id, continueCost.Amount);
                CurrentLevel++;
                OnContinueSucceed?.Invoke();
                Debug.Log("[QContinue] Continue succeed");
            }
            else
                Debug.Log("[QContinue] Continue failed");

            return canContinue;
        }

        static void RefreshContinueCost()
        {
            continueCost = continueData.GetCurrencyForLevel(currentLevel);
        }
    }
}