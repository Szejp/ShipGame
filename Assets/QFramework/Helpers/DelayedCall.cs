using System;
using System.Collections;
using UnityEngine;

namespace QFramework.Helpers
{
    public class DelayedCall : Singleton<DelayedCall>
    {
        public static void Call(Action callback, float delay)
        {
            Instance.StartCoroutine(RunDelayed(callback, delay));
        }

        static IEnumerator RunDelayed(Action callback, float delay)
        {
            yield return new WaitForSeconds(delay);
            callback?.Invoke();
        }
    }
}