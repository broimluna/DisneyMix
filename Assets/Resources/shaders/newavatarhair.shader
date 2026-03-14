Shader "NewAvatarHair" {
Properties {
 HairColor ("Hair Color", Color) = (1.000000,1.000000,1.000000,1.000000)
 SkinColor ("Skin Color", Color) = (1.000000,1.000000,1.000000,1.000000)
 SkinXOffset ("Skin Augment Offset X", Float) = 0.000000
 SkinYOffset ("Skin Augment Offset Y", Float) = 0.000000
 NoseXOffset ("Nose Offset X", Float) = 0.000000
 NoseYOffset ("Nose Offset Y", Float) = 0.000000
 HairXOffset ("Hair X Offset", Float) = 0.000000
 HairYOffset ("Hair Y Offset", Float) = 0.000000
 SkinBlendMode00 ("Skin Augment Blend Mode", Float) = 0.000000
 CostumeBlendMode00 ("Costume Blend Mode", Float) = 5.000000
 NoseBlendMode00 ("Nose Blend Mode", Float) = 0.000000
 NoseBlendMode01 ("Nose Screen Blend Mode", Float) = 0.000000
 HairBlendMode00 ("Hair Blend Mode", Float) = 0.000000
 HairBlendMode01 ("Hair Highlight Blend Mode", Float) = 0.000000
 HairBlendMode02 ("Hair Shadow Blend Mode", Float) = 0.000000
 NoseCrop ("Nose crop", Float) = 1.000000
 SkinTex00 ("Skin Augment", 2D) = "alpha" { }
 CostumeTex00 ("Costume", 2D) = "alpha" { }
 NoseTex00 ("Nose", 2D) = "alpha" { }
 NoseTex01 ("Nose Screen", 2D) = "alpha" { }
 HairTex00 ("Hair", 2D) = "alpha" { }
 HairTex01 ("Hair Highlights", 2D) = "alpha" { }
 HairTex02 ("Hair Shadow", 2D) = "alpha" { }
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