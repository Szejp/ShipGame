using UnityEngine;

namespace QFramework.Helpers {
	public class SimpleFollow : MonoBehaviour {

		public Transform target;

		public bool blockX;

		private Vector3 cachedPos;

		// Use this for initialization
		void OnEnable () {
			cachedPos = target.position;
		}
	
		// Update is called once per frame
		void Update () {
			if(!blockX){
				transform.position += (target.position - cachedPos).x * Vector3.right;
			}

			transform.position += (target.position - cachedPos).y * Vector3.up;
			cachedPos = target.position;	
		}
	}
}
