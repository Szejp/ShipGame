using System;
using QFramework.GameModule.GameTools.Entities;
using QFramework.GameModule.GameTools.ObjectPool;
using UnityEngine;

namespace LevelManagement {
    public class Container : Entity
    {
        public static Action<Container> OnSpawned;
        public static Action<Container> OnCollected;
        [SerializeField] float height = 1;

        public float Height => height;

        public float GetYSize()
        {
            return height;
        }

        public override void SetCollector(ICollector collector)
        {
            base.SetCollector(collector);
            OnSpawned?.Invoke(this);
        }

        public override void Collect()
        {
            base.Collect();
            OnCollected?.Invoke(this);
        }
    }
}

