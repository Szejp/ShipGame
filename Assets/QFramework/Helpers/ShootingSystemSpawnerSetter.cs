// using Game.Scripts.Core;

using QFramework.Shooting.Scripts;
using UnityEngine;

namespace QFramework.Helpers
{
    public class ShootingSystemSpawnerSetter : MonoBehaviour
    {
        void Awake()
        {
            var systems = GetComponentsInChildren<ShootingSystem>();
   //         foreach (var s in systems)
     //           s.SetSpawner(GameContext.Spawner);
        }
    }
}