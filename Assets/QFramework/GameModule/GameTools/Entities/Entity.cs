using System;
using System.Collections;
using QFramework.GameModule.GameTools.Effects;
using QFramework.GameModule.GameTools.ObjectPool;
using QFramework.Helpers;
using UnityEngine;
using UnityEngine.Events;

namespace QFramework.GameModule.GameTools.Entities
{
    public class Entity : MonoBehaviour, IPoolCollectable
    {
        public UnityAction<Entity> OnDestroyed;
        public static Action<Entity> OnDestroyedCallback;

        [SerializeField] EntityConfig entityConfig;
        [SerializeField] int teamId;
        [SerializeField] protected bool isImmortal;

        ICollector collector;
        bool destroyedAlready;

        public UnityEvent OnSetImmortal;
        public UnityEvent OnImmortalityLost;

        public int TeamId
        {
            private set => teamId = value;
            get => teamId;
        }

        public bool IsAlive => !destroyedAlready;
        public Effect DestroyEffect => entityConfig?.destroyEffect;

        public void Destroy(bool forceKill = false, UnityAction onDestroyedCallback = null)
        {
            StartCoroutine(DestroyCoroutine(forceKill, onDestroyedCallback));
        }

        public void SetImmortal(float time, Action onImmortalityLostCallback = null)
        {
            isImmortal = true;
            OnSetImmortal?.Invoke();

            if (time != -1)
            {
                DelayedCall.Call(() =>
                {
                    isImmortal = false;
                    OnImmortalityLost?.Invoke();
                    onImmortalityLostCallback?.Invoke();
                }, time);
            }
        }

        public void SetTeamId(int teamId)
        {
            TeamId = teamId;
        }

        public virtual void SetCollector(ICollector collector)
        {
            this.collector = collector;
        }

        public virtual void Collect()
        {
            if (collector == null)
                Debug.LogWarning(
                    "[Entity] you are trying to colllect an object without a collector reference set. Name: " +
                    gameObject.name);

            collector?.Collect(this);
        }

        void HandleEntityDestroyed(bool force = false, UnityAction onDestroyedCallback = null)
        {
            if (isImmortal && !force)
                return;
            
            DestroyCallback();
            onDestroyedCallback?.Invoke();
            OnDestroyed?.Invoke(this);
            OnDestroyedCallback?.Invoke(this);
            destroyedAlready = true;
            Collect();
        }

        protected virtual void DestroyCallback() { }

        protected virtual void OnEnable()
        {
            destroyedAlready = false;
        }

        IEnumerator DestroyCoroutine(bool forceKill = false, UnityAction onDestroyedCallback = null)
        {
            if (destroyedAlready)
                yield return null;

            var time = entityConfig != null ? entityConfig.destroyTime : 0;
            yield return new WaitForSeconds(time);
            HandleEntityDestroyed();
        }
    }
}