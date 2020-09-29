using UnityEngine;

namespace Utils {
	public static class VectorUtils {

		public static Vector3 GetRandomVector(float min, float max) {
			return new Vector3(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
		}

		public static Vector3 GetRandomVector(float min, float max, float factor) {
			return new Vector3(Random.Range(min, max) * factor, Random.Range(min, max) * factor, Random.Range(min, max) * factor);
		}

		public static Vector3 GetRandomVector(Vector3 min, Vector3 max) {
			return new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
		}

		public static Vector3 GetProjection(Vector3 vector, Vector3 origin) {
			return vector.sqrMagnitude * Vector3.Angle(vector, origin) * origin.normalized;
		}
	}
}
