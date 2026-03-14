Shader "NewAvatarEyes" {
Properties {
 EyesColor ("Eye Color", Color) = (1.000000,1.000000,1.000000,1.000000)
 EyesXOffset ("Eye Offset X", Float) = 0.000000
 EyesYOffset ("Eye Offset Y", Float) = 0.000000
 EyesBlendMode00 ("Eyes Blend Mode", Float) = 0.000000
 EyesBlendMode02 ("Iris Blend Mode", Float) = 0.000000
 EyesCrop ("Eyes crop", Float) = 1.000000
 EyesTex00 ("Eyes", 2D) = "alpha" { }
 EyesMask ("Eye Mask", 2D) = "alpha" { }
 _MainTex ("Main Texture", 2D) = "alpha" { }
}
	//DummyShaderTextExporter
	
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Lambert
#pragma target 3.0
		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};
		void surf(Input IN, inout SurfaceOutput o)
		{
			float4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
		}
		ENDCG
	}
}