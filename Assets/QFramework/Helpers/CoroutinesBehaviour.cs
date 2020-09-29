using System.Collections;

namespace QFramework.Helpers
{
    public class CoroutinesBehaviour : Singleton<CoroutinesBehaviour>
    {
        public static void StartNew(IEnumerator coroutine)
        {
            Instance.StartCoroutine(coroutine);
        }

        void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}