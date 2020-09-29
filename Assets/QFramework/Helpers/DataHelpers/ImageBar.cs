using UnityEngine;
using UnityEngine.UI;

namespace QFramework.Helpers.DataHelpers
{
    [RequireComponent(typeof(Image))]
    public class ImageBar : MonoBehaviour
    {
        [SerializeField] Image image;
        [SerializeField] FloatDataContainer DataContainer;

        FloatData floatData;

        void Awake()
        {
            floatData = DataContainer.floatData;
            floatData.OnValueChanged += UpdateProgress;
        }

        void OnDestroy()
        {
            floatData.OnValueChanged -= UpdateProgress;
        }

        void UpdateProgress(float value)
        {
            if (!floatData.HasMinMax())
                Debug.LogError(
                    "[ImageBar] You are trying to update progress but data value has no min/max border values");

            image.fillAmount = floatData.Value / floatData.MinMax.Max;
        }
    }
}