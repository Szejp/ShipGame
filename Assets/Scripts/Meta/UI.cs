using UnityEngine;
using UnityEngine.UI;

namespace Meta {
	public class UI : MonoBehaviour {

		[SerializeField]
		Text scoreText;

		public void SetScoreText(string text) {
			scoreText.text = text;
		}
	}
}
