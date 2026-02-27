Shader "FX/Water" {
Properties {
 _WaveScale ("Wave scale", Range(0.020000,0.150000)) = 0.063000
 _ReflDistort ("Reflection distort", Range(0.000000,1.500000)) = 0.440000
 _RefrDistort ("Refraction distort", Range(0.000000,1.500000)) = 0.400000
 _RefrColor ("Refraction color", Color) = (0.340000,0.850000,0.920000,1.000000)
[NoScaleOffset]  _Fresnel ("Fresnel (A) ", 2D) = "gray" { }
[NoScaleOffset]  _BumpMap ("Normalmap ", 2D) = "bump" { }
 WaveSpeed ("Wave speed (map1 x,y; map2 x,y)", Vector) = (19.000000,9.000000,-16.000000,-7.000000)
[NoScaleOffset]  _ReflectiveColor ("Reflective color (RGB) fresnel (A) ", 2D) = "" { }
 _HorizonColor ("Simple water horizon color", Color) = (0.172000,0.463000,0.435000,1.000000)
[HideInInspector]  _ReflectionTex ("Internal Reflection", 2D) = "" { }
[HideInInspector]  _RefractionTex ("Internal Refraction", 2D) = "" { }
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