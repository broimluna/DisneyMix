Shader "AvatarDiffuseFeatures" {
Properties {
 HairColor ("Hair Color", Color) = (1.000000,1.000000,1.000000,1.000000)
 AccessoryColor ("Accessory Color", Color) = (1.000000,1.000000,1.000000,1.000000)
 BrowXOffset ("Eyebrows X Offset", Float) = 0.000000
 BrowYOffset ("Eyebrows Y Offset", Float) = 0.000000
 AccessoryXOffset ("Accessories X Offset", Float) = 0.000000
 AccessoryYOffset ("Accessories Y Offset", Float) = 0.000000
 HairXOffset ("Hair X Offset", Float) = 0.000000
 HairYOffset ("Hair Y Offset", Float) = 0.000000
 BrowBlendMode00 ("Brow Blend Mode", Float) = 0.000000
 AccessoryBlendMode00 ("Accessory Blend Mode", Float) = 0.000000
 AccessoryBlendMode01 ("Accessory Blend Mode", Float) = 0.000000
 HairBlendMode00 ("Hair Blend Mode", Float) = 0.000000
 HairBlendMode01 ("Hair Highlight Blend Mode", Float) = 0.000000
 HairBlendMode02 ("Hair Shadow Blend Mode", Float) = 0.000000
 AvatarDiffuseBase ("Avatar Diffuse Base", 2D) = "white" { }
 BrowTex00 ("Eyebrows", 2D) = "alpha" { }
 AccessoryTex00 ("Accessory", 2D) = "alpha" { }
 AccessoryMask ("Accessory Mask", 2D) = "alpha" { }
 HairTex00 ("Hair", 2D) = "alpha" { }
 HairTex01 ("Hair Highlights", 2D) = "alpha" { }
 HairTex02 ("Hair Shadow", 2D) = "alpha" { }
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