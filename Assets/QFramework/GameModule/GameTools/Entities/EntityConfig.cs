using QFramework.GameModule.GameTools.Effects;
using UnityEngine;

namespace QFramework.GameModule.GameTools.Entities {
	[CreateAssetMenu(fileName = "EntityConfig.asset", menuName = "Game/Config/Agents/EntityConfig")]
	public class EntityConfig : ScriptableObject {
		public float destroyTime;
		public Effect destroyEffect;
	}
}
