Shader "AvatarNormals" {
Properties {
 NmlColor ("Base Normal Color", Color) = (0.500000,0.500000,1.000000,1.000000)
 NoseXOffset ("Nose Normal Offset X", Float) = 0.000000
 NoseYOffset ("Nose Normal Offset Y", Float) = 0.000000
 EyesXOffset ("Eyes Normal Offset X", Float) = 0.000000
 EyesYOffset ("Eyes Normal Offset Y", Float) = 0.000000
 BrowXOffset ("Brow Normal Offset X", Float) = 0.000000
 BrowYOffset ("Brow Normal Offset Y", Float) = 0.000000
 AccessoryXOffset ("Accessory Normal Offset X", Float) = 0.000000
 AccessoryYOffset ("Accessory Normal Offset Y", Float) = 0.000000
 HairXOffset ("Hair Normal Offset X", Float) = 0.000000
 HairYOffset ("Hair Normal Offset Y", Float) = 0.000000
 NoseBlendMode00 ("Nose Blend Mode", Float) = 4.000000
 EyesBlendMode00 ("Eyes Blend Mode", Float) = 4.000000
 BrowBlendMode00 ("Brow Blend Mode", Float) = 4.000000
 AccessoryBlendMode00 ("Accessory Blend Mode", Float) = 4.000000
 HairBlendMode00 ("Hair Blend Mode", Float) = 4.000000
 CostumeBlendMode00 ("Costume Blend Mode", Float) = 4.000000
 NoseTex00 ("Nose", 2D) = "white" { }
 EyesTex00 ("Eyes", 2D) = "white" { }
 BrowTex00 ("Eyebrows", 2D) = "white" { }
 AccessoryTex00 ("Accessory", 2D) = "white" { }
 HairTex00 ("Hair", 2D) = "white" { }
 CostumeTex00 ("Costume", 2D) = "white" { }
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