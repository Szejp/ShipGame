using Interfaces;
using ObjectManagement;
using QFramework.GameModule.GameTools.Entities;
using UnityEngine;
using Utils;

namespace Enviroment {
	[RequireComponent(typeof(Collider))]
	public class Clusterable : Entity, IClusterable {

		[SerializeField]
		private Join[] joins;
		private new Collider collider;

		public Bounds Bounds {
			get {
				return collider.bounds;
			}
		}

		public Transform Transform {
			get { return transform; }
		}

		public Join RandomJoin {
			get {
				return joins[Random.Range(0, joins.Length)];
			}
		}

		public void AdjustNeightbour(IClusterable obstacle) {
			var vector = Bounds.GetRandomPointOnBounds();
			obstacle.Transform.position = vector + Vector3.Scale((vector - Bounds.center), obstacle.Bounds.size);
			var vec2 = obstacle.Bounds.ClosestPoint(vector);
			obstacle.Transform.position = vector + (obstacle.Transform.position - vec2);
		}

		private void Awake() {
			collider = GetComponent<Collider>();
		}
	}
}
