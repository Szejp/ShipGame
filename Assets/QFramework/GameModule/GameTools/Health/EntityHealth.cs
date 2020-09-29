using System;
using QFramework.GameModule.GameTools.Entities;
using QFramework.Helpers.Interfaces;
using UnityEngine;

namespace QFramework.GameModule.GameTools.Health
{
    public class EntityHealth : MonoBehaviour, IDamagable
    {
        public float hp = 1;
        public float maxHp = 1;

        public float Hp
        {
            get => hp;
            set
            {
                hp = value; 
                OnHealthChanged?.Invoke(hp);
            }
        }
        
        Entity entity;

        public event Action<float> OnHealthChanged;

        void Awake()
        {
            entity = GetComponent<Entity>();
        }

        public void SetDamage(float damage)
        {
            hp = Mathf.Max(0, hp - damage);
            if (hp.Equals(0))
                entity.Destroy();
        }

        public int GetSideId()
        {
            return entity.TeamId;
        }
    }
}