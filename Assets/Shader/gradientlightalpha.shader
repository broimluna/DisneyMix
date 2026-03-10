Shader "Custom/gradientlightalpha" {
    Properties {
        _MainTex ("Base (RGB) Alpha (A)", 2D) = "white" { }
        _RampTex ("Lighting Ramp", 2D) = "white" { }
        _RimColor ("Rim Color", Color) = (1,1,1,1)
        _RimPower ("Rim Power", Float) = 2.0
    }
    SubShader {
        // Changed to Transparent queue
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200
        
        CGPROGRAM
        // Added alpha:fade to the pragma
        #pragma surface surf Ramp alpha:fade

        sampler2D _MainTex;
        sampler2D _RampTex;
        float4 _RimColor;
        float _RimPower;

        struct Input {
            float2 uv_MainTex;
            float3 viewDir;
        };

        fixed4 LightingRamp (SurfaceOutput s, fixed3 lightDir, fixed atten) {
            half d = dot (s.Normal, lightDir) * 0.5 + 0.5;
            fixed3 ramp = tex2D (_RampTex, float2(d, d)).rgb;
            fixed4 c;
            c.rgb = s.Albedo * _LightColor0.rgb * ramp * atten;
            c.a = s.Alpha;
            return c;
        }

        void surf (Input IN, inout SurfaceOutput o) {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Alpha = c.a; // Assign alpha from texture
            
            half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
            o.Emission = _RimColor.rgb * pow (rim, _RimPower);
        }
        ENDCG
    }
    Fallback "Transparent/VertexLit"
}