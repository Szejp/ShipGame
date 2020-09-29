using QFramework.Helpers.Interfaces;
using UnityEngine;

namespace QFramework.Shooting.Scripts
{
    public class BasicTargetProvider : MonoBehaviour, ITargetProvider
    {
        [SerializeField] Transform target;

        public Transform GetTarget()
        {
            return target;
        }
    }
}