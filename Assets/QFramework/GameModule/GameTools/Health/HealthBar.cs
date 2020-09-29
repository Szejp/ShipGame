using UnityEngine;
using ProgressBar = QFramework.Helpers.UI.ProgressBar;

namespace QFramework.GameModule.GameTools.Health
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] EntityHealth health;
        [SerializeField] ProgressBar progressBar;

        void Awake()
        {
            health.OnHealthChanged += OnHealthChanged;
            OnHealthChanged(health.hp);
        }

        void OnDestroy()
        {
            health.OnHealthChanged -= OnHealthChanged;
        }

        void OnHealthChanged(float obj)
        {
            progressBar.UpdateProgress(obj / health.maxHp);
        }
    }
}