using UnityEngine;
using UsefulStuff.Assets.Movement.SimplePlane3D.Scripts;

public class Player : MonoBehaviour {

	[SerializeField]
	private SimplePlane3D planeCache;

	public SimplePlane3D plane {
		get {
			return planeCache;
		}
	}

	public void Kill()
	{
		
	}
}
