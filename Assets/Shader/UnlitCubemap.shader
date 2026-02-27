Shader "Custom/UnlitCubemap" {
Properties {
 _MainTex ("Texture", 2D) = "white" { }
 _Cube ("Cubemap", CUBE) = "" { }
 _Intensity ("Intensity", Range(0.000000,1.000000)) = 1.000000
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