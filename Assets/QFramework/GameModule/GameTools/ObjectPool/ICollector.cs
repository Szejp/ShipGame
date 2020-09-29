using UnityEngine;

namespace QFramework.GameModule.GameTools.ObjectPool {
    public interface ICollector{
        void Collect(Component objToCollect);
    }
}