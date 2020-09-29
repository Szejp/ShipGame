using System;
using UnityEngine;

namespace QFramework.Helpers
{
    public class LazyComponent<T> where T : Component
    {
        T cache;
        Func<T> getter;
        
        public T Result
        {
            get
            {
                if (cache == null)
                    cache = getter();

                return cache;
            }
        }
        
        public LazyComponent(Func<T> getter)
        {
            this.getter = getter;
        }
    }
}