Shader "Custom/gradientlight" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" { }
        _RampTex ("Lighting Ramp", 2D) = "white" { }
        _RimColor ("Rim Color", Color) = (1,1,1,1)
        _RimPower ("Rim Power", Float) = 2.0
        _LightDir ("Lighting Direction", Vector) = (0,0,-1,0)
    }
    SubShader {
        Tags { "RenderType" = "Opaque" }
        LOD 200
        
        CGPROGRAM
        #pragma surface surf Ramp

        sampler2D _MainTex;
        sampler2D _RampTex;
        float4 _RimColor;
        float _RimPower;
        float4 _LightDir; // Re-added from source 

        struct Input {
            float2 uv_MainTex;
            float3 viewDir;
        };

        fixed4 LightingRamp (SurfaceOutput s, fixed3 lightDir, fixed atten) {
            // Combine Unity's light with your custom _LightDir property
            float3 combinedLightDir = normalize(lightDir + _LightDir.xyz);
            half d = dot (s.Normal, combinedLightDir) * 0.5 + 0.5;
            
            // Sample the ramp
            fixed3 ramp = tex2D (_RampTex, float2(d, 0.5)).rgb;
            
            fixed4 c;
            c.rgb = s.Albedo * _LightColor0.rgb * ramp * atten;
            c.a = s.Alpha;
            return c;
        }

        void surf (Input IN, inout SurfaceOutput o) {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            
            half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
            o.Emission = _RimColor.rgb * pow (rim, _RimPower);
        }
        ENDCG
    }
    Fallback "Diffuse"
}