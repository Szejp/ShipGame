using UnityEngine;

namespace UsefulStuff.Assets.Movement.PhysicsMovement.Scripts {
	public class PhysicsSplineJump : NavMeshMovement.Scripts.Movement {

		[SerializeField]
		private float _defaultForce = 100;
		[SerializeField]
		private float _maxDistance = 2f;
		private Rigidbody _rb;

		private void Awake() {
			_rb = GetComponent<Rigidbody>();
		}

		public void Move(Vector3 direction, Vector3 up, float force = 0) {
			if (!isGrounded) return;
			if (force == 0) force = _defaultForce;
			_rb.velocity = ((direction + up) * force) / _rb.mass;
		}

		private void Update() {
#if UNITY_EDITOR
			if (UnityEngine.Input.GetMouseButton(0)) {
				var direction = new Vector3(Mathf.Clamp(UnityEngine.Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition).origin.x - transform.position.x, -_maxDistance, _maxDistance), 0, 0);
				Move(direction, Vector3.up);
			}
#endif
		}
	}
}
