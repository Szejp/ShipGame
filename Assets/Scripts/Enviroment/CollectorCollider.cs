using UnityEngine;

namespace Enviroment {
	public class CollectorCollider : MonoBehaviour {

		public event System.Action<Collider> OnObstacleHit = p => { };

		[SerializeField]
		private string obstacleTag = "Obstacle";

		private void OnTriggerEnter(Collider other) {
			if (other.CompareTag(obstacleTag))
				OnObstacleHit.Invoke(other);
		}
	}
}
