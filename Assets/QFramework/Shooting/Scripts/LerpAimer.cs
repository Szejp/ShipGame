using QFramework.Helpers.Interfaces;
using UnityEngine;

namespace QFramework.Shooting.Scripts
{
    public class LerpAimer : MonoBehaviour
    {
        [SerializeField] float velocityModifer;
        [Range(0, 10)] [SerializeField] float lerpFactor = 1;
        [SerializeField] Transform target;
        
        Vector3 targetVelocity;
        Vector3 previousTargetPosition;
        Vector3 positionToShootAt;
        [SerializeField]
        ITargetProvider targetProvider;

        public Transform Target
        {
            get
            {
                if (targetProvider != null)
                    target = targetProvider.GetTarget();

                return target;
            }
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        void Awake()
        {
            targetProvider = GetComponent<ITargetProvider>();
        }

        void Update()
        {
            if (Target != null)
            {
                targetVelocity = target.position - previousTargetPosition;
                previousTargetPosition = target.position;
                positionToShootAt = target.position + targetVelocity * velocityModifer;

                transform.up = Vector3.Lerp(transform.up, positionToShootAt - transform.position,
                    lerpFactor * Time.deltaTime);
            }
        }
    }
}