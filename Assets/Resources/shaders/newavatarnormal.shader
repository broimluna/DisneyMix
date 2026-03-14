Shader "NewAvatarNormal" {
Properties {
 NmlColor ("Base Normal Color", Color) = (0.500000,0.500000,1.000000,1.000000)
 AccessoryXOffset ("Accessory Normal Offset X", Float) = 0.000000
 AccessoryYOffset ("Accessory Normal Offset Y", Float) = 0.000000
 HairXOffset ("Hair Normal Offset X", Float) = 0.000000
 HairYOffset ("Hair Normal Offset Y", Float) = 0.000000
 AccessoryBlendMode00 ("Accessory Blend Mode", Float) = 4.000000
 HairBlendMode00 ("Hair Blend Mode", Float) = 4.000000
 AccessoryCrop ("Accessory Crop", Float) = 1.000000
 AccessoryTex00 ("Accessory", 2D) = "white" { }
 HairTex00 ("Hair", 2D) = "white" { }
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