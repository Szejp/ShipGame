using UnityEngine;

namespace QFramework.GameModule.GameTools.ObjectPool {
	public class CollectArea2D : MonoBehaviour {

		void OnTriggerEnter2D(Collider2D collider){
			var collectable = collider.GetComponent<IPoolCollectable>();
			collectable?.Collect();
		}
	}
}
