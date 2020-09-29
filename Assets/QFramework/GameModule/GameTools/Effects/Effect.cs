using UnityEngine;

namespace QFramework.GameModule.GameTools.Effects
{
    public class Effect : MonoBehaviour
    {
        [SerializeField]
        float duration;

        public virtual float Duration => duration;
    }
}