using System.Collections;
using QFramework.GameModule.GameTools.ObjectPool;
using UnityEngine;

namespace QFramework.Helpers.Spawning
{
    public class Spawner : MonoBehaviour, ICollector, ISpawner
    {
        ObjectPool pool;
        public bool IsEnabled { get; set; }

        public ObjectPool Pool
        {
            get
            {
                if (pool == null) pool = new ObjectPool();
                return pool;
            }
        }

        public Component Spawn(Component component, Vector3 position, Quaternion rotation)
        {
            var result = Spawn(component, position);
            if (result != null)
            {
                result.transform.rotation = rotation;
            }

            return result;
        }

        public Component Spawn(Component component, Transform parent)
        {
            var result = Spawn(component);
            result.transform.parent = parent;
            result.transform.position = Vector3.zero;
            return result;
        }

        public Component Spawn(Component component, Vector3 position)
        {
            var result = Spawn(component);
            if (result != null)
            {
                result.transform.position = position;
            }

            return result;
        }

        public Component Spawn(Component component)
        {
            if (component == null)
                return null;
            
            var result = Pool.GetFreeObject(component);
            result.GetComponent<IPoolCollectable>()?.SetCollector(this);
            return result;
        }

        public void Spawn(Component component, Vector3 position, Quaternion rotation, float delay, int count)
        {
            StartCoroutine(SpawnCoroutine(component, position, rotation, delay, count));
        }
        
        public void Collect(Component component)
        {
            Pool.Collect(component);
        }

        IEnumerator SpawnCoroutine(Component component, Vector3 position, Quaternion rotation, float delay, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Spawn(component, position, rotation);
                yield return new WaitForSeconds(delay);
            }
        }
    }
}