using UnityEngine;
using UnityExtensions = ExternalTools.Scripts.UnityExtensions;

namespace QFramework.Helpers
{
    public class OnEnable2DRotator : MonoBehaviour
    {
        [SerializeField] int[] rotations;

        void OnEnable()
        {
            transform.rotation = Quaternion.Euler(Vector3.forward * UnityExtensions.RandomOne(rotations));
        }
    }
}