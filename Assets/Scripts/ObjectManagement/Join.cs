using Enviroment;
using Interfaces;
using UnityEngine;

namespace ObjectManagement {
	public class Join : MonoBehaviour, IJoin {

		[SerializeField]
		private Clusterable[] possibleNeightbours;

		public Vector3[] GetJoinPositions() {
			return new Vector3[] { transform.position };
		}

		public Clusterable[] PossibleNeightbours {
			get {
				return possibleNeightbours;
			}
		}

		public Clusterable RandomNeightbour {
			get {
				return possibleNeightbours[Random.Range(0, possibleNeightbours.Length)];
			}
		}
	}
}
