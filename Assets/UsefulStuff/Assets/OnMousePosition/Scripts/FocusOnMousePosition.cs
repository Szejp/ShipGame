using UnityEngine;

namespace UsefulStuff.Assets.OnMousePosition.Scripts {
    public class FocusOnMousePosition : MonoBehaviour {

        private void Update() {
            Vector3 position = RectTransformUtility.ScreenPointToRay(UnityEngine.Camera.main, UnityEngine.Input.mousePosition).origin;
            position = new Vector3(position.x, position.y, 0);
            transform.rotation = Quaternion.LookRotation(position - transform.position);
        }
    }
}
