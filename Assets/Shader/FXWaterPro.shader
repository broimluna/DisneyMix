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
SubShader { 
 Tags { "RenderType"="Opaque" "WaterMode"="Refractive" }
 Pass {
  Tags { "RenderType"="Opaque" "WaterMode"="Refractive" }
  GpuProgramID 3231
Program "vp" {
SubProgram "gles hw_tier01 " {
Keywords { "WATER_REFRACTIVE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _ProjectionParams;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp vec4 _WaveScale4;
					uniform highp vec4 _WaveOffset;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec2 xlv_TEXCOORD2;
					varying highp vec3 xlv_TEXCOORD3;
					void main ()
					{
					  highp vec4 temp_1;
					  highp vec4 tmpvar_2;
					  tmpvar_2 = (glstate_matrix_mvp * _glesVertex);
					  highp vec4 tmpvar_3;
					  tmpvar_3 = (unity_ObjectToWorld * _glesVertex);
					  temp_1 = ((tmpvar_3.xzxz * _WaveScale4) + _WaveOffset);
					  highp vec4 o_4;
					  highp vec4 tmpvar_5;
					  tmpvar_5 = (tmpvar_2 * 0.5);
					  highp vec2 tmpvar_6;
					  tmpvar_6.x = tmpvar_5.x;
					  tmpvar_6.y = (tmpvar_5.y * _ProjectionParams.x);
					  o_4.xy = (tmpvar_6 + tmpvar_5.w);
					  o_4.zw = tmpvar_2.zw;
					  gl_Position = tmpvar_2;
					  xlv_TEXCOORD0 = o_4;
					  xlv_TEXCOORD1 = temp_1.xy;
					  xlv_TEXCOORD2 = temp_1.wz;
					  xlv_TEXCOORD3 = (_WorldSpaceCameraPos - tmpvar_3.xyz).xzy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp float _ReflDistort;
					uniform highp float _RefrDistort;
					uniform sampler2D _ReflectionTex;
					uniform sampler2D _Fresnel;
					uniform sampler2D _RefractionTex;
					uniform highp vec4 _RefrColor;
					uniform sampler2D _BumpMap;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec2 xlv_TEXCOORD2;
					varying highp vec3 xlv_TEXCOORD3;
					void main ()
					{
					  mediump float fresnel_1;
					  mediump vec4 refr_2;
					  highp vec4 uv2_3;
					  mediump vec4 refl_4;
					  highp vec4 uv1_5;
					  mediump float fresnelFac_6;
					  mediump vec3 bump2_7;
					  mediump vec3 bump1_8;
					  lowp vec3 tmpvar_9;
					  tmpvar_9 = ((texture2D (_BumpMap, xlv_TEXCOORD1).xyz * 2.0) - 1.0);
					  bump1_8 = tmpvar_9;
					  lowp vec3 tmpvar_10;
					  tmpvar_10 = ((texture2D (_BumpMap, xlv_TEXCOORD2).xyz * 2.0) - 1.0);
					  bump2_7 = tmpvar_10;
					  mediump vec3 tmpvar_11;
					  tmpvar_11 = ((bump1_8 + bump2_7) * 0.5);
					  highp float tmpvar_12;
					  tmpvar_12 = dot (normalize(xlv_TEXCOORD3), tmpvar_11);
					  fresnelFac_6 = tmpvar_12;
					  uv1_5.zw = xlv_TEXCOORD0.zw;
					  uv1_5.xy = (xlv_TEXCOORD0.xy + (tmpvar_11 * _ReflDistort).xy);
					  lowp vec4 tmpvar_13;
					  tmpvar_13 = texture2DProj (_ReflectionTex, uv1_5);
					  refl_4 = tmpvar_13;
					  uv2_3.zw = xlv_TEXCOORD0.zw;
					  uv2_3.xy = (xlv_TEXCOORD0.xy - (tmpvar_11 * _RefrDistort).xy);
					  lowp vec4 tmpvar_14;
					  tmpvar_14 = texture2DProj (_RefractionTex, uv2_3);
					  highp vec4 tmpvar_15;
					  tmpvar_15 = (tmpvar_14 * _RefrColor);
					  refr_2 = tmpvar_15;
					  lowp float tmpvar_16;
					  tmpvar_16 = texture2D (_Fresnel, vec2(fresnelFac_6)).w;
					  fresnel_1 = tmpvar_16;
					  gl_FragData[0] = mix (refr_2, refl_4, vec4(fresnel_1));
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "WATER_REFRACTIVE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _ProjectionParams;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp vec4 _WaveScale4;
					uniform highp vec4 _WaveOffset;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec2 xlv_TEXCOORD2;
					varying highp vec3 xlv_TEXCOORD3;
					void main ()
					{
					  highp vec4 temp_1;
					  highp vec4 tmpvar_2;
					  tmpvar_2 = (glstate_matrix_mvp * _glesVertex);
					  highp vec4 tmpvar_3;
					  tmpvar_3 = (unity_ObjectToWorld * _glesVertex);
					  temp_1 = ((tmpvar_3.xzxz * _WaveScale4) + _WaveOffset);
					  highp vec4 o_4;
					  highp vec4 tmpvar_5;
					  tmpvar_5 = (tmpvar_2 * 0.5);
					  highp vec2 tmpvar_6;
					  tmpvar_6.x = tmpvar_5.x;
					  tmpvar_6.y = (tmpvar_5.y * _ProjectionParams.x);
					  o_4.xy = (tmpvar_6 + tmpvar_5.w);
					  o_4.zw = tmpvar_2.zw;
					  gl_Position = tmpvar_2;
					  xlv_TEXCOORD0 = o_4;
					  xlv_TEXCOORD1 = temp_1.xy;
					  xlv_TEXCOORD2 = temp_1.wz;
					  xlv_TEXCOORD3 = (_WorldSpaceCameraPos - tmpvar_3.xyz).xzy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp float _ReflDistort;
					uniform highp float _RefrDistort;
					uniform sampler2D _ReflectionTex;
					uniform sampler2D _Fresnel;
					uniform sampler2D _RefractionTex;
					uniform highp vec4 _RefrColor;
					uniform sampler2D _BumpMap;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec2 xlv_TEXCOORD2;
					varying highp vec3 xlv_TEXCOORD3;
					void main ()
					{
					  mediump float fresnel_1;
					  mediump vec4 refr_2;
					  highp vec4 uv2_3;
					  mediump vec4 refl_4;
					  highp vec4 uv1_5;
					  mediump float fresnelFac_6;
					  mediump vec3 bump2_7;
					  mediump vec3 bump1_8;
					  lowp vec3 tmpvar_9;
					  tmpvar_9 = ((texture2D (_BumpMap, xlv_TEXCOORD1).xyz * 2.0) - 1.0);
					  bump1_8 = tmpvar_9;
					  lowp vec3 tmpvar_10;
					  tmpvar_10 = ((texture2D (_BumpMap, xlv_TEXCOORD2).xyz * 2.0) - 1.0);
					  bump2_7 = tmpvar_10;
					  mediump vec3 tmpvar_11;
					  tmpvar_11 = ((bump1_8 + bump2_7) * 0.5);
					  highp float tmpvar_12;
					  tmpvar_12 = dot (normalize(xlv_TEXCOORD3), tmpvar_11);
					  fresnelFac_6 = tmpvar_12;
					  uv1_5.zw = xlv_TEXCOORD0.zw;
					  uv1_5.xy = (xlv_TEXCOORD0.xy + (tmpvar_11 * _ReflDistort).xy);
					  lowp vec4 tmpvar_13;
					  tmpvar_13 = texture2DProj (_ReflectionTex, uv1_5);
					  refl_4 = tmpvar_13;
					  uv2_3.zw = xlv_TEXCOORD0.zw;
					  uv2_3.xy = (xlv_TEXCOORD0.xy - (tmpvar_11 * _RefrDistort).xy);
					  lowp vec4 tmpvar_14;
					  tmpvar_14 = texture2DProj (_RefractionTex, uv2_3);
					  highp vec4 tmpvar_15;
					  tmpvar_15 = (tmpvar_14 * _RefrColor);
					  refr_2 = tmpvar_15;
					  lowp float tmpvar_16;
					  tmpvar_16 = texture2D (_Fresnel, vec2(fresnelFac_6)).w;
					  fresnel_1 = tmpvar_16;
					  gl_FragData[0] = mix (refr_2, refl_4, vec4(fresnel_1));
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "WATER_REFRACTIVE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _ProjectionParams;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp vec4 _WaveScale4;
					uniform highp vec4 _WaveOffset;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec2 xlv_TEXCOORD2;
					varying highp vec3 xlv_TEXCOORD3;
					void main ()
					{
					  highp vec4 temp_1;
					  highp vec4 tmpvar_2;
					  tmpvar_2 = (glstate_matrix_mvp * _glesVertex);
					  highp vec4 tmpvar_3;
					  tmpvar_3 = (unity_ObjectToWorld * _glesVertex);
					  temp_1 = ((tmpvar_3.xzxz * _WaveScale4) + _WaveOffset);
					  highp vec4 o_4;
					  highp vec4 tmpvar_5;
					  tmpvar_5 = (tmpvar_2 * 0.5);
					  highp vec2 tmpvar_6;
					  tmpvar_6.x = tmpvar_5.x;
					  tmpvar_6.y = (tmpvar_5.y * _ProjectionParams.x);
					  o_4.xy = (tmpvar_6 + tmpvar_5.w);
					  o_4.zw = tmpvar_2.zw;
					  gl_Position = tmpvar_2;
					  xlv_TEXCOORD0 = o_4;
					  xlv_TEXCOORD1 = temp_1.xy;
					  xlv_TEXCOORD2 = temp_1.wz;
					  xlv_TEXCOORD3 = (_WorldSpaceCameraPos - tmpvar_3.xyz).xzy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp float _ReflDistort;
					uniform highp float _RefrDistort;
					uniform sampler2D _ReflectionTex;
					uniform sampler2D _Fresnel;
					uniform sampler2D _RefractionTex;
					uniform highp vec4 _RefrColor;
					uniform sampler2D _BumpMap;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec2 xlv_TEXCOORD2;
					varying highp vec3 xlv_TEXCOORD3;
					void main ()
					{
					  mediump float fresnel_1;
					  mediump vec4 refr_2;
					  highp vec4 uv2_3;
					  mediump vec4 refl_4;
					  highp vec4 uv1_5;
					  mediump float fresnelFac_6;
					  mediump vec3 bump2_7;
					  mediump vec3 bump1_8;
					  lowp vec3 tmpvar_9;
					  tmpvar_9 = ((texture2D (_BumpMap, xlv_TEXCOORD1).xyz * 2.0) - 1.0);
					  bump1_8 = tmpvar_9;
					  lowp vec3 tmpvar_10;
					  tmpvar_10 = ((texture2D (_BumpMap, xlv_TEXCOORD2).xyz * 2.0) - 1.0);
					  bump2_7 = tmpvar_10;
					  mediump vec3 tmpvar_11;
					  tmpvar_11 = ((bump1_8 + bump2_7) * 0.5);
					  highp float tmpvar_12;
					  tmpvar_12 = dot (normalize(xlv_TEXCOORD3), tmpvar_11);
					  fresnelFac_6 = tmpvar_12;
					  uv1_5.zw = xlv_TEXCOORD0.zw;
					  uv1_5.xy = (xlv_TEXCOORD0.xy + (tmpvar_11 * _ReflDistort).xy);
					  lowp vec4 tmpvar_13;
					  tmpvar_13 = texture2DProj (_ReflectionTex, uv1_5);
					  refl_4 = tmpvar_13;
					  uv2_3.zw = xlv_TEXCOORD0.zw;
					  uv2_3.xy = (xlv_TEXCOORD0.xy - (tmpvar_11 * _RefrDistort).xy);
					  lowp vec4 tmpvar_14;
					  tmpvar_14 = texture2DProj (_RefractionTex, uv2_3);
					  highp vec4 tmpvar_15;
					  tmpvar_15 = (tmpvar_14 * _RefrColor);
					  refr_2 = tmpvar_15;
					  lowp float tmpvar_16;
					  tmpvar_16 = texture2D (_Fresnel, vec2(fresnelFac_6)).w;
					  fresnel_1 = tmpvar_16;
					  gl_FragData[0] = mix (refr_2, refl_4, vec4(fresnel_1));
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "WATER_REFLECTIVE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _ProjectionParams;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp vec4 _WaveScale4;
					uniform highp vec4 _WaveOffset;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec2 xlv_TEXCOORD2;
					varying highp vec3 xlv_TEXCOORD3;
					void main ()
					{
					  highp vec4 temp_1;
					  highp vec4 tmpvar_2;
					  tmpvar_2 = (glstate_matrix_mvp * _glesVertex);
					  highp vec4 tmpvar_3;
					  tmpvar_3 = (unity_ObjectToWorld * _glesVertex);
					  temp_1 = ((tmpvar_3.xzxz * _WaveScale4) + _WaveOffset);
					  highp vec4 o_4;
					  highp vec4 tmpvar_5;
					  tmpvar_5 = (tmpvar_2 * 0.5);
					  highp vec2 tmpvar_6;
					  tmpvar_6.x = tmpvar_5.x;
					  tmpvar_6.y = (tmpvar_5.y * _ProjectionParams.x);
					  o_4.xy = (tmpvar_6 + tmpvar_5.w);
					  o_4.zw = tmpvar_2.zw;
					  gl_Position = tmpvar_2;
					  xlv_TEXCOORD0 = o_4;
					  xlv_TEXCOORD1 = temp_1.xy;
					  xlv_TEXCOORD2 = temp_1.wz;
					  xlv_TEXCOORD3 = (_WorldSpaceCameraPos - tmpvar_3.xyz).xzy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp float _ReflDistort;
					uniform sampler2D _ReflectionTex;
					uniform sampler2D _ReflectiveColor;
					uniform sampler2D _BumpMap;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec2 xlv_TEXCOORD2;
					varying highp vec3 xlv_TEXCOORD3;
					void main ()
					{
					  mediump vec4 water_1;
					  mediump vec4 color_2;
					  mediump vec4 refl_3;
					  highp vec4 uv1_4;
					  mediump float fresnelFac_5;
					  mediump vec3 bump2_6;
					  mediump vec3 bump1_7;
					  lowp vec3 tmpvar_8;
					  tmpvar_8 = ((texture2D (_BumpMap, xlv_TEXCOORD1).xyz * 2.0) - 1.0);
					  bump1_7 = tmpvar_8;
					  lowp vec3 tmpvar_9;
					  tmpvar_9 = ((texture2D (_BumpMap, xlv_TEXCOORD2).xyz * 2.0) - 1.0);
					  bump2_6 = tmpvar_9;
					  mediump vec3 tmpvar_10;
					  tmpvar_10 = ((bump1_7 + bump2_6) * 0.5);
					  highp float tmpvar_11;
					  tmpvar_11 = dot (normalize(xlv_TEXCOORD3), tmpvar_10);
					  fresnelFac_5 = tmpvar_11;
					  uv1_4.zw = xlv_TEXCOORD0.zw;
					  uv1_4.xy = (xlv_TEXCOORD0.xy + (tmpvar_10 * _ReflDistort).xy);
					  lowp vec4 tmpvar_12;
					  tmpvar_12 = texture2DProj (_ReflectionTex, uv1_4);
					  refl_3 = tmpvar_12;
					  lowp vec4 tmpvar_13;
					  tmpvar_13 = texture2D (_ReflectiveColor, vec2(fresnelFac_5));
					  water_1 = tmpvar_13;
					  color_2.xyz = mix (water_1.xyz, refl_3.xyz, water_1.www);
					  color_2.w = (refl_3.w * water_1.w);
					  gl_FragData[0] = color_2;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "WATER_REFLECTIVE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _ProjectionParams;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp vec4 _WaveScale4;
					uniform highp vec4 _WaveOffset;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec2 xlv_TEXCOORD2;
					varying highp vec3 xlv_TEXCOORD3;
					void main ()
					{
					  highp vec4 temp_1;
					  highp vec4 tmpvar_2;
					  tmpvar_2 = (glstate_matrix_mvp * _glesVertex);
					  highp vec4 tmpvar_3;
					  tmpvar_3 = (unity_ObjectToWorld * _glesVertex);
					  temp_1 = ((tmpvar_3.xzxz * _WaveScale4) + _WaveOffset);
					  highp vec4 o_4;
					  highp vec4 tmpvar_5;
					  tmpvar_5 = (tmpvar_2 * 0.5);
					  highp vec2 tmpvar_6;
					  tmpvar_6.x = tmpvar_5.x;
					  tmpvar_6.y = (tmpvar_5.y * _ProjectionParams.x);
					  o_4.xy = (tmpvar_6 + tmpvar_5.w);
					  o_4.zw = tmpvar_2.zw;
					  gl_Position = tmpvar_2;
					  xlv_TEXCOORD0 = o_4;
					  xlv_TEXCOORD1 = temp_1.xy;
					  xlv_TEXCOORD2 = temp_1.wz;
					  xlv_TEXCOORD3 = (_WorldSpaceCameraPos - tmpvar_3.xyz).xzy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp float _ReflDistort;
					uniform sampler2D _ReflectionTex;
					uniform sampler2D _ReflectiveColor;
					uniform sampler2D _BumpMap;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec2 xlv_TEXCOORD2;
					varying highp vec3 xlv_TEXCOORD3;
					void main ()
					{
					  mediump vec4 water_1;
					  mediump vec4 color_2;
					  mediump vec4 refl_3;
					  highp vec4 uv1_4;
					  mediump float fresnelFac_5;
					  mediump vec3 bump2_6;
					  mediump vec3 bump1_7;
					  lowp vec3 tmpvar_8;
					  tmpvar_8 = ((texture2D (_BumpMap, xlv_TEXCOORD1).xyz * 2.0) - 1.0);
					  bump1_7 = tmpvar_8;
					  lowp vec3 tmpvar_9;
					  tmpvar_9 = ((texture2D (_BumpMap, xlv_TEXCOORD2).xyz * 2.0) - 1.0);
					  bump2_6 = tmpvar_9;
					  mediump vec3 tmpvar_10;
					  tmpvar_10 = ((bump1_7 + bump2_6) * 0.5);
					  highp float tmpvar_11;
					  tmpvar_11 = dot (normalize(xlv_TEXCOORD3), tmpvar_10);
					  fresnelFac_5 = tmpvar_11;
					  uv1_4.zw = xlv_TEXCOORD0.zw;
					  uv1_4.xy = (xlv_TEXCOORD0.xy + (tmpvar_10 * _ReflDistort).xy);
					  lowp vec4 tmpvar_12;
					  tmpvar_12 = texture2DProj (_ReflectionTex, uv1_4);
					  refl_3 = tmpvar_12;
					  lowp vec4 tmpvar_13;
					  tmpvar_13 = texture2D (_ReflectiveColor, vec2(fresnelFac_5));
					  water_1 = tmpvar_13;
					  color_2.xyz = mix (water_1.xyz, refl_3.xyz, water_1.www);
					  color_2.w = (refl_3.w * water_1.w);
					  gl_FragData[0] = color_2;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "WATER_REFLECTIVE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _ProjectionParams;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp vec4 _WaveScale4;
					uniform highp vec4 _WaveOffset;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec2 xlv_TEXCOORD2;
					varying highp vec3 xlv_TEXCOORD3;
					void main ()
					{
					  highp vec4 temp_1;
					  highp vec4 tmpvar_2;
					  tmpvar_2 = (glstate_matrix_mvp * _glesVertex);
					  highp vec4 tmpvar_3;
					  tmpvar_3 = (unity_ObjectToWorld * _glesVertex);
					  temp_1 = ((tmpvar_3.xzxz * _WaveScale4) + _WaveOffset);
					  highp vec4 o_4;
					  highp vec4 tmpvar_5;
					  tmpvar_5 = (tmpvar_2 * 0.5);
					  highp vec2 tmpvar_6;
					  tmpvar_6.x = tmpvar_5.x;
					  tmpvar_6.y = (tmpvar_5.y * _ProjectionParams.x);
					  o_4.xy = (tmpvar_6 + tmpvar_5.w);
					  o_4.zw = tmpvar_2.zw;
					  gl_Position = tmpvar_2;
					  xlv_TEXCOORD0 = o_4;
					  xlv_TEXCOORD1 = temp_1.xy;
					  xlv_TEXCOORD2 = temp_1.wz;
					  xlv_TEXCOORD3 = (_WorldSpaceCameraPos - tmpvar_3.xyz).xzy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp float _ReflDistort;
					uniform sampler2D _ReflectionTex;
					uniform sampler2D _ReflectiveColor;
					uniform sampler2D _BumpMap;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec2 xlv_TEXCOORD2;
					varying highp vec3 xlv_TEXCOORD3;
					void main ()
					{
					  mediump vec4 water_1;
					  mediump vec4 color_2;
					  mediump vec4 refl_3;
					  highp vec4 uv1_4;
					  mediump float fresnelFac_5;
					  mediump vec3 bump2_6;
					  mediump vec3 bump1_7;
					  lowp vec3 tmpvar_8;
					  tmpvar_8 = ((texture2D (_BumpMap, xlv_TEXCOORD1).xyz * 2.0) - 1.0);
					  bump1_7 = tmpvar_8;
					  lowp vec3 tmpvar_9;
					  tmpvar_9 = ((texture2D (_BumpMap, xlv_TEXCOORD2).xyz * 2.0) - 1.0);
					  bump2_6 = tmpvar_9;
					  mediump vec3 tmpvar_10;
					  tmpvar_10 = ((bump1_7 + bump2_6) * 0.5);
					  highp float tmpvar_11;
					  tmpvar_11 = dot (normalize(xlv_TEXCOORD3), tmpvar_10);
					  fresnelFac_5 = tmpvar_11;
					  uv1_4.zw = xlv_TEXCOORD0.zw;
					  uv1_4.xy = (xlv_TEXCOORD0.xy + (tmpvar_10 * _ReflDistort).xy);
					  lowp vec4 tmpvar_12;
					  tmpvar_12 = texture2DProj (_ReflectionTex, uv1_4);
					  refl_3 = tmpvar_12;
					  lowp vec4 tmpvar_13;
					  tmpvar_13 = texture2D (_ReflectiveColor, vec2(fresnelFac_5));
					  water_1 = tmpvar_13;
					  color_2.xyz = mix (water_1.xyz, refl_3.xyz, water_1.www);
					  color_2.w = (refl_3.w * water_1.w);
					  gl_FragData[0] = color_2;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "WATER_SIMPLE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp vec4 _WaveScale4;
					uniform highp vec4 _WaveOffset;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec3 xlv_TEXCOORD2;
					void main ()
					{
					  highp vec4 temp_1;
					  highp vec4 tmpvar_2;
					  tmpvar_2 = (unity_ObjectToWorld * _glesVertex);
					  temp_1 = ((tmpvar_2.xzxz * _WaveScale4) + _WaveOffset);
					  gl_Position = (glstate_matrix_mvp * _glesVertex);
					  xlv_TEXCOORD0 = temp_1.xy;
					  xlv_TEXCOORD1 = temp_1.wz;
					  xlv_TEXCOORD2 = (_WorldSpaceCameraPos - tmpvar_2.xyz).xzy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _ReflectiveColor;
					uniform highp vec4 _HorizonColor;
					uniform sampler2D _BumpMap;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec3 xlv_TEXCOORD2;
					void main ()
					{
					  mediump vec4 water_1;
					  mediump vec4 color_2;
					  mediump float fresnelFac_3;
					  mediump vec3 bump2_4;
					  mediump vec3 bump1_5;
					  lowp vec3 tmpvar_6;
					  tmpvar_6 = ((texture2D (_BumpMap, xlv_TEXCOORD0).xyz * 2.0) - 1.0);
					  bump1_5 = tmpvar_6;
					  lowp vec3 tmpvar_7;
					  tmpvar_7 = ((texture2D (_BumpMap, xlv_TEXCOORD1).xyz * 2.0) - 1.0);
					  bump2_4 = tmpvar_7;
					  mediump vec3 tmpvar_8;
					  tmpvar_8 = ((bump1_5 + bump2_4) * 0.5);
					  highp float tmpvar_9;
					  tmpvar_9 = dot (normalize(xlv_TEXCOORD2), tmpvar_8);
					  fresnelFac_3 = tmpvar_9;
					  lowp vec4 tmpvar_10;
					  tmpvar_10 = texture2D (_ReflectiveColor, vec2(fresnelFac_3));
					  water_1 = tmpvar_10;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = mix (water_1.xyz, _HorizonColor.xyz, water_1.www);
					  color_2.xyz = tmpvar_11;
					  color_2.w = _HorizonColor.w;
					  gl_FragData[0] = color_2;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "WATER_SIMPLE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp vec4 _WaveScale4;
					uniform highp vec4 _WaveOffset;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec3 xlv_TEXCOORD2;
					void main ()
					{
					  highp vec4 temp_1;
					  highp vec4 tmpvar_2;
					  tmpvar_2 = (unity_ObjectToWorld * _glesVertex);
					  temp_1 = ((tmpvar_2.xzxz * _WaveScale4) + _WaveOffset);
					  gl_Position = (glstate_matrix_mvp * _glesVertex);
					  xlv_TEXCOORD0 = temp_1.xy;
					  xlv_TEXCOORD1 = temp_1.wz;
					  xlv_TEXCOORD2 = (_WorldSpaceCameraPos - tmpvar_2.xyz).xzy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _ReflectiveColor;
					uniform highp vec4 _HorizonColor;
					uniform sampler2D _BumpMap;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec3 xlv_TEXCOORD2;
					void main ()
					{
					  mediump vec4 water_1;
					  mediump vec4 color_2;
					  mediump float fresnelFac_3;
					  mediump vec3 bump2_4;
					  mediump vec3 bump1_5;
					  lowp vec3 tmpvar_6;
					  tmpvar_6 = ((texture2D (_BumpMap, xlv_TEXCOORD0).xyz * 2.0) - 1.0);
					  bump1_5 = tmpvar_6;
					  lowp vec3 tmpvar_7;
					  tmpvar_7 = ((texture2D (_BumpMap, xlv_TEXCOORD1).xyz * 2.0) - 1.0);
					  bump2_4 = tmpvar_7;
					  mediump vec3 tmpvar_8;
					  tmpvar_8 = ((bump1_5 + bump2_4) * 0.5);
					  highp float tmpvar_9;
					  tmpvar_9 = dot (normalize(xlv_TEXCOORD2), tmpvar_8);
					  fresnelFac_3 = tmpvar_9;
					  lowp vec4 tmpvar_10;
					  tmpvar_10 = texture2D (_ReflectiveColor, vec2(fresnelFac_3));
					  water_1 = tmpvar_10;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = mix (water_1.xyz, _HorizonColor.xyz, water_1.www);
					  color_2.xyz = tmpvar_11;
					  color_2.w = _HorizonColor.w;
					  gl_FragData[0] = color_2;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "WATER_SIMPLE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp vec4 _WaveScale4;
					uniform highp vec4 _WaveOffset;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec3 xlv_TEXCOORD2;
					void main ()
					{
					  highp vec4 temp_1;
					  highp vec4 tmpvar_2;
					  tmpvar_2 = (unity_ObjectToWorld * _glesVertex);
					  temp_1 = ((tmpvar_2.xzxz * _WaveScale4) + _WaveOffset);
					  gl_Position = (glstate_matrix_mvp * _glesVertex);
					  xlv_TEXCOORD0 = temp_1.xy;
					  xlv_TEXCOORD1 = temp_1.wz;
					  xlv_TEXCOORD2 = (_WorldSpaceCameraPos - tmpvar_2.xyz).xzy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _ReflectiveColor;
					uniform highp vec4 _HorizonColor;
					uniform sampler2D _BumpMap;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec3 xlv_TEXCOORD2;
					void main ()
					{
					  mediump vec4 water_1;
					  mediump vec4 color_2;
					  mediump float fresnelFac_3;
					  mediump vec3 bump2_4;
					  mediump vec3 bump1_5;
					  lowp vec3 tmpvar_6;
					  tmpvar_6 = ((texture2D (_BumpMap, xlv_TEXCOORD0).xyz * 2.0) - 1.0);
					  bump1_5 = tmpvar_6;
					  lowp vec3 tmpvar_7;
					  tmpvar_7 = ((texture2D (_BumpMap, xlv_TEXCOORD1).xyz * 2.0) - 1.0);
					  bump2_4 = tmpvar_7;
					  mediump vec3 tmpvar_8;
					  tmpvar_8 = ((bump1_5 + bump2_4) * 0.5);
					  highp float tmpvar_9;
					  tmpvar_9 = dot (normalize(xlv_TEXCOORD2), tmpvar_8);
					  fresnelFac_3 = tmpvar_9;
					  lowp vec4 tmpvar_10;
					  tmpvar_10 = texture2D (_ReflectiveColor, vec2(fresnelFac_3));
					  water_1 = tmpvar_10;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = mix (water_1.xyz, _HorizonColor.xyz, water_1.www);
					  color_2.xyz = tmpvar_11;
					  color_2.w = _HorizonColor.w;
					  gl_FragData[0] = color_2;
					}
					
					
					#endif
}
}
Program "fp" {
SubProgram "gles hw_tier01 " {
Keywords { "WATER_REFRACTIVE" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "WATER_REFRACTIVE" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "WATER_REFRACTIVE" }
					
}
SubProgram "gles hw_tier01 " {
Keywords { "WATER_REFLECTIVE" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "WATER_REFLECTIVE" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "WATER_REFLECTIVE" }
					
}
SubProgram "gles hw_tier01 " {
Keywords { "WATER_SIMPLE" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "WATER_SIMPLE" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "WATER_SIMPLE" }
					
}
}
 }
}
}