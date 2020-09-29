using QFramework.GameModule.GameTools.ObjectPool;
using QFramework.Helpers.Interfaces;
using QFramework.Helpers.VelocityRelated;
using QFramework.Shooting.Scripts.Projectiles;
using UnityEngine;

namespace QFramework.Shooting.Scripts {
    [RequireComponent(typeof(VelocityMeasurer))]
    public class ShootingSystem : MonoBehaviour
    {
        [SerializeField] bool isAutomaticMode;
        [SerializeField] Transform projectile;
        [SerializeField] ShootingData shootingData;
        [SerializeField] float angleRangeToShoot = 45f;

        float lastFiredTime;
        ISpawner spawner;
        ITargetProvider targetProvider;
        Vector3 velocity;
        VelocityMeasurer velocityMeasurer;

        public bool IsAutomaticMode
        {
            get => isAutomaticMode;
            set => isAutomaticMode = value;
        }

        public bool CanShoot => IsAutomaticMode && targetProvider != null && targetProvider.GetTarget() != null &&
                                AngleToTarget < angleRangeToShoot;

        float AngleToTarget =>
            Vector3.Angle(targetProvider.GetTarget().transform.position - transform.position, transform.up);

        public void SetProjectile(Transform projectile)
        {
            this.projectile = projectile;
        }

        public void SetShootingData(ShootingData shootingData)
        {
            this.shootingData = shootingData;
        }

        public void SetSpawner(ISpawner spawner)
        {
            this.spawner = spawner;
        }

        public void Fire()
        {
            for (int i = 0; i < shootingData.burstCount; i++)
            {
                var randomAngle = -shootingData.shootingAngleSpread / 2 + i * shootingData.shootingAngleSpread / shootingData.burstCount;
                FireProjectile(randomAngle);
            }

            lastFiredTime = Time.realtimeSinceStartup;
        }
        
        void FireProjectile(float angle)
        {
            var projectileRotation =  Quaternion.AngleAxis(angle, Vector3.forward) * transform.rotation;
            var projectileObject = spawner.Spawn(projectile, transform.position, projectileRotation)
                .GetComponent<BasicProjectile>();
            projectileObject.Fire(velocity + velocityMeasurer.Velocity);
            projectileObject.SetCollector(spawner);

            var setTarget = projectileObject.GetComponent<ISetTarget>();
            setTarget?.SetTarget(targetProvider?.GetTarget());
        }

        void Awake()
        {
            targetProvider = GetComponent<ITargetProvider>();
            velocityMeasurer = GetComponent<VelocityMeasurer>();
        }

        void OnEnable()
        {
            lastFiredTime = Time.realtimeSinceStartup;
        }

        void Update()
        {
            if (CanShoot && Time.realtimeSinceStartup - lastFiredTime > shootingData.fireRate)
            {
                Fire();
            }
        }
    }

    public class MeeleCombatSystem : MonoBehaviour
    {
        public float attackRate;
        public float minAttackRange;
    
        ITargetProvider targetProvider;

        public void SetTargetProvider(ITargetProvider targetProvider)
        {
            this.targetProvider = targetProvider;
        }

// ... dalsza część kodu ...

        void Attack(IDamagable damagable, float damage)
        {
            damagable.SetDamage(damage);
        }
    }
}