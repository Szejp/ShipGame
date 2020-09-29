using UnityEngine;

namespace QFramework.GameModule.GameTools.ObjectPool
{
    public interface ISpawner : ICollector
    {
        Component Spawn(Component component, Vector3 position, Quaternion rotation);
        Component Spawn(Component component, Transform parent);
        Component Spawn(Component component, Vector3 position);
        Component Spawn(Component component);
        void Spawn(Component component, Vector3 position, Quaternion rotation, float delay, int count);
    }
}