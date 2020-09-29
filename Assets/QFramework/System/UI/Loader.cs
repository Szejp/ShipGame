using System.Collections;
using QFramework.System.Initialisation;
using UnityEngine;

namespace QFramework.System.UI
{
    public class Loader : MonoBehaviour
    {
        bool isLoaded;

        [SerializeField] float loaderTimeout = 5f;

        void Awake()
        {
            FirebaseInitializer.OnInitalised += OnInitHandler;

            StartCoroutine(LoaderCoroutine());
        }

        void OnDestroy()
        {
            FirebaseInitializer.OnInitalised -= OnInitHandler;
        }

        void OnInitHandler()
        {
            isLoaded = true;
        }

        IEnumerator LoaderCoroutine()
        {
            while (Time.realtimeSinceStartup < loaderTimeout && !isLoaded)
                yield return new WaitForSeconds(.1f);

            gameObject.SetActive(false);
        }
    }
}