using TMPro;
using UnityEngine;

namespace QFramework.Helpers.UI.ValueWidget
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ValueWidget<T> : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI textMesh;

        public T Value { get; private set; }

        public void SetValue(T value)
        {
            SetValue(value.ToString());
        }

        public void SetValue(string value)
        {
            textMesh.text = value;
        }
    }
}