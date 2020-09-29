using UnityEngine;
using UnityEngine.EventSystems;

namespace UsefulStuff.Assets.Input {
	public class TouchInput : MonoBehaviour, IDragHandler {

		public static System.Action<Vector2> OnDragEvent = v => { };

		public void OnDrag(PointerEventData eventData) {
			OnDragEvent.Invoke(eventData.delta);
		}
	}
}