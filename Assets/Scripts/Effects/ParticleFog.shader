// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/ParticleFog"
{
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
	_Fader("Fader Slider", Float) = 0
		_MinOpacity("Minimum Opacity", Range(0,1)) = 0.1
	}
		SubShader{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 200
		ZWrite Off
		ZTest Less
		Cull Back

		CGPROGRAM
#pragma surface surf Lambert alpha


		sampler2D _MainTex;
	float _Fader;
	float _MinOpacity;

	struct Input {
		float2 uv_MainTex;
		float dist;
	};

	void vert(inout appdata_full v, out Input o) {
		o.dist = distance(_WorldSpaceCameraPos, mul(unity_ObjectToWorld, v.vertex));
	}

	void surf(Input IN, inout SurfaceOutput o) {
		o.Albedo = IN.dist;
	}
	ENDCG
	}
		FallBack "Diffuse"
}