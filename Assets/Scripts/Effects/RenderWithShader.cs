using UnityEngine;

namespace Effects {
	[ExecuteInEditMode]
	public class RenderWithShader : MonoBehaviour {

		[SerializeField]
		Material mat;

		[SerializeField]
		Shader replacementShader;

		private void Start() {
			var camera = GetComponent<Camera>();
			camera.depthTextureMode = DepthTextureMode.Depth;
			//camera.SetReplacementShader(replacementShader, "");

			//camera.CopyFrom(Camera.main);

			//var target = new RenderTexture(Screen.width, Screen.height, 16, RenderTextureFormat.Depth);
			//camera.targetTexture = target;
			//camera.depthTextureMode = DepthTextureMode.None;

			//camera.SetReplacementShader(replacementShader, "RenderType");
			//Shader.SetGlobalTexture("_GBuffer", target);

		}

		void OnRenderImage(RenderTexture source, RenderTexture destination) {
			Graphics.Blit(source, destination, mat);
		}
	}
}
