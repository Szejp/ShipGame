using System;
using UnityEngine;

namespace QFramework.Helpers.Physics
{
    [RequireComponent(typeof(Collider2D))]
    public class OnTrigger2D : MonoBehaviour
    {
        public event Action<OnTrigger2D, Collider2D> onTriggerEnter2D;
        public event Action<OnTrigger2D, Collider2D> onTriggerExit2D;
        
        void OnTriggerEnter2D(Collider2D other)
        {
            onTriggerEnter2D?.Invoke(this, other);
        }

        void OnTriggerExit2D(Collider2D other)
        {
            onTriggerExit2D?.Invoke(this, other);
        }
    }
}