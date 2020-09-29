using QFramework.Helpers.PlayerPrefs.Data;
using UnityEngine;

namespace QFramework.Helpers.DataHelpers
{
    [CreateAssetMenu(fileName = "IntPPScriptableData", menuName = "Data/IntPPScriptableData")]
    public class IntPPScriptableData : ScriptableObject
    {
        [SerializeField] int value;
        [SerializeField] string ppKey;

        PPInt ppIntValue;

        public int Value
        {
            get
            {
                ValuesCheck();
                return ppIntValue.Value;
            }
            set
            {
                this.value = value;
                ValuesCheck();
            }
        }

        void ValuesCheck()
        {
            if (ppIntValue == null)
                ppIntValue = new PPInt(ppKey);

            if (ppIntValue.Value != value)
                ppIntValue.Value = value;
        }
    }
}