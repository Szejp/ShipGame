using System;
using QFramework.Helpers.UI.Views;
using QFramework.Modules.QAds.Scripts.Api;
using UnityEngine;
using UnityEngine.UI;

namespace QFramework.GameModule.QContinue.View
{
    [RequireComponent(typeof(Button))]
    public class ContinueButton : GameObjectView
    {
        [SerializeField] Button button;

        public override bool CanShow => Continue.CanContinue;

        public static event Action OnContinueFailed;

        static ContinueButton instance;

        public static bool TryShow()
        {
            if (instance.CanShow)
                instance.Show();

            return instance.CanShow;
        }

        void Awake()
        {
            Hide();

            instance = this;

            if (button == null)
                button = GetComponent<Button>();

            button.onClick.AddListener(OnContinueClickedHandler);
            GameStateController.OnGameFinished += OnPlayerDiedHandler;
        }

        void OnDestroy()
        {
            button.onClick.RemoveListener(OnContinueClickedHandler);
            GameStateController.OnGameFinished -= OnPlayerDiedHandler;
        }

        void OnPlayerDiedHandler()
        {
            if (CanShow)
                Show();
            else
                FailContinue();
        }

        void FailContinue()
        {
            OnContinueFailed?.Invoke();
        }

        void OnContinueClickedHandler()
        {
            Ads.ShowRewarded(() =>
            {
                Continue.ForceContinueSuccess();
                Hide();
            }, () =>
            {
                FailContinue();
                Hide();
            });
        }
    }
}