using System;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.System
{
    public class ThreadDispatcher : MonoBehaviour
    {
        static readonly Queue<Action> actionsQueue = new Queue<Action>();

        [RuntimeInitializeOnLoadMethod]
        public static void Init()
        {
            var go = new GameObject("ThreadDispatcher").AddComponent<ThreadDispatcher>();
            DontDestroyOnLoad(go);
        }

        public static void Dispatch(Action action)
        {
            actionsQueue.Enqueue(action);
        }

        void Update()
        {
            if (actionsQueue.Count > 0)
                actionsQueue.Dequeue()();
        }
    }
}