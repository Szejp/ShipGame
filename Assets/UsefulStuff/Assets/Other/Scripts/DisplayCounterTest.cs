using UnityEngine;
using UsefulStuff.Assets.Other.Scripts.WinaApi;

namespace UsefulStuff.Assets.Other.Scripts {
	public class Test : MonoBehaviour {

		public UnityEngine.UI.Text text;

		private void Start() {
			foreach (Display d in Display.displays) {
				d.Activate();
			}
		}

		private void Update() {

			if (UnityEngine.Input.GetKeyUp(KeyCode.Space)) {
				DisplayCounter.Count();
			}

			if (UnityEngine.Input.GetKeyUp(KeyCode.M)) {
				DisplayCounter.RefreshUnityDisplaysCount();
			}

			string s = "";
			foreach (var d in Display.displays) {
				s += d.systemWidth + "x" + d.systemHeight + "\n";
			}

			foreach (var ptr in DisplayCounter.windowPointers) {
				s += "ptr: " + ptr.ToString() + "\n";
			}

			s += Display.displays.Length.ToString(); ;
			text.text = s;
		}
	}
}
