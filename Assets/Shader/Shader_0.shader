Shader "Standard" {
Properties {
 _Color ("Color", Color) = (1.000000,1.000000,1.000000,1.000000)
 _MainTex ("Albedo", 2D) = "white" { }
 _Cutoff ("Alpha Cutoff", Range(0.000000,1.000000)) = 0.500000
 _Glossiness ("Smoothness", Range(0.000000,1.000000)) = 0.500000
 _GlossMapScale ("Smoothness Scale", Range(0.000000,1.000000)) = 1.000000
[Enum(Metallic Alpha,0,Albedo Alpha,1)]  _SmoothnessTextureChannel ("Smoothness texture channel", Float) = 0.000000
[Gamma]  _Metallic ("Metallic", Range(0.000000,1.000000)) = 0.000000
 _MetallicGlossMap ("Metallic", 2D) = "white" { }
[ToggleOff]  _SpecularHighlights ("Specular Highlights", Float) = 1.000000
[ToggleOff]  _GlossyReflections ("Glossy Reflections", Float) = 1.000000
 _BumpScale ("Scale", Float) = 1.000000
 _BumpMap ("Normal Map", 2D) = "bump" { }
 _Parallax ("Height Scale", Range(0.005000,0.080000)) = 0.020000
 _ParallaxMap ("Height Map", 2D) = "black" { }
 _OcclusionStrength ("Strength", Range(0.000000,1.000000)) = 1.000000
 _OcclusionMap ("Occlusion", 2D) = "white" { }
 _EmissionColor ("Color", Color) = (0.000000,0.000000,0.000000,1.000000)
 _EmissionMap ("Emission", 2D) = "white" { }
 _DetailMask ("Detail Mask", 2D) = "white" { }
 _DetailAlbedoMap ("Detail Albedo x2", 2D) = "grey" { }
 _DetailNormalMapScale ("Scale", Float) = 1.000000
 _DetailNormalMap ("Normal Map", 2D) = "bump" { }
[Enum(UV0,0,UV1,1)]  _UVSec ("UV Set for secondary textures", Float) = 0.000000
[HideInInspector]  _Mode ("__mode", Float) = 0.000000
[HideInInspector]  _SrcBlend ("__src", Float) = 1.000000
[HideInInspector]  _DstBlend ("__dst", Float) = 0.000000
[HideInInspector]  _ZWrite ("__zw", Float) = 1.000000
}
SubShader { 
 LOD 300
 Tags { "RenderType"="Opaque" "PerformanceChecks"="False" }
 Pass {
  Name "FORWARD"
  Tags { "LIGHTMODE"="ForwardBase" "SHADOWSUPPORT"="true" "RenderType"="Opaque" "PerformanceChecks"="False" }
  ZWrite [_ZWrite]
  Blend [_SrcBlend] [_DstBlend]
  GpuProgramID 61372
Program "vp" {
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 unity_SHAr;
					uniform mediump vec4 unity_SHAg;
					uniform mediump vec4 unity_SHAb;
					uniform mediump vec4 unity_SHBr;
					uniform mediump vec4 unity_SHBg;
					uniform mediump vec4 unity_SHBb;
					uniform mediump vec4 unity_SHC;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec4 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD4;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  mediump vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_10;
					  highp vec4 tmpvar_11;
					  tmpvar_11.w = 1.0;
					  tmpvar_11.xyz = _glesVertex.xyz;
					  tmpvar_10 = (glstate_matrix_mvp * tmpvar_11);
					  highp vec4 texcoord_12;
					  texcoord_12.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_13;
					  if ((_UVSec == 0.0)) {
					    tmpvar_13 = tmpvar_2;
					  } else {
					    tmpvar_13 = tmpvar_3;
					  };
					  texcoord_12.zw = ((tmpvar_13 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_9.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  normalWorld_4 = tmpvar_19;
					  tmpvar_8.xyz = normalWorld_4;
					  tmpvar_6.xyz = eyeVec_5;
					  mediump vec4 ambientOrLightmapUV_20;
					  ambientOrLightmapUV_20.w = 0.0;
					  mediump vec4 tmpvar_21;
					  tmpvar_21.w = 1.0;
					  tmpvar_21.xyz = normalWorld_4;
					  mediump vec3 res_22;
					  mediump vec3 x_23;
					  x_23.x = dot (unity_SHAr, tmpvar_21);
					  x_23.y = dot (unity_SHAg, tmpvar_21);
					  x_23.z = dot (unity_SHAb, tmpvar_21);
					  mediump vec3 x1_24;
					  mediump vec4 tmpvar_25;
					  tmpvar_25 = (normalWorld_4.xyzz * normalWorld_4.yzzx);
					  x1_24.x = dot (unity_SHBr, tmpvar_25);
					  x1_24.y = dot (unity_SHBg, tmpvar_25);
					  x1_24.z = dot (unity_SHBb, tmpvar_25);
					  res_22 = (x_23 + (x1_24 + (unity_SHC.xyz * 
					    ((normalWorld_4.x * normalWorld_4.x) - (normalWorld_4.y * normalWorld_4.y))
					  )));
					  res_22 = max (((1.055 * 
					    pow (max (res_22, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  ambientOrLightmapUV_20.xyz = max (vec3(0.0, 0.0, 0.0), res_22);
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  mediump float x_26;
					  x_26 = (1.0 - max (0.0, dot (normalWorld_4, 
					    -(eyeVec_5)
					  )));
					  tmpvar_8.w = ((x_26 * x_26) * (x_26 * x_26));
					  tmpvar_6.w = clamp ((_Glossiness + (1.0 - 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  )), 0.0, 1.0);
					  gl_Position = tmpvar_10;
					  xlv_TEXCOORD0 = texcoord_12;
					  xlv_TEXCOORD1 = tmpvar_6;
					  xlv_TEXCOORD2 = ambientOrLightmapUV_20;
					  xlv_TEXCOORD4 = tmpvar_7;
					  xlv_TEXCOORD5 = tmpvar_8;
					}
					
					
					#endif
					#ifdef FRAGMENT
					#extension GL_EXT_shader_texture_lod : enable
					lowp vec4 impl_low_textureCubeLodEXT(lowp samplerCube sampler, highp vec3 coord, mediump float lod)
					{
					#if defined(GL_EXT_shader_texture_lod)
						return textureCubeLodEXT(sampler, coord, lod);
					#else
						return textureCube(sampler, coord, lod);
					#endif
					}
					
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform lowp samplerCube unity_SpecCube0;
					uniform mediump vec4 unity_SpecCube0_HDR;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _OcclusionMap;
					uniform mediump float _OcclusionStrength;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec4 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD4;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump float rl_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump vec3 tmpvar_4;
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic));
					  tmpvar_4 = (tmpvar_3 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)));
					  mediump vec3 tmpvar_6;
					  tmpvar_6 = _LightColor0.xyz;
					  mediump float occ_7;
					  lowp float tmpvar_8;
					  tmpvar_8 = texture2D (_OcclusionMap, xlv_TEXCOORD0.xy).y;
					  occ_7 = tmpvar_8;
					  mediump float tmpvar_9;
					  tmpvar_9 = ((1.0 - _OcclusionStrength) + (occ_7 * _OcclusionStrength));
					  rl_1 = dot (xlv_TEXCOORD4.yzw, _WorldSpaceLightPos0.xyz);
					  highp vec4 tmpvar_10;
					  tmpvar_10 = unity_SpecCube0_HDR;
					  mediump float tmpvar_11;
					  tmpvar_11 = (1.0 - _Glossiness);
					  mediump vec4 hdr_12;
					  hdr_12 = tmpvar_10;
					  mediump vec4 tmpvar_13;
					  tmpvar_13.xyz = xlv_TEXCOORD4.yzw;
					  tmpvar_13.w = ((tmpvar_11 * (1.7 - 
					    (0.7 * tmpvar_11)
					  )) * 6.0);
					  lowp vec4 tmpvar_14;
					  tmpvar_14 = impl_low_textureCubeLodEXT (unity_SpecCube0, xlv_TEXCOORD4.yzw, tmpvar_13.w);
					  mediump vec4 tmpvar_15;
					  tmpvar_15 = tmpvar_14;
					  mediump vec2 tmpvar_16;
					  tmpvar_16.x = ((rl_1 * rl_1) * (rl_1 * rl_1));
					  tmpvar_16.y = tmpvar_11;
					  lowp vec4 tmpvar_17;
					  tmpvar_17 = texture2D (unity_NHxRoughness, tmpvar_16);
					  mediump vec4 tmpvar_18;
					  tmpvar_18.w = 1.0;
					  tmpvar_18.xyz = (((
					    (xlv_TEXCOORD2.xyz * tmpvar_9)
					   * tmpvar_4) + (
					    (((hdr_12.x * tmpvar_15.w) * tmpvar_15.xyz) * tmpvar_9)
					   * 
					    mix (tmpvar_5, xlv_TEXCOORD1.www, xlv_TEXCOORD5.www)
					  )) + ((tmpvar_4 + 
					    ((tmpvar_17.w * 16.0) * tmpvar_5)
					  ) * (tmpvar_6 * 
					    max (0.0, dot (xlv_TEXCOORD5.xyz, _WorldSpaceLightPos0.xyz))
					  )));
					  mediump vec4 xlat_varoutput_19;
					  xlat_varoutput_19.xyz = tmpvar_18.xyz;
					  xlat_varoutput_19.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_19;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 unity_SHBr;
					uniform mediump vec4 unity_SHBg;
					uniform mediump vec4 unity_SHBb;
					uniform mediump vec4 unity_SHC;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 n_13;
					  n_13 = (tmpvar_7.xyz - _WorldSpaceCameraPos);
					  tmpvar_12 = n_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  mediump vec3 normalWorld_19;
					  normalWorld_19 = tmpvar_18;
					  mediump vec4 ambientOrLightmapUV_20;
					  ambientOrLightmapUV_20.w = 0.0;
					  mediump vec3 x1_21;
					  mediump vec4 tmpvar_22;
					  tmpvar_22 = (normalWorld_19.xyzz * normalWorld_19.yzzx);
					  x1_21.x = dot (unity_SHBr, tmpvar_22);
					  x1_21.y = dot (unity_SHBg, tmpvar_22);
					  x1_21.z = dot (unity_SHBb, tmpvar_22);
					  ambientOrLightmapUV_20.xyz = (x1_21 + (unity_SHC.xyz * (
					    (normalWorld_19.x * normalWorld_19.x)
					   - 
					    (normalWorld_19.y * normalWorld_19.y)
					  )));
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					  xlv_TEXCOORD5 = ambientOrLightmapUV_20;
					}
					
					
					#endif
					#ifdef FRAGMENT
					#extension GL_EXT_shader_texture_lod : enable
					lowp vec4 impl_low_textureCubeLodEXT(lowp samplerCube sampler, highp vec3 coord, mediump float lod)
					{
					#if defined(GL_EXT_shader_texture_lod)
						return textureCubeLodEXT(sampler, coord, lod);
					#else
						return textureCube(sampler, coord, lod);
					#endif
					}
					
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform mediump vec4 unity_SHAr;
					uniform mediump vec4 unity_SHAg;
					uniform mediump vec4 unity_SHAb;
					uniform lowp samplerCube unity_SpecCube0;
					uniform mediump vec4 unity_SpecCube0_HDR;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _OcclusionMap;
					uniform mediump float _OcclusionStrength;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec4 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump vec3 tmpvar_4;
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic));
					  mediump float tmpvar_6;
					  tmpvar_6 = (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w));
					  tmpvar_4 = (tmpvar_3 * tmpvar_6);
					  mediump vec3 tmpvar_7;
					  tmpvar_7 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_8;
					  tmpvar_8 = normalize(xlv_TEXCOORD1);
					  mediump vec3 tmpvar_9;
					  tmpvar_9 = _LightColor0.xyz;
					  mediump float occ_10;
					  lowp float tmpvar_11;
					  tmpvar_11 = texture2D (_OcclusionMap, xlv_TEXCOORD0.xy).y;
					  occ_10 = tmpvar_11;
					  mediump float tmpvar_12;
					  tmpvar_12 = ((1.0 - _OcclusionStrength) + (occ_10 * _OcclusionStrength));
					  highp vec4 tmpvar_13;
					  tmpvar_13 = unity_SpecCube0_HDR;
					  mediump float tmpvar_14;
					  tmpvar_14 = (1.0 - _Glossiness);
					  mediump vec3 tmpvar_15;
					  tmpvar_15 = (tmpvar_8 - (2.0 * (
					    dot (tmpvar_7, tmpvar_8)
					   * tmpvar_7)));
					  mediump vec4 tmpvar_16;
					  tmpvar_16.w = 1.0;
					  tmpvar_16.xyz = tmpvar_7;
					  mediump vec3 x_17;
					  x_17.x = dot (unity_SHAr, tmpvar_16);
					  x_17.y = dot (unity_SHAg, tmpvar_16);
					  x_17.z = dot (unity_SHAb, tmpvar_16);
					  mediump vec4 hdr_18;
					  hdr_18 = tmpvar_13;
					  mediump vec4 tmpvar_19;
					  tmpvar_19.xyz = tmpvar_15;
					  tmpvar_19.w = ((tmpvar_14 * (1.7 - 
					    (0.7 * tmpvar_14)
					  )) * 6.0);
					  lowp vec4 tmpvar_20;
					  tmpvar_20 = impl_low_textureCubeLodEXT (unity_SpecCube0, tmpvar_15, tmpvar_19.w);
					  mediump vec4 tmpvar_21;
					  tmpvar_21 = tmpvar_20;
					  mediump vec3 viewDir_22;
					  viewDir_22 = -(tmpvar_8);
					  mediump float specular_23;
					  mediump vec3 tmpvar_24;
					  mediump vec3 inVec_25;
					  inVec_25 = (_WorldSpaceLightPos0.xyz + viewDir_22);
					  tmpvar_24 = (inVec_25 * inversesqrt(max (0.001, 
					    dot (inVec_25, inVec_25)
					  )));
					  mediump float tmpvar_26;
					  tmpvar_26 = max (0.0, dot (_WorldSpaceLightPos0.xyz, tmpvar_24));
					  mediump float tmpvar_27;
					  tmpvar_27 = max (0.0001, (tmpvar_14 * tmpvar_14));
					  mediump float tmpvar_28;
					  tmpvar_28 = max (((2.0 / 
					    (tmpvar_27 * tmpvar_27)
					  ) - 2.0), 0.0001);
					  specular_23 = sqrt(max (0.0001, (
					    ((tmpvar_28 + 1.0) * pow (max (0.0, dot (tmpvar_7, tmpvar_24)), tmpvar_28))
					   / 
					    (((8.0 * (
					      ((tmpvar_26 * tmpvar_26) * _Glossiness)
					     + 
					      (tmpvar_14 * tmpvar_14)
					    )) * tmpvar_26) + 0.0001)
					  )));
					  mediump float tmpvar_29;
					  tmpvar_29 = clamp (specular_23, 0.0, 100.0);
					  specular_23 = tmpvar_29;
					  mediump float x_30;
					  x_30 = (1.0 - max (0.0, dot (tmpvar_7, viewDir_22)));
					  mediump vec3 tmpvar_31;
					  tmpvar_31 = (((
					    ((tmpvar_4 + (tmpvar_29 * tmpvar_5)) * tmpvar_9)
					   * 
					    max (0.0, dot (tmpvar_7, _WorldSpaceLightPos0.xyz))
					  ) + (
					    (max (((1.055 * 
					      pow (max (vec3(0.0, 0.0, 0.0), (xlv_TEXCOORD5.xyz + x_17)), vec3(0.4166667, 0.4166667, 0.4166667))
					    ) - 0.055), vec3(0.0, 0.0, 0.0)) * tmpvar_12)
					   * tmpvar_4)) + ((
					    (1.0 - ((tmpvar_14 * tmpvar_14) * (tmpvar_14 * 0.28)))
					   * 
					    (((hdr_18.x * tmpvar_21.w) * tmpvar_21.xyz) * tmpvar_12)
					  ) * mix (tmpvar_5, vec3(
					    clamp ((_Glossiness + (1.0 - tmpvar_6)), 0.0, 1.0)
					  ), vec3(
					    ((x_30 * x_30) * (x_30 * x_30))
					  ))));
					  mediump vec4 tmpvar_32;
					  tmpvar_32.w = 1.0;
					  tmpvar_32.xyz = tmpvar_31;
					  c_1.w = tmpvar_32.w;
					  c_1.xyz = tmpvar_31;
					  c_1.xyz = c_1.xyz;
					  mediump vec4 xlat_varoutput_33;
					  xlat_varoutput_33.xyz = c_1.xyz;
					  xlat_varoutput_33.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_33;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 unity_SHBr;
					uniform mediump vec4 unity_SHBg;
					uniform mediump vec4 unity_SHBb;
					uniform mediump vec4 unity_SHC;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 n_13;
					  n_13 = (tmpvar_7.xyz - _WorldSpaceCameraPos);
					  tmpvar_12 = n_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  mediump vec3 normalWorld_19;
					  normalWorld_19 = tmpvar_18;
					  mediump vec4 ambientOrLightmapUV_20;
					  ambientOrLightmapUV_20.w = 0.0;
					  mediump vec3 x1_21;
					  mediump vec4 tmpvar_22;
					  tmpvar_22 = (normalWorld_19.xyzz * normalWorld_19.yzzx);
					  x1_21.x = dot (unity_SHBr, tmpvar_22);
					  x1_21.y = dot (unity_SHBg, tmpvar_22);
					  x1_21.z = dot (unity_SHBb, tmpvar_22);
					  ambientOrLightmapUV_20.xyz = (x1_21 + (unity_SHC.xyz * (
					    (normalWorld_19.x * normalWorld_19.x)
					   - 
					    (normalWorld_19.y * normalWorld_19.y)
					  )));
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					  xlv_TEXCOORD5 = ambientOrLightmapUV_20;
					}
					
					
					#endif
					#ifdef FRAGMENT
					#extension GL_EXT_shader_texture_lod : enable
					lowp vec4 impl_low_textureCubeLodEXT(lowp samplerCube sampler, highp vec3 coord, mediump float lod)
					{
					#if defined(GL_EXT_shader_texture_lod)
						return textureCubeLodEXT(sampler, coord, lod);
					#else
						return textureCube(sampler, coord, lod);
					#endif
					}
					
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform mediump vec4 unity_SHAr;
					uniform mediump vec4 unity_SHAg;
					uniform mediump vec4 unity_SHAb;
					uniform lowp samplerCube unity_SpecCube0;
					uniform mediump vec4 unity_SpecCube0_HDR;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _OcclusionMap;
					uniform mediump float _OcclusionStrength;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec4 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump vec3 tmpvar_4;
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic));
					  mediump float tmpvar_6;
					  tmpvar_6 = (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w));
					  tmpvar_4 = (tmpvar_3 * tmpvar_6);
					  mediump vec3 tmpvar_7;
					  tmpvar_7 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_8;
					  tmpvar_8 = normalize(xlv_TEXCOORD1);
					  mediump vec3 tmpvar_9;
					  tmpvar_9 = _LightColor0.xyz;
					  mediump float occ_10;
					  lowp float tmpvar_11;
					  tmpvar_11 = texture2D (_OcclusionMap, xlv_TEXCOORD0.xy).y;
					  occ_10 = tmpvar_11;
					  mediump float tmpvar_12;
					  tmpvar_12 = ((1.0 - _OcclusionStrength) + (occ_10 * _OcclusionStrength));
					  highp vec4 tmpvar_13;
					  tmpvar_13 = unity_SpecCube0_HDR;
					  mediump float tmpvar_14;
					  tmpvar_14 = (1.0 - _Glossiness);
					  mediump vec3 tmpvar_15;
					  tmpvar_15 = (tmpvar_8 - (2.0 * (
					    dot (tmpvar_7, tmpvar_8)
					   * tmpvar_7)));
					  mediump vec4 tmpvar_16;
					  tmpvar_16.w = 1.0;
					  tmpvar_16.xyz = tmpvar_7;
					  mediump vec3 x_17;
					  x_17.x = dot (unity_SHAr, tmpvar_16);
					  x_17.y = dot (unity_SHAg, tmpvar_16);
					  x_17.z = dot (unity_SHAb, tmpvar_16);
					  mediump vec4 hdr_18;
					  hdr_18 = tmpvar_13;
					  mediump vec4 tmpvar_19;
					  tmpvar_19.xyz = tmpvar_15;
					  tmpvar_19.w = ((tmpvar_14 * (1.7 - 
					    (0.7 * tmpvar_14)
					  )) * 6.0);
					  lowp vec4 tmpvar_20;
					  tmpvar_20 = impl_low_textureCubeLodEXT (unity_SpecCube0, tmpvar_15, tmpvar_19.w);
					  mediump vec4 tmpvar_21;
					  tmpvar_21 = tmpvar_20;
					  mediump vec3 viewDir_22;
					  viewDir_22 = -(tmpvar_8);
					  mediump float specular_23;
					  mediump vec3 tmpvar_24;
					  mediump vec3 inVec_25;
					  inVec_25 = (_WorldSpaceLightPos0.xyz + viewDir_22);
					  tmpvar_24 = (inVec_25 * inversesqrt(max (0.001, 
					    dot (inVec_25, inVec_25)
					  )));
					  mediump float tmpvar_26;
					  tmpvar_26 = max (0.0, dot (_WorldSpaceLightPos0.xyz, tmpvar_24));
					  mediump float tmpvar_27;
					  tmpvar_27 = max (0.0001, (tmpvar_14 * tmpvar_14));
					  mediump float tmpvar_28;
					  tmpvar_28 = max (((2.0 / 
					    (tmpvar_27 * tmpvar_27)
					  ) - 2.0), 0.0001);
					  specular_23 = sqrt(max (0.0001, (
					    ((tmpvar_28 + 1.0) * pow (max (0.0, dot (tmpvar_7, tmpvar_24)), tmpvar_28))
					   / 
					    (((8.0 * (
					      ((tmpvar_26 * tmpvar_26) * _Glossiness)
					     + 
					      (tmpvar_14 * tmpvar_14)
					    )) * tmpvar_26) + 0.0001)
					  )));
					  mediump float tmpvar_29;
					  tmpvar_29 = clamp (specular_23, 0.0, 100.0);
					  specular_23 = tmpvar_29;
					  mediump float x_30;
					  x_30 = (1.0 - max (0.0, dot (tmpvar_7, viewDir_22)));
					  mediump vec3 tmpvar_31;
					  tmpvar_31 = (((
					    ((tmpvar_4 + (tmpvar_29 * tmpvar_5)) * tmpvar_9)
					   * 
					    max (0.0, dot (tmpvar_7, _WorldSpaceLightPos0.xyz))
					  ) + (
					    (max (((1.055 * 
					      pow (max (vec3(0.0, 0.0, 0.0), (xlv_TEXCOORD5.xyz + x_17)), vec3(0.4166667, 0.4166667, 0.4166667))
					    ) - 0.055), vec3(0.0, 0.0, 0.0)) * tmpvar_12)
					   * tmpvar_4)) + ((
					    (1.0 - ((tmpvar_14 * tmpvar_14) * (tmpvar_14 * 0.28)))
					   * 
					    (((hdr_18.x * tmpvar_21.w) * tmpvar_21.xyz) * tmpvar_12)
					  ) * mix (tmpvar_5, vec3(
					    clamp ((_Glossiness + (1.0 - tmpvar_6)), 0.0, 1.0)
					  ), vec3(
					    ((x_30 * x_30) * (x_30 * x_30))
					  ))));
					  mediump vec4 tmpvar_32;
					  tmpvar_32.w = 1.0;
					  tmpvar_32.xyz = tmpvar_31;
					  c_1.w = tmpvar_32.w;
					  c_1.xyz = tmpvar_31;
					  c_1.xyz = c_1.xyz;
					  mediump vec4 xlat_varoutput_33;
					  xlat_varoutput_33.xyz = c_1.xyz;
					  xlat_varoutput_33.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_33;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 unity_SHAr;
					uniform mediump vec4 unity_SHAg;
					uniform mediump vec4 unity_SHAb;
					uniform mediump vec4 unity_SHBr;
					uniform mediump vec4 unity_SHBg;
					uniform mediump vec4 unity_SHBb;
					uniform mediump vec4 unity_SHC;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec4 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec4 xlv_TEXCOORD4;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  mediump vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_10;
					  highp vec4 tmpvar_11;
					  tmpvar_11.w = 1.0;
					  tmpvar_11.xyz = _glesVertex.xyz;
					  tmpvar_10 = (glstate_matrix_mvp * tmpvar_11);
					  highp vec4 texcoord_12;
					  texcoord_12.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_13;
					  if ((_UVSec == 0.0)) {
					    tmpvar_13 = tmpvar_2;
					  } else {
					    tmpvar_13 = tmpvar_3;
					  };
					  texcoord_12.zw = ((tmpvar_13 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_9.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  normalWorld_4 = tmpvar_19;
					  tmpvar_8.xyz = normalWorld_4;
					  tmpvar_6.xyz = eyeVec_5;
					  mediump vec4 ambientOrLightmapUV_20;
					  ambientOrLightmapUV_20.w = 0.0;
					  mediump vec4 tmpvar_21;
					  tmpvar_21.w = 1.0;
					  tmpvar_21.xyz = normalWorld_4;
					  mediump vec3 res_22;
					  mediump vec3 x_23;
					  x_23.x = dot (unity_SHAr, tmpvar_21);
					  x_23.y = dot (unity_SHAg, tmpvar_21);
					  x_23.z = dot (unity_SHAb, tmpvar_21);
					  mediump vec3 x1_24;
					  mediump vec4 tmpvar_25;
					  tmpvar_25 = (normalWorld_4.xyzz * normalWorld_4.yzzx);
					  x1_24.x = dot (unity_SHBr, tmpvar_25);
					  x1_24.y = dot (unity_SHBg, tmpvar_25);
					  x1_24.z = dot (unity_SHBb, tmpvar_25);
					  res_22 = (x_23 + (x1_24 + (unity_SHC.xyz * 
					    ((normalWorld_4.x * normalWorld_4.x) - (normalWorld_4.y * normalWorld_4.y))
					  )));
					  res_22 = max (((1.055 * 
					    pow (max (res_22, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  ambientOrLightmapUV_20.xyz = max (vec3(0.0, 0.0, 0.0), res_22);
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  mediump float x_26;
					  x_26 = (1.0 - max (0.0, dot (normalWorld_4, 
					    -(eyeVec_5)
					  )));
					  tmpvar_8.w = ((x_26 * x_26) * (x_26 * x_26));
					  tmpvar_6.w = clamp ((_Glossiness + (1.0 - 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  )), 0.0, 1.0);
					  gl_Position = tmpvar_10;
					  xlv_TEXCOORD0 = texcoord_12;
					  xlv_TEXCOORD1 = tmpvar_6;
					  xlv_TEXCOORD2 = ambientOrLightmapUV_20;
					  xlv_TEXCOORD3 = (unity_WorldToShadow[0] * (unity_ObjectToWorld * _glesVertex));
					  xlv_TEXCOORD4 = tmpvar_7;
					  xlv_TEXCOORD5 = tmpvar_8;
					}
					
					
					#endif
					#ifdef FRAGMENT
					#extension GL_EXT_shader_texture_lod : enable
					lowp vec4 impl_low_textureCubeLodEXT(lowp samplerCube sampler, highp vec3 coord, mediump float lod)
					{
					#if defined(GL_EXT_shader_texture_lod)
						return textureCubeLodEXT(sampler, coord, lod);
					#else
						return textureCube(sampler, coord, lod);
					#endif
					}
					
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform mediump vec4 _LightShadowData;
					uniform lowp samplerCube unity_SpecCube0;
					uniform mediump vec4 unity_SpecCube0_HDR;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _OcclusionMap;
					uniform mediump float _OcclusionStrength;
					uniform highp sampler2D _ShadowMapTexture;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec4 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec4 xlv_TEXCOORD4;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump float rl_1;
					  mediump float atten_2;
					  lowp vec4 tmpvar_3;
					  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_4;
					  tmpvar_4 = (_Color.xyz * tmpvar_3.xyz);
					  mediump vec3 tmpvar_5;
					  mediump vec3 tmpvar_6;
					  tmpvar_6 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_4, vec3(_Metallic));
					  tmpvar_5 = (tmpvar_4 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)));
					  mediump vec3 tmpvar_7;
					  tmpvar_7 = _LightColor0.xyz;
					  lowp float tmpvar_8;
					  highp float lightShadowDataX_9;
					  mediump float tmpvar_10;
					  tmpvar_10 = _LightShadowData.x;
					  lightShadowDataX_9 = tmpvar_10;
					  highp float tmpvar_11;
					  tmpvar_11 = max (float((texture2D (_ShadowMapTexture, xlv_TEXCOORD3.xy).x > xlv_TEXCOORD3.z)), lightShadowDataX_9);
					  tmpvar_8 = tmpvar_11;
					  atten_2 = tmpvar_8;
					  mediump float occ_12;
					  lowp float tmpvar_13;
					  tmpvar_13 = texture2D (_OcclusionMap, xlv_TEXCOORD0.xy).y;
					  occ_12 = tmpvar_13;
					  mediump float tmpvar_14;
					  tmpvar_14 = ((1.0 - _OcclusionStrength) + (occ_12 * _OcclusionStrength));
					  rl_1 = dot (xlv_TEXCOORD4.yzw, _WorldSpaceLightPos0.xyz);
					  highp vec4 tmpvar_15;
					  tmpvar_15 = unity_SpecCube0_HDR;
					  mediump float tmpvar_16;
					  tmpvar_16 = (1.0 - _Glossiness);
					  mediump vec4 hdr_17;
					  hdr_17 = tmpvar_15;
					  mediump vec4 tmpvar_18;
					  tmpvar_18.xyz = xlv_TEXCOORD4.yzw;
					  tmpvar_18.w = ((tmpvar_16 * (1.7 - 
					    (0.7 * tmpvar_16)
					  )) * 6.0);
					  lowp vec4 tmpvar_19;
					  tmpvar_19 = impl_low_textureCubeLodEXT (unity_SpecCube0, xlv_TEXCOORD4.yzw, tmpvar_18.w);
					  mediump vec4 tmpvar_20;
					  tmpvar_20 = tmpvar_19;
					  mediump vec2 tmpvar_21;
					  tmpvar_21.x = ((rl_1 * rl_1) * (rl_1 * rl_1));
					  tmpvar_21.y = tmpvar_16;
					  lowp vec4 tmpvar_22;
					  tmpvar_22 = texture2D (unity_NHxRoughness, tmpvar_21);
					  mediump vec4 tmpvar_23;
					  tmpvar_23.w = 1.0;
					  tmpvar_23.xyz = (((
					    (xlv_TEXCOORD2.xyz * tmpvar_14)
					   * tmpvar_5) + (
					    (((hdr_17.x * tmpvar_20.w) * tmpvar_20.xyz) * tmpvar_14)
					   * 
					    mix (tmpvar_6, xlv_TEXCOORD1.www, xlv_TEXCOORD5.www)
					  )) + ((tmpvar_5 + 
					    ((tmpvar_22.w * 16.0) * tmpvar_6)
					  ) * (
					    (tmpvar_7 * atten_2)
					   * 
					    max (0.0, dot (xlv_TEXCOORD5.xyz, _WorldSpaceLightPos0.xyz))
					  )));
					  mediump vec4 xlat_varoutput_24;
					  xlat_varoutput_24.xyz = tmpvar_23.xyz;
					  xlat_varoutput_24.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_24;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 unity_SHBr;
					uniform mediump vec4 unity_SHBg;
					uniform mediump vec4 unity_SHBb;
					uniform mediump vec4 unity_SHC;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 n_13;
					  n_13 = (tmpvar_7.xyz - _WorldSpaceCameraPos);
					  tmpvar_12 = n_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  mediump vec3 normalWorld_19;
					  normalWorld_19 = tmpvar_18;
					  mediump vec4 ambientOrLightmapUV_20;
					  ambientOrLightmapUV_20.w = 0.0;
					  mediump vec3 x1_21;
					  mediump vec4 tmpvar_22;
					  tmpvar_22 = (normalWorld_19.xyzz * normalWorld_19.yzzx);
					  x1_21.x = dot (unity_SHBr, tmpvar_22);
					  x1_21.y = dot (unity_SHBg, tmpvar_22);
					  x1_21.z = dot (unity_SHBb, tmpvar_22);
					  ambientOrLightmapUV_20.xyz = (x1_21 + (unity_SHC.xyz * (
					    (normalWorld_19.x * normalWorld_19.x)
					   - 
					    (normalWorld_19.y * normalWorld_19.y)
					  )));
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					  xlv_TEXCOORD5 = ambientOrLightmapUV_20;
					  xlv_TEXCOORD6 = (unity_WorldToShadow[0] * (unity_ObjectToWorld * _glesVertex));
					}
					
					
					#endif
					#ifdef FRAGMENT
					#extension GL_EXT_shader_texture_lod : enable
					lowp vec4 impl_low_textureCubeLodEXT(lowp samplerCube sampler, highp vec3 coord, mediump float lod)
					{
					#if defined(GL_EXT_shader_texture_lod)
						return textureCubeLodEXT(sampler, coord, lod);
					#else
						return textureCube(sampler, coord, lod);
					#endif
					}
					
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform mediump vec4 unity_SHAr;
					uniform mediump vec4 unity_SHAg;
					uniform mediump vec4 unity_SHAb;
					uniform mediump vec4 _LightShadowData;
					uniform lowp samplerCube unity_SpecCube0;
					uniform mediump vec4 unity_SpecCube0_HDR;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _OcclusionMap;
					uniform mediump float _OcclusionStrength;
					uniform highp sampler2D _ShadowMapTexture;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec4 c_1;
					  mediump float atten_2;
					  lowp vec4 tmpvar_3;
					  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_4;
					  tmpvar_4 = (_Color.xyz * tmpvar_3.xyz);
					  mediump vec3 tmpvar_5;
					  mediump vec3 tmpvar_6;
					  tmpvar_6 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_4, vec3(_Metallic));
					  mediump float tmpvar_7;
					  tmpvar_7 = (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w));
					  tmpvar_5 = (tmpvar_4 * tmpvar_7);
					  mediump vec3 tmpvar_8;
					  tmpvar_8 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_9;
					  tmpvar_9 = normalize(xlv_TEXCOORD1);
					  mediump vec3 tmpvar_10;
					  tmpvar_10 = _LightColor0.xyz;
					  lowp float tmpvar_11;
					  highp float lightShadowDataX_12;
					  mediump float tmpvar_13;
					  tmpvar_13 = _LightShadowData.x;
					  lightShadowDataX_12 = tmpvar_13;
					  highp float tmpvar_14;
					  tmpvar_14 = max (float((texture2D (_ShadowMapTexture, xlv_TEXCOORD6.xy).x > xlv_TEXCOORD6.z)), lightShadowDataX_12);
					  tmpvar_11 = tmpvar_14;
					  atten_2 = tmpvar_11;
					  mediump float occ_15;
					  lowp float tmpvar_16;
					  tmpvar_16 = texture2D (_OcclusionMap, xlv_TEXCOORD0.xy).y;
					  occ_15 = tmpvar_16;
					  mediump float tmpvar_17;
					  tmpvar_17 = ((1.0 - _OcclusionStrength) + (occ_15 * _OcclusionStrength));
					  highp vec4 tmpvar_18;
					  tmpvar_18 = unity_SpecCube0_HDR;
					  mediump float tmpvar_19;
					  tmpvar_19 = (1.0 - _Glossiness);
					  mediump vec3 tmpvar_20;
					  tmpvar_20 = (tmpvar_9 - (2.0 * (
					    dot (tmpvar_8, tmpvar_9)
					   * tmpvar_8)));
					  mediump vec4 tmpvar_21;
					  tmpvar_21.w = 1.0;
					  tmpvar_21.xyz = tmpvar_8;
					  mediump vec3 x_22;
					  x_22.x = dot (unity_SHAr, tmpvar_21);
					  x_22.y = dot (unity_SHAg, tmpvar_21);
					  x_22.z = dot (unity_SHAb, tmpvar_21);
					  mediump vec4 hdr_23;
					  hdr_23 = tmpvar_18;
					  mediump vec4 tmpvar_24;
					  tmpvar_24.xyz = tmpvar_20;
					  tmpvar_24.w = ((tmpvar_19 * (1.7 - 
					    (0.7 * tmpvar_19)
					  )) * 6.0);
					  lowp vec4 tmpvar_25;
					  tmpvar_25 = impl_low_textureCubeLodEXT (unity_SpecCube0, tmpvar_20, tmpvar_24.w);
					  mediump vec4 tmpvar_26;
					  tmpvar_26 = tmpvar_25;
					  mediump vec3 viewDir_27;
					  viewDir_27 = -(tmpvar_9);
					  mediump float specular_28;
					  mediump vec3 tmpvar_29;
					  mediump vec3 inVec_30;
					  inVec_30 = (_WorldSpaceLightPos0.xyz + viewDir_27);
					  tmpvar_29 = (inVec_30 * inversesqrt(max (0.001, 
					    dot (inVec_30, inVec_30)
					  )));
					  mediump float tmpvar_31;
					  tmpvar_31 = max (0.0, dot (_WorldSpaceLightPos0.xyz, tmpvar_29));
					  mediump float tmpvar_32;
					  tmpvar_32 = max (0.0001, (tmpvar_19 * tmpvar_19));
					  mediump float tmpvar_33;
					  tmpvar_33 = max (((2.0 / 
					    (tmpvar_32 * tmpvar_32)
					  ) - 2.0), 0.0001);
					  specular_28 = sqrt(max (0.0001, (
					    ((tmpvar_33 + 1.0) * pow (max (0.0, dot (tmpvar_8, tmpvar_29)), tmpvar_33))
					   / 
					    (((8.0 * (
					      ((tmpvar_31 * tmpvar_31) * _Glossiness)
					     + 
					      (tmpvar_19 * tmpvar_19)
					    )) * tmpvar_31) + 0.0001)
					  )));
					  mediump float tmpvar_34;
					  tmpvar_34 = clamp (specular_28, 0.0, 100.0);
					  specular_28 = tmpvar_34;
					  mediump float x_35;
					  x_35 = (1.0 - max (0.0, dot (tmpvar_8, viewDir_27)));
					  mediump vec3 tmpvar_36;
					  tmpvar_36 = (((
					    ((tmpvar_5 + (tmpvar_34 * tmpvar_6)) * (tmpvar_10 * atten_2))
					   * 
					    max (0.0, dot (tmpvar_8, _WorldSpaceLightPos0.xyz))
					  ) + (
					    (max (((1.055 * 
					      pow (max (vec3(0.0, 0.0, 0.0), (xlv_TEXCOORD5.xyz + x_22)), vec3(0.4166667, 0.4166667, 0.4166667))
					    ) - 0.055), vec3(0.0, 0.0, 0.0)) * tmpvar_17)
					   * tmpvar_5)) + ((
					    (1.0 - ((tmpvar_19 * tmpvar_19) * (tmpvar_19 * 0.28)))
					   * 
					    (((hdr_23.x * tmpvar_26.w) * tmpvar_26.xyz) * tmpvar_17)
					  ) * mix (tmpvar_6, vec3(
					    clamp ((_Glossiness + (1.0 - tmpvar_7)), 0.0, 1.0)
					  ), vec3(
					    ((x_35 * x_35) * (x_35 * x_35))
					  ))));
					  mediump vec4 tmpvar_37;
					  tmpvar_37.w = 1.0;
					  tmpvar_37.xyz = tmpvar_36;
					  c_1.w = tmpvar_37.w;
					  c_1.xyz = tmpvar_36;
					  c_1.xyz = c_1.xyz;
					  mediump vec4 xlat_varoutput_38;
					  xlat_varoutput_38.xyz = c_1.xyz;
					  xlat_varoutput_38.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_38;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 unity_SHBr;
					uniform mediump vec4 unity_SHBg;
					uniform mediump vec4 unity_SHBb;
					uniform mediump vec4 unity_SHC;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 n_13;
					  n_13 = (tmpvar_7.xyz - _WorldSpaceCameraPos);
					  tmpvar_12 = n_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  mediump vec3 normalWorld_19;
					  normalWorld_19 = tmpvar_18;
					  mediump vec4 ambientOrLightmapUV_20;
					  ambientOrLightmapUV_20.w = 0.0;
					  mediump vec3 x1_21;
					  mediump vec4 tmpvar_22;
					  tmpvar_22 = (normalWorld_19.xyzz * normalWorld_19.yzzx);
					  x1_21.x = dot (unity_SHBr, tmpvar_22);
					  x1_21.y = dot (unity_SHBg, tmpvar_22);
					  x1_21.z = dot (unity_SHBb, tmpvar_22);
					  ambientOrLightmapUV_20.xyz = (x1_21 + (unity_SHC.xyz * (
					    (normalWorld_19.x * normalWorld_19.x)
					   - 
					    (normalWorld_19.y * normalWorld_19.y)
					  )));
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					  xlv_TEXCOORD5 = ambientOrLightmapUV_20;
					  xlv_TEXCOORD6 = (unity_WorldToShadow[0] * (unity_ObjectToWorld * _glesVertex));
					}
					
					
					#endif
					#ifdef FRAGMENT
					#extension GL_EXT_shader_texture_lod : enable
					lowp vec4 impl_low_textureCubeLodEXT(lowp samplerCube sampler, highp vec3 coord, mediump float lod)
					{
					#if defined(GL_EXT_shader_texture_lod)
						return textureCubeLodEXT(sampler, coord, lod);
					#else
						return textureCube(sampler, coord, lod);
					#endif
					}
					
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform mediump vec4 unity_SHAr;
					uniform mediump vec4 unity_SHAg;
					uniform mediump vec4 unity_SHAb;
					uniform mediump vec4 _LightShadowData;
					uniform lowp samplerCube unity_SpecCube0;
					uniform mediump vec4 unity_SpecCube0_HDR;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _OcclusionMap;
					uniform mediump float _OcclusionStrength;
					uniform highp sampler2D _ShadowMapTexture;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec4 c_1;
					  mediump float atten_2;
					  lowp vec4 tmpvar_3;
					  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_4;
					  tmpvar_4 = (_Color.xyz * tmpvar_3.xyz);
					  mediump vec3 tmpvar_5;
					  mediump vec3 tmpvar_6;
					  tmpvar_6 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_4, vec3(_Metallic));
					  mediump float tmpvar_7;
					  tmpvar_7 = (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w));
					  tmpvar_5 = (tmpvar_4 * tmpvar_7);
					  mediump vec3 tmpvar_8;
					  tmpvar_8 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_9;
					  tmpvar_9 = normalize(xlv_TEXCOORD1);
					  mediump vec3 tmpvar_10;
					  tmpvar_10 = _LightColor0.xyz;
					  lowp float tmpvar_11;
					  highp float lightShadowDataX_12;
					  mediump float tmpvar_13;
					  tmpvar_13 = _LightShadowData.x;
					  lightShadowDataX_12 = tmpvar_13;
					  highp float tmpvar_14;
					  tmpvar_14 = max (float((texture2D (_ShadowMapTexture, xlv_TEXCOORD6.xy).x > xlv_TEXCOORD6.z)), lightShadowDataX_12);
					  tmpvar_11 = tmpvar_14;
					  atten_2 = tmpvar_11;
					  mediump float occ_15;
					  lowp float tmpvar_16;
					  tmpvar_16 = texture2D (_OcclusionMap, xlv_TEXCOORD0.xy).y;
					  occ_15 = tmpvar_16;
					  mediump float tmpvar_17;
					  tmpvar_17 = ((1.0 - _OcclusionStrength) + (occ_15 * _OcclusionStrength));
					  highp vec4 tmpvar_18;
					  tmpvar_18 = unity_SpecCube0_HDR;
					  mediump float tmpvar_19;
					  tmpvar_19 = (1.0 - _Glossiness);
					  mediump vec3 tmpvar_20;
					  tmpvar_20 = (tmpvar_9 - (2.0 * (
					    dot (tmpvar_8, tmpvar_9)
					   * tmpvar_8)));
					  mediump vec4 tmpvar_21;
					  tmpvar_21.w = 1.0;
					  tmpvar_21.xyz = tmpvar_8;
					  mediump vec3 x_22;
					  x_22.x = dot (unity_SHAr, tmpvar_21);
					  x_22.y = dot (unity_SHAg, tmpvar_21);
					  x_22.z = dot (unity_SHAb, tmpvar_21);
					  mediump vec4 hdr_23;
					  hdr_23 = tmpvar_18;
					  mediump vec4 tmpvar_24;
					  tmpvar_24.xyz = tmpvar_20;
					  tmpvar_24.w = ((tmpvar_19 * (1.7 - 
					    (0.7 * tmpvar_19)
					  )) * 6.0);
					  lowp vec4 tmpvar_25;
					  tmpvar_25 = impl_low_textureCubeLodEXT (unity_SpecCube0, tmpvar_20, tmpvar_24.w);
					  mediump vec4 tmpvar_26;
					  tmpvar_26 = tmpvar_25;
					  mediump vec3 viewDir_27;
					  viewDir_27 = -(tmpvar_9);
					  mediump float specular_28;
					  mediump vec3 tmpvar_29;
					  mediump vec3 inVec_30;
					  inVec_30 = (_WorldSpaceLightPos0.xyz + viewDir_27);
					  tmpvar_29 = (inVec_30 * inversesqrt(max (0.001, 
					    dot (inVec_30, inVec_30)
					  )));
					  mediump float tmpvar_31;
					  tmpvar_31 = max (0.0, dot (_WorldSpaceLightPos0.xyz, tmpvar_29));
					  mediump float tmpvar_32;
					  tmpvar_32 = max (0.0001, (tmpvar_19 * tmpvar_19));
					  mediump float tmpvar_33;
					  tmpvar_33 = max (((2.0 / 
					    (tmpvar_32 * tmpvar_32)
					  ) - 2.0), 0.0001);
					  specular_28 = sqrt(max (0.0001, (
					    ((tmpvar_33 + 1.0) * pow (max (0.0, dot (tmpvar_8, tmpvar_29)), tmpvar_33))
					   / 
					    (((8.0 * (
					      ((tmpvar_31 * tmpvar_31) * _Glossiness)
					     + 
					      (tmpvar_19 * tmpvar_19)
					    )) * tmpvar_31) + 0.0001)
					  )));
					  mediump float tmpvar_34;
					  tmpvar_34 = clamp (specular_28, 0.0, 100.0);
					  specular_28 = tmpvar_34;
					  mediump float x_35;
					  x_35 = (1.0 - max (0.0, dot (tmpvar_8, viewDir_27)));
					  mediump vec3 tmpvar_36;
					  tmpvar_36 = (((
					    ((tmpvar_5 + (tmpvar_34 * tmpvar_6)) * (tmpvar_10 * atten_2))
					   * 
					    max (0.0, dot (tmpvar_8, _WorldSpaceLightPos0.xyz))
					  ) + (
					    (max (((1.055 * 
					      pow (max (vec3(0.0, 0.0, 0.0), (xlv_TEXCOORD5.xyz + x_22)), vec3(0.4166667, 0.4166667, 0.4166667))
					    ) - 0.055), vec3(0.0, 0.0, 0.0)) * tmpvar_17)
					   * tmpvar_5)) + ((
					    (1.0 - ((tmpvar_19 * tmpvar_19) * (tmpvar_19 * 0.28)))
					   * 
					    (((hdr_23.x * tmpvar_26.w) * tmpvar_26.xyz) * tmpvar_17)
					  ) * mix (tmpvar_6, vec3(
					    clamp ((_Glossiness + (1.0 - tmpvar_7)), 0.0, 1.0)
					  ), vec3(
					    ((x_35 * x_35) * (x_35 * x_35))
					  ))));
					  mediump vec4 tmpvar_37;
					  tmpvar_37.w = 1.0;
					  tmpvar_37.xyz = tmpvar_36;
					  c_1.w = tmpvar_37.w;
					  c_1.xyz = tmpvar_36;
					  c_1.xyz = c_1.xyz;
					  mediump vec4 xlat_varoutput_38;
					  xlat_varoutput_38.xyz = c_1.xyz;
					  xlat_varoutput_38.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_38;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 unity_4LightPosX0;
					uniform highp vec4 unity_4LightPosY0;
					uniform highp vec4 unity_4LightPosZ0;
					uniform mediump vec4 unity_4LightAtten0;
					uniform mediump vec4 unity_LightColor[8];
					uniform mediump vec4 unity_SHAr;
					uniform mediump vec4 unity_SHAg;
					uniform mediump vec4 unity_SHAb;
					uniform mediump vec4 unity_SHBr;
					uniform mediump vec4 unity_SHBg;
					uniform mediump vec4 unity_SHBb;
					uniform mediump vec4 unity_SHC;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec4 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD4;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  mediump vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_10;
					  highp vec4 tmpvar_11;
					  tmpvar_11.w = 1.0;
					  tmpvar_11.xyz = _glesVertex.xyz;
					  tmpvar_10 = (glstate_matrix_mvp * tmpvar_11);
					  highp vec4 texcoord_12;
					  texcoord_12.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_13;
					  if ((_UVSec == 0.0)) {
					    tmpvar_13 = tmpvar_2;
					  } else {
					    tmpvar_13 = tmpvar_3;
					  };
					  texcoord_12.zw = ((tmpvar_13 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_9.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  normalWorld_4 = tmpvar_19;
					  tmpvar_8.xyz = normalWorld_4;
					  tmpvar_6.xyz = eyeVec_5;
					  mediump vec4 ambientOrLightmapUV_20;
					  ambientOrLightmapUV_20.w = 0.0;
					  highp vec3 lightColor0_21;
					  lightColor0_21 = unity_LightColor[0].xyz;
					  highp vec3 lightColor1_22;
					  lightColor1_22 = unity_LightColor[1].xyz;
					  highp vec3 lightColor2_23;
					  lightColor2_23 = unity_LightColor[2].xyz;
					  highp vec3 lightColor3_24;
					  lightColor3_24 = unity_LightColor[3].xyz;
					  highp vec4 lightAttenSq_25;
					  lightAttenSq_25 = unity_4LightAtten0;
					  highp vec3 normal_26;
					  normal_26 = normalWorld_4;
					  highp vec3 col_27;
					  highp vec4 ndotl_28;
					  highp vec4 lengthSq_29;
					  highp vec4 tmpvar_30;
					  tmpvar_30 = (unity_4LightPosX0 - tmpvar_9.x);
					  highp vec4 tmpvar_31;
					  tmpvar_31 = (unity_4LightPosY0 - tmpvar_9.y);
					  highp vec4 tmpvar_32;
					  tmpvar_32 = (unity_4LightPosZ0 - tmpvar_9.z);
					  lengthSq_29 = (tmpvar_30 * tmpvar_30);
					  lengthSq_29 = (lengthSq_29 + (tmpvar_31 * tmpvar_31));
					  lengthSq_29 = (lengthSq_29 + (tmpvar_32 * tmpvar_32));
					  ndotl_28 = (tmpvar_30 * normal_26.x);
					  ndotl_28 = (ndotl_28 + (tmpvar_31 * normal_26.y));
					  ndotl_28 = (ndotl_28 + (tmpvar_32 * normal_26.z));
					  highp vec4 tmpvar_33;
					  tmpvar_33 = max (vec4(0.0, 0.0, 0.0, 0.0), (ndotl_28 * inversesqrt(lengthSq_29)));
					  ndotl_28 = tmpvar_33;
					  highp vec4 tmpvar_34;
					  tmpvar_34 = (tmpvar_33 * (1.0/((1.0 + 
					    (lengthSq_29 * lightAttenSq_25)
					  ))));
					  col_27 = (lightColor0_21 * tmpvar_34.x);
					  col_27 = (col_27 + (lightColor1_22 * tmpvar_34.y));
					  col_27 = (col_27 + (lightColor2_23 * tmpvar_34.z));
					  col_27 = (col_27 + (lightColor3_24 * tmpvar_34.w));
					  ambientOrLightmapUV_20.xyz = col_27;
					  mediump vec4 tmpvar_35;
					  tmpvar_35.w = 1.0;
					  tmpvar_35.xyz = normalWorld_4;
					  mediump vec3 res_36;
					  mediump vec3 x_37;
					  x_37.x = dot (unity_SHAr, tmpvar_35);
					  x_37.y = dot (unity_SHAg, tmpvar_35);
					  x_37.z = dot (unity_SHAb, tmpvar_35);
					  mediump vec3 x1_38;
					  mediump vec4 tmpvar_39;
					  tmpvar_39 = (normalWorld_4.xyzz * normalWorld_4.yzzx);
					  x1_38.x = dot (unity_SHBr, tmpvar_39);
					  x1_38.y = dot (unity_SHBg, tmpvar_39);
					  x1_38.z = dot (unity_SHBb, tmpvar_39);
					  res_36 = (x_37 + (x1_38 + (unity_SHC.xyz * 
					    ((normalWorld_4.x * normalWorld_4.x) - (normalWorld_4.y * normalWorld_4.y))
					  )));
					  res_36 = max (((1.055 * 
					    pow (max (res_36, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  ambientOrLightmapUV_20.xyz = (ambientOrLightmapUV_20.xyz + max (vec3(0.0, 0.0, 0.0), res_36));
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  mediump float x_40;
					  x_40 = (1.0 - max (0.0, dot (normalWorld_4, 
					    -(eyeVec_5)
					  )));
					  tmpvar_8.w = ((x_40 * x_40) * (x_40 * x_40));
					  tmpvar_6.w = clamp ((_Glossiness + (1.0 - 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  )), 0.0, 1.0);
					  gl_Position = tmpvar_10;
					  xlv_TEXCOORD0 = texcoord_12;
					  xlv_TEXCOORD1 = tmpvar_6;
					  xlv_TEXCOORD2 = ambientOrLightmapUV_20;
					  xlv_TEXCOORD4 = tmpvar_7;
					  xlv_TEXCOORD5 = tmpvar_8;
					}
					
					
					#endif
					#ifdef FRAGMENT
					#extension GL_EXT_shader_texture_lod : enable
					lowp vec4 impl_low_textureCubeLodEXT(lowp samplerCube sampler, highp vec3 coord, mediump float lod)
					{
					#if defined(GL_EXT_shader_texture_lod)
						return textureCubeLodEXT(sampler, coord, lod);
					#else
						return textureCube(sampler, coord, lod);
					#endif
					}
					
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform lowp samplerCube unity_SpecCube0;
					uniform mediump vec4 unity_SpecCube0_HDR;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _OcclusionMap;
					uniform mediump float _OcclusionStrength;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec4 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD4;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump float rl_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump vec3 tmpvar_4;
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic));
					  tmpvar_4 = (tmpvar_3 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)));
					  mediump vec3 tmpvar_6;
					  tmpvar_6 = _LightColor0.xyz;
					  mediump float occ_7;
					  lowp float tmpvar_8;
					  tmpvar_8 = texture2D (_OcclusionMap, xlv_TEXCOORD0.xy).y;
					  occ_7 = tmpvar_8;
					  mediump float tmpvar_9;
					  tmpvar_9 = ((1.0 - _OcclusionStrength) + (occ_7 * _OcclusionStrength));
					  rl_1 = dot (xlv_TEXCOORD4.yzw, _WorldSpaceLightPos0.xyz);
					  highp vec4 tmpvar_10;
					  tmpvar_10 = unity_SpecCube0_HDR;
					  mediump float tmpvar_11;
					  tmpvar_11 = (1.0 - _Glossiness);
					  mediump vec4 hdr_12;
					  hdr_12 = tmpvar_10;
					  mediump vec4 tmpvar_13;
					  tmpvar_13.xyz = xlv_TEXCOORD4.yzw;
					  tmpvar_13.w = ((tmpvar_11 * (1.7 - 
					    (0.7 * tmpvar_11)
					  )) * 6.0);
					  lowp vec4 tmpvar_14;
					  tmpvar_14 = impl_low_textureCubeLodEXT (unity_SpecCube0, xlv_TEXCOORD4.yzw, tmpvar_13.w);
					  mediump vec4 tmpvar_15;
					  tmpvar_15 = tmpvar_14;
					  mediump vec2 tmpvar_16;
					  tmpvar_16.x = ((rl_1 * rl_1) * (rl_1 * rl_1));
					  tmpvar_16.y = tmpvar_11;
					  lowp vec4 tmpvar_17;
					  tmpvar_17 = texture2D (unity_NHxRoughness, tmpvar_16);
					  mediump vec4 tmpvar_18;
					  tmpvar_18.w = 1.0;
					  tmpvar_18.xyz = (((
					    (xlv_TEXCOORD2.xyz * tmpvar_9)
					   * tmpvar_4) + (
					    (((hdr_12.x * tmpvar_15.w) * tmpvar_15.xyz) * tmpvar_9)
					   * 
					    mix (tmpvar_5, xlv_TEXCOORD1.www, xlv_TEXCOORD5.www)
					  )) + ((tmpvar_4 + 
					    ((tmpvar_17.w * 16.0) * tmpvar_5)
					  ) * (tmpvar_6 * 
					    max (0.0, dot (xlv_TEXCOORD5.xyz, _WorldSpaceLightPos0.xyz))
					  )));
					  mediump vec4 xlat_varoutput_19;
					  xlat_varoutput_19.xyz = tmpvar_18.xyz;
					  xlat_varoutput_19.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_19;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 unity_4LightPosX0;
					uniform highp vec4 unity_4LightPosY0;
					uniform highp vec4 unity_4LightPosZ0;
					uniform mediump vec4 unity_4LightAtten0;
					uniform mediump vec4 unity_LightColor[8];
					uniform mediump vec4 unity_SHBr;
					uniform mediump vec4 unity_SHBg;
					uniform mediump vec4 unity_SHBb;
					uniform mediump vec4 unity_SHC;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 n_13;
					  n_13 = (tmpvar_7.xyz - _WorldSpaceCameraPos);
					  tmpvar_12 = n_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  mediump vec3 normalWorld_19;
					  normalWorld_19 = tmpvar_18;
					  mediump vec4 ambientOrLightmapUV_20;
					  ambientOrLightmapUV_20.w = 0.0;
					  highp vec3 lightColor0_21;
					  lightColor0_21 = unity_LightColor[0].xyz;
					  highp vec3 lightColor1_22;
					  lightColor1_22 = unity_LightColor[1].xyz;
					  highp vec3 lightColor2_23;
					  lightColor2_23 = unity_LightColor[2].xyz;
					  highp vec3 lightColor3_24;
					  lightColor3_24 = unity_LightColor[3].xyz;
					  highp vec4 lightAttenSq_25;
					  lightAttenSq_25 = unity_4LightAtten0;
					  highp vec3 normal_26;
					  normal_26 = normalWorld_19;
					  highp vec3 col_27;
					  highp vec4 ndotl_28;
					  highp vec4 lengthSq_29;
					  highp vec4 tmpvar_30;
					  tmpvar_30 = (unity_4LightPosX0 - tmpvar_7.x);
					  highp vec4 tmpvar_31;
					  tmpvar_31 = (unity_4LightPosY0 - tmpvar_7.y);
					  highp vec4 tmpvar_32;
					  tmpvar_32 = (unity_4LightPosZ0 - tmpvar_7.z);
					  lengthSq_29 = (tmpvar_30 * tmpvar_30);
					  lengthSq_29 = (lengthSq_29 + (tmpvar_31 * tmpvar_31));
					  lengthSq_29 = (lengthSq_29 + (tmpvar_32 * tmpvar_32));
					  ndotl_28 = (tmpvar_30 * normal_26.x);
					  ndotl_28 = (ndotl_28 + (tmpvar_31 * normal_26.y));
					  ndotl_28 = (ndotl_28 + (tmpvar_32 * normal_26.z));
					  highp vec4 tmpvar_33;
					  tmpvar_33 = max (vec4(0.0, 0.0, 0.0, 0.0), (ndotl_28 * inversesqrt(lengthSq_29)));
					  ndotl_28 = tmpvar_33;
					  highp vec4 tmpvar_34;
					  tmpvar_34 = (tmpvar_33 * (1.0/((1.0 + 
					    (lengthSq_29 * lightAttenSq_25)
					  ))));
					  col_27 = (lightColor0_21 * tmpvar_34.x);
					  col_27 = (col_27 + (lightColor1_22 * tmpvar_34.y));
					  col_27 = (col_27 + (lightColor2_23 * tmpvar_34.z));
					  col_27 = (col_27 + (lightColor3_24 * tmpvar_34.w));
					  ambientOrLightmapUV_20.xyz = col_27;
					  mediump vec3 ambient_35;
					  ambient_35 = (ambientOrLightmapUV_20.xyz * ((ambientOrLightmapUV_20.xyz * 
					    ((ambientOrLightmapUV_20.xyz * 0.305306) + 0.6821711)
					  ) + 0.01252288));
					  mediump vec3 x1_36;
					  mediump vec4 tmpvar_37;
					  tmpvar_37 = (normalWorld_19.xyzz * normalWorld_19.yzzx);
					  x1_36.x = dot (unity_SHBr, tmpvar_37);
					  x1_36.y = dot (unity_SHBg, tmpvar_37);
					  x1_36.z = dot (unity_SHBb, tmpvar_37);
					  ambient_35 = (ambient_35 + (x1_36 + (unity_SHC.xyz * 
					    ((normalWorld_19.x * normalWorld_19.x) - (normalWorld_19.y * normalWorld_19.y))
					  )));
					  ambientOrLightmapUV_20.xyz = ambient_35;
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					  xlv_TEXCOORD5 = ambientOrLightmapUV_20;
					}
					
					
					#endif
					#ifdef FRAGMENT
					#extension GL_EXT_shader_texture_lod : enable
					lowp vec4 impl_low_textureCubeLodEXT(lowp samplerCube sampler, highp vec3 coord, mediump float lod)
					{
					#if defined(GL_EXT_shader_texture_lod)
						return textureCubeLodEXT(sampler, coord, lod);
					#else
						return textureCube(sampler, coord, lod);
					#endif
					}
					
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform mediump vec4 unity_SHAr;
					uniform mediump vec4 unity_SHAg;
					uniform mediump vec4 unity_SHAb;
					uniform lowp samplerCube unity_SpecCube0;
					uniform mediump vec4 unity_SpecCube0_HDR;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _OcclusionMap;
					uniform mediump float _OcclusionStrength;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec4 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump vec3 tmpvar_4;
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic));
					  mediump float tmpvar_6;
					  tmpvar_6 = (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w));
					  tmpvar_4 = (tmpvar_3 * tmpvar_6);
					  mediump vec3 tmpvar_7;
					  tmpvar_7 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_8;
					  tmpvar_8 = normalize(xlv_TEXCOORD1);
					  mediump vec3 tmpvar_9;
					  tmpvar_9 = _LightColor0.xyz;
					  mediump float occ_10;
					  lowp float tmpvar_11;
					  tmpvar_11 = texture2D (_OcclusionMap, xlv_TEXCOORD0.xy).y;
					  occ_10 = tmpvar_11;
					  mediump float tmpvar_12;
					  tmpvar_12 = ((1.0 - _OcclusionStrength) + (occ_10 * _OcclusionStrength));
					  highp vec4 tmpvar_13;
					  tmpvar_13 = unity_SpecCube0_HDR;
					  mediump float tmpvar_14;
					  tmpvar_14 = (1.0 - _Glossiness);
					  mediump vec3 tmpvar_15;
					  tmpvar_15 = (tmpvar_8 - (2.0 * (
					    dot (tmpvar_7, tmpvar_8)
					   * tmpvar_7)));
					  mediump vec4 tmpvar_16;
					  tmpvar_16.w = 1.0;
					  tmpvar_16.xyz = tmpvar_7;
					  mediump vec3 x_17;
					  x_17.x = dot (unity_SHAr, tmpvar_16);
					  x_17.y = dot (unity_SHAg, tmpvar_16);
					  x_17.z = dot (unity_SHAb, tmpvar_16);
					  mediump vec4 hdr_18;
					  hdr_18 = tmpvar_13;
					  mediump vec4 tmpvar_19;
					  tmpvar_19.xyz = tmpvar_15;
					  tmpvar_19.w = ((tmpvar_14 * (1.7 - 
					    (0.7 * tmpvar_14)
					  )) * 6.0);
					  lowp vec4 tmpvar_20;
					  tmpvar_20 = impl_low_textureCubeLodEXT (unity_SpecCube0, tmpvar_15, tmpvar_19.w);
					  mediump vec4 tmpvar_21;
					  tmpvar_21 = tmpvar_20;
					  mediump vec3 viewDir_22;
					  viewDir_22 = -(tmpvar_8);
					  mediump float specular_23;
					  mediump vec3 tmpvar_24;
					  mediump vec3 inVec_25;
					  inVec_25 = (_WorldSpaceLightPos0.xyz + viewDir_22);
					  tmpvar_24 = (inVec_25 * inversesqrt(max (0.001, 
					    dot (inVec_25, inVec_25)
					  )));
					  mediump float tmpvar_26;
					  tmpvar_26 = max (0.0, dot (_WorldSpaceLightPos0.xyz, tmpvar_24));
					  mediump float tmpvar_27;
					  tmpvar_27 = max (0.0001, (tmpvar_14 * tmpvar_14));
					  mediump float tmpvar_28;
					  tmpvar_28 = max (((2.0 / 
					    (tmpvar_27 * tmpvar_27)
					  ) - 2.0), 0.0001);
					  specular_23 = sqrt(max (0.0001, (
					    ((tmpvar_28 + 1.0) * pow (max (0.0, dot (tmpvar_7, tmpvar_24)), tmpvar_28))
					   / 
					    (((8.0 * (
					      ((tmpvar_26 * tmpvar_26) * _Glossiness)
					     + 
					      (tmpvar_14 * tmpvar_14)
					    )) * tmpvar_26) + 0.0001)
					  )));
					  mediump float tmpvar_29;
					  tmpvar_29 = clamp (specular_23, 0.0, 100.0);
					  specular_23 = tmpvar_29;
					  mediump float x_30;
					  x_30 = (1.0 - max (0.0, dot (tmpvar_7, viewDir_22)));
					  mediump vec3 tmpvar_31;
					  tmpvar_31 = (((
					    ((tmpvar_4 + (tmpvar_29 * tmpvar_5)) * tmpvar_9)
					   * 
					    max (0.0, dot (tmpvar_7, _WorldSpaceLightPos0.xyz))
					  ) + (
					    (max (((1.055 * 
					      pow (max (vec3(0.0, 0.0, 0.0), (xlv_TEXCOORD5.xyz + x_17)), vec3(0.4166667, 0.4166667, 0.4166667))
					    ) - 0.055), vec3(0.0, 0.0, 0.0)) * tmpvar_12)
					   * tmpvar_4)) + ((
					    (1.0 - ((tmpvar_14 * tmpvar_14) * (tmpvar_14 * 0.28)))
					   * 
					    (((hdr_18.x * tmpvar_21.w) * tmpvar_21.xyz) * tmpvar_12)
					  ) * mix (tmpvar_5, vec3(
					    clamp ((_Glossiness + (1.0 - tmpvar_6)), 0.0, 1.0)
					  ), vec3(
					    ((x_30 * x_30) * (x_30 * x_30))
					  ))));
					  mediump vec4 tmpvar_32;
					  tmpvar_32.w = 1.0;
					  tmpvar_32.xyz = tmpvar_31;
					  c_1.w = tmpvar_32.w;
					  c_1.xyz = tmpvar_31;
					  c_1.xyz = c_1.xyz;
					  mediump vec4 xlat_varoutput_33;
					  xlat_varoutput_33.xyz = c_1.xyz;
					  xlat_varoutput_33.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_33;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 unity_4LightPosX0;
					uniform highp vec4 unity_4LightPosY0;
					uniform highp vec4 unity_4LightPosZ0;
					uniform mediump vec4 unity_4LightAtten0;
					uniform mediump vec4 unity_LightColor[8];
					uniform mediump vec4 unity_SHBr;
					uniform mediump vec4 unity_SHBg;
					uniform mediump vec4 unity_SHBb;
					uniform mediump vec4 unity_SHC;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 n_13;
					  n_13 = (tmpvar_7.xyz - _WorldSpaceCameraPos);
					  tmpvar_12 = n_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  mediump vec3 normalWorld_19;
					  normalWorld_19 = tmpvar_18;
					  mediump vec4 ambientOrLightmapUV_20;
					  ambientOrLightmapUV_20.w = 0.0;
					  highp vec3 lightColor0_21;
					  lightColor0_21 = unity_LightColor[0].xyz;
					  highp vec3 lightColor1_22;
					  lightColor1_22 = unity_LightColor[1].xyz;
					  highp vec3 lightColor2_23;
					  lightColor2_23 = unity_LightColor[2].xyz;
					  highp vec3 lightColor3_24;
					  lightColor3_24 = unity_LightColor[3].xyz;
					  highp vec4 lightAttenSq_25;
					  lightAttenSq_25 = unity_4LightAtten0;
					  highp vec3 normal_26;
					  normal_26 = normalWorld_19;
					  highp vec3 col_27;
					  highp vec4 ndotl_28;
					  highp vec4 lengthSq_29;
					  highp vec4 tmpvar_30;
					  tmpvar_30 = (unity_4LightPosX0 - tmpvar_7.x);
					  highp vec4 tmpvar_31;
					  tmpvar_31 = (unity_4LightPosY0 - tmpvar_7.y);
					  highp vec4 tmpvar_32;
					  tmpvar_32 = (unity_4LightPosZ0 - tmpvar_7.z);
					  lengthSq_29 = (tmpvar_30 * tmpvar_30);
					  lengthSq_29 = (lengthSq_29 + (tmpvar_31 * tmpvar_31));
					  lengthSq_29 = (lengthSq_29 + (tmpvar_32 * tmpvar_32));
					  ndotl_28 = (tmpvar_30 * normal_26.x);
					  ndotl_28 = (ndotl_28 + (tmpvar_31 * normal_26.y));
					  ndotl_28 = (ndotl_28 + (tmpvar_32 * normal_26.z));
					  highp vec4 tmpvar_33;
					  tmpvar_33 = max (vec4(0.0, 0.0, 0.0, 0.0), (ndotl_28 * inversesqrt(lengthSq_29)));
					  ndotl_28 = tmpvar_33;
					  highp vec4 tmpvar_34;
					  tmpvar_34 = (tmpvar_33 * (1.0/((1.0 + 
					    (lengthSq_29 * lightAttenSq_25)
					  ))));
					  col_27 = (lightColor0_21 * tmpvar_34.x);
					  col_27 = (col_27 + (lightColor1_22 * tmpvar_34.y));
					  col_27 = (col_27 + (lightColor2_23 * tmpvar_34.z));
					  col_27 = (col_27 + (lightColor3_24 * tmpvar_34.w));
					  ambientOrLightmapUV_20.xyz = col_27;
					  mediump vec3 ambient_35;
					  ambient_35 = (ambientOrLightmapUV_20.xyz * ((ambientOrLightmapUV_20.xyz * 
					    ((ambientOrLightmapUV_20.xyz * 0.305306) + 0.6821711)
					  ) + 0.01252288));
					  mediump vec3 x1_36;
					  mediump vec4 tmpvar_37;
					  tmpvar_37 = (normalWorld_19.xyzz * normalWorld_19.yzzx);
					  x1_36.x = dot (unity_SHBr, tmpvar_37);
					  x1_36.y = dot (unity_SHBg, tmpvar_37);
					  x1_36.z = dot (unity_SHBb, tmpvar_37);
					  ambient_35 = (ambient_35 + (x1_36 + (unity_SHC.xyz * 
					    ((normalWorld_19.x * normalWorld_19.x) - (normalWorld_19.y * normalWorld_19.y))
					  )));
					  ambientOrLightmapUV_20.xyz = ambient_35;
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					  xlv_TEXCOORD5 = ambientOrLightmapUV_20;
					}
					
					
					#endif
					#ifdef FRAGMENT
					#extension GL_EXT_shader_texture_lod : enable
					lowp vec4 impl_low_textureCubeLodEXT(lowp samplerCube sampler, highp vec3 coord, mediump float lod)
					{
					#if defined(GL_EXT_shader_texture_lod)
						return textureCubeLodEXT(sampler, coord, lod);
					#else
						return textureCube(sampler, coord, lod);
					#endif
					}
					
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform mediump vec4 unity_SHAr;
					uniform mediump vec4 unity_SHAg;
					uniform mediump vec4 unity_SHAb;
					uniform lowp samplerCube unity_SpecCube0;
					uniform mediump vec4 unity_SpecCube0_HDR;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _OcclusionMap;
					uniform mediump float _OcclusionStrength;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec4 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump vec3 tmpvar_4;
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic));
					  mediump float tmpvar_6;
					  tmpvar_6 = (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w));
					  tmpvar_4 = (tmpvar_3 * tmpvar_6);
					  mediump vec3 tmpvar_7;
					  tmpvar_7 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_8;
					  tmpvar_8 = normalize(xlv_TEXCOORD1);
					  mediump vec3 tmpvar_9;
					  tmpvar_9 = _LightColor0.xyz;
					  mediump float occ_10;
					  lowp float tmpvar_11;
					  tmpvar_11 = texture2D (_OcclusionMap, xlv_TEXCOORD0.xy).y;
					  occ_10 = tmpvar_11;
					  mediump float tmpvar_12;
					  tmpvar_12 = ((1.0 - _OcclusionStrength) + (occ_10 * _OcclusionStrength));
					  highp vec4 tmpvar_13;
					  tmpvar_13 = unity_SpecCube0_HDR;
					  mediump float tmpvar_14;
					  tmpvar_14 = (1.0 - _Glossiness);
					  mediump vec3 tmpvar_15;
					  tmpvar_15 = (tmpvar_8 - (2.0 * (
					    dot (tmpvar_7, tmpvar_8)
					   * tmpvar_7)));
					  mediump vec4 tmpvar_16;
					  tmpvar_16.w = 1.0;
					  tmpvar_16.xyz = tmpvar_7;
					  mediump vec3 x_17;
					  x_17.x = dot (unity_SHAr, tmpvar_16);
					  x_17.y = dot (unity_SHAg, tmpvar_16);
					  x_17.z = dot (unity_SHAb, tmpvar_16);
					  mediump vec4 hdr_18;
					  hdr_18 = tmpvar_13;
					  mediump vec4 tmpvar_19;
					  tmpvar_19.xyz = tmpvar_15;
					  tmpvar_19.w = ((tmpvar_14 * (1.7 - 
					    (0.7 * tmpvar_14)
					  )) * 6.0);
					  lowp vec4 tmpvar_20;
					  tmpvar_20 = impl_low_textureCubeLodEXT (unity_SpecCube0, tmpvar_15, tmpvar_19.w);
					  mediump vec4 tmpvar_21;
					  tmpvar_21 = tmpvar_20;
					  mediump vec3 viewDir_22;
					  viewDir_22 = -(tmpvar_8);
					  mediump float specular_23;
					  mediump vec3 tmpvar_24;
					  mediump vec3 inVec_25;
					  inVec_25 = (_WorldSpaceLightPos0.xyz + viewDir_22);
					  tmpvar_24 = (inVec_25 * inversesqrt(max (0.001, 
					    dot (inVec_25, inVec_25)
					  )));
					  mediump float tmpvar_26;
					  tmpvar_26 = max (0.0, dot (_WorldSpaceLightPos0.xyz, tmpvar_24));
					  mediump float tmpvar_27;
					  tmpvar_27 = max (0.0001, (tmpvar_14 * tmpvar_14));
					  mediump float tmpvar_28;
					  tmpvar_28 = max (((2.0 / 
					    (tmpvar_27 * tmpvar_27)
					  ) - 2.0), 0.0001);
					  specular_23 = sqrt(max (0.0001, (
					    ((tmpvar_28 + 1.0) * pow (max (0.0, dot (tmpvar_7, tmpvar_24)), tmpvar_28))
					   / 
					    (((8.0 * (
					      ((tmpvar_26 * tmpvar_26) * _Glossiness)
					     + 
					      (tmpvar_14 * tmpvar_14)
					    )) * tmpvar_26) + 0.0001)
					  )));
					  mediump float tmpvar_29;
					  tmpvar_29 = clamp (specular_23, 0.0, 100.0);
					  specular_23 = tmpvar_29;
					  mediump float x_30;
					  x_30 = (1.0 - max (0.0, dot (tmpvar_7, viewDir_22)));
					  mediump vec3 tmpvar_31;
					  tmpvar_31 = (((
					    ((tmpvar_4 + (tmpvar_29 * tmpvar_5)) * tmpvar_9)
					   * 
					    max (0.0, dot (tmpvar_7, _WorldSpaceLightPos0.xyz))
					  ) + (
					    (max (((1.055 * 
					      pow (max (vec3(0.0, 0.0, 0.0), (xlv_TEXCOORD5.xyz + x_17)), vec3(0.4166667, 0.4166667, 0.4166667))
					    ) - 0.055), vec3(0.0, 0.0, 0.0)) * tmpvar_12)
					   * tmpvar_4)) + ((
					    (1.0 - ((tmpvar_14 * tmpvar_14) * (tmpvar_14 * 0.28)))
					   * 
					    (((hdr_18.x * tmpvar_21.w) * tmpvar_21.xyz) * tmpvar_12)
					  ) * mix (tmpvar_5, vec3(
					    clamp ((_Glossiness + (1.0 - tmpvar_6)), 0.0, 1.0)
					  ), vec3(
					    ((x_30 * x_30) * (x_30 * x_30))
					  ))));
					  mediump vec4 tmpvar_32;
					  tmpvar_32.w = 1.0;
					  tmpvar_32.xyz = tmpvar_31;
					  c_1.w = tmpvar_32.w;
					  c_1.xyz = tmpvar_31;
					  c_1.xyz = c_1.xyz;
					  mediump vec4 xlat_varoutput_33;
					  xlat_varoutput_33.xyz = c_1.xyz;
					  xlat_varoutput_33.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_33;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 unity_4LightPosX0;
					uniform highp vec4 unity_4LightPosY0;
					uniform highp vec4 unity_4LightPosZ0;
					uniform mediump vec4 unity_4LightAtten0;
					uniform mediump vec4 unity_LightColor[8];
					uniform mediump vec4 unity_SHAr;
					uniform mediump vec4 unity_SHAg;
					uniform mediump vec4 unity_SHAb;
					uniform mediump vec4 unity_SHBr;
					uniform mediump vec4 unity_SHBg;
					uniform mediump vec4 unity_SHBb;
					uniform mediump vec4 unity_SHC;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec4 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec4 xlv_TEXCOORD4;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  mediump vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_10;
					  highp vec4 tmpvar_11;
					  tmpvar_11.w = 1.0;
					  tmpvar_11.xyz = _glesVertex.xyz;
					  tmpvar_10 = (glstate_matrix_mvp * tmpvar_11);
					  highp vec4 texcoord_12;
					  texcoord_12.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_13;
					  if ((_UVSec == 0.0)) {
					    tmpvar_13 = tmpvar_2;
					  } else {
					    tmpvar_13 = tmpvar_3;
					  };
					  texcoord_12.zw = ((tmpvar_13 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_9.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  normalWorld_4 = tmpvar_19;
					  tmpvar_8.xyz = normalWorld_4;
					  tmpvar_6.xyz = eyeVec_5;
					  mediump vec4 ambientOrLightmapUV_20;
					  ambientOrLightmapUV_20.w = 0.0;
					  highp vec3 lightColor0_21;
					  lightColor0_21 = unity_LightColor[0].xyz;
					  highp vec3 lightColor1_22;
					  lightColor1_22 = unity_LightColor[1].xyz;
					  highp vec3 lightColor2_23;
					  lightColor2_23 = unity_LightColor[2].xyz;
					  highp vec3 lightColor3_24;
					  lightColor3_24 = unity_LightColor[3].xyz;
					  highp vec4 lightAttenSq_25;
					  lightAttenSq_25 = unity_4LightAtten0;
					  highp vec3 normal_26;
					  normal_26 = normalWorld_4;
					  highp vec3 col_27;
					  highp vec4 ndotl_28;
					  highp vec4 lengthSq_29;
					  highp vec4 tmpvar_30;
					  tmpvar_30 = (unity_4LightPosX0 - tmpvar_9.x);
					  highp vec4 tmpvar_31;
					  tmpvar_31 = (unity_4LightPosY0 - tmpvar_9.y);
					  highp vec4 tmpvar_32;
					  tmpvar_32 = (unity_4LightPosZ0 - tmpvar_9.z);
					  lengthSq_29 = (tmpvar_30 * tmpvar_30);
					  lengthSq_29 = (lengthSq_29 + (tmpvar_31 * tmpvar_31));
					  lengthSq_29 = (lengthSq_29 + (tmpvar_32 * tmpvar_32));
					  ndotl_28 = (tmpvar_30 * normal_26.x);
					  ndotl_28 = (ndotl_28 + (tmpvar_31 * normal_26.y));
					  ndotl_28 = (ndotl_28 + (tmpvar_32 * normal_26.z));
					  highp vec4 tmpvar_33;
					  tmpvar_33 = max (vec4(0.0, 0.0, 0.0, 0.0), (ndotl_28 * inversesqrt(lengthSq_29)));
					  ndotl_28 = tmpvar_33;
					  highp vec4 tmpvar_34;
					  tmpvar_34 = (tmpvar_33 * (1.0/((1.0 + 
					    (lengthSq_29 * lightAttenSq_25)
					  ))));
					  col_27 = (lightColor0_21 * tmpvar_34.x);
					  col_27 = (col_27 + (lightColor1_22 * tmpvar_34.y));
					  col_27 = (col_27 + (lightColor2_23 * tmpvar_34.z));
					  col_27 = (col_27 + (lightColor3_24 * tmpvar_34.w));
					  ambientOrLightmapUV_20.xyz = col_27;
					  mediump vec4 tmpvar_35;
					  tmpvar_35.w = 1.0;
					  tmpvar_35.xyz = normalWorld_4;
					  mediump vec3 res_36;
					  mediump vec3 x_37;
					  x_37.x = dot (unity_SHAr, tmpvar_35);
					  x_37.y = dot (unity_SHAg, tmpvar_35);
					  x_37.z = dot (unity_SHAb, tmpvar_35);
					  mediump vec3 x1_38;
					  mediump vec4 tmpvar_39;
					  tmpvar_39 = (normalWorld_4.xyzz * normalWorld_4.yzzx);
					  x1_38.x = dot (unity_SHBr, tmpvar_39);
					  x1_38.y = dot (unity_SHBg, tmpvar_39);
					  x1_38.z = dot (unity_SHBb, tmpvar_39);
					  res_36 = (x_37 + (x1_38 + (unity_SHC.xyz * 
					    ((normalWorld_4.x * normalWorld_4.x) - (normalWorld_4.y * normalWorld_4.y))
					  )));
					  res_36 = max (((1.055 * 
					    pow (max (res_36, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  ambientOrLightmapUV_20.xyz = (ambientOrLightmapUV_20.xyz + max (vec3(0.0, 0.0, 0.0), res_36));
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  mediump float x_40;
					  x_40 = (1.0 - max (0.0, dot (normalWorld_4, 
					    -(eyeVec_5)
					  )));
					  tmpvar_8.w = ((x_40 * x_40) * (x_40 * x_40));
					  tmpvar_6.w = clamp ((_Glossiness + (1.0 - 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  )), 0.0, 1.0);
					  gl_Position = tmpvar_10;
					  xlv_TEXCOORD0 = texcoord_12;
					  xlv_TEXCOORD1 = tmpvar_6;
					  xlv_TEXCOORD2 = ambientOrLightmapUV_20;
					  xlv_TEXCOORD3 = (unity_WorldToShadow[0] * (unity_ObjectToWorld * _glesVertex));
					  xlv_TEXCOORD4 = tmpvar_7;
					  xlv_TEXCOORD5 = tmpvar_8;
					}
					
					
					#endif
					#ifdef FRAGMENT
					#extension GL_EXT_shader_texture_lod : enable
					lowp vec4 impl_low_textureCubeLodEXT(lowp samplerCube sampler, highp vec3 coord, mediump float lod)
					{
					#if defined(GL_EXT_shader_texture_lod)
						return textureCubeLodEXT(sampler, coord, lod);
					#else
						return textureCube(sampler, coord, lod);
					#endif
					}
					
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform mediump vec4 _LightShadowData;
					uniform lowp samplerCube unity_SpecCube0;
					uniform mediump vec4 unity_SpecCube0_HDR;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _OcclusionMap;
					uniform mediump float _OcclusionStrength;
					uniform highp sampler2D _ShadowMapTexture;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec4 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec4 xlv_TEXCOORD4;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump float rl_1;
					  mediump float atten_2;
					  lowp vec4 tmpvar_3;
					  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_4;
					  tmpvar_4 = (_Color.xyz * tmpvar_3.xyz);
					  mediump vec3 tmpvar_5;
					  mediump vec3 tmpvar_6;
					  tmpvar_6 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_4, vec3(_Metallic));
					  tmpvar_5 = (tmpvar_4 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)));
					  mediump vec3 tmpvar_7;
					  tmpvar_7 = _LightColor0.xyz;
					  lowp float tmpvar_8;
					  highp float lightShadowDataX_9;
					  mediump float tmpvar_10;
					  tmpvar_10 = _LightShadowData.x;
					  lightShadowDataX_9 = tmpvar_10;
					  highp float tmpvar_11;
					  tmpvar_11 = max (float((texture2D (_ShadowMapTexture, xlv_TEXCOORD3.xy).x > xlv_TEXCOORD3.z)), lightShadowDataX_9);
					  tmpvar_8 = tmpvar_11;
					  atten_2 = tmpvar_8;
					  mediump float occ_12;
					  lowp float tmpvar_13;
					  tmpvar_13 = texture2D (_OcclusionMap, xlv_TEXCOORD0.xy).y;
					  occ_12 = tmpvar_13;
					  mediump float tmpvar_14;
					  tmpvar_14 = ((1.0 - _OcclusionStrength) + (occ_12 * _OcclusionStrength));
					  rl_1 = dot (xlv_TEXCOORD4.yzw, _WorldSpaceLightPos0.xyz);
					  highp vec4 tmpvar_15;
					  tmpvar_15 = unity_SpecCube0_HDR;
					  mediump float tmpvar_16;
					  tmpvar_16 = (1.0 - _Glossiness);
					  mediump vec4 hdr_17;
					  hdr_17 = tmpvar_15;
					  mediump vec4 tmpvar_18;
					  tmpvar_18.xyz = xlv_TEXCOORD4.yzw;
					  tmpvar_18.w = ((tmpvar_16 * (1.7 - 
					    (0.7 * tmpvar_16)
					  )) * 6.0);
					  lowp vec4 tmpvar_19;
					  tmpvar_19 = impl_low_textureCubeLodEXT (unity_SpecCube0, xlv_TEXCOORD4.yzw, tmpvar_18.w);
					  mediump vec4 tmpvar_20;
					  tmpvar_20 = tmpvar_19;
					  mediump vec2 tmpvar_21;
					  tmpvar_21.x = ((rl_1 * rl_1) * (rl_1 * rl_1));
					  tmpvar_21.y = tmpvar_16;
					  lowp vec4 tmpvar_22;
					  tmpvar_22 = texture2D (unity_NHxRoughness, tmpvar_21);
					  mediump vec4 tmpvar_23;
					  tmpvar_23.w = 1.0;
					  tmpvar_23.xyz = (((
					    (xlv_TEXCOORD2.xyz * tmpvar_14)
					   * tmpvar_5) + (
					    (((hdr_17.x * tmpvar_20.w) * tmpvar_20.xyz) * tmpvar_14)
					   * 
					    mix (tmpvar_6, xlv_TEXCOORD1.www, xlv_TEXCOORD5.www)
					  )) + ((tmpvar_5 + 
					    ((tmpvar_22.w * 16.0) * tmpvar_6)
					  ) * (
					    (tmpvar_7 * atten_2)
					   * 
					    max (0.0, dot (xlv_TEXCOORD5.xyz, _WorldSpaceLightPos0.xyz))
					  )));
					  mediump vec4 xlat_varoutput_24;
					  xlat_varoutput_24.xyz = tmpvar_23.xyz;
					  xlat_varoutput_24.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_24;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 unity_4LightPosX0;
					uniform highp vec4 unity_4LightPosY0;
					uniform highp vec4 unity_4LightPosZ0;
					uniform mediump vec4 unity_4LightAtten0;
					uniform mediump vec4 unity_LightColor[8];
					uniform mediump vec4 unity_SHBr;
					uniform mediump vec4 unity_SHBg;
					uniform mediump vec4 unity_SHBb;
					uniform mediump vec4 unity_SHC;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 n_13;
					  n_13 = (tmpvar_7.xyz - _WorldSpaceCameraPos);
					  tmpvar_12 = n_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  mediump vec3 normalWorld_19;
					  normalWorld_19 = tmpvar_18;
					  mediump vec4 ambientOrLightmapUV_20;
					  ambientOrLightmapUV_20.w = 0.0;
					  highp vec3 lightColor0_21;
					  lightColor0_21 = unity_LightColor[0].xyz;
					  highp vec3 lightColor1_22;
					  lightColor1_22 = unity_LightColor[1].xyz;
					  highp vec3 lightColor2_23;
					  lightColor2_23 = unity_LightColor[2].xyz;
					  highp vec3 lightColor3_24;
					  lightColor3_24 = unity_LightColor[3].xyz;
					  highp vec4 lightAttenSq_25;
					  lightAttenSq_25 = unity_4LightAtten0;
					  highp vec3 normal_26;
					  normal_26 = normalWorld_19;
					  highp vec3 col_27;
					  highp vec4 ndotl_28;
					  highp vec4 lengthSq_29;
					  highp vec4 tmpvar_30;
					  tmpvar_30 = (unity_4LightPosX0 - tmpvar_7.x);
					  highp vec4 tmpvar_31;
					  tmpvar_31 = (unity_4LightPosY0 - tmpvar_7.y);
					  highp vec4 tmpvar_32;
					  tmpvar_32 = (unity_4LightPosZ0 - tmpvar_7.z);
					  lengthSq_29 = (tmpvar_30 * tmpvar_30);
					  lengthSq_29 = (lengthSq_29 + (tmpvar_31 * tmpvar_31));
					  lengthSq_29 = (lengthSq_29 + (tmpvar_32 * tmpvar_32));
					  ndotl_28 = (tmpvar_30 * normal_26.x);
					  ndotl_28 = (ndotl_28 + (tmpvar_31 * normal_26.y));
					  ndotl_28 = (ndotl_28 + (tmpvar_32 * normal_26.z));
					  highp vec4 tmpvar_33;
					  tmpvar_33 = max (vec4(0.0, 0.0, 0.0, 0.0), (ndotl_28 * inversesqrt(lengthSq_29)));
					  ndotl_28 = tmpvar_33;
					  highp vec4 tmpvar_34;
					  tmpvar_34 = (tmpvar_33 * (1.0/((1.0 + 
					    (lengthSq_29 * lightAttenSq_25)
					  ))));
					  col_27 = (lightColor0_21 * tmpvar_34.x);
					  col_27 = (col_27 + (lightColor1_22 * tmpvar_34.y));
					  col_27 = (col_27 + (lightColor2_23 * tmpvar_34.z));
					  col_27 = (col_27 + (lightColor3_24 * tmpvar_34.w));
					  ambientOrLightmapUV_20.xyz = col_27;
					  mediump vec3 ambient_35;
					  ambient_35 = (ambientOrLightmapUV_20.xyz * ((ambientOrLightmapUV_20.xyz * 
					    ((ambientOrLightmapUV_20.xyz * 0.305306) + 0.6821711)
					  ) + 0.01252288));
					  mediump vec3 x1_36;
					  mediump vec4 tmpvar_37;
					  tmpvar_37 = (normalWorld_19.xyzz * normalWorld_19.yzzx);
					  x1_36.x = dot (unity_SHBr, tmpvar_37);
					  x1_36.y = dot (unity_SHBg, tmpvar_37);
					  x1_36.z = dot (unity_SHBb, tmpvar_37);
					  ambient_35 = (ambient_35 + (x1_36 + (unity_SHC.xyz * 
					    ((normalWorld_19.x * normalWorld_19.x) - (normalWorld_19.y * normalWorld_19.y))
					  )));
					  ambientOrLightmapUV_20.xyz = ambient_35;
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					  xlv_TEXCOORD5 = ambientOrLightmapUV_20;
					  xlv_TEXCOORD6 = (unity_WorldToShadow[0] * (unity_ObjectToWorld * _glesVertex));
					}
					
					
					#endif
					#ifdef FRAGMENT
					#extension GL_EXT_shader_texture_lod : enable
					lowp vec4 impl_low_textureCubeLodEXT(lowp samplerCube sampler, highp vec3 coord, mediump float lod)
					{
					#if defined(GL_EXT_shader_texture_lod)
						return textureCubeLodEXT(sampler, coord, lod);
					#else
						return textureCube(sampler, coord, lod);
					#endif
					}
					
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform mediump vec4 unity_SHAr;
					uniform mediump vec4 unity_SHAg;
					uniform mediump vec4 unity_SHAb;
					uniform mediump vec4 _LightShadowData;
					uniform lowp samplerCube unity_SpecCube0;
					uniform mediump vec4 unity_SpecCube0_HDR;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _OcclusionMap;
					uniform mediump float _OcclusionStrength;
					uniform highp sampler2D _ShadowMapTexture;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec4 c_1;
					  mediump float atten_2;
					  lowp vec4 tmpvar_3;
					  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_4;
					  tmpvar_4 = (_Color.xyz * tmpvar_3.xyz);
					  mediump vec3 tmpvar_5;
					  mediump vec3 tmpvar_6;
					  tmpvar_6 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_4, vec3(_Metallic));
					  mediump float tmpvar_7;
					  tmpvar_7 = (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w));
					  tmpvar_5 = (tmpvar_4 * tmpvar_7);
					  mediump vec3 tmpvar_8;
					  tmpvar_8 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_9;
					  tmpvar_9 = normalize(xlv_TEXCOORD1);
					  mediump vec3 tmpvar_10;
					  tmpvar_10 = _LightColor0.xyz;
					  lowp float tmpvar_11;
					  highp float lightShadowDataX_12;
					  mediump float tmpvar_13;
					  tmpvar_13 = _LightShadowData.x;
					  lightShadowDataX_12 = tmpvar_13;
					  highp float tmpvar_14;
					  tmpvar_14 = max (float((texture2D (_ShadowMapTexture, xlv_TEXCOORD6.xy).x > xlv_TEXCOORD6.z)), lightShadowDataX_12);
					  tmpvar_11 = tmpvar_14;
					  atten_2 = tmpvar_11;
					  mediump float occ_15;
					  lowp float tmpvar_16;
					  tmpvar_16 = texture2D (_OcclusionMap, xlv_TEXCOORD0.xy).y;
					  occ_15 = tmpvar_16;
					  mediump float tmpvar_17;
					  tmpvar_17 = ((1.0 - _OcclusionStrength) + (occ_15 * _OcclusionStrength));
					  highp vec4 tmpvar_18;
					  tmpvar_18 = unity_SpecCube0_HDR;
					  mediump float tmpvar_19;
					  tmpvar_19 = (1.0 - _Glossiness);
					  mediump vec3 tmpvar_20;
					  tmpvar_20 = (tmpvar_9 - (2.0 * (
					    dot (tmpvar_8, tmpvar_9)
					   * tmpvar_8)));
					  mediump vec4 tmpvar_21;
					  tmpvar_21.w = 1.0;
					  tmpvar_21.xyz = tmpvar_8;
					  mediump vec3 x_22;
					  x_22.x = dot (unity_SHAr, tmpvar_21);
					  x_22.y = dot (unity_SHAg, tmpvar_21);
					  x_22.z = dot (unity_SHAb, tmpvar_21);
					  mediump vec4 hdr_23;
					  hdr_23 = tmpvar_18;
					  mediump vec4 tmpvar_24;
					  tmpvar_24.xyz = tmpvar_20;
					  tmpvar_24.w = ((tmpvar_19 * (1.7 - 
					    (0.7 * tmpvar_19)
					  )) * 6.0);
					  lowp vec4 tmpvar_25;
					  tmpvar_25 = impl_low_textureCubeLodEXT (unity_SpecCube0, tmpvar_20, tmpvar_24.w);
					  mediump vec4 tmpvar_26;
					  tmpvar_26 = tmpvar_25;
					  mediump vec3 viewDir_27;
					  viewDir_27 = -(tmpvar_9);
					  mediump float specular_28;
					  mediump vec3 tmpvar_29;
					  mediump vec3 inVec_30;
					  inVec_30 = (_WorldSpaceLightPos0.xyz + viewDir_27);
					  tmpvar_29 = (inVec_30 * inversesqrt(max (0.001, 
					    dot (inVec_30, inVec_30)
					  )));
					  mediump float tmpvar_31;
					  tmpvar_31 = max (0.0, dot (_WorldSpaceLightPos0.xyz, tmpvar_29));
					  mediump float tmpvar_32;
					  tmpvar_32 = max (0.0001, (tmpvar_19 * tmpvar_19));
					  mediump float tmpvar_33;
					  tmpvar_33 = max (((2.0 / 
					    (tmpvar_32 * tmpvar_32)
					  ) - 2.0), 0.0001);
					  specular_28 = sqrt(max (0.0001, (
					    ((tmpvar_33 + 1.0) * pow (max (0.0, dot (tmpvar_8, tmpvar_29)), tmpvar_33))
					   / 
					    (((8.0 * (
					      ((tmpvar_31 * tmpvar_31) * _Glossiness)
					     + 
					      (tmpvar_19 * tmpvar_19)
					    )) * tmpvar_31) + 0.0001)
					  )));
					  mediump float tmpvar_34;
					  tmpvar_34 = clamp (specular_28, 0.0, 100.0);
					  specular_28 = tmpvar_34;
					  mediump float x_35;
					  x_35 = (1.0 - max (0.0, dot (tmpvar_8, viewDir_27)));
					  mediump vec3 tmpvar_36;
					  tmpvar_36 = (((
					    ((tmpvar_5 + (tmpvar_34 * tmpvar_6)) * (tmpvar_10 * atten_2))
					   * 
					    max (0.0, dot (tmpvar_8, _WorldSpaceLightPos0.xyz))
					  ) + (
					    (max (((1.055 * 
					      pow (max (vec3(0.0, 0.0, 0.0), (xlv_TEXCOORD5.xyz + x_22)), vec3(0.4166667, 0.4166667, 0.4166667))
					    ) - 0.055), vec3(0.0, 0.0, 0.0)) * tmpvar_17)
					   * tmpvar_5)) + ((
					    (1.0 - ((tmpvar_19 * tmpvar_19) * (tmpvar_19 * 0.28)))
					   * 
					    (((hdr_23.x * tmpvar_26.w) * tmpvar_26.xyz) * tmpvar_17)
					  ) * mix (tmpvar_6, vec3(
					    clamp ((_Glossiness + (1.0 - tmpvar_7)), 0.0, 1.0)
					  ), vec3(
					    ((x_35 * x_35) * (x_35 * x_35))
					  ))));
					  mediump vec4 tmpvar_37;
					  tmpvar_37.w = 1.0;
					  tmpvar_37.xyz = tmpvar_36;
					  c_1.w = tmpvar_37.w;
					  c_1.xyz = tmpvar_36;
					  c_1.xyz = c_1.xyz;
					  mediump vec4 xlat_varoutput_38;
					  xlat_varoutput_38.xyz = c_1.xyz;
					  xlat_varoutput_38.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_38;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 unity_4LightPosX0;
					uniform highp vec4 unity_4LightPosY0;
					uniform highp vec4 unity_4LightPosZ0;
					uniform mediump vec4 unity_4LightAtten0;
					uniform mediump vec4 unity_LightColor[8];
					uniform mediump vec4 unity_SHBr;
					uniform mediump vec4 unity_SHBg;
					uniform mediump vec4 unity_SHBb;
					uniform mediump vec4 unity_SHC;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 n_13;
					  n_13 = (tmpvar_7.xyz - _WorldSpaceCameraPos);
					  tmpvar_12 = n_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  mediump vec3 normalWorld_19;
					  normalWorld_19 = tmpvar_18;
					  mediump vec4 ambientOrLightmapUV_20;
					  ambientOrLightmapUV_20.w = 0.0;
					  highp vec3 lightColor0_21;
					  lightColor0_21 = unity_LightColor[0].xyz;
					  highp vec3 lightColor1_22;
					  lightColor1_22 = unity_LightColor[1].xyz;
					  highp vec3 lightColor2_23;
					  lightColor2_23 = unity_LightColor[2].xyz;
					  highp vec3 lightColor3_24;
					  lightColor3_24 = unity_LightColor[3].xyz;
					  highp vec4 lightAttenSq_25;
					  lightAttenSq_25 = unity_4LightAtten0;
					  highp vec3 normal_26;
					  normal_26 = normalWorld_19;
					  highp vec3 col_27;
					  highp vec4 ndotl_28;
					  highp vec4 lengthSq_29;
					  highp vec4 tmpvar_30;
					  tmpvar_30 = (unity_4LightPosX0 - tmpvar_7.x);
					  highp vec4 tmpvar_31;
					  tmpvar_31 = (unity_4LightPosY0 - tmpvar_7.y);
					  highp vec4 tmpvar_32;
					  tmpvar_32 = (unity_4LightPosZ0 - tmpvar_7.z);
					  lengthSq_29 = (tmpvar_30 * tmpvar_30);
					  lengthSq_29 = (lengthSq_29 + (tmpvar_31 * tmpvar_31));
					  lengthSq_29 = (lengthSq_29 + (tmpvar_32 * tmpvar_32));
					  ndotl_28 = (tmpvar_30 * normal_26.x);
					  ndotl_28 = (ndotl_28 + (tmpvar_31 * normal_26.y));
					  ndotl_28 = (ndotl_28 + (tmpvar_32 * normal_26.z));
					  highp vec4 tmpvar_33;
					  tmpvar_33 = max (vec4(0.0, 0.0, 0.0, 0.0), (ndotl_28 * inversesqrt(lengthSq_29)));
					  ndotl_28 = tmpvar_33;
					  highp vec4 tmpvar_34;
					  tmpvar_34 = (tmpvar_33 * (1.0/((1.0 + 
					    (lengthSq_29 * lightAttenSq_25)
					  ))));
					  col_27 = (lightColor0_21 * tmpvar_34.x);
					  col_27 = (col_27 + (lightColor1_22 * tmpvar_34.y));
					  col_27 = (col_27 + (lightColor2_23 * tmpvar_34.z));
					  col_27 = (col_27 + (lightColor3_24 * tmpvar_34.w));
					  ambientOrLightmapUV_20.xyz = col_27;
					  mediump vec3 ambient_35;
					  ambient_35 = (ambientOrLightmapUV_20.xyz * ((ambientOrLightmapUV_20.xyz * 
					    ((ambientOrLightmapUV_20.xyz * 0.305306) + 0.6821711)
					  ) + 0.01252288));
					  mediump vec3 x1_36;
					  mediump vec4 tmpvar_37;
					  tmpvar_37 = (normalWorld_19.xyzz * normalWorld_19.yzzx);
					  x1_36.x = dot (unity_SHBr, tmpvar_37);
					  x1_36.y = dot (unity_SHBg, tmpvar_37);
					  x1_36.z = dot (unity_SHBb, tmpvar_37);
					  ambient_35 = (ambient_35 + (x1_36 + (unity_SHC.xyz * 
					    ((normalWorld_19.x * normalWorld_19.x) - (normalWorld_19.y * normalWorld_19.y))
					  )));
					  ambientOrLightmapUV_20.xyz = ambient_35;
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					  xlv_TEXCOORD5 = ambientOrLightmapUV_20;
					  xlv_TEXCOORD6 = (unity_WorldToShadow[0] * (unity_ObjectToWorld * _glesVertex));
					}
					
					
					#endif
					#ifdef FRAGMENT
					#extension GL_EXT_shader_texture_lod : enable
					lowp vec4 impl_low_textureCubeLodEXT(lowp samplerCube sampler, highp vec3 coord, mediump float lod)
					{
					#if defined(GL_EXT_shader_texture_lod)
						return textureCubeLodEXT(sampler, coord, lod);
					#else
						return textureCube(sampler, coord, lod);
					#endif
					}
					
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform mediump vec4 unity_SHAr;
					uniform mediump vec4 unity_SHAg;
					uniform mediump vec4 unity_SHAb;
					uniform mediump vec4 _LightShadowData;
					uniform lowp samplerCube unity_SpecCube0;
					uniform mediump vec4 unity_SpecCube0_HDR;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _OcclusionMap;
					uniform mediump float _OcclusionStrength;
					uniform highp sampler2D _ShadowMapTexture;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec4 c_1;
					  mediump float atten_2;
					  lowp vec4 tmpvar_3;
					  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_4;
					  tmpvar_4 = (_Color.xyz * tmpvar_3.xyz);
					  mediump vec3 tmpvar_5;
					  mediump vec3 tmpvar_6;
					  tmpvar_6 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_4, vec3(_Metallic));
					  mediump float tmpvar_7;
					  tmpvar_7 = (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w));
					  tmpvar_5 = (tmpvar_4 * tmpvar_7);
					  mediump vec3 tmpvar_8;
					  tmpvar_8 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_9;
					  tmpvar_9 = normalize(xlv_TEXCOORD1);
					  mediump vec3 tmpvar_10;
					  tmpvar_10 = _LightColor0.xyz;
					  lowp float tmpvar_11;
					  highp float lightShadowDataX_12;
					  mediump float tmpvar_13;
					  tmpvar_13 = _LightShadowData.x;
					  lightShadowDataX_12 = tmpvar_13;
					  highp float tmpvar_14;
					  tmpvar_14 = max (float((texture2D (_ShadowMapTexture, xlv_TEXCOORD6.xy).x > xlv_TEXCOORD6.z)), lightShadowDataX_12);
					  tmpvar_11 = tmpvar_14;
					  atten_2 = tmpvar_11;
					  mediump float occ_15;
					  lowp float tmpvar_16;
					  tmpvar_16 = texture2D (_OcclusionMap, xlv_TEXCOORD0.xy).y;
					  occ_15 = tmpvar_16;
					  mediump float tmpvar_17;
					  tmpvar_17 = ((1.0 - _OcclusionStrength) + (occ_15 * _OcclusionStrength));
					  highp vec4 tmpvar_18;
					  tmpvar_18 = unity_SpecCube0_HDR;
					  mediump float tmpvar_19;
					  tmpvar_19 = (1.0 - _Glossiness);
					  mediump vec3 tmpvar_20;
					  tmpvar_20 = (tmpvar_9 - (2.0 * (
					    dot (tmpvar_8, tmpvar_9)
					   * tmpvar_8)));
					  mediump vec4 tmpvar_21;
					  tmpvar_21.w = 1.0;
					  tmpvar_21.xyz = tmpvar_8;
					  mediump vec3 x_22;
					  x_22.x = dot (unity_SHAr, tmpvar_21);
					  x_22.y = dot (unity_SHAg, tmpvar_21);
					  x_22.z = dot (unity_SHAb, tmpvar_21);
					  mediump vec4 hdr_23;
					  hdr_23 = tmpvar_18;
					  mediump vec4 tmpvar_24;
					  tmpvar_24.xyz = tmpvar_20;
					  tmpvar_24.w = ((tmpvar_19 * (1.7 - 
					    (0.7 * tmpvar_19)
					  )) * 6.0);
					  lowp vec4 tmpvar_25;
					  tmpvar_25 = impl_low_textureCubeLodEXT (unity_SpecCube0, tmpvar_20, tmpvar_24.w);
					  mediump vec4 tmpvar_26;
					  tmpvar_26 = tmpvar_25;
					  mediump vec3 viewDir_27;
					  viewDir_27 = -(tmpvar_9);
					  mediump float specular_28;
					  mediump vec3 tmpvar_29;
					  mediump vec3 inVec_30;
					  inVec_30 = (_WorldSpaceLightPos0.xyz + viewDir_27);
					  tmpvar_29 = (inVec_30 * inversesqrt(max (0.001, 
					    dot (inVec_30, inVec_30)
					  )));
					  mediump float tmpvar_31;
					  tmpvar_31 = max (0.0, dot (_WorldSpaceLightPos0.xyz, tmpvar_29));
					  mediump float tmpvar_32;
					  tmpvar_32 = max (0.0001, (tmpvar_19 * tmpvar_19));
					  mediump float tmpvar_33;
					  tmpvar_33 = max (((2.0 / 
					    (tmpvar_32 * tmpvar_32)
					  ) - 2.0), 0.0001);
					  specular_28 = sqrt(max (0.0001, (
					    ((tmpvar_33 + 1.0) * pow (max (0.0, dot (tmpvar_8, tmpvar_29)), tmpvar_33))
					   / 
					    (((8.0 * (
					      ((tmpvar_31 * tmpvar_31) * _Glossiness)
					     + 
					      (tmpvar_19 * tmpvar_19)
					    )) * tmpvar_31) + 0.0001)
					  )));
					  mediump float tmpvar_34;
					  tmpvar_34 = clamp (specular_28, 0.0, 100.0);
					  specular_28 = tmpvar_34;
					  mediump float x_35;
					  x_35 = (1.0 - max (0.0, dot (tmpvar_8, viewDir_27)));
					  mediump vec3 tmpvar_36;
					  tmpvar_36 = (((
					    ((tmpvar_5 + (tmpvar_34 * tmpvar_6)) * (tmpvar_10 * atten_2))
					   * 
					    max (0.0, dot (tmpvar_8, _WorldSpaceLightPos0.xyz))
					  ) + (
					    (max (((1.055 * 
					      pow (max (vec3(0.0, 0.0, 0.0), (xlv_TEXCOORD5.xyz + x_22)), vec3(0.4166667, 0.4166667, 0.4166667))
					    ) - 0.055), vec3(0.0, 0.0, 0.0)) * tmpvar_17)
					   * tmpvar_5)) + ((
					    (1.0 - ((tmpvar_19 * tmpvar_19) * (tmpvar_19 * 0.28)))
					   * 
					    (((hdr_23.x * tmpvar_26.w) * tmpvar_26.xyz) * tmpvar_17)
					  ) * mix (tmpvar_6, vec3(
					    clamp ((_Glossiness + (1.0 - tmpvar_7)), 0.0, 1.0)
					  ), vec3(
					    ((x_35 * x_35) * (x_35 * x_35))
					  ))));
					  mediump vec4 tmpvar_37;
					  tmpvar_37.w = 1.0;
					  tmpvar_37.xyz = tmpvar_36;
					  c_1.w = tmpvar_37.w;
					  c_1.xyz = tmpvar_36;
					  c_1.xyz = c_1.xyz;
					  mediump vec4 xlat_varoutput_38;
					  xlat_varoutput_38.xyz = c_1.xyz;
					  xlat_varoutput_38.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_38;
					}
					
					
					#endif
}
}
Program "fp" {
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
}
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
}
}
 }
 Pass {
  Name "FORWARD_DELTA"
  Tags { "LIGHTMODE"="ForwardAdd" "SHADOWSUPPORT"="true" "RenderType"="Opaque" "PerformanceChecks"="False" }
  ZWrite Off
  Blend [_SrcBlend] One
  GpuProgramID 70942
Program "vp" {
SubProgram "gles hw_tier01 " {
Keywords { "POINT" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec3 lightDir_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_13;
					  tmpvar_13 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  lightDir_6 = tmpvar_13;
					  mediump vec3 tmpvar_14;
					  highp vec3 n_15;
					  n_15 = lightDir_6;
					  highp vec3 tmpvar_16;
					  tmpvar_16 = normalize(n_15);
					  tmpvar_14 = tmpvar_16;
					  lightDir_6 = tmpvar_14;
					  highp vec3 tmpvar_17;
					  tmpvar_17 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_17;
					  highp vec3 norm_18;
					  norm_18 = tmpvar_1;
					  highp vec4 v_19;
					  v_19.x = unity_WorldToObject[0].x;
					  v_19.y = unity_WorldToObject[1].x;
					  v_19.z = unity_WorldToObject[2].x;
					  v_19.w = unity_WorldToObject[3].x;
					  highp vec4 v_20;
					  v_20.x = unity_WorldToObject[0].y;
					  v_20.y = unity_WorldToObject[1].y;
					  v_20.z = unity_WorldToObject[2].y;
					  v_20.w = unity_WorldToObject[3].y;
					  highp vec4 v_21;
					  v_21.x = unity_WorldToObject[0].z;
					  v_21.y = unity_WorldToObject[1].z;
					  v_21.z = unity_WorldToObject[2].z;
					  v_21.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_22;
					  tmpvar_22 = normalize(((
					    (v_19.xyz * norm_18.x)
					   + 
					    (v_20.xyz * norm_18.y)
					  ) + (v_21.xyz * norm_18.z)));
					  normalWorld_4 = tmpvar_22;
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = (unity_WorldToLight * (unity_ObjectToWorld * _glesVertex)).xyz;
					  xlv_TEXCOORD3 = tmpvar_7;
					  xlv_TEXCOORD4 = tmpvar_14;
					  xlv_TEXCOORD5 = normalWorld_4;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _LightTexture0;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump float tmpvar_4;
					  tmpvar_4 = dot (xlv_TEXCOORD3.yzw, xlv_TEXCOORD4);
					  mediump vec2 tmpvar_5;
					  tmpvar_5.x = ((tmpvar_4 * tmpvar_4) * (tmpvar_4 * tmpvar_4));
					  tmpvar_5.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (unity_NHxRoughness, tmpvar_5);
					  c_1 = (((tmpvar_3 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_6.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic))
					  )) * _LightColor0.xyz);
					  highp float tmpvar_7;
					  tmpvar_7 = dot (xlv_TEXCOORD1, xlv_TEXCOORD1);
					  lowp vec4 tmpvar_8;
					  tmpvar_8 = texture2D (_LightTexture0, vec2(tmpvar_7));
					  c_1 = (c_1 * (tmpvar_8.w * max (0.0, 
					    dot (xlv_TEXCOORD5, xlv_TEXCOORD4)
					  )));
					  mediump vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = c_1;
					  mediump vec4 xlat_varoutput_10;
					  xlat_varoutput_10.xyz = tmpvar_9.xyz;
					  xlat_varoutput_10.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_10;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "POINT" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  highp vec3 lightDir_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_13;
					  highp vec3 n_14;
					  n_14 = (tmpvar_8.xyz - _WorldSpaceCameraPos);
					  tmpvar_13 = n_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_7.xyz = tmpvar_19;
					  highp vec3 tmpvar_20;
					  tmpvar_20 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  mediump vec3 tmpvar_21;
					  tmpvar_21 = tmpvar_20;
					  lightDir_4 = tmpvar_21;
					  tmpvar_5.w = lightDir_4.x;
					  tmpvar_6.w = lightDir_4.y;
					  tmpvar_7.w = lightDir_4.z;
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = tmpvar_13;
					  xlv_TEXCOORD2 = tmpvar_5;
					  xlv_TEXCOORD2_1 = tmpvar_6;
					  xlv_TEXCOORD2_2 = tmpvar_7;
					  xlv_TEXCOORD5 = (unity_WorldToLight * (unity_ObjectToWorld * _glesVertex)).xyz;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _LightTexture0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_4;
					  tmpvar_4.x = xlv_TEXCOORD2.w;
					  tmpvar_4.y = xlv_TEXCOORD2_1.w;
					  tmpvar_4.z = xlv_TEXCOORD2_2.w;
					  highp float tmpvar_5;
					  tmpvar_5 = dot (xlv_TEXCOORD5, xlv_TEXCOORD5);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (_LightTexture0, vec2(tmpvar_5));
					  mediump float atten_7;
					  atten_7 = tmpvar_6.w;
					  mediump vec3 tmpvar_8;
					  tmpvar_8 = _LightColor0.xyz;
					  mediump vec3 tmpvar_9;
					  tmpvar_9 = normalize(tmpvar_4);
					  tmpvar_8 = (tmpvar_8 * atten_7);
					  mediump float specular_10;
					  mediump vec3 tmpvar_11;
					  mediump vec3 inVec_12;
					  inVec_12 = (tmpvar_9 - normalize(xlv_TEXCOORD1));
					  tmpvar_11 = (inVec_12 * inversesqrt(max (0.001, 
					    dot (inVec_12, inVec_12)
					  )));
					  mediump float tmpvar_13;
					  tmpvar_13 = max (0.0, dot (tmpvar_9, tmpvar_11));
					  mediump float tmpvar_14;
					  tmpvar_14 = (1.0 - _Glossiness);
					  mediump float tmpvar_15;
					  tmpvar_15 = max (0.0001, (tmpvar_14 * tmpvar_14));
					  mediump float tmpvar_16;
					  tmpvar_16 = max (((2.0 / 
					    (tmpvar_15 * tmpvar_15)
					  ) - 2.0), 0.0001);
					  specular_10 = sqrt(max (0.0001, (
					    ((tmpvar_16 + 1.0) * pow (max (0.0, dot (tmpvar_3, tmpvar_11)), tmpvar_16))
					   / 
					    (((8.0 * (
					      ((tmpvar_13 * tmpvar_13) * _Glossiness)
					     + 
					      (tmpvar_14 * tmpvar_14)
					    )) * tmpvar_13) + 0.0001)
					  )));
					  mediump float tmpvar_17;
					  tmpvar_17 = clamp (specular_10, 0.0, 100.0);
					  specular_10 = tmpvar_17;
					  mediump vec4 tmpvar_18;
					  tmpvar_18.w = 1.0;
					  tmpvar_18.xyz = (((
					    (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)))
					   + 
					    (tmpvar_17 * mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic)))
					  ) * tmpvar_8) * max (0.0, dot (tmpvar_3, tmpvar_9)));
					  mediump vec4 xlat_varoutput_19;
					  xlat_varoutput_19.xyz = tmpvar_18.xyz;
					  xlat_varoutput_19.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_19;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "POINT" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  highp vec3 lightDir_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_13;
					  highp vec3 n_14;
					  n_14 = (tmpvar_8.xyz - _WorldSpaceCameraPos);
					  tmpvar_13 = n_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_7.xyz = tmpvar_19;
					  highp vec3 tmpvar_20;
					  tmpvar_20 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  mediump vec3 tmpvar_21;
					  tmpvar_21 = tmpvar_20;
					  lightDir_4 = tmpvar_21;
					  tmpvar_5.w = lightDir_4.x;
					  tmpvar_6.w = lightDir_4.y;
					  tmpvar_7.w = lightDir_4.z;
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = tmpvar_13;
					  xlv_TEXCOORD2 = tmpvar_5;
					  xlv_TEXCOORD2_1 = tmpvar_6;
					  xlv_TEXCOORD2_2 = tmpvar_7;
					  xlv_TEXCOORD5 = (unity_WorldToLight * (unity_ObjectToWorld * _glesVertex)).xyz;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _LightTexture0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_4;
					  tmpvar_4.x = xlv_TEXCOORD2.w;
					  tmpvar_4.y = xlv_TEXCOORD2_1.w;
					  tmpvar_4.z = xlv_TEXCOORD2_2.w;
					  highp float tmpvar_5;
					  tmpvar_5 = dot (xlv_TEXCOORD5, xlv_TEXCOORD5);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (_LightTexture0, vec2(tmpvar_5));
					  mediump float atten_7;
					  atten_7 = tmpvar_6.w;
					  mediump vec3 tmpvar_8;
					  tmpvar_8 = _LightColor0.xyz;
					  mediump vec3 tmpvar_9;
					  tmpvar_9 = normalize(tmpvar_4);
					  tmpvar_8 = (tmpvar_8 * atten_7);
					  mediump float specular_10;
					  mediump vec3 tmpvar_11;
					  mediump vec3 inVec_12;
					  inVec_12 = (tmpvar_9 - normalize(xlv_TEXCOORD1));
					  tmpvar_11 = (inVec_12 * inversesqrt(max (0.001, 
					    dot (inVec_12, inVec_12)
					  )));
					  mediump float tmpvar_13;
					  tmpvar_13 = max (0.0, dot (tmpvar_9, tmpvar_11));
					  mediump float tmpvar_14;
					  tmpvar_14 = (1.0 - _Glossiness);
					  mediump float tmpvar_15;
					  tmpvar_15 = max (0.0001, (tmpvar_14 * tmpvar_14));
					  mediump float tmpvar_16;
					  tmpvar_16 = max (((2.0 / 
					    (tmpvar_15 * tmpvar_15)
					  ) - 2.0), 0.0001);
					  specular_10 = sqrt(max (0.0001, (
					    ((tmpvar_16 + 1.0) * pow (max (0.0, dot (tmpvar_3, tmpvar_11)), tmpvar_16))
					   / 
					    (((8.0 * (
					      ((tmpvar_13 * tmpvar_13) * _Glossiness)
					     + 
					      (tmpvar_14 * tmpvar_14)
					    )) * tmpvar_13) + 0.0001)
					  )));
					  mediump float tmpvar_17;
					  tmpvar_17 = clamp (specular_10, 0.0, 100.0);
					  specular_10 = tmpvar_17;
					  mediump vec4 tmpvar_18;
					  tmpvar_18.w = 1.0;
					  tmpvar_18.xyz = (((
					    (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)))
					   + 
					    (tmpvar_17 * mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic)))
					  ) * tmpvar_8) * max (0.0, dot (tmpvar_3, tmpvar_9)));
					  mediump vec4 xlat_varoutput_19;
					  xlat_varoutput_19.xyz = tmpvar_18.xyz;
					  xlat_varoutput_19.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_19;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec3 lightDir_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_13;
					  tmpvar_13 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  lightDir_6 = tmpvar_13;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  normalWorld_4 = tmpvar_19;
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD3 = tmpvar_7;
					  xlv_TEXCOORD4 = lightDir_6;
					  xlv_TEXCOORD5 = normalWorld_4;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump float tmpvar_4;
					  tmpvar_4 = dot (xlv_TEXCOORD3.yzw, xlv_TEXCOORD4);
					  mediump vec2 tmpvar_5;
					  tmpvar_5.x = ((tmpvar_4 * tmpvar_4) * (tmpvar_4 * tmpvar_4));
					  tmpvar_5.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (unity_NHxRoughness, tmpvar_5);
					  c_1 = (((tmpvar_3 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_6.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic))
					  )) * _LightColor0.xyz);
					  c_1 = (c_1 * max (0.0, dot (xlv_TEXCOORD5, xlv_TEXCOORD4)));
					  mediump vec4 tmpvar_7;
					  tmpvar_7.w = 1.0;
					  tmpvar_7.xyz = c_1;
					  mediump vec4 xlat_varoutput_8;
					  xlat_varoutput_8.xyz = tmpvar_7.xyz;
					  xlat_varoutput_8.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_8;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 n_13;
					  n_13 = (tmpvar_7.xyz - _WorldSpaceCameraPos);
					  tmpvar_12 = n_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = (_WorldSpaceLightPos0.xyz - (tmpvar_7.xyz * _WorldSpaceLightPos0.w));
					  tmpvar_4.w = tmpvar_19.x;
					  tmpvar_5.w = tmpvar_19.y;
					  tmpvar_6.w = tmpvar_19.z;
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_4;
					  tmpvar_4.x = xlv_TEXCOORD2.w;
					  tmpvar_4.y = xlv_TEXCOORD2_1.w;
					  tmpvar_4.z = xlv_TEXCOORD2_2.w;
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = _LightColor0.xyz;
					  mediump float specular_6;
					  mediump vec3 tmpvar_7;
					  mediump vec3 inVec_8;
					  inVec_8 = (tmpvar_4 - normalize(xlv_TEXCOORD1));
					  tmpvar_7 = (inVec_8 * inversesqrt(max (0.001, 
					    dot (inVec_8, inVec_8)
					  )));
					  mediump float tmpvar_9;
					  tmpvar_9 = max (0.0, dot (tmpvar_4, tmpvar_7));
					  mediump float tmpvar_10;
					  tmpvar_10 = (1.0 - _Glossiness);
					  mediump float tmpvar_11;
					  tmpvar_11 = max (0.0001, (tmpvar_10 * tmpvar_10));
					  mediump float tmpvar_12;
					  tmpvar_12 = max (((2.0 / 
					    (tmpvar_11 * tmpvar_11)
					  ) - 2.0), 0.0001);
					  specular_6 = sqrt(max (0.0001, (
					    ((tmpvar_12 + 1.0) * pow (max (0.0, dot (tmpvar_3, tmpvar_7)), tmpvar_12))
					   / 
					    (((8.0 * (
					      ((tmpvar_9 * tmpvar_9) * _Glossiness)
					     + 
					      (tmpvar_10 * tmpvar_10)
					    )) * tmpvar_9) + 0.0001)
					  )));
					  mediump float tmpvar_13;
					  tmpvar_13 = clamp (specular_6, 0.0, 100.0);
					  specular_6 = tmpvar_13;
					  mediump vec4 tmpvar_14;
					  tmpvar_14.w = 1.0;
					  tmpvar_14.xyz = (((
					    (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)))
					   + 
					    (tmpvar_13 * mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic)))
					  ) * tmpvar_5) * max (0.0, dot (tmpvar_3, tmpvar_4)));
					  mediump vec4 xlat_varoutput_15;
					  xlat_varoutput_15.xyz = tmpvar_14.xyz;
					  xlat_varoutput_15.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_15;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 n_13;
					  n_13 = (tmpvar_7.xyz - _WorldSpaceCameraPos);
					  tmpvar_12 = n_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = (_WorldSpaceLightPos0.xyz - (tmpvar_7.xyz * _WorldSpaceLightPos0.w));
					  tmpvar_4.w = tmpvar_19.x;
					  tmpvar_5.w = tmpvar_19.y;
					  tmpvar_6.w = tmpvar_19.z;
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_4;
					  tmpvar_4.x = xlv_TEXCOORD2.w;
					  tmpvar_4.y = xlv_TEXCOORD2_1.w;
					  tmpvar_4.z = xlv_TEXCOORD2_2.w;
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = _LightColor0.xyz;
					  mediump float specular_6;
					  mediump vec3 tmpvar_7;
					  mediump vec3 inVec_8;
					  inVec_8 = (tmpvar_4 - normalize(xlv_TEXCOORD1));
					  tmpvar_7 = (inVec_8 * inversesqrt(max (0.001, 
					    dot (inVec_8, inVec_8)
					  )));
					  mediump float tmpvar_9;
					  tmpvar_9 = max (0.0, dot (tmpvar_4, tmpvar_7));
					  mediump float tmpvar_10;
					  tmpvar_10 = (1.0 - _Glossiness);
					  mediump float tmpvar_11;
					  tmpvar_11 = max (0.0001, (tmpvar_10 * tmpvar_10));
					  mediump float tmpvar_12;
					  tmpvar_12 = max (((2.0 / 
					    (tmpvar_11 * tmpvar_11)
					  ) - 2.0), 0.0001);
					  specular_6 = sqrt(max (0.0001, (
					    ((tmpvar_12 + 1.0) * pow (max (0.0, dot (tmpvar_3, tmpvar_7)), tmpvar_12))
					   / 
					    (((8.0 * (
					      ((tmpvar_9 * tmpvar_9) * _Glossiness)
					     + 
					      (tmpvar_10 * tmpvar_10)
					    )) * tmpvar_9) + 0.0001)
					  )));
					  mediump float tmpvar_13;
					  tmpvar_13 = clamp (specular_6, 0.0, 100.0);
					  specular_6 = tmpvar_13;
					  mediump vec4 tmpvar_14;
					  tmpvar_14.w = 1.0;
					  tmpvar_14.xyz = (((
					    (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)))
					   + 
					    (tmpvar_13 * mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic)))
					  ) * tmpvar_5) * max (0.0, dot (tmpvar_3, tmpvar_4)));
					  mediump vec4 xlat_varoutput_15;
					  xlat_varoutput_15.xyz = tmpvar_14.xyz;
					  xlat_varoutput_15.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_15;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "SPOT" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec3 lightDir_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_13;
					  tmpvar_13 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  lightDir_6 = tmpvar_13;
					  mediump vec3 tmpvar_14;
					  highp vec3 n_15;
					  n_15 = lightDir_6;
					  highp vec3 tmpvar_16;
					  tmpvar_16 = normalize(n_15);
					  tmpvar_14 = tmpvar_16;
					  lightDir_6 = tmpvar_14;
					  highp vec3 tmpvar_17;
					  tmpvar_17 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_17;
					  highp vec3 norm_18;
					  norm_18 = tmpvar_1;
					  highp vec4 v_19;
					  v_19.x = unity_WorldToObject[0].x;
					  v_19.y = unity_WorldToObject[1].x;
					  v_19.z = unity_WorldToObject[2].x;
					  v_19.w = unity_WorldToObject[3].x;
					  highp vec4 v_20;
					  v_20.x = unity_WorldToObject[0].y;
					  v_20.y = unity_WorldToObject[1].y;
					  v_20.z = unity_WorldToObject[2].y;
					  v_20.w = unity_WorldToObject[3].y;
					  highp vec4 v_21;
					  v_21.x = unity_WorldToObject[0].z;
					  v_21.y = unity_WorldToObject[1].z;
					  v_21.z = unity_WorldToObject[2].z;
					  v_21.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_22;
					  tmpvar_22 = normalize(((
					    (v_19.xyz * norm_18.x)
					   + 
					    (v_20.xyz * norm_18.y)
					  ) + (v_21.xyz * norm_18.z)));
					  normalWorld_4 = tmpvar_22;
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = (unity_WorldToLight * (unity_ObjectToWorld * _glesVertex));
					  xlv_TEXCOORD3 = tmpvar_7;
					  xlv_TEXCOORD4 = tmpvar_14;
					  xlv_TEXCOORD5 = normalWorld_4;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _LightTexture0;
					uniform sampler2D _LightTextureB0;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump float tmpvar_4;
					  tmpvar_4 = dot (xlv_TEXCOORD3.yzw, xlv_TEXCOORD4);
					  mediump vec2 tmpvar_5;
					  tmpvar_5.x = ((tmpvar_4 * tmpvar_4) * (tmpvar_4 * tmpvar_4));
					  tmpvar_5.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (unity_NHxRoughness, tmpvar_5);
					  c_1 = (((tmpvar_3 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_6.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic))
					  )) * _LightColor0.xyz);
					  lowp vec4 tmpvar_7;
					  highp vec2 P_8;
					  P_8 = ((xlv_TEXCOORD1.xy / xlv_TEXCOORD1.w) + 0.5);
					  tmpvar_7 = texture2D (_LightTexture0, P_8);
					  highp float tmpvar_9;
					  tmpvar_9 = dot (xlv_TEXCOORD1.xyz, xlv_TEXCOORD1.xyz);
					  lowp vec4 tmpvar_10;
					  tmpvar_10 = texture2D (_LightTextureB0, vec2(tmpvar_9));
					  c_1 = (c_1 * ((
					    (float((xlv_TEXCOORD1.z > 0.0)) * tmpvar_7.w)
					   * tmpvar_10.w) * max (0.0, 
					    dot (xlv_TEXCOORD5, xlv_TEXCOORD4)
					  )));
					  mediump vec4 tmpvar_11;
					  tmpvar_11.w = 1.0;
					  tmpvar_11.xyz = c_1;
					  mediump vec4 xlat_varoutput_12;
					  xlat_varoutput_12.xyz = tmpvar_11.xyz;
					  xlat_varoutput_12.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_12;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "SPOT" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  highp vec3 lightDir_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_13;
					  highp vec3 n_14;
					  n_14 = (tmpvar_8.xyz - _WorldSpaceCameraPos);
					  tmpvar_13 = n_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_7.xyz = tmpvar_19;
					  highp vec3 tmpvar_20;
					  tmpvar_20 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  mediump vec3 tmpvar_21;
					  tmpvar_21 = tmpvar_20;
					  lightDir_4 = tmpvar_21;
					  tmpvar_5.w = lightDir_4.x;
					  tmpvar_6.w = lightDir_4.y;
					  tmpvar_7.w = lightDir_4.z;
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = tmpvar_13;
					  xlv_TEXCOORD2 = tmpvar_5;
					  xlv_TEXCOORD2_1 = tmpvar_6;
					  xlv_TEXCOORD2_2 = tmpvar_7;
					  xlv_TEXCOORD5 = (unity_WorldToLight * (unity_ObjectToWorld * _glesVertex));
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _LightTexture0;
					uniform sampler2D _LightTextureB0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec4 xlv_TEXCOORD5;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_4;
					  tmpvar_4.x = xlv_TEXCOORD2.w;
					  tmpvar_4.y = xlv_TEXCOORD2_1.w;
					  tmpvar_4.z = xlv_TEXCOORD2_2.w;
					  lowp vec4 tmpvar_5;
					  highp vec2 P_6;
					  P_6 = ((xlv_TEXCOORD5.xy / xlv_TEXCOORD5.w) + 0.5);
					  tmpvar_5 = texture2D (_LightTexture0, P_6);
					  highp float tmpvar_7;
					  tmpvar_7 = dot (xlv_TEXCOORD5.xyz, xlv_TEXCOORD5.xyz);
					  lowp vec4 tmpvar_8;
					  tmpvar_8 = texture2D (_LightTextureB0, vec2(tmpvar_7));
					  mediump float atten_9;
					  atten_9 = ((float(
					    (xlv_TEXCOORD5.z > 0.0)
					  ) * tmpvar_5.w) * tmpvar_8.w);
					  mediump vec3 tmpvar_10;
					  tmpvar_10 = _LightColor0.xyz;
					  mediump vec3 tmpvar_11;
					  tmpvar_11 = normalize(tmpvar_4);
					  tmpvar_10 = (tmpvar_10 * atten_9);
					  mediump float specular_12;
					  mediump vec3 tmpvar_13;
					  mediump vec3 inVec_14;
					  inVec_14 = (tmpvar_11 - normalize(xlv_TEXCOORD1));
					  tmpvar_13 = (inVec_14 * inversesqrt(max (0.001, 
					    dot (inVec_14, inVec_14)
					  )));
					  mediump float tmpvar_15;
					  tmpvar_15 = max (0.0, dot (tmpvar_11, tmpvar_13));
					  mediump float tmpvar_16;
					  tmpvar_16 = (1.0 - _Glossiness);
					  mediump float tmpvar_17;
					  tmpvar_17 = max (0.0001, (tmpvar_16 * tmpvar_16));
					  mediump float tmpvar_18;
					  tmpvar_18 = max (((2.0 / 
					    (tmpvar_17 * tmpvar_17)
					  ) - 2.0), 0.0001);
					  specular_12 = sqrt(max (0.0001, (
					    ((tmpvar_18 + 1.0) * pow (max (0.0, dot (tmpvar_3, tmpvar_13)), tmpvar_18))
					   / 
					    (((8.0 * (
					      ((tmpvar_15 * tmpvar_15) * _Glossiness)
					     + 
					      (tmpvar_16 * tmpvar_16)
					    )) * tmpvar_15) + 0.0001)
					  )));
					  mediump float tmpvar_19;
					  tmpvar_19 = clamp (specular_12, 0.0, 100.0);
					  specular_12 = tmpvar_19;
					  mediump vec4 tmpvar_20;
					  tmpvar_20.w = 1.0;
					  tmpvar_20.xyz = (((
					    (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)))
					   + 
					    (tmpvar_19 * mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic)))
					  ) * tmpvar_10) * max (0.0, dot (tmpvar_3, tmpvar_11)));
					  mediump vec4 xlat_varoutput_21;
					  xlat_varoutput_21.xyz = tmpvar_20.xyz;
					  xlat_varoutput_21.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_21;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "SPOT" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  highp vec3 lightDir_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_13;
					  highp vec3 n_14;
					  n_14 = (tmpvar_8.xyz - _WorldSpaceCameraPos);
					  tmpvar_13 = n_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_7.xyz = tmpvar_19;
					  highp vec3 tmpvar_20;
					  tmpvar_20 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  mediump vec3 tmpvar_21;
					  tmpvar_21 = tmpvar_20;
					  lightDir_4 = tmpvar_21;
					  tmpvar_5.w = lightDir_4.x;
					  tmpvar_6.w = lightDir_4.y;
					  tmpvar_7.w = lightDir_4.z;
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = tmpvar_13;
					  xlv_TEXCOORD2 = tmpvar_5;
					  xlv_TEXCOORD2_1 = tmpvar_6;
					  xlv_TEXCOORD2_2 = tmpvar_7;
					  xlv_TEXCOORD5 = (unity_WorldToLight * (unity_ObjectToWorld * _glesVertex));
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _LightTexture0;
					uniform sampler2D _LightTextureB0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec4 xlv_TEXCOORD5;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_4;
					  tmpvar_4.x = xlv_TEXCOORD2.w;
					  tmpvar_4.y = xlv_TEXCOORD2_1.w;
					  tmpvar_4.z = xlv_TEXCOORD2_2.w;
					  lowp vec4 tmpvar_5;
					  highp vec2 P_6;
					  P_6 = ((xlv_TEXCOORD5.xy / xlv_TEXCOORD5.w) + 0.5);
					  tmpvar_5 = texture2D (_LightTexture0, P_6);
					  highp float tmpvar_7;
					  tmpvar_7 = dot (xlv_TEXCOORD5.xyz, xlv_TEXCOORD5.xyz);
					  lowp vec4 tmpvar_8;
					  tmpvar_8 = texture2D (_LightTextureB0, vec2(tmpvar_7));
					  mediump float atten_9;
					  atten_9 = ((float(
					    (xlv_TEXCOORD5.z > 0.0)
					  ) * tmpvar_5.w) * tmpvar_8.w);
					  mediump vec3 tmpvar_10;
					  tmpvar_10 = _LightColor0.xyz;
					  mediump vec3 tmpvar_11;
					  tmpvar_11 = normalize(tmpvar_4);
					  tmpvar_10 = (tmpvar_10 * atten_9);
					  mediump float specular_12;
					  mediump vec3 tmpvar_13;
					  mediump vec3 inVec_14;
					  inVec_14 = (tmpvar_11 - normalize(xlv_TEXCOORD1));
					  tmpvar_13 = (inVec_14 * inversesqrt(max (0.001, 
					    dot (inVec_14, inVec_14)
					  )));
					  mediump float tmpvar_15;
					  tmpvar_15 = max (0.0, dot (tmpvar_11, tmpvar_13));
					  mediump float tmpvar_16;
					  tmpvar_16 = (1.0 - _Glossiness);
					  mediump float tmpvar_17;
					  tmpvar_17 = max (0.0001, (tmpvar_16 * tmpvar_16));
					  mediump float tmpvar_18;
					  tmpvar_18 = max (((2.0 / 
					    (tmpvar_17 * tmpvar_17)
					  ) - 2.0), 0.0001);
					  specular_12 = sqrt(max (0.0001, (
					    ((tmpvar_18 + 1.0) * pow (max (0.0, dot (tmpvar_3, tmpvar_13)), tmpvar_18))
					   / 
					    (((8.0 * (
					      ((tmpvar_15 * tmpvar_15) * _Glossiness)
					     + 
					      (tmpvar_16 * tmpvar_16)
					    )) * tmpvar_15) + 0.0001)
					  )));
					  mediump float tmpvar_19;
					  tmpvar_19 = clamp (specular_12, 0.0, 100.0);
					  specular_12 = tmpvar_19;
					  mediump vec4 tmpvar_20;
					  tmpvar_20.w = 1.0;
					  tmpvar_20.xyz = (((
					    (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)))
					   + 
					    (tmpvar_19 * mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic)))
					  ) * tmpvar_10) * max (0.0, dot (tmpvar_3, tmpvar_11)));
					  mediump vec4 xlat_varoutput_21;
					  xlat_varoutput_21.xyz = tmpvar_20.xyz;
					  xlat_varoutput_21.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_21;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "POINT_COOKIE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec3 lightDir_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_13;
					  tmpvar_13 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  lightDir_6 = tmpvar_13;
					  mediump vec3 tmpvar_14;
					  highp vec3 n_15;
					  n_15 = lightDir_6;
					  highp vec3 tmpvar_16;
					  tmpvar_16 = normalize(n_15);
					  tmpvar_14 = tmpvar_16;
					  lightDir_6 = tmpvar_14;
					  highp vec3 tmpvar_17;
					  tmpvar_17 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_17;
					  highp vec3 norm_18;
					  norm_18 = tmpvar_1;
					  highp vec4 v_19;
					  v_19.x = unity_WorldToObject[0].x;
					  v_19.y = unity_WorldToObject[1].x;
					  v_19.z = unity_WorldToObject[2].x;
					  v_19.w = unity_WorldToObject[3].x;
					  highp vec4 v_20;
					  v_20.x = unity_WorldToObject[0].y;
					  v_20.y = unity_WorldToObject[1].y;
					  v_20.z = unity_WorldToObject[2].y;
					  v_20.w = unity_WorldToObject[3].y;
					  highp vec4 v_21;
					  v_21.x = unity_WorldToObject[0].z;
					  v_21.y = unity_WorldToObject[1].z;
					  v_21.z = unity_WorldToObject[2].z;
					  v_21.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_22;
					  tmpvar_22 = normalize(((
					    (v_19.xyz * norm_18.x)
					   + 
					    (v_20.xyz * norm_18.y)
					  ) + (v_21.xyz * norm_18.z)));
					  normalWorld_4 = tmpvar_22;
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = (unity_WorldToLight * (unity_ObjectToWorld * _glesVertex)).xyz;
					  xlv_TEXCOORD3 = tmpvar_7;
					  xlv_TEXCOORD4 = tmpvar_14;
					  xlv_TEXCOORD5 = normalWorld_4;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform lowp samplerCube _LightTexture0;
					uniform sampler2D _LightTextureB0;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump float tmpvar_4;
					  tmpvar_4 = dot (xlv_TEXCOORD3.yzw, xlv_TEXCOORD4);
					  mediump vec2 tmpvar_5;
					  tmpvar_5.x = ((tmpvar_4 * tmpvar_4) * (tmpvar_4 * tmpvar_4));
					  tmpvar_5.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (unity_NHxRoughness, tmpvar_5);
					  c_1 = (((tmpvar_3 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_6.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic))
					  )) * _LightColor0.xyz);
					  highp float tmpvar_7;
					  tmpvar_7 = dot (xlv_TEXCOORD1, xlv_TEXCOORD1);
					  lowp vec4 tmpvar_8;
					  tmpvar_8 = texture2D (_LightTextureB0, vec2(tmpvar_7));
					  lowp vec4 tmpvar_9;
					  tmpvar_9 = textureCube (_LightTexture0, xlv_TEXCOORD1);
					  c_1 = (c_1 * ((tmpvar_8.w * tmpvar_9.w) * max (0.0, 
					    dot (xlv_TEXCOORD5, xlv_TEXCOORD4)
					  )));
					  mediump vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = c_1;
					  mediump vec4 xlat_varoutput_11;
					  xlat_varoutput_11.xyz = tmpvar_10.xyz;
					  xlat_varoutput_11.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_11;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "POINT_COOKIE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  highp vec3 lightDir_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_13;
					  highp vec3 n_14;
					  n_14 = (tmpvar_8.xyz - _WorldSpaceCameraPos);
					  tmpvar_13 = n_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_7.xyz = tmpvar_19;
					  highp vec3 tmpvar_20;
					  tmpvar_20 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  mediump vec3 tmpvar_21;
					  tmpvar_21 = tmpvar_20;
					  lightDir_4 = tmpvar_21;
					  tmpvar_5.w = lightDir_4.x;
					  tmpvar_6.w = lightDir_4.y;
					  tmpvar_7.w = lightDir_4.z;
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = tmpvar_13;
					  xlv_TEXCOORD2 = tmpvar_5;
					  xlv_TEXCOORD2_1 = tmpvar_6;
					  xlv_TEXCOORD2_2 = tmpvar_7;
					  xlv_TEXCOORD5 = (unity_WorldToLight * (unity_ObjectToWorld * _glesVertex)).xyz;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform lowp samplerCube _LightTexture0;
					uniform sampler2D _LightTextureB0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_4;
					  tmpvar_4.x = xlv_TEXCOORD2.w;
					  tmpvar_4.y = xlv_TEXCOORD2_1.w;
					  tmpvar_4.z = xlv_TEXCOORD2_2.w;
					  highp float tmpvar_5;
					  tmpvar_5 = dot (xlv_TEXCOORD5, xlv_TEXCOORD5);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (_LightTextureB0, vec2(tmpvar_5));
					  lowp vec4 tmpvar_7;
					  tmpvar_7 = textureCube (_LightTexture0, xlv_TEXCOORD5);
					  mediump float atten_8;
					  atten_8 = (tmpvar_6.w * tmpvar_7.w);
					  mediump vec3 tmpvar_9;
					  tmpvar_9 = _LightColor0.xyz;
					  mediump vec3 tmpvar_10;
					  tmpvar_10 = normalize(tmpvar_4);
					  tmpvar_9 = (tmpvar_9 * atten_8);
					  mediump float specular_11;
					  mediump vec3 tmpvar_12;
					  mediump vec3 inVec_13;
					  inVec_13 = (tmpvar_10 - normalize(xlv_TEXCOORD1));
					  tmpvar_12 = (inVec_13 * inversesqrt(max (0.001, 
					    dot (inVec_13, inVec_13)
					  )));
					  mediump float tmpvar_14;
					  tmpvar_14 = max (0.0, dot (tmpvar_10, tmpvar_12));
					  mediump float tmpvar_15;
					  tmpvar_15 = (1.0 - _Glossiness);
					  mediump float tmpvar_16;
					  tmpvar_16 = max (0.0001, (tmpvar_15 * tmpvar_15));
					  mediump float tmpvar_17;
					  tmpvar_17 = max (((2.0 / 
					    (tmpvar_16 * tmpvar_16)
					  ) - 2.0), 0.0001);
					  specular_11 = sqrt(max (0.0001, (
					    ((tmpvar_17 + 1.0) * pow (max (0.0, dot (tmpvar_3, tmpvar_12)), tmpvar_17))
					   / 
					    (((8.0 * (
					      ((tmpvar_14 * tmpvar_14) * _Glossiness)
					     + 
					      (tmpvar_15 * tmpvar_15)
					    )) * tmpvar_14) + 0.0001)
					  )));
					  mediump float tmpvar_18;
					  tmpvar_18 = clamp (specular_11, 0.0, 100.0);
					  specular_11 = tmpvar_18;
					  mediump vec4 tmpvar_19;
					  tmpvar_19.w = 1.0;
					  tmpvar_19.xyz = (((
					    (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)))
					   + 
					    (tmpvar_18 * mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic)))
					  ) * tmpvar_9) * max (0.0, dot (tmpvar_3, tmpvar_10)));
					  mediump vec4 xlat_varoutput_20;
					  xlat_varoutput_20.xyz = tmpvar_19.xyz;
					  xlat_varoutput_20.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_20;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "POINT_COOKIE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  highp vec3 lightDir_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_13;
					  highp vec3 n_14;
					  n_14 = (tmpvar_8.xyz - _WorldSpaceCameraPos);
					  tmpvar_13 = n_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_7.xyz = tmpvar_19;
					  highp vec3 tmpvar_20;
					  tmpvar_20 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  mediump vec3 tmpvar_21;
					  tmpvar_21 = tmpvar_20;
					  lightDir_4 = tmpvar_21;
					  tmpvar_5.w = lightDir_4.x;
					  tmpvar_6.w = lightDir_4.y;
					  tmpvar_7.w = lightDir_4.z;
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = tmpvar_13;
					  xlv_TEXCOORD2 = tmpvar_5;
					  xlv_TEXCOORD2_1 = tmpvar_6;
					  xlv_TEXCOORD2_2 = tmpvar_7;
					  xlv_TEXCOORD5 = (unity_WorldToLight * (unity_ObjectToWorld * _glesVertex)).xyz;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform lowp samplerCube _LightTexture0;
					uniform sampler2D _LightTextureB0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_4;
					  tmpvar_4.x = xlv_TEXCOORD2.w;
					  tmpvar_4.y = xlv_TEXCOORD2_1.w;
					  tmpvar_4.z = xlv_TEXCOORD2_2.w;
					  highp float tmpvar_5;
					  tmpvar_5 = dot (xlv_TEXCOORD5, xlv_TEXCOORD5);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (_LightTextureB0, vec2(tmpvar_5));
					  lowp vec4 tmpvar_7;
					  tmpvar_7 = textureCube (_LightTexture0, xlv_TEXCOORD5);
					  mediump float atten_8;
					  atten_8 = (tmpvar_6.w * tmpvar_7.w);
					  mediump vec3 tmpvar_9;
					  tmpvar_9 = _LightColor0.xyz;
					  mediump vec3 tmpvar_10;
					  tmpvar_10 = normalize(tmpvar_4);
					  tmpvar_9 = (tmpvar_9 * atten_8);
					  mediump float specular_11;
					  mediump vec3 tmpvar_12;
					  mediump vec3 inVec_13;
					  inVec_13 = (tmpvar_10 - normalize(xlv_TEXCOORD1));
					  tmpvar_12 = (inVec_13 * inversesqrt(max (0.001, 
					    dot (inVec_13, inVec_13)
					  )));
					  mediump float tmpvar_14;
					  tmpvar_14 = max (0.0, dot (tmpvar_10, tmpvar_12));
					  mediump float tmpvar_15;
					  tmpvar_15 = (1.0 - _Glossiness);
					  mediump float tmpvar_16;
					  tmpvar_16 = max (0.0001, (tmpvar_15 * tmpvar_15));
					  mediump float tmpvar_17;
					  tmpvar_17 = max (((2.0 / 
					    (tmpvar_16 * tmpvar_16)
					  ) - 2.0), 0.0001);
					  specular_11 = sqrt(max (0.0001, (
					    ((tmpvar_17 + 1.0) * pow (max (0.0, dot (tmpvar_3, tmpvar_12)), tmpvar_17))
					   / 
					    (((8.0 * (
					      ((tmpvar_14 * tmpvar_14) * _Glossiness)
					     + 
					      (tmpvar_15 * tmpvar_15)
					    )) * tmpvar_14) + 0.0001)
					  )));
					  mediump float tmpvar_18;
					  tmpvar_18 = clamp (specular_11, 0.0, 100.0);
					  specular_11 = tmpvar_18;
					  mediump vec4 tmpvar_19;
					  tmpvar_19.w = 1.0;
					  tmpvar_19.xyz = (((
					    (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)))
					   + 
					    (tmpvar_18 * mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic)))
					  ) * tmpvar_9) * max (0.0, dot (tmpvar_3, tmpvar_10)));
					  mediump vec4 xlat_varoutput_20;
					  xlat_varoutput_20.xyz = tmpvar_19.xyz;
					  xlat_varoutput_20.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_20;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL_COOKIE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec3 lightDir_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_13;
					  tmpvar_13 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  lightDir_6 = tmpvar_13;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  normalWorld_4 = tmpvar_19;
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = (unity_WorldToLight * (unity_ObjectToWorld * _glesVertex)).xy;
					  xlv_TEXCOORD3 = tmpvar_7;
					  xlv_TEXCOORD4 = lightDir_6;
					  xlv_TEXCOORD5 = normalWorld_4;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _LightTexture0;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump float tmpvar_4;
					  tmpvar_4 = dot (xlv_TEXCOORD3.yzw, xlv_TEXCOORD4);
					  mediump vec2 tmpvar_5;
					  tmpvar_5.x = ((tmpvar_4 * tmpvar_4) * (tmpvar_4 * tmpvar_4));
					  tmpvar_5.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (unity_NHxRoughness, tmpvar_5);
					  c_1 = (((tmpvar_3 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_6.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic))
					  )) * _LightColor0.xyz);
					  lowp vec4 tmpvar_7;
					  tmpvar_7 = texture2D (_LightTexture0, xlv_TEXCOORD1);
					  c_1 = (c_1 * (tmpvar_7.w * max (0.0, 
					    dot (xlv_TEXCOORD5, xlv_TEXCOORD4)
					  )));
					  mediump vec4 tmpvar_8;
					  tmpvar_8.w = 1.0;
					  tmpvar_8.xyz = c_1;
					  mediump vec4 xlat_varoutput_9;
					  xlat_varoutput_9.xyz = tmpvar_8.xyz;
					  xlat_varoutput_9.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_9;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL_COOKIE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec2 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 n_13;
					  n_13 = (tmpvar_7.xyz - _WorldSpaceCameraPos);
					  tmpvar_12 = n_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = (_WorldSpaceLightPos0.xyz - (tmpvar_7.xyz * _WorldSpaceLightPos0.w));
					  tmpvar_4.w = tmpvar_19.x;
					  tmpvar_5.w = tmpvar_19.y;
					  tmpvar_6.w = tmpvar_19.z;
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					  xlv_TEXCOORD5 = (unity_WorldToLight * (unity_ObjectToWorld * _glesVertex)).xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _LightTexture0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec2 xlv_TEXCOORD5;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_4;
					  tmpvar_4.x = xlv_TEXCOORD2.w;
					  tmpvar_4.y = xlv_TEXCOORD2_1.w;
					  tmpvar_4.z = xlv_TEXCOORD2_2.w;
					  lowp vec4 tmpvar_5;
					  tmpvar_5 = texture2D (_LightTexture0, xlv_TEXCOORD5);
					  mediump float atten_6;
					  atten_6 = tmpvar_5.w;
					  mediump vec3 tmpvar_7;
					  tmpvar_7 = _LightColor0.xyz;
					  tmpvar_7 = (tmpvar_7 * atten_6);
					  mediump float specular_8;
					  mediump vec3 tmpvar_9;
					  mediump vec3 inVec_10;
					  inVec_10 = (tmpvar_4 - normalize(xlv_TEXCOORD1));
					  tmpvar_9 = (inVec_10 * inversesqrt(max (0.001, 
					    dot (inVec_10, inVec_10)
					  )));
					  mediump float tmpvar_11;
					  tmpvar_11 = max (0.0, dot (tmpvar_4, tmpvar_9));
					  mediump float tmpvar_12;
					  tmpvar_12 = (1.0 - _Glossiness);
					  mediump float tmpvar_13;
					  tmpvar_13 = max (0.0001, (tmpvar_12 * tmpvar_12));
					  mediump float tmpvar_14;
					  tmpvar_14 = max (((2.0 / 
					    (tmpvar_13 * tmpvar_13)
					  ) - 2.0), 0.0001);
					  specular_8 = sqrt(max (0.0001, (
					    ((tmpvar_14 + 1.0) * pow (max (0.0, dot (tmpvar_3, tmpvar_9)), tmpvar_14))
					   / 
					    (((8.0 * (
					      ((tmpvar_11 * tmpvar_11) * _Glossiness)
					     + 
					      (tmpvar_12 * tmpvar_12)
					    )) * tmpvar_11) + 0.0001)
					  )));
					  mediump float tmpvar_15;
					  tmpvar_15 = clamp (specular_8, 0.0, 100.0);
					  specular_8 = tmpvar_15;
					  mediump vec4 tmpvar_16;
					  tmpvar_16.w = 1.0;
					  tmpvar_16.xyz = (((
					    (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)))
					   + 
					    (tmpvar_15 * mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic)))
					  ) * tmpvar_7) * max (0.0, dot (tmpvar_3, tmpvar_4)));
					  mediump vec4 xlat_varoutput_17;
					  xlat_varoutput_17.xyz = tmpvar_16.xyz;
					  xlat_varoutput_17.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_17;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL_COOKIE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec2 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 n_13;
					  n_13 = (tmpvar_7.xyz - _WorldSpaceCameraPos);
					  tmpvar_12 = n_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = (_WorldSpaceLightPos0.xyz - (tmpvar_7.xyz * _WorldSpaceLightPos0.w));
					  tmpvar_4.w = tmpvar_19.x;
					  tmpvar_5.w = tmpvar_19.y;
					  tmpvar_6.w = tmpvar_19.z;
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					  xlv_TEXCOORD5 = (unity_WorldToLight * (unity_ObjectToWorld * _glesVertex)).xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _LightTexture0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec2 xlv_TEXCOORD5;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_4;
					  tmpvar_4.x = xlv_TEXCOORD2.w;
					  tmpvar_4.y = xlv_TEXCOORD2_1.w;
					  tmpvar_4.z = xlv_TEXCOORD2_2.w;
					  lowp vec4 tmpvar_5;
					  tmpvar_5 = texture2D (_LightTexture0, xlv_TEXCOORD5);
					  mediump float atten_6;
					  atten_6 = tmpvar_5.w;
					  mediump vec3 tmpvar_7;
					  tmpvar_7 = _LightColor0.xyz;
					  tmpvar_7 = (tmpvar_7 * atten_6);
					  mediump float specular_8;
					  mediump vec3 tmpvar_9;
					  mediump vec3 inVec_10;
					  inVec_10 = (tmpvar_4 - normalize(xlv_TEXCOORD1));
					  tmpvar_9 = (inVec_10 * inversesqrt(max (0.001, 
					    dot (inVec_10, inVec_10)
					  )));
					  mediump float tmpvar_11;
					  tmpvar_11 = max (0.0, dot (tmpvar_4, tmpvar_9));
					  mediump float tmpvar_12;
					  tmpvar_12 = (1.0 - _Glossiness);
					  mediump float tmpvar_13;
					  tmpvar_13 = max (0.0001, (tmpvar_12 * tmpvar_12));
					  mediump float tmpvar_14;
					  tmpvar_14 = max (((2.0 / 
					    (tmpvar_13 * tmpvar_13)
					  ) - 2.0), 0.0001);
					  specular_8 = sqrt(max (0.0001, (
					    ((tmpvar_14 + 1.0) * pow (max (0.0, dot (tmpvar_3, tmpvar_9)), tmpvar_14))
					   / 
					    (((8.0 * (
					      ((tmpvar_11 * tmpvar_11) * _Glossiness)
					     + 
					      (tmpvar_12 * tmpvar_12)
					    )) * tmpvar_11) + 0.0001)
					  )));
					  mediump float tmpvar_15;
					  tmpvar_15 = clamp (specular_8, 0.0, 100.0);
					  specular_8 = tmpvar_15;
					  mediump vec4 tmpvar_16;
					  tmpvar_16.w = 1.0;
					  tmpvar_16.xyz = (((
					    (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)))
					   + 
					    (tmpvar_15 * mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic)))
					  ) * tmpvar_7) * max (0.0, dot (tmpvar_3, tmpvar_4)));
					  mediump vec4 xlat_varoutput_17;
					  xlat_varoutput_17.xyz = tmpvar_16.xyz;
					  xlat_varoutput_17.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_17;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "SPOT" "SHADOWS_DEPTH" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec3 lightDir_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_13;
					  tmpvar_13 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  lightDir_6 = tmpvar_13;
					  mediump vec3 tmpvar_14;
					  highp vec3 n_15;
					  n_15 = lightDir_6;
					  highp vec3 tmpvar_16;
					  tmpvar_16 = normalize(n_15);
					  tmpvar_14 = tmpvar_16;
					  lightDir_6 = tmpvar_14;
					  highp vec3 tmpvar_17;
					  tmpvar_17 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_17;
					  highp vec3 norm_18;
					  norm_18 = tmpvar_1;
					  highp vec4 v_19;
					  v_19.x = unity_WorldToObject[0].x;
					  v_19.y = unity_WorldToObject[1].x;
					  v_19.z = unity_WorldToObject[2].x;
					  v_19.w = unity_WorldToObject[3].x;
					  highp vec4 v_20;
					  v_20.x = unity_WorldToObject[0].y;
					  v_20.y = unity_WorldToObject[1].y;
					  v_20.z = unity_WorldToObject[2].y;
					  v_20.w = unity_WorldToObject[3].y;
					  highp vec4 v_21;
					  v_21.x = unity_WorldToObject[0].z;
					  v_21.y = unity_WorldToObject[1].z;
					  v_21.z = unity_WorldToObject[2].z;
					  v_21.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_22;
					  tmpvar_22 = normalize(((
					    (v_19.xyz * norm_18.x)
					   + 
					    (v_20.xyz * norm_18.y)
					  ) + (v_21.xyz * norm_18.z)));
					  normalWorld_4 = tmpvar_22;
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  highp vec4 tmpvar_23;
					  tmpvar_23 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD1 = (unity_WorldToLight * tmpvar_23);
					  xlv_TEXCOORD2 = (unity_WorldToShadow[0] * tmpvar_23);
					  xlv_TEXCOORD3 = tmpvar_7;
					  xlv_TEXCOORD4 = tmpvar_14;
					  xlv_TEXCOORD5 = normalWorld_4;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp sampler2D _ShadowMapTexture;
					uniform sampler2D _LightTexture0;
					uniform sampler2D _LightTextureB0;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump float tmpvar_4;
					  tmpvar_4 = dot (xlv_TEXCOORD3.yzw, xlv_TEXCOORD4);
					  mediump vec2 tmpvar_5;
					  tmpvar_5.x = ((tmpvar_4 * tmpvar_4) * (tmpvar_4 * tmpvar_4));
					  tmpvar_5.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (unity_NHxRoughness, tmpvar_5);
					  c_1 = (((tmpvar_3 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_6.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic))
					  )) * _LightColor0.xyz);
					  lowp vec4 tmpvar_7;
					  highp vec2 P_8;
					  P_8 = ((xlv_TEXCOORD1.xy / xlv_TEXCOORD1.w) + 0.5);
					  tmpvar_7 = texture2D (_LightTexture0, P_8);
					  highp float tmpvar_9;
					  tmpvar_9 = dot (xlv_TEXCOORD1.xyz, xlv_TEXCOORD1.xyz);
					  lowp vec4 tmpvar_10;
					  tmpvar_10 = texture2D (_LightTextureB0, vec2(tmpvar_9));
					  lowp float tmpvar_11;
					  highp vec4 tmpvar_12;
					  tmpvar_12 = texture2DProj (_ShadowMapTexture, xlv_TEXCOORD2);
					  mediump float tmpvar_13;
					  if ((tmpvar_12.x < (xlv_TEXCOORD2.z / xlv_TEXCOORD2.w))) {
					    tmpvar_13 = _LightShadowData.x;
					  } else {
					    tmpvar_13 = 1.0;
					  };
					  tmpvar_11 = tmpvar_13;
					  c_1 = (c_1 * ((
					    ((float((xlv_TEXCOORD1.z > 0.0)) * tmpvar_7.w) * tmpvar_10.w)
					   * tmpvar_11) * max (0.0, 
					    dot (xlv_TEXCOORD5, xlv_TEXCOORD4)
					  )));
					  mediump vec4 tmpvar_14;
					  tmpvar_14.w = 1.0;
					  tmpvar_14.xyz = c_1;
					  mediump vec4 xlat_varoutput_15;
					  xlat_varoutput_15.xyz = tmpvar_14.xyz;
					  xlat_varoutput_15.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_15;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "SPOT" "SHADOWS_DEPTH" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec4 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  highp vec3 lightDir_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_13;
					  highp vec3 n_14;
					  n_14 = (tmpvar_8.xyz - _WorldSpaceCameraPos);
					  tmpvar_13 = n_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_7.xyz = tmpvar_19;
					  highp vec3 tmpvar_20;
					  tmpvar_20 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  mediump vec3 tmpvar_21;
					  tmpvar_21 = tmpvar_20;
					  lightDir_4 = tmpvar_21;
					  tmpvar_5.w = lightDir_4.x;
					  tmpvar_6.w = lightDir_4.y;
					  tmpvar_7.w = lightDir_4.z;
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = tmpvar_13;
					  xlv_TEXCOORD2 = tmpvar_5;
					  xlv_TEXCOORD2_1 = tmpvar_6;
					  xlv_TEXCOORD2_2 = tmpvar_7;
					  highp vec4 tmpvar_22;
					  tmpvar_22 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD5 = (unity_WorldToLight * tmpvar_22);
					  xlv_TEXCOORD6 = (unity_WorldToShadow[0] * tmpvar_22);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp sampler2D _ShadowMapTexture;
					uniform sampler2D _LightTexture0;
					uniform sampler2D _LightTextureB0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec4 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  mediump vec3 tmpvar_4;
					  tmpvar_4 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic));
					  tmpvar_3 = (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)));
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_6;
					  tmpvar_6 = normalize(xlv_TEXCOORD1);
					  mediump vec3 tmpvar_7;
					  tmpvar_7.x = xlv_TEXCOORD2.w;
					  tmpvar_7.y = xlv_TEXCOORD2_1.w;
					  tmpvar_7.z = xlv_TEXCOORD2_2.w;
					  lowp vec4 tmpvar_8;
					  highp vec2 P_9;
					  P_9 = ((xlv_TEXCOORD5.xy / xlv_TEXCOORD5.w) + 0.5);
					  tmpvar_8 = texture2D (_LightTexture0, P_9);
					  highp float tmpvar_10;
					  tmpvar_10 = dot (xlv_TEXCOORD5.xyz, xlv_TEXCOORD5.xyz);
					  lowp vec4 tmpvar_11;
					  tmpvar_11 = texture2D (_LightTextureB0, vec2(tmpvar_10));
					  lowp float tmpvar_12;
					  highp vec4 tmpvar_13;
					  tmpvar_13 = texture2DProj (_ShadowMapTexture, xlv_TEXCOORD6);
					  mediump float tmpvar_14;
					  if ((tmpvar_13.x < (xlv_TEXCOORD6.z / xlv_TEXCOORD6.w))) {
					    tmpvar_14 = _LightShadowData.x;
					  } else {
					    tmpvar_14 = 1.0;
					  };
					  tmpvar_12 = tmpvar_14;
					  mediump float atten_15;
					  atten_15 = (((
					    float((xlv_TEXCOORD5.z > 0.0))
					   * tmpvar_8.w) * tmpvar_11.w) * tmpvar_12);
					  mediump vec3 tmpvar_16;
					  tmpvar_16 = _LightColor0.xyz;
					  mediump vec3 tmpvar_17;
					  tmpvar_17 = normalize(tmpvar_7);
					  tmpvar_16 = (tmpvar_16 * atten_15);
					  mediump float specular_18;
					  mediump vec3 tmpvar_19;
					  mediump vec3 inVec_20;
					  inVec_20 = (tmpvar_17 - tmpvar_6);
					  tmpvar_19 = (inVec_20 * inversesqrt(max (0.001, 
					    dot (inVec_20, inVec_20)
					  )));
					  mediump float tmpvar_21;
					  tmpvar_21 = max (0.0, dot (tmpvar_17, tmpvar_19));
					  mediump float tmpvar_22;
					  tmpvar_22 = (1.0 - _Glossiness);
					  mediump float tmpvar_23;
					  tmpvar_23 = max (0.0001, (tmpvar_22 * tmpvar_22));
					  mediump float tmpvar_24;
					  tmpvar_24 = max (((2.0 / 
					    (tmpvar_23 * tmpvar_23)
					  ) - 2.0), 0.0001);
					  specular_18 = sqrt(max (0.0001, (
					    ((tmpvar_24 + 1.0) * pow (max (0.0, dot (tmpvar_5, tmpvar_19)), tmpvar_24))
					   / 
					    (((8.0 * (
					      ((tmpvar_21 * tmpvar_21) * _Glossiness)
					     + 
					      (tmpvar_22 * tmpvar_22)
					    )) * tmpvar_21) + 0.0001)
					  )));
					  mediump float tmpvar_25;
					  tmpvar_25 = clamp (specular_18, 0.0, 100.0);
					  specular_18 = tmpvar_25;
					  mediump vec4 tmpvar_26;
					  tmpvar_26.w = 1.0;
					  tmpvar_26.xyz = (((tmpvar_3 + 
					    (tmpvar_25 * tmpvar_4)
					  ) * tmpvar_16) * max (0.0, dot (tmpvar_5, tmpvar_17)));
					  mediump vec4 xlat_varoutput_27;
					  xlat_varoutput_27.xyz = tmpvar_26.xyz;
					  xlat_varoutput_27.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_27;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "SPOT" "SHADOWS_DEPTH" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec4 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  highp vec3 lightDir_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_13;
					  highp vec3 n_14;
					  n_14 = (tmpvar_8.xyz - _WorldSpaceCameraPos);
					  tmpvar_13 = n_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_7.xyz = tmpvar_19;
					  highp vec3 tmpvar_20;
					  tmpvar_20 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  mediump vec3 tmpvar_21;
					  tmpvar_21 = tmpvar_20;
					  lightDir_4 = tmpvar_21;
					  tmpvar_5.w = lightDir_4.x;
					  tmpvar_6.w = lightDir_4.y;
					  tmpvar_7.w = lightDir_4.z;
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = tmpvar_13;
					  xlv_TEXCOORD2 = tmpvar_5;
					  xlv_TEXCOORD2_1 = tmpvar_6;
					  xlv_TEXCOORD2_2 = tmpvar_7;
					  highp vec4 tmpvar_22;
					  tmpvar_22 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD5 = (unity_WorldToLight * tmpvar_22);
					  xlv_TEXCOORD6 = (unity_WorldToShadow[0] * tmpvar_22);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp sampler2D _ShadowMapTexture;
					uniform sampler2D _LightTexture0;
					uniform sampler2D _LightTextureB0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec4 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  mediump vec3 tmpvar_4;
					  tmpvar_4 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic));
					  tmpvar_3 = (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)));
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_6;
					  tmpvar_6 = normalize(xlv_TEXCOORD1);
					  mediump vec3 tmpvar_7;
					  tmpvar_7.x = xlv_TEXCOORD2.w;
					  tmpvar_7.y = xlv_TEXCOORD2_1.w;
					  tmpvar_7.z = xlv_TEXCOORD2_2.w;
					  lowp vec4 tmpvar_8;
					  highp vec2 P_9;
					  P_9 = ((xlv_TEXCOORD5.xy / xlv_TEXCOORD5.w) + 0.5);
					  tmpvar_8 = texture2D (_LightTexture0, P_9);
					  highp float tmpvar_10;
					  tmpvar_10 = dot (xlv_TEXCOORD5.xyz, xlv_TEXCOORD5.xyz);
					  lowp vec4 tmpvar_11;
					  tmpvar_11 = texture2D (_LightTextureB0, vec2(tmpvar_10));
					  lowp float tmpvar_12;
					  highp vec4 tmpvar_13;
					  tmpvar_13 = texture2DProj (_ShadowMapTexture, xlv_TEXCOORD6);
					  mediump float tmpvar_14;
					  if ((tmpvar_13.x < (xlv_TEXCOORD6.z / xlv_TEXCOORD6.w))) {
					    tmpvar_14 = _LightShadowData.x;
					  } else {
					    tmpvar_14 = 1.0;
					  };
					  tmpvar_12 = tmpvar_14;
					  mediump float atten_15;
					  atten_15 = (((
					    float((xlv_TEXCOORD5.z > 0.0))
					   * tmpvar_8.w) * tmpvar_11.w) * tmpvar_12);
					  mediump vec3 tmpvar_16;
					  tmpvar_16 = _LightColor0.xyz;
					  mediump vec3 tmpvar_17;
					  tmpvar_17 = normalize(tmpvar_7);
					  tmpvar_16 = (tmpvar_16 * atten_15);
					  mediump float specular_18;
					  mediump vec3 tmpvar_19;
					  mediump vec3 inVec_20;
					  inVec_20 = (tmpvar_17 - tmpvar_6);
					  tmpvar_19 = (inVec_20 * inversesqrt(max (0.001, 
					    dot (inVec_20, inVec_20)
					  )));
					  mediump float tmpvar_21;
					  tmpvar_21 = max (0.0, dot (tmpvar_17, tmpvar_19));
					  mediump float tmpvar_22;
					  tmpvar_22 = (1.0 - _Glossiness);
					  mediump float tmpvar_23;
					  tmpvar_23 = max (0.0001, (tmpvar_22 * tmpvar_22));
					  mediump float tmpvar_24;
					  tmpvar_24 = max (((2.0 / 
					    (tmpvar_23 * tmpvar_23)
					  ) - 2.0), 0.0001);
					  specular_18 = sqrt(max (0.0001, (
					    ((tmpvar_24 + 1.0) * pow (max (0.0, dot (tmpvar_5, tmpvar_19)), tmpvar_24))
					   / 
					    (((8.0 * (
					      ((tmpvar_21 * tmpvar_21) * _Glossiness)
					     + 
					      (tmpvar_22 * tmpvar_22)
					    )) * tmpvar_21) + 0.0001)
					  )));
					  mediump float tmpvar_25;
					  tmpvar_25 = clamp (specular_18, 0.0, 100.0);
					  specular_18 = tmpvar_25;
					  mediump vec4 tmpvar_26;
					  tmpvar_26.w = 1.0;
					  tmpvar_26.xyz = (((tmpvar_3 + 
					    (tmpvar_25 * tmpvar_4)
					  ) * tmpvar_16) * max (0.0, dot (tmpvar_5, tmpvar_17)));
					  mediump vec4 xlat_varoutput_27;
					  xlat_varoutput_27.xyz = tmpvar_26.xyz;
					  xlat_varoutput_27.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_27;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec3 lightDir_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_13;
					  tmpvar_13 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  lightDir_6 = tmpvar_13;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  normalWorld_4 = tmpvar_19;
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = (unity_WorldToShadow[0] * (unity_ObjectToWorld * _glesVertex));
					  xlv_TEXCOORD3 = tmpvar_7;
					  xlv_TEXCOORD4 = lightDir_6;
					  xlv_TEXCOORD5 = normalWorld_4;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp sampler2D _ShadowMapTexture;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump float tmpvar_4;
					  tmpvar_4 = dot (xlv_TEXCOORD3.yzw, xlv_TEXCOORD4);
					  mediump vec2 tmpvar_5;
					  tmpvar_5.x = ((tmpvar_4 * tmpvar_4) * (tmpvar_4 * tmpvar_4));
					  tmpvar_5.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (unity_NHxRoughness, tmpvar_5);
					  c_1 = (((tmpvar_3 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_6.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic))
					  )) * _LightColor0.xyz);
					  lowp float tmpvar_7;
					  highp float lightShadowDataX_8;
					  mediump float tmpvar_9;
					  tmpvar_9 = _LightShadowData.x;
					  lightShadowDataX_8 = tmpvar_9;
					  highp float tmpvar_10;
					  tmpvar_10 = max (float((texture2D (_ShadowMapTexture, xlv_TEXCOORD1.xy).x > xlv_TEXCOORD1.z)), lightShadowDataX_8);
					  tmpvar_7 = tmpvar_10;
					  c_1 = (c_1 * (tmpvar_7 * max (0.0, 
					    dot (xlv_TEXCOORD5, xlv_TEXCOORD4)
					  )));
					  mediump vec4 tmpvar_11;
					  tmpvar_11.w = 1.0;
					  tmpvar_11.xyz = c_1;
					  mediump vec4 xlat_varoutput_12;
					  xlat_varoutput_12.xyz = tmpvar_11.xyz;
					  xlat_varoutput_12.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_12;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 n_13;
					  n_13 = (tmpvar_7.xyz - _WorldSpaceCameraPos);
					  tmpvar_12 = n_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = (_WorldSpaceLightPos0.xyz - (tmpvar_7.xyz * _WorldSpaceLightPos0.w));
					  tmpvar_4.w = tmpvar_19.x;
					  tmpvar_5.w = tmpvar_19.y;
					  tmpvar_6.w = tmpvar_19.z;
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					  xlv_TEXCOORD5 = (unity_WorldToShadow[0] * (unity_ObjectToWorld * _glesVertex));
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp sampler2D _ShadowMapTexture;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec4 xlv_TEXCOORD5;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_4;
					  tmpvar_4.x = xlv_TEXCOORD2.w;
					  tmpvar_4.y = xlv_TEXCOORD2_1.w;
					  tmpvar_4.z = xlv_TEXCOORD2_2.w;
					  lowp float tmpvar_5;
					  highp float lightShadowDataX_6;
					  mediump float tmpvar_7;
					  tmpvar_7 = _LightShadowData.x;
					  lightShadowDataX_6 = tmpvar_7;
					  highp float tmpvar_8;
					  tmpvar_8 = max (float((texture2D (_ShadowMapTexture, xlv_TEXCOORD5.xy).x > xlv_TEXCOORD5.z)), lightShadowDataX_6);
					  tmpvar_5 = tmpvar_8;
					  mediump float atten_9;
					  atten_9 = tmpvar_5;
					  mediump vec3 tmpvar_10;
					  tmpvar_10 = _LightColor0.xyz;
					  tmpvar_10 = (tmpvar_10 * atten_9);
					  mediump float specular_11;
					  mediump vec3 tmpvar_12;
					  mediump vec3 inVec_13;
					  inVec_13 = (tmpvar_4 - normalize(xlv_TEXCOORD1));
					  tmpvar_12 = (inVec_13 * inversesqrt(max (0.001, 
					    dot (inVec_13, inVec_13)
					  )));
					  mediump float tmpvar_14;
					  tmpvar_14 = max (0.0, dot (tmpvar_4, tmpvar_12));
					  mediump float tmpvar_15;
					  tmpvar_15 = (1.0 - _Glossiness);
					  mediump float tmpvar_16;
					  tmpvar_16 = max (0.0001, (tmpvar_15 * tmpvar_15));
					  mediump float tmpvar_17;
					  tmpvar_17 = max (((2.0 / 
					    (tmpvar_16 * tmpvar_16)
					  ) - 2.0), 0.0001);
					  specular_11 = sqrt(max (0.0001, (
					    ((tmpvar_17 + 1.0) * pow (max (0.0, dot (tmpvar_3, tmpvar_12)), tmpvar_17))
					   / 
					    (((8.0 * (
					      ((tmpvar_14 * tmpvar_14) * _Glossiness)
					     + 
					      (tmpvar_15 * tmpvar_15)
					    )) * tmpvar_14) + 0.0001)
					  )));
					  mediump float tmpvar_18;
					  tmpvar_18 = clamp (specular_11, 0.0, 100.0);
					  specular_11 = tmpvar_18;
					  mediump vec4 tmpvar_19;
					  tmpvar_19.w = 1.0;
					  tmpvar_19.xyz = (((
					    (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)))
					   + 
					    (tmpvar_18 * mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic)))
					  ) * tmpvar_10) * max (0.0, dot (tmpvar_3, tmpvar_4)));
					  mediump vec4 xlat_varoutput_20;
					  xlat_varoutput_20.xyz = tmpvar_19.xyz;
					  xlat_varoutput_20.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_20;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 n_13;
					  n_13 = (tmpvar_7.xyz - _WorldSpaceCameraPos);
					  tmpvar_12 = n_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = (_WorldSpaceLightPos0.xyz - (tmpvar_7.xyz * _WorldSpaceLightPos0.w));
					  tmpvar_4.w = tmpvar_19.x;
					  tmpvar_5.w = tmpvar_19.y;
					  tmpvar_6.w = tmpvar_19.z;
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					  xlv_TEXCOORD5 = (unity_WorldToShadow[0] * (unity_ObjectToWorld * _glesVertex));
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp sampler2D _ShadowMapTexture;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec4 xlv_TEXCOORD5;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_4;
					  tmpvar_4.x = xlv_TEXCOORD2.w;
					  tmpvar_4.y = xlv_TEXCOORD2_1.w;
					  tmpvar_4.z = xlv_TEXCOORD2_2.w;
					  lowp float tmpvar_5;
					  highp float lightShadowDataX_6;
					  mediump float tmpvar_7;
					  tmpvar_7 = _LightShadowData.x;
					  lightShadowDataX_6 = tmpvar_7;
					  highp float tmpvar_8;
					  tmpvar_8 = max (float((texture2D (_ShadowMapTexture, xlv_TEXCOORD5.xy).x > xlv_TEXCOORD5.z)), lightShadowDataX_6);
					  tmpvar_5 = tmpvar_8;
					  mediump float atten_9;
					  atten_9 = tmpvar_5;
					  mediump vec3 tmpvar_10;
					  tmpvar_10 = _LightColor0.xyz;
					  tmpvar_10 = (tmpvar_10 * atten_9);
					  mediump float specular_11;
					  mediump vec3 tmpvar_12;
					  mediump vec3 inVec_13;
					  inVec_13 = (tmpvar_4 - normalize(xlv_TEXCOORD1));
					  tmpvar_12 = (inVec_13 * inversesqrt(max (0.001, 
					    dot (inVec_13, inVec_13)
					  )));
					  mediump float tmpvar_14;
					  tmpvar_14 = max (0.0, dot (tmpvar_4, tmpvar_12));
					  mediump float tmpvar_15;
					  tmpvar_15 = (1.0 - _Glossiness);
					  mediump float tmpvar_16;
					  tmpvar_16 = max (0.0001, (tmpvar_15 * tmpvar_15));
					  mediump float tmpvar_17;
					  tmpvar_17 = max (((2.0 / 
					    (tmpvar_16 * tmpvar_16)
					  ) - 2.0), 0.0001);
					  specular_11 = sqrt(max (0.0001, (
					    ((tmpvar_17 + 1.0) * pow (max (0.0, dot (tmpvar_3, tmpvar_12)), tmpvar_17))
					   / 
					    (((8.0 * (
					      ((tmpvar_14 * tmpvar_14) * _Glossiness)
					     + 
					      (tmpvar_15 * tmpvar_15)
					    )) * tmpvar_14) + 0.0001)
					  )));
					  mediump float tmpvar_18;
					  tmpvar_18 = clamp (specular_11, 0.0, 100.0);
					  specular_11 = tmpvar_18;
					  mediump vec4 tmpvar_19;
					  tmpvar_19.w = 1.0;
					  tmpvar_19.xyz = (((
					    (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)))
					   + 
					    (tmpvar_18 * mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic)))
					  ) * tmpvar_10) * max (0.0, dot (tmpvar_3, tmpvar_4)));
					  mediump vec4 xlat_varoutput_20;
					  xlat_varoutput_20.xyz = tmpvar_19.xyz;
					  xlat_varoutput_20.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_20;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec3 lightDir_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_13;
					  tmpvar_13 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  lightDir_6 = tmpvar_13;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  normalWorld_4 = tmpvar_19;
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  highp vec4 tmpvar_20;
					  tmpvar_20 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD1 = (unity_WorldToLight * tmpvar_20).xy;
					  xlv_TEXCOORD2 = (unity_WorldToShadow[0] * tmpvar_20);
					  xlv_TEXCOORD3 = tmpvar_7;
					  xlv_TEXCOORD4 = lightDir_6;
					  xlv_TEXCOORD5 = normalWorld_4;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp sampler2D _ShadowMapTexture;
					uniform sampler2D _LightTexture0;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump float tmpvar_4;
					  tmpvar_4 = dot (xlv_TEXCOORD3.yzw, xlv_TEXCOORD4);
					  mediump vec2 tmpvar_5;
					  tmpvar_5.x = ((tmpvar_4 * tmpvar_4) * (tmpvar_4 * tmpvar_4));
					  tmpvar_5.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (unity_NHxRoughness, tmpvar_5);
					  c_1 = (((tmpvar_3 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_6.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic))
					  )) * _LightColor0.xyz);
					  lowp vec4 tmpvar_7;
					  tmpvar_7 = texture2D (_LightTexture0, xlv_TEXCOORD1);
					  lowp float tmpvar_8;
					  highp float lightShadowDataX_9;
					  mediump float tmpvar_10;
					  tmpvar_10 = _LightShadowData.x;
					  lightShadowDataX_9 = tmpvar_10;
					  highp float tmpvar_11;
					  tmpvar_11 = max (float((texture2D (_ShadowMapTexture, xlv_TEXCOORD2.xy).x > xlv_TEXCOORD2.z)), lightShadowDataX_9);
					  tmpvar_8 = tmpvar_11;
					  c_1 = (c_1 * ((tmpvar_7.w * tmpvar_8) * max (0.0, 
					    dot (xlv_TEXCOORD5, xlv_TEXCOORD4)
					  )));
					  mediump vec4 tmpvar_12;
					  tmpvar_12.w = 1.0;
					  tmpvar_12.xyz = c_1;
					  mediump vec4 xlat_varoutput_13;
					  xlat_varoutput_13.xyz = tmpvar_12.xyz;
					  xlat_varoutput_13.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_13;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec2 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 n_13;
					  n_13 = (tmpvar_7.xyz - _WorldSpaceCameraPos);
					  tmpvar_12 = n_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = (_WorldSpaceLightPos0.xyz - (tmpvar_7.xyz * _WorldSpaceLightPos0.w));
					  tmpvar_4.w = tmpvar_19.x;
					  tmpvar_5.w = tmpvar_19.y;
					  tmpvar_6.w = tmpvar_19.z;
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					  highp vec4 tmpvar_20;
					  tmpvar_20 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD5 = (unity_WorldToLight * tmpvar_20).xy;
					  xlv_TEXCOORD6 = (unity_WorldToShadow[0] * tmpvar_20);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp sampler2D _ShadowMapTexture;
					uniform sampler2D _LightTexture0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec2 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_4;
					  tmpvar_4.x = xlv_TEXCOORD2.w;
					  tmpvar_4.y = xlv_TEXCOORD2_1.w;
					  tmpvar_4.z = xlv_TEXCOORD2_2.w;
					  lowp vec4 tmpvar_5;
					  tmpvar_5 = texture2D (_LightTexture0, xlv_TEXCOORD5);
					  lowp float tmpvar_6;
					  highp float lightShadowDataX_7;
					  mediump float tmpvar_8;
					  tmpvar_8 = _LightShadowData.x;
					  lightShadowDataX_7 = tmpvar_8;
					  highp float tmpvar_9;
					  tmpvar_9 = max (float((texture2D (_ShadowMapTexture, xlv_TEXCOORD6.xy).x > xlv_TEXCOORD6.z)), lightShadowDataX_7);
					  tmpvar_6 = tmpvar_9;
					  mediump float atten_10;
					  atten_10 = (tmpvar_5.w * tmpvar_6);
					  mediump vec3 tmpvar_11;
					  tmpvar_11 = _LightColor0.xyz;
					  tmpvar_11 = (tmpvar_11 * atten_10);
					  mediump float specular_12;
					  mediump vec3 tmpvar_13;
					  mediump vec3 inVec_14;
					  inVec_14 = (tmpvar_4 - normalize(xlv_TEXCOORD1));
					  tmpvar_13 = (inVec_14 * inversesqrt(max (0.001, 
					    dot (inVec_14, inVec_14)
					  )));
					  mediump float tmpvar_15;
					  tmpvar_15 = max (0.0, dot (tmpvar_4, tmpvar_13));
					  mediump float tmpvar_16;
					  tmpvar_16 = (1.0 - _Glossiness);
					  mediump float tmpvar_17;
					  tmpvar_17 = max (0.0001, (tmpvar_16 * tmpvar_16));
					  mediump float tmpvar_18;
					  tmpvar_18 = max (((2.0 / 
					    (tmpvar_17 * tmpvar_17)
					  ) - 2.0), 0.0001);
					  specular_12 = sqrt(max (0.0001, (
					    ((tmpvar_18 + 1.0) * pow (max (0.0, dot (tmpvar_3, tmpvar_13)), tmpvar_18))
					   / 
					    (((8.0 * (
					      ((tmpvar_15 * tmpvar_15) * _Glossiness)
					     + 
					      (tmpvar_16 * tmpvar_16)
					    )) * tmpvar_15) + 0.0001)
					  )));
					  mediump float tmpvar_19;
					  tmpvar_19 = clamp (specular_12, 0.0, 100.0);
					  specular_12 = tmpvar_19;
					  mediump vec4 tmpvar_20;
					  tmpvar_20.w = 1.0;
					  tmpvar_20.xyz = (((
					    (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)))
					   + 
					    (tmpvar_19 * mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic)))
					  ) * tmpvar_11) * max (0.0, dot (tmpvar_3, tmpvar_4)));
					  mediump vec4 xlat_varoutput_21;
					  xlat_varoutput_21.xyz = tmpvar_20.xyz;
					  xlat_varoutput_21.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_21;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec2 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 n_13;
					  n_13 = (tmpvar_7.xyz - _WorldSpaceCameraPos);
					  tmpvar_12 = n_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = (_WorldSpaceLightPos0.xyz - (tmpvar_7.xyz * _WorldSpaceLightPos0.w));
					  tmpvar_4.w = tmpvar_19.x;
					  tmpvar_5.w = tmpvar_19.y;
					  tmpvar_6.w = tmpvar_19.z;
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					  highp vec4 tmpvar_20;
					  tmpvar_20 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD5 = (unity_WorldToLight * tmpvar_20).xy;
					  xlv_TEXCOORD6 = (unity_WorldToShadow[0] * tmpvar_20);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp sampler2D _ShadowMapTexture;
					uniform sampler2D _LightTexture0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec2 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_4;
					  tmpvar_4.x = xlv_TEXCOORD2.w;
					  tmpvar_4.y = xlv_TEXCOORD2_1.w;
					  tmpvar_4.z = xlv_TEXCOORD2_2.w;
					  lowp vec4 tmpvar_5;
					  tmpvar_5 = texture2D (_LightTexture0, xlv_TEXCOORD5);
					  lowp float tmpvar_6;
					  highp float lightShadowDataX_7;
					  mediump float tmpvar_8;
					  tmpvar_8 = _LightShadowData.x;
					  lightShadowDataX_7 = tmpvar_8;
					  highp float tmpvar_9;
					  tmpvar_9 = max (float((texture2D (_ShadowMapTexture, xlv_TEXCOORD6.xy).x > xlv_TEXCOORD6.z)), lightShadowDataX_7);
					  tmpvar_6 = tmpvar_9;
					  mediump float atten_10;
					  atten_10 = (tmpvar_5.w * tmpvar_6);
					  mediump vec3 tmpvar_11;
					  tmpvar_11 = _LightColor0.xyz;
					  tmpvar_11 = (tmpvar_11 * atten_10);
					  mediump float specular_12;
					  mediump vec3 tmpvar_13;
					  mediump vec3 inVec_14;
					  inVec_14 = (tmpvar_4 - normalize(xlv_TEXCOORD1));
					  tmpvar_13 = (inVec_14 * inversesqrt(max (0.001, 
					    dot (inVec_14, inVec_14)
					  )));
					  mediump float tmpvar_15;
					  tmpvar_15 = max (0.0, dot (tmpvar_4, tmpvar_13));
					  mediump float tmpvar_16;
					  tmpvar_16 = (1.0 - _Glossiness);
					  mediump float tmpvar_17;
					  tmpvar_17 = max (0.0001, (tmpvar_16 * tmpvar_16));
					  mediump float tmpvar_18;
					  tmpvar_18 = max (((2.0 / 
					    (tmpvar_17 * tmpvar_17)
					  ) - 2.0), 0.0001);
					  specular_12 = sqrt(max (0.0001, (
					    ((tmpvar_18 + 1.0) * pow (max (0.0, dot (tmpvar_3, tmpvar_13)), tmpvar_18))
					   / 
					    (((8.0 * (
					      ((tmpvar_15 * tmpvar_15) * _Glossiness)
					     + 
					      (tmpvar_16 * tmpvar_16)
					    )) * tmpvar_15) + 0.0001)
					  )));
					  mediump float tmpvar_19;
					  tmpvar_19 = clamp (specular_12, 0.0, 100.0);
					  specular_12 = tmpvar_19;
					  mediump vec4 tmpvar_20;
					  tmpvar_20.w = 1.0;
					  tmpvar_20.xyz = (((
					    (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)))
					   + 
					    (tmpvar_19 * mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic)))
					  ) * tmpvar_11) * max (0.0, dot (tmpvar_3, tmpvar_4)));
					  mediump vec4 xlat_varoutput_21;
					  xlat_varoutput_21.xyz = tmpvar_20.xyz;
					  xlat_varoutput_21.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_21;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "POINT" "SHADOWS_CUBE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp vec4 _LightPositionRange;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD1;
					varying highp vec3 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec3 lightDir_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_13;
					  tmpvar_13 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  lightDir_6 = tmpvar_13;
					  mediump vec3 tmpvar_14;
					  highp vec3 n_15;
					  n_15 = lightDir_6;
					  highp vec3 tmpvar_16;
					  tmpvar_16 = normalize(n_15);
					  tmpvar_14 = tmpvar_16;
					  lightDir_6 = tmpvar_14;
					  highp vec3 tmpvar_17;
					  tmpvar_17 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_17;
					  highp vec3 norm_18;
					  norm_18 = tmpvar_1;
					  highp vec4 v_19;
					  v_19.x = unity_WorldToObject[0].x;
					  v_19.y = unity_WorldToObject[1].x;
					  v_19.z = unity_WorldToObject[2].x;
					  v_19.w = unity_WorldToObject[3].x;
					  highp vec4 v_20;
					  v_20.x = unity_WorldToObject[0].y;
					  v_20.y = unity_WorldToObject[1].y;
					  v_20.z = unity_WorldToObject[2].y;
					  v_20.w = unity_WorldToObject[3].y;
					  highp vec4 v_21;
					  v_21.x = unity_WorldToObject[0].z;
					  v_21.y = unity_WorldToObject[1].z;
					  v_21.z = unity_WorldToObject[2].z;
					  v_21.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_22;
					  tmpvar_22 = normalize(((
					    (v_19.xyz * norm_18.x)
					   + 
					    (v_20.xyz * norm_18.y)
					  ) + (v_21.xyz * norm_18.z)));
					  normalWorld_4 = tmpvar_22;
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  highp vec4 tmpvar_23;
					  tmpvar_23 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD1 = (unity_WorldToLight * tmpvar_23).xyz;
					  xlv_TEXCOORD2 = (tmpvar_23.xyz - _LightPositionRange.xyz);
					  xlv_TEXCOORD3 = tmpvar_7;
					  xlv_TEXCOORD4 = tmpvar_14;
					  xlv_TEXCOORD5 = normalWorld_4;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _LightPositionRange;
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp samplerCube _ShadowMapTexture;
					uniform sampler2D _LightTexture0;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD1;
					varying highp vec3 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump float tmpvar_4;
					  tmpvar_4 = dot (xlv_TEXCOORD3.yzw, xlv_TEXCOORD4);
					  mediump vec2 tmpvar_5;
					  tmpvar_5.x = ((tmpvar_4 * tmpvar_4) * (tmpvar_4 * tmpvar_4));
					  tmpvar_5.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (unity_NHxRoughness, tmpvar_5);
					  c_1 = (((tmpvar_3 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_6.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic))
					  )) * _LightColor0.xyz);
					  highp float tmpvar_7;
					  tmpvar_7 = dot (xlv_TEXCOORD1, xlv_TEXCOORD1);
					  lowp vec4 tmpvar_8;
					  tmpvar_8 = texture2D (_LightTexture0, vec2(tmpvar_7));
					  highp float mydist_9;
					  mydist_9 = ((sqrt(
					    dot (xlv_TEXCOORD2, xlv_TEXCOORD2)
					  ) * _LightPositionRange.w) * 0.97);
					  highp float tmpvar_10;
					  tmpvar_10 = dot (textureCube (_ShadowMapTexture, xlv_TEXCOORD2), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  mediump float tmpvar_11;
					  if ((tmpvar_10 < mydist_9)) {
					    tmpvar_11 = _LightShadowData.x;
					  } else {
					    tmpvar_11 = 1.0;
					  };
					  c_1 = (c_1 * ((tmpvar_8.w * tmpvar_11) * max (0.0, 
					    dot (xlv_TEXCOORD5, xlv_TEXCOORD4)
					  )));
					  mediump vec4 tmpvar_12;
					  tmpvar_12.w = 1.0;
					  tmpvar_12.xyz = c_1;
					  mediump vec4 xlat_varoutput_13;
					  xlat_varoutput_13.xyz = tmpvar_12.xyz;
					  xlat_varoutput_13.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_13;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "POINT" "SHADOWS_CUBE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp vec4 _LightPositionRange;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					varying highp vec3 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  highp vec3 lightDir_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_13;
					  highp vec3 n_14;
					  n_14 = (tmpvar_8.xyz - _WorldSpaceCameraPos);
					  tmpvar_13 = n_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_7.xyz = tmpvar_19;
					  highp vec3 tmpvar_20;
					  tmpvar_20 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  mediump vec3 tmpvar_21;
					  tmpvar_21 = tmpvar_20;
					  lightDir_4 = tmpvar_21;
					  tmpvar_5.w = lightDir_4.x;
					  tmpvar_6.w = lightDir_4.y;
					  tmpvar_7.w = lightDir_4.z;
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = tmpvar_13;
					  xlv_TEXCOORD2 = tmpvar_5;
					  xlv_TEXCOORD2_1 = tmpvar_6;
					  xlv_TEXCOORD2_2 = tmpvar_7;
					  highp vec4 tmpvar_22;
					  tmpvar_22 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD5 = (unity_WorldToLight * tmpvar_22).xyz;
					  xlv_TEXCOORD6 = (tmpvar_22.xyz - _LightPositionRange.xyz);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _LightPositionRange;
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp samplerCube _ShadowMapTexture;
					uniform sampler2D _LightTexture0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					varying highp vec3 xlv_TEXCOORD6;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  mediump vec3 tmpvar_4;
					  tmpvar_4 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic));
					  tmpvar_3 = (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)));
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_6;
					  tmpvar_6 = normalize(xlv_TEXCOORD1);
					  mediump vec3 tmpvar_7;
					  tmpvar_7.x = xlv_TEXCOORD2.w;
					  tmpvar_7.y = xlv_TEXCOORD2_1.w;
					  tmpvar_7.z = xlv_TEXCOORD2_2.w;
					  highp float tmpvar_8;
					  tmpvar_8 = dot (xlv_TEXCOORD5, xlv_TEXCOORD5);
					  lowp vec4 tmpvar_9;
					  tmpvar_9 = texture2D (_LightTexture0, vec2(tmpvar_8));
					  highp float mydist_10;
					  mydist_10 = ((sqrt(
					    dot (xlv_TEXCOORD6, xlv_TEXCOORD6)
					  ) * _LightPositionRange.w) * 0.97);
					  highp float tmpvar_11;
					  tmpvar_11 = dot (textureCube (_ShadowMapTexture, xlv_TEXCOORD6), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  mediump float tmpvar_12;
					  if ((tmpvar_11 < mydist_10)) {
					    tmpvar_12 = _LightShadowData.x;
					  } else {
					    tmpvar_12 = 1.0;
					  };
					  mediump vec3 tmpvar_13;
					  tmpvar_13 = _LightColor0.xyz;
					  mediump vec3 tmpvar_14;
					  tmpvar_14 = normalize(tmpvar_7);
					  tmpvar_13 = (tmpvar_13 * (tmpvar_9.w * tmpvar_12));
					  mediump float specular_15;
					  mediump vec3 tmpvar_16;
					  mediump vec3 inVec_17;
					  inVec_17 = (tmpvar_14 - tmpvar_6);
					  tmpvar_16 = (inVec_17 * inversesqrt(max (0.001, 
					    dot (inVec_17, inVec_17)
					  )));
					  mediump float tmpvar_18;
					  tmpvar_18 = max (0.0, dot (tmpvar_14, tmpvar_16));
					  mediump float tmpvar_19;
					  tmpvar_19 = (1.0 - _Glossiness);
					  mediump float tmpvar_20;
					  tmpvar_20 = max (0.0001, (tmpvar_19 * tmpvar_19));
					  mediump float tmpvar_21;
					  tmpvar_21 = max (((2.0 / 
					    (tmpvar_20 * tmpvar_20)
					  ) - 2.0), 0.0001);
					  specular_15 = sqrt(max (0.0001, (
					    ((tmpvar_21 + 1.0) * pow (max (0.0, dot (tmpvar_5, tmpvar_16)), tmpvar_21))
					   / 
					    (((8.0 * (
					      ((tmpvar_18 * tmpvar_18) * _Glossiness)
					     + 
					      (tmpvar_19 * tmpvar_19)
					    )) * tmpvar_18) + 0.0001)
					  )));
					  mediump float tmpvar_22;
					  tmpvar_22 = clamp (specular_15, 0.0, 100.0);
					  specular_15 = tmpvar_22;
					  mediump vec4 tmpvar_23;
					  tmpvar_23.w = 1.0;
					  tmpvar_23.xyz = (((tmpvar_3 + 
					    (tmpvar_22 * tmpvar_4)
					  ) * tmpvar_13) * max (0.0, dot (tmpvar_5, tmpvar_14)));
					  mediump vec4 xlat_varoutput_24;
					  xlat_varoutput_24.xyz = tmpvar_23.xyz;
					  xlat_varoutput_24.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_24;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "POINT" "SHADOWS_CUBE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp vec4 _LightPositionRange;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					varying highp vec3 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  highp vec3 lightDir_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_13;
					  highp vec3 n_14;
					  n_14 = (tmpvar_8.xyz - _WorldSpaceCameraPos);
					  tmpvar_13 = n_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_7.xyz = tmpvar_19;
					  highp vec3 tmpvar_20;
					  tmpvar_20 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  mediump vec3 tmpvar_21;
					  tmpvar_21 = tmpvar_20;
					  lightDir_4 = tmpvar_21;
					  tmpvar_5.w = lightDir_4.x;
					  tmpvar_6.w = lightDir_4.y;
					  tmpvar_7.w = lightDir_4.z;
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = tmpvar_13;
					  xlv_TEXCOORD2 = tmpvar_5;
					  xlv_TEXCOORD2_1 = tmpvar_6;
					  xlv_TEXCOORD2_2 = tmpvar_7;
					  highp vec4 tmpvar_22;
					  tmpvar_22 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD5 = (unity_WorldToLight * tmpvar_22).xyz;
					  xlv_TEXCOORD6 = (tmpvar_22.xyz - _LightPositionRange.xyz);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _LightPositionRange;
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp samplerCube _ShadowMapTexture;
					uniform sampler2D _LightTexture0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					varying highp vec3 xlv_TEXCOORD6;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  mediump vec3 tmpvar_4;
					  tmpvar_4 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic));
					  tmpvar_3 = (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)));
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_6;
					  tmpvar_6 = normalize(xlv_TEXCOORD1);
					  mediump vec3 tmpvar_7;
					  tmpvar_7.x = xlv_TEXCOORD2.w;
					  tmpvar_7.y = xlv_TEXCOORD2_1.w;
					  tmpvar_7.z = xlv_TEXCOORD2_2.w;
					  highp float tmpvar_8;
					  tmpvar_8 = dot (xlv_TEXCOORD5, xlv_TEXCOORD5);
					  lowp vec4 tmpvar_9;
					  tmpvar_9 = texture2D (_LightTexture0, vec2(tmpvar_8));
					  highp float mydist_10;
					  mydist_10 = ((sqrt(
					    dot (xlv_TEXCOORD6, xlv_TEXCOORD6)
					  ) * _LightPositionRange.w) * 0.97);
					  highp float tmpvar_11;
					  tmpvar_11 = dot (textureCube (_ShadowMapTexture, xlv_TEXCOORD6), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  mediump float tmpvar_12;
					  if ((tmpvar_11 < mydist_10)) {
					    tmpvar_12 = _LightShadowData.x;
					  } else {
					    tmpvar_12 = 1.0;
					  };
					  mediump vec3 tmpvar_13;
					  tmpvar_13 = _LightColor0.xyz;
					  mediump vec3 tmpvar_14;
					  tmpvar_14 = normalize(tmpvar_7);
					  tmpvar_13 = (tmpvar_13 * (tmpvar_9.w * tmpvar_12));
					  mediump float specular_15;
					  mediump vec3 tmpvar_16;
					  mediump vec3 inVec_17;
					  inVec_17 = (tmpvar_14 - tmpvar_6);
					  tmpvar_16 = (inVec_17 * inversesqrt(max (0.001, 
					    dot (inVec_17, inVec_17)
					  )));
					  mediump float tmpvar_18;
					  tmpvar_18 = max (0.0, dot (tmpvar_14, tmpvar_16));
					  mediump float tmpvar_19;
					  tmpvar_19 = (1.0 - _Glossiness);
					  mediump float tmpvar_20;
					  tmpvar_20 = max (0.0001, (tmpvar_19 * tmpvar_19));
					  mediump float tmpvar_21;
					  tmpvar_21 = max (((2.0 / 
					    (tmpvar_20 * tmpvar_20)
					  ) - 2.0), 0.0001);
					  specular_15 = sqrt(max (0.0001, (
					    ((tmpvar_21 + 1.0) * pow (max (0.0, dot (tmpvar_5, tmpvar_16)), tmpvar_21))
					   / 
					    (((8.0 * (
					      ((tmpvar_18 * tmpvar_18) * _Glossiness)
					     + 
					      (tmpvar_19 * tmpvar_19)
					    )) * tmpvar_18) + 0.0001)
					  )));
					  mediump float tmpvar_22;
					  tmpvar_22 = clamp (specular_15, 0.0, 100.0);
					  specular_15 = tmpvar_22;
					  mediump vec4 tmpvar_23;
					  tmpvar_23.w = 1.0;
					  tmpvar_23.xyz = (((tmpvar_3 + 
					    (tmpvar_22 * tmpvar_4)
					  ) * tmpvar_13) * max (0.0, dot (tmpvar_5, tmpvar_14)));
					  mediump vec4 xlat_varoutput_24;
					  xlat_varoutput_24.xyz = tmpvar_23.xyz;
					  xlat_varoutput_24.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_24;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp vec4 _LightPositionRange;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD1;
					varying highp vec3 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec3 lightDir_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_13;
					  tmpvar_13 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  lightDir_6 = tmpvar_13;
					  mediump vec3 tmpvar_14;
					  highp vec3 n_15;
					  n_15 = lightDir_6;
					  highp vec3 tmpvar_16;
					  tmpvar_16 = normalize(n_15);
					  tmpvar_14 = tmpvar_16;
					  lightDir_6 = tmpvar_14;
					  highp vec3 tmpvar_17;
					  tmpvar_17 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_17;
					  highp vec3 norm_18;
					  norm_18 = tmpvar_1;
					  highp vec4 v_19;
					  v_19.x = unity_WorldToObject[0].x;
					  v_19.y = unity_WorldToObject[1].x;
					  v_19.z = unity_WorldToObject[2].x;
					  v_19.w = unity_WorldToObject[3].x;
					  highp vec4 v_20;
					  v_20.x = unity_WorldToObject[0].y;
					  v_20.y = unity_WorldToObject[1].y;
					  v_20.z = unity_WorldToObject[2].y;
					  v_20.w = unity_WorldToObject[3].y;
					  highp vec4 v_21;
					  v_21.x = unity_WorldToObject[0].z;
					  v_21.y = unity_WorldToObject[1].z;
					  v_21.z = unity_WorldToObject[2].z;
					  v_21.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_22;
					  tmpvar_22 = normalize(((
					    (v_19.xyz * norm_18.x)
					   + 
					    (v_20.xyz * norm_18.y)
					  ) + (v_21.xyz * norm_18.z)));
					  normalWorld_4 = tmpvar_22;
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  highp vec4 tmpvar_23;
					  tmpvar_23 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD1 = (unity_WorldToLight * tmpvar_23).xyz;
					  xlv_TEXCOORD2 = (tmpvar_23.xyz - _LightPositionRange.xyz);
					  xlv_TEXCOORD3 = tmpvar_7;
					  xlv_TEXCOORD4 = tmpvar_14;
					  xlv_TEXCOORD5 = normalWorld_4;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _LightPositionRange;
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp samplerCube _ShadowMapTexture;
					uniform lowp samplerCube _LightTexture0;
					uniform sampler2D _LightTextureB0;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD1;
					varying highp vec3 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump float tmpvar_4;
					  tmpvar_4 = dot (xlv_TEXCOORD3.yzw, xlv_TEXCOORD4);
					  mediump vec2 tmpvar_5;
					  tmpvar_5.x = ((tmpvar_4 * tmpvar_4) * (tmpvar_4 * tmpvar_4));
					  tmpvar_5.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (unity_NHxRoughness, tmpvar_5);
					  c_1 = (((tmpvar_3 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_6.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic))
					  )) * _LightColor0.xyz);
					  highp float tmpvar_7;
					  tmpvar_7 = dot (xlv_TEXCOORD1, xlv_TEXCOORD1);
					  lowp vec4 tmpvar_8;
					  tmpvar_8 = texture2D (_LightTextureB0, vec2(tmpvar_7));
					  lowp vec4 tmpvar_9;
					  tmpvar_9 = textureCube (_LightTexture0, xlv_TEXCOORD1);
					  highp float mydist_10;
					  mydist_10 = ((sqrt(
					    dot (xlv_TEXCOORD2, xlv_TEXCOORD2)
					  ) * _LightPositionRange.w) * 0.97);
					  highp float tmpvar_11;
					  tmpvar_11 = dot (textureCube (_ShadowMapTexture, xlv_TEXCOORD2), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  mediump float tmpvar_12;
					  if ((tmpvar_11 < mydist_10)) {
					    tmpvar_12 = _LightShadowData.x;
					  } else {
					    tmpvar_12 = 1.0;
					  };
					  c_1 = (c_1 * ((
					    (tmpvar_8.w * tmpvar_9.w)
					   * tmpvar_12) * max (0.0, 
					    dot (xlv_TEXCOORD5, xlv_TEXCOORD4)
					  )));
					  mediump vec4 tmpvar_13;
					  tmpvar_13.w = 1.0;
					  tmpvar_13.xyz = c_1;
					  mediump vec4 xlat_varoutput_14;
					  xlat_varoutput_14.xyz = tmpvar_13.xyz;
					  xlat_varoutput_14.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_14;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp vec4 _LightPositionRange;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					varying highp vec3 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  highp vec3 lightDir_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_13;
					  highp vec3 n_14;
					  n_14 = (tmpvar_8.xyz - _WorldSpaceCameraPos);
					  tmpvar_13 = n_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_7.xyz = tmpvar_19;
					  highp vec3 tmpvar_20;
					  tmpvar_20 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  mediump vec3 tmpvar_21;
					  tmpvar_21 = tmpvar_20;
					  lightDir_4 = tmpvar_21;
					  tmpvar_5.w = lightDir_4.x;
					  tmpvar_6.w = lightDir_4.y;
					  tmpvar_7.w = lightDir_4.z;
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = tmpvar_13;
					  xlv_TEXCOORD2 = tmpvar_5;
					  xlv_TEXCOORD2_1 = tmpvar_6;
					  xlv_TEXCOORD2_2 = tmpvar_7;
					  highp vec4 tmpvar_22;
					  tmpvar_22 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD5 = (unity_WorldToLight * tmpvar_22).xyz;
					  xlv_TEXCOORD6 = (tmpvar_22.xyz - _LightPositionRange.xyz);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _LightPositionRange;
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp samplerCube _ShadowMapTexture;
					uniform lowp samplerCube _LightTexture0;
					uniform sampler2D _LightTextureB0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					varying highp vec3 xlv_TEXCOORD6;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  mediump vec3 tmpvar_4;
					  tmpvar_4 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic));
					  tmpvar_3 = (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)));
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_6;
					  tmpvar_6 = normalize(xlv_TEXCOORD1);
					  mediump vec3 tmpvar_7;
					  tmpvar_7.x = xlv_TEXCOORD2.w;
					  tmpvar_7.y = xlv_TEXCOORD2_1.w;
					  tmpvar_7.z = xlv_TEXCOORD2_2.w;
					  highp float tmpvar_8;
					  tmpvar_8 = dot (xlv_TEXCOORD5, xlv_TEXCOORD5);
					  lowp vec4 tmpvar_9;
					  tmpvar_9 = texture2D (_LightTextureB0, vec2(tmpvar_8));
					  lowp vec4 tmpvar_10;
					  tmpvar_10 = textureCube (_LightTexture0, xlv_TEXCOORD5);
					  highp float mydist_11;
					  mydist_11 = ((sqrt(
					    dot (xlv_TEXCOORD6, xlv_TEXCOORD6)
					  ) * _LightPositionRange.w) * 0.97);
					  highp float tmpvar_12;
					  tmpvar_12 = dot (textureCube (_ShadowMapTexture, xlv_TEXCOORD6), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  mediump float tmpvar_13;
					  if ((tmpvar_12 < mydist_11)) {
					    tmpvar_13 = _LightShadowData.x;
					  } else {
					    tmpvar_13 = 1.0;
					  };
					  mediump vec3 tmpvar_14;
					  tmpvar_14 = _LightColor0.xyz;
					  mediump vec3 tmpvar_15;
					  tmpvar_15 = normalize(tmpvar_7);
					  tmpvar_14 = (tmpvar_14 * ((tmpvar_9.w * tmpvar_10.w) * tmpvar_13));
					  mediump float specular_16;
					  mediump vec3 tmpvar_17;
					  mediump vec3 inVec_18;
					  inVec_18 = (tmpvar_15 - tmpvar_6);
					  tmpvar_17 = (inVec_18 * inversesqrt(max (0.001, 
					    dot (inVec_18, inVec_18)
					  )));
					  mediump float tmpvar_19;
					  tmpvar_19 = max (0.0, dot (tmpvar_15, tmpvar_17));
					  mediump float tmpvar_20;
					  tmpvar_20 = (1.0 - _Glossiness);
					  mediump float tmpvar_21;
					  tmpvar_21 = max (0.0001, (tmpvar_20 * tmpvar_20));
					  mediump float tmpvar_22;
					  tmpvar_22 = max (((2.0 / 
					    (tmpvar_21 * tmpvar_21)
					  ) - 2.0), 0.0001);
					  specular_16 = sqrt(max (0.0001, (
					    ((tmpvar_22 + 1.0) * pow (max (0.0, dot (tmpvar_5, tmpvar_17)), tmpvar_22))
					   / 
					    (((8.0 * (
					      ((tmpvar_19 * tmpvar_19) * _Glossiness)
					     + 
					      (tmpvar_20 * tmpvar_20)
					    )) * tmpvar_19) + 0.0001)
					  )));
					  mediump float tmpvar_23;
					  tmpvar_23 = clamp (specular_16, 0.0, 100.0);
					  specular_16 = tmpvar_23;
					  mediump vec4 tmpvar_24;
					  tmpvar_24.w = 1.0;
					  tmpvar_24.xyz = (((tmpvar_3 + 
					    (tmpvar_23 * tmpvar_4)
					  ) * tmpvar_14) * max (0.0, dot (tmpvar_5, tmpvar_15)));
					  mediump vec4 xlat_varoutput_25;
					  xlat_varoutput_25.xyz = tmpvar_24.xyz;
					  xlat_varoutput_25.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_25;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp vec4 _LightPositionRange;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					varying highp vec3 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  highp vec3 lightDir_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_13;
					  highp vec3 n_14;
					  n_14 = (tmpvar_8.xyz - _WorldSpaceCameraPos);
					  tmpvar_13 = n_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_7.xyz = tmpvar_19;
					  highp vec3 tmpvar_20;
					  tmpvar_20 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  mediump vec3 tmpvar_21;
					  tmpvar_21 = tmpvar_20;
					  lightDir_4 = tmpvar_21;
					  tmpvar_5.w = lightDir_4.x;
					  tmpvar_6.w = lightDir_4.y;
					  tmpvar_7.w = lightDir_4.z;
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = tmpvar_13;
					  xlv_TEXCOORD2 = tmpvar_5;
					  xlv_TEXCOORD2_1 = tmpvar_6;
					  xlv_TEXCOORD2_2 = tmpvar_7;
					  highp vec4 tmpvar_22;
					  tmpvar_22 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD5 = (unity_WorldToLight * tmpvar_22).xyz;
					  xlv_TEXCOORD6 = (tmpvar_22.xyz - _LightPositionRange.xyz);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _LightPositionRange;
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp samplerCube _ShadowMapTexture;
					uniform lowp samplerCube _LightTexture0;
					uniform sampler2D _LightTextureB0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					varying highp vec3 xlv_TEXCOORD6;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  mediump vec3 tmpvar_4;
					  tmpvar_4 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic));
					  tmpvar_3 = (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)));
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_6;
					  tmpvar_6 = normalize(xlv_TEXCOORD1);
					  mediump vec3 tmpvar_7;
					  tmpvar_7.x = xlv_TEXCOORD2.w;
					  tmpvar_7.y = xlv_TEXCOORD2_1.w;
					  tmpvar_7.z = xlv_TEXCOORD2_2.w;
					  highp float tmpvar_8;
					  tmpvar_8 = dot (xlv_TEXCOORD5, xlv_TEXCOORD5);
					  lowp vec4 tmpvar_9;
					  tmpvar_9 = texture2D (_LightTextureB0, vec2(tmpvar_8));
					  lowp vec4 tmpvar_10;
					  tmpvar_10 = textureCube (_LightTexture0, xlv_TEXCOORD5);
					  highp float mydist_11;
					  mydist_11 = ((sqrt(
					    dot (xlv_TEXCOORD6, xlv_TEXCOORD6)
					  ) * _LightPositionRange.w) * 0.97);
					  highp float tmpvar_12;
					  tmpvar_12 = dot (textureCube (_ShadowMapTexture, xlv_TEXCOORD6), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  mediump float tmpvar_13;
					  if ((tmpvar_12 < mydist_11)) {
					    tmpvar_13 = _LightShadowData.x;
					  } else {
					    tmpvar_13 = 1.0;
					  };
					  mediump vec3 tmpvar_14;
					  tmpvar_14 = _LightColor0.xyz;
					  mediump vec3 tmpvar_15;
					  tmpvar_15 = normalize(tmpvar_7);
					  tmpvar_14 = (tmpvar_14 * ((tmpvar_9.w * tmpvar_10.w) * tmpvar_13));
					  mediump float specular_16;
					  mediump vec3 tmpvar_17;
					  mediump vec3 inVec_18;
					  inVec_18 = (tmpvar_15 - tmpvar_6);
					  tmpvar_17 = (inVec_18 * inversesqrt(max (0.001, 
					    dot (inVec_18, inVec_18)
					  )));
					  mediump float tmpvar_19;
					  tmpvar_19 = max (0.0, dot (tmpvar_15, tmpvar_17));
					  mediump float tmpvar_20;
					  tmpvar_20 = (1.0 - _Glossiness);
					  mediump float tmpvar_21;
					  tmpvar_21 = max (0.0001, (tmpvar_20 * tmpvar_20));
					  mediump float tmpvar_22;
					  tmpvar_22 = max (((2.0 / 
					    (tmpvar_21 * tmpvar_21)
					  ) - 2.0), 0.0001);
					  specular_16 = sqrt(max (0.0001, (
					    ((tmpvar_22 + 1.0) * pow (max (0.0, dot (tmpvar_5, tmpvar_17)), tmpvar_22))
					   / 
					    (((8.0 * (
					      ((tmpvar_19 * tmpvar_19) * _Glossiness)
					     + 
					      (tmpvar_20 * tmpvar_20)
					    )) * tmpvar_19) + 0.0001)
					  )));
					  mediump float tmpvar_23;
					  tmpvar_23 = clamp (specular_16, 0.0, 100.0);
					  specular_16 = tmpvar_23;
					  mediump vec4 tmpvar_24;
					  tmpvar_24.w = 1.0;
					  tmpvar_24.xyz = (((tmpvar_3 + 
					    (tmpvar_23 * tmpvar_4)
					  ) * tmpvar_14) * max (0.0, dot (tmpvar_5, tmpvar_15)));
					  mediump vec4 xlat_varoutput_25;
					  xlat_varoutput_25.xyz = tmpvar_24.xyz;
					  xlat_varoutput_25.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_25;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "SPOT" "SHADOWS_DEPTH" "SHADOWS_SOFT" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec3 lightDir_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_13;
					  tmpvar_13 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  lightDir_6 = tmpvar_13;
					  mediump vec3 tmpvar_14;
					  highp vec3 n_15;
					  n_15 = lightDir_6;
					  highp vec3 tmpvar_16;
					  tmpvar_16 = normalize(n_15);
					  tmpvar_14 = tmpvar_16;
					  lightDir_6 = tmpvar_14;
					  highp vec3 tmpvar_17;
					  tmpvar_17 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_17;
					  highp vec3 norm_18;
					  norm_18 = tmpvar_1;
					  highp vec4 v_19;
					  v_19.x = unity_WorldToObject[0].x;
					  v_19.y = unity_WorldToObject[1].x;
					  v_19.z = unity_WorldToObject[2].x;
					  v_19.w = unity_WorldToObject[3].x;
					  highp vec4 v_20;
					  v_20.x = unity_WorldToObject[0].y;
					  v_20.y = unity_WorldToObject[1].y;
					  v_20.z = unity_WorldToObject[2].y;
					  v_20.w = unity_WorldToObject[3].y;
					  highp vec4 v_21;
					  v_21.x = unity_WorldToObject[0].z;
					  v_21.y = unity_WorldToObject[1].z;
					  v_21.z = unity_WorldToObject[2].z;
					  v_21.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_22;
					  tmpvar_22 = normalize(((
					    (v_19.xyz * norm_18.x)
					   + 
					    (v_20.xyz * norm_18.y)
					  ) + (v_21.xyz * norm_18.z)));
					  normalWorld_4 = tmpvar_22;
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  highp vec4 tmpvar_23;
					  tmpvar_23 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD1 = (unity_WorldToLight * tmpvar_23);
					  xlv_TEXCOORD2 = (unity_WorldToShadow[0] * tmpvar_23);
					  xlv_TEXCOORD3 = tmpvar_7;
					  xlv_TEXCOORD4 = tmpvar_14;
					  xlv_TEXCOORD5 = normalWorld_4;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp sampler2D _ShadowMapTexture;
					uniform highp vec4 _ShadowOffsets[4];
					uniform sampler2D _LightTexture0;
					uniform sampler2D _LightTextureB0;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump float tmpvar_4;
					  tmpvar_4 = dot (xlv_TEXCOORD3.yzw, xlv_TEXCOORD4);
					  mediump vec2 tmpvar_5;
					  tmpvar_5.x = ((tmpvar_4 * tmpvar_4) * (tmpvar_4 * tmpvar_4));
					  tmpvar_5.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (unity_NHxRoughness, tmpvar_5);
					  c_1 = (((tmpvar_3 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_6.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic))
					  )) * _LightColor0.xyz);
					  lowp vec4 tmpvar_7;
					  highp vec2 P_8;
					  P_8 = ((xlv_TEXCOORD1.xy / xlv_TEXCOORD1.w) + 0.5);
					  tmpvar_7 = texture2D (_LightTexture0, P_8);
					  highp float tmpvar_9;
					  tmpvar_9 = dot (xlv_TEXCOORD1.xyz, xlv_TEXCOORD1.xyz);
					  lowp vec4 tmpvar_10;
					  tmpvar_10 = texture2D (_LightTextureB0, vec2(tmpvar_9));
					  lowp float tmpvar_11;
					  highp vec4 shadowVals_12;
					  highp vec3 tmpvar_13;
					  tmpvar_13 = (xlv_TEXCOORD2.xyz / xlv_TEXCOORD2.w);
					  shadowVals_12.x = texture2D (_ShadowMapTexture, (tmpvar_13.xy + _ShadowOffsets[0].xy)).x;
					  shadowVals_12.y = texture2D (_ShadowMapTexture, (tmpvar_13.xy + _ShadowOffsets[1].xy)).x;
					  shadowVals_12.z = texture2D (_ShadowMapTexture, (tmpvar_13.xy + _ShadowOffsets[2].xy)).x;
					  shadowVals_12.w = texture2D (_ShadowMapTexture, (tmpvar_13.xy + _ShadowOffsets[3].xy)).x;
					  bvec4 tmpvar_14;
					  tmpvar_14 = lessThan (shadowVals_12, tmpvar_13.zzzz);
					  mediump vec4 tmpvar_15;
					  tmpvar_15 = _LightShadowData.xxxx;
					  mediump float tmpvar_16;
					  if (tmpvar_14.x) {
					    tmpvar_16 = tmpvar_15.x;
					  } else {
					    tmpvar_16 = 1.0;
					  };
					  mediump float tmpvar_17;
					  if (tmpvar_14.y) {
					    tmpvar_17 = tmpvar_15.y;
					  } else {
					    tmpvar_17 = 1.0;
					  };
					  mediump float tmpvar_18;
					  if (tmpvar_14.z) {
					    tmpvar_18 = tmpvar_15.z;
					  } else {
					    tmpvar_18 = 1.0;
					  };
					  mediump float tmpvar_19;
					  if (tmpvar_14.w) {
					    tmpvar_19 = tmpvar_15.w;
					  } else {
					    tmpvar_19 = 1.0;
					  };
					  mediump vec4 tmpvar_20;
					  tmpvar_20.x = tmpvar_16;
					  tmpvar_20.y = tmpvar_17;
					  tmpvar_20.z = tmpvar_18;
					  tmpvar_20.w = tmpvar_19;
					  mediump float tmpvar_21;
					  tmpvar_21 = dot (tmpvar_20, vec4(0.25, 0.25, 0.25, 0.25));
					  tmpvar_11 = tmpvar_21;
					  c_1 = (c_1 * ((
					    ((float((xlv_TEXCOORD1.z > 0.0)) * tmpvar_7.w) * tmpvar_10.w)
					   * tmpvar_11) * max (0.0, 
					    dot (xlv_TEXCOORD5, xlv_TEXCOORD4)
					  )));
					  mediump vec4 tmpvar_22;
					  tmpvar_22.w = 1.0;
					  tmpvar_22.xyz = c_1;
					  mediump vec4 xlat_varoutput_23;
					  xlat_varoutput_23.xyz = tmpvar_22.xyz;
					  xlat_varoutput_23.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_23;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "SPOT" "SHADOWS_DEPTH" "SHADOWS_SOFT" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec4 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  highp vec3 lightDir_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_13;
					  highp vec3 n_14;
					  n_14 = (tmpvar_8.xyz - _WorldSpaceCameraPos);
					  tmpvar_13 = n_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_7.xyz = tmpvar_19;
					  highp vec3 tmpvar_20;
					  tmpvar_20 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  mediump vec3 tmpvar_21;
					  tmpvar_21 = tmpvar_20;
					  lightDir_4 = tmpvar_21;
					  tmpvar_5.w = lightDir_4.x;
					  tmpvar_6.w = lightDir_4.y;
					  tmpvar_7.w = lightDir_4.z;
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = tmpvar_13;
					  xlv_TEXCOORD2 = tmpvar_5;
					  xlv_TEXCOORD2_1 = tmpvar_6;
					  xlv_TEXCOORD2_2 = tmpvar_7;
					  highp vec4 tmpvar_22;
					  tmpvar_22 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD5 = (unity_WorldToLight * tmpvar_22);
					  xlv_TEXCOORD6 = (unity_WorldToShadow[0] * tmpvar_22);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp sampler2D _ShadowMapTexture;
					uniform highp vec4 _ShadowOffsets[4];
					uniform sampler2D _LightTexture0;
					uniform sampler2D _LightTextureB0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec4 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  mediump vec3 tmpvar_4;
					  tmpvar_4 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic));
					  tmpvar_3 = (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)));
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_6;
					  tmpvar_6 = normalize(xlv_TEXCOORD1);
					  mediump vec3 tmpvar_7;
					  tmpvar_7.x = xlv_TEXCOORD2.w;
					  tmpvar_7.y = xlv_TEXCOORD2_1.w;
					  tmpvar_7.z = xlv_TEXCOORD2_2.w;
					  lowp vec4 tmpvar_8;
					  highp vec2 P_9;
					  P_9 = ((xlv_TEXCOORD5.xy / xlv_TEXCOORD5.w) + 0.5);
					  tmpvar_8 = texture2D (_LightTexture0, P_9);
					  highp float tmpvar_10;
					  tmpvar_10 = dot (xlv_TEXCOORD5.xyz, xlv_TEXCOORD5.xyz);
					  lowp vec4 tmpvar_11;
					  tmpvar_11 = texture2D (_LightTextureB0, vec2(tmpvar_10));
					  lowp float tmpvar_12;
					  highp vec4 shadowVals_13;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = (xlv_TEXCOORD6.xyz / xlv_TEXCOORD6.w);
					  shadowVals_13.x = texture2D (_ShadowMapTexture, (tmpvar_14.xy + _ShadowOffsets[0].xy)).x;
					  shadowVals_13.y = texture2D (_ShadowMapTexture, (tmpvar_14.xy + _ShadowOffsets[1].xy)).x;
					  shadowVals_13.z = texture2D (_ShadowMapTexture, (tmpvar_14.xy + _ShadowOffsets[2].xy)).x;
					  shadowVals_13.w = texture2D (_ShadowMapTexture, (tmpvar_14.xy + _ShadowOffsets[3].xy)).x;
					  bvec4 tmpvar_15;
					  tmpvar_15 = lessThan (shadowVals_13, tmpvar_14.zzzz);
					  mediump vec4 tmpvar_16;
					  tmpvar_16 = _LightShadowData.xxxx;
					  mediump float tmpvar_17;
					  if (tmpvar_15.x) {
					    tmpvar_17 = tmpvar_16.x;
					  } else {
					    tmpvar_17 = 1.0;
					  };
					  mediump float tmpvar_18;
					  if (tmpvar_15.y) {
					    tmpvar_18 = tmpvar_16.y;
					  } else {
					    tmpvar_18 = 1.0;
					  };
					  mediump float tmpvar_19;
					  if (tmpvar_15.z) {
					    tmpvar_19 = tmpvar_16.z;
					  } else {
					    tmpvar_19 = 1.0;
					  };
					  mediump float tmpvar_20;
					  if (tmpvar_15.w) {
					    tmpvar_20 = tmpvar_16.w;
					  } else {
					    tmpvar_20 = 1.0;
					  };
					  mediump vec4 tmpvar_21;
					  tmpvar_21.x = tmpvar_17;
					  tmpvar_21.y = tmpvar_18;
					  tmpvar_21.z = tmpvar_19;
					  tmpvar_21.w = tmpvar_20;
					  mediump float tmpvar_22;
					  tmpvar_22 = dot (tmpvar_21, vec4(0.25, 0.25, 0.25, 0.25));
					  tmpvar_12 = tmpvar_22;
					  mediump float atten_23;
					  atten_23 = (((
					    float((xlv_TEXCOORD5.z > 0.0))
					   * tmpvar_8.w) * tmpvar_11.w) * tmpvar_12);
					  mediump vec3 tmpvar_24;
					  tmpvar_24 = _LightColor0.xyz;
					  mediump vec3 tmpvar_25;
					  tmpvar_25 = normalize(tmpvar_7);
					  tmpvar_24 = (tmpvar_24 * atten_23);
					  mediump float specular_26;
					  mediump vec3 tmpvar_27;
					  mediump vec3 inVec_28;
					  inVec_28 = (tmpvar_25 - tmpvar_6);
					  tmpvar_27 = (inVec_28 * inversesqrt(max (0.001, 
					    dot (inVec_28, inVec_28)
					  )));
					  mediump float tmpvar_29;
					  tmpvar_29 = max (0.0, dot (tmpvar_25, tmpvar_27));
					  mediump float tmpvar_30;
					  tmpvar_30 = (1.0 - _Glossiness);
					  mediump float tmpvar_31;
					  tmpvar_31 = max (0.0001, (tmpvar_30 * tmpvar_30));
					  mediump float tmpvar_32;
					  tmpvar_32 = max (((2.0 / 
					    (tmpvar_31 * tmpvar_31)
					  ) - 2.0), 0.0001);
					  specular_26 = sqrt(max (0.0001, (
					    ((tmpvar_32 + 1.0) * pow (max (0.0, dot (tmpvar_5, tmpvar_27)), tmpvar_32))
					   / 
					    (((8.0 * (
					      ((tmpvar_29 * tmpvar_29) * _Glossiness)
					     + 
					      (tmpvar_30 * tmpvar_30)
					    )) * tmpvar_29) + 0.0001)
					  )));
					  mediump float tmpvar_33;
					  tmpvar_33 = clamp (specular_26, 0.0, 100.0);
					  specular_26 = tmpvar_33;
					  mediump vec4 tmpvar_34;
					  tmpvar_34.w = 1.0;
					  tmpvar_34.xyz = (((tmpvar_3 + 
					    (tmpvar_33 * tmpvar_4)
					  ) * tmpvar_24) * max (0.0, dot (tmpvar_5, tmpvar_25)));
					  mediump vec4 xlat_varoutput_35;
					  xlat_varoutput_35.xyz = tmpvar_34.xyz;
					  xlat_varoutput_35.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_35;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "SPOT" "SHADOWS_DEPTH" "SHADOWS_SOFT" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec4 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  highp vec3 lightDir_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_13;
					  highp vec3 n_14;
					  n_14 = (tmpvar_8.xyz - _WorldSpaceCameraPos);
					  tmpvar_13 = n_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_7.xyz = tmpvar_19;
					  highp vec3 tmpvar_20;
					  tmpvar_20 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  mediump vec3 tmpvar_21;
					  tmpvar_21 = tmpvar_20;
					  lightDir_4 = tmpvar_21;
					  tmpvar_5.w = lightDir_4.x;
					  tmpvar_6.w = lightDir_4.y;
					  tmpvar_7.w = lightDir_4.z;
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = tmpvar_13;
					  xlv_TEXCOORD2 = tmpvar_5;
					  xlv_TEXCOORD2_1 = tmpvar_6;
					  xlv_TEXCOORD2_2 = tmpvar_7;
					  highp vec4 tmpvar_22;
					  tmpvar_22 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD5 = (unity_WorldToLight * tmpvar_22);
					  xlv_TEXCOORD6 = (unity_WorldToShadow[0] * tmpvar_22);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp sampler2D _ShadowMapTexture;
					uniform highp vec4 _ShadowOffsets[4];
					uniform sampler2D _LightTexture0;
					uniform sampler2D _LightTextureB0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec4 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  mediump vec3 tmpvar_4;
					  tmpvar_4 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic));
					  tmpvar_3 = (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)));
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_6;
					  tmpvar_6 = normalize(xlv_TEXCOORD1);
					  mediump vec3 tmpvar_7;
					  tmpvar_7.x = xlv_TEXCOORD2.w;
					  tmpvar_7.y = xlv_TEXCOORD2_1.w;
					  tmpvar_7.z = xlv_TEXCOORD2_2.w;
					  lowp vec4 tmpvar_8;
					  highp vec2 P_9;
					  P_9 = ((xlv_TEXCOORD5.xy / xlv_TEXCOORD5.w) + 0.5);
					  tmpvar_8 = texture2D (_LightTexture0, P_9);
					  highp float tmpvar_10;
					  tmpvar_10 = dot (xlv_TEXCOORD5.xyz, xlv_TEXCOORD5.xyz);
					  lowp vec4 tmpvar_11;
					  tmpvar_11 = texture2D (_LightTextureB0, vec2(tmpvar_10));
					  lowp float tmpvar_12;
					  highp vec4 shadowVals_13;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = (xlv_TEXCOORD6.xyz / xlv_TEXCOORD6.w);
					  shadowVals_13.x = texture2D (_ShadowMapTexture, (tmpvar_14.xy + _ShadowOffsets[0].xy)).x;
					  shadowVals_13.y = texture2D (_ShadowMapTexture, (tmpvar_14.xy + _ShadowOffsets[1].xy)).x;
					  shadowVals_13.z = texture2D (_ShadowMapTexture, (tmpvar_14.xy + _ShadowOffsets[2].xy)).x;
					  shadowVals_13.w = texture2D (_ShadowMapTexture, (tmpvar_14.xy + _ShadowOffsets[3].xy)).x;
					  bvec4 tmpvar_15;
					  tmpvar_15 = lessThan (shadowVals_13, tmpvar_14.zzzz);
					  mediump vec4 tmpvar_16;
					  tmpvar_16 = _LightShadowData.xxxx;
					  mediump float tmpvar_17;
					  if (tmpvar_15.x) {
					    tmpvar_17 = tmpvar_16.x;
					  } else {
					    tmpvar_17 = 1.0;
					  };
					  mediump float tmpvar_18;
					  if (tmpvar_15.y) {
					    tmpvar_18 = tmpvar_16.y;
					  } else {
					    tmpvar_18 = 1.0;
					  };
					  mediump float tmpvar_19;
					  if (tmpvar_15.z) {
					    tmpvar_19 = tmpvar_16.z;
					  } else {
					    tmpvar_19 = 1.0;
					  };
					  mediump float tmpvar_20;
					  if (tmpvar_15.w) {
					    tmpvar_20 = tmpvar_16.w;
					  } else {
					    tmpvar_20 = 1.0;
					  };
					  mediump vec4 tmpvar_21;
					  tmpvar_21.x = tmpvar_17;
					  tmpvar_21.y = tmpvar_18;
					  tmpvar_21.z = tmpvar_19;
					  tmpvar_21.w = tmpvar_20;
					  mediump float tmpvar_22;
					  tmpvar_22 = dot (tmpvar_21, vec4(0.25, 0.25, 0.25, 0.25));
					  tmpvar_12 = tmpvar_22;
					  mediump float atten_23;
					  atten_23 = (((
					    float((xlv_TEXCOORD5.z > 0.0))
					   * tmpvar_8.w) * tmpvar_11.w) * tmpvar_12);
					  mediump vec3 tmpvar_24;
					  tmpvar_24 = _LightColor0.xyz;
					  mediump vec3 tmpvar_25;
					  tmpvar_25 = normalize(tmpvar_7);
					  tmpvar_24 = (tmpvar_24 * atten_23);
					  mediump float specular_26;
					  mediump vec3 tmpvar_27;
					  mediump vec3 inVec_28;
					  inVec_28 = (tmpvar_25 - tmpvar_6);
					  tmpvar_27 = (inVec_28 * inversesqrt(max (0.001, 
					    dot (inVec_28, inVec_28)
					  )));
					  mediump float tmpvar_29;
					  tmpvar_29 = max (0.0, dot (tmpvar_25, tmpvar_27));
					  mediump float tmpvar_30;
					  tmpvar_30 = (1.0 - _Glossiness);
					  mediump float tmpvar_31;
					  tmpvar_31 = max (0.0001, (tmpvar_30 * tmpvar_30));
					  mediump float tmpvar_32;
					  tmpvar_32 = max (((2.0 / 
					    (tmpvar_31 * tmpvar_31)
					  ) - 2.0), 0.0001);
					  specular_26 = sqrt(max (0.0001, (
					    ((tmpvar_32 + 1.0) * pow (max (0.0, dot (tmpvar_5, tmpvar_27)), tmpvar_32))
					   / 
					    (((8.0 * (
					      ((tmpvar_29 * tmpvar_29) * _Glossiness)
					     + 
					      (tmpvar_30 * tmpvar_30)
					    )) * tmpvar_29) + 0.0001)
					  )));
					  mediump float tmpvar_33;
					  tmpvar_33 = clamp (specular_26, 0.0, 100.0);
					  specular_26 = tmpvar_33;
					  mediump vec4 tmpvar_34;
					  tmpvar_34.w = 1.0;
					  tmpvar_34.xyz = (((tmpvar_3 + 
					    (tmpvar_33 * tmpvar_4)
					  ) * tmpvar_24) * max (0.0, dot (tmpvar_5, tmpvar_25)));
					  mediump vec4 xlat_varoutput_35;
					  xlat_varoutput_35.xyz = tmpvar_34.xyz;
					  xlat_varoutput_35.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_35;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "POINT" "SHADOWS_CUBE" "SHADOWS_SOFT" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp vec4 _LightPositionRange;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD1;
					varying highp vec3 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec3 lightDir_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_13;
					  tmpvar_13 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  lightDir_6 = tmpvar_13;
					  mediump vec3 tmpvar_14;
					  highp vec3 n_15;
					  n_15 = lightDir_6;
					  highp vec3 tmpvar_16;
					  tmpvar_16 = normalize(n_15);
					  tmpvar_14 = tmpvar_16;
					  lightDir_6 = tmpvar_14;
					  highp vec3 tmpvar_17;
					  tmpvar_17 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_17;
					  highp vec3 norm_18;
					  norm_18 = tmpvar_1;
					  highp vec4 v_19;
					  v_19.x = unity_WorldToObject[0].x;
					  v_19.y = unity_WorldToObject[1].x;
					  v_19.z = unity_WorldToObject[2].x;
					  v_19.w = unity_WorldToObject[3].x;
					  highp vec4 v_20;
					  v_20.x = unity_WorldToObject[0].y;
					  v_20.y = unity_WorldToObject[1].y;
					  v_20.z = unity_WorldToObject[2].y;
					  v_20.w = unity_WorldToObject[3].y;
					  highp vec4 v_21;
					  v_21.x = unity_WorldToObject[0].z;
					  v_21.y = unity_WorldToObject[1].z;
					  v_21.z = unity_WorldToObject[2].z;
					  v_21.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_22;
					  tmpvar_22 = normalize(((
					    (v_19.xyz * norm_18.x)
					   + 
					    (v_20.xyz * norm_18.y)
					  ) + (v_21.xyz * norm_18.z)));
					  normalWorld_4 = tmpvar_22;
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  highp vec4 tmpvar_23;
					  tmpvar_23 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD1 = (unity_WorldToLight * tmpvar_23).xyz;
					  xlv_TEXCOORD2 = (tmpvar_23.xyz - _LightPositionRange.xyz);
					  xlv_TEXCOORD3 = tmpvar_7;
					  xlv_TEXCOORD4 = tmpvar_14;
					  xlv_TEXCOORD5 = normalWorld_4;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _LightPositionRange;
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp samplerCube _ShadowMapTexture;
					uniform sampler2D _LightTexture0;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD1;
					varying highp vec3 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump float tmpvar_4;
					  tmpvar_4 = dot (xlv_TEXCOORD3.yzw, xlv_TEXCOORD4);
					  mediump vec2 tmpvar_5;
					  tmpvar_5.x = ((tmpvar_4 * tmpvar_4) * (tmpvar_4 * tmpvar_4));
					  tmpvar_5.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (unity_NHxRoughness, tmpvar_5);
					  c_1 = (((tmpvar_3 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_6.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic))
					  )) * _LightColor0.xyz);
					  highp float tmpvar_7;
					  tmpvar_7 = dot (xlv_TEXCOORD1, xlv_TEXCOORD1);
					  lowp vec4 tmpvar_8;
					  tmpvar_8 = texture2D (_LightTexture0, vec2(tmpvar_7));
					  highp vec4 shadowVals_9;
					  highp float mydist_10;
					  mydist_10 = ((sqrt(
					    dot (xlv_TEXCOORD2, xlv_TEXCOORD2)
					  ) * _LightPositionRange.w) * 0.97);
					  shadowVals_9.x = dot (textureCube (_ShadowMapTexture, (xlv_TEXCOORD2 + vec3(0.0078125, 0.0078125, 0.0078125))), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  shadowVals_9.y = dot (textureCube (_ShadowMapTexture, (xlv_TEXCOORD2 + vec3(-0.0078125, -0.0078125, 0.0078125))), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  shadowVals_9.z = dot (textureCube (_ShadowMapTexture, (xlv_TEXCOORD2 + vec3(-0.0078125, 0.0078125, -0.0078125))), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  shadowVals_9.w = dot (textureCube (_ShadowMapTexture, (xlv_TEXCOORD2 + vec3(0.0078125, -0.0078125, -0.0078125))), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  bvec4 tmpvar_11;
					  tmpvar_11 = lessThan (shadowVals_9, vec4(mydist_10));
					  mediump vec4 tmpvar_12;
					  tmpvar_12 = _LightShadowData.xxxx;
					  mediump float tmpvar_13;
					  if (tmpvar_11.x) {
					    tmpvar_13 = tmpvar_12.x;
					  } else {
					    tmpvar_13 = 1.0;
					  };
					  mediump float tmpvar_14;
					  if (tmpvar_11.y) {
					    tmpvar_14 = tmpvar_12.y;
					  } else {
					    tmpvar_14 = 1.0;
					  };
					  mediump float tmpvar_15;
					  if (tmpvar_11.z) {
					    tmpvar_15 = tmpvar_12.z;
					  } else {
					    tmpvar_15 = 1.0;
					  };
					  mediump float tmpvar_16;
					  if (tmpvar_11.w) {
					    tmpvar_16 = tmpvar_12.w;
					  } else {
					    tmpvar_16 = 1.0;
					  };
					  mediump vec4 tmpvar_17;
					  tmpvar_17.x = tmpvar_13;
					  tmpvar_17.y = tmpvar_14;
					  tmpvar_17.z = tmpvar_15;
					  tmpvar_17.w = tmpvar_16;
					  c_1 = (c_1 * ((tmpvar_8.w * 
					    dot (tmpvar_17, vec4(0.25, 0.25, 0.25, 0.25))
					  ) * max (0.0, 
					    dot (xlv_TEXCOORD5, xlv_TEXCOORD4)
					  )));
					  mediump vec4 tmpvar_18;
					  tmpvar_18.w = 1.0;
					  tmpvar_18.xyz = c_1;
					  mediump vec4 xlat_varoutput_19;
					  xlat_varoutput_19.xyz = tmpvar_18.xyz;
					  xlat_varoutput_19.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_19;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "POINT" "SHADOWS_CUBE" "SHADOWS_SOFT" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp vec4 _LightPositionRange;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					varying highp vec3 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  highp vec3 lightDir_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_13;
					  highp vec3 n_14;
					  n_14 = (tmpvar_8.xyz - _WorldSpaceCameraPos);
					  tmpvar_13 = n_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_7.xyz = tmpvar_19;
					  highp vec3 tmpvar_20;
					  tmpvar_20 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  mediump vec3 tmpvar_21;
					  tmpvar_21 = tmpvar_20;
					  lightDir_4 = tmpvar_21;
					  tmpvar_5.w = lightDir_4.x;
					  tmpvar_6.w = lightDir_4.y;
					  tmpvar_7.w = lightDir_4.z;
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = tmpvar_13;
					  xlv_TEXCOORD2 = tmpvar_5;
					  xlv_TEXCOORD2_1 = tmpvar_6;
					  xlv_TEXCOORD2_2 = tmpvar_7;
					  highp vec4 tmpvar_22;
					  tmpvar_22 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD5 = (unity_WorldToLight * tmpvar_22).xyz;
					  xlv_TEXCOORD6 = (tmpvar_22.xyz - _LightPositionRange.xyz);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _LightPositionRange;
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp samplerCube _ShadowMapTexture;
					uniform sampler2D _LightTexture0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					varying highp vec3 xlv_TEXCOORD6;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  mediump vec3 tmpvar_4;
					  tmpvar_4 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic));
					  tmpvar_3 = (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)));
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_6;
					  tmpvar_6 = normalize(xlv_TEXCOORD1);
					  mediump vec3 tmpvar_7;
					  tmpvar_7.x = xlv_TEXCOORD2.w;
					  tmpvar_7.y = xlv_TEXCOORD2_1.w;
					  tmpvar_7.z = xlv_TEXCOORD2_2.w;
					  highp float tmpvar_8;
					  tmpvar_8 = dot (xlv_TEXCOORD5, xlv_TEXCOORD5);
					  lowp vec4 tmpvar_9;
					  tmpvar_9 = texture2D (_LightTexture0, vec2(tmpvar_8));
					  highp vec4 shadowVals_10;
					  highp float mydist_11;
					  mydist_11 = ((sqrt(
					    dot (xlv_TEXCOORD6, xlv_TEXCOORD6)
					  ) * _LightPositionRange.w) * 0.97);
					  shadowVals_10.x = dot (textureCube (_ShadowMapTexture, (xlv_TEXCOORD6 + vec3(0.0078125, 0.0078125, 0.0078125))), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  shadowVals_10.y = dot (textureCube (_ShadowMapTexture, (xlv_TEXCOORD6 + vec3(-0.0078125, -0.0078125, 0.0078125))), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  shadowVals_10.z = dot (textureCube (_ShadowMapTexture, (xlv_TEXCOORD6 + vec3(-0.0078125, 0.0078125, -0.0078125))), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  shadowVals_10.w = dot (textureCube (_ShadowMapTexture, (xlv_TEXCOORD6 + vec3(0.0078125, -0.0078125, -0.0078125))), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  bvec4 tmpvar_12;
					  tmpvar_12 = lessThan (shadowVals_10, vec4(mydist_11));
					  mediump vec4 tmpvar_13;
					  tmpvar_13 = _LightShadowData.xxxx;
					  mediump float tmpvar_14;
					  if (tmpvar_12.x) {
					    tmpvar_14 = tmpvar_13.x;
					  } else {
					    tmpvar_14 = 1.0;
					  };
					  mediump float tmpvar_15;
					  if (tmpvar_12.y) {
					    tmpvar_15 = tmpvar_13.y;
					  } else {
					    tmpvar_15 = 1.0;
					  };
					  mediump float tmpvar_16;
					  if (tmpvar_12.z) {
					    tmpvar_16 = tmpvar_13.z;
					  } else {
					    tmpvar_16 = 1.0;
					  };
					  mediump float tmpvar_17;
					  if (tmpvar_12.w) {
					    tmpvar_17 = tmpvar_13.w;
					  } else {
					    tmpvar_17 = 1.0;
					  };
					  mediump vec4 tmpvar_18;
					  tmpvar_18.x = tmpvar_14;
					  tmpvar_18.y = tmpvar_15;
					  tmpvar_18.z = tmpvar_16;
					  tmpvar_18.w = tmpvar_17;
					  mediump vec3 tmpvar_19;
					  tmpvar_19 = _LightColor0.xyz;
					  mediump vec3 tmpvar_20;
					  tmpvar_20 = normalize(tmpvar_7);
					  tmpvar_19 = (tmpvar_19 * (tmpvar_9.w * dot (tmpvar_18, vec4(0.25, 0.25, 0.25, 0.25))));
					  mediump float specular_21;
					  mediump vec3 tmpvar_22;
					  mediump vec3 inVec_23;
					  inVec_23 = (tmpvar_20 - tmpvar_6);
					  tmpvar_22 = (inVec_23 * inversesqrt(max (0.001, 
					    dot (inVec_23, inVec_23)
					  )));
					  mediump float tmpvar_24;
					  tmpvar_24 = max (0.0, dot (tmpvar_20, tmpvar_22));
					  mediump float tmpvar_25;
					  tmpvar_25 = (1.0 - _Glossiness);
					  mediump float tmpvar_26;
					  tmpvar_26 = max (0.0001, (tmpvar_25 * tmpvar_25));
					  mediump float tmpvar_27;
					  tmpvar_27 = max (((2.0 / 
					    (tmpvar_26 * tmpvar_26)
					  ) - 2.0), 0.0001);
					  specular_21 = sqrt(max (0.0001, (
					    ((tmpvar_27 + 1.0) * pow (max (0.0, dot (tmpvar_5, tmpvar_22)), tmpvar_27))
					   / 
					    (((8.0 * (
					      ((tmpvar_24 * tmpvar_24) * _Glossiness)
					     + 
					      (tmpvar_25 * tmpvar_25)
					    )) * tmpvar_24) + 0.0001)
					  )));
					  mediump float tmpvar_28;
					  tmpvar_28 = clamp (specular_21, 0.0, 100.0);
					  specular_21 = tmpvar_28;
					  mediump vec4 tmpvar_29;
					  tmpvar_29.w = 1.0;
					  tmpvar_29.xyz = (((tmpvar_3 + 
					    (tmpvar_28 * tmpvar_4)
					  ) * tmpvar_19) * max (0.0, dot (tmpvar_5, tmpvar_20)));
					  mediump vec4 xlat_varoutput_30;
					  xlat_varoutput_30.xyz = tmpvar_29.xyz;
					  xlat_varoutput_30.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_30;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "POINT" "SHADOWS_CUBE" "SHADOWS_SOFT" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp vec4 _LightPositionRange;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					varying highp vec3 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  highp vec3 lightDir_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_13;
					  highp vec3 n_14;
					  n_14 = (tmpvar_8.xyz - _WorldSpaceCameraPos);
					  tmpvar_13 = n_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_7.xyz = tmpvar_19;
					  highp vec3 tmpvar_20;
					  tmpvar_20 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  mediump vec3 tmpvar_21;
					  tmpvar_21 = tmpvar_20;
					  lightDir_4 = tmpvar_21;
					  tmpvar_5.w = lightDir_4.x;
					  tmpvar_6.w = lightDir_4.y;
					  tmpvar_7.w = lightDir_4.z;
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = tmpvar_13;
					  xlv_TEXCOORD2 = tmpvar_5;
					  xlv_TEXCOORD2_1 = tmpvar_6;
					  xlv_TEXCOORD2_2 = tmpvar_7;
					  highp vec4 tmpvar_22;
					  tmpvar_22 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD5 = (unity_WorldToLight * tmpvar_22).xyz;
					  xlv_TEXCOORD6 = (tmpvar_22.xyz - _LightPositionRange.xyz);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _LightPositionRange;
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp samplerCube _ShadowMapTexture;
					uniform sampler2D _LightTexture0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					varying highp vec3 xlv_TEXCOORD6;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  mediump vec3 tmpvar_4;
					  tmpvar_4 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic));
					  tmpvar_3 = (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)));
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_6;
					  tmpvar_6 = normalize(xlv_TEXCOORD1);
					  mediump vec3 tmpvar_7;
					  tmpvar_7.x = xlv_TEXCOORD2.w;
					  tmpvar_7.y = xlv_TEXCOORD2_1.w;
					  tmpvar_7.z = xlv_TEXCOORD2_2.w;
					  highp float tmpvar_8;
					  tmpvar_8 = dot (xlv_TEXCOORD5, xlv_TEXCOORD5);
					  lowp vec4 tmpvar_9;
					  tmpvar_9 = texture2D (_LightTexture0, vec2(tmpvar_8));
					  highp vec4 shadowVals_10;
					  highp float mydist_11;
					  mydist_11 = ((sqrt(
					    dot (xlv_TEXCOORD6, xlv_TEXCOORD6)
					  ) * _LightPositionRange.w) * 0.97);
					  shadowVals_10.x = dot (textureCube (_ShadowMapTexture, (xlv_TEXCOORD6 + vec3(0.0078125, 0.0078125, 0.0078125))), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  shadowVals_10.y = dot (textureCube (_ShadowMapTexture, (xlv_TEXCOORD6 + vec3(-0.0078125, -0.0078125, 0.0078125))), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  shadowVals_10.z = dot (textureCube (_ShadowMapTexture, (xlv_TEXCOORD6 + vec3(-0.0078125, 0.0078125, -0.0078125))), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  shadowVals_10.w = dot (textureCube (_ShadowMapTexture, (xlv_TEXCOORD6 + vec3(0.0078125, -0.0078125, -0.0078125))), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  bvec4 tmpvar_12;
					  tmpvar_12 = lessThan (shadowVals_10, vec4(mydist_11));
					  mediump vec4 tmpvar_13;
					  tmpvar_13 = _LightShadowData.xxxx;
					  mediump float tmpvar_14;
					  if (tmpvar_12.x) {
					    tmpvar_14 = tmpvar_13.x;
					  } else {
					    tmpvar_14 = 1.0;
					  };
					  mediump float tmpvar_15;
					  if (tmpvar_12.y) {
					    tmpvar_15 = tmpvar_13.y;
					  } else {
					    tmpvar_15 = 1.0;
					  };
					  mediump float tmpvar_16;
					  if (tmpvar_12.z) {
					    tmpvar_16 = tmpvar_13.z;
					  } else {
					    tmpvar_16 = 1.0;
					  };
					  mediump float tmpvar_17;
					  if (tmpvar_12.w) {
					    tmpvar_17 = tmpvar_13.w;
					  } else {
					    tmpvar_17 = 1.0;
					  };
					  mediump vec4 tmpvar_18;
					  tmpvar_18.x = tmpvar_14;
					  tmpvar_18.y = tmpvar_15;
					  tmpvar_18.z = tmpvar_16;
					  tmpvar_18.w = tmpvar_17;
					  mediump vec3 tmpvar_19;
					  tmpvar_19 = _LightColor0.xyz;
					  mediump vec3 tmpvar_20;
					  tmpvar_20 = normalize(tmpvar_7);
					  tmpvar_19 = (tmpvar_19 * (tmpvar_9.w * dot (tmpvar_18, vec4(0.25, 0.25, 0.25, 0.25))));
					  mediump float specular_21;
					  mediump vec3 tmpvar_22;
					  mediump vec3 inVec_23;
					  inVec_23 = (tmpvar_20 - tmpvar_6);
					  tmpvar_22 = (inVec_23 * inversesqrt(max (0.001, 
					    dot (inVec_23, inVec_23)
					  )));
					  mediump float tmpvar_24;
					  tmpvar_24 = max (0.0, dot (tmpvar_20, tmpvar_22));
					  mediump float tmpvar_25;
					  tmpvar_25 = (1.0 - _Glossiness);
					  mediump float tmpvar_26;
					  tmpvar_26 = max (0.0001, (tmpvar_25 * tmpvar_25));
					  mediump float tmpvar_27;
					  tmpvar_27 = max (((2.0 / 
					    (tmpvar_26 * tmpvar_26)
					  ) - 2.0), 0.0001);
					  specular_21 = sqrt(max (0.0001, (
					    ((tmpvar_27 + 1.0) * pow (max (0.0, dot (tmpvar_5, tmpvar_22)), tmpvar_27))
					   / 
					    (((8.0 * (
					      ((tmpvar_24 * tmpvar_24) * _Glossiness)
					     + 
					      (tmpvar_25 * tmpvar_25)
					    )) * tmpvar_24) + 0.0001)
					  )));
					  mediump float tmpvar_28;
					  tmpvar_28 = clamp (specular_21, 0.0, 100.0);
					  specular_21 = tmpvar_28;
					  mediump vec4 tmpvar_29;
					  tmpvar_29.w = 1.0;
					  tmpvar_29.xyz = (((tmpvar_3 + 
					    (tmpvar_28 * tmpvar_4)
					  ) * tmpvar_19) * max (0.0, dot (tmpvar_5, tmpvar_20)));
					  mediump vec4 xlat_varoutput_30;
					  xlat_varoutput_30.xyz = tmpvar_29.xyz;
					  xlat_varoutput_30.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_30;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" "SHADOWS_SOFT" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp vec4 _LightPositionRange;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD1;
					varying highp vec3 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec3 lightDir_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_13;
					  tmpvar_13 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  lightDir_6 = tmpvar_13;
					  mediump vec3 tmpvar_14;
					  highp vec3 n_15;
					  n_15 = lightDir_6;
					  highp vec3 tmpvar_16;
					  tmpvar_16 = normalize(n_15);
					  tmpvar_14 = tmpvar_16;
					  lightDir_6 = tmpvar_14;
					  highp vec3 tmpvar_17;
					  tmpvar_17 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_17;
					  highp vec3 norm_18;
					  norm_18 = tmpvar_1;
					  highp vec4 v_19;
					  v_19.x = unity_WorldToObject[0].x;
					  v_19.y = unity_WorldToObject[1].x;
					  v_19.z = unity_WorldToObject[2].x;
					  v_19.w = unity_WorldToObject[3].x;
					  highp vec4 v_20;
					  v_20.x = unity_WorldToObject[0].y;
					  v_20.y = unity_WorldToObject[1].y;
					  v_20.z = unity_WorldToObject[2].y;
					  v_20.w = unity_WorldToObject[3].y;
					  highp vec4 v_21;
					  v_21.x = unity_WorldToObject[0].z;
					  v_21.y = unity_WorldToObject[1].z;
					  v_21.z = unity_WorldToObject[2].z;
					  v_21.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_22;
					  tmpvar_22 = normalize(((
					    (v_19.xyz * norm_18.x)
					   + 
					    (v_20.xyz * norm_18.y)
					  ) + (v_21.xyz * norm_18.z)));
					  normalWorld_4 = tmpvar_22;
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  highp vec4 tmpvar_23;
					  tmpvar_23 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD1 = (unity_WorldToLight * tmpvar_23).xyz;
					  xlv_TEXCOORD2 = (tmpvar_23.xyz - _LightPositionRange.xyz);
					  xlv_TEXCOORD3 = tmpvar_7;
					  xlv_TEXCOORD4 = tmpvar_14;
					  xlv_TEXCOORD5 = normalWorld_4;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _LightPositionRange;
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp samplerCube _ShadowMapTexture;
					uniform lowp samplerCube _LightTexture0;
					uniform sampler2D _LightTextureB0;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD1;
					varying highp vec3 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump float tmpvar_4;
					  tmpvar_4 = dot (xlv_TEXCOORD3.yzw, xlv_TEXCOORD4);
					  mediump vec2 tmpvar_5;
					  tmpvar_5.x = ((tmpvar_4 * tmpvar_4) * (tmpvar_4 * tmpvar_4));
					  tmpvar_5.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (unity_NHxRoughness, tmpvar_5);
					  c_1 = (((tmpvar_3 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_6.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic))
					  )) * _LightColor0.xyz);
					  highp float tmpvar_7;
					  tmpvar_7 = dot (xlv_TEXCOORD1, xlv_TEXCOORD1);
					  lowp vec4 tmpvar_8;
					  tmpvar_8 = texture2D (_LightTextureB0, vec2(tmpvar_7));
					  lowp vec4 tmpvar_9;
					  tmpvar_9 = textureCube (_LightTexture0, xlv_TEXCOORD1);
					  highp vec4 shadowVals_10;
					  highp float mydist_11;
					  mydist_11 = ((sqrt(
					    dot (xlv_TEXCOORD2, xlv_TEXCOORD2)
					  ) * _LightPositionRange.w) * 0.97);
					  shadowVals_10.x = dot (textureCube (_ShadowMapTexture, (xlv_TEXCOORD2 + vec3(0.0078125, 0.0078125, 0.0078125))), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  shadowVals_10.y = dot (textureCube (_ShadowMapTexture, (xlv_TEXCOORD2 + vec3(-0.0078125, -0.0078125, 0.0078125))), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  shadowVals_10.z = dot (textureCube (_ShadowMapTexture, (xlv_TEXCOORD2 + vec3(-0.0078125, 0.0078125, -0.0078125))), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  shadowVals_10.w = dot (textureCube (_ShadowMapTexture, (xlv_TEXCOORD2 + vec3(0.0078125, -0.0078125, -0.0078125))), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  bvec4 tmpvar_12;
					  tmpvar_12 = lessThan (shadowVals_10, vec4(mydist_11));
					  mediump vec4 tmpvar_13;
					  tmpvar_13 = _LightShadowData.xxxx;
					  mediump float tmpvar_14;
					  if (tmpvar_12.x) {
					    tmpvar_14 = tmpvar_13.x;
					  } else {
					    tmpvar_14 = 1.0;
					  };
					  mediump float tmpvar_15;
					  if (tmpvar_12.y) {
					    tmpvar_15 = tmpvar_13.y;
					  } else {
					    tmpvar_15 = 1.0;
					  };
					  mediump float tmpvar_16;
					  if (tmpvar_12.z) {
					    tmpvar_16 = tmpvar_13.z;
					  } else {
					    tmpvar_16 = 1.0;
					  };
					  mediump float tmpvar_17;
					  if (tmpvar_12.w) {
					    tmpvar_17 = tmpvar_13.w;
					  } else {
					    tmpvar_17 = 1.0;
					  };
					  mediump vec4 tmpvar_18;
					  tmpvar_18.x = tmpvar_14;
					  tmpvar_18.y = tmpvar_15;
					  tmpvar_18.z = tmpvar_16;
					  tmpvar_18.w = tmpvar_17;
					  c_1 = (c_1 * ((
					    (tmpvar_8.w * tmpvar_9.w)
					   * 
					    dot (tmpvar_18, vec4(0.25, 0.25, 0.25, 0.25))
					  ) * max (0.0, 
					    dot (xlv_TEXCOORD5, xlv_TEXCOORD4)
					  )));
					  mediump vec4 tmpvar_19;
					  tmpvar_19.w = 1.0;
					  tmpvar_19.xyz = c_1;
					  mediump vec4 xlat_varoutput_20;
					  xlat_varoutput_20.xyz = tmpvar_19.xyz;
					  xlat_varoutput_20.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_20;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" "SHADOWS_SOFT" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp vec4 _LightPositionRange;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					varying highp vec3 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  highp vec3 lightDir_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_13;
					  highp vec3 n_14;
					  n_14 = (tmpvar_8.xyz - _WorldSpaceCameraPos);
					  tmpvar_13 = n_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_7.xyz = tmpvar_19;
					  highp vec3 tmpvar_20;
					  tmpvar_20 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  mediump vec3 tmpvar_21;
					  tmpvar_21 = tmpvar_20;
					  lightDir_4 = tmpvar_21;
					  tmpvar_5.w = lightDir_4.x;
					  tmpvar_6.w = lightDir_4.y;
					  tmpvar_7.w = lightDir_4.z;
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = tmpvar_13;
					  xlv_TEXCOORD2 = tmpvar_5;
					  xlv_TEXCOORD2_1 = tmpvar_6;
					  xlv_TEXCOORD2_2 = tmpvar_7;
					  highp vec4 tmpvar_22;
					  tmpvar_22 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD5 = (unity_WorldToLight * tmpvar_22).xyz;
					  xlv_TEXCOORD6 = (tmpvar_22.xyz - _LightPositionRange.xyz);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _LightPositionRange;
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp samplerCube _ShadowMapTexture;
					uniform lowp samplerCube _LightTexture0;
					uniform sampler2D _LightTextureB0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					varying highp vec3 xlv_TEXCOORD6;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  mediump vec3 tmpvar_4;
					  tmpvar_4 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic));
					  tmpvar_3 = (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)));
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_6;
					  tmpvar_6 = normalize(xlv_TEXCOORD1);
					  mediump vec3 tmpvar_7;
					  tmpvar_7.x = xlv_TEXCOORD2.w;
					  tmpvar_7.y = xlv_TEXCOORD2_1.w;
					  tmpvar_7.z = xlv_TEXCOORD2_2.w;
					  highp float tmpvar_8;
					  tmpvar_8 = dot (xlv_TEXCOORD5, xlv_TEXCOORD5);
					  lowp vec4 tmpvar_9;
					  tmpvar_9 = texture2D (_LightTextureB0, vec2(tmpvar_8));
					  lowp vec4 tmpvar_10;
					  tmpvar_10 = textureCube (_LightTexture0, xlv_TEXCOORD5);
					  highp vec4 shadowVals_11;
					  highp float mydist_12;
					  mydist_12 = ((sqrt(
					    dot (xlv_TEXCOORD6, xlv_TEXCOORD6)
					  ) * _LightPositionRange.w) * 0.97);
					  shadowVals_11.x = dot (textureCube (_ShadowMapTexture, (xlv_TEXCOORD6 + vec3(0.0078125, 0.0078125, 0.0078125))), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  shadowVals_11.y = dot (textureCube (_ShadowMapTexture, (xlv_TEXCOORD6 + vec3(-0.0078125, -0.0078125, 0.0078125))), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  shadowVals_11.z = dot (textureCube (_ShadowMapTexture, (xlv_TEXCOORD6 + vec3(-0.0078125, 0.0078125, -0.0078125))), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  shadowVals_11.w = dot (textureCube (_ShadowMapTexture, (xlv_TEXCOORD6 + vec3(0.0078125, -0.0078125, -0.0078125))), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  bvec4 tmpvar_13;
					  tmpvar_13 = lessThan (shadowVals_11, vec4(mydist_12));
					  mediump vec4 tmpvar_14;
					  tmpvar_14 = _LightShadowData.xxxx;
					  mediump float tmpvar_15;
					  if (tmpvar_13.x) {
					    tmpvar_15 = tmpvar_14.x;
					  } else {
					    tmpvar_15 = 1.0;
					  };
					  mediump float tmpvar_16;
					  if (tmpvar_13.y) {
					    tmpvar_16 = tmpvar_14.y;
					  } else {
					    tmpvar_16 = 1.0;
					  };
					  mediump float tmpvar_17;
					  if (tmpvar_13.z) {
					    tmpvar_17 = tmpvar_14.z;
					  } else {
					    tmpvar_17 = 1.0;
					  };
					  mediump float tmpvar_18;
					  if (tmpvar_13.w) {
					    tmpvar_18 = tmpvar_14.w;
					  } else {
					    tmpvar_18 = 1.0;
					  };
					  mediump vec4 tmpvar_19;
					  tmpvar_19.x = tmpvar_15;
					  tmpvar_19.y = tmpvar_16;
					  tmpvar_19.z = tmpvar_17;
					  tmpvar_19.w = tmpvar_18;
					  mediump vec3 tmpvar_20;
					  tmpvar_20 = _LightColor0.xyz;
					  mediump vec3 tmpvar_21;
					  tmpvar_21 = normalize(tmpvar_7);
					  tmpvar_20 = (tmpvar_20 * ((tmpvar_9.w * tmpvar_10.w) * dot (tmpvar_19, vec4(0.25, 0.25, 0.25, 0.25))));
					  mediump float specular_22;
					  mediump vec3 tmpvar_23;
					  mediump vec3 inVec_24;
					  inVec_24 = (tmpvar_21 - tmpvar_6);
					  tmpvar_23 = (inVec_24 * inversesqrt(max (0.001, 
					    dot (inVec_24, inVec_24)
					  )));
					  mediump float tmpvar_25;
					  tmpvar_25 = max (0.0, dot (tmpvar_21, tmpvar_23));
					  mediump float tmpvar_26;
					  tmpvar_26 = (1.0 - _Glossiness);
					  mediump float tmpvar_27;
					  tmpvar_27 = max (0.0001, (tmpvar_26 * tmpvar_26));
					  mediump float tmpvar_28;
					  tmpvar_28 = max (((2.0 / 
					    (tmpvar_27 * tmpvar_27)
					  ) - 2.0), 0.0001);
					  specular_22 = sqrt(max (0.0001, (
					    ((tmpvar_28 + 1.0) * pow (max (0.0, dot (tmpvar_5, tmpvar_23)), tmpvar_28))
					   / 
					    (((8.0 * (
					      ((tmpvar_25 * tmpvar_25) * _Glossiness)
					     + 
					      (tmpvar_26 * tmpvar_26)
					    )) * tmpvar_25) + 0.0001)
					  )));
					  mediump float tmpvar_29;
					  tmpvar_29 = clamp (specular_22, 0.0, 100.0);
					  specular_22 = tmpvar_29;
					  mediump vec4 tmpvar_30;
					  tmpvar_30.w = 1.0;
					  tmpvar_30.xyz = (((tmpvar_3 + 
					    (tmpvar_29 * tmpvar_4)
					  ) * tmpvar_20) * max (0.0, dot (tmpvar_5, tmpvar_21)));
					  mediump vec4 xlat_varoutput_31;
					  xlat_varoutput_31.xyz = tmpvar_30.xyz;
					  xlat_varoutput_31.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_31;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" "SHADOWS_SOFT" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp vec4 _LightPositionRange;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					varying highp vec3 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  highp vec3 lightDir_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_13;
					  highp vec3 n_14;
					  n_14 = (tmpvar_8.xyz - _WorldSpaceCameraPos);
					  tmpvar_13 = n_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_7.xyz = tmpvar_19;
					  highp vec3 tmpvar_20;
					  tmpvar_20 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  mediump vec3 tmpvar_21;
					  tmpvar_21 = tmpvar_20;
					  lightDir_4 = tmpvar_21;
					  tmpvar_5.w = lightDir_4.x;
					  tmpvar_6.w = lightDir_4.y;
					  tmpvar_7.w = lightDir_4.z;
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = tmpvar_13;
					  xlv_TEXCOORD2 = tmpvar_5;
					  xlv_TEXCOORD2_1 = tmpvar_6;
					  xlv_TEXCOORD2_2 = tmpvar_7;
					  highp vec4 tmpvar_22;
					  tmpvar_22 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD5 = (unity_WorldToLight * tmpvar_22).xyz;
					  xlv_TEXCOORD6 = (tmpvar_22.xyz - _LightPositionRange.xyz);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _LightPositionRange;
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp samplerCube _ShadowMapTexture;
					uniform lowp samplerCube _LightTexture0;
					uniform sampler2D _LightTextureB0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					varying highp vec3 xlv_TEXCOORD6;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  mediump vec3 tmpvar_4;
					  tmpvar_4 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic));
					  tmpvar_3 = (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)));
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_6;
					  tmpvar_6 = normalize(xlv_TEXCOORD1);
					  mediump vec3 tmpvar_7;
					  tmpvar_7.x = xlv_TEXCOORD2.w;
					  tmpvar_7.y = xlv_TEXCOORD2_1.w;
					  tmpvar_7.z = xlv_TEXCOORD2_2.w;
					  highp float tmpvar_8;
					  tmpvar_8 = dot (xlv_TEXCOORD5, xlv_TEXCOORD5);
					  lowp vec4 tmpvar_9;
					  tmpvar_9 = texture2D (_LightTextureB0, vec2(tmpvar_8));
					  lowp vec4 tmpvar_10;
					  tmpvar_10 = textureCube (_LightTexture0, xlv_TEXCOORD5);
					  highp vec4 shadowVals_11;
					  highp float mydist_12;
					  mydist_12 = ((sqrt(
					    dot (xlv_TEXCOORD6, xlv_TEXCOORD6)
					  ) * _LightPositionRange.w) * 0.97);
					  shadowVals_11.x = dot (textureCube (_ShadowMapTexture, (xlv_TEXCOORD6 + vec3(0.0078125, 0.0078125, 0.0078125))), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  shadowVals_11.y = dot (textureCube (_ShadowMapTexture, (xlv_TEXCOORD6 + vec3(-0.0078125, -0.0078125, 0.0078125))), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  shadowVals_11.z = dot (textureCube (_ShadowMapTexture, (xlv_TEXCOORD6 + vec3(-0.0078125, 0.0078125, -0.0078125))), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  shadowVals_11.w = dot (textureCube (_ShadowMapTexture, (xlv_TEXCOORD6 + vec3(0.0078125, -0.0078125, -0.0078125))), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  bvec4 tmpvar_13;
					  tmpvar_13 = lessThan (shadowVals_11, vec4(mydist_12));
					  mediump vec4 tmpvar_14;
					  tmpvar_14 = _LightShadowData.xxxx;
					  mediump float tmpvar_15;
					  if (tmpvar_13.x) {
					    tmpvar_15 = tmpvar_14.x;
					  } else {
					    tmpvar_15 = 1.0;
					  };
					  mediump float tmpvar_16;
					  if (tmpvar_13.y) {
					    tmpvar_16 = tmpvar_14.y;
					  } else {
					    tmpvar_16 = 1.0;
					  };
					  mediump float tmpvar_17;
					  if (tmpvar_13.z) {
					    tmpvar_17 = tmpvar_14.z;
					  } else {
					    tmpvar_17 = 1.0;
					  };
					  mediump float tmpvar_18;
					  if (tmpvar_13.w) {
					    tmpvar_18 = tmpvar_14.w;
					  } else {
					    tmpvar_18 = 1.0;
					  };
					  mediump vec4 tmpvar_19;
					  tmpvar_19.x = tmpvar_15;
					  tmpvar_19.y = tmpvar_16;
					  tmpvar_19.z = tmpvar_17;
					  tmpvar_19.w = tmpvar_18;
					  mediump vec3 tmpvar_20;
					  tmpvar_20 = _LightColor0.xyz;
					  mediump vec3 tmpvar_21;
					  tmpvar_21 = normalize(tmpvar_7);
					  tmpvar_20 = (tmpvar_20 * ((tmpvar_9.w * tmpvar_10.w) * dot (tmpvar_19, vec4(0.25, 0.25, 0.25, 0.25))));
					  mediump float specular_22;
					  mediump vec3 tmpvar_23;
					  mediump vec3 inVec_24;
					  inVec_24 = (tmpvar_21 - tmpvar_6);
					  tmpvar_23 = (inVec_24 * inversesqrt(max (0.001, 
					    dot (inVec_24, inVec_24)
					  )));
					  mediump float tmpvar_25;
					  tmpvar_25 = max (0.0, dot (tmpvar_21, tmpvar_23));
					  mediump float tmpvar_26;
					  tmpvar_26 = (1.0 - _Glossiness);
					  mediump float tmpvar_27;
					  tmpvar_27 = max (0.0001, (tmpvar_26 * tmpvar_26));
					  mediump float tmpvar_28;
					  tmpvar_28 = max (((2.0 / 
					    (tmpvar_27 * tmpvar_27)
					  ) - 2.0), 0.0001);
					  specular_22 = sqrt(max (0.0001, (
					    ((tmpvar_28 + 1.0) * pow (max (0.0, dot (tmpvar_5, tmpvar_23)), tmpvar_28))
					   / 
					    (((8.0 * (
					      ((tmpvar_25 * tmpvar_25) * _Glossiness)
					     + 
					      (tmpvar_26 * tmpvar_26)
					    )) * tmpvar_25) + 0.0001)
					  )));
					  mediump float tmpvar_29;
					  tmpvar_29 = clamp (specular_22, 0.0, 100.0);
					  specular_22 = tmpvar_29;
					  mediump vec4 tmpvar_30;
					  tmpvar_30.w = 1.0;
					  tmpvar_30.xyz = (((tmpvar_3 + 
					    (tmpvar_29 * tmpvar_4)
					  ) * tmpvar_20) * max (0.0, dot (tmpvar_5, tmpvar_21)));
					  mediump vec4 xlat_varoutput_31;
					  xlat_varoutput_31.xyz = tmpvar_30.xyz;
					  xlat_varoutput_31.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_31;
					}
					
					
					#endif
}
}
Program "fp" {
SubProgram "gles hw_tier01 " {
Keywords { "POINT" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "POINT" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "POINT" }
					
}
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL" }
					
}
SubProgram "gles hw_tier01 " {
Keywords { "SPOT" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "SPOT" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "SPOT" }
					
}
SubProgram "gles hw_tier01 " {
Keywords { "POINT_COOKIE" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "POINT_COOKIE" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "POINT_COOKIE" }
					
}
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL_COOKIE" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL_COOKIE" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL_COOKIE" }
					
}
SubProgram "gles hw_tier01 " {
Keywords { "SPOT" "SHADOWS_DEPTH" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "SPOT" "SHADOWS_DEPTH" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "SPOT" "SHADOWS_DEPTH" }
					
}
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
					
}
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" }
					
}
SubProgram "gles hw_tier01 " {
Keywords { "POINT" "SHADOWS_CUBE" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "POINT" "SHADOWS_CUBE" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "POINT" "SHADOWS_CUBE" }
					
}
SubProgram "gles hw_tier01 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" }
					
}
SubProgram "gles hw_tier01 " {
Keywords { "SPOT" "SHADOWS_DEPTH" "SHADOWS_SOFT" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "SPOT" "SHADOWS_DEPTH" "SHADOWS_SOFT" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "SPOT" "SHADOWS_DEPTH" "SHADOWS_SOFT" }
					
}
SubProgram "gles hw_tier01 " {
Keywords { "POINT" "SHADOWS_CUBE" "SHADOWS_SOFT" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "POINT" "SHADOWS_CUBE" "SHADOWS_SOFT" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "POINT" "SHADOWS_CUBE" "SHADOWS_SOFT" }
					
}
SubProgram "gles hw_tier01 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" "SHADOWS_SOFT" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" "SHADOWS_SOFT" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" "SHADOWS_SOFT" }
					
}
}
 }
 Pass {
  Name "SHADOWCASTER"
  Tags { "LIGHTMODE"="SHADOWCASTER" "SHADOWSUPPORT"="true" "RenderType"="Opaque" "PerformanceChecks"="False" }
  GpuProgramID 144189
Program "vp" {
SubProgram "gles hw_tier01 " {
Keywords { "SHADOWS_DEPTH" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp vec4 unity_LightShadowBias;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp mat4 unity_MatrixVP;
					void main ()
					{
					  highp vec3 vertex_1;
					  vertex_1 = _glesVertex.xyz;
					  highp vec4 clipPos_2;
					  if ((unity_LightShadowBias.z != 0.0)) {
					    highp vec4 tmpvar_3;
					    tmpvar_3.w = 1.0;
					    tmpvar_3.xyz = vertex_1;
					    highp vec3 tmpvar_4;
					    tmpvar_4 = (unity_ObjectToWorld * tmpvar_3).xyz;
					    highp vec4 v_5;
					    v_5.x = unity_WorldToObject[0].x;
					    v_5.y = unity_WorldToObject[1].x;
					    v_5.z = unity_WorldToObject[2].x;
					    v_5.w = unity_WorldToObject[3].x;
					    highp vec4 v_6;
					    v_6.x = unity_WorldToObject[0].y;
					    v_6.y = unity_WorldToObject[1].y;
					    v_6.z = unity_WorldToObject[2].y;
					    v_6.w = unity_WorldToObject[3].y;
					    highp vec4 v_7;
					    v_7.x = unity_WorldToObject[0].z;
					    v_7.y = unity_WorldToObject[1].z;
					    v_7.z = unity_WorldToObject[2].z;
					    v_7.w = unity_WorldToObject[3].z;
					    highp vec3 tmpvar_8;
					    tmpvar_8 = normalize(((
					      (v_5.xyz * _glesNormal.x)
					     + 
					      (v_6.xyz * _glesNormal.y)
					    ) + (v_7.xyz * _glesNormal.z)));
					    highp float tmpvar_9;
					    tmpvar_9 = dot (tmpvar_8, normalize((_WorldSpaceLightPos0.xyz - 
					      (tmpvar_4 * _WorldSpaceLightPos0.w)
					    )));
					    highp vec4 tmpvar_10;
					    tmpvar_10.w = 1.0;
					    tmpvar_10.xyz = (tmpvar_4 - (tmpvar_8 * (unity_LightShadowBias.z * 
					      sqrt((1.0 - (tmpvar_9 * tmpvar_9)))
					    )));
					    clipPos_2 = (unity_MatrixVP * tmpvar_10);
					  } else {
					    highp vec4 tmpvar_11;
					    tmpvar_11.w = 1.0;
					    tmpvar_11.xyz = vertex_1;
					    clipPos_2 = (glstate_matrix_mvp * tmpvar_11);
					  };
					  highp vec4 clipPos_12;
					  clipPos_12.xyw = clipPos_2.xyw;
					  clipPos_12.z = (clipPos_2.z + clamp ((unity_LightShadowBias.x / clipPos_2.w), 0.0, 1.0));
					  clipPos_12.z = mix (clipPos_12.z, max (clipPos_12.z, -(clipPos_2.w)), unity_LightShadowBias.y);
					  gl_Position = clipPos_12;
					}
					
					
					#endif
					#ifdef FRAGMENT
					void main ()
					{
					  gl_FragData[0] = vec4(0.0, 0.0, 0.0, 0.0);
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "SHADOWS_DEPTH" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp vec4 unity_LightShadowBias;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp mat4 unity_MatrixVP;
					void main ()
					{
					  highp vec3 vertex_1;
					  vertex_1 = _glesVertex.xyz;
					  highp vec4 clipPos_2;
					  if ((unity_LightShadowBias.z != 0.0)) {
					    highp vec4 tmpvar_3;
					    tmpvar_3.w = 1.0;
					    tmpvar_3.xyz = vertex_1;
					    highp vec3 tmpvar_4;
					    tmpvar_4 = (unity_ObjectToWorld * tmpvar_3).xyz;
					    highp vec4 v_5;
					    v_5.x = unity_WorldToObject[0].x;
					    v_5.y = unity_WorldToObject[1].x;
					    v_5.z = unity_WorldToObject[2].x;
					    v_5.w = unity_WorldToObject[3].x;
					    highp vec4 v_6;
					    v_6.x = unity_WorldToObject[0].y;
					    v_6.y = unity_WorldToObject[1].y;
					    v_6.z = unity_WorldToObject[2].y;
					    v_6.w = unity_WorldToObject[3].y;
					    highp vec4 v_7;
					    v_7.x = unity_WorldToObject[0].z;
					    v_7.y = unity_WorldToObject[1].z;
					    v_7.z = unity_WorldToObject[2].z;
					    v_7.w = unity_WorldToObject[3].z;
					    highp vec3 tmpvar_8;
					    tmpvar_8 = normalize(((
					      (v_5.xyz * _glesNormal.x)
					     + 
					      (v_6.xyz * _glesNormal.y)
					    ) + (v_7.xyz * _glesNormal.z)));
					    highp float tmpvar_9;
					    tmpvar_9 = dot (tmpvar_8, normalize((_WorldSpaceLightPos0.xyz - 
					      (tmpvar_4 * _WorldSpaceLightPos0.w)
					    )));
					    highp vec4 tmpvar_10;
					    tmpvar_10.w = 1.0;
					    tmpvar_10.xyz = (tmpvar_4 - (tmpvar_8 * (unity_LightShadowBias.z * 
					      sqrt((1.0 - (tmpvar_9 * tmpvar_9)))
					    )));
					    clipPos_2 = (unity_MatrixVP * tmpvar_10);
					  } else {
					    highp vec4 tmpvar_11;
					    tmpvar_11.w = 1.0;
					    tmpvar_11.xyz = vertex_1;
					    clipPos_2 = (glstate_matrix_mvp * tmpvar_11);
					  };
					  highp vec4 clipPos_12;
					  clipPos_12.xyw = clipPos_2.xyw;
					  clipPos_12.z = (clipPos_2.z + clamp ((unity_LightShadowBias.x / clipPos_2.w), 0.0, 1.0));
					  clipPos_12.z = mix (clipPos_12.z, max (clipPos_12.z, -(clipPos_2.w)), unity_LightShadowBias.y);
					  gl_Position = clipPos_12;
					}
					
					
					#endif
					#ifdef FRAGMENT
					void main ()
					{
					  gl_FragData[0] = vec4(0.0, 0.0, 0.0, 0.0);
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "SHADOWS_DEPTH" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp vec4 unity_LightShadowBias;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp mat4 unity_MatrixVP;
					void main ()
					{
					  highp vec3 vertex_1;
					  vertex_1 = _glesVertex.xyz;
					  highp vec4 clipPos_2;
					  if ((unity_LightShadowBias.z != 0.0)) {
					    highp vec4 tmpvar_3;
					    tmpvar_3.w = 1.0;
					    tmpvar_3.xyz = vertex_1;
					    highp vec3 tmpvar_4;
					    tmpvar_4 = (unity_ObjectToWorld * tmpvar_3).xyz;
					    highp vec4 v_5;
					    v_5.x = unity_WorldToObject[0].x;
					    v_5.y = unity_WorldToObject[1].x;
					    v_5.z = unity_WorldToObject[2].x;
					    v_5.w = unity_WorldToObject[3].x;
					    highp vec4 v_6;
					    v_6.x = unity_WorldToObject[0].y;
					    v_6.y = unity_WorldToObject[1].y;
					    v_6.z = unity_WorldToObject[2].y;
					    v_6.w = unity_WorldToObject[3].y;
					    highp vec4 v_7;
					    v_7.x = unity_WorldToObject[0].z;
					    v_7.y = unity_WorldToObject[1].z;
					    v_7.z = unity_WorldToObject[2].z;
					    v_7.w = unity_WorldToObject[3].z;
					    highp vec3 tmpvar_8;
					    tmpvar_8 = normalize(((
					      (v_5.xyz * _glesNormal.x)
					     + 
					      (v_6.xyz * _glesNormal.y)
					    ) + (v_7.xyz * _glesNormal.z)));
					    highp float tmpvar_9;
					    tmpvar_9 = dot (tmpvar_8, normalize((_WorldSpaceLightPos0.xyz - 
					      (tmpvar_4 * _WorldSpaceLightPos0.w)
					    )));
					    highp vec4 tmpvar_10;
					    tmpvar_10.w = 1.0;
					    tmpvar_10.xyz = (tmpvar_4 - (tmpvar_8 * (unity_LightShadowBias.z * 
					      sqrt((1.0 - (tmpvar_9 * tmpvar_9)))
					    )));
					    clipPos_2 = (unity_MatrixVP * tmpvar_10);
					  } else {
					    highp vec4 tmpvar_11;
					    tmpvar_11.w = 1.0;
					    tmpvar_11.xyz = vertex_1;
					    clipPos_2 = (glstate_matrix_mvp * tmpvar_11);
					  };
					  highp vec4 clipPos_12;
					  clipPos_12.xyw = clipPos_2.xyw;
					  clipPos_12.z = (clipPos_2.z + clamp ((unity_LightShadowBias.x / clipPos_2.w), 0.0, 1.0));
					  clipPos_12.z = mix (clipPos_12.z, max (clipPos_12.z, -(clipPos_2.w)), unity_LightShadowBias.y);
					  gl_Position = clipPos_12;
					}
					
					
					#endif
					#ifdef FRAGMENT
					void main ()
					{
					  gl_FragData[0] = vec4(0.0, 0.0, 0.0, 0.0);
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "SHADOWS_CUBE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					uniform highp vec4 _LightPositionRange;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					varying highp vec3 xlv_TEXCOORD0;
					void main ()
					{
					  highp vec4 tmpvar_1;
					  tmpvar_1.w = 1.0;
					  tmpvar_1.xyz = _glesVertex.xyz;
					  xlv_TEXCOORD0 = ((unity_ObjectToWorld * _glesVertex).xyz - _LightPositionRange.xyz);
					  gl_Position = (glstate_matrix_mvp * tmpvar_1);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _LightPositionRange;
					uniform highp vec4 unity_LightShadowBias;
					varying highp vec3 xlv_TEXCOORD0;
					void main ()
					{
					  mediump vec4 tmpvar_1;
					  highp vec4 enc_2;
					  highp vec4 tmpvar_3;
					  tmpvar_3 = fract((vec4(1.0, 255.0, 65025.0, 1.658138e+07) * min (
					    ((sqrt(dot (xlv_TEXCOORD0, xlv_TEXCOORD0)) + unity_LightShadowBias.x) * _LightPositionRange.w)
					  , 0.999)));
					  enc_2 = (tmpvar_3 - (tmpvar_3.yzww * 0.003921569));
					  tmpvar_1 = enc_2;
					  gl_FragData[0] = tmpvar_1;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "SHADOWS_CUBE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					uniform highp vec4 _LightPositionRange;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					varying highp vec3 xlv_TEXCOORD0;
					void main ()
					{
					  highp vec4 tmpvar_1;
					  tmpvar_1.w = 1.0;
					  tmpvar_1.xyz = _glesVertex.xyz;
					  xlv_TEXCOORD0 = ((unity_ObjectToWorld * _glesVertex).xyz - _LightPositionRange.xyz);
					  gl_Position = (glstate_matrix_mvp * tmpvar_1);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _LightPositionRange;
					uniform highp vec4 unity_LightShadowBias;
					varying highp vec3 xlv_TEXCOORD0;
					void main ()
					{
					  mediump vec4 tmpvar_1;
					  highp vec4 enc_2;
					  highp vec4 tmpvar_3;
					  tmpvar_3 = fract((vec4(1.0, 255.0, 65025.0, 1.658138e+07) * min (
					    ((sqrt(dot (xlv_TEXCOORD0, xlv_TEXCOORD0)) + unity_LightShadowBias.x) * _LightPositionRange.w)
					  , 0.999)));
					  enc_2 = (tmpvar_3 - (tmpvar_3.yzww * 0.003921569));
					  tmpvar_1 = enc_2;
					  gl_FragData[0] = tmpvar_1;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "SHADOWS_CUBE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					uniform highp vec4 _LightPositionRange;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					varying highp vec3 xlv_TEXCOORD0;
					void main ()
					{
					  highp vec4 tmpvar_1;
					  tmpvar_1.w = 1.0;
					  tmpvar_1.xyz = _glesVertex.xyz;
					  xlv_TEXCOORD0 = ((unity_ObjectToWorld * _glesVertex).xyz - _LightPositionRange.xyz);
					  gl_Position = (glstate_matrix_mvp * tmpvar_1);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _LightPositionRange;
					uniform highp vec4 unity_LightShadowBias;
					varying highp vec3 xlv_TEXCOORD0;
					void main ()
					{
					  mediump vec4 tmpvar_1;
					  highp vec4 enc_2;
					  highp vec4 tmpvar_3;
					  tmpvar_3 = fract((vec4(1.0, 255.0, 65025.0, 1.658138e+07) * min (
					    ((sqrt(dot (xlv_TEXCOORD0, xlv_TEXCOORD0)) + unity_LightShadowBias.x) * _LightPositionRange.w)
					  , 0.999)));
					  enc_2 = (tmpvar_3 - (tmpvar_3.yzww * 0.003921569));
					  tmpvar_1 = enc_2;
					  gl_FragData[0] = tmpvar_1;
					}
					
					
					#endif
}
}
Program "fp" {
SubProgram "gles hw_tier01 " {
Keywords { "SHADOWS_DEPTH" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "SHADOWS_DEPTH" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "SHADOWS_DEPTH" }
					
}
SubProgram "gles hw_tier01 " {
Keywords { "SHADOWS_CUBE" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "SHADOWS_CUBE" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "SHADOWS_CUBE" }
					
}
}
 }
}
SubShader { 
 LOD 150
 Tags { "RenderType"="Opaque" "PerformanceChecks"="False" }
 Pass {
  Name "FORWARD"
  Tags { "LIGHTMODE"="ForwardBase" "SHADOWSUPPORT"="true" "RenderType"="Opaque" "PerformanceChecks"="False" }
  ZWrite [_ZWrite]
  Blend [_SrcBlend] [_DstBlend]
  GpuProgramID 329169
Program "vp" {
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 unity_SHAr;
					uniform mediump vec4 unity_SHAg;
					uniform mediump vec4 unity_SHAb;
					uniform mediump vec4 unity_SHBr;
					uniform mediump vec4 unity_SHBg;
					uniform mediump vec4 unity_SHBb;
					uniform mediump vec4 unity_SHC;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec4 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD4;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  mediump vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_10;
					  highp vec4 tmpvar_11;
					  tmpvar_11.w = 1.0;
					  tmpvar_11.xyz = _glesVertex.xyz;
					  tmpvar_10 = (glstate_matrix_mvp * tmpvar_11);
					  highp vec4 texcoord_12;
					  texcoord_12.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_13;
					  if ((_UVSec == 0.0)) {
					    tmpvar_13 = tmpvar_2;
					  } else {
					    tmpvar_13 = tmpvar_3;
					  };
					  texcoord_12.zw = ((tmpvar_13 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_9.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  normalWorld_4 = tmpvar_19;
					  tmpvar_8.xyz = normalWorld_4;
					  tmpvar_6.xyz = eyeVec_5;
					  mediump vec4 ambientOrLightmapUV_20;
					  ambientOrLightmapUV_20.w = 0.0;
					  mediump vec4 tmpvar_21;
					  tmpvar_21.w = 1.0;
					  tmpvar_21.xyz = normalWorld_4;
					  mediump vec3 res_22;
					  mediump vec3 x_23;
					  x_23.x = dot (unity_SHAr, tmpvar_21);
					  x_23.y = dot (unity_SHAg, tmpvar_21);
					  x_23.z = dot (unity_SHAb, tmpvar_21);
					  mediump vec3 x1_24;
					  mediump vec4 tmpvar_25;
					  tmpvar_25 = (normalWorld_4.xyzz * normalWorld_4.yzzx);
					  x1_24.x = dot (unity_SHBr, tmpvar_25);
					  x1_24.y = dot (unity_SHBg, tmpvar_25);
					  x1_24.z = dot (unity_SHBb, tmpvar_25);
					  res_22 = (x_23 + (x1_24 + (unity_SHC.xyz * 
					    ((normalWorld_4.x * normalWorld_4.x) - (normalWorld_4.y * normalWorld_4.y))
					  )));
					  res_22 = max (((1.055 * 
					    pow (max (res_22, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  ambientOrLightmapUV_20.xyz = max (vec3(0.0, 0.0, 0.0), res_22);
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  mediump float x_26;
					  x_26 = (1.0 - clamp (dot (normalWorld_4, 
					    -(eyeVec_5)
					  ), 0.0, 1.0));
					  tmpvar_8.w = ((x_26 * x_26) * (x_26 * x_26));
					  tmpvar_6.w = clamp ((_Glossiness + (1.0 - 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  )), 0.0, 1.0);
					  gl_Position = tmpvar_10;
					  xlv_TEXCOORD0 = texcoord_12;
					  xlv_TEXCOORD1 = tmpvar_6;
					  xlv_TEXCOORD2 = ambientOrLightmapUV_20;
					  xlv_TEXCOORD4 = tmpvar_7;
					  xlv_TEXCOORD5 = tmpvar_8;
					}
					
					
					#endif
					#ifdef FRAGMENT
					#extension GL_EXT_shader_texture_lod : enable
					lowp vec4 impl_low_textureCubeLodEXT(lowp samplerCube sampler, highp vec3 coord, mediump float lod)
					{
					#if defined(GL_EXT_shader_texture_lod)
						return textureCubeLodEXT(sampler, coord, lod);
					#else
						return textureCube(sampler, coord, lod);
					#endif
					}
					
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform lowp samplerCube unity_SpecCube0;
					uniform mediump vec4 unity_SpecCube0_HDR;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _OcclusionMap;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec4 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD4;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump float rl_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump vec3 tmpvar_4;
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic));
					  tmpvar_4 = (tmpvar_3 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)));
					  mediump vec3 tmpvar_6;
					  tmpvar_6 = _LightColor0.xyz;
					  mediump float tmpvar_7;
					  lowp vec4 tmpvar_8;
					  tmpvar_8 = texture2D (_OcclusionMap, xlv_TEXCOORD0.xy);
					  tmpvar_7 = tmpvar_8.y;
					  rl_1 = dot (xlv_TEXCOORD4.yzw, _WorldSpaceLightPos0.xyz);
					  highp vec4 tmpvar_9;
					  tmpvar_9 = unity_SpecCube0_HDR;
					  mediump float tmpvar_10;
					  tmpvar_10 = (1.0 - _Glossiness);
					  mediump vec4 hdr_11;
					  hdr_11 = tmpvar_9;
					  mediump vec4 tmpvar_12;
					  tmpvar_12.xyz = xlv_TEXCOORD4.yzw;
					  tmpvar_12.w = ((tmpvar_10 * (1.7 - 
					    (0.7 * tmpvar_10)
					  )) * 6.0);
					  lowp vec4 tmpvar_13;
					  tmpvar_13 = impl_low_textureCubeLodEXT (unity_SpecCube0, xlv_TEXCOORD4.yzw, tmpvar_12.w);
					  mediump vec4 tmpvar_14;
					  tmpvar_14 = tmpvar_13;
					  mediump vec2 tmpvar_15;
					  tmpvar_15.x = ((rl_1 * rl_1) * (rl_1 * rl_1));
					  tmpvar_15.y = tmpvar_10;
					  lowp vec4 tmpvar_16;
					  tmpvar_16 = texture2D (unity_NHxRoughness, tmpvar_15);
					  mediump vec4 tmpvar_17;
					  tmpvar_17.w = 1.0;
					  tmpvar_17.xyz = (((
					    (xlv_TEXCOORD2.xyz * tmpvar_7)
					   * tmpvar_4) + (
					    (((tmpvar_14.w * hdr_11.x) * tmpvar_14.xyz) * tmpvar_7)
					   * 
					    mix (tmpvar_5, xlv_TEXCOORD1.www, xlv_TEXCOORD5.www)
					  )) + ((tmpvar_4 + 
					    ((tmpvar_16.w * 16.0) * tmpvar_5)
					  ) * (tmpvar_6 * 
					    clamp (dot (xlv_TEXCOORD5.xyz, _WorldSpaceLightPos0.xyz), 0.0, 1.0)
					  )));
					  mediump vec4 xlat_varoutput_18;
					  xlat_varoutput_18.xyz = tmpvar_17.xyz;
					  xlat_varoutput_18.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_18;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 unity_SHAr;
					uniform mediump vec4 unity_SHAg;
					uniform mediump vec4 unity_SHAb;
					uniform mediump vec4 unity_SHBr;
					uniform mediump vec4 unity_SHBg;
					uniform mediump vec4 unity_SHBb;
					uniform mediump vec4 unity_SHC;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 tmpvar_13;
					  tmpvar_13 = normalize((tmpvar_7.xyz - _WorldSpaceCameraPos));
					  tmpvar_12 = tmpvar_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  mediump vec3 normalWorld_19;
					  normalWorld_19 = tmpvar_18;
					  mediump vec4 ambientOrLightmapUV_20;
					  ambientOrLightmapUV_20.w = 0.0;
					  mediump vec4 tmpvar_21;
					  tmpvar_21.w = 1.0;
					  tmpvar_21.xyz = normalWorld_19;
					  mediump vec3 res_22;
					  mediump vec3 x_23;
					  x_23.x = dot (unity_SHAr, tmpvar_21);
					  x_23.y = dot (unity_SHAg, tmpvar_21);
					  x_23.z = dot (unity_SHAb, tmpvar_21);
					  mediump vec3 x1_24;
					  mediump vec4 tmpvar_25;
					  tmpvar_25 = (normalWorld_19.xyzz * normalWorld_19.yzzx);
					  x1_24.x = dot (unity_SHBr, tmpvar_25);
					  x1_24.y = dot (unity_SHBg, tmpvar_25);
					  x1_24.z = dot (unity_SHBb, tmpvar_25);
					  res_22 = (x_23 + (x1_24 + (unity_SHC.xyz * 
					    ((normalWorld_19.x * normalWorld_19.x) - (normalWorld_19.y * normalWorld_19.y))
					  )));
					  res_22 = max (((1.055 * 
					    pow (max (res_22, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  ambientOrLightmapUV_20.xyz = max (vec3(0.0, 0.0, 0.0), res_22);
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					  xlv_TEXCOORD5 = ambientOrLightmapUV_20;
					}
					
					
					#endif
					#ifdef FRAGMENT
					#extension GL_EXT_shader_texture_lod : enable
					lowp vec4 impl_low_textureCubeLodEXT(lowp samplerCube sampler, highp vec3 coord, mediump float lod)
					{
					#if defined(GL_EXT_shader_texture_lod)
						return textureCubeLodEXT(sampler, coord, lod);
					#else
						return textureCube(sampler, coord, lod);
					#endif
					}
					
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform lowp samplerCube unity_SpecCube0;
					uniform mediump vec4 unity_SpecCube0_HDR;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _OcclusionMap;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec4 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump vec3 tmpvar_4;
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic));
					  mediump float tmpvar_6;
					  tmpvar_6 = (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w));
					  tmpvar_4 = (tmpvar_3 * tmpvar_6);
					  mediump vec3 tmpvar_7;
					  tmpvar_7 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_8;
					  tmpvar_8 = _LightColor0.xyz;
					  mediump float tmpvar_9;
					  lowp vec4 tmpvar_10;
					  tmpvar_10 = texture2D (_OcclusionMap, xlv_TEXCOORD0.xy);
					  tmpvar_9 = tmpvar_10.y;
					  highp vec4 tmpvar_11;
					  tmpvar_11 = unity_SpecCube0_HDR;
					  mediump vec3 tmpvar_12;
					  mediump float tmpvar_13;
					  tmpvar_13 = (1.0 - _Glossiness);
					  tmpvar_12 = (xlv_TEXCOORD1 - (2.0 * (
					    dot (tmpvar_7, xlv_TEXCOORD1)
					   * tmpvar_7)));
					  mediump vec4 hdr_14;
					  hdr_14 = tmpvar_11;
					  mediump vec4 tmpvar_15;
					  tmpvar_15.xyz = tmpvar_12;
					  tmpvar_15.w = ((tmpvar_13 * (1.7 - 
					    (0.7 * tmpvar_13)
					  )) * 6.0);
					  lowp vec4 tmpvar_16;
					  tmpvar_16 = impl_low_textureCubeLodEXT (unity_SpecCube0, tmpvar_12, tmpvar_15.w);
					  mediump vec4 tmpvar_17;
					  tmpvar_17 = tmpvar_16;
					  mediump vec3 viewDir_18;
					  viewDir_18 = -(xlv_TEXCOORD1);
					  mediump vec3 color_19;
					  mediump vec2 tmpvar_20;
					  tmpvar_20.x = dot ((viewDir_18 - (2.0 * 
					    (dot (tmpvar_7, viewDir_18) * tmpvar_7)
					  )), _WorldSpaceLightPos0.xyz);
					  tmpvar_20.y = (1.0 - clamp (dot (tmpvar_7, viewDir_18), 0.0, 1.0));
					  mediump vec2 tmpvar_21;
					  tmpvar_21 = ((tmpvar_20 * tmpvar_20) * (tmpvar_20 * tmpvar_20));
					  mediump vec2 tmpvar_22;
					  tmpvar_22.x = tmpvar_21.x;
					  tmpvar_22.y = tmpvar_13;
					  lowp vec4 tmpvar_23;
					  tmpvar_23 = texture2D (unity_NHxRoughness, tmpvar_22);
					  color_19 = ((tmpvar_4 + (
					    (tmpvar_23.w * 16.0)
					   * tmpvar_5)) * (tmpvar_8 * clamp (
					    dot (tmpvar_7, _WorldSpaceLightPos0.xyz)
					  , 0.0, 1.0)));
					  color_19 = (color_19 + ((
					    (xlv_TEXCOORD5.xyz * tmpvar_9)
					   * tmpvar_4) + (
					    (((tmpvar_17.w * hdr_14.x) * tmpvar_17.xyz) * tmpvar_9)
					   * 
					    mix (tmpvar_5, vec3(clamp ((_Glossiness + (1.0 - tmpvar_6)), 0.0, 1.0)), tmpvar_21.yyy)
					  )));
					  mediump vec4 tmpvar_24;
					  tmpvar_24.w = 1.0;
					  tmpvar_24.xyz = color_19;
					  c_1.w = tmpvar_24.w;
					  c_1.xyz = color_19;
					  c_1.xyz = c_1.xyz;
					  mediump vec4 xlat_varoutput_25;
					  xlat_varoutput_25.xyz = c_1.xyz;
					  xlat_varoutput_25.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_25;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 unity_SHAr;
					uniform mediump vec4 unity_SHAg;
					uniform mediump vec4 unity_SHAb;
					uniform mediump vec4 unity_SHBr;
					uniform mediump vec4 unity_SHBg;
					uniform mediump vec4 unity_SHBb;
					uniform mediump vec4 unity_SHC;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 tmpvar_13;
					  tmpvar_13 = normalize((tmpvar_7.xyz - _WorldSpaceCameraPos));
					  tmpvar_12 = tmpvar_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  mediump vec3 normalWorld_19;
					  normalWorld_19 = tmpvar_18;
					  mediump vec4 ambientOrLightmapUV_20;
					  ambientOrLightmapUV_20.w = 0.0;
					  mediump vec4 tmpvar_21;
					  tmpvar_21.w = 1.0;
					  tmpvar_21.xyz = normalWorld_19;
					  mediump vec3 res_22;
					  mediump vec3 x_23;
					  x_23.x = dot (unity_SHAr, tmpvar_21);
					  x_23.y = dot (unity_SHAg, tmpvar_21);
					  x_23.z = dot (unity_SHAb, tmpvar_21);
					  mediump vec3 x1_24;
					  mediump vec4 tmpvar_25;
					  tmpvar_25 = (normalWorld_19.xyzz * normalWorld_19.yzzx);
					  x1_24.x = dot (unity_SHBr, tmpvar_25);
					  x1_24.y = dot (unity_SHBg, tmpvar_25);
					  x1_24.z = dot (unity_SHBb, tmpvar_25);
					  res_22 = (x_23 + (x1_24 + (unity_SHC.xyz * 
					    ((normalWorld_19.x * normalWorld_19.x) - (normalWorld_19.y * normalWorld_19.y))
					  )));
					  res_22 = max (((1.055 * 
					    pow (max (res_22, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  ambientOrLightmapUV_20.xyz = max (vec3(0.0, 0.0, 0.0), res_22);
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					  xlv_TEXCOORD5 = ambientOrLightmapUV_20;
					}
					
					
					#endif
					#ifdef FRAGMENT
					#extension GL_EXT_shader_texture_lod : enable
					lowp vec4 impl_low_textureCubeLodEXT(lowp samplerCube sampler, highp vec3 coord, mediump float lod)
					{
					#if defined(GL_EXT_shader_texture_lod)
						return textureCubeLodEXT(sampler, coord, lod);
					#else
						return textureCube(sampler, coord, lod);
					#endif
					}
					
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform lowp samplerCube unity_SpecCube0;
					uniform mediump vec4 unity_SpecCube0_HDR;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _OcclusionMap;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec4 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump vec3 tmpvar_4;
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic));
					  mediump float tmpvar_6;
					  tmpvar_6 = (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w));
					  tmpvar_4 = (tmpvar_3 * tmpvar_6);
					  mediump vec3 tmpvar_7;
					  tmpvar_7 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_8;
					  tmpvar_8 = _LightColor0.xyz;
					  mediump float tmpvar_9;
					  lowp vec4 tmpvar_10;
					  tmpvar_10 = texture2D (_OcclusionMap, xlv_TEXCOORD0.xy);
					  tmpvar_9 = tmpvar_10.y;
					  highp vec4 tmpvar_11;
					  tmpvar_11 = unity_SpecCube0_HDR;
					  mediump vec3 tmpvar_12;
					  mediump float tmpvar_13;
					  tmpvar_13 = (1.0 - _Glossiness);
					  tmpvar_12 = (xlv_TEXCOORD1 - (2.0 * (
					    dot (tmpvar_7, xlv_TEXCOORD1)
					   * tmpvar_7)));
					  mediump vec4 hdr_14;
					  hdr_14 = tmpvar_11;
					  mediump vec4 tmpvar_15;
					  tmpvar_15.xyz = tmpvar_12;
					  tmpvar_15.w = ((tmpvar_13 * (1.7 - 
					    (0.7 * tmpvar_13)
					  )) * 6.0);
					  lowp vec4 tmpvar_16;
					  tmpvar_16 = impl_low_textureCubeLodEXT (unity_SpecCube0, tmpvar_12, tmpvar_15.w);
					  mediump vec4 tmpvar_17;
					  tmpvar_17 = tmpvar_16;
					  mediump vec3 viewDir_18;
					  viewDir_18 = -(xlv_TEXCOORD1);
					  mediump vec3 color_19;
					  mediump vec2 tmpvar_20;
					  tmpvar_20.x = dot ((viewDir_18 - (2.0 * 
					    (dot (tmpvar_7, viewDir_18) * tmpvar_7)
					  )), _WorldSpaceLightPos0.xyz);
					  tmpvar_20.y = (1.0 - clamp (dot (tmpvar_7, viewDir_18), 0.0, 1.0));
					  mediump vec2 tmpvar_21;
					  tmpvar_21 = ((tmpvar_20 * tmpvar_20) * (tmpvar_20 * tmpvar_20));
					  mediump vec2 tmpvar_22;
					  tmpvar_22.x = tmpvar_21.x;
					  tmpvar_22.y = tmpvar_13;
					  lowp vec4 tmpvar_23;
					  tmpvar_23 = texture2D (unity_NHxRoughness, tmpvar_22);
					  color_19 = ((tmpvar_4 + (
					    (tmpvar_23.w * 16.0)
					   * tmpvar_5)) * (tmpvar_8 * clamp (
					    dot (tmpvar_7, _WorldSpaceLightPos0.xyz)
					  , 0.0, 1.0)));
					  color_19 = (color_19 + ((
					    (xlv_TEXCOORD5.xyz * tmpvar_9)
					   * tmpvar_4) + (
					    (((tmpvar_17.w * hdr_14.x) * tmpvar_17.xyz) * tmpvar_9)
					   * 
					    mix (tmpvar_5, vec3(clamp ((_Glossiness + (1.0 - tmpvar_6)), 0.0, 1.0)), tmpvar_21.yyy)
					  )));
					  mediump vec4 tmpvar_24;
					  tmpvar_24.w = 1.0;
					  tmpvar_24.xyz = color_19;
					  c_1.w = tmpvar_24.w;
					  c_1.xyz = color_19;
					  c_1.xyz = c_1.xyz;
					  mediump vec4 xlat_varoutput_25;
					  xlat_varoutput_25.xyz = c_1.xyz;
					  xlat_varoutput_25.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_25;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 unity_SHAr;
					uniform mediump vec4 unity_SHAg;
					uniform mediump vec4 unity_SHAb;
					uniform mediump vec4 unity_SHBr;
					uniform mediump vec4 unity_SHBg;
					uniform mediump vec4 unity_SHBb;
					uniform mediump vec4 unity_SHC;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec4 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec4 xlv_TEXCOORD4;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  mediump vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_10;
					  highp vec4 tmpvar_11;
					  tmpvar_11.w = 1.0;
					  tmpvar_11.xyz = _glesVertex.xyz;
					  tmpvar_10 = (glstate_matrix_mvp * tmpvar_11);
					  highp vec4 texcoord_12;
					  texcoord_12.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_13;
					  if ((_UVSec == 0.0)) {
					    tmpvar_13 = tmpvar_2;
					  } else {
					    tmpvar_13 = tmpvar_3;
					  };
					  texcoord_12.zw = ((tmpvar_13 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_9.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  normalWorld_4 = tmpvar_19;
					  tmpvar_8.xyz = normalWorld_4;
					  tmpvar_6.xyz = eyeVec_5;
					  mediump vec4 ambientOrLightmapUV_20;
					  ambientOrLightmapUV_20.w = 0.0;
					  mediump vec4 tmpvar_21;
					  tmpvar_21.w = 1.0;
					  tmpvar_21.xyz = normalWorld_4;
					  mediump vec3 res_22;
					  mediump vec3 x_23;
					  x_23.x = dot (unity_SHAr, tmpvar_21);
					  x_23.y = dot (unity_SHAg, tmpvar_21);
					  x_23.z = dot (unity_SHAb, tmpvar_21);
					  mediump vec3 x1_24;
					  mediump vec4 tmpvar_25;
					  tmpvar_25 = (normalWorld_4.xyzz * normalWorld_4.yzzx);
					  x1_24.x = dot (unity_SHBr, tmpvar_25);
					  x1_24.y = dot (unity_SHBg, tmpvar_25);
					  x1_24.z = dot (unity_SHBb, tmpvar_25);
					  res_22 = (x_23 + (x1_24 + (unity_SHC.xyz * 
					    ((normalWorld_4.x * normalWorld_4.x) - (normalWorld_4.y * normalWorld_4.y))
					  )));
					  res_22 = max (((1.055 * 
					    pow (max (res_22, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  ambientOrLightmapUV_20.xyz = max (vec3(0.0, 0.0, 0.0), res_22);
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  mediump float x_26;
					  x_26 = (1.0 - clamp (dot (normalWorld_4, 
					    -(eyeVec_5)
					  ), 0.0, 1.0));
					  tmpvar_8.w = ((x_26 * x_26) * (x_26 * x_26));
					  tmpvar_6.w = clamp ((_Glossiness + (1.0 - 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  )), 0.0, 1.0);
					  gl_Position = tmpvar_10;
					  xlv_TEXCOORD0 = texcoord_12;
					  xlv_TEXCOORD1 = tmpvar_6;
					  xlv_TEXCOORD2 = ambientOrLightmapUV_20;
					  xlv_TEXCOORD3 = (unity_WorldToShadow[0] * (unity_ObjectToWorld * _glesVertex));
					  xlv_TEXCOORD4 = tmpvar_7;
					  xlv_TEXCOORD5 = tmpvar_8;
					}
					
					
					#endif
					#ifdef FRAGMENT
					#extension GL_EXT_shader_texture_lod : enable
					lowp vec4 impl_low_textureCubeLodEXT(lowp samplerCube sampler, highp vec3 coord, mediump float lod)
					{
					#if defined(GL_EXT_shader_texture_lod)
						return textureCubeLodEXT(sampler, coord, lod);
					#else
						return textureCube(sampler, coord, lod);
					#endif
					}
					
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform mediump vec4 _LightShadowData;
					uniform lowp samplerCube unity_SpecCube0;
					uniform mediump vec4 unity_SpecCube0_HDR;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _OcclusionMap;
					uniform highp sampler2D _ShadowMapTexture;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec4 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec4 xlv_TEXCOORD4;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump float rl_1;
					  mediump float atten_2;
					  lowp vec4 tmpvar_3;
					  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_4;
					  tmpvar_4 = (_Color.xyz * tmpvar_3.xyz);
					  mediump vec3 tmpvar_5;
					  mediump vec3 tmpvar_6;
					  tmpvar_6 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_4, vec3(_Metallic));
					  tmpvar_5 = (tmpvar_4 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)));
					  mediump vec3 tmpvar_7;
					  tmpvar_7 = _LightColor0.xyz;
					  lowp float tmpvar_8;
					  highp float lightShadowDataX_9;
					  mediump float tmpvar_10;
					  tmpvar_10 = _LightShadowData.x;
					  lightShadowDataX_9 = tmpvar_10;
					  highp float tmpvar_11;
					  tmpvar_11 = max (float((texture2D (_ShadowMapTexture, xlv_TEXCOORD3.xy).x > xlv_TEXCOORD3.z)), lightShadowDataX_9);
					  tmpvar_8 = tmpvar_11;
					  atten_2 = tmpvar_8;
					  mediump float tmpvar_12;
					  lowp vec4 tmpvar_13;
					  tmpvar_13 = texture2D (_OcclusionMap, xlv_TEXCOORD0.xy);
					  tmpvar_12 = tmpvar_13.y;
					  rl_1 = dot (xlv_TEXCOORD4.yzw, _WorldSpaceLightPos0.xyz);
					  highp vec4 tmpvar_14;
					  tmpvar_14 = unity_SpecCube0_HDR;
					  mediump float tmpvar_15;
					  tmpvar_15 = (1.0 - _Glossiness);
					  mediump vec4 hdr_16;
					  hdr_16 = tmpvar_14;
					  mediump vec4 tmpvar_17;
					  tmpvar_17.xyz = xlv_TEXCOORD4.yzw;
					  tmpvar_17.w = ((tmpvar_15 * (1.7 - 
					    (0.7 * tmpvar_15)
					  )) * 6.0);
					  lowp vec4 tmpvar_18;
					  tmpvar_18 = impl_low_textureCubeLodEXT (unity_SpecCube0, xlv_TEXCOORD4.yzw, tmpvar_17.w);
					  mediump vec4 tmpvar_19;
					  tmpvar_19 = tmpvar_18;
					  mediump vec2 tmpvar_20;
					  tmpvar_20.x = ((rl_1 * rl_1) * (rl_1 * rl_1));
					  tmpvar_20.y = tmpvar_15;
					  lowp vec4 tmpvar_21;
					  tmpvar_21 = texture2D (unity_NHxRoughness, tmpvar_20);
					  mediump vec4 tmpvar_22;
					  tmpvar_22.w = 1.0;
					  tmpvar_22.xyz = (((
					    (xlv_TEXCOORD2.xyz * tmpvar_12)
					   * tmpvar_5) + (
					    (((tmpvar_19.w * hdr_16.x) * tmpvar_19.xyz) * tmpvar_12)
					   * 
					    mix (tmpvar_6, xlv_TEXCOORD1.www, xlv_TEXCOORD5.www)
					  )) + ((tmpvar_5 + 
					    ((tmpvar_21.w * 16.0) * tmpvar_6)
					  ) * (
					    (tmpvar_7 * atten_2)
					   * 
					    clamp (dot (xlv_TEXCOORD5.xyz, _WorldSpaceLightPos0.xyz), 0.0, 1.0)
					  )));
					  mediump vec4 xlat_varoutput_23;
					  xlat_varoutput_23.xyz = tmpvar_22.xyz;
					  xlat_varoutput_23.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_23;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 unity_SHAr;
					uniform mediump vec4 unity_SHAg;
					uniform mediump vec4 unity_SHAb;
					uniform mediump vec4 unity_SHBr;
					uniform mediump vec4 unity_SHBg;
					uniform mediump vec4 unity_SHBb;
					uniform mediump vec4 unity_SHC;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 tmpvar_13;
					  tmpvar_13 = normalize((tmpvar_7.xyz - _WorldSpaceCameraPos));
					  tmpvar_12 = tmpvar_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  mediump vec3 normalWorld_19;
					  normalWorld_19 = tmpvar_18;
					  mediump vec4 ambientOrLightmapUV_20;
					  ambientOrLightmapUV_20.w = 0.0;
					  mediump vec4 tmpvar_21;
					  tmpvar_21.w = 1.0;
					  tmpvar_21.xyz = normalWorld_19;
					  mediump vec3 res_22;
					  mediump vec3 x_23;
					  x_23.x = dot (unity_SHAr, tmpvar_21);
					  x_23.y = dot (unity_SHAg, tmpvar_21);
					  x_23.z = dot (unity_SHAb, tmpvar_21);
					  mediump vec3 x1_24;
					  mediump vec4 tmpvar_25;
					  tmpvar_25 = (normalWorld_19.xyzz * normalWorld_19.yzzx);
					  x1_24.x = dot (unity_SHBr, tmpvar_25);
					  x1_24.y = dot (unity_SHBg, tmpvar_25);
					  x1_24.z = dot (unity_SHBb, tmpvar_25);
					  res_22 = (x_23 + (x1_24 + (unity_SHC.xyz * 
					    ((normalWorld_19.x * normalWorld_19.x) - (normalWorld_19.y * normalWorld_19.y))
					  )));
					  res_22 = max (((1.055 * 
					    pow (max (res_22, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  ambientOrLightmapUV_20.xyz = max (vec3(0.0, 0.0, 0.0), res_22);
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					  xlv_TEXCOORD5 = ambientOrLightmapUV_20;
					  xlv_TEXCOORD6 = (unity_WorldToShadow[0] * (unity_ObjectToWorld * _glesVertex));
					}
					
					
					#endif
					#ifdef FRAGMENT
					#extension GL_EXT_shader_texture_lod : enable
					lowp vec4 impl_low_textureCubeLodEXT(lowp samplerCube sampler, highp vec3 coord, mediump float lod)
					{
					#if defined(GL_EXT_shader_texture_lod)
						return textureCubeLodEXT(sampler, coord, lod);
					#else
						return textureCube(sampler, coord, lod);
					#endif
					}
					
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform mediump vec4 _LightShadowData;
					uniform lowp samplerCube unity_SpecCube0;
					uniform mediump vec4 unity_SpecCube0_HDR;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _OcclusionMap;
					uniform highp sampler2D _ShadowMapTexture;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec4 c_1;
					  mediump float atten_2;
					  lowp vec4 tmpvar_3;
					  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_4;
					  tmpvar_4 = (_Color.xyz * tmpvar_3.xyz);
					  mediump vec3 tmpvar_5;
					  mediump vec3 tmpvar_6;
					  tmpvar_6 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_4, vec3(_Metallic));
					  mediump float tmpvar_7;
					  tmpvar_7 = (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w));
					  tmpvar_5 = (tmpvar_4 * tmpvar_7);
					  mediump vec3 tmpvar_8;
					  tmpvar_8 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_9;
					  tmpvar_9 = _LightColor0.xyz;
					  lowp float tmpvar_10;
					  highp float lightShadowDataX_11;
					  mediump float tmpvar_12;
					  tmpvar_12 = _LightShadowData.x;
					  lightShadowDataX_11 = tmpvar_12;
					  highp float tmpvar_13;
					  tmpvar_13 = max (float((texture2D (_ShadowMapTexture, xlv_TEXCOORD6.xy).x > xlv_TEXCOORD6.z)), lightShadowDataX_11);
					  tmpvar_10 = tmpvar_13;
					  atten_2 = tmpvar_10;
					  mediump float tmpvar_14;
					  lowp vec4 tmpvar_15;
					  tmpvar_15 = texture2D (_OcclusionMap, xlv_TEXCOORD0.xy);
					  tmpvar_14 = tmpvar_15.y;
					  highp vec4 tmpvar_16;
					  tmpvar_16 = unity_SpecCube0_HDR;
					  mediump vec3 tmpvar_17;
					  mediump float tmpvar_18;
					  tmpvar_18 = (1.0 - _Glossiness);
					  tmpvar_17 = (xlv_TEXCOORD1 - (2.0 * (
					    dot (tmpvar_8, xlv_TEXCOORD1)
					   * tmpvar_8)));
					  mediump vec4 hdr_19;
					  hdr_19 = tmpvar_16;
					  mediump vec4 tmpvar_20;
					  tmpvar_20.xyz = tmpvar_17;
					  tmpvar_20.w = ((tmpvar_18 * (1.7 - 
					    (0.7 * tmpvar_18)
					  )) * 6.0);
					  lowp vec4 tmpvar_21;
					  tmpvar_21 = impl_low_textureCubeLodEXT (unity_SpecCube0, tmpvar_17, tmpvar_20.w);
					  mediump vec4 tmpvar_22;
					  tmpvar_22 = tmpvar_21;
					  mediump vec3 viewDir_23;
					  viewDir_23 = -(xlv_TEXCOORD1);
					  mediump vec3 color_24;
					  mediump vec2 tmpvar_25;
					  tmpvar_25.x = dot ((viewDir_23 - (2.0 * 
					    (dot (tmpvar_8, viewDir_23) * tmpvar_8)
					  )), _WorldSpaceLightPos0.xyz);
					  tmpvar_25.y = (1.0 - clamp (dot (tmpvar_8, viewDir_23), 0.0, 1.0));
					  mediump vec2 tmpvar_26;
					  tmpvar_26 = ((tmpvar_25 * tmpvar_25) * (tmpvar_25 * tmpvar_25));
					  mediump vec2 tmpvar_27;
					  tmpvar_27.x = tmpvar_26.x;
					  tmpvar_27.y = tmpvar_18;
					  lowp vec4 tmpvar_28;
					  tmpvar_28 = texture2D (unity_NHxRoughness, tmpvar_27);
					  color_24 = ((tmpvar_5 + (
					    (tmpvar_28.w * 16.0)
					   * tmpvar_6)) * ((tmpvar_9 * atten_2) * clamp (
					    dot (tmpvar_8, _WorldSpaceLightPos0.xyz)
					  , 0.0, 1.0)));
					  color_24 = (color_24 + ((
					    (xlv_TEXCOORD5.xyz * tmpvar_14)
					   * tmpvar_5) + (
					    (((tmpvar_22.w * hdr_19.x) * tmpvar_22.xyz) * tmpvar_14)
					   * 
					    mix (tmpvar_6, vec3(clamp ((_Glossiness + (1.0 - tmpvar_7)), 0.0, 1.0)), tmpvar_26.yyy)
					  )));
					  mediump vec4 tmpvar_29;
					  tmpvar_29.w = 1.0;
					  tmpvar_29.xyz = color_24;
					  c_1.w = tmpvar_29.w;
					  c_1.xyz = color_24;
					  c_1.xyz = c_1.xyz;
					  mediump vec4 xlat_varoutput_30;
					  xlat_varoutput_30.xyz = c_1.xyz;
					  xlat_varoutput_30.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_30;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 unity_SHAr;
					uniform mediump vec4 unity_SHAg;
					uniform mediump vec4 unity_SHAb;
					uniform mediump vec4 unity_SHBr;
					uniform mediump vec4 unity_SHBg;
					uniform mediump vec4 unity_SHBb;
					uniform mediump vec4 unity_SHC;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 tmpvar_13;
					  tmpvar_13 = normalize((tmpvar_7.xyz - _WorldSpaceCameraPos));
					  tmpvar_12 = tmpvar_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  mediump vec3 normalWorld_19;
					  normalWorld_19 = tmpvar_18;
					  mediump vec4 ambientOrLightmapUV_20;
					  ambientOrLightmapUV_20.w = 0.0;
					  mediump vec4 tmpvar_21;
					  tmpvar_21.w = 1.0;
					  tmpvar_21.xyz = normalWorld_19;
					  mediump vec3 res_22;
					  mediump vec3 x_23;
					  x_23.x = dot (unity_SHAr, tmpvar_21);
					  x_23.y = dot (unity_SHAg, tmpvar_21);
					  x_23.z = dot (unity_SHAb, tmpvar_21);
					  mediump vec3 x1_24;
					  mediump vec4 tmpvar_25;
					  tmpvar_25 = (normalWorld_19.xyzz * normalWorld_19.yzzx);
					  x1_24.x = dot (unity_SHBr, tmpvar_25);
					  x1_24.y = dot (unity_SHBg, tmpvar_25);
					  x1_24.z = dot (unity_SHBb, tmpvar_25);
					  res_22 = (x_23 + (x1_24 + (unity_SHC.xyz * 
					    ((normalWorld_19.x * normalWorld_19.x) - (normalWorld_19.y * normalWorld_19.y))
					  )));
					  res_22 = max (((1.055 * 
					    pow (max (res_22, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  ambientOrLightmapUV_20.xyz = max (vec3(0.0, 0.0, 0.0), res_22);
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					  xlv_TEXCOORD5 = ambientOrLightmapUV_20;
					  xlv_TEXCOORD6 = (unity_WorldToShadow[0] * (unity_ObjectToWorld * _glesVertex));
					}
					
					
					#endif
					#ifdef FRAGMENT
					#extension GL_EXT_shader_texture_lod : enable
					lowp vec4 impl_low_textureCubeLodEXT(lowp samplerCube sampler, highp vec3 coord, mediump float lod)
					{
					#if defined(GL_EXT_shader_texture_lod)
						return textureCubeLodEXT(sampler, coord, lod);
					#else
						return textureCube(sampler, coord, lod);
					#endif
					}
					
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform mediump vec4 _LightShadowData;
					uniform lowp samplerCube unity_SpecCube0;
					uniform mediump vec4 unity_SpecCube0_HDR;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _OcclusionMap;
					uniform highp sampler2D _ShadowMapTexture;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec4 c_1;
					  mediump float atten_2;
					  lowp vec4 tmpvar_3;
					  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_4;
					  tmpvar_4 = (_Color.xyz * tmpvar_3.xyz);
					  mediump vec3 tmpvar_5;
					  mediump vec3 tmpvar_6;
					  tmpvar_6 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_4, vec3(_Metallic));
					  mediump float tmpvar_7;
					  tmpvar_7 = (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w));
					  tmpvar_5 = (tmpvar_4 * tmpvar_7);
					  mediump vec3 tmpvar_8;
					  tmpvar_8 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_9;
					  tmpvar_9 = _LightColor0.xyz;
					  lowp float tmpvar_10;
					  highp float lightShadowDataX_11;
					  mediump float tmpvar_12;
					  tmpvar_12 = _LightShadowData.x;
					  lightShadowDataX_11 = tmpvar_12;
					  highp float tmpvar_13;
					  tmpvar_13 = max (float((texture2D (_ShadowMapTexture, xlv_TEXCOORD6.xy).x > xlv_TEXCOORD6.z)), lightShadowDataX_11);
					  tmpvar_10 = tmpvar_13;
					  atten_2 = tmpvar_10;
					  mediump float tmpvar_14;
					  lowp vec4 tmpvar_15;
					  tmpvar_15 = texture2D (_OcclusionMap, xlv_TEXCOORD0.xy);
					  tmpvar_14 = tmpvar_15.y;
					  highp vec4 tmpvar_16;
					  tmpvar_16 = unity_SpecCube0_HDR;
					  mediump vec3 tmpvar_17;
					  mediump float tmpvar_18;
					  tmpvar_18 = (1.0 - _Glossiness);
					  tmpvar_17 = (xlv_TEXCOORD1 - (2.0 * (
					    dot (tmpvar_8, xlv_TEXCOORD1)
					   * tmpvar_8)));
					  mediump vec4 hdr_19;
					  hdr_19 = tmpvar_16;
					  mediump vec4 tmpvar_20;
					  tmpvar_20.xyz = tmpvar_17;
					  tmpvar_20.w = ((tmpvar_18 * (1.7 - 
					    (0.7 * tmpvar_18)
					  )) * 6.0);
					  lowp vec4 tmpvar_21;
					  tmpvar_21 = impl_low_textureCubeLodEXT (unity_SpecCube0, tmpvar_17, tmpvar_20.w);
					  mediump vec4 tmpvar_22;
					  tmpvar_22 = tmpvar_21;
					  mediump vec3 viewDir_23;
					  viewDir_23 = -(xlv_TEXCOORD1);
					  mediump vec3 color_24;
					  mediump vec2 tmpvar_25;
					  tmpvar_25.x = dot ((viewDir_23 - (2.0 * 
					    (dot (tmpvar_8, viewDir_23) * tmpvar_8)
					  )), _WorldSpaceLightPos0.xyz);
					  tmpvar_25.y = (1.0 - clamp (dot (tmpvar_8, viewDir_23), 0.0, 1.0));
					  mediump vec2 tmpvar_26;
					  tmpvar_26 = ((tmpvar_25 * tmpvar_25) * (tmpvar_25 * tmpvar_25));
					  mediump vec2 tmpvar_27;
					  tmpvar_27.x = tmpvar_26.x;
					  tmpvar_27.y = tmpvar_18;
					  lowp vec4 tmpvar_28;
					  tmpvar_28 = texture2D (unity_NHxRoughness, tmpvar_27);
					  color_24 = ((tmpvar_5 + (
					    (tmpvar_28.w * 16.0)
					   * tmpvar_6)) * ((tmpvar_9 * atten_2) * clamp (
					    dot (tmpvar_8, _WorldSpaceLightPos0.xyz)
					  , 0.0, 1.0)));
					  color_24 = (color_24 + ((
					    (xlv_TEXCOORD5.xyz * tmpvar_14)
					   * tmpvar_5) + (
					    (((tmpvar_22.w * hdr_19.x) * tmpvar_22.xyz) * tmpvar_14)
					   * 
					    mix (tmpvar_6, vec3(clamp ((_Glossiness + (1.0 - tmpvar_7)), 0.0, 1.0)), tmpvar_26.yyy)
					  )));
					  mediump vec4 tmpvar_29;
					  tmpvar_29.w = 1.0;
					  tmpvar_29.xyz = color_24;
					  c_1.w = tmpvar_29.w;
					  c_1.xyz = color_24;
					  c_1.xyz = c_1.xyz;
					  mediump vec4 xlat_varoutput_30;
					  xlat_varoutput_30.xyz = c_1.xyz;
					  xlat_varoutput_30.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_30;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 unity_4LightPosX0;
					uniform highp vec4 unity_4LightPosY0;
					uniform highp vec4 unity_4LightPosZ0;
					uniform mediump vec4 unity_4LightAtten0;
					uniform mediump vec4 unity_LightColor[8];
					uniform mediump vec4 unity_SHAr;
					uniform mediump vec4 unity_SHAg;
					uniform mediump vec4 unity_SHAb;
					uniform mediump vec4 unity_SHBr;
					uniform mediump vec4 unity_SHBg;
					uniform mediump vec4 unity_SHBb;
					uniform mediump vec4 unity_SHC;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec4 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD4;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  mediump vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_10;
					  highp vec4 tmpvar_11;
					  tmpvar_11.w = 1.0;
					  tmpvar_11.xyz = _glesVertex.xyz;
					  tmpvar_10 = (glstate_matrix_mvp * tmpvar_11);
					  highp vec4 texcoord_12;
					  texcoord_12.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_13;
					  if ((_UVSec == 0.0)) {
					    tmpvar_13 = tmpvar_2;
					  } else {
					    tmpvar_13 = tmpvar_3;
					  };
					  texcoord_12.zw = ((tmpvar_13 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_9.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  normalWorld_4 = tmpvar_19;
					  tmpvar_8.xyz = normalWorld_4;
					  tmpvar_6.xyz = eyeVec_5;
					  mediump vec4 ambientOrLightmapUV_20;
					  ambientOrLightmapUV_20.w = 0.0;
					  highp vec3 lightColor0_21;
					  lightColor0_21 = unity_LightColor[0].xyz;
					  highp vec3 lightColor1_22;
					  lightColor1_22 = unity_LightColor[1].xyz;
					  highp vec3 lightColor2_23;
					  lightColor2_23 = unity_LightColor[2].xyz;
					  highp vec3 lightColor3_24;
					  lightColor3_24 = unity_LightColor[3].xyz;
					  highp vec4 lightAttenSq_25;
					  lightAttenSq_25 = unity_4LightAtten0;
					  highp vec3 normal_26;
					  normal_26 = normalWorld_4;
					  highp vec3 col_27;
					  highp vec4 ndotl_28;
					  highp vec4 lengthSq_29;
					  highp vec4 tmpvar_30;
					  tmpvar_30 = (unity_4LightPosX0 - tmpvar_9.x);
					  highp vec4 tmpvar_31;
					  tmpvar_31 = (unity_4LightPosY0 - tmpvar_9.y);
					  highp vec4 tmpvar_32;
					  tmpvar_32 = (unity_4LightPosZ0 - tmpvar_9.z);
					  lengthSq_29 = (tmpvar_30 * tmpvar_30);
					  lengthSq_29 = (lengthSq_29 + (tmpvar_31 * tmpvar_31));
					  lengthSq_29 = (lengthSq_29 + (tmpvar_32 * tmpvar_32));
					  ndotl_28 = (tmpvar_30 * normal_26.x);
					  ndotl_28 = (ndotl_28 + (tmpvar_31 * normal_26.y));
					  ndotl_28 = (ndotl_28 + (tmpvar_32 * normal_26.z));
					  highp vec4 tmpvar_33;
					  tmpvar_33 = max (vec4(0.0, 0.0, 0.0, 0.0), (ndotl_28 * inversesqrt(lengthSq_29)));
					  ndotl_28 = tmpvar_33;
					  highp vec4 tmpvar_34;
					  tmpvar_34 = (tmpvar_33 * (1.0/((1.0 + 
					    (lengthSq_29 * lightAttenSq_25)
					  ))));
					  col_27 = (lightColor0_21 * tmpvar_34.x);
					  col_27 = (col_27 + (lightColor1_22 * tmpvar_34.y));
					  col_27 = (col_27 + (lightColor2_23 * tmpvar_34.z));
					  col_27 = (col_27 + (lightColor3_24 * tmpvar_34.w));
					  ambientOrLightmapUV_20.xyz = col_27;
					  mediump vec4 tmpvar_35;
					  tmpvar_35.w = 1.0;
					  tmpvar_35.xyz = normalWorld_4;
					  mediump vec3 res_36;
					  mediump vec3 x_37;
					  x_37.x = dot (unity_SHAr, tmpvar_35);
					  x_37.y = dot (unity_SHAg, tmpvar_35);
					  x_37.z = dot (unity_SHAb, tmpvar_35);
					  mediump vec3 x1_38;
					  mediump vec4 tmpvar_39;
					  tmpvar_39 = (normalWorld_4.xyzz * normalWorld_4.yzzx);
					  x1_38.x = dot (unity_SHBr, tmpvar_39);
					  x1_38.y = dot (unity_SHBg, tmpvar_39);
					  x1_38.z = dot (unity_SHBb, tmpvar_39);
					  res_36 = (x_37 + (x1_38 + (unity_SHC.xyz * 
					    ((normalWorld_4.x * normalWorld_4.x) - (normalWorld_4.y * normalWorld_4.y))
					  )));
					  res_36 = max (((1.055 * 
					    pow (max (res_36, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  ambientOrLightmapUV_20.xyz = (ambientOrLightmapUV_20.xyz + max (vec3(0.0, 0.0, 0.0), res_36));
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  mediump float x_40;
					  x_40 = (1.0 - clamp (dot (normalWorld_4, 
					    -(eyeVec_5)
					  ), 0.0, 1.0));
					  tmpvar_8.w = ((x_40 * x_40) * (x_40 * x_40));
					  tmpvar_6.w = clamp ((_Glossiness + (1.0 - 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  )), 0.0, 1.0);
					  gl_Position = tmpvar_10;
					  xlv_TEXCOORD0 = texcoord_12;
					  xlv_TEXCOORD1 = tmpvar_6;
					  xlv_TEXCOORD2 = ambientOrLightmapUV_20;
					  xlv_TEXCOORD4 = tmpvar_7;
					  xlv_TEXCOORD5 = tmpvar_8;
					}
					
					
					#endif
					#ifdef FRAGMENT
					#extension GL_EXT_shader_texture_lod : enable
					lowp vec4 impl_low_textureCubeLodEXT(lowp samplerCube sampler, highp vec3 coord, mediump float lod)
					{
					#if defined(GL_EXT_shader_texture_lod)
						return textureCubeLodEXT(sampler, coord, lod);
					#else
						return textureCube(sampler, coord, lod);
					#endif
					}
					
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform lowp samplerCube unity_SpecCube0;
					uniform mediump vec4 unity_SpecCube0_HDR;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _OcclusionMap;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec4 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD4;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump float rl_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump vec3 tmpvar_4;
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic));
					  tmpvar_4 = (tmpvar_3 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)));
					  mediump vec3 tmpvar_6;
					  tmpvar_6 = _LightColor0.xyz;
					  mediump float tmpvar_7;
					  lowp vec4 tmpvar_8;
					  tmpvar_8 = texture2D (_OcclusionMap, xlv_TEXCOORD0.xy);
					  tmpvar_7 = tmpvar_8.y;
					  rl_1 = dot (xlv_TEXCOORD4.yzw, _WorldSpaceLightPos0.xyz);
					  highp vec4 tmpvar_9;
					  tmpvar_9 = unity_SpecCube0_HDR;
					  mediump float tmpvar_10;
					  tmpvar_10 = (1.0 - _Glossiness);
					  mediump vec4 hdr_11;
					  hdr_11 = tmpvar_9;
					  mediump vec4 tmpvar_12;
					  tmpvar_12.xyz = xlv_TEXCOORD4.yzw;
					  tmpvar_12.w = ((tmpvar_10 * (1.7 - 
					    (0.7 * tmpvar_10)
					  )) * 6.0);
					  lowp vec4 tmpvar_13;
					  tmpvar_13 = impl_low_textureCubeLodEXT (unity_SpecCube0, xlv_TEXCOORD4.yzw, tmpvar_12.w);
					  mediump vec4 tmpvar_14;
					  tmpvar_14 = tmpvar_13;
					  mediump vec2 tmpvar_15;
					  tmpvar_15.x = ((rl_1 * rl_1) * (rl_1 * rl_1));
					  tmpvar_15.y = tmpvar_10;
					  lowp vec4 tmpvar_16;
					  tmpvar_16 = texture2D (unity_NHxRoughness, tmpvar_15);
					  mediump vec4 tmpvar_17;
					  tmpvar_17.w = 1.0;
					  tmpvar_17.xyz = (((
					    (xlv_TEXCOORD2.xyz * tmpvar_7)
					   * tmpvar_4) + (
					    (((tmpvar_14.w * hdr_11.x) * tmpvar_14.xyz) * tmpvar_7)
					   * 
					    mix (tmpvar_5, xlv_TEXCOORD1.www, xlv_TEXCOORD5.www)
					  )) + ((tmpvar_4 + 
					    ((tmpvar_16.w * 16.0) * tmpvar_5)
					  ) * (tmpvar_6 * 
					    clamp (dot (xlv_TEXCOORD5.xyz, _WorldSpaceLightPos0.xyz), 0.0, 1.0)
					  )));
					  mediump vec4 xlat_varoutput_18;
					  xlat_varoutput_18.xyz = tmpvar_17.xyz;
					  xlat_varoutput_18.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_18;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 unity_4LightPosX0;
					uniform highp vec4 unity_4LightPosY0;
					uniform highp vec4 unity_4LightPosZ0;
					uniform mediump vec4 unity_4LightAtten0;
					uniform mediump vec4 unity_LightColor[8];
					uniform mediump vec4 unity_SHAr;
					uniform mediump vec4 unity_SHAg;
					uniform mediump vec4 unity_SHAb;
					uniform mediump vec4 unity_SHBr;
					uniform mediump vec4 unity_SHBg;
					uniform mediump vec4 unity_SHBb;
					uniform mediump vec4 unity_SHC;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 tmpvar_13;
					  tmpvar_13 = normalize((tmpvar_7.xyz - _WorldSpaceCameraPos));
					  tmpvar_12 = tmpvar_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  mediump vec3 normalWorld_19;
					  normalWorld_19 = tmpvar_18;
					  mediump vec4 ambientOrLightmapUV_20;
					  ambientOrLightmapUV_20.w = 0.0;
					  highp vec3 lightColor0_21;
					  lightColor0_21 = unity_LightColor[0].xyz;
					  highp vec3 lightColor1_22;
					  lightColor1_22 = unity_LightColor[1].xyz;
					  highp vec3 lightColor2_23;
					  lightColor2_23 = unity_LightColor[2].xyz;
					  highp vec3 lightColor3_24;
					  lightColor3_24 = unity_LightColor[3].xyz;
					  highp vec4 lightAttenSq_25;
					  lightAttenSq_25 = unity_4LightAtten0;
					  highp vec3 normal_26;
					  normal_26 = normalWorld_19;
					  highp vec3 col_27;
					  highp vec4 ndotl_28;
					  highp vec4 lengthSq_29;
					  highp vec4 tmpvar_30;
					  tmpvar_30 = (unity_4LightPosX0 - tmpvar_7.x);
					  highp vec4 tmpvar_31;
					  tmpvar_31 = (unity_4LightPosY0 - tmpvar_7.y);
					  highp vec4 tmpvar_32;
					  tmpvar_32 = (unity_4LightPosZ0 - tmpvar_7.z);
					  lengthSq_29 = (tmpvar_30 * tmpvar_30);
					  lengthSq_29 = (lengthSq_29 + (tmpvar_31 * tmpvar_31));
					  lengthSq_29 = (lengthSq_29 + (tmpvar_32 * tmpvar_32));
					  ndotl_28 = (tmpvar_30 * normal_26.x);
					  ndotl_28 = (ndotl_28 + (tmpvar_31 * normal_26.y));
					  ndotl_28 = (ndotl_28 + (tmpvar_32 * normal_26.z));
					  highp vec4 tmpvar_33;
					  tmpvar_33 = max (vec4(0.0, 0.0, 0.0, 0.0), (ndotl_28 * inversesqrt(lengthSq_29)));
					  ndotl_28 = tmpvar_33;
					  highp vec4 tmpvar_34;
					  tmpvar_34 = (tmpvar_33 * (1.0/((1.0 + 
					    (lengthSq_29 * lightAttenSq_25)
					  ))));
					  col_27 = (lightColor0_21 * tmpvar_34.x);
					  col_27 = (col_27 + (lightColor1_22 * tmpvar_34.y));
					  col_27 = (col_27 + (lightColor2_23 * tmpvar_34.z));
					  col_27 = (col_27 + (lightColor3_24 * tmpvar_34.w));
					  ambientOrLightmapUV_20.xyz = col_27;
					  mediump vec4 tmpvar_35;
					  tmpvar_35.w = 1.0;
					  tmpvar_35.xyz = normalWorld_19;
					  mediump vec3 res_36;
					  mediump vec3 x_37;
					  x_37.x = dot (unity_SHAr, tmpvar_35);
					  x_37.y = dot (unity_SHAg, tmpvar_35);
					  x_37.z = dot (unity_SHAb, tmpvar_35);
					  mediump vec3 x1_38;
					  mediump vec4 tmpvar_39;
					  tmpvar_39 = (normalWorld_19.xyzz * normalWorld_19.yzzx);
					  x1_38.x = dot (unity_SHBr, tmpvar_39);
					  x1_38.y = dot (unity_SHBg, tmpvar_39);
					  x1_38.z = dot (unity_SHBb, tmpvar_39);
					  res_36 = (x_37 + (x1_38 + (unity_SHC.xyz * 
					    ((normalWorld_19.x * normalWorld_19.x) - (normalWorld_19.y * normalWorld_19.y))
					  )));
					  res_36 = max (((1.055 * 
					    pow (max (res_36, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  ambientOrLightmapUV_20.xyz = (ambientOrLightmapUV_20.xyz + max (vec3(0.0, 0.0, 0.0), res_36));
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					  xlv_TEXCOORD5 = ambientOrLightmapUV_20;
					}
					
					
					#endif
					#ifdef FRAGMENT
					#extension GL_EXT_shader_texture_lod : enable
					lowp vec4 impl_low_textureCubeLodEXT(lowp samplerCube sampler, highp vec3 coord, mediump float lod)
					{
					#if defined(GL_EXT_shader_texture_lod)
						return textureCubeLodEXT(sampler, coord, lod);
					#else
						return textureCube(sampler, coord, lod);
					#endif
					}
					
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform lowp samplerCube unity_SpecCube0;
					uniform mediump vec4 unity_SpecCube0_HDR;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _OcclusionMap;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec4 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump vec3 tmpvar_4;
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic));
					  mediump float tmpvar_6;
					  tmpvar_6 = (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w));
					  tmpvar_4 = (tmpvar_3 * tmpvar_6);
					  mediump vec3 tmpvar_7;
					  tmpvar_7 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_8;
					  tmpvar_8 = _LightColor0.xyz;
					  mediump float tmpvar_9;
					  lowp vec4 tmpvar_10;
					  tmpvar_10 = texture2D (_OcclusionMap, xlv_TEXCOORD0.xy);
					  tmpvar_9 = tmpvar_10.y;
					  highp vec4 tmpvar_11;
					  tmpvar_11 = unity_SpecCube0_HDR;
					  mediump vec3 tmpvar_12;
					  mediump float tmpvar_13;
					  tmpvar_13 = (1.0 - _Glossiness);
					  tmpvar_12 = (xlv_TEXCOORD1 - (2.0 * (
					    dot (tmpvar_7, xlv_TEXCOORD1)
					   * tmpvar_7)));
					  mediump vec4 hdr_14;
					  hdr_14 = tmpvar_11;
					  mediump vec4 tmpvar_15;
					  tmpvar_15.xyz = tmpvar_12;
					  tmpvar_15.w = ((tmpvar_13 * (1.7 - 
					    (0.7 * tmpvar_13)
					  )) * 6.0);
					  lowp vec4 tmpvar_16;
					  tmpvar_16 = impl_low_textureCubeLodEXT (unity_SpecCube0, tmpvar_12, tmpvar_15.w);
					  mediump vec4 tmpvar_17;
					  tmpvar_17 = tmpvar_16;
					  mediump vec3 viewDir_18;
					  viewDir_18 = -(xlv_TEXCOORD1);
					  mediump vec3 color_19;
					  mediump vec2 tmpvar_20;
					  tmpvar_20.x = dot ((viewDir_18 - (2.0 * 
					    (dot (tmpvar_7, viewDir_18) * tmpvar_7)
					  )), _WorldSpaceLightPos0.xyz);
					  tmpvar_20.y = (1.0 - clamp (dot (tmpvar_7, viewDir_18), 0.0, 1.0));
					  mediump vec2 tmpvar_21;
					  tmpvar_21 = ((tmpvar_20 * tmpvar_20) * (tmpvar_20 * tmpvar_20));
					  mediump vec2 tmpvar_22;
					  tmpvar_22.x = tmpvar_21.x;
					  tmpvar_22.y = tmpvar_13;
					  lowp vec4 tmpvar_23;
					  tmpvar_23 = texture2D (unity_NHxRoughness, tmpvar_22);
					  color_19 = ((tmpvar_4 + (
					    (tmpvar_23.w * 16.0)
					   * tmpvar_5)) * (tmpvar_8 * clamp (
					    dot (tmpvar_7, _WorldSpaceLightPos0.xyz)
					  , 0.0, 1.0)));
					  color_19 = (color_19 + ((
					    (xlv_TEXCOORD5.xyz * tmpvar_9)
					   * tmpvar_4) + (
					    (((tmpvar_17.w * hdr_14.x) * tmpvar_17.xyz) * tmpvar_9)
					   * 
					    mix (tmpvar_5, vec3(clamp ((_Glossiness + (1.0 - tmpvar_6)), 0.0, 1.0)), tmpvar_21.yyy)
					  )));
					  mediump vec4 tmpvar_24;
					  tmpvar_24.w = 1.0;
					  tmpvar_24.xyz = color_19;
					  c_1.w = tmpvar_24.w;
					  c_1.xyz = color_19;
					  c_1.xyz = c_1.xyz;
					  mediump vec4 xlat_varoutput_25;
					  xlat_varoutput_25.xyz = c_1.xyz;
					  xlat_varoutput_25.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_25;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 unity_4LightPosX0;
					uniform highp vec4 unity_4LightPosY0;
					uniform highp vec4 unity_4LightPosZ0;
					uniform mediump vec4 unity_4LightAtten0;
					uniform mediump vec4 unity_LightColor[8];
					uniform mediump vec4 unity_SHAr;
					uniform mediump vec4 unity_SHAg;
					uniform mediump vec4 unity_SHAb;
					uniform mediump vec4 unity_SHBr;
					uniform mediump vec4 unity_SHBg;
					uniform mediump vec4 unity_SHBb;
					uniform mediump vec4 unity_SHC;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 tmpvar_13;
					  tmpvar_13 = normalize((tmpvar_7.xyz - _WorldSpaceCameraPos));
					  tmpvar_12 = tmpvar_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  mediump vec3 normalWorld_19;
					  normalWorld_19 = tmpvar_18;
					  mediump vec4 ambientOrLightmapUV_20;
					  ambientOrLightmapUV_20.w = 0.0;
					  highp vec3 lightColor0_21;
					  lightColor0_21 = unity_LightColor[0].xyz;
					  highp vec3 lightColor1_22;
					  lightColor1_22 = unity_LightColor[1].xyz;
					  highp vec3 lightColor2_23;
					  lightColor2_23 = unity_LightColor[2].xyz;
					  highp vec3 lightColor3_24;
					  lightColor3_24 = unity_LightColor[3].xyz;
					  highp vec4 lightAttenSq_25;
					  lightAttenSq_25 = unity_4LightAtten0;
					  highp vec3 normal_26;
					  normal_26 = normalWorld_19;
					  highp vec3 col_27;
					  highp vec4 ndotl_28;
					  highp vec4 lengthSq_29;
					  highp vec4 tmpvar_30;
					  tmpvar_30 = (unity_4LightPosX0 - tmpvar_7.x);
					  highp vec4 tmpvar_31;
					  tmpvar_31 = (unity_4LightPosY0 - tmpvar_7.y);
					  highp vec4 tmpvar_32;
					  tmpvar_32 = (unity_4LightPosZ0 - tmpvar_7.z);
					  lengthSq_29 = (tmpvar_30 * tmpvar_30);
					  lengthSq_29 = (lengthSq_29 + (tmpvar_31 * tmpvar_31));
					  lengthSq_29 = (lengthSq_29 + (tmpvar_32 * tmpvar_32));
					  ndotl_28 = (tmpvar_30 * normal_26.x);
					  ndotl_28 = (ndotl_28 + (tmpvar_31 * normal_26.y));
					  ndotl_28 = (ndotl_28 + (tmpvar_32 * normal_26.z));
					  highp vec4 tmpvar_33;
					  tmpvar_33 = max (vec4(0.0, 0.0, 0.0, 0.0), (ndotl_28 * inversesqrt(lengthSq_29)));
					  ndotl_28 = tmpvar_33;
					  highp vec4 tmpvar_34;
					  tmpvar_34 = (tmpvar_33 * (1.0/((1.0 + 
					    (lengthSq_29 * lightAttenSq_25)
					  ))));
					  col_27 = (lightColor0_21 * tmpvar_34.x);
					  col_27 = (col_27 + (lightColor1_22 * tmpvar_34.y));
					  col_27 = (col_27 + (lightColor2_23 * tmpvar_34.z));
					  col_27 = (col_27 + (lightColor3_24 * tmpvar_34.w));
					  ambientOrLightmapUV_20.xyz = col_27;
					  mediump vec4 tmpvar_35;
					  tmpvar_35.w = 1.0;
					  tmpvar_35.xyz = normalWorld_19;
					  mediump vec3 res_36;
					  mediump vec3 x_37;
					  x_37.x = dot (unity_SHAr, tmpvar_35);
					  x_37.y = dot (unity_SHAg, tmpvar_35);
					  x_37.z = dot (unity_SHAb, tmpvar_35);
					  mediump vec3 x1_38;
					  mediump vec4 tmpvar_39;
					  tmpvar_39 = (normalWorld_19.xyzz * normalWorld_19.yzzx);
					  x1_38.x = dot (unity_SHBr, tmpvar_39);
					  x1_38.y = dot (unity_SHBg, tmpvar_39);
					  x1_38.z = dot (unity_SHBb, tmpvar_39);
					  res_36 = (x_37 + (x1_38 + (unity_SHC.xyz * 
					    ((normalWorld_19.x * normalWorld_19.x) - (normalWorld_19.y * normalWorld_19.y))
					  )));
					  res_36 = max (((1.055 * 
					    pow (max (res_36, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  ambientOrLightmapUV_20.xyz = (ambientOrLightmapUV_20.xyz + max (vec3(0.0, 0.0, 0.0), res_36));
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					  xlv_TEXCOORD5 = ambientOrLightmapUV_20;
					}
					
					
					#endif
					#ifdef FRAGMENT
					#extension GL_EXT_shader_texture_lod : enable
					lowp vec4 impl_low_textureCubeLodEXT(lowp samplerCube sampler, highp vec3 coord, mediump float lod)
					{
					#if defined(GL_EXT_shader_texture_lod)
						return textureCubeLodEXT(sampler, coord, lod);
					#else
						return textureCube(sampler, coord, lod);
					#endif
					}
					
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform lowp samplerCube unity_SpecCube0;
					uniform mediump vec4 unity_SpecCube0_HDR;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _OcclusionMap;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec4 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump vec3 tmpvar_4;
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic));
					  mediump float tmpvar_6;
					  tmpvar_6 = (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w));
					  tmpvar_4 = (tmpvar_3 * tmpvar_6);
					  mediump vec3 tmpvar_7;
					  tmpvar_7 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_8;
					  tmpvar_8 = _LightColor0.xyz;
					  mediump float tmpvar_9;
					  lowp vec4 tmpvar_10;
					  tmpvar_10 = texture2D (_OcclusionMap, xlv_TEXCOORD0.xy);
					  tmpvar_9 = tmpvar_10.y;
					  highp vec4 tmpvar_11;
					  tmpvar_11 = unity_SpecCube0_HDR;
					  mediump vec3 tmpvar_12;
					  mediump float tmpvar_13;
					  tmpvar_13 = (1.0 - _Glossiness);
					  tmpvar_12 = (xlv_TEXCOORD1 - (2.0 * (
					    dot (tmpvar_7, xlv_TEXCOORD1)
					   * tmpvar_7)));
					  mediump vec4 hdr_14;
					  hdr_14 = tmpvar_11;
					  mediump vec4 tmpvar_15;
					  tmpvar_15.xyz = tmpvar_12;
					  tmpvar_15.w = ((tmpvar_13 * (1.7 - 
					    (0.7 * tmpvar_13)
					  )) * 6.0);
					  lowp vec4 tmpvar_16;
					  tmpvar_16 = impl_low_textureCubeLodEXT (unity_SpecCube0, tmpvar_12, tmpvar_15.w);
					  mediump vec4 tmpvar_17;
					  tmpvar_17 = tmpvar_16;
					  mediump vec3 viewDir_18;
					  viewDir_18 = -(xlv_TEXCOORD1);
					  mediump vec3 color_19;
					  mediump vec2 tmpvar_20;
					  tmpvar_20.x = dot ((viewDir_18 - (2.0 * 
					    (dot (tmpvar_7, viewDir_18) * tmpvar_7)
					  )), _WorldSpaceLightPos0.xyz);
					  tmpvar_20.y = (1.0 - clamp (dot (tmpvar_7, viewDir_18), 0.0, 1.0));
					  mediump vec2 tmpvar_21;
					  tmpvar_21 = ((tmpvar_20 * tmpvar_20) * (tmpvar_20 * tmpvar_20));
					  mediump vec2 tmpvar_22;
					  tmpvar_22.x = tmpvar_21.x;
					  tmpvar_22.y = tmpvar_13;
					  lowp vec4 tmpvar_23;
					  tmpvar_23 = texture2D (unity_NHxRoughness, tmpvar_22);
					  color_19 = ((tmpvar_4 + (
					    (tmpvar_23.w * 16.0)
					   * tmpvar_5)) * (tmpvar_8 * clamp (
					    dot (tmpvar_7, _WorldSpaceLightPos0.xyz)
					  , 0.0, 1.0)));
					  color_19 = (color_19 + ((
					    (xlv_TEXCOORD5.xyz * tmpvar_9)
					   * tmpvar_4) + (
					    (((tmpvar_17.w * hdr_14.x) * tmpvar_17.xyz) * tmpvar_9)
					   * 
					    mix (tmpvar_5, vec3(clamp ((_Glossiness + (1.0 - tmpvar_6)), 0.0, 1.0)), tmpvar_21.yyy)
					  )));
					  mediump vec4 tmpvar_24;
					  tmpvar_24.w = 1.0;
					  tmpvar_24.xyz = color_19;
					  c_1.w = tmpvar_24.w;
					  c_1.xyz = color_19;
					  c_1.xyz = c_1.xyz;
					  mediump vec4 xlat_varoutput_25;
					  xlat_varoutput_25.xyz = c_1.xyz;
					  xlat_varoutput_25.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_25;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 unity_4LightPosX0;
					uniform highp vec4 unity_4LightPosY0;
					uniform highp vec4 unity_4LightPosZ0;
					uniform mediump vec4 unity_4LightAtten0;
					uniform mediump vec4 unity_LightColor[8];
					uniform mediump vec4 unity_SHAr;
					uniform mediump vec4 unity_SHAg;
					uniform mediump vec4 unity_SHAb;
					uniform mediump vec4 unity_SHBr;
					uniform mediump vec4 unity_SHBg;
					uniform mediump vec4 unity_SHBb;
					uniform mediump vec4 unity_SHC;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec4 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec4 xlv_TEXCOORD4;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  mediump vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_10;
					  highp vec4 tmpvar_11;
					  tmpvar_11.w = 1.0;
					  tmpvar_11.xyz = _glesVertex.xyz;
					  tmpvar_10 = (glstate_matrix_mvp * tmpvar_11);
					  highp vec4 texcoord_12;
					  texcoord_12.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_13;
					  if ((_UVSec == 0.0)) {
					    tmpvar_13 = tmpvar_2;
					  } else {
					    tmpvar_13 = tmpvar_3;
					  };
					  texcoord_12.zw = ((tmpvar_13 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_9.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  normalWorld_4 = tmpvar_19;
					  tmpvar_8.xyz = normalWorld_4;
					  tmpvar_6.xyz = eyeVec_5;
					  mediump vec4 ambientOrLightmapUV_20;
					  ambientOrLightmapUV_20.w = 0.0;
					  highp vec3 lightColor0_21;
					  lightColor0_21 = unity_LightColor[0].xyz;
					  highp vec3 lightColor1_22;
					  lightColor1_22 = unity_LightColor[1].xyz;
					  highp vec3 lightColor2_23;
					  lightColor2_23 = unity_LightColor[2].xyz;
					  highp vec3 lightColor3_24;
					  lightColor3_24 = unity_LightColor[3].xyz;
					  highp vec4 lightAttenSq_25;
					  lightAttenSq_25 = unity_4LightAtten0;
					  highp vec3 normal_26;
					  normal_26 = normalWorld_4;
					  highp vec3 col_27;
					  highp vec4 ndotl_28;
					  highp vec4 lengthSq_29;
					  highp vec4 tmpvar_30;
					  tmpvar_30 = (unity_4LightPosX0 - tmpvar_9.x);
					  highp vec4 tmpvar_31;
					  tmpvar_31 = (unity_4LightPosY0 - tmpvar_9.y);
					  highp vec4 tmpvar_32;
					  tmpvar_32 = (unity_4LightPosZ0 - tmpvar_9.z);
					  lengthSq_29 = (tmpvar_30 * tmpvar_30);
					  lengthSq_29 = (lengthSq_29 + (tmpvar_31 * tmpvar_31));
					  lengthSq_29 = (lengthSq_29 + (tmpvar_32 * tmpvar_32));
					  ndotl_28 = (tmpvar_30 * normal_26.x);
					  ndotl_28 = (ndotl_28 + (tmpvar_31 * normal_26.y));
					  ndotl_28 = (ndotl_28 + (tmpvar_32 * normal_26.z));
					  highp vec4 tmpvar_33;
					  tmpvar_33 = max (vec4(0.0, 0.0, 0.0, 0.0), (ndotl_28 * inversesqrt(lengthSq_29)));
					  ndotl_28 = tmpvar_33;
					  highp vec4 tmpvar_34;
					  tmpvar_34 = (tmpvar_33 * (1.0/((1.0 + 
					    (lengthSq_29 * lightAttenSq_25)
					  ))));
					  col_27 = (lightColor0_21 * tmpvar_34.x);
					  col_27 = (col_27 + (lightColor1_22 * tmpvar_34.y));
					  col_27 = (col_27 + (lightColor2_23 * tmpvar_34.z));
					  col_27 = (col_27 + (lightColor3_24 * tmpvar_34.w));
					  ambientOrLightmapUV_20.xyz = col_27;
					  mediump vec4 tmpvar_35;
					  tmpvar_35.w = 1.0;
					  tmpvar_35.xyz = normalWorld_4;
					  mediump vec3 res_36;
					  mediump vec3 x_37;
					  x_37.x = dot (unity_SHAr, tmpvar_35);
					  x_37.y = dot (unity_SHAg, tmpvar_35);
					  x_37.z = dot (unity_SHAb, tmpvar_35);
					  mediump vec3 x1_38;
					  mediump vec4 tmpvar_39;
					  tmpvar_39 = (normalWorld_4.xyzz * normalWorld_4.yzzx);
					  x1_38.x = dot (unity_SHBr, tmpvar_39);
					  x1_38.y = dot (unity_SHBg, tmpvar_39);
					  x1_38.z = dot (unity_SHBb, tmpvar_39);
					  res_36 = (x_37 + (x1_38 + (unity_SHC.xyz * 
					    ((normalWorld_4.x * normalWorld_4.x) - (normalWorld_4.y * normalWorld_4.y))
					  )));
					  res_36 = max (((1.055 * 
					    pow (max (res_36, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  ambientOrLightmapUV_20.xyz = (ambientOrLightmapUV_20.xyz + max (vec3(0.0, 0.0, 0.0), res_36));
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  mediump float x_40;
					  x_40 = (1.0 - clamp (dot (normalWorld_4, 
					    -(eyeVec_5)
					  ), 0.0, 1.0));
					  tmpvar_8.w = ((x_40 * x_40) * (x_40 * x_40));
					  tmpvar_6.w = clamp ((_Glossiness + (1.0 - 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  )), 0.0, 1.0);
					  gl_Position = tmpvar_10;
					  xlv_TEXCOORD0 = texcoord_12;
					  xlv_TEXCOORD1 = tmpvar_6;
					  xlv_TEXCOORD2 = ambientOrLightmapUV_20;
					  xlv_TEXCOORD3 = (unity_WorldToShadow[0] * (unity_ObjectToWorld * _glesVertex));
					  xlv_TEXCOORD4 = tmpvar_7;
					  xlv_TEXCOORD5 = tmpvar_8;
					}
					
					
					#endif
					#ifdef FRAGMENT
					#extension GL_EXT_shader_texture_lod : enable
					lowp vec4 impl_low_textureCubeLodEXT(lowp samplerCube sampler, highp vec3 coord, mediump float lod)
					{
					#if defined(GL_EXT_shader_texture_lod)
						return textureCubeLodEXT(sampler, coord, lod);
					#else
						return textureCube(sampler, coord, lod);
					#endif
					}
					
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform mediump vec4 _LightShadowData;
					uniform lowp samplerCube unity_SpecCube0;
					uniform mediump vec4 unity_SpecCube0_HDR;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _OcclusionMap;
					uniform highp sampler2D _ShadowMapTexture;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec4 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec4 xlv_TEXCOORD4;
					varying mediump vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump float rl_1;
					  mediump float atten_2;
					  lowp vec4 tmpvar_3;
					  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_4;
					  tmpvar_4 = (_Color.xyz * tmpvar_3.xyz);
					  mediump vec3 tmpvar_5;
					  mediump vec3 tmpvar_6;
					  tmpvar_6 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_4, vec3(_Metallic));
					  tmpvar_5 = (tmpvar_4 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)));
					  mediump vec3 tmpvar_7;
					  tmpvar_7 = _LightColor0.xyz;
					  lowp float tmpvar_8;
					  highp float lightShadowDataX_9;
					  mediump float tmpvar_10;
					  tmpvar_10 = _LightShadowData.x;
					  lightShadowDataX_9 = tmpvar_10;
					  highp float tmpvar_11;
					  tmpvar_11 = max (float((texture2D (_ShadowMapTexture, xlv_TEXCOORD3.xy).x > xlv_TEXCOORD3.z)), lightShadowDataX_9);
					  tmpvar_8 = tmpvar_11;
					  atten_2 = tmpvar_8;
					  mediump float tmpvar_12;
					  lowp vec4 tmpvar_13;
					  tmpvar_13 = texture2D (_OcclusionMap, xlv_TEXCOORD0.xy);
					  tmpvar_12 = tmpvar_13.y;
					  rl_1 = dot (xlv_TEXCOORD4.yzw, _WorldSpaceLightPos0.xyz);
					  highp vec4 tmpvar_14;
					  tmpvar_14 = unity_SpecCube0_HDR;
					  mediump float tmpvar_15;
					  tmpvar_15 = (1.0 - _Glossiness);
					  mediump vec4 hdr_16;
					  hdr_16 = tmpvar_14;
					  mediump vec4 tmpvar_17;
					  tmpvar_17.xyz = xlv_TEXCOORD4.yzw;
					  tmpvar_17.w = ((tmpvar_15 * (1.7 - 
					    (0.7 * tmpvar_15)
					  )) * 6.0);
					  lowp vec4 tmpvar_18;
					  tmpvar_18 = impl_low_textureCubeLodEXT (unity_SpecCube0, xlv_TEXCOORD4.yzw, tmpvar_17.w);
					  mediump vec4 tmpvar_19;
					  tmpvar_19 = tmpvar_18;
					  mediump vec2 tmpvar_20;
					  tmpvar_20.x = ((rl_1 * rl_1) * (rl_1 * rl_1));
					  tmpvar_20.y = tmpvar_15;
					  lowp vec4 tmpvar_21;
					  tmpvar_21 = texture2D (unity_NHxRoughness, tmpvar_20);
					  mediump vec4 tmpvar_22;
					  tmpvar_22.w = 1.0;
					  tmpvar_22.xyz = (((
					    (xlv_TEXCOORD2.xyz * tmpvar_12)
					   * tmpvar_5) + (
					    (((tmpvar_19.w * hdr_16.x) * tmpvar_19.xyz) * tmpvar_12)
					   * 
					    mix (tmpvar_6, xlv_TEXCOORD1.www, xlv_TEXCOORD5.www)
					  )) + ((tmpvar_5 + 
					    ((tmpvar_21.w * 16.0) * tmpvar_6)
					  ) * (
					    (tmpvar_7 * atten_2)
					   * 
					    clamp (dot (xlv_TEXCOORD5.xyz, _WorldSpaceLightPos0.xyz), 0.0, 1.0)
					  )));
					  mediump vec4 xlat_varoutput_23;
					  xlat_varoutput_23.xyz = tmpvar_22.xyz;
					  xlat_varoutput_23.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_23;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 unity_4LightPosX0;
					uniform highp vec4 unity_4LightPosY0;
					uniform highp vec4 unity_4LightPosZ0;
					uniform mediump vec4 unity_4LightAtten0;
					uniform mediump vec4 unity_LightColor[8];
					uniform mediump vec4 unity_SHAr;
					uniform mediump vec4 unity_SHAg;
					uniform mediump vec4 unity_SHAb;
					uniform mediump vec4 unity_SHBr;
					uniform mediump vec4 unity_SHBg;
					uniform mediump vec4 unity_SHBb;
					uniform mediump vec4 unity_SHC;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 tmpvar_13;
					  tmpvar_13 = normalize((tmpvar_7.xyz - _WorldSpaceCameraPos));
					  tmpvar_12 = tmpvar_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  mediump vec3 normalWorld_19;
					  normalWorld_19 = tmpvar_18;
					  mediump vec4 ambientOrLightmapUV_20;
					  ambientOrLightmapUV_20.w = 0.0;
					  highp vec3 lightColor0_21;
					  lightColor0_21 = unity_LightColor[0].xyz;
					  highp vec3 lightColor1_22;
					  lightColor1_22 = unity_LightColor[1].xyz;
					  highp vec3 lightColor2_23;
					  lightColor2_23 = unity_LightColor[2].xyz;
					  highp vec3 lightColor3_24;
					  lightColor3_24 = unity_LightColor[3].xyz;
					  highp vec4 lightAttenSq_25;
					  lightAttenSq_25 = unity_4LightAtten0;
					  highp vec3 normal_26;
					  normal_26 = normalWorld_19;
					  highp vec3 col_27;
					  highp vec4 ndotl_28;
					  highp vec4 lengthSq_29;
					  highp vec4 tmpvar_30;
					  tmpvar_30 = (unity_4LightPosX0 - tmpvar_7.x);
					  highp vec4 tmpvar_31;
					  tmpvar_31 = (unity_4LightPosY0 - tmpvar_7.y);
					  highp vec4 tmpvar_32;
					  tmpvar_32 = (unity_4LightPosZ0 - tmpvar_7.z);
					  lengthSq_29 = (tmpvar_30 * tmpvar_30);
					  lengthSq_29 = (lengthSq_29 + (tmpvar_31 * tmpvar_31));
					  lengthSq_29 = (lengthSq_29 + (tmpvar_32 * tmpvar_32));
					  ndotl_28 = (tmpvar_30 * normal_26.x);
					  ndotl_28 = (ndotl_28 + (tmpvar_31 * normal_26.y));
					  ndotl_28 = (ndotl_28 + (tmpvar_32 * normal_26.z));
					  highp vec4 tmpvar_33;
					  tmpvar_33 = max (vec4(0.0, 0.0, 0.0, 0.0), (ndotl_28 * inversesqrt(lengthSq_29)));
					  ndotl_28 = tmpvar_33;
					  highp vec4 tmpvar_34;
					  tmpvar_34 = (tmpvar_33 * (1.0/((1.0 + 
					    (lengthSq_29 * lightAttenSq_25)
					  ))));
					  col_27 = (lightColor0_21 * tmpvar_34.x);
					  col_27 = (col_27 + (lightColor1_22 * tmpvar_34.y));
					  col_27 = (col_27 + (lightColor2_23 * tmpvar_34.z));
					  col_27 = (col_27 + (lightColor3_24 * tmpvar_34.w));
					  ambientOrLightmapUV_20.xyz = col_27;
					  mediump vec4 tmpvar_35;
					  tmpvar_35.w = 1.0;
					  tmpvar_35.xyz = normalWorld_19;
					  mediump vec3 res_36;
					  mediump vec3 x_37;
					  x_37.x = dot (unity_SHAr, tmpvar_35);
					  x_37.y = dot (unity_SHAg, tmpvar_35);
					  x_37.z = dot (unity_SHAb, tmpvar_35);
					  mediump vec3 x1_38;
					  mediump vec4 tmpvar_39;
					  tmpvar_39 = (normalWorld_19.xyzz * normalWorld_19.yzzx);
					  x1_38.x = dot (unity_SHBr, tmpvar_39);
					  x1_38.y = dot (unity_SHBg, tmpvar_39);
					  x1_38.z = dot (unity_SHBb, tmpvar_39);
					  res_36 = (x_37 + (x1_38 + (unity_SHC.xyz * 
					    ((normalWorld_19.x * normalWorld_19.x) - (normalWorld_19.y * normalWorld_19.y))
					  )));
					  res_36 = max (((1.055 * 
					    pow (max (res_36, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  ambientOrLightmapUV_20.xyz = (ambientOrLightmapUV_20.xyz + max (vec3(0.0, 0.0, 0.0), res_36));
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					  xlv_TEXCOORD5 = ambientOrLightmapUV_20;
					  xlv_TEXCOORD6 = (unity_WorldToShadow[0] * (unity_ObjectToWorld * _glesVertex));
					}
					
					
					#endif
					#ifdef FRAGMENT
					#extension GL_EXT_shader_texture_lod : enable
					lowp vec4 impl_low_textureCubeLodEXT(lowp samplerCube sampler, highp vec3 coord, mediump float lod)
					{
					#if defined(GL_EXT_shader_texture_lod)
						return textureCubeLodEXT(sampler, coord, lod);
					#else
						return textureCube(sampler, coord, lod);
					#endif
					}
					
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform mediump vec4 _LightShadowData;
					uniform lowp samplerCube unity_SpecCube0;
					uniform mediump vec4 unity_SpecCube0_HDR;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _OcclusionMap;
					uniform highp sampler2D _ShadowMapTexture;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec4 c_1;
					  mediump float atten_2;
					  lowp vec4 tmpvar_3;
					  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_4;
					  tmpvar_4 = (_Color.xyz * tmpvar_3.xyz);
					  mediump vec3 tmpvar_5;
					  mediump vec3 tmpvar_6;
					  tmpvar_6 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_4, vec3(_Metallic));
					  mediump float tmpvar_7;
					  tmpvar_7 = (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w));
					  tmpvar_5 = (tmpvar_4 * tmpvar_7);
					  mediump vec3 tmpvar_8;
					  tmpvar_8 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_9;
					  tmpvar_9 = _LightColor0.xyz;
					  lowp float tmpvar_10;
					  highp float lightShadowDataX_11;
					  mediump float tmpvar_12;
					  tmpvar_12 = _LightShadowData.x;
					  lightShadowDataX_11 = tmpvar_12;
					  highp float tmpvar_13;
					  tmpvar_13 = max (float((texture2D (_ShadowMapTexture, xlv_TEXCOORD6.xy).x > xlv_TEXCOORD6.z)), lightShadowDataX_11);
					  tmpvar_10 = tmpvar_13;
					  atten_2 = tmpvar_10;
					  mediump float tmpvar_14;
					  lowp vec4 tmpvar_15;
					  tmpvar_15 = texture2D (_OcclusionMap, xlv_TEXCOORD0.xy);
					  tmpvar_14 = tmpvar_15.y;
					  highp vec4 tmpvar_16;
					  tmpvar_16 = unity_SpecCube0_HDR;
					  mediump vec3 tmpvar_17;
					  mediump float tmpvar_18;
					  tmpvar_18 = (1.0 - _Glossiness);
					  tmpvar_17 = (xlv_TEXCOORD1 - (2.0 * (
					    dot (tmpvar_8, xlv_TEXCOORD1)
					   * tmpvar_8)));
					  mediump vec4 hdr_19;
					  hdr_19 = tmpvar_16;
					  mediump vec4 tmpvar_20;
					  tmpvar_20.xyz = tmpvar_17;
					  tmpvar_20.w = ((tmpvar_18 * (1.7 - 
					    (0.7 * tmpvar_18)
					  )) * 6.0);
					  lowp vec4 tmpvar_21;
					  tmpvar_21 = impl_low_textureCubeLodEXT (unity_SpecCube0, tmpvar_17, tmpvar_20.w);
					  mediump vec4 tmpvar_22;
					  tmpvar_22 = tmpvar_21;
					  mediump vec3 viewDir_23;
					  viewDir_23 = -(xlv_TEXCOORD1);
					  mediump vec3 color_24;
					  mediump vec2 tmpvar_25;
					  tmpvar_25.x = dot ((viewDir_23 - (2.0 * 
					    (dot (tmpvar_8, viewDir_23) * tmpvar_8)
					  )), _WorldSpaceLightPos0.xyz);
					  tmpvar_25.y = (1.0 - clamp (dot (tmpvar_8, viewDir_23), 0.0, 1.0));
					  mediump vec2 tmpvar_26;
					  tmpvar_26 = ((tmpvar_25 * tmpvar_25) * (tmpvar_25 * tmpvar_25));
					  mediump vec2 tmpvar_27;
					  tmpvar_27.x = tmpvar_26.x;
					  tmpvar_27.y = tmpvar_18;
					  lowp vec4 tmpvar_28;
					  tmpvar_28 = texture2D (unity_NHxRoughness, tmpvar_27);
					  color_24 = ((tmpvar_5 + (
					    (tmpvar_28.w * 16.0)
					   * tmpvar_6)) * ((tmpvar_9 * atten_2) * clamp (
					    dot (tmpvar_8, _WorldSpaceLightPos0.xyz)
					  , 0.0, 1.0)));
					  color_24 = (color_24 + ((
					    (xlv_TEXCOORD5.xyz * tmpvar_14)
					   * tmpvar_5) + (
					    (((tmpvar_22.w * hdr_19.x) * tmpvar_22.xyz) * tmpvar_14)
					   * 
					    mix (tmpvar_6, vec3(clamp ((_Glossiness + (1.0 - tmpvar_7)), 0.0, 1.0)), tmpvar_26.yyy)
					  )));
					  mediump vec4 tmpvar_29;
					  tmpvar_29.w = 1.0;
					  tmpvar_29.xyz = color_24;
					  c_1.w = tmpvar_29.w;
					  c_1.xyz = color_24;
					  c_1.xyz = c_1.xyz;
					  mediump vec4 xlat_varoutput_30;
					  xlat_varoutput_30.xyz = c_1.xyz;
					  xlat_varoutput_30.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_30;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 unity_4LightPosX0;
					uniform highp vec4 unity_4LightPosY0;
					uniform highp vec4 unity_4LightPosZ0;
					uniform mediump vec4 unity_4LightAtten0;
					uniform mediump vec4 unity_LightColor[8];
					uniform mediump vec4 unity_SHAr;
					uniform mediump vec4 unity_SHAg;
					uniform mediump vec4 unity_SHAb;
					uniform mediump vec4 unity_SHBr;
					uniform mediump vec4 unity_SHBg;
					uniform mediump vec4 unity_SHBb;
					uniform mediump vec4 unity_SHC;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 tmpvar_13;
					  tmpvar_13 = normalize((tmpvar_7.xyz - _WorldSpaceCameraPos));
					  tmpvar_12 = tmpvar_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  mediump vec3 normalWorld_19;
					  normalWorld_19 = tmpvar_18;
					  mediump vec4 ambientOrLightmapUV_20;
					  ambientOrLightmapUV_20.w = 0.0;
					  highp vec3 lightColor0_21;
					  lightColor0_21 = unity_LightColor[0].xyz;
					  highp vec3 lightColor1_22;
					  lightColor1_22 = unity_LightColor[1].xyz;
					  highp vec3 lightColor2_23;
					  lightColor2_23 = unity_LightColor[2].xyz;
					  highp vec3 lightColor3_24;
					  lightColor3_24 = unity_LightColor[3].xyz;
					  highp vec4 lightAttenSq_25;
					  lightAttenSq_25 = unity_4LightAtten0;
					  highp vec3 normal_26;
					  normal_26 = normalWorld_19;
					  highp vec3 col_27;
					  highp vec4 ndotl_28;
					  highp vec4 lengthSq_29;
					  highp vec4 tmpvar_30;
					  tmpvar_30 = (unity_4LightPosX0 - tmpvar_7.x);
					  highp vec4 tmpvar_31;
					  tmpvar_31 = (unity_4LightPosY0 - tmpvar_7.y);
					  highp vec4 tmpvar_32;
					  tmpvar_32 = (unity_4LightPosZ0 - tmpvar_7.z);
					  lengthSq_29 = (tmpvar_30 * tmpvar_30);
					  lengthSq_29 = (lengthSq_29 + (tmpvar_31 * tmpvar_31));
					  lengthSq_29 = (lengthSq_29 + (tmpvar_32 * tmpvar_32));
					  ndotl_28 = (tmpvar_30 * normal_26.x);
					  ndotl_28 = (ndotl_28 + (tmpvar_31 * normal_26.y));
					  ndotl_28 = (ndotl_28 + (tmpvar_32 * normal_26.z));
					  highp vec4 tmpvar_33;
					  tmpvar_33 = max (vec4(0.0, 0.0, 0.0, 0.0), (ndotl_28 * inversesqrt(lengthSq_29)));
					  ndotl_28 = tmpvar_33;
					  highp vec4 tmpvar_34;
					  tmpvar_34 = (tmpvar_33 * (1.0/((1.0 + 
					    (lengthSq_29 * lightAttenSq_25)
					  ))));
					  col_27 = (lightColor0_21 * tmpvar_34.x);
					  col_27 = (col_27 + (lightColor1_22 * tmpvar_34.y));
					  col_27 = (col_27 + (lightColor2_23 * tmpvar_34.z));
					  col_27 = (col_27 + (lightColor3_24 * tmpvar_34.w));
					  ambientOrLightmapUV_20.xyz = col_27;
					  mediump vec4 tmpvar_35;
					  tmpvar_35.w = 1.0;
					  tmpvar_35.xyz = normalWorld_19;
					  mediump vec3 res_36;
					  mediump vec3 x_37;
					  x_37.x = dot (unity_SHAr, tmpvar_35);
					  x_37.y = dot (unity_SHAg, tmpvar_35);
					  x_37.z = dot (unity_SHAb, tmpvar_35);
					  mediump vec3 x1_38;
					  mediump vec4 tmpvar_39;
					  tmpvar_39 = (normalWorld_19.xyzz * normalWorld_19.yzzx);
					  x1_38.x = dot (unity_SHBr, tmpvar_39);
					  x1_38.y = dot (unity_SHBg, tmpvar_39);
					  x1_38.z = dot (unity_SHBb, tmpvar_39);
					  res_36 = (x_37 + (x1_38 + (unity_SHC.xyz * 
					    ((normalWorld_19.x * normalWorld_19.x) - (normalWorld_19.y * normalWorld_19.y))
					  )));
					  res_36 = max (((1.055 * 
					    pow (max (res_36, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  ambientOrLightmapUV_20.xyz = (ambientOrLightmapUV_20.xyz + max (vec3(0.0, 0.0, 0.0), res_36));
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					  xlv_TEXCOORD5 = ambientOrLightmapUV_20;
					  xlv_TEXCOORD6 = (unity_WorldToShadow[0] * (unity_ObjectToWorld * _glesVertex));
					}
					
					
					#endif
					#ifdef FRAGMENT
					#extension GL_EXT_shader_texture_lod : enable
					lowp vec4 impl_low_textureCubeLodEXT(lowp samplerCube sampler, highp vec3 coord, mediump float lod)
					{
					#if defined(GL_EXT_shader_texture_lod)
						return textureCubeLodEXT(sampler, coord, lod);
					#else
						return textureCube(sampler, coord, lod);
					#endif
					}
					
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform mediump vec4 _LightShadowData;
					uniform lowp samplerCube unity_SpecCube0;
					uniform mediump vec4 unity_SpecCube0_HDR;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _OcclusionMap;
					uniform highp sampler2D _ShadowMapTexture;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying mediump vec4 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec4 c_1;
					  mediump float atten_2;
					  lowp vec4 tmpvar_3;
					  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_4;
					  tmpvar_4 = (_Color.xyz * tmpvar_3.xyz);
					  mediump vec3 tmpvar_5;
					  mediump vec3 tmpvar_6;
					  tmpvar_6 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_4, vec3(_Metallic));
					  mediump float tmpvar_7;
					  tmpvar_7 = (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w));
					  tmpvar_5 = (tmpvar_4 * tmpvar_7);
					  mediump vec3 tmpvar_8;
					  tmpvar_8 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_9;
					  tmpvar_9 = _LightColor0.xyz;
					  lowp float tmpvar_10;
					  highp float lightShadowDataX_11;
					  mediump float tmpvar_12;
					  tmpvar_12 = _LightShadowData.x;
					  lightShadowDataX_11 = tmpvar_12;
					  highp float tmpvar_13;
					  tmpvar_13 = max (float((texture2D (_ShadowMapTexture, xlv_TEXCOORD6.xy).x > xlv_TEXCOORD6.z)), lightShadowDataX_11);
					  tmpvar_10 = tmpvar_13;
					  atten_2 = tmpvar_10;
					  mediump float tmpvar_14;
					  lowp vec4 tmpvar_15;
					  tmpvar_15 = texture2D (_OcclusionMap, xlv_TEXCOORD0.xy);
					  tmpvar_14 = tmpvar_15.y;
					  highp vec4 tmpvar_16;
					  tmpvar_16 = unity_SpecCube0_HDR;
					  mediump vec3 tmpvar_17;
					  mediump float tmpvar_18;
					  tmpvar_18 = (1.0 - _Glossiness);
					  tmpvar_17 = (xlv_TEXCOORD1 - (2.0 * (
					    dot (tmpvar_8, xlv_TEXCOORD1)
					   * tmpvar_8)));
					  mediump vec4 hdr_19;
					  hdr_19 = tmpvar_16;
					  mediump vec4 tmpvar_20;
					  tmpvar_20.xyz = tmpvar_17;
					  tmpvar_20.w = ((tmpvar_18 * (1.7 - 
					    (0.7 * tmpvar_18)
					  )) * 6.0);
					  lowp vec4 tmpvar_21;
					  tmpvar_21 = impl_low_textureCubeLodEXT (unity_SpecCube0, tmpvar_17, tmpvar_20.w);
					  mediump vec4 tmpvar_22;
					  tmpvar_22 = tmpvar_21;
					  mediump vec3 viewDir_23;
					  viewDir_23 = -(xlv_TEXCOORD1);
					  mediump vec3 color_24;
					  mediump vec2 tmpvar_25;
					  tmpvar_25.x = dot ((viewDir_23 - (2.0 * 
					    (dot (tmpvar_8, viewDir_23) * tmpvar_8)
					  )), _WorldSpaceLightPos0.xyz);
					  tmpvar_25.y = (1.0 - clamp (dot (tmpvar_8, viewDir_23), 0.0, 1.0));
					  mediump vec2 tmpvar_26;
					  tmpvar_26 = ((tmpvar_25 * tmpvar_25) * (tmpvar_25 * tmpvar_25));
					  mediump vec2 tmpvar_27;
					  tmpvar_27.x = tmpvar_26.x;
					  tmpvar_27.y = tmpvar_18;
					  lowp vec4 tmpvar_28;
					  tmpvar_28 = texture2D (unity_NHxRoughness, tmpvar_27);
					  color_24 = ((tmpvar_5 + (
					    (tmpvar_28.w * 16.0)
					   * tmpvar_6)) * ((tmpvar_9 * atten_2) * clamp (
					    dot (tmpvar_8, _WorldSpaceLightPos0.xyz)
					  , 0.0, 1.0)));
					  color_24 = (color_24 + ((
					    (xlv_TEXCOORD5.xyz * tmpvar_14)
					   * tmpvar_5) + (
					    (((tmpvar_22.w * hdr_19.x) * tmpvar_22.xyz) * tmpvar_14)
					   * 
					    mix (tmpvar_6, vec3(clamp ((_Glossiness + (1.0 - tmpvar_7)), 0.0, 1.0)), tmpvar_26.yyy)
					  )));
					  mediump vec4 tmpvar_29;
					  tmpvar_29.w = 1.0;
					  tmpvar_29.xyz = color_24;
					  c_1.w = tmpvar_29.w;
					  c_1.xyz = color_24;
					  c_1.xyz = c_1.xyz;
					  mediump vec4 xlat_varoutput_30;
					  xlat_varoutput_30.xyz = c_1.xyz;
					  xlat_varoutput_30.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_30;
					}
					
					
					#endif
}
}
Program "fp" {
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
}
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
}
}
 }
 Pass {
  Name "FORWARD_DELTA"
  Tags { "LIGHTMODE"="ForwardAdd" "SHADOWSUPPORT"="true" "RenderType"="Opaque" "PerformanceChecks"="False" }
  ZWrite Off
  Blend [_SrcBlend] One
  GpuProgramID 438143
Program "vp" {
SubProgram "gles hw_tier01 " {
Keywords { "POINT" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec3 lightDir_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_13;
					  tmpvar_13 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  lightDir_6 = tmpvar_13;
					  mediump vec3 tmpvar_14;
					  highp vec3 n_15;
					  n_15 = lightDir_6;
					  highp vec3 tmpvar_16;
					  tmpvar_16 = normalize(n_15);
					  tmpvar_14 = tmpvar_16;
					  lightDir_6 = tmpvar_14;
					  highp vec3 tmpvar_17;
					  tmpvar_17 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_17;
					  highp vec3 norm_18;
					  norm_18 = tmpvar_1;
					  highp vec4 v_19;
					  v_19.x = unity_WorldToObject[0].x;
					  v_19.y = unity_WorldToObject[1].x;
					  v_19.z = unity_WorldToObject[2].x;
					  v_19.w = unity_WorldToObject[3].x;
					  highp vec4 v_20;
					  v_20.x = unity_WorldToObject[0].y;
					  v_20.y = unity_WorldToObject[1].y;
					  v_20.z = unity_WorldToObject[2].y;
					  v_20.w = unity_WorldToObject[3].y;
					  highp vec4 v_21;
					  v_21.x = unity_WorldToObject[0].z;
					  v_21.y = unity_WorldToObject[1].z;
					  v_21.z = unity_WorldToObject[2].z;
					  v_21.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_22;
					  tmpvar_22 = normalize(((
					    (v_19.xyz * norm_18.x)
					   + 
					    (v_20.xyz * norm_18.y)
					  ) + (v_21.xyz * norm_18.z)));
					  normalWorld_4 = tmpvar_22;
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = (unity_WorldToLight * (unity_ObjectToWorld * _glesVertex)).xyz;
					  xlv_TEXCOORD3 = tmpvar_7;
					  xlv_TEXCOORD4 = tmpvar_14;
					  xlv_TEXCOORD5 = normalWorld_4;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _LightTexture0;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump float tmpvar_4;
					  tmpvar_4 = dot (xlv_TEXCOORD3.yzw, xlv_TEXCOORD4);
					  mediump vec2 tmpvar_5;
					  tmpvar_5.x = ((tmpvar_4 * tmpvar_4) * (tmpvar_4 * tmpvar_4));
					  tmpvar_5.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (unity_NHxRoughness, tmpvar_5);
					  c_1 = (((tmpvar_3 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_6.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic))
					  )) * _LightColor0.xyz);
					  highp float tmpvar_7;
					  tmpvar_7 = dot (xlv_TEXCOORD1, xlv_TEXCOORD1);
					  lowp vec4 tmpvar_8;
					  tmpvar_8 = texture2D (_LightTexture0, vec2(tmpvar_7));
					  c_1 = (c_1 * (tmpvar_8.w * clamp (
					    dot (xlv_TEXCOORD5, xlv_TEXCOORD4)
					  , 0.0, 1.0)));
					  mediump vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = c_1;
					  mediump vec4 xlat_varoutput_10;
					  xlat_varoutput_10.xyz = tmpvar_9.xyz;
					  xlat_varoutput_10.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_10;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "POINT" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  highp vec3 lightDir_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_13;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  tmpvar_13 = tmpvar_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_7.xyz = tmpvar_19;
					  mediump vec3 tmpvar_20;
					  highp vec3 tmpvar_21;
					  tmpvar_21 = normalize((_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w)));
					  tmpvar_20 = tmpvar_21;
					  lightDir_4 = tmpvar_20;
					  tmpvar_5.w = lightDir_4.x;
					  tmpvar_6.w = lightDir_4.y;
					  tmpvar_7.w = lightDir_4.z;
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = tmpvar_13;
					  xlv_TEXCOORD2 = tmpvar_5;
					  xlv_TEXCOORD2_1 = tmpvar_6;
					  xlv_TEXCOORD2_2 = tmpvar_7;
					  xlv_TEXCOORD5 = (unity_WorldToLight * (unity_ObjectToWorld * _glesVertex)).xyz;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _LightTexture0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_4;
					  tmpvar_4.x = xlv_TEXCOORD2.w;
					  tmpvar_4.y = xlv_TEXCOORD2_1.w;
					  tmpvar_4.z = xlv_TEXCOORD2_2.w;
					  highp float tmpvar_5;
					  tmpvar_5 = dot (xlv_TEXCOORD5, xlv_TEXCOORD5);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (_LightTexture0, vec2(tmpvar_5));
					  mediump float atten_7;
					  atten_7 = tmpvar_6.w;
					  mediump vec3 tmpvar_8;
					  tmpvar_8 = _LightColor0.xyz;
					  tmpvar_8 = (tmpvar_8 * atten_7);
					  mediump vec3 viewDir_9;
					  viewDir_9 = -(xlv_TEXCOORD1);
					  mediump vec2 tmpvar_10;
					  tmpvar_10.x = dot ((viewDir_9 - (2.0 * 
					    (dot (tmpvar_3, viewDir_9) * tmpvar_3)
					  )), tmpvar_4);
					  tmpvar_10.y = (1.0 - clamp (dot (tmpvar_3, viewDir_9), 0.0, 1.0));
					  mediump vec2 tmpvar_11;
					  tmpvar_11.x = ((tmpvar_10 * tmpvar_10) * (tmpvar_10 * tmpvar_10)).x;
					  tmpvar_11.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_12;
					  tmpvar_12 = texture2D (unity_NHxRoughness, tmpvar_11);
					  mediump vec4 tmpvar_13;
					  tmpvar_13.w = 1.0;
					  tmpvar_13.xyz = (((tmpvar_2 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_12.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic))
					  )) * (tmpvar_8 * clamp (
					    dot (tmpvar_3, tmpvar_4)
					  , 0.0, 1.0)));
					  mediump vec4 xlat_varoutput_14;
					  xlat_varoutput_14.xyz = tmpvar_13.xyz;
					  xlat_varoutput_14.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_14;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "POINT" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  highp vec3 lightDir_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_13;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  tmpvar_13 = tmpvar_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_7.xyz = tmpvar_19;
					  mediump vec3 tmpvar_20;
					  highp vec3 tmpvar_21;
					  tmpvar_21 = normalize((_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w)));
					  tmpvar_20 = tmpvar_21;
					  lightDir_4 = tmpvar_20;
					  tmpvar_5.w = lightDir_4.x;
					  tmpvar_6.w = lightDir_4.y;
					  tmpvar_7.w = lightDir_4.z;
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = tmpvar_13;
					  xlv_TEXCOORD2 = tmpvar_5;
					  xlv_TEXCOORD2_1 = tmpvar_6;
					  xlv_TEXCOORD2_2 = tmpvar_7;
					  xlv_TEXCOORD5 = (unity_WorldToLight * (unity_ObjectToWorld * _glesVertex)).xyz;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _LightTexture0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_4;
					  tmpvar_4.x = xlv_TEXCOORD2.w;
					  tmpvar_4.y = xlv_TEXCOORD2_1.w;
					  tmpvar_4.z = xlv_TEXCOORD2_2.w;
					  highp float tmpvar_5;
					  tmpvar_5 = dot (xlv_TEXCOORD5, xlv_TEXCOORD5);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (_LightTexture0, vec2(tmpvar_5));
					  mediump float atten_7;
					  atten_7 = tmpvar_6.w;
					  mediump vec3 tmpvar_8;
					  tmpvar_8 = _LightColor0.xyz;
					  tmpvar_8 = (tmpvar_8 * atten_7);
					  mediump vec3 viewDir_9;
					  viewDir_9 = -(xlv_TEXCOORD1);
					  mediump vec2 tmpvar_10;
					  tmpvar_10.x = dot ((viewDir_9 - (2.0 * 
					    (dot (tmpvar_3, viewDir_9) * tmpvar_3)
					  )), tmpvar_4);
					  tmpvar_10.y = (1.0 - clamp (dot (tmpvar_3, viewDir_9), 0.0, 1.0));
					  mediump vec2 tmpvar_11;
					  tmpvar_11.x = ((tmpvar_10 * tmpvar_10) * (tmpvar_10 * tmpvar_10)).x;
					  tmpvar_11.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_12;
					  tmpvar_12 = texture2D (unity_NHxRoughness, tmpvar_11);
					  mediump vec4 tmpvar_13;
					  tmpvar_13.w = 1.0;
					  tmpvar_13.xyz = (((tmpvar_2 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_12.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic))
					  )) * (tmpvar_8 * clamp (
					    dot (tmpvar_3, tmpvar_4)
					  , 0.0, 1.0)));
					  mediump vec4 xlat_varoutput_14;
					  xlat_varoutput_14.xyz = tmpvar_13.xyz;
					  xlat_varoutput_14.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_14;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec3 lightDir_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_13;
					  tmpvar_13 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  lightDir_6 = tmpvar_13;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  normalWorld_4 = tmpvar_19;
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD3 = tmpvar_7;
					  xlv_TEXCOORD4 = lightDir_6;
					  xlv_TEXCOORD5 = normalWorld_4;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump float tmpvar_4;
					  tmpvar_4 = dot (xlv_TEXCOORD3.yzw, xlv_TEXCOORD4);
					  mediump vec2 tmpvar_5;
					  tmpvar_5.x = ((tmpvar_4 * tmpvar_4) * (tmpvar_4 * tmpvar_4));
					  tmpvar_5.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (unity_NHxRoughness, tmpvar_5);
					  c_1 = (((tmpvar_3 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_6.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic))
					  )) * _LightColor0.xyz);
					  c_1 = (c_1 * clamp (dot (xlv_TEXCOORD5, xlv_TEXCOORD4), 0.0, 1.0));
					  mediump vec4 tmpvar_7;
					  tmpvar_7.w = 1.0;
					  tmpvar_7.xyz = c_1;
					  mediump vec4 xlat_varoutput_8;
					  xlat_varoutput_8.xyz = tmpvar_7.xyz;
					  xlat_varoutput_8.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_8;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 tmpvar_13;
					  tmpvar_13 = normalize((tmpvar_7.xyz - _WorldSpaceCameraPos));
					  tmpvar_12 = tmpvar_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = (_WorldSpaceLightPos0.xyz - (tmpvar_7.xyz * _WorldSpaceLightPos0.w));
					  tmpvar_4.w = tmpvar_19.x;
					  tmpvar_5.w = tmpvar_19.y;
					  tmpvar_6.w = tmpvar_19.z;
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_4;
					  tmpvar_4.x = xlv_TEXCOORD2.w;
					  tmpvar_4.y = xlv_TEXCOORD2_1.w;
					  tmpvar_4.z = xlv_TEXCOORD2_2.w;
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = _LightColor0.xyz;
					  mediump vec3 viewDir_6;
					  viewDir_6 = -(xlv_TEXCOORD1);
					  mediump vec2 tmpvar_7;
					  tmpvar_7.x = dot ((viewDir_6 - (2.0 * 
					    (dot (tmpvar_3, viewDir_6) * tmpvar_3)
					  )), tmpvar_4);
					  tmpvar_7.y = (1.0 - clamp (dot (tmpvar_3, viewDir_6), 0.0, 1.0));
					  mediump vec2 tmpvar_8;
					  tmpvar_8.x = ((tmpvar_7 * tmpvar_7) * (tmpvar_7 * tmpvar_7)).x;
					  tmpvar_8.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_9;
					  tmpvar_9 = texture2D (unity_NHxRoughness, tmpvar_8);
					  mediump vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = (((tmpvar_2 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_9.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic))
					  )) * (tmpvar_5 * clamp (
					    dot (tmpvar_3, tmpvar_4)
					  , 0.0, 1.0)));
					  mediump vec4 xlat_varoutput_11;
					  xlat_varoutput_11.xyz = tmpvar_10.xyz;
					  xlat_varoutput_11.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_11;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 tmpvar_13;
					  tmpvar_13 = normalize((tmpvar_7.xyz - _WorldSpaceCameraPos));
					  tmpvar_12 = tmpvar_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = (_WorldSpaceLightPos0.xyz - (tmpvar_7.xyz * _WorldSpaceLightPos0.w));
					  tmpvar_4.w = tmpvar_19.x;
					  tmpvar_5.w = tmpvar_19.y;
					  tmpvar_6.w = tmpvar_19.z;
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_4;
					  tmpvar_4.x = xlv_TEXCOORD2.w;
					  tmpvar_4.y = xlv_TEXCOORD2_1.w;
					  tmpvar_4.z = xlv_TEXCOORD2_2.w;
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = _LightColor0.xyz;
					  mediump vec3 viewDir_6;
					  viewDir_6 = -(xlv_TEXCOORD1);
					  mediump vec2 tmpvar_7;
					  tmpvar_7.x = dot ((viewDir_6 - (2.0 * 
					    (dot (tmpvar_3, viewDir_6) * tmpvar_3)
					  )), tmpvar_4);
					  tmpvar_7.y = (1.0 - clamp (dot (tmpvar_3, viewDir_6), 0.0, 1.0));
					  mediump vec2 tmpvar_8;
					  tmpvar_8.x = ((tmpvar_7 * tmpvar_7) * (tmpvar_7 * tmpvar_7)).x;
					  tmpvar_8.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_9;
					  tmpvar_9 = texture2D (unity_NHxRoughness, tmpvar_8);
					  mediump vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = (((tmpvar_2 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_9.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic))
					  )) * (tmpvar_5 * clamp (
					    dot (tmpvar_3, tmpvar_4)
					  , 0.0, 1.0)));
					  mediump vec4 xlat_varoutput_11;
					  xlat_varoutput_11.xyz = tmpvar_10.xyz;
					  xlat_varoutput_11.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_11;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "SPOT" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec3 lightDir_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_13;
					  tmpvar_13 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  lightDir_6 = tmpvar_13;
					  mediump vec3 tmpvar_14;
					  highp vec3 n_15;
					  n_15 = lightDir_6;
					  highp vec3 tmpvar_16;
					  tmpvar_16 = normalize(n_15);
					  tmpvar_14 = tmpvar_16;
					  lightDir_6 = tmpvar_14;
					  highp vec3 tmpvar_17;
					  tmpvar_17 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_17;
					  highp vec3 norm_18;
					  norm_18 = tmpvar_1;
					  highp vec4 v_19;
					  v_19.x = unity_WorldToObject[0].x;
					  v_19.y = unity_WorldToObject[1].x;
					  v_19.z = unity_WorldToObject[2].x;
					  v_19.w = unity_WorldToObject[3].x;
					  highp vec4 v_20;
					  v_20.x = unity_WorldToObject[0].y;
					  v_20.y = unity_WorldToObject[1].y;
					  v_20.z = unity_WorldToObject[2].y;
					  v_20.w = unity_WorldToObject[3].y;
					  highp vec4 v_21;
					  v_21.x = unity_WorldToObject[0].z;
					  v_21.y = unity_WorldToObject[1].z;
					  v_21.z = unity_WorldToObject[2].z;
					  v_21.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_22;
					  tmpvar_22 = normalize(((
					    (v_19.xyz * norm_18.x)
					   + 
					    (v_20.xyz * norm_18.y)
					  ) + (v_21.xyz * norm_18.z)));
					  normalWorld_4 = tmpvar_22;
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = (unity_WorldToLight * (unity_ObjectToWorld * _glesVertex));
					  xlv_TEXCOORD3 = tmpvar_7;
					  xlv_TEXCOORD4 = tmpvar_14;
					  xlv_TEXCOORD5 = normalWorld_4;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _LightTexture0;
					uniform sampler2D _LightTextureB0;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump float tmpvar_4;
					  tmpvar_4 = dot (xlv_TEXCOORD3.yzw, xlv_TEXCOORD4);
					  mediump vec2 tmpvar_5;
					  tmpvar_5.x = ((tmpvar_4 * tmpvar_4) * (tmpvar_4 * tmpvar_4));
					  tmpvar_5.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (unity_NHxRoughness, tmpvar_5);
					  c_1 = (((tmpvar_3 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_6.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic))
					  )) * _LightColor0.xyz);
					  lowp vec4 tmpvar_7;
					  highp vec2 P_8;
					  P_8 = ((xlv_TEXCOORD1.xy / xlv_TEXCOORD1.w) + 0.5);
					  tmpvar_7 = texture2D (_LightTexture0, P_8);
					  highp float tmpvar_9;
					  tmpvar_9 = dot (xlv_TEXCOORD1.xyz, xlv_TEXCOORD1.xyz);
					  lowp vec4 tmpvar_10;
					  tmpvar_10 = texture2D (_LightTextureB0, vec2(tmpvar_9));
					  c_1 = (c_1 * ((
					    (float((xlv_TEXCOORD1.z > 0.0)) * tmpvar_7.w)
					   * tmpvar_10.w) * clamp (
					    dot (xlv_TEXCOORD5, xlv_TEXCOORD4)
					  , 0.0, 1.0)));
					  mediump vec4 tmpvar_11;
					  tmpvar_11.w = 1.0;
					  tmpvar_11.xyz = c_1;
					  mediump vec4 xlat_varoutput_12;
					  xlat_varoutput_12.xyz = tmpvar_11.xyz;
					  xlat_varoutput_12.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_12;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "SPOT" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  highp vec3 lightDir_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_13;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  tmpvar_13 = tmpvar_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_7.xyz = tmpvar_19;
					  mediump vec3 tmpvar_20;
					  highp vec3 tmpvar_21;
					  tmpvar_21 = normalize((_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w)));
					  tmpvar_20 = tmpvar_21;
					  lightDir_4 = tmpvar_20;
					  tmpvar_5.w = lightDir_4.x;
					  tmpvar_6.w = lightDir_4.y;
					  tmpvar_7.w = lightDir_4.z;
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = tmpvar_13;
					  xlv_TEXCOORD2 = tmpvar_5;
					  xlv_TEXCOORD2_1 = tmpvar_6;
					  xlv_TEXCOORD2_2 = tmpvar_7;
					  xlv_TEXCOORD5 = (unity_WorldToLight * (unity_ObjectToWorld * _glesVertex));
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _LightTexture0;
					uniform sampler2D _LightTextureB0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec4 xlv_TEXCOORD5;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_4;
					  tmpvar_4.x = xlv_TEXCOORD2.w;
					  tmpvar_4.y = xlv_TEXCOORD2_1.w;
					  tmpvar_4.z = xlv_TEXCOORD2_2.w;
					  lowp vec4 tmpvar_5;
					  highp vec2 P_6;
					  P_6 = ((xlv_TEXCOORD5.xy / xlv_TEXCOORD5.w) + 0.5);
					  tmpvar_5 = texture2D (_LightTexture0, P_6);
					  highp float tmpvar_7;
					  tmpvar_7 = dot (xlv_TEXCOORD5.xyz, xlv_TEXCOORD5.xyz);
					  lowp vec4 tmpvar_8;
					  tmpvar_8 = texture2D (_LightTextureB0, vec2(tmpvar_7));
					  mediump float atten_9;
					  atten_9 = ((float(
					    (xlv_TEXCOORD5.z > 0.0)
					  ) * tmpvar_5.w) * tmpvar_8.w);
					  mediump vec3 tmpvar_10;
					  tmpvar_10 = _LightColor0.xyz;
					  tmpvar_10 = (tmpvar_10 * atten_9);
					  mediump vec3 viewDir_11;
					  viewDir_11 = -(xlv_TEXCOORD1);
					  mediump vec2 tmpvar_12;
					  tmpvar_12.x = dot ((viewDir_11 - (2.0 * 
					    (dot (tmpvar_3, viewDir_11) * tmpvar_3)
					  )), tmpvar_4);
					  tmpvar_12.y = (1.0 - clamp (dot (tmpvar_3, viewDir_11), 0.0, 1.0));
					  mediump vec2 tmpvar_13;
					  tmpvar_13.x = ((tmpvar_12 * tmpvar_12) * (tmpvar_12 * tmpvar_12)).x;
					  tmpvar_13.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_14;
					  tmpvar_14 = texture2D (unity_NHxRoughness, tmpvar_13);
					  mediump vec4 tmpvar_15;
					  tmpvar_15.w = 1.0;
					  tmpvar_15.xyz = (((tmpvar_2 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_14.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic))
					  )) * (tmpvar_10 * clamp (
					    dot (tmpvar_3, tmpvar_4)
					  , 0.0, 1.0)));
					  mediump vec4 xlat_varoutput_16;
					  xlat_varoutput_16.xyz = tmpvar_15.xyz;
					  xlat_varoutput_16.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_16;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "SPOT" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  highp vec3 lightDir_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_13;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  tmpvar_13 = tmpvar_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_7.xyz = tmpvar_19;
					  mediump vec3 tmpvar_20;
					  highp vec3 tmpvar_21;
					  tmpvar_21 = normalize((_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w)));
					  tmpvar_20 = tmpvar_21;
					  lightDir_4 = tmpvar_20;
					  tmpvar_5.w = lightDir_4.x;
					  tmpvar_6.w = lightDir_4.y;
					  tmpvar_7.w = lightDir_4.z;
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = tmpvar_13;
					  xlv_TEXCOORD2 = tmpvar_5;
					  xlv_TEXCOORD2_1 = tmpvar_6;
					  xlv_TEXCOORD2_2 = tmpvar_7;
					  xlv_TEXCOORD5 = (unity_WorldToLight * (unity_ObjectToWorld * _glesVertex));
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _LightTexture0;
					uniform sampler2D _LightTextureB0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec4 xlv_TEXCOORD5;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_4;
					  tmpvar_4.x = xlv_TEXCOORD2.w;
					  tmpvar_4.y = xlv_TEXCOORD2_1.w;
					  tmpvar_4.z = xlv_TEXCOORD2_2.w;
					  lowp vec4 tmpvar_5;
					  highp vec2 P_6;
					  P_6 = ((xlv_TEXCOORD5.xy / xlv_TEXCOORD5.w) + 0.5);
					  tmpvar_5 = texture2D (_LightTexture0, P_6);
					  highp float tmpvar_7;
					  tmpvar_7 = dot (xlv_TEXCOORD5.xyz, xlv_TEXCOORD5.xyz);
					  lowp vec4 tmpvar_8;
					  tmpvar_8 = texture2D (_LightTextureB0, vec2(tmpvar_7));
					  mediump float atten_9;
					  atten_9 = ((float(
					    (xlv_TEXCOORD5.z > 0.0)
					  ) * tmpvar_5.w) * tmpvar_8.w);
					  mediump vec3 tmpvar_10;
					  tmpvar_10 = _LightColor0.xyz;
					  tmpvar_10 = (tmpvar_10 * atten_9);
					  mediump vec3 viewDir_11;
					  viewDir_11 = -(xlv_TEXCOORD1);
					  mediump vec2 tmpvar_12;
					  tmpvar_12.x = dot ((viewDir_11 - (2.0 * 
					    (dot (tmpvar_3, viewDir_11) * tmpvar_3)
					  )), tmpvar_4);
					  tmpvar_12.y = (1.0 - clamp (dot (tmpvar_3, viewDir_11), 0.0, 1.0));
					  mediump vec2 tmpvar_13;
					  tmpvar_13.x = ((tmpvar_12 * tmpvar_12) * (tmpvar_12 * tmpvar_12)).x;
					  tmpvar_13.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_14;
					  tmpvar_14 = texture2D (unity_NHxRoughness, tmpvar_13);
					  mediump vec4 tmpvar_15;
					  tmpvar_15.w = 1.0;
					  tmpvar_15.xyz = (((tmpvar_2 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_14.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic))
					  )) * (tmpvar_10 * clamp (
					    dot (tmpvar_3, tmpvar_4)
					  , 0.0, 1.0)));
					  mediump vec4 xlat_varoutput_16;
					  xlat_varoutput_16.xyz = tmpvar_15.xyz;
					  xlat_varoutput_16.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_16;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "POINT_COOKIE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec3 lightDir_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_13;
					  tmpvar_13 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  lightDir_6 = tmpvar_13;
					  mediump vec3 tmpvar_14;
					  highp vec3 n_15;
					  n_15 = lightDir_6;
					  highp vec3 tmpvar_16;
					  tmpvar_16 = normalize(n_15);
					  tmpvar_14 = tmpvar_16;
					  lightDir_6 = tmpvar_14;
					  highp vec3 tmpvar_17;
					  tmpvar_17 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_17;
					  highp vec3 norm_18;
					  norm_18 = tmpvar_1;
					  highp vec4 v_19;
					  v_19.x = unity_WorldToObject[0].x;
					  v_19.y = unity_WorldToObject[1].x;
					  v_19.z = unity_WorldToObject[2].x;
					  v_19.w = unity_WorldToObject[3].x;
					  highp vec4 v_20;
					  v_20.x = unity_WorldToObject[0].y;
					  v_20.y = unity_WorldToObject[1].y;
					  v_20.z = unity_WorldToObject[2].y;
					  v_20.w = unity_WorldToObject[3].y;
					  highp vec4 v_21;
					  v_21.x = unity_WorldToObject[0].z;
					  v_21.y = unity_WorldToObject[1].z;
					  v_21.z = unity_WorldToObject[2].z;
					  v_21.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_22;
					  tmpvar_22 = normalize(((
					    (v_19.xyz * norm_18.x)
					   + 
					    (v_20.xyz * norm_18.y)
					  ) + (v_21.xyz * norm_18.z)));
					  normalWorld_4 = tmpvar_22;
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = (unity_WorldToLight * (unity_ObjectToWorld * _glesVertex)).xyz;
					  xlv_TEXCOORD3 = tmpvar_7;
					  xlv_TEXCOORD4 = tmpvar_14;
					  xlv_TEXCOORD5 = normalWorld_4;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform lowp samplerCube _LightTexture0;
					uniform sampler2D _LightTextureB0;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump float tmpvar_4;
					  tmpvar_4 = dot (xlv_TEXCOORD3.yzw, xlv_TEXCOORD4);
					  mediump vec2 tmpvar_5;
					  tmpvar_5.x = ((tmpvar_4 * tmpvar_4) * (tmpvar_4 * tmpvar_4));
					  tmpvar_5.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (unity_NHxRoughness, tmpvar_5);
					  c_1 = (((tmpvar_3 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_6.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic))
					  )) * _LightColor0.xyz);
					  highp float tmpvar_7;
					  tmpvar_7 = dot (xlv_TEXCOORD1, xlv_TEXCOORD1);
					  lowp vec4 tmpvar_8;
					  tmpvar_8 = texture2D (_LightTextureB0, vec2(tmpvar_7));
					  lowp vec4 tmpvar_9;
					  tmpvar_9 = textureCube (_LightTexture0, xlv_TEXCOORD1);
					  c_1 = (c_1 * ((tmpvar_8.w * tmpvar_9.w) * clamp (
					    dot (xlv_TEXCOORD5, xlv_TEXCOORD4)
					  , 0.0, 1.0)));
					  mediump vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = c_1;
					  mediump vec4 xlat_varoutput_11;
					  xlat_varoutput_11.xyz = tmpvar_10.xyz;
					  xlat_varoutput_11.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_11;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "POINT_COOKIE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  highp vec3 lightDir_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_13;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  tmpvar_13 = tmpvar_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_7.xyz = tmpvar_19;
					  mediump vec3 tmpvar_20;
					  highp vec3 tmpvar_21;
					  tmpvar_21 = normalize((_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w)));
					  tmpvar_20 = tmpvar_21;
					  lightDir_4 = tmpvar_20;
					  tmpvar_5.w = lightDir_4.x;
					  tmpvar_6.w = lightDir_4.y;
					  tmpvar_7.w = lightDir_4.z;
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = tmpvar_13;
					  xlv_TEXCOORD2 = tmpvar_5;
					  xlv_TEXCOORD2_1 = tmpvar_6;
					  xlv_TEXCOORD2_2 = tmpvar_7;
					  xlv_TEXCOORD5 = (unity_WorldToLight * (unity_ObjectToWorld * _glesVertex)).xyz;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform lowp samplerCube _LightTexture0;
					uniform sampler2D _LightTextureB0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_4;
					  tmpvar_4.x = xlv_TEXCOORD2.w;
					  tmpvar_4.y = xlv_TEXCOORD2_1.w;
					  tmpvar_4.z = xlv_TEXCOORD2_2.w;
					  highp float tmpvar_5;
					  tmpvar_5 = dot (xlv_TEXCOORD5, xlv_TEXCOORD5);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (_LightTextureB0, vec2(tmpvar_5));
					  lowp vec4 tmpvar_7;
					  tmpvar_7 = textureCube (_LightTexture0, xlv_TEXCOORD5);
					  mediump float atten_8;
					  atten_8 = (tmpvar_6.w * tmpvar_7.w);
					  mediump vec3 tmpvar_9;
					  tmpvar_9 = _LightColor0.xyz;
					  tmpvar_9 = (tmpvar_9 * atten_8);
					  mediump vec3 viewDir_10;
					  viewDir_10 = -(xlv_TEXCOORD1);
					  mediump vec2 tmpvar_11;
					  tmpvar_11.x = dot ((viewDir_10 - (2.0 * 
					    (dot (tmpvar_3, viewDir_10) * tmpvar_3)
					  )), tmpvar_4);
					  tmpvar_11.y = (1.0 - clamp (dot (tmpvar_3, viewDir_10), 0.0, 1.0));
					  mediump vec2 tmpvar_12;
					  tmpvar_12.x = ((tmpvar_11 * tmpvar_11) * (tmpvar_11 * tmpvar_11)).x;
					  tmpvar_12.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_13;
					  tmpvar_13 = texture2D (unity_NHxRoughness, tmpvar_12);
					  mediump vec4 tmpvar_14;
					  tmpvar_14.w = 1.0;
					  tmpvar_14.xyz = (((tmpvar_2 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_13.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic))
					  )) * (tmpvar_9 * clamp (
					    dot (tmpvar_3, tmpvar_4)
					  , 0.0, 1.0)));
					  mediump vec4 xlat_varoutput_15;
					  xlat_varoutput_15.xyz = tmpvar_14.xyz;
					  xlat_varoutput_15.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_15;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "POINT_COOKIE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  highp vec3 lightDir_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_13;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  tmpvar_13 = tmpvar_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_7.xyz = tmpvar_19;
					  mediump vec3 tmpvar_20;
					  highp vec3 tmpvar_21;
					  tmpvar_21 = normalize((_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w)));
					  tmpvar_20 = tmpvar_21;
					  lightDir_4 = tmpvar_20;
					  tmpvar_5.w = lightDir_4.x;
					  tmpvar_6.w = lightDir_4.y;
					  tmpvar_7.w = lightDir_4.z;
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = tmpvar_13;
					  xlv_TEXCOORD2 = tmpvar_5;
					  xlv_TEXCOORD2_1 = tmpvar_6;
					  xlv_TEXCOORD2_2 = tmpvar_7;
					  xlv_TEXCOORD5 = (unity_WorldToLight * (unity_ObjectToWorld * _glesVertex)).xyz;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform lowp samplerCube _LightTexture0;
					uniform sampler2D _LightTextureB0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_4;
					  tmpvar_4.x = xlv_TEXCOORD2.w;
					  tmpvar_4.y = xlv_TEXCOORD2_1.w;
					  tmpvar_4.z = xlv_TEXCOORD2_2.w;
					  highp float tmpvar_5;
					  tmpvar_5 = dot (xlv_TEXCOORD5, xlv_TEXCOORD5);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (_LightTextureB0, vec2(tmpvar_5));
					  lowp vec4 tmpvar_7;
					  tmpvar_7 = textureCube (_LightTexture0, xlv_TEXCOORD5);
					  mediump float atten_8;
					  atten_8 = (tmpvar_6.w * tmpvar_7.w);
					  mediump vec3 tmpvar_9;
					  tmpvar_9 = _LightColor0.xyz;
					  tmpvar_9 = (tmpvar_9 * atten_8);
					  mediump vec3 viewDir_10;
					  viewDir_10 = -(xlv_TEXCOORD1);
					  mediump vec2 tmpvar_11;
					  tmpvar_11.x = dot ((viewDir_10 - (2.0 * 
					    (dot (tmpvar_3, viewDir_10) * tmpvar_3)
					  )), tmpvar_4);
					  tmpvar_11.y = (1.0 - clamp (dot (tmpvar_3, viewDir_10), 0.0, 1.0));
					  mediump vec2 tmpvar_12;
					  tmpvar_12.x = ((tmpvar_11 * tmpvar_11) * (tmpvar_11 * tmpvar_11)).x;
					  tmpvar_12.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_13;
					  tmpvar_13 = texture2D (unity_NHxRoughness, tmpvar_12);
					  mediump vec4 tmpvar_14;
					  tmpvar_14.w = 1.0;
					  tmpvar_14.xyz = (((tmpvar_2 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_13.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic))
					  )) * (tmpvar_9 * clamp (
					    dot (tmpvar_3, tmpvar_4)
					  , 0.0, 1.0)));
					  mediump vec4 xlat_varoutput_15;
					  xlat_varoutput_15.xyz = tmpvar_14.xyz;
					  xlat_varoutput_15.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_15;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL_COOKIE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec3 lightDir_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_13;
					  tmpvar_13 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  lightDir_6 = tmpvar_13;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  normalWorld_4 = tmpvar_19;
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = (unity_WorldToLight * (unity_ObjectToWorld * _glesVertex)).xy;
					  xlv_TEXCOORD3 = tmpvar_7;
					  xlv_TEXCOORD4 = lightDir_6;
					  xlv_TEXCOORD5 = normalWorld_4;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _LightTexture0;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump float tmpvar_4;
					  tmpvar_4 = dot (xlv_TEXCOORD3.yzw, xlv_TEXCOORD4);
					  mediump vec2 tmpvar_5;
					  tmpvar_5.x = ((tmpvar_4 * tmpvar_4) * (tmpvar_4 * tmpvar_4));
					  tmpvar_5.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (unity_NHxRoughness, tmpvar_5);
					  c_1 = (((tmpvar_3 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_6.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic))
					  )) * _LightColor0.xyz);
					  lowp vec4 tmpvar_7;
					  tmpvar_7 = texture2D (_LightTexture0, xlv_TEXCOORD1);
					  c_1 = (c_1 * (tmpvar_7.w * clamp (
					    dot (xlv_TEXCOORD5, xlv_TEXCOORD4)
					  , 0.0, 1.0)));
					  mediump vec4 tmpvar_8;
					  tmpvar_8.w = 1.0;
					  tmpvar_8.xyz = c_1;
					  mediump vec4 xlat_varoutput_9;
					  xlat_varoutput_9.xyz = tmpvar_8.xyz;
					  xlat_varoutput_9.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_9;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL_COOKIE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec2 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 tmpvar_13;
					  tmpvar_13 = normalize((tmpvar_7.xyz - _WorldSpaceCameraPos));
					  tmpvar_12 = tmpvar_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = (_WorldSpaceLightPos0.xyz - (tmpvar_7.xyz * _WorldSpaceLightPos0.w));
					  tmpvar_4.w = tmpvar_19.x;
					  tmpvar_5.w = tmpvar_19.y;
					  tmpvar_6.w = tmpvar_19.z;
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					  xlv_TEXCOORD5 = (unity_WorldToLight * (unity_ObjectToWorld * _glesVertex)).xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _LightTexture0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec2 xlv_TEXCOORD5;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_4;
					  tmpvar_4.x = xlv_TEXCOORD2.w;
					  tmpvar_4.y = xlv_TEXCOORD2_1.w;
					  tmpvar_4.z = xlv_TEXCOORD2_2.w;
					  lowp vec4 tmpvar_5;
					  tmpvar_5 = texture2D (_LightTexture0, xlv_TEXCOORD5);
					  mediump float atten_6;
					  atten_6 = tmpvar_5.w;
					  mediump vec3 tmpvar_7;
					  tmpvar_7 = _LightColor0.xyz;
					  tmpvar_7 = (tmpvar_7 * atten_6);
					  mediump vec3 viewDir_8;
					  viewDir_8 = -(xlv_TEXCOORD1);
					  mediump vec2 tmpvar_9;
					  tmpvar_9.x = dot ((viewDir_8 - (2.0 * 
					    (dot (tmpvar_3, viewDir_8) * tmpvar_3)
					  )), tmpvar_4);
					  tmpvar_9.y = (1.0 - clamp (dot (tmpvar_3, viewDir_8), 0.0, 1.0));
					  mediump vec2 tmpvar_10;
					  tmpvar_10.x = ((tmpvar_9 * tmpvar_9) * (tmpvar_9 * tmpvar_9)).x;
					  tmpvar_10.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_11;
					  tmpvar_11 = texture2D (unity_NHxRoughness, tmpvar_10);
					  mediump vec4 tmpvar_12;
					  tmpvar_12.w = 1.0;
					  tmpvar_12.xyz = (((tmpvar_2 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_11.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic))
					  )) * (tmpvar_7 * clamp (
					    dot (tmpvar_3, tmpvar_4)
					  , 0.0, 1.0)));
					  mediump vec4 xlat_varoutput_13;
					  xlat_varoutput_13.xyz = tmpvar_12.xyz;
					  xlat_varoutput_13.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_13;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL_COOKIE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec2 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 tmpvar_13;
					  tmpvar_13 = normalize((tmpvar_7.xyz - _WorldSpaceCameraPos));
					  tmpvar_12 = tmpvar_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = (_WorldSpaceLightPos0.xyz - (tmpvar_7.xyz * _WorldSpaceLightPos0.w));
					  tmpvar_4.w = tmpvar_19.x;
					  tmpvar_5.w = tmpvar_19.y;
					  tmpvar_6.w = tmpvar_19.z;
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					  xlv_TEXCOORD5 = (unity_WorldToLight * (unity_ObjectToWorld * _glesVertex)).xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform sampler2D _LightTexture0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec2 xlv_TEXCOORD5;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_4;
					  tmpvar_4.x = xlv_TEXCOORD2.w;
					  tmpvar_4.y = xlv_TEXCOORD2_1.w;
					  tmpvar_4.z = xlv_TEXCOORD2_2.w;
					  lowp vec4 tmpvar_5;
					  tmpvar_5 = texture2D (_LightTexture0, xlv_TEXCOORD5);
					  mediump float atten_6;
					  atten_6 = tmpvar_5.w;
					  mediump vec3 tmpvar_7;
					  tmpvar_7 = _LightColor0.xyz;
					  tmpvar_7 = (tmpvar_7 * atten_6);
					  mediump vec3 viewDir_8;
					  viewDir_8 = -(xlv_TEXCOORD1);
					  mediump vec2 tmpvar_9;
					  tmpvar_9.x = dot ((viewDir_8 - (2.0 * 
					    (dot (tmpvar_3, viewDir_8) * tmpvar_3)
					  )), tmpvar_4);
					  tmpvar_9.y = (1.0 - clamp (dot (tmpvar_3, viewDir_8), 0.0, 1.0));
					  mediump vec2 tmpvar_10;
					  tmpvar_10.x = ((tmpvar_9 * tmpvar_9) * (tmpvar_9 * tmpvar_9)).x;
					  tmpvar_10.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_11;
					  tmpvar_11 = texture2D (unity_NHxRoughness, tmpvar_10);
					  mediump vec4 tmpvar_12;
					  tmpvar_12.w = 1.0;
					  tmpvar_12.xyz = (((tmpvar_2 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_11.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic))
					  )) * (tmpvar_7 * clamp (
					    dot (tmpvar_3, tmpvar_4)
					  , 0.0, 1.0)));
					  mediump vec4 xlat_varoutput_13;
					  xlat_varoutput_13.xyz = tmpvar_12.xyz;
					  xlat_varoutput_13.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_13;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "SPOT" "SHADOWS_DEPTH" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec3 lightDir_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_13;
					  tmpvar_13 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  lightDir_6 = tmpvar_13;
					  mediump vec3 tmpvar_14;
					  highp vec3 n_15;
					  n_15 = lightDir_6;
					  highp vec3 tmpvar_16;
					  tmpvar_16 = normalize(n_15);
					  tmpvar_14 = tmpvar_16;
					  lightDir_6 = tmpvar_14;
					  highp vec3 tmpvar_17;
					  tmpvar_17 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_17;
					  highp vec3 norm_18;
					  norm_18 = tmpvar_1;
					  highp vec4 v_19;
					  v_19.x = unity_WorldToObject[0].x;
					  v_19.y = unity_WorldToObject[1].x;
					  v_19.z = unity_WorldToObject[2].x;
					  v_19.w = unity_WorldToObject[3].x;
					  highp vec4 v_20;
					  v_20.x = unity_WorldToObject[0].y;
					  v_20.y = unity_WorldToObject[1].y;
					  v_20.z = unity_WorldToObject[2].y;
					  v_20.w = unity_WorldToObject[3].y;
					  highp vec4 v_21;
					  v_21.x = unity_WorldToObject[0].z;
					  v_21.y = unity_WorldToObject[1].z;
					  v_21.z = unity_WorldToObject[2].z;
					  v_21.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_22;
					  tmpvar_22 = normalize(((
					    (v_19.xyz * norm_18.x)
					   + 
					    (v_20.xyz * norm_18.y)
					  ) + (v_21.xyz * norm_18.z)));
					  normalWorld_4 = tmpvar_22;
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  highp vec4 tmpvar_23;
					  tmpvar_23 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD1 = (unity_WorldToLight * tmpvar_23);
					  xlv_TEXCOORD2 = (unity_WorldToShadow[0] * tmpvar_23);
					  xlv_TEXCOORD3 = tmpvar_7;
					  xlv_TEXCOORD4 = tmpvar_14;
					  xlv_TEXCOORD5 = normalWorld_4;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp sampler2D _ShadowMapTexture;
					uniform sampler2D _LightTexture0;
					uniform sampler2D _LightTextureB0;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump float tmpvar_4;
					  tmpvar_4 = dot (xlv_TEXCOORD3.yzw, xlv_TEXCOORD4);
					  mediump vec2 tmpvar_5;
					  tmpvar_5.x = ((tmpvar_4 * tmpvar_4) * (tmpvar_4 * tmpvar_4));
					  tmpvar_5.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (unity_NHxRoughness, tmpvar_5);
					  c_1 = (((tmpvar_3 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_6.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic))
					  )) * _LightColor0.xyz);
					  lowp vec4 tmpvar_7;
					  highp vec2 P_8;
					  P_8 = ((xlv_TEXCOORD1.xy / xlv_TEXCOORD1.w) + 0.5);
					  tmpvar_7 = texture2D (_LightTexture0, P_8);
					  highp float tmpvar_9;
					  tmpvar_9 = dot (xlv_TEXCOORD1.xyz, xlv_TEXCOORD1.xyz);
					  lowp vec4 tmpvar_10;
					  tmpvar_10 = texture2D (_LightTextureB0, vec2(tmpvar_9));
					  lowp float tmpvar_11;
					  highp vec4 tmpvar_12;
					  tmpvar_12 = texture2DProj (_ShadowMapTexture, xlv_TEXCOORD2);
					  mediump float tmpvar_13;
					  if ((tmpvar_12.x < (xlv_TEXCOORD2.z / xlv_TEXCOORD2.w))) {
					    tmpvar_13 = _LightShadowData.x;
					  } else {
					    tmpvar_13 = 1.0;
					  };
					  tmpvar_11 = tmpvar_13;
					  c_1 = (c_1 * ((
					    ((float((xlv_TEXCOORD1.z > 0.0)) * tmpvar_7.w) * tmpvar_10.w)
					   * tmpvar_11) * clamp (
					    dot (xlv_TEXCOORD5, xlv_TEXCOORD4)
					  , 0.0, 1.0)));
					  mediump vec4 tmpvar_14;
					  tmpvar_14.w = 1.0;
					  tmpvar_14.xyz = c_1;
					  mediump vec4 xlat_varoutput_15;
					  xlat_varoutput_15.xyz = tmpvar_14.xyz;
					  xlat_varoutput_15.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_15;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "SPOT" "SHADOWS_DEPTH" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec4 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  highp vec3 lightDir_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_13;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  tmpvar_13 = tmpvar_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_7.xyz = tmpvar_19;
					  mediump vec3 tmpvar_20;
					  highp vec3 tmpvar_21;
					  tmpvar_21 = normalize((_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w)));
					  tmpvar_20 = tmpvar_21;
					  lightDir_4 = tmpvar_20;
					  tmpvar_5.w = lightDir_4.x;
					  tmpvar_6.w = lightDir_4.y;
					  tmpvar_7.w = lightDir_4.z;
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = tmpvar_13;
					  xlv_TEXCOORD2 = tmpvar_5;
					  xlv_TEXCOORD2_1 = tmpvar_6;
					  xlv_TEXCOORD2_2 = tmpvar_7;
					  highp vec4 tmpvar_22;
					  tmpvar_22 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD5 = (unity_WorldToLight * tmpvar_22);
					  xlv_TEXCOORD6 = (unity_WorldToShadow[0] * tmpvar_22);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp sampler2D _ShadowMapTexture;
					uniform sampler2D _LightTexture0;
					uniform sampler2D _LightTextureB0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec4 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  mediump vec3 tmpvar_4;
					  tmpvar_4 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic));
					  tmpvar_3 = (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)));
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_6;
					  tmpvar_6.x = xlv_TEXCOORD2.w;
					  tmpvar_6.y = xlv_TEXCOORD2_1.w;
					  tmpvar_6.z = xlv_TEXCOORD2_2.w;
					  lowp vec4 tmpvar_7;
					  highp vec2 P_8;
					  P_8 = ((xlv_TEXCOORD5.xy / xlv_TEXCOORD5.w) + 0.5);
					  tmpvar_7 = texture2D (_LightTexture0, P_8);
					  highp float tmpvar_9;
					  tmpvar_9 = dot (xlv_TEXCOORD5.xyz, xlv_TEXCOORD5.xyz);
					  lowp vec4 tmpvar_10;
					  tmpvar_10 = texture2D (_LightTextureB0, vec2(tmpvar_9));
					  lowp float tmpvar_11;
					  highp vec4 tmpvar_12;
					  tmpvar_12 = texture2DProj (_ShadowMapTexture, xlv_TEXCOORD6);
					  mediump float tmpvar_13;
					  if ((tmpvar_12.x < (xlv_TEXCOORD6.z / xlv_TEXCOORD6.w))) {
					    tmpvar_13 = _LightShadowData.x;
					  } else {
					    tmpvar_13 = 1.0;
					  };
					  tmpvar_11 = tmpvar_13;
					  mediump float atten_14;
					  atten_14 = (((
					    float((xlv_TEXCOORD5.z > 0.0))
					   * tmpvar_7.w) * tmpvar_10.w) * tmpvar_11);
					  mediump vec3 tmpvar_15;
					  tmpvar_15 = _LightColor0.xyz;
					  tmpvar_15 = (tmpvar_15 * atten_14);
					  mediump vec3 viewDir_16;
					  viewDir_16 = -(xlv_TEXCOORD1);
					  mediump vec2 tmpvar_17;
					  tmpvar_17.x = dot ((viewDir_16 - (2.0 * 
					    (dot (tmpvar_5, viewDir_16) * tmpvar_5)
					  )), tmpvar_6);
					  tmpvar_17.y = (1.0 - clamp (dot (tmpvar_5, viewDir_16), 0.0, 1.0));
					  mediump vec2 tmpvar_18;
					  tmpvar_18.x = ((tmpvar_17 * tmpvar_17) * (tmpvar_17 * tmpvar_17)).x;
					  tmpvar_18.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_19;
					  tmpvar_19 = texture2D (unity_NHxRoughness, tmpvar_18);
					  mediump vec4 tmpvar_20;
					  tmpvar_20.w = 1.0;
					  tmpvar_20.xyz = ((tmpvar_3 + (
					    (tmpvar_19.w * 16.0)
					   * tmpvar_4)) * (tmpvar_15 * clamp (
					    dot (tmpvar_5, tmpvar_6)
					  , 0.0, 1.0)));
					  mediump vec4 xlat_varoutput_21;
					  xlat_varoutput_21.xyz = tmpvar_20.xyz;
					  xlat_varoutput_21.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_21;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "SPOT" "SHADOWS_DEPTH" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec4 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  highp vec3 lightDir_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_13;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  tmpvar_13 = tmpvar_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_7.xyz = tmpvar_19;
					  mediump vec3 tmpvar_20;
					  highp vec3 tmpvar_21;
					  tmpvar_21 = normalize((_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w)));
					  tmpvar_20 = tmpvar_21;
					  lightDir_4 = tmpvar_20;
					  tmpvar_5.w = lightDir_4.x;
					  tmpvar_6.w = lightDir_4.y;
					  tmpvar_7.w = lightDir_4.z;
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = tmpvar_13;
					  xlv_TEXCOORD2 = tmpvar_5;
					  xlv_TEXCOORD2_1 = tmpvar_6;
					  xlv_TEXCOORD2_2 = tmpvar_7;
					  highp vec4 tmpvar_22;
					  tmpvar_22 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD5 = (unity_WorldToLight * tmpvar_22);
					  xlv_TEXCOORD6 = (unity_WorldToShadow[0] * tmpvar_22);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp sampler2D _ShadowMapTexture;
					uniform sampler2D _LightTexture0;
					uniform sampler2D _LightTextureB0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec4 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  mediump vec3 tmpvar_4;
					  tmpvar_4 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic));
					  tmpvar_3 = (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)));
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_6;
					  tmpvar_6.x = xlv_TEXCOORD2.w;
					  tmpvar_6.y = xlv_TEXCOORD2_1.w;
					  tmpvar_6.z = xlv_TEXCOORD2_2.w;
					  lowp vec4 tmpvar_7;
					  highp vec2 P_8;
					  P_8 = ((xlv_TEXCOORD5.xy / xlv_TEXCOORD5.w) + 0.5);
					  tmpvar_7 = texture2D (_LightTexture0, P_8);
					  highp float tmpvar_9;
					  tmpvar_9 = dot (xlv_TEXCOORD5.xyz, xlv_TEXCOORD5.xyz);
					  lowp vec4 tmpvar_10;
					  tmpvar_10 = texture2D (_LightTextureB0, vec2(tmpvar_9));
					  lowp float tmpvar_11;
					  highp vec4 tmpvar_12;
					  tmpvar_12 = texture2DProj (_ShadowMapTexture, xlv_TEXCOORD6);
					  mediump float tmpvar_13;
					  if ((tmpvar_12.x < (xlv_TEXCOORD6.z / xlv_TEXCOORD6.w))) {
					    tmpvar_13 = _LightShadowData.x;
					  } else {
					    tmpvar_13 = 1.0;
					  };
					  tmpvar_11 = tmpvar_13;
					  mediump float atten_14;
					  atten_14 = (((
					    float((xlv_TEXCOORD5.z > 0.0))
					   * tmpvar_7.w) * tmpvar_10.w) * tmpvar_11);
					  mediump vec3 tmpvar_15;
					  tmpvar_15 = _LightColor0.xyz;
					  tmpvar_15 = (tmpvar_15 * atten_14);
					  mediump vec3 viewDir_16;
					  viewDir_16 = -(xlv_TEXCOORD1);
					  mediump vec2 tmpvar_17;
					  tmpvar_17.x = dot ((viewDir_16 - (2.0 * 
					    (dot (tmpvar_5, viewDir_16) * tmpvar_5)
					  )), tmpvar_6);
					  tmpvar_17.y = (1.0 - clamp (dot (tmpvar_5, viewDir_16), 0.0, 1.0));
					  mediump vec2 tmpvar_18;
					  tmpvar_18.x = ((tmpvar_17 * tmpvar_17) * (tmpvar_17 * tmpvar_17)).x;
					  tmpvar_18.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_19;
					  tmpvar_19 = texture2D (unity_NHxRoughness, tmpvar_18);
					  mediump vec4 tmpvar_20;
					  tmpvar_20.w = 1.0;
					  tmpvar_20.xyz = ((tmpvar_3 + (
					    (tmpvar_19.w * 16.0)
					   * tmpvar_4)) * (tmpvar_15 * clamp (
					    dot (tmpvar_5, tmpvar_6)
					  , 0.0, 1.0)));
					  mediump vec4 xlat_varoutput_21;
					  xlat_varoutput_21.xyz = tmpvar_20.xyz;
					  xlat_varoutput_21.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_21;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec3 lightDir_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_13;
					  tmpvar_13 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  lightDir_6 = tmpvar_13;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  normalWorld_4 = tmpvar_19;
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = (unity_WorldToShadow[0] * (unity_ObjectToWorld * _glesVertex));
					  xlv_TEXCOORD3 = tmpvar_7;
					  xlv_TEXCOORD4 = lightDir_6;
					  xlv_TEXCOORD5 = normalWorld_4;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp sampler2D _ShadowMapTexture;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump float tmpvar_4;
					  tmpvar_4 = dot (xlv_TEXCOORD3.yzw, xlv_TEXCOORD4);
					  mediump vec2 tmpvar_5;
					  tmpvar_5.x = ((tmpvar_4 * tmpvar_4) * (tmpvar_4 * tmpvar_4));
					  tmpvar_5.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (unity_NHxRoughness, tmpvar_5);
					  c_1 = (((tmpvar_3 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_6.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic))
					  )) * _LightColor0.xyz);
					  lowp float tmpvar_7;
					  highp float lightShadowDataX_8;
					  mediump float tmpvar_9;
					  tmpvar_9 = _LightShadowData.x;
					  lightShadowDataX_8 = tmpvar_9;
					  highp float tmpvar_10;
					  tmpvar_10 = max (float((texture2D (_ShadowMapTexture, xlv_TEXCOORD1.xy).x > xlv_TEXCOORD1.z)), lightShadowDataX_8);
					  tmpvar_7 = tmpvar_10;
					  c_1 = (c_1 * (tmpvar_7 * clamp (
					    dot (xlv_TEXCOORD5, xlv_TEXCOORD4)
					  , 0.0, 1.0)));
					  mediump vec4 tmpvar_11;
					  tmpvar_11.w = 1.0;
					  tmpvar_11.xyz = c_1;
					  mediump vec4 xlat_varoutput_12;
					  xlat_varoutput_12.xyz = tmpvar_11.xyz;
					  xlat_varoutput_12.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_12;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 tmpvar_13;
					  tmpvar_13 = normalize((tmpvar_7.xyz - _WorldSpaceCameraPos));
					  tmpvar_12 = tmpvar_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = (_WorldSpaceLightPos0.xyz - (tmpvar_7.xyz * _WorldSpaceLightPos0.w));
					  tmpvar_4.w = tmpvar_19.x;
					  tmpvar_5.w = tmpvar_19.y;
					  tmpvar_6.w = tmpvar_19.z;
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					  xlv_TEXCOORD5 = (unity_WorldToShadow[0] * (unity_ObjectToWorld * _glesVertex));
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp sampler2D _ShadowMapTexture;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec4 xlv_TEXCOORD5;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_4;
					  tmpvar_4.x = xlv_TEXCOORD2.w;
					  tmpvar_4.y = xlv_TEXCOORD2_1.w;
					  tmpvar_4.z = xlv_TEXCOORD2_2.w;
					  lowp float tmpvar_5;
					  highp float lightShadowDataX_6;
					  mediump float tmpvar_7;
					  tmpvar_7 = _LightShadowData.x;
					  lightShadowDataX_6 = tmpvar_7;
					  highp float tmpvar_8;
					  tmpvar_8 = max (float((texture2D (_ShadowMapTexture, xlv_TEXCOORD5.xy).x > xlv_TEXCOORD5.z)), lightShadowDataX_6);
					  tmpvar_5 = tmpvar_8;
					  mediump float atten_9;
					  atten_9 = tmpvar_5;
					  mediump vec3 tmpvar_10;
					  tmpvar_10 = _LightColor0.xyz;
					  tmpvar_10 = (tmpvar_10 * atten_9);
					  mediump vec3 viewDir_11;
					  viewDir_11 = -(xlv_TEXCOORD1);
					  mediump vec2 tmpvar_12;
					  tmpvar_12.x = dot ((viewDir_11 - (2.0 * 
					    (dot (tmpvar_3, viewDir_11) * tmpvar_3)
					  )), tmpvar_4);
					  tmpvar_12.y = (1.0 - clamp (dot (tmpvar_3, viewDir_11), 0.0, 1.0));
					  mediump vec2 tmpvar_13;
					  tmpvar_13.x = ((tmpvar_12 * tmpvar_12) * (tmpvar_12 * tmpvar_12)).x;
					  tmpvar_13.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_14;
					  tmpvar_14 = texture2D (unity_NHxRoughness, tmpvar_13);
					  mediump vec4 tmpvar_15;
					  tmpvar_15.w = 1.0;
					  tmpvar_15.xyz = (((tmpvar_2 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_14.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic))
					  )) * (tmpvar_10 * clamp (
					    dot (tmpvar_3, tmpvar_4)
					  , 0.0, 1.0)));
					  mediump vec4 xlat_varoutput_16;
					  xlat_varoutput_16.xyz = tmpvar_15.xyz;
					  xlat_varoutput_16.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_16;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 tmpvar_13;
					  tmpvar_13 = normalize((tmpvar_7.xyz - _WorldSpaceCameraPos));
					  tmpvar_12 = tmpvar_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = (_WorldSpaceLightPos0.xyz - (tmpvar_7.xyz * _WorldSpaceLightPos0.w));
					  tmpvar_4.w = tmpvar_19.x;
					  tmpvar_5.w = tmpvar_19.y;
					  tmpvar_6.w = tmpvar_19.z;
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					  xlv_TEXCOORD5 = (unity_WorldToShadow[0] * (unity_ObjectToWorld * _glesVertex));
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp sampler2D _ShadowMapTexture;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec4 xlv_TEXCOORD5;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_4;
					  tmpvar_4.x = xlv_TEXCOORD2.w;
					  tmpvar_4.y = xlv_TEXCOORD2_1.w;
					  tmpvar_4.z = xlv_TEXCOORD2_2.w;
					  lowp float tmpvar_5;
					  highp float lightShadowDataX_6;
					  mediump float tmpvar_7;
					  tmpvar_7 = _LightShadowData.x;
					  lightShadowDataX_6 = tmpvar_7;
					  highp float tmpvar_8;
					  tmpvar_8 = max (float((texture2D (_ShadowMapTexture, xlv_TEXCOORD5.xy).x > xlv_TEXCOORD5.z)), lightShadowDataX_6);
					  tmpvar_5 = tmpvar_8;
					  mediump float atten_9;
					  atten_9 = tmpvar_5;
					  mediump vec3 tmpvar_10;
					  tmpvar_10 = _LightColor0.xyz;
					  tmpvar_10 = (tmpvar_10 * atten_9);
					  mediump vec3 viewDir_11;
					  viewDir_11 = -(xlv_TEXCOORD1);
					  mediump vec2 tmpvar_12;
					  tmpvar_12.x = dot ((viewDir_11 - (2.0 * 
					    (dot (tmpvar_3, viewDir_11) * tmpvar_3)
					  )), tmpvar_4);
					  tmpvar_12.y = (1.0 - clamp (dot (tmpvar_3, viewDir_11), 0.0, 1.0));
					  mediump vec2 tmpvar_13;
					  tmpvar_13.x = ((tmpvar_12 * tmpvar_12) * (tmpvar_12 * tmpvar_12)).x;
					  tmpvar_13.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_14;
					  tmpvar_14 = texture2D (unity_NHxRoughness, tmpvar_13);
					  mediump vec4 tmpvar_15;
					  tmpvar_15.w = 1.0;
					  tmpvar_15.xyz = (((tmpvar_2 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_14.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic))
					  )) * (tmpvar_10 * clamp (
					    dot (tmpvar_3, tmpvar_4)
					  , 0.0, 1.0)));
					  mediump vec4 xlat_varoutput_16;
					  xlat_varoutput_16.xyz = tmpvar_15.xyz;
					  xlat_varoutput_16.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_16;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec3 lightDir_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_13;
					  tmpvar_13 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  lightDir_6 = tmpvar_13;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  normalWorld_4 = tmpvar_19;
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  highp vec4 tmpvar_20;
					  tmpvar_20 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD1 = (unity_WorldToLight * tmpvar_20).xy;
					  xlv_TEXCOORD2 = (unity_WorldToShadow[0] * tmpvar_20);
					  xlv_TEXCOORD3 = tmpvar_7;
					  xlv_TEXCOORD4 = lightDir_6;
					  xlv_TEXCOORD5 = normalWorld_4;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp sampler2D _ShadowMapTexture;
					uniform sampler2D _LightTexture0;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump float tmpvar_4;
					  tmpvar_4 = dot (xlv_TEXCOORD3.yzw, xlv_TEXCOORD4);
					  mediump vec2 tmpvar_5;
					  tmpvar_5.x = ((tmpvar_4 * tmpvar_4) * (tmpvar_4 * tmpvar_4));
					  tmpvar_5.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (unity_NHxRoughness, tmpvar_5);
					  c_1 = (((tmpvar_3 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_6.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic))
					  )) * _LightColor0.xyz);
					  lowp vec4 tmpvar_7;
					  tmpvar_7 = texture2D (_LightTexture0, xlv_TEXCOORD1);
					  lowp float tmpvar_8;
					  highp float lightShadowDataX_9;
					  mediump float tmpvar_10;
					  tmpvar_10 = _LightShadowData.x;
					  lightShadowDataX_9 = tmpvar_10;
					  highp float tmpvar_11;
					  tmpvar_11 = max (float((texture2D (_ShadowMapTexture, xlv_TEXCOORD2.xy).x > xlv_TEXCOORD2.z)), lightShadowDataX_9);
					  tmpvar_8 = tmpvar_11;
					  c_1 = (c_1 * ((tmpvar_7.w * tmpvar_8) * clamp (
					    dot (xlv_TEXCOORD5, xlv_TEXCOORD4)
					  , 0.0, 1.0)));
					  mediump vec4 tmpvar_12;
					  tmpvar_12.w = 1.0;
					  tmpvar_12.xyz = c_1;
					  mediump vec4 xlat_varoutput_13;
					  xlat_varoutput_13.xyz = tmpvar_12.xyz;
					  xlat_varoutput_13.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_13;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec2 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 tmpvar_13;
					  tmpvar_13 = normalize((tmpvar_7.xyz - _WorldSpaceCameraPos));
					  tmpvar_12 = tmpvar_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = (_WorldSpaceLightPos0.xyz - (tmpvar_7.xyz * _WorldSpaceLightPos0.w));
					  tmpvar_4.w = tmpvar_19.x;
					  tmpvar_5.w = tmpvar_19.y;
					  tmpvar_6.w = tmpvar_19.z;
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					  highp vec4 tmpvar_20;
					  tmpvar_20 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD5 = (unity_WorldToLight * tmpvar_20).xy;
					  xlv_TEXCOORD6 = (unity_WorldToShadow[0] * tmpvar_20);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp sampler2D _ShadowMapTexture;
					uniform sampler2D _LightTexture0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec2 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_4;
					  tmpvar_4.x = xlv_TEXCOORD2.w;
					  tmpvar_4.y = xlv_TEXCOORD2_1.w;
					  tmpvar_4.z = xlv_TEXCOORD2_2.w;
					  lowp vec4 tmpvar_5;
					  tmpvar_5 = texture2D (_LightTexture0, xlv_TEXCOORD5);
					  lowp float tmpvar_6;
					  highp float lightShadowDataX_7;
					  mediump float tmpvar_8;
					  tmpvar_8 = _LightShadowData.x;
					  lightShadowDataX_7 = tmpvar_8;
					  highp float tmpvar_9;
					  tmpvar_9 = max (float((texture2D (_ShadowMapTexture, xlv_TEXCOORD6.xy).x > xlv_TEXCOORD6.z)), lightShadowDataX_7);
					  tmpvar_6 = tmpvar_9;
					  mediump float atten_10;
					  atten_10 = (tmpvar_5.w * tmpvar_6);
					  mediump vec3 tmpvar_11;
					  tmpvar_11 = _LightColor0.xyz;
					  tmpvar_11 = (tmpvar_11 * atten_10);
					  mediump vec3 viewDir_12;
					  viewDir_12 = -(xlv_TEXCOORD1);
					  mediump vec2 tmpvar_13;
					  tmpvar_13.x = dot ((viewDir_12 - (2.0 * 
					    (dot (tmpvar_3, viewDir_12) * tmpvar_3)
					  )), tmpvar_4);
					  tmpvar_13.y = (1.0 - clamp (dot (tmpvar_3, viewDir_12), 0.0, 1.0));
					  mediump vec2 tmpvar_14;
					  tmpvar_14.x = ((tmpvar_13 * tmpvar_13) * (tmpvar_13 * tmpvar_13)).x;
					  tmpvar_14.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_15;
					  tmpvar_15 = texture2D (unity_NHxRoughness, tmpvar_14);
					  mediump vec4 tmpvar_16;
					  tmpvar_16.w = 1.0;
					  tmpvar_16.xyz = (((tmpvar_2 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_15.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic))
					  )) * (tmpvar_11 * clamp (
					    dot (tmpvar_3, tmpvar_4)
					  , 0.0, 1.0)));
					  mediump vec4 xlat_varoutput_17;
					  xlat_varoutput_17.xyz = tmpvar_16.xyz;
					  xlat_varoutput_17.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_17;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform highp mat4 unity_WorldToShadow[4];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec2 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec4 tmpvar_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_8;
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = _glesVertex.xyz;
					  tmpvar_8 = (glstate_matrix_mvp * tmpvar_9);
					  highp vec4 texcoord_10;
					  texcoord_10.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_11;
					  if ((_UVSec == 0.0)) {
					    tmpvar_11 = tmpvar_2;
					  } else {
					    tmpvar_11 = tmpvar_3;
					  };
					  texcoord_10.zw = ((tmpvar_11 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_12;
					  highp vec3 tmpvar_13;
					  tmpvar_13 = normalize((tmpvar_7.xyz - _WorldSpaceCameraPos));
					  tmpvar_12 = tmpvar_13;
					  highp vec3 norm_14;
					  norm_14 = tmpvar_1;
					  highp vec4 v_15;
					  v_15.x = unity_WorldToObject[0].x;
					  v_15.y = unity_WorldToObject[1].x;
					  v_15.z = unity_WorldToObject[2].x;
					  v_15.w = unity_WorldToObject[3].x;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].y;
					  v_16.y = unity_WorldToObject[1].y;
					  v_16.z = unity_WorldToObject[2].y;
					  v_16.w = unity_WorldToObject[3].y;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].z;
					  v_17.y = unity_WorldToObject[1].z;
					  v_17.z = unity_WorldToObject[2].z;
					  v_17.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = normalize(((
					    (v_15.xyz * norm_14.x)
					   + 
					    (v_16.xyz * norm_14.y)
					  ) + (v_17.xyz * norm_14.z)));
					  tmpvar_4.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = tmpvar_18;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = (_WorldSpaceLightPos0.xyz - (tmpvar_7.xyz * _WorldSpaceLightPos0.w));
					  tmpvar_4.w = tmpvar_19.x;
					  tmpvar_5.w = tmpvar_19.y;
					  tmpvar_6.w = tmpvar_19.z;
					  gl_Position = tmpvar_8;
					  xlv_TEXCOORD0 = texcoord_10;
					  xlv_TEXCOORD1 = tmpvar_12;
					  xlv_TEXCOORD2 = tmpvar_4;
					  xlv_TEXCOORD2_1 = tmpvar_5;
					  xlv_TEXCOORD2_2 = tmpvar_6;
					  highp vec4 tmpvar_20;
					  tmpvar_20 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD5 = (unity_WorldToLight * tmpvar_20).xy;
					  xlv_TEXCOORD6 = (unity_WorldToShadow[0] * tmpvar_20);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp sampler2D _ShadowMapTexture;
					uniform sampler2D _LightTexture0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec2 xlv_TEXCOORD5;
					varying highp vec4 xlv_TEXCOORD6;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_4;
					  tmpvar_4.x = xlv_TEXCOORD2.w;
					  tmpvar_4.y = xlv_TEXCOORD2_1.w;
					  tmpvar_4.z = xlv_TEXCOORD2_2.w;
					  lowp vec4 tmpvar_5;
					  tmpvar_5 = texture2D (_LightTexture0, xlv_TEXCOORD5);
					  lowp float tmpvar_6;
					  highp float lightShadowDataX_7;
					  mediump float tmpvar_8;
					  tmpvar_8 = _LightShadowData.x;
					  lightShadowDataX_7 = tmpvar_8;
					  highp float tmpvar_9;
					  tmpvar_9 = max (float((texture2D (_ShadowMapTexture, xlv_TEXCOORD6.xy).x > xlv_TEXCOORD6.z)), lightShadowDataX_7);
					  tmpvar_6 = tmpvar_9;
					  mediump float atten_10;
					  atten_10 = (tmpvar_5.w * tmpvar_6);
					  mediump vec3 tmpvar_11;
					  tmpvar_11 = _LightColor0.xyz;
					  tmpvar_11 = (tmpvar_11 * atten_10);
					  mediump vec3 viewDir_12;
					  viewDir_12 = -(xlv_TEXCOORD1);
					  mediump vec2 tmpvar_13;
					  tmpvar_13.x = dot ((viewDir_12 - (2.0 * 
					    (dot (tmpvar_3, viewDir_12) * tmpvar_3)
					  )), tmpvar_4);
					  tmpvar_13.y = (1.0 - clamp (dot (tmpvar_3, viewDir_12), 0.0, 1.0));
					  mediump vec2 tmpvar_14;
					  tmpvar_14.x = ((tmpvar_13 * tmpvar_13) * (tmpvar_13 * tmpvar_13)).x;
					  tmpvar_14.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_15;
					  tmpvar_15 = texture2D (unity_NHxRoughness, tmpvar_14);
					  mediump vec4 tmpvar_16;
					  tmpvar_16.w = 1.0;
					  tmpvar_16.xyz = (((tmpvar_2 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_15.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic))
					  )) * (tmpvar_11 * clamp (
					    dot (tmpvar_3, tmpvar_4)
					  , 0.0, 1.0)));
					  mediump vec4 xlat_varoutput_17;
					  xlat_varoutput_17.xyz = tmpvar_16.xyz;
					  xlat_varoutput_17.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_17;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "POINT" "SHADOWS_CUBE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp vec4 _LightPositionRange;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD1;
					varying highp vec3 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec3 lightDir_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_13;
					  tmpvar_13 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  lightDir_6 = tmpvar_13;
					  mediump vec3 tmpvar_14;
					  highp vec3 n_15;
					  n_15 = lightDir_6;
					  highp vec3 tmpvar_16;
					  tmpvar_16 = normalize(n_15);
					  tmpvar_14 = tmpvar_16;
					  lightDir_6 = tmpvar_14;
					  highp vec3 tmpvar_17;
					  tmpvar_17 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_17;
					  highp vec3 norm_18;
					  norm_18 = tmpvar_1;
					  highp vec4 v_19;
					  v_19.x = unity_WorldToObject[0].x;
					  v_19.y = unity_WorldToObject[1].x;
					  v_19.z = unity_WorldToObject[2].x;
					  v_19.w = unity_WorldToObject[3].x;
					  highp vec4 v_20;
					  v_20.x = unity_WorldToObject[0].y;
					  v_20.y = unity_WorldToObject[1].y;
					  v_20.z = unity_WorldToObject[2].y;
					  v_20.w = unity_WorldToObject[3].y;
					  highp vec4 v_21;
					  v_21.x = unity_WorldToObject[0].z;
					  v_21.y = unity_WorldToObject[1].z;
					  v_21.z = unity_WorldToObject[2].z;
					  v_21.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_22;
					  tmpvar_22 = normalize(((
					    (v_19.xyz * norm_18.x)
					   + 
					    (v_20.xyz * norm_18.y)
					  ) + (v_21.xyz * norm_18.z)));
					  normalWorld_4 = tmpvar_22;
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  highp vec4 tmpvar_23;
					  tmpvar_23 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD1 = (unity_WorldToLight * tmpvar_23).xyz;
					  xlv_TEXCOORD2 = (tmpvar_23.xyz - _LightPositionRange.xyz);
					  xlv_TEXCOORD3 = tmpvar_7;
					  xlv_TEXCOORD4 = tmpvar_14;
					  xlv_TEXCOORD5 = normalWorld_4;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _LightPositionRange;
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp samplerCube _ShadowMapTexture;
					uniform sampler2D _LightTexture0;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD1;
					varying highp vec3 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump float tmpvar_4;
					  tmpvar_4 = dot (xlv_TEXCOORD3.yzw, xlv_TEXCOORD4);
					  mediump vec2 tmpvar_5;
					  tmpvar_5.x = ((tmpvar_4 * tmpvar_4) * (tmpvar_4 * tmpvar_4));
					  tmpvar_5.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (unity_NHxRoughness, tmpvar_5);
					  c_1 = (((tmpvar_3 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_6.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic))
					  )) * _LightColor0.xyz);
					  highp float tmpvar_7;
					  tmpvar_7 = dot (xlv_TEXCOORD1, xlv_TEXCOORD1);
					  lowp vec4 tmpvar_8;
					  tmpvar_8 = texture2D (_LightTexture0, vec2(tmpvar_7));
					  highp float mydist_9;
					  mydist_9 = ((sqrt(
					    dot (xlv_TEXCOORD2, xlv_TEXCOORD2)
					  ) * _LightPositionRange.w) * 0.97);
					  highp float tmpvar_10;
					  tmpvar_10 = dot (textureCube (_ShadowMapTexture, xlv_TEXCOORD2), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  mediump float tmpvar_11;
					  if ((tmpvar_10 < mydist_9)) {
					    tmpvar_11 = _LightShadowData.x;
					  } else {
					    tmpvar_11 = 1.0;
					  };
					  c_1 = (c_1 * ((tmpvar_8.w * tmpvar_11) * clamp (
					    dot (xlv_TEXCOORD5, xlv_TEXCOORD4)
					  , 0.0, 1.0)));
					  mediump vec4 tmpvar_12;
					  tmpvar_12.w = 1.0;
					  tmpvar_12.xyz = c_1;
					  mediump vec4 xlat_varoutput_13;
					  xlat_varoutput_13.xyz = tmpvar_12.xyz;
					  xlat_varoutput_13.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_13;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "POINT" "SHADOWS_CUBE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp vec4 _LightPositionRange;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					varying highp vec3 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  highp vec3 lightDir_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_13;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  tmpvar_13 = tmpvar_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_7.xyz = tmpvar_19;
					  mediump vec3 tmpvar_20;
					  highp vec3 tmpvar_21;
					  tmpvar_21 = normalize((_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w)));
					  tmpvar_20 = tmpvar_21;
					  lightDir_4 = tmpvar_20;
					  tmpvar_5.w = lightDir_4.x;
					  tmpvar_6.w = lightDir_4.y;
					  tmpvar_7.w = lightDir_4.z;
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = tmpvar_13;
					  xlv_TEXCOORD2 = tmpvar_5;
					  xlv_TEXCOORD2_1 = tmpvar_6;
					  xlv_TEXCOORD2_2 = tmpvar_7;
					  highp vec4 tmpvar_22;
					  tmpvar_22 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD5 = (unity_WorldToLight * tmpvar_22).xyz;
					  xlv_TEXCOORD6 = (tmpvar_22.xyz - _LightPositionRange.xyz);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _LightPositionRange;
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp samplerCube _ShadowMapTexture;
					uniform sampler2D _LightTexture0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					varying highp vec3 xlv_TEXCOORD6;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  mediump vec3 tmpvar_4;
					  tmpvar_4 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic));
					  tmpvar_3 = (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)));
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_6;
					  tmpvar_6.x = xlv_TEXCOORD2.w;
					  tmpvar_6.y = xlv_TEXCOORD2_1.w;
					  tmpvar_6.z = xlv_TEXCOORD2_2.w;
					  highp float tmpvar_7;
					  tmpvar_7 = dot (xlv_TEXCOORD5, xlv_TEXCOORD5);
					  lowp vec4 tmpvar_8;
					  tmpvar_8 = texture2D (_LightTexture0, vec2(tmpvar_7));
					  highp float mydist_9;
					  mydist_9 = ((sqrt(
					    dot (xlv_TEXCOORD6, xlv_TEXCOORD6)
					  ) * _LightPositionRange.w) * 0.97);
					  highp float tmpvar_10;
					  tmpvar_10 = dot (textureCube (_ShadowMapTexture, xlv_TEXCOORD6), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  mediump float tmpvar_11;
					  if ((tmpvar_10 < mydist_9)) {
					    tmpvar_11 = _LightShadowData.x;
					  } else {
					    tmpvar_11 = 1.0;
					  };
					  mediump vec3 tmpvar_12;
					  tmpvar_12 = _LightColor0.xyz;
					  tmpvar_12 = (tmpvar_12 * (tmpvar_8.w * tmpvar_11));
					  mediump vec3 viewDir_13;
					  viewDir_13 = -(xlv_TEXCOORD1);
					  mediump vec2 tmpvar_14;
					  tmpvar_14.x = dot ((viewDir_13 - (2.0 * 
					    (dot (tmpvar_5, viewDir_13) * tmpvar_5)
					  )), tmpvar_6);
					  tmpvar_14.y = (1.0 - clamp (dot (tmpvar_5, viewDir_13), 0.0, 1.0));
					  mediump vec2 tmpvar_15;
					  tmpvar_15.x = ((tmpvar_14 * tmpvar_14) * (tmpvar_14 * tmpvar_14)).x;
					  tmpvar_15.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_16;
					  tmpvar_16 = texture2D (unity_NHxRoughness, tmpvar_15);
					  mediump vec4 tmpvar_17;
					  tmpvar_17.w = 1.0;
					  tmpvar_17.xyz = ((tmpvar_3 + (
					    (tmpvar_16.w * 16.0)
					   * tmpvar_4)) * (tmpvar_12 * clamp (
					    dot (tmpvar_5, tmpvar_6)
					  , 0.0, 1.0)));
					  mediump vec4 xlat_varoutput_18;
					  xlat_varoutput_18.xyz = tmpvar_17.xyz;
					  xlat_varoutput_18.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_18;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "POINT" "SHADOWS_CUBE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp vec4 _LightPositionRange;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					varying highp vec3 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  highp vec3 lightDir_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_13;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  tmpvar_13 = tmpvar_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_7.xyz = tmpvar_19;
					  mediump vec3 tmpvar_20;
					  highp vec3 tmpvar_21;
					  tmpvar_21 = normalize((_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w)));
					  tmpvar_20 = tmpvar_21;
					  lightDir_4 = tmpvar_20;
					  tmpvar_5.w = lightDir_4.x;
					  tmpvar_6.w = lightDir_4.y;
					  tmpvar_7.w = lightDir_4.z;
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = tmpvar_13;
					  xlv_TEXCOORD2 = tmpvar_5;
					  xlv_TEXCOORD2_1 = tmpvar_6;
					  xlv_TEXCOORD2_2 = tmpvar_7;
					  highp vec4 tmpvar_22;
					  tmpvar_22 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD5 = (unity_WorldToLight * tmpvar_22).xyz;
					  xlv_TEXCOORD6 = (tmpvar_22.xyz - _LightPositionRange.xyz);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _LightPositionRange;
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp samplerCube _ShadowMapTexture;
					uniform sampler2D _LightTexture0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					varying highp vec3 xlv_TEXCOORD6;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  mediump vec3 tmpvar_4;
					  tmpvar_4 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic));
					  tmpvar_3 = (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)));
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_6;
					  tmpvar_6.x = xlv_TEXCOORD2.w;
					  tmpvar_6.y = xlv_TEXCOORD2_1.w;
					  tmpvar_6.z = xlv_TEXCOORD2_2.w;
					  highp float tmpvar_7;
					  tmpvar_7 = dot (xlv_TEXCOORD5, xlv_TEXCOORD5);
					  lowp vec4 tmpvar_8;
					  tmpvar_8 = texture2D (_LightTexture0, vec2(tmpvar_7));
					  highp float mydist_9;
					  mydist_9 = ((sqrt(
					    dot (xlv_TEXCOORD6, xlv_TEXCOORD6)
					  ) * _LightPositionRange.w) * 0.97);
					  highp float tmpvar_10;
					  tmpvar_10 = dot (textureCube (_ShadowMapTexture, xlv_TEXCOORD6), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  mediump float tmpvar_11;
					  if ((tmpvar_10 < mydist_9)) {
					    tmpvar_11 = _LightShadowData.x;
					  } else {
					    tmpvar_11 = 1.0;
					  };
					  mediump vec3 tmpvar_12;
					  tmpvar_12 = _LightColor0.xyz;
					  tmpvar_12 = (tmpvar_12 * (tmpvar_8.w * tmpvar_11));
					  mediump vec3 viewDir_13;
					  viewDir_13 = -(xlv_TEXCOORD1);
					  mediump vec2 tmpvar_14;
					  tmpvar_14.x = dot ((viewDir_13 - (2.0 * 
					    (dot (tmpvar_5, viewDir_13) * tmpvar_5)
					  )), tmpvar_6);
					  tmpvar_14.y = (1.0 - clamp (dot (tmpvar_5, viewDir_13), 0.0, 1.0));
					  mediump vec2 tmpvar_15;
					  tmpvar_15.x = ((tmpvar_14 * tmpvar_14) * (tmpvar_14 * tmpvar_14)).x;
					  tmpvar_15.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_16;
					  tmpvar_16 = texture2D (unity_NHxRoughness, tmpvar_15);
					  mediump vec4 tmpvar_17;
					  tmpvar_17.w = 1.0;
					  tmpvar_17.xyz = ((tmpvar_3 + (
					    (tmpvar_16.w * 16.0)
					   * tmpvar_4)) * (tmpvar_12 * clamp (
					    dot (tmpvar_5, tmpvar_6)
					  , 0.0, 1.0)));
					  mediump vec4 xlat_varoutput_18;
					  xlat_varoutput_18.xyz = tmpvar_17.xyz;
					  xlat_varoutput_18.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_18;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp vec4 _LightPositionRange;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD1;
					varying highp vec3 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  mediump vec3 normalWorld_4;
					  mediump vec3 eyeVec_5;
					  mediump vec3 lightDir_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  highp vec3 tmpvar_13;
					  tmpvar_13 = (_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w));
					  lightDir_6 = tmpvar_13;
					  mediump vec3 tmpvar_14;
					  highp vec3 n_15;
					  n_15 = lightDir_6;
					  highp vec3 tmpvar_16;
					  tmpvar_16 = normalize(n_15);
					  tmpvar_14 = tmpvar_16;
					  lightDir_6 = tmpvar_14;
					  highp vec3 tmpvar_17;
					  tmpvar_17 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  eyeVec_5 = tmpvar_17;
					  highp vec3 norm_18;
					  norm_18 = tmpvar_1;
					  highp vec4 v_19;
					  v_19.x = unity_WorldToObject[0].x;
					  v_19.y = unity_WorldToObject[1].x;
					  v_19.z = unity_WorldToObject[2].x;
					  v_19.w = unity_WorldToObject[3].x;
					  highp vec4 v_20;
					  v_20.x = unity_WorldToObject[0].y;
					  v_20.y = unity_WorldToObject[1].y;
					  v_20.z = unity_WorldToObject[2].y;
					  v_20.w = unity_WorldToObject[3].y;
					  highp vec4 v_21;
					  v_21.x = unity_WorldToObject[0].z;
					  v_21.y = unity_WorldToObject[1].z;
					  v_21.z = unity_WorldToObject[2].z;
					  v_21.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_22;
					  tmpvar_22 = normalize(((
					    (v_19.xyz * norm_18.x)
					   + 
					    (v_20.xyz * norm_18.y)
					  ) + (v_21.xyz * norm_18.z)));
					  normalWorld_4 = tmpvar_22;
					  tmpvar_7.yzw = (eyeVec_5 - (2.0 * (
					    dot (normalWorld_4, eyeVec_5)
					   * normalWorld_4)));
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  highp vec4 tmpvar_23;
					  tmpvar_23 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD1 = (unity_WorldToLight * tmpvar_23).xyz;
					  xlv_TEXCOORD2 = (tmpvar_23.xyz - _LightPositionRange.xyz);
					  xlv_TEXCOORD3 = tmpvar_7;
					  xlv_TEXCOORD4 = tmpvar_14;
					  xlv_TEXCOORD5 = normalWorld_4;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _LightPositionRange;
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp samplerCube _ShadowMapTexture;
					uniform lowp samplerCube _LightTexture0;
					uniform sampler2D _LightTextureB0;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD1;
					varying highp vec3 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying mediump vec3 xlv_TEXCOORD5;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_3;
					  tmpvar_3 = (_Color.xyz * tmpvar_2.xyz);
					  mediump float tmpvar_4;
					  tmpvar_4 = dot (xlv_TEXCOORD3.yzw, xlv_TEXCOORD4);
					  mediump vec2 tmpvar_5;
					  tmpvar_5.x = ((tmpvar_4 * tmpvar_4) * (tmpvar_4 * tmpvar_4));
					  tmpvar_5.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (unity_NHxRoughness, tmpvar_5);
					  c_1 = (((tmpvar_3 * 
					    (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w))
					  ) + (
					    (tmpvar_6.w * 16.0)
					   * 
					    mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_3, vec3(_Metallic))
					  )) * _LightColor0.xyz);
					  highp float tmpvar_7;
					  tmpvar_7 = dot (xlv_TEXCOORD1, xlv_TEXCOORD1);
					  lowp vec4 tmpvar_8;
					  tmpvar_8 = texture2D (_LightTextureB0, vec2(tmpvar_7));
					  lowp vec4 tmpvar_9;
					  tmpvar_9 = textureCube (_LightTexture0, xlv_TEXCOORD1);
					  highp float mydist_10;
					  mydist_10 = ((sqrt(
					    dot (xlv_TEXCOORD2, xlv_TEXCOORD2)
					  ) * _LightPositionRange.w) * 0.97);
					  highp float tmpvar_11;
					  tmpvar_11 = dot (textureCube (_ShadowMapTexture, xlv_TEXCOORD2), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  mediump float tmpvar_12;
					  if ((tmpvar_11 < mydist_10)) {
					    tmpvar_12 = _LightShadowData.x;
					  } else {
					    tmpvar_12 = 1.0;
					  };
					  c_1 = (c_1 * ((
					    (tmpvar_8.w * tmpvar_9.w)
					   * tmpvar_12) * clamp (
					    dot (xlv_TEXCOORD5, xlv_TEXCOORD4)
					  , 0.0, 1.0)));
					  mediump vec4 tmpvar_13;
					  tmpvar_13.w = 1.0;
					  tmpvar_13.xyz = c_1;
					  mediump vec4 xlat_varoutput_14;
					  xlat_varoutput_14.xyz = tmpvar_13.xyz;
					  xlat_varoutput_14.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_14;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp vec4 _LightPositionRange;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					varying highp vec3 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  highp vec3 lightDir_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_13;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  tmpvar_13 = tmpvar_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_7.xyz = tmpvar_19;
					  mediump vec3 tmpvar_20;
					  highp vec3 tmpvar_21;
					  tmpvar_21 = normalize((_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w)));
					  tmpvar_20 = tmpvar_21;
					  lightDir_4 = tmpvar_20;
					  tmpvar_5.w = lightDir_4.x;
					  tmpvar_6.w = lightDir_4.y;
					  tmpvar_7.w = lightDir_4.z;
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = tmpvar_13;
					  xlv_TEXCOORD2 = tmpvar_5;
					  xlv_TEXCOORD2_1 = tmpvar_6;
					  xlv_TEXCOORD2_2 = tmpvar_7;
					  highp vec4 tmpvar_22;
					  tmpvar_22 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD5 = (unity_WorldToLight * tmpvar_22).xyz;
					  xlv_TEXCOORD6 = (tmpvar_22.xyz - _LightPositionRange.xyz);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _LightPositionRange;
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp samplerCube _ShadowMapTexture;
					uniform lowp samplerCube _LightTexture0;
					uniform sampler2D _LightTextureB0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					varying highp vec3 xlv_TEXCOORD6;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  mediump vec3 tmpvar_4;
					  tmpvar_4 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic));
					  tmpvar_3 = (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)));
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_6;
					  tmpvar_6.x = xlv_TEXCOORD2.w;
					  tmpvar_6.y = xlv_TEXCOORD2_1.w;
					  tmpvar_6.z = xlv_TEXCOORD2_2.w;
					  highp float tmpvar_7;
					  tmpvar_7 = dot (xlv_TEXCOORD5, xlv_TEXCOORD5);
					  lowp vec4 tmpvar_8;
					  tmpvar_8 = texture2D (_LightTextureB0, vec2(tmpvar_7));
					  lowp vec4 tmpvar_9;
					  tmpvar_9 = textureCube (_LightTexture0, xlv_TEXCOORD5);
					  highp float mydist_10;
					  mydist_10 = ((sqrt(
					    dot (xlv_TEXCOORD6, xlv_TEXCOORD6)
					  ) * _LightPositionRange.w) * 0.97);
					  highp float tmpvar_11;
					  tmpvar_11 = dot (textureCube (_ShadowMapTexture, xlv_TEXCOORD6), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  mediump float tmpvar_12;
					  if ((tmpvar_11 < mydist_10)) {
					    tmpvar_12 = _LightShadowData.x;
					  } else {
					    tmpvar_12 = 1.0;
					  };
					  mediump vec3 tmpvar_13;
					  tmpvar_13 = _LightColor0.xyz;
					  tmpvar_13 = (tmpvar_13 * ((tmpvar_8.w * tmpvar_9.w) * tmpvar_12));
					  mediump vec3 viewDir_14;
					  viewDir_14 = -(xlv_TEXCOORD1);
					  mediump vec2 tmpvar_15;
					  tmpvar_15.x = dot ((viewDir_14 - (2.0 * 
					    (dot (tmpvar_5, viewDir_14) * tmpvar_5)
					  )), tmpvar_6);
					  tmpvar_15.y = (1.0 - clamp (dot (tmpvar_5, viewDir_14), 0.0, 1.0));
					  mediump vec2 tmpvar_16;
					  tmpvar_16.x = ((tmpvar_15 * tmpvar_15) * (tmpvar_15 * tmpvar_15)).x;
					  tmpvar_16.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_17;
					  tmpvar_17 = texture2D (unity_NHxRoughness, tmpvar_16);
					  mediump vec4 tmpvar_18;
					  tmpvar_18.w = 1.0;
					  tmpvar_18.xyz = ((tmpvar_3 + (
					    (tmpvar_17.w * 16.0)
					   * tmpvar_4)) * (tmpvar_13 * clamp (
					    dot (tmpvar_5, tmpvar_6)
					  , 0.0, 1.0)));
					  mediump vec4 xlat_varoutput_19;
					  xlat_varoutput_19.xyz = tmpvar_18.xyz;
					  xlat_varoutput_19.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_19;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp vec4 _LightPositionRange;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _DetailAlbedoMap_ST;
					uniform mediump float _UVSec;
					uniform highp mat4 unity_WorldToLight;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					varying highp vec3 xlv_TEXCOORD6;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  tmpvar_1 = _glesNormal;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = _glesMultiTexCoord0.xy;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = _glesMultiTexCoord1.xy;
					  highp vec3 lightDir_4;
					  mediump vec4 tmpvar_5;
					  mediump vec4 tmpvar_6;
					  mediump vec4 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  highp vec4 tmpvar_9;
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = _glesVertex.xyz;
					  tmpvar_9 = (glstate_matrix_mvp * tmpvar_10);
					  highp vec4 texcoord_11;
					  texcoord_11.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec2 tmpvar_12;
					  if ((_UVSec == 0.0)) {
					    tmpvar_12 = tmpvar_2;
					  } else {
					    tmpvar_12 = tmpvar_3;
					  };
					  texcoord_11.zw = ((tmpvar_12 * _DetailAlbedoMap_ST.xy) + _DetailAlbedoMap_ST.zw);
					  mediump vec3 tmpvar_13;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_8.xyz - _WorldSpaceCameraPos));
					  tmpvar_13 = tmpvar_14;
					  highp vec3 norm_15;
					  norm_15 = tmpvar_1;
					  highp vec4 v_16;
					  v_16.x = unity_WorldToObject[0].x;
					  v_16.y = unity_WorldToObject[1].x;
					  v_16.z = unity_WorldToObject[2].x;
					  v_16.w = unity_WorldToObject[3].x;
					  highp vec4 v_17;
					  v_17.x = unity_WorldToObject[0].y;
					  v_17.y = unity_WorldToObject[1].y;
					  v_17.z = unity_WorldToObject[2].y;
					  v_17.w = unity_WorldToObject[3].y;
					  highp vec4 v_18;
					  v_18.x = unity_WorldToObject[0].z;
					  v_18.y = unity_WorldToObject[1].z;
					  v_18.z = unity_WorldToObject[2].z;
					  v_18.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_19;
					  tmpvar_19 = normalize(((
					    (v_16.xyz * norm_15.x)
					   + 
					    (v_17.xyz * norm_15.y)
					  ) + (v_18.xyz * norm_15.z)));
					  tmpvar_5.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_6.xyz = vec3(0.0, 0.0, 0.0);
					  tmpvar_7.xyz = tmpvar_19;
					  mediump vec3 tmpvar_20;
					  highp vec3 tmpvar_21;
					  tmpvar_21 = normalize((_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w)));
					  tmpvar_20 = tmpvar_21;
					  lightDir_4 = tmpvar_20;
					  tmpvar_5.w = lightDir_4.x;
					  tmpvar_6.w = lightDir_4.y;
					  tmpvar_7.w = lightDir_4.z;
					  gl_Position = tmpvar_9;
					  xlv_TEXCOORD0 = texcoord_11;
					  xlv_TEXCOORD1 = tmpvar_13;
					  xlv_TEXCOORD2 = tmpvar_5;
					  xlv_TEXCOORD2_1 = tmpvar_6;
					  xlv_TEXCOORD2_2 = tmpvar_7;
					  highp vec4 tmpvar_22;
					  tmpvar_22 = (unity_ObjectToWorld * _glesVertex);
					  xlv_TEXCOORD5 = (unity_WorldToLight * tmpvar_22).xyz;
					  xlv_TEXCOORD6 = (tmpvar_22.xyz - _LightPositionRange.xyz);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _LightPositionRange;
					uniform mediump vec4 _LightShadowData;
					uniform mediump vec4 unity_ColorSpaceDielectricSpec;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D unity_NHxRoughness;
					uniform mediump vec4 _Color;
					uniform sampler2D _MainTex;
					uniform mediump float _Metallic;
					uniform mediump float _Glossiness;
					uniform highp samplerCube _ShadowMapTexture;
					uniform lowp samplerCube _LightTexture0;
					uniform sampler2D _LightTextureB0;
					varying highp vec4 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec4 xlv_TEXCOORD2;
					varying mediump vec4 xlv_TEXCOORD2_1;
					varying mediump vec4 xlv_TEXCOORD2_2;
					varying highp vec3 xlv_TEXCOORD5;
					varying highp vec3 xlv_TEXCOORD6;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  mediump vec3 tmpvar_2;
					  tmpvar_2 = (_Color.xyz * tmpvar_1.xyz);
					  mediump vec3 tmpvar_3;
					  mediump vec3 tmpvar_4;
					  tmpvar_4 = mix (unity_ColorSpaceDielectricSpec.xyz, tmpvar_2, vec3(_Metallic));
					  tmpvar_3 = (tmpvar_2 * (unity_ColorSpaceDielectricSpec.w - (_Metallic * unity_ColorSpaceDielectricSpec.w)));
					  mediump vec3 tmpvar_5;
					  tmpvar_5 = normalize(xlv_TEXCOORD2_2.xyz);
					  mediump vec3 tmpvar_6;
					  tmpvar_6.x = xlv_TEXCOORD2.w;
					  tmpvar_6.y = xlv_TEXCOORD2_1.w;
					  tmpvar_6.z = xlv_TEXCOORD2_2.w;
					  highp float tmpvar_7;
					  tmpvar_7 = dot (xlv_TEXCOORD5, xlv_TEXCOORD5);
					  lowp vec4 tmpvar_8;
					  tmpvar_8 = texture2D (_LightTextureB0, vec2(tmpvar_7));
					  lowp vec4 tmpvar_9;
					  tmpvar_9 = textureCube (_LightTexture0, xlv_TEXCOORD5);
					  highp float mydist_10;
					  mydist_10 = ((sqrt(
					    dot (xlv_TEXCOORD6, xlv_TEXCOORD6)
					  ) * _LightPositionRange.w) * 0.97);
					  highp float tmpvar_11;
					  tmpvar_11 = dot (textureCube (_ShadowMapTexture, xlv_TEXCOORD6), vec4(1.0, 0.003921569, 1.53787e-05, 6.030863e-08));
					  mediump float tmpvar_12;
					  if ((tmpvar_11 < mydist_10)) {
					    tmpvar_12 = _LightShadowData.x;
					  } else {
					    tmpvar_12 = 1.0;
					  };
					  mediump vec3 tmpvar_13;
					  tmpvar_13 = _LightColor0.xyz;
					  tmpvar_13 = (tmpvar_13 * ((tmpvar_8.w * tmpvar_9.w) * tmpvar_12));
					  mediump vec3 viewDir_14;
					  viewDir_14 = -(xlv_TEXCOORD1);
					  mediump vec2 tmpvar_15;
					  tmpvar_15.x = dot ((viewDir_14 - (2.0 * 
					    (dot (tmpvar_5, viewDir_14) * tmpvar_5)
					  )), tmpvar_6);
					  tmpvar_15.y = (1.0 - clamp (dot (tmpvar_5, viewDir_14), 0.0, 1.0));
					  mediump vec2 tmpvar_16;
					  tmpvar_16.x = ((tmpvar_15 * tmpvar_15) * (tmpvar_15 * tmpvar_15)).x;
					  tmpvar_16.y = (1.0 - _Glossiness);
					  lowp vec4 tmpvar_17;
					  tmpvar_17 = texture2D (unity_NHxRoughness, tmpvar_16);
					  mediump vec4 tmpvar_18;
					  tmpvar_18.w = 1.0;
					  tmpvar_18.xyz = ((tmpvar_3 + (
					    (tmpvar_17.w * 16.0)
					   * tmpvar_4)) * (tmpvar_13 * clamp (
					    dot (tmpvar_5, tmpvar_6)
					  , 0.0, 1.0)));
					  mediump vec4 xlat_varoutput_19;
					  xlat_varoutput_19.xyz = tmpvar_18.xyz;
					  xlat_varoutput_19.w = 1.0;
					  gl_FragData[0] = xlat_varoutput_19;
					}
					
					
					#endif
}
}
Program "fp" {
SubProgram "gles hw_tier01 " {
Keywords { "POINT" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "POINT" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "POINT" }
					
}
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL" }
					
}
SubProgram "gles hw_tier01 " {
Keywords { "SPOT" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "SPOT" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "SPOT" }
					
}
SubProgram "gles hw_tier01 " {
Keywords { "POINT_COOKIE" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "POINT_COOKIE" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "POINT_COOKIE" }
					
}
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL_COOKIE" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL_COOKIE" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL_COOKIE" }
					
}
SubProgram "gles hw_tier01 " {
Keywords { "SPOT" "SHADOWS_DEPTH" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "SPOT" "SHADOWS_DEPTH" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "SPOT" "SHADOWS_DEPTH" }
					
}
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
					
}
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" }
					
}
SubProgram "gles hw_tier01 " {
Keywords { "POINT" "SHADOWS_CUBE" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "POINT" "SHADOWS_CUBE" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "POINT" "SHADOWS_CUBE" }
					
}
SubProgram "gles hw_tier01 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" }
					
}
}
 }
 Pass {
  Name "SHADOWCASTER"
  Tags { "LIGHTMODE"="SHADOWCASTER" "SHADOWSUPPORT"="true" "RenderType"="Opaque" "PerformanceChecks"="False" }
  GpuProgramID 515176
Program "vp" {
SubProgram "gles hw_tier01 " {
Keywords { "SHADOWS_DEPTH" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp vec4 unity_LightShadowBias;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp mat4 unity_MatrixVP;
					void main ()
					{
					  highp vec3 vertex_1;
					  vertex_1 = _glesVertex.xyz;
					  highp vec4 clipPos_2;
					  if ((unity_LightShadowBias.z != 0.0)) {
					    highp vec4 tmpvar_3;
					    tmpvar_3.w = 1.0;
					    tmpvar_3.xyz = vertex_1;
					    highp vec3 tmpvar_4;
					    tmpvar_4 = (unity_ObjectToWorld * tmpvar_3).xyz;
					    highp vec4 v_5;
					    v_5.x = unity_WorldToObject[0].x;
					    v_5.y = unity_WorldToObject[1].x;
					    v_5.z = unity_WorldToObject[2].x;
					    v_5.w = unity_WorldToObject[3].x;
					    highp vec4 v_6;
					    v_6.x = unity_WorldToObject[0].y;
					    v_6.y = unity_WorldToObject[1].y;
					    v_6.z = unity_WorldToObject[2].y;
					    v_6.w = unity_WorldToObject[3].y;
					    highp vec4 v_7;
					    v_7.x = unity_WorldToObject[0].z;
					    v_7.y = unity_WorldToObject[1].z;
					    v_7.z = unity_WorldToObject[2].z;
					    v_7.w = unity_WorldToObject[3].z;
					    highp vec3 tmpvar_8;
					    tmpvar_8 = normalize(((
					      (v_5.xyz * _glesNormal.x)
					     + 
					      (v_6.xyz * _glesNormal.y)
					    ) + (v_7.xyz * _glesNormal.z)));
					    highp float tmpvar_9;
					    tmpvar_9 = dot (tmpvar_8, normalize((_WorldSpaceLightPos0.xyz - 
					      (tmpvar_4 * _WorldSpaceLightPos0.w)
					    )));
					    highp vec4 tmpvar_10;
					    tmpvar_10.w = 1.0;
					    tmpvar_10.xyz = (tmpvar_4 - (tmpvar_8 * (unity_LightShadowBias.z * 
					      sqrt((1.0 - (tmpvar_9 * tmpvar_9)))
					    )));
					    clipPos_2 = (unity_MatrixVP * tmpvar_10);
					  } else {
					    highp vec4 tmpvar_11;
					    tmpvar_11.w = 1.0;
					    tmpvar_11.xyz = vertex_1;
					    clipPos_2 = (glstate_matrix_mvp * tmpvar_11);
					  };
					  highp vec4 clipPos_12;
					  clipPos_12.xyw = clipPos_2.xyw;
					  clipPos_12.z = (clipPos_2.z + clamp ((unity_LightShadowBias.x / clipPos_2.w), 0.0, 1.0));
					  clipPos_12.z = mix (clipPos_12.z, max (clipPos_12.z, -(clipPos_2.w)), unity_LightShadowBias.y);
					  gl_Position = clipPos_12;
					}
					
					
					#endif
					#ifdef FRAGMENT
					void main ()
					{
					  gl_FragData[0] = vec4(0.0, 0.0, 0.0, 0.0);
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "SHADOWS_DEPTH" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp vec4 unity_LightShadowBias;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp mat4 unity_MatrixVP;
					void main ()
					{
					  highp vec3 vertex_1;
					  vertex_1 = _glesVertex.xyz;
					  highp vec4 clipPos_2;
					  if ((unity_LightShadowBias.z != 0.0)) {
					    highp vec4 tmpvar_3;
					    tmpvar_3.w = 1.0;
					    tmpvar_3.xyz = vertex_1;
					    highp vec3 tmpvar_4;
					    tmpvar_4 = (unity_ObjectToWorld * tmpvar_3).xyz;
					    highp vec4 v_5;
					    v_5.x = unity_WorldToObject[0].x;
					    v_5.y = unity_WorldToObject[1].x;
					    v_5.z = unity_WorldToObject[2].x;
					    v_5.w = unity_WorldToObject[3].x;
					    highp vec4 v_6;
					    v_6.x = unity_WorldToObject[0].y;
					    v_6.y = unity_WorldToObject[1].y;
					    v_6.z = unity_WorldToObject[2].y;
					    v_6.w = unity_WorldToObject[3].y;
					    highp vec4 v_7;
					    v_7.x = unity_WorldToObject[0].z;
					    v_7.y = unity_WorldToObject[1].z;
					    v_7.z = unity_WorldToObject[2].z;
					    v_7.w = unity_WorldToObject[3].z;
					    highp vec3 tmpvar_8;
					    tmpvar_8 = normalize(((
					      (v_5.xyz * _glesNormal.x)
					     + 
					      (v_6.xyz * _glesNormal.y)
					    ) + (v_7.xyz * _glesNormal.z)));
					    highp float tmpvar_9;
					    tmpvar_9 = dot (tmpvar_8, normalize((_WorldSpaceLightPos0.xyz - 
					      (tmpvar_4 * _WorldSpaceLightPos0.w)
					    )));
					    highp vec4 tmpvar_10;
					    tmpvar_10.w = 1.0;
					    tmpvar_10.xyz = (tmpvar_4 - (tmpvar_8 * (unity_LightShadowBias.z * 
					      sqrt((1.0 - (tmpvar_9 * tmpvar_9)))
					    )));
					    clipPos_2 = (unity_MatrixVP * tmpvar_10);
					  } else {
					    highp vec4 tmpvar_11;
					    tmpvar_11.w = 1.0;
					    tmpvar_11.xyz = vertex_1;
					    clipPos_2 = (glstate_matrix_mvp * tmpvar_11);
					  };
					  highp vec4 clipPos_12;
					  clipPos_12.xyw = clipPos_2.xyw;
					  clipPos_12.z = (clipPos_2.z + clamp ((unity_LightShadowBias.x / clipPos_2.w), 0.0, 1.0));
					  clipPos_12.z = mix (clipPos_12.z, max (clipPos_12.z, -(clipPos_2.w)), unity_LightShadowBias.y);
					  gl_Position = clipPos_12;
					}
					
					
					#endif
					#ifdef FRAGMENT
					void main ()
					{
					  gl_FragData[0] = vec4(0.0, 0.0, 0.0, 0.0);
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "SHADOWS_DEPTH" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform highp vec4 unity_LightShadowBias;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp mat4 unity_MatrixVP;
					void main ()
					{
					  highp vec3 vertex_1;
					  vertex_1 = _glesVertex.xyz;
					  highp vec4 clipPos_2;
					  if ((unity_LightShadowBias.z != 0.0)) {
					    highp vec4 tmpvar_3;
					    tmpvar_3.w = 1.0;
					    tmpvar_3.xyz = vertex_1;
					    highp vec3 tmpvar_4;
					    tmpvar_4 = (unity_ObjectToWorld * tmpvar_3).xyz;
					    highp vec4 v_5;
					    v_5.x = unity_WorldToObject[0].x;
					    v_5.y = unity_WorldToObject[1].x;
					    v_5.z = unity_WorldToObject[2].x;
					    v_5.w = unity_WorldToObject[3].x;
					    highp vec4 v_6;
					    v_6.x = unity_WorldToObject[0].y;
					    v_6.y = unity_WorldToObject[1].y;
					    v_6.z = unity_WorldToObject[2].y;
					    v_6.w = unity_WorldToObject[3].y;
					    highp vec4 v_7;
					    v_7.x = unity_WorldToObject[0].z;
					    v_7.y = unity_WorldToObject[1].z;
					    v_7.z = unity_WorldToObject[2].z;
					    v_7.w = unity_WorldToObject[3].z;
					    highp vec3 tmpvar_8;
					    tmpvar_8 = normalize(((
					      (v_5.xyz * _glesNormal.x)
					     + 
					      (v_6.xyz * _glesNormal.y)
					    ) + (v_7.xyz * _glesNormal.z)));
					    highp float tmpvar_9;
					    tmpvar_9 = dot (tmpvar_8, normalize((_WorldSpaceLightPos0.xyz - 
					      (tmpvar_4 * _WorldSpaceLightPos0.w)
					    )));
					    highp vec4 tmpvar_10;
					    tmpvar_10.w = 1.0;
					    tmpvar_10.xyz = (tmpvar_4 - (tmpvar_8 * (unity_LightShadowBias.z * 
					      sqrt((1.0 - (tmpvar_9 * tmpvar_9)))
					    )));
					    clipPos_2 = (unity_MatrixVP * tmpvar_10);
					  } else {
					    highp vec4 tmpvar_11;
					    tmpvar_11.w = 1.0;
					    tmpvar_11.xyz = vertex_1;
					    clipPos_2 = (glstate_matrix_mvp * tmpvar_11);
					  };
					  highp vec4 clipPos_12;
					  clipPos_12.xyw = clipPos_2.xyw;
					  clipPos_12.z = (clipPos_2.z + clamp ((unity_LightShadowBias.x / clipPos_2.w), 0.0, 1.0));
					  clipPos_12.z = mix (clipPos_12.z, max (clipPos_12.z, -(clipPos_2.w)), unity_LightShadowBias.y);
					  gl_Position = clipPos_12;
					}
					
					
					#endif
					#ifdef FRAGMENT
					void main ()
					{
					  gl_FragData[0] = vec4(0.0, 0.0, 0.0, 0.0);
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "SHADOWS_CUBE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					uniform highp vec4 _LightPositionRange;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					varying highp vec3 xlv_TEXCOORD0;
					void main ()
					{
					  highp vec4 tmpvar_1;
					  tmpvar_1.w = 1.0;
					  tmpvar_1.xyz = _glesVertex.xyz;
					  xlv_TEXCOORD0 = ((unity_ObjectToWorld * _glesVertex).xyz - _LightPositionRange.xyz);
					  gl_Position = (glstate_matrix_mvp * tmpvar_1);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _LightPositionRange;
					uniform highp vec4 unity_LightShadowBias;
					varying highp vec3 xlv_TEXCOORD0;
					void main ()
					{
					  mediump vec4 tmpvar_1;
					  highp vec4 enc_2;
					  highp vec4 tmpvar_3;
					  tmpvar_3 = fract((vec4(1.0, 255.0, 65025.0, 1.658138e+07) * min (
					    ((sqrt(dot (xlv_TEXCOORD0, xlv_TEXCOORD0)) + unity_LightShadowBias.x) * _LightPositionRange.w)
					  , 0.999)));
					  enc_2 = (tmpvar_3 - (tmpvar_3.yzww * 0.003921569));
					  tmpvar_1 = enc_2;
					  gl_FragData[0] = tmpvar_1;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "SHADOWS_CUBE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					uniform highp vec4 _LightPositionRange;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					varying highp vec3 xlv_TEXCOORD0;
					void main ()
					{
					  highp vec4 tmpvar_1;
					  tmpvar_1.w = 1.0;
					  tmpvar_1.xyz = _glesVertex.xyz;
					  xlv_TEXCOORD0 = ((unity_ObjectToWorld * _glesVertex).xyz - _LightPositionRange.xyz);
					  gl_Position = (glstate_matrix_mvp * tmpvar_1);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _LightPositionRange;
					uniform highp vec4 unity_LightShadowBias;
					varying highp vec3 xlv_TEXCOORD0;
					void main ()
					{
					  mediump vec4 tmpvar_1;
					  highp vec4 enc_2;
					  highp vec4 tmpvar_3;
					  tmpvar_3 = fract((vec4(1.0, 255.0, 65025.0, 1.658138e+07) * min (
					    ((sqrt(dot (xlv_TEXCOORD0, xlv_TEXCOORD0)) + unity_LightShadowBias.x) * _LightPositionRange.w)
					  , 0.999)));
					  enc_2 = (tmpvar_3 - (tmpvar_3.yzww * 0.003921569));
					  tmpvar_1 = enc_2;
					  gl_FragData[0] = tmpvar_1;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "SHADOWS_CUBE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					uniform highp vec4 _LightPositionRange;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					varying highp vec3 xlv_TEXCOORD0;
					void main ()
					{
					  highp vec4 tmpvar_1;
					  tmpvar_1.w = 1.0;
					  tmpvar_1.xyz = _glesVertex.xyz;
					  xlv_TEXCOORD0 = ((unity_ObjectToWorld * _glesVertex).xyz - _LightPositionRange.xyz);
					  gl_Position = (glstate_matrix_mvp * tmpvar_1);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _LightPositionRange;
					uniform highp vec4 unity_LightShadowBias;
					varying highp vec3 xlv_TEXCOORD0;
					void main ()
					{
					  mediump vec4 tmpvar_1;
					  highp vec4 enc_2;
					  highp vec4 tmpvar_3;
					  tmpvar_3 = fract((vec4(1.0, 255.0, 65025.0, 1.658138e+07) * min (
					    ((sqrt(dot (xlv_TEXCOORD0, xlv_TEXCOORD0)) + unity_LightShadowBias.x) * _LightPositionRange.w)
					  , 0.999)));
					  enc_2 = (tmpvar_3 - (tmpvar_3.yzww * 0.003921569));
					  tmpvar_1 = enc_2;
					  gl_FragData[0] = tmpvar_1;
					}
					
					
					#endif
}
}
Program "fp" {
SubProgram "gles hw_tier01 " {
Keywords { "SHADOWS_DEPTH" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "SHADOWS_DEPTH" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "SHADOWS_DEPTH" }
					
}
SubProgram "gles hw_tier01 " {
Keywords { "SHADOWS_CUBE" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "SHADOWS_CUBE" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "SHADOWS_CUBE" }
					
}
}
 }
}
CustomEditor "StandardShaderGUI"
Fallback "VertexLit"
}