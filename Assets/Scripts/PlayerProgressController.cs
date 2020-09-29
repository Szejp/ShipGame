using System.Collections;
using Controllers;
using Interfaces;
using UnityEngine;

public class PlayerProgressController : MonoBehaviour {

	[SerializeField]
	Vector3 progressVector;
	[SerializeField]
	float updatePeriod = 1;
	[SerializeField]
	float progressFactor = 1;
	float longestMagnitude;
	float magnitudeRest = 0;
	IScoreController scoreController;

	private void Awake() {
		scoreController = GameMaster.Instance.ScoreController;
	}

	IEnumerator Start() {
		while (isActiveAndEnabled) {
			Vector3 project = Vector3.Project(transform.position - Vector3.zero, progressVector);
			if (project.normalized - progressVector == Vector3.zero) {
				float newMagniture = project.magnitude;
				if (newMagniture > longestMagnitude) {
					float delta = newMagniture - longestMagnitude + magnitudeRest;
					int iterations = (int)((delta) / progressFactor);
					for (int i = 0; i < iterations; i++) {
						UpdatePlayerProgress();
					}

					magnitudeRest = delta % progressFactor;
					longestMagnitude = newMagniture;
				}
			}
			yield return new WaitForSeconds(updatePeriod);
		}
	}

	void UpdatePlayerProgress() {
		scoreController.AddScore(1);
	}
}
