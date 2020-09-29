using System.Collections;
using System.Collections.Generic;
using Interfaces;
using ObjectManagement;
using QFramework.GameModule.GameTools.Entities;
using QFramework.GameModule.GameTools.ObjectPool;
using QFramework.Helpers.Spawning;
using UnityEngine;

namespace Enviroment {
	public class ObstaclesGenerator : MonoBehaviour {

		[SerializeField]
		private List<Clusterable> obstacles;
		[SerializeField]
		private Vector3 volumeVector = Vector3.one;
		[SerializeField]
		private float checkboxVolume = 2;
		[SerializeField]
		private float interval = 1;
		[SerializeField]
		private int maxNest = 3;
		[SerializeField]
		private CollectorCollider collectorCollider;
		private ISpawner spawner;
		private int joinNestingFactor = 0;

		[SerializeField]
		private Vector2[] sideObstaclePositions;

		public void GenerateObstacle(Clusterable obstacle) {
			var sidePosition = sideObstaclePositions[Random.Range(0, sideObstaclePositions.Length)];
			Vector3 pos = new Vector3(sidePosition.x, sidePosition.y, transform.position.z);
			var result = spawner.Spawn(obstacle, pos) as Clusterable;
			joinNestingFactor = 0;

			GenerateRandomJoin(result.RandomJoin);
		}

		public void GenerateRandomJoin(Join join) {
			if (join != null && joinNestingFactor <= maxNest) {
				var pos = join.transform.position;
				spawner.Spawn(join.RandomNeightbour, pos);
				joinNestingFactor++;
			}
		}

		private void Awake() {
			spawner = new Spawner();
			collectorCollider.OnObstacleHit += HideObstacle;
		}

		private void OnDestroy() {
			collectorCollider.OnObstacleHit -= HideObstacle;
		}

		private void Start() {
			StartCoroutine(GenerateCoroutine());
		}

		private IEnumerator GenerateCoroutine() {
			while (true) {
				GenerateObstacle(obstacles[Random.Range(0, obstacles.Count)]);
				yield return new WaitForSeconds(interval);
			}
		}

		private void HideObstacle(Collider obstacleCollider) {
			spawner.Collect(obstacleCollider.GetComponent<Entity>());
		}
	}
}
