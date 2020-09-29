using UnityEngine;

namespace QFramework.Shooting.Scripts.Projectiles
{
    public class HoomingProjectile : BasicProjectile, ISetTarget
    {
        public float lerpFactor;
        public float deathTime = 3f;

        Transform target;

        float timeElapsed;

        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        void Update()
        {
            if (target != null)
                rb.velocity = Vector3.Lerp(rb.velocity,
                    (target.position - transform.position).normalized * speed, lerpFactor * Time.deltaTime);

            if (target == null)
                timeElapsed += Time.deltaTime;

            if (timeElapsed > deathTime)
            {
                timeElapsed = 0;
                Destroy();
            }
        }
    }
}