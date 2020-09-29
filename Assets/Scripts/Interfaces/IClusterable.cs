using UnityEngine;

namespace Interfaces {
	public interface IClusterable {
		Bounds Bounds { get; }
		Transform Transform { get; }
		void AdjustNeightbour(IClusterable obstacle);
	}
}
