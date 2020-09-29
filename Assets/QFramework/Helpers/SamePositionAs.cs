using UnityEngine;

namespace QFramework.Helpers
{
    public class SamePositionAs : MonoBehaviour
    {
        [SerializeField] Transform target;

        void Update()
        {
            transform.position = target.position;
        }
    }
}