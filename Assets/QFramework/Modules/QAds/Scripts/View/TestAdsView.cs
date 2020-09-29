using System;
using UnityEngine;
using UnityEngine.UI;

namespace QFramework.Modules.QAds.Scripts.View
{
    public class TestAdsView : MonoBehaviour
    {
        [SerializeField] Button successButton;
        [SerializeField] Button skipButton;
        [SerializeField] Button closeButton;

        Action onSuccess;
        Action onFailure;

        public void ShowRewarded(Action onSuccess, Action onFailure)
        {
            this.onSuccess = onSuccess;
            this.onFailure = onFailure;

            gameObject.SetActive(true);
        }

        void Awake()
        {
            successButton.onClick.AddListener(ProceedSuccess);
            skipButton.onClick.AddListener(ProceedSkip);
            closeButton.onClick.AddListener(ProceedClose);
        }

        void ProceedSuccess()
        {
            onSuccess?.Invoke();
        }

        void ProceedSkip()
        {
            onFailure?.Invoke();
            gameObject.SetActive(false);
        }

        void ProceedClose()
        {
            onFailure?.Invoke();
            gameObject.SetActive(false);
        }
    }
}