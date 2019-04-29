// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/BuildingColorShader" {
	Properties{
		_BaseTex("Base Tex(RGB)", 2D) = "white" {}
		_Color("Color", Color) = (0.2, 0.2, 1, 1)
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM

#pragma surface surf StandardSpecular vertex:vert //fullforwardshadows
#pragma target 3.0

	struct Input {
		half2 uv_MainTex;
		half3 worldPos;
		INTERNAL_DATA
	};

	sampler2D   _BaseTex;
	half4 _Color;

	void vert(inout appdata_full v, out Input o) {
		UNITY_SETUP_INSTANCE_ID(v);
		UNITY_INITIALIZE_OUTPUT(Input,o);
	}

	void surf(Input IN, inout SurfaceOutputStandardSpecular o) {

		fixed3 main = tex2D(_BaseTex, IN.worldPos.xz * 1.2);

		fixed lo = 0.0;
		fixed hi = 1.0;

		// rescale the range 0.2 .. 0.5 to 0.0 .. 1.0
		fixed desaturation = saturate((main.g - lo) / (hi - lo));

		// change 0.0 .. 0.5 .. 1.0 to 1.0 .. 0.0 .. 1.0
		desaturation = abs((desaturation * 2.0) - 1.0);

		// lerp between grey and colorized grey
		fixed3 col = lerp(main * _Color.rgb, fixed3(main.g, main.g, main.g), desaturation);
		o.Albedo = col;

		o.Specular = 0.001; //lowest possible
		o.Smoothness = 0;

	}
	ENDCG
	}
		FallBack "Diffuse"
}