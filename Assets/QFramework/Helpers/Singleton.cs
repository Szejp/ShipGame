using System;
using UnityEngine;

namespace QFramework.Helpers {
	public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
		protected static T _instance;
		private static GameObject go;

		public static T Instance {
			get {
				if (_instance == null) {
					_instance = (T)FindObjectOfType(typeof(T));

					if (_instance == null) {
						go = Instantiate(new GameObject(String.Format("singleton - {0}", typeof(T).GetType().ToString())));
						_instance = go.AddComponent<T>();
						DontDestroyOnLoad(go);
					}
				}
				return _instance;
			}
		}

		protected virtual void Awake() {
			if (_instance != null && _instance != this) {
				DestroyImmediate(this);
				throw new Exception("Singeton is singleton");
			}
		}
	}
}
