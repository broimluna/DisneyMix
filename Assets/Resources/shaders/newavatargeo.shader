Shader "NewAvatarGeo" {
Properties {
 AccessoryColor ("Accessory Color", Color) = (1.000000,1.000000,1.000000,1.000000)
 AccessoryXOffset ("Accessory X Offset", Float) = 0.000000
 AccessoryYOffset ("Accessory Y Offset", Float) = 0.000000
 AccessoryBlendMode00 ("Prop Blend Mode", Float) = 0.000000
 AccessoryBlendMode01 ("Prop Mask Mode", Float) = 0.000000
 AccessoryCrop ("Accessory crop", Float) = 1.000000
 AccessoryTex00 ("Prop", 2D) = "alpha" { }
 AccessoryTex01 ("Prop Diffuse Layer", 2D) = "alpha" { }
 AccessoryMask ("Prop Mask", 2D) = "alpha" { }
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