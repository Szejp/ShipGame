using System.Collections;
using System.Collections.Generic;
using QFramework.Helpers.Spawning;
using UnityEngine;
using UnityEngine.Assertions;

namespace ObjectPoolTest {
	public class ObjectPoolTest : MonoBehaviour {

		[SerializeField]
		private GameObject[] objects;
		[SerializeField]
		private int iterations = 100;
		private Spawner spawner;

		private Queue<Transform> spawned = new Queue<Transform>();

		private void Awake() {
			spawner = new Spawner();
		}

		private void Start() {
			StartCoroutine(TestSpawnCoroutine());
		}

		private IEnumerator TestSpawnCoroutine() {
			for (int j = 0; j < objects.Length; j++) {
				for (int i = 0; i < iterations; i++) {
					spawned.Enqueue((Transform)spawner.Spawn(objects[j].transform, Vector3.zero));
				}

				for (int i = 0; i < iterations; i++) {
					spawner.Collect(spawned.Dequeue());
				}

				for (int i = 0; i < iterations; i++) {
					spawned.Enqueue((Transform)spawner.Spawn(objects[j].transform, Vector3.zero));
				}

				Assert.IsTrue(spawned.Count.Equals(iterations));

				for (int i = 0; i < iterations; i++) {
					spawner.Collect(spawned.Dequeue());
				}

				yield return new WaitForSeconds(1);

				spawned.Clear();
			}
		}
	}
}
