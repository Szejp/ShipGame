using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace QFramework.Helpers.UI {
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] Image image;
        [SerializeField] ProgressBarData data;

        public void UpdateProgress(float progress, bool immediately = true)
        {
            StartCoroutine(UpdateProgressCoroutine(progress, immediately));
        }

        IEnumerator UpdateProgressCoroutine(float progress, bool immediately = true)
        {
            float timeElapsed = 0;

            while (timeElapsed < data.updateTime && !immediately)
            {
                image.fillAmount = Mathf.Lerp(image.fillAmount, progress, timeElapsed);
                yield return new WaitForEndOfFrame();
            }

            Debug.Log("[ProgressBar] progress updated, progress: " + progress);
            image.fillAmount = progress;
        }
    }

    [Serializable]
    public class ProgressBarData
    {
        public float updateTime = 1f;
    }
}