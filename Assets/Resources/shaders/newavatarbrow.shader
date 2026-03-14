Shader "NewAvatarBrow" {
Properties {
 BrowColor ("Brow Color", Color) = (1.000000,1.000000,1.000000,1.000000)
 BrowXOffset ("Eyebrows X Offset", Float) = 0.000000
 BrowYOffset ("Eyebrows Y Offset", Float) = 0.000000
 BrowBlendMode00 ("Brow Blend Mode", Float) = 0.000000
 BrowCrop ("Brow crop", Float) = 1.000000
 BrowTex00 ("Eyebrows", 2D) = "alpha" { }
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