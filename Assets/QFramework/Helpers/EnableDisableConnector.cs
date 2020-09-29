using UnityEngine;

namespace QFramework.Helpers
{
    public class EnableDisableConnector : MonoBehaviour
    {
        [SerializeField] GameObject gameObjectToFollow;

        void OnEnable()
        {
            gameObjectToFollow.SetActive(true);
        }

        void OnDisable()
        {
            gameObjectToFollow.SetActive(false);
        }
    }
}