using Interfaces;
using UnityEngine;
using UsefulStuff.Assets.Input;

namespace Controllers {
	public class MovementController : IMovementController {

		private ITurnable turnable;

		public MovementController(ITurnable turnable) {
			this.turnable = turnable;
			TouchInput.OnDragEvent += OnDragEventHandler;
		}

		~MovementController() {
			TouchInput.OnDragEvent -= OnDragEventHandler;
		}

		private void OnDragEventHandler(Vector2 dragData) {
			turnable.Turn(new Vector2(-dragData.x, dragData.y));
		}
	}
}
