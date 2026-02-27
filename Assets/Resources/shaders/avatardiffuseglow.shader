Shader "AvatarDiffuseGlow" {
Properties {
 CostumeBlendMode00 ("Costume Blend Mode", Float) = 0.000000
 GlowBlendMode00 ("Glow Blend Mode", Float) = 0.000000
 AvatarDiffuseBase ("Avatar Diffuse Base", 2D) = "white" { }
 CostumeTex00 ("Costume", 2D) = "alpha" { }
 GlowTex00 ("Glow", 2D) = "alpha" { }
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