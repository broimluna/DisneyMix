Shader "NewAvatarMouth" {
Properties {
 MouthColor ("Mouth Color", Color) = (1.000000,1.000000,1.000000,1.000000)
 MouthXOffset ("Mouth Offset X", Float) = 0.000000
 MouthYOffset ("Mouth Offset Y", Float) = 0.000000
 MouthBlendMode00 ("Mouth Blend Mode", Float) = 0.000000
 MouthCrop ("Mouth crop", Float) = 1.000000
 MouthTex00 ("Mouth", 2D) = "alpha" { }
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