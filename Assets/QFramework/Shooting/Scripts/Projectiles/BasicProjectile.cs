using QFramework.GameModule.GameTools.Entities;
using UnityEngine;

namespace QFramework.Shooting.Scripts.Projectiles
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BasicProjectile : Entity
    {
        public float speed;

        protected Rigidbody2D rb;
        Vector3 initialSpeed;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void Fire(Vector3 initialSpeed)
        {
            rb.velocity = transform.up * speed + initialSpeed;
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.GetComponentInParent<Player>();
            if (player != null)
            {
                player.Kill();
                Destroy();    
            }
        }
    }
}