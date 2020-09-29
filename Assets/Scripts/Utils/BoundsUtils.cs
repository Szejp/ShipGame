using UnityEngine;

namespace Utils {
	public static class BoundsUtils {
		public static Vector3 GetRandomPointOnBounds(this Bounds bounds) {
			var randomVector = VectorUtils.GetRandomVector(bounds.min, bounds.max);
			return bounds.ClosestPoint(randomVector);
		}
	}
}
