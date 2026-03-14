Shader "Custom/Drop/GroundShader" {
Properties {
 _MainTex ("Main", 2D) = "white" { }
 _SnowTex ("Snow", 2D) = "white" { }
 _RippleMaskTex ("Snow Ripple", 2D) = "white" { }
 _MaskTex ("Mask", 2D) = "black" { }
 _Min ("Min", Range(0.000000,1.000000)) = 0.000000
 _Mid ("Mid", Range(0.000000,1.000000)) = 0.500000
 _Max ("Max", Range(0.000000,1.000000)) = 1.000000
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