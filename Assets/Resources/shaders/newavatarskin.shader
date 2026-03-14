Shader "NewAvatarSkin" {
Properties {
 SkinColor ("Skin Color", Color) = (1.000000,1.000000,1.000000,1.000000)
 SkinXOffset ("Skin Augment Offset X", Float) = 0.000000
 SkinYOffset ("Skin Augment Offset Y", Float) = 0.000000
 SkinBlendMode00 ("Skin Augment Blend Mode", Float) = 0.000000
 CostumeBlendMode00 ("Costume Blend Mode", Float) = 5.000000
 SkinTex00 ("Skin Augment", 2D) = "alpha" { }
 CostumeTex00 ("Costume", 2D) = "alpha" { }
 _MainTex ("Main Texture", 2D) = "white" { }
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