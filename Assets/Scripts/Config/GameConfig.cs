using UnityEngine;

namespace Config {
	[CreateAssetMenu(fileName = "GameConfig", menuName = "Config/GameConfig")]
	public class GameConfig : ScriptableObject {
		public int scoreFactor;
	}
}
