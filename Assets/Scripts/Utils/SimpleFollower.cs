using UnityEngine;

namespace Utils {
	public class SimpleFollower : MonoBehaviour {

		[SerializeField]
		private Transform target;

		[SerializeField]
		private bool blockX = false;
		[SerializeField]
		private bool blockY = false;
		[SerializeField]
		private bool blockZ = false;

		private Vector3 targetPreviousPosition;
		private Vector3 positionChanged;
		private Vector3 delta;

		// Use this for initialization
		void Start() {
			if (target != null)
				targetPreviousPosition = target.transform.position;
		}

		// Update is called once per frame
		void Update() {
			positionChanged = target.transform.position - targetPreviousPosition;
			targetPreviousPosition = target.transform.position;
			delta = Vector3.zero;

			if (!blockX)
				delta += Vector3.right * positionChanged.x;

			if (!blockY)
				delta += Vector3.up * positionChanged.y;

			if (!blockZ)
				delta += Vector3.forward * positionChanged.z;

			transform.position += delta;
		}
	}
}
