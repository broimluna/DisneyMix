Shader "Bumped Diffuse (unpacked)" {
Properties {
 _Color ("Fade Color", Color) = (1.000000,1.000000,1.000000,1.000000)
 _MainTex ("Base (RGB)", 2D) = "white" { }
 _BumpMap ("Normalmap", 2D) = "bump" { }
 _Inv ("Invert Normal", Range(0.000000,1.000000)) = 0.000000
 _Fade ("Fade", Range(0.000000,1.000000)) = 0.000000
}
SubShader { 
 LOD 300
 Tags { "RenderType"="Opaque" }
 Pass {
  Name "FORWARD"
  Tags { "LIGHTMODE"="ForwardBase" "SHADOWSUPPORT"="true" "RenderType"="Opaque" }
  GpuProgramID 38685
Program "vp" {
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesTANGENT;
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
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
					uniform highp vec4 unity_WorldTransformParams;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _BumpMap_ST;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					void main ()
					{
					  lowp float tangentSign_1;
					  lowp vec3 worldTangent_2;
					  lowp vec3 worldNormal_3;
					  highp vec4 tmpvar_4;
					  highp vec4 tmpvar_5;
					  tmpvar_5.w = 1.0;
					  tmpvar_5.xyz = _glesVertex.xyz;
					  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
					  highp vec3 tmpvar_6;
					  tmpvar_6 = (unity_ObjectToWorld * _glesVertex).xyz;
					  highp vec4 v_7;
					  v_7.x = unity_WorldToObject[0].x;
					  v_7.y = unity_WorldToObject[1].x;
					  v_7.z = unity_WorldToObject[2].x;
					  v_7.w = unity_WorldToObject[3].x;
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].y;
					  v_8.y = unity_WorldToObject[1].y;
					  v_8.z = unity_WorldToObject[2].y;
					  v_8.w = unity_WorldToObject[3].y;
					  highp vec4 v_9;
					  v_9.x = unity_WorldToObject[0].z;
					  v_9.y = unity_WorldToObject[1].z;
					  v_9.z = unity_WorldToObject[2].z;
					  v_9.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_10;
					  tmpvar_10 = normalize(((
					    (v_7.xyz * _glesNormal.x)
					   + 
					    (v_8.xyz * _glesNormal.y)
					  ) + (v_9.xyz * _glesNormal.z)));
					  worldNormal_3 = tmpvar_10;
					  highp mat3 tmpvar_11;
					  tmpvar_11[0] = unity_ObjectToWorld[0].xyz;
					  tmpvar_11[1] = unity_ObjectToWorld[1].xyz;
					  tmpvar_11[2] = unity_ObjectToWorld[2].xyz;
					  highp vec3 tmpvar_12;
					  tmpvar_12 = normalize((tmpvar_11 * _glesTANGENT.xyz));
					  worldTangent_2 = tmpvar_12;
					  highp float tmpvar_13;
					  tmpvar_13 = (_glesTANGENT.w * unity_WorldTransformParams.w);
					  tangentSign_1 = tmpvar_13;
					  lowp vec3 tmpvar_14;
					  tmpvar_14 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
					  highp vec4 tmpvar_15;
					  tmpvar_15.x = worldTangent_2.x;
					  tmpvar_15.y = tmpvar_14.x;
					  tmpvar_15.z = worldNormal_3.x;
					  tmpvar_15.w = tmpvar_6.x;
					  highp vec4 tmpvar_16;
					  tmpvar_16.x = worldTangent_2.y;
					  tmpvar_16.y = tmpvar_14.y;
					  tmpvar_16.z = worldNormal_3.y;
					  tmpvar_16.w = tmpvar_6.y;
					  highp vec4 tmpvar_17;
					  tmpvar_17.x = worldTangent_2.z;
					  tmpvar_17.y = tmpvar_14.z;
					  tmpvar_17.z = worldNormal_3.z;
					  tmpvar_17.w = tmpvar_6.z;
					  mediump vec3 normal_18;
					  normal_18 = worldNormal_3;
					  mediump vec4 tmpvar_19;
					  tmpvar_19.w = 1.0;
					  tmpvar_19.xyz = normal_18;
					  mediump vec3 res_20;
					  mediump vec3 x_21;
					  x_21.x = dot (unity_SHAr, tmpvar_19);
					  x_21.y = dot (unity_SHAg, tmpvar_19);
					  x_21.z = dot (unity_SHAb, tmpvar_19);
					  mediump vec3 x1_22;
					  mediump vec4 tmpvar_23;
					  tmpvar_23 = (normal_18.xyzz * normal_18.yzzx);
					  x1_22.x = dot (unity_SHBr, tmpvar_23);
					  x1_22.y = dot (unity_SHBg, tmpvar_23);
					  x1_22.z = dot (unity_SHBb, tmpvar_23);
					  res_20 = (x_21 + (x1_22 + (unity_SHC.xyz * 
					    ((normal_18.x * normal_18.x) - (normal_18.y * normal_18.y))
					  )));
					  res_20 = max (((1.055 * 
					    pow (max (res_20, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  gl_Position = (glstate_matrix_mvp * tmpvar_5);
					  xlv_TEXCOORD0 = tmpvar_4;
					  xlv_TEXCOORD1 = tmpvar_15;
					  xlv_TEXCOORD2 = tmpvar_16;
					  xlv_TEXCOORD3 = tmpvar_17;
					  xlv_TEXCOORD4 = max (vec3(0.0, 0.0, 0.0), res_20);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  mediump vec3 tmpvar_2;
					  lowp vec3 worldN_3;
					  lowp vec4 c_4;
					  lowp vec3 tmpvar_5;
					  lowp vec3 lightDir_6;
					  mediump vec3 tmpvar_7;
					  tmpvar_7 = _WorldSpaceLightPos0.xyz;
					  lightDir_6 = tmpvar_7;
					  lowp vec3 tmpvar_8;
					  lowp vec3 tmpvar_9;
					  lowp float tmpvar_10;
					  tmpvar_9 = tmpvar_5;
					  highp vec4 tempNorm_11;
					  lowp vec4 tmpvar_12;
					  tmpvar_12 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  lowp vec4 tmpvar_13;
					  tmpvar_13.w = 0.0;
					  tmpvar_13.xyz = tmpvar_12.xyz;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = mix (tmpvar_13, _Color, vec4(_Fade)).xyz;
					  tmpvar_8 = tmpvar_14;
					  tmpvar_10 = tmpvar_12.w;
					  lowp vec4 tmpvar_15;
					  tmpvar_15 = texture2D (_BumpMap, xlv_TEXCOORD0.zw);
					  tempNorm_11 = tmpvar_15;
					  if ((_Inv == 1.0)) {
					    tempNorm_11.xy = (vec2(1.0, 1.0) - tempNorm_11.xy);
					  };
					  highp vec3 tmpvar_16;
					  tmpvar_16 = ((tempNorm_11 * 2.0) - 1.0).xyz;
					  tmpvar_9 = tmpvar_16;
					  highp float tmpvar_17;
					  tmpvar_17 = dot (xlv_TEXCOORD1.xyz, tmpvar_9);
					  worldN_3.x = tmpvar_17;
					  highp float tmpvar_18;
					  tmpvar_18 = dot (xlv_TEXCOORD2.xyz, tmpvar_9);
					  worldN_3.y = tmpvar_18;
					  highp float tmpvar_19;
					  tmpvar_19 = dot (xlv_TEXCOORD3.xyz, tmpvar_9);
					  worldN_3.z = tmpvar_19;
					  tmpvar_5 = worldN_3;
					  tmpvar_1 = _LightColor0.xyz;
					  tmpvar_2 = lightDir_6;
					  lowp vec4 c_20;
					  lowp vec4 c_21;
					  lowp float diff_22;
					  mediump float tmpvar_23;
					  tmpvar_23 = max (0.0, dot (worldN_3, tmpvar_2));
					  diff_22 = tmpvar_23;
					  c_21.xyz = ((tmpvar_8 * tmpvar_1) * diff_22);
					  c_21.w = tmpvar_10;
					  c_20.w = c_21.w;
					  c_20.xyz = (c_21.xyz + (tmpvar_8 * xlv_TEXCOORD4));
					  c_4.xyz = c_20.xyz;
					  c_4.w = 1.0;
					  gl_FragData[0] = c_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesTANGENT;
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
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
					uniform highp vec4 unity_WorldTransformParams;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _BumpMap_ST;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					void main ()
					{
					  lowp float tangentSign_1;
					  lowp vec3 worldTangent_2;
					  lowp vec3 worldNormal_3;
					  highp vec4 tmpvar_4;
					  highp vec4 tmpvar_5;
					  tmpvar_5.w = 1.0;
					  tmpvar_5.xyz = _glesVertex.xyz;
					  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
					  highp vec3 tmpvar_6;
					  tmpvar_6 = (unity_ObjectToWorld * _glesVertex).xyz;
					  highp vec4 v_7;
					  v_7.x = unity_WorldToObject[0].x;
					  v_7.y = unity_WorldToObject[1].x;
					  v_7.z = unity_WorldToObject[2].x;
					  v_7.w = unity_WorldToObject[3].x;
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].y;
					  v_8.y = unity_WorldToObject[1].y;
					  v_8.z = unity_WorldToObject[2].y;
					  v_8.w = unity_WorldToObject[3].y;
					  highp vec4 v_9;
					  v_9.x = unity_WorldToObject[0].z;
					  v_9.y = unity_WorldToObject[1].z;
					  v_9.z = unity_WorldToObject[2].z;
					  v_9.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_10;
					  tmpvar_10 = normalize(((
					    (v_7.xyz * _glesNormal.x)
					   + 
					    (v_8.xyz * _glesNormal.y)
					  ) + (v_9.xyz * _glesNormal.z)));
					  worldNormal_3 = tmpvar_10;
					  highp mat3 tmpvar_11;
					  tmpvar_11[0] = unity_ObjectToWorld[0].xyz;
					  tmpvar_11[1] = unity_ObjectToWorld[1].xyz;
					  tmpvar_11[2] = unity_ObjectToWorld[2].xyz;
					  highp vec3 tmpvar_12;
					  tmpvar_12 = normalize((tmpvar_11 * _glesTANGENT.xyz));
					  worldTangent_2 = tmpvar_12;
					  highp float tmpvar_13;
					  tmpvar_13 = (_glesTANGENT.w * unity_WorldTransformParams.w);
					  tangentSign_1 = tmpvar_13;
					  lowp vec3 tmpvar_14;
					  tmpvar_14 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
					  highp vec4 tmpvar_15;
					  tmpvar_15.x = worldTangent_2.x;
					  tmpvar_15.y = tmpvar_14.x;
					  tmpvar_15.z = worldNormal_3.x;
					  tmpvar_15.w = tmpvar_6.x;
					  highp vec4 tmpvar_16;
					  tmpvar_16.x = worldTangent_2.y;
					  tmpvar_16.y = tmpvar_14.y;
					  tmpvar_16.z = worldNormal_3.y;
					  tmpvar_16.w = tmpvar_6.y;
					  highp vec4 tmpvar_17;
					  tmpvar_17.x = worldTangent_2.z;
					  tmpvar_17.y = tmpvar_14.z;
					  tmpvar_17.z = worldNormal_3.z;
					  tmpvar_17.w = tmpvar_6.z;
					  mediump vec3 normal_18;
					  normal_18 = worldNormal_3;
					  mediump vec4 tmpvar_19;
					  tmpvar_19.w = 1.0;
					  tmpvar_19.xyz = normal_18;
					  mediump vec3 res_20;
					  mediump vec3 x_21;
					  x_21.x = dot (unity_SHAr, tmpvar_19);
					  x_21.y = dot (unity_SHAg, tmpvar_19);
					  x_21.z = dot (unity_SHAb, tmpvar_19);
					  mediump vec3 x1_22;
					  mediump vec4 tmpvar_23;
					  tmpvar_23 = (normal_18.xyzz * normal_18.yzzx);
					  x1_22.x = dot (unity_SHBr, tmpvar_23);
					  x1_22.y = dot (unity_SHBg, tmpvar_23);
					  x1_22.z = dot (unity_SHBb, tmpvar_23);
					  res_20 = (x_21 + (x1_22 + (unity_SHC.xyz * 
					    ((normal_18.x * normal_18.x) - (normal_18.y * normal_18.y))
					  )));
					  res_20 = max (((1.055 * 
					    pow (max (res_20, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  gl_Position = (glstate_matrix_mvp * tmpvar_5);
					  xlv_TEXCOORD0 = tmpvar_4;
					  xlv_TEXCOORD1 = tmpvar_15;
					  xlv_TEXCOORD2 = tmpvar_16;
					  xlv_TEXCOORD3 = tmpvar_17;
					  xlv_TEXCOORD4 = max (vec3(0.0, 0.0, 0.0), res_20);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  mediump vec3 tmpvar_2;
					  lowp vec3 worldN_3;
					  lowp vec4 c_4;
					  lowp vec3 tmpvar_5;
					  lowp vec3 lightDir_6;
					  mediump vec3 tmpvar_7;
					  tmpvar_7 = _WorldSpaceLightPos0.xyz;
					  lightDir_6 = tmpvar_7;
					  lowp vec3 tmpvar_8;
					  lowp vec3 tmpvar_9;
					  lowp float tmpvar_10;
					  tmpvar_9 = tmpvar_5;
					  highp vec4 tempNorm_11;
					  lowp vec4 tmpvar_12;
					  tmpvar_12 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  lowp vec4 tmpvar_13;
					  tmpvar_13.w = 0.0;
					  tmpvar_13.xyz = tmpvar_12.xyz;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = mix (tmpvar_13, _Color, vec4(_Fade)).xyz;
					  tmpvar_8 = tmpvar_14;
					  tmpvar_10 = tmpvar_12.w;
					  lowp vec4 tmpvar_15;
					  tmpvar_15 = texture2D (_BumpMap, xlv_TEXCOORD0.zw);
					  tempNorm_11 = tmpvar_15;
					  if ((_Inv == 1.0)) {
					    tempNorm_11.xy = (vec2(1.0, 1.0) - tempNorm_11.xy);
					  };
					  highp vec3 tmpvar_16;
					  tmpvar_16 = ((tempNorm_11 * 2.0) - 1.0).xyz;
					  tmpvar_9 = tmpvar_16;
					  highp float tmpvar_17;
					  tmpvar_17 = dot (xlv_TEXCOORD1.xyz, tmpvar_9);
					  worldN_3.x = tmpvar_17;
					  highp float tmpvar_18;
					  tmpvar_18 = dot (xlv_TEXCOORD2.xyz, tmpvar_9);
					  worldN_3.y = tmpvar_18;
					  highp float tmpvar_19;
					  tmpvar_19 = dot (xlv_TEXCOORD3.xyz, tmpvar_9);
					  worldN_3.z = tmpvar_19;
					  tmpvar_5 = worldN_3;
					  tmpvar_1 = _LightColor0.xyz;
					  tmpvar_2 = lightDir_6;
					  lowp vec4 c_20;
					  lowp vec4 c_21;
					  lowp float diff_22;
					  mediump float tmpvar_23;
					  tmpvar_23 = max (0.0, dot (worldN_3, tmpvar_2));
					  diff_22 = tmpvar_23;
					  c_21.xyz = ((tmpvar_8 * tmpvar_1) * diff_22);
					  c_21.w = tmpvar_10;
					  c_20.w = c_21.w;
					  c_20.xyz = (c_21.xyz + (tmpvar_8 * xlv_TEXCOORD4));
					  c_4.xyz = c_20.xyz;
					  c_4.w = 1.0;
					  gl_FragData[0] = c_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesTANGENT;
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
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
					uniform highp vec4 unity_WorldTransformParams;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _BumpMap_ST;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					void main ()
					{
					  lowp float tangentSign_1;
					  lowp vec3 worldTangent_2;
					  lowp vec3 worldNormal_3;
					  highp vec4 tmpvar_4;
					  highp vec4 tmpvar_5;
					  tmpvar_5.w = 1.0;
					  tmpvar_5.xyz = _glesVertex.xyz;
					  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
					  highp vec3 tmpvar_6;
					  tmpvar_6 = (unity_ObjectToWorld * _glesVertex).xyz;
					  highp vec4 v_7;
					  v_7.x = unity_WorldToObject[0].x;
					  v_7.y = unity_WorldToObject[1].x;
					  v_7.z = unity_WorldToObject[2].x;
					  v_7.w = unity_WorldToObject[3].x;
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].y;
					  v_8.y = unity_WorldToObject[1].y;
					  v_8.z = unity_WorldToObject[2].y;
					  v_8.w = unity_WorldToObject[3].y;
					  highp vec4 v_9;
					  v_9.x = unity_WorldToObject[0].z;
					  v_9.y = unity_WorldToObject[1].z;
					  v_9.z = unity_WorldToObject[2].z;
					  v_9.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_10;
					  tmpvar_10 = normalize(((
					    (v_7.xyz * _glesNormal.x)
					   + 
					    (v_8.xyz * _glesNormal.y)
					  ) + (v_9.xyz * _glesNormal.z)));
					  worldNormal_3 = tmpvar_10;
					  highp mat3 tmpvar_11;
					  tmpvar_11[0] = unity_ObjectToWorld[0].xyz;
					  tmpvar_11[1] = unity_ObjectToWorld[1].xyz;
					  tmpvar_11[2] = unity_ObjectToWorld[2].xyz;
					  highp vec3 tmpvar_12;
					  tmpvar_12 = normalize((tmpvar_11 * _glesTANGENT.xyz));
					  worldTangent_2 = tmpvar_12;
					  highp float tmpvar_13;
					  tmpvar_13 = (_glesTANGENT.w * unity_WorldTransformParams.w);
					  tangentSign_1 = tmpvar_13;
					  lowp vec3 tmpvar_14;
					  tmpvar_14 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
					  highp vec4 tmpvar_15;
					  tmpvar_15.x = worldTangent_2.x;
					  tmpvar_15.y = tmpvar_14.x;
					  tmpvar_15.z = worldNormal_3.x;
					  tmpvar_15.w = tmpvar_6.x;
					  highp vec4 tmpvar_16;
					  tmpvar_16.x = worldTangent_2.y;
					  tmpvar_16.y = tmpvar_14.y;
					  tmpvar_16.z = worldNormal_3.y;
					  tmpvar_16.w = tmpvar_6.y;
					  highp vec4 tmpvar_17;
					  tmpvar_17.x = worldTangent_2.z;
					  tmpvar_17.y = tmpvar_14.z;
					  tmpvar_17.z = worldNormal_3.z;
					  tmpvar_17.w = tmpvar_6.z;
					  mediump vec3 normal_18;
					  normal_18 = worldNormal_3;
					  mediump vec4 tmpvar_19;
					  tmpvar_19.w = 1.0;
					  tmpvar_19.xyz = normal_18;
					  mediump vec3 res_20;
					  mediump vec3 x_21;
					  x_21.x = dot (unity_SHAr, tmpvar_19);
					  x_21.y = dot (unity_SHAg, tmpvar_19);
					  x_21.z = dot (unity_SHAb, tmpvar_19);
					  mediump vec3 x1_22;
					  mediump vec4 tmpvar_23;
					  tmpvar_23 = (normal_18.xyzz * normal_18.yzzx);
					  x1_22.x = dot (unity_SHBr, tmpvar_23);
					  x1_22.y = dot (unity_SHBg, tmpvar_23);
					  x1_22.z = dot (unity_SHBb, tmpvar_23);
					  res_20 = (x_21 + (x1_22 + (unity_SHC.xyz * 
					    ((normal_18.x * normal_18.x) - (normal_18.y * normal_18.y))
					  )));
					  res_20 = max (((1.055 * 
					    pow (max (res_20, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  gl_Position = (glstate_matrix_mvp * tmpvar_5);
					  xlv_TEXCOORD0 = tmpvar_4;
					  xlv_TEXCOORD1 = tmpvar_15;
					  xlv_TEXCOORD2 = tmpvar_16;
					  xlv_TEXCOORD3 = tmpvar_17;
					  xlv_TEXCOORD4 = max (vec3(0.0, 0.0, 0.0), res_20);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  mediump vec3 tmpvar_2;
					  lowp vec3 worldN_3;
					  lowp vec4 c_4;
					  lowp vec3 tmpvar_5;
					  lowp vec3 lightDir_6;
					  mediump vec3 tmpvar_7;
					  tmpvar_7 = _WorldSpaceLightPos0.xyz;
					  lightDir_6 = tmpvar_7;
					  lowp vec3 tmpvar_8;
					  lowp vec3 tmpvar_9;
					  lowp float tmpvar_10;
					  tmpvar_9 = tmpvar_5;
					  highp vec4 tempNorm_11;
					  lowp vec4 tmpvar_12;
					  tmpvar_12 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  lowp vec4 tmpvar_13;
					  tmpvar_13.w = 0.0;
					  tmpvar_13.xyz = tmpvar_12.xyz;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = mix (tmpvar_13, _Color, vec4(_Fade)).xyz;
					  tmpvar_8 = tmpvar_14;
					  tmpvar_10 = tmpvar_12.w;
					  lowp vec4 tmpvar_15;
					  tmpvar_15 = texture2D (_BumpMap, xlv_TEXCOORD0.zw);
					  tempNorm_11 = tmpvar_15;
					  if ((_Inv == 1.0)) {
					    tempNorm_11.xy = (vec2(1.0, 1.0) - tempNorm_11.xy);
					  };
					  highp vec3 tmpvar_16;
					  tmpvar_16 = ((tempNorm_11 * 2.0) - 1.0).xyz;
					  tmpvar_9 = tmpvar_16;
					  highp float tmpvar_17;
					  tmpvar_17 = dot (xlv_TEXCOORD1.xyz, tmpvar_9);
					  worldN_3.x = tmpvar_17;
					  highp float tmpvar_18;
					  tmpvar_18 = dot (xlv_TEXCOORD2.xyz, tmpvar_9);
					  worldN_3.y = tmpvar_18;
					  highp float tmpvar_19;
					  tmpvar_19 = dot (xlv_TEXCOORD3.xyz, tmpvar_9);
					  worldN_3.z = tmpvar_19;
					  tmpvar_5 = worldN_3;
					  tmpvar_1 = _LightColor0.xyz;
					  tmpvar_2 = lightDir_6;
					  lowp vec4 c_20;
					  lowp vec4 c_21;
					  lowp float diff_22;
					  mediump float tmpvar_23;
					  tmpvar_23 = max (0.0, dot (worldN_3, tmpvar_2));
					  diff_22 = tmpvar_23;
					  c_21.xyz = ((tmpvar_8 * tmpvar_1) * diff_22);
					  c_21.w = tmpvar_10;
					  c_20.w = c_21.w;
					  c_20.xyz = (c_21.xyz + (tmpvar_8 * xlv_TEXCOORD4));
					  c_4.xyz = c_20.xyz;
					  c_4.w = 1.0;
					  gl_FragData[0] = c_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesTANGENT;
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
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
					uniform highp vec4 unity_WorldTransformParams;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _BumpMap_ST;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying highp vec4 xlv_TEXCOORD5;
					void main ()
					{
					  lowp float tangentSign_1;
					  lowp vec3 worldTangent_2;
					  lowp vec3 worldNormal_3;
					  highp vec4 tmpvar_4;
					  highp vec4 tmpvar_5;
					  tmpvar_5.w = 1.0;
					  tmpvar_5.xyz = _glesVertex.xyz;
					  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
					  highp vec3 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  tmpvar_6 = tmpvar_7.xyz;
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].x;
					  v_8.y = unity_WorldToObject[1].x;
					  v_8.z = unity_WorldToObject[2].x;
					  v_8.w = unity_WorldToObject[3].x;
					  highp vec4 v_9;
					  v_9.x = unity_WorldToObject[0].y;
					  v_9.y = unity_WorldToObject[1].y;
					  v_9.z = unity_WorldToObject[2].y;
					  v_9.w = unity_WorldToObject[3].y;
					  highp vec4 v_10;
					  v_10.x = unity_WorldToObject[0].z;
					  v_10.y = unity_WorldToObject[1].z;
					  v_10.z = unity_WorldToObject[2].z;
					  v_10.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = normalize(((
					    (v_8.xyz * _glesNormal.x)
					   + 
					    (v_9.xyz * _glesNormal.y)
					  ) + (v_10.xyz * _glesNormal.z)));
					  worldNormal_3 = tmpvar_11;
					  highp mat3 tmpvar_12;
					  tmpvar_12[0] = unity_ObjectToWorld[0].xyz;
					  tmpvar_12[1] = unity_ObjectToWorld[1].xyz;
					  tmpvar_12[2] = unity_ObjectToWorld[2].xyz;
					  highp vec3 tmpvar_13;
					  tmpvar_13 = normalize((tmpvar_12 * _glesTANGENT.xyz));
					  worldTangent_2 = tmpvar_13;
					  highp float tmpvar_14;
					  tmpvar_14 = (_glesTANGENT.w * unity_WorldTransformParams.w);
					  tangentSign_1 = tmpvar_14;
					  lowp vec3 tmpvar_15;
					  tmpvar_15 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
					  highp vec4 tmpvar_16;
					  tmpvar_16.x = worldTangent_2.x;
					  tmpvar_16.y = tmpvar_15.x;
					  tmpvar_16.z = worldNormal_3.x;
					  tmpvar_16.w = tmpvar_6.x;
					  highp vec4 tmpvar_17;
					  tmpvar_17.x = worldTangent_2.y;
					  tmpvar_17.y = tmpvar_15.y;
					  tmpvar_17.z = worldNormal_3.y;
					  tmpvar_17.w = tmpvar_6.y;
					  highp vec4 tmpvar_18;
					  tmpvar_18.x = worldTangent_2.z;
					  tmpvar_18.y = tmpvar_15.z;
					  tmpvar_18.z = worldNormal_3.z;
					  tmpvar_18.w = tmpvar_6.z;
					  mediump vec3 normal_19;
					  normal_19 = worldNormal_3;
					  mediump vec4 tmpvar_20;
					  tmpvar_20.w = 1.0;
					  tmpvar_20.xyz = normal_19;
					  mediump vec3 res_21;
					  mediump vec3 x_22;
					  x_22.x = dot (unity_SHAr, tmpvar_20);
					  x_22.y = dot (unity_SHAg, tmpvar_20);
					  x_22.z = dot (unity_SHAb, tmpvar_20);
					  mediump vec3 x1_23;
					  mediump vec4 tmpvar_24;
					  tmpvar_24 = (normal_19.xyzz * normal_19.yzzx);
					  x1_23.x = dot (unity_SHBr, tmpvar_24);
					  x1_23.y = dot (unity_SHBg, tmpvar_24);
					  x1_23.z = dot (unity_SHBb, tmpvar_24);
					  res_21 = (x_22 + (x1_23 + (unity_SHC.xyz * 
					    ((normal_19.x * normal_19.x) - (normal_19.y * normal_19.y))
					  )));
					  res_21 = max (((1.055 * 
					    pow (max (res_21, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  gl_Position = (glstate_matrix_mvp * tmpvar_5);
					  xlv_TEXCOORD0 = tmpvar_4;
					  xlv_TEXCOORD1 = tmpvar_16;
					  xlv_TEXCOORD2 = tmpvar_17;
					  xlv_TEXCOORD3 = tmpvar_18;
					  xlv_TEXCOORD4 = max (vec3(0.0, 0.0, 0.0), res_21);
					  xlv_TEXCOORD5 = (unity_WorldToShadow[0] * tmpvar_7);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform mediump vec4 _LightShadowData;
					uniform lowp vec4 _LightColor0;
					uniform highp sampler2D _ShadowMapTexture;
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying highp vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump float tmpvar_1;
					  mediump vec3 tmpvar_2;
					  mediump vec3 tmpvar_3;
					  lowp vec3 worldN_4;
					  lowp vec4 c_5;
					  lowp vec3 tmpvar_6;
					  lowp vec3 lightDir_7;
					  mediump vec3 tmpvar_8;
					  tmpvar_8 = _WorldSpaceLightPos0.xyz;
					  lightDir_7 = tmpvar_8;
					  lowp vec3 tmpvar_9;
					  lowp vec3 tmpvar_10;
					  lowp float tmpvar_11;
					  tmpvar_10 = tmpvar_6;
					  highp vec4 tempNorm_12;
					  lowp vec4 tmpvar_13;
					  tmpvar_13 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  lowp vec4 tmpvar_14;
					  tmpvar_14.w = 0.0;
					  tmpvar_14.xyz = tmpvar_13.xyz;
					  highp vec3 tmpvar_15;
					  tmpvar_15 = mix (tmpvar_14, _Color, vec4(_Fade)).xyz;
					  tmpvar_9 = tmpvar_15;
					  tmpvar_11 = tmpvar_13.w;
					  lowp vec4 tmpvar_16;
					  tmpvar_16 = texture2D (_BumpMap, xlv_TEXCOORD0.zw);
					  tempNorm_12 = tmpvar_16;
					  if ((_Inv == 1.0)) {
					    tempNorm_12.xy = (vec2(1.0, 1.0) - tempNorm_12.xy);
					  };
					  highp vec3 tmpvar_17;
					  tmpvar_17 = ((tempNorm_12 * 2.0) - 1.0).xyz;
					  tmpvar_10 = tmpvar_17;
					  lowp float tmpvar_18;
					  highp float lightShadowDataX_19;
					  mediump float tmpvar_20;
					  tmpvar_20 = _LightShadowData.x;
					  lightShadowDataX_19 = tmpvar_20;
					  highp float tmpvar_21;
					  tmpvar_21 = max (float((texture2D (_ShadowMapTexture, xlv_TEXCOORD5.xy).x > xlv_TEXCOORD5.z)), lightShadowDataX_19);
					  tmpvar_18 = tmpvar_21;
					  highp float tmpvar_22;
					  tmpvar_22 = dot (xlv_TEXCOORD1.xyz, tmpvar_10);
					  worldN_4.x = tmpvar_22;
					  highp float tmpvar_23;
					  tmpvar_23 = dot (xlv_TEXCOORD2.xyz, tmpvar_10);
					  worldN_4.y = tmpvar_23;
					  highp float tmpvar_24;
					  tmpvar_24 = dot (xlv_TEXCOORD3.xyz, tmpvar_10);
					  worldN_4.z = tmpvar_24;
					  tmpvar_6 = worldN_4;
					  tmpvar_2 = _LightColor0.xyz;
					  tmpvar_3 = lightDir_7;
					  tmpvar_1 = tmpvar_18;
					  mediump vec3 tmpvar_25;
					  tmpvar_25 = (tmpvar_2 * tmpvar_1);
					  tmpvar_2 = tmpvar_25;
					  lowp vec4 c_26;
					  lowp vec4 c_27;
					  lowp float diff_28;
					  mediump float tmpvar_29;
					  tmpvar_29 = max (0.0, dot (worldN_4, tmpvar_3));
					  diff_28 = tmpvar_29;
					  c_27.xyz = ((tmpvar_9 * tmpvar_25) * diff_28);
					  c_27.w = tmpvar_11;
					  c_26.w = c_27.w;
					  c_26.xyz = (c_27.xyz + (tmpvar_9 * xlv_TEXCOORD4));
					  c_5.xyz = c_26.xyz;
					  c_5.w = 1.0;
					  gl_FragData[0] = c_5;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesTANGENT;
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
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
					uniform highp vec4 unity_WorldTransformParams;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _BumpMap_ST;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying highp vec4 xlv_TEXCOORD5;
					void main ()
					{
					  lowp float tangentSign_1;
					  lowp vec3 worldTangent_2;
					  lowp vec3 worldNormal_3;
					  highp vec4 tmpvar_4;
					  highp vec4 tmpvar_5;
					  tmpvar_5.w = 1.0;
					  tmpvar_5.xyz = _glesVertex.xyz;
					  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
					  highp vec3 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  tmpvar_6 = tmpvar_7.xyz;
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].x;
					  v_8.y = unity_WorldToObject[1].x;
					  v_8.z = unity_WorldToObject[2].x;
					  v_8.w = unity_WorldToObject[3].x;
					  highp vec4 v_9;
					  v_9.x = unity_WorldToObject[0].y;
					  v_9.y = unity_WorldToObject[1].y;
					  v_9.z = unity_WorldToObject[2].y;
					  v_9.w = unity_WorldToObject[3].y;
					  highp vec4 v_10;
					  v_10.x = unity_WorldToObject[0].z;
					  v_10.y = unity_WorldToObject[1].z;
					  v_10.z = unity_WorldToObject[2].z;
					  v_10.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = normalize(((
					    (v_8.xyz * _glesNormal.x)
					   + 
					    (v_9.xyz * _glesNormal.y)
					  ) + (v_10.xyz * _glesNormal.z)));
					  worldNormal_3 = tmpvar_11;
					  highp mat3 tmpvar_12;
					  tmpvar_12[0] = unity_ObjectToWorld[0].xyz;
					  tmpvar_12[1] = unity_ObjectToWorld[1].xyz;
					  tmpvar_12[2] = unity_ObjectToWorld[2].xyz;
					  highp vec3 tmpvar_13;
					  tmpvar_13 = normalize((tmpvar_12 * _glesTANGENT.xyz));
					  worldTangent_2 = tmpvar_13;
					  highp float tmpvar_14;
					  tmpvar_14 = (_glesTANGENT.w * unity_WorldTransformParams.w);
					  tangentSign_1 = tmpvar_14;
					  lowp vec3 tmpvar_15;
					  tmpvar_15 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
					  highp vec4 tmpvar_16;
					  tmpvar_16.x = worldTangent_2.x;
					  tmpvar_16.y = tmpvar_15.x;
					  tmpvar_16.z = worldNormal_3.x;
					  tmpvar_16.w = tmpvar_6.x;
					  highp vec4 tmpvar_17;
					  tmpvar_17.x = worldTangent_2.y;
					  tmpvar_17.y = tmpvar_15.y;
					  tmpvar_17.z = worldNormal_3.y;
					  tmpvar_17.w = tmpvar_6.y;
					  highp vec4 tmpvar_18;
					  tmpvar_18.x = worldTangent_2.z;
					  tmpvar_18.y = tmpvar_15.z;
					  tmpvar_18.z = worldNormal_3.z;
					  tmpvar_18.w = tmpvar_6.z;
					  mediump vec3 normal_19;
					  normal_19 = worldNormal_3;
					  mediump vec4 tmpvar_20;
					  tmpvar_20.w = 1.0;
					  tmpvar_20.xyz = normal_19;
					  mediump vec3 res_21;
					  mediump vec3 x_22;
					  x_22.x = dot (unity_SHAr, tmpvar_20);
					  x_22.y = dot (unity_SHAg, tmpvar_20);
					  x_22.z = dot (unity_SHAb, tmpvar_20);
					  mediump vec3 x1_23;
					  mediump vec4 tmpvar_24;
					  tmpvar_24 = (normal_19.xyzz * normal_19.yzzx);
					  x1_23.x = dot (unity_SHBr, tmpvar_24);
					  x1_23.y = dot (unity_SHBg, tmpvar_24);
					  x1_23.z = dot (unity_SHBb, tmpvar_24);
					  res_21 = (x_22 + (x1_23 + (unity_SHC.xyz * 
					    ((normal_19.x * normal_19.x) - (normal_19.y * normal_19.y))
					  )));
					  res_21 = max (((1.055 * 
					    pow (max (res_21, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  gl_Position = (glstate_matrix_mvp * tmpvar_5);
					  xlv_TEXCOORD0 = tmpvar_4;
					  xlv_TEXCOORD1 = tmpvar_16;
					  xlv_TEXCOORD2 = tmpvar_17;
					  xlv_TEXCOORD3 = tmpvar_18;
					  xlv_TEXCOORD4 = max (vec3(0.0, 0.0, 0.0), res_21);
					  xlv_TEXCOORD5 = (unity_WorldToShadow[0] * tmpvar_7);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform mediump vec4 _LightShadowData;
					uniform lowp vec4 _LightColor0;
					uniform highp sampler2D _ShadowMapTexture;
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying highp vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump float tmpvar_1;
					  mediump vec3 tmpvar_2;
					  mediump vec3 tmpvar_3;
					  lowp vec3 worldN_4;
					  lowp vec4 c_5;
					  lowp vec3 tmpvar_6;
					  lowp vec3 lightDir_7;
					  mediump vec3 tmpvar_8;
					  tmpvar_8 = _WorldSpaceLightPos0.xyz;
					  lightDir_7 = tmpvar_8;
					  lowp vec3 tmpvar_9;
					  lowp vec3 tmpvar_10;
					  lowp float tmpvar_11;
					  tmpvar_10 = tmpvar_6;
					  highp vec4 tempNorm_12;
					  lowp vec4 tmpvar_13;
					  tmpvar_13 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  lowp vec4 tmpvar_14;
					  tmpvar_14.w = 0.0;
					  tmpvar_14.xyz = tmpvar_13.xyz;
					  highp vec3 tmpvar_15;
					  tmpvar_15 = mix (tmpvar_14, _Color, vec4(_Fade)).xyz;
					  tmpvar_9 = tmpvar_15;
					  tmpvar_11 = tmpvar_13.w;
					  lowp vec4 tmpvar_16;
					  tmpvar_16 = texture2D (_BumpMap, xlv_TEXCOORD0.zw);
					  tempNorm_12 = tmpvar_16;
					  if ((_Inv == 1.0)) {
					    tempNorm_12.xy = (vec2(1.0, 1.0) - tempNorm_12.xy);
					  };
					  highp vec3 tmpvar_17;
					  tmpvar_17 = ((tempNorm_12 * 2.0) - 1.0).xyz;
					  tmpvar_10 = tmpvar_17;
					  lowp float tmpvar_18;
					  highp float lightShadowDataX_19;
					  mediump float tmpvar_20;
					  tmpvar_20 = _LightShadowData.x;
					  lightShadowDataX_19 = tmpvar_20;
					  highp float tmpvar_21;
					  tmpvar_21 = max (float((texture2D (_ShadowMapTexture, xlv_TEXCOORD5.xy).x > xlv_TEXCOORD5.z)), lightShadowDataX_19);
					  tmpvar_18 = tmpvar_21;
					  highp float tmpvar_22;
					  tmpvar_22 = dot (xlv_TEXCOORD1.xyz, tmpvar_10);
					  worldN_4.x = tmpvar_22;
					  highp float tmpvar_23;
					  tmpvar_23 = dot (xlv_TEXCOORD2.xyz, tmpvar_10);
					  worldN_4.y = tmpvar_23;
					  highp float tmpvar_24;
					  tmpvar_24 = dot (xlv_TEXCOORD3.xyz, tmpvar_10);
					  worldN_4.z = tmpvar_24;
					  tmpvar_6 = worldN_4;
					  tmpvar_2 = _LightColor0.xyz;
					  tmpvar_3 = lightDir_7;
					  tmpvar_1 = tmpvar_18;
					  mediump vec3 tmpvar_25;
					  tmpvar_25 = (tmpvar_2 * tmpvar_1);
					  tmpvar_2 = tmpvar_25;
					  lowp vec4 c_26;
					  lowp vec4 c_27;
					  lowp float diff_28;
					  mediump float tmpvar_29;
					  tmpvar_29 = max (0.0, dot (worldN_4, tmpvar_3));
					  diff_28 = tmpvar_29;
					  c_27.xyz = ((tmpvar_9 * tmpvar_25) * diff_28);
					  c_27.w = tmpvar_11;
					  c_26.w = c_27.w;
					  c_26.xyz = (c_27.xyz + (tmpvar_9 * xlv_TEXCOORD4));
					  c_5.xyz = c_26.xyz;
					  c_5.w = 1.0;
					  gl_FragData[0] = c_5;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesTANGENT;
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
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
					uniform highp vec4 unity_WorldTransformParams;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _BumpMap_ST;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying highp vec4 xlv_TEXCOORD5;
					void main ()
					{
					  lowp float tangentSign_1;
					  lowp vec3 worldTangent_2;
					  lowp vec3 worldNormal_3;
					  highp vec4 tmpvar_4;
					  highp vec4 tmpvar_5;
					  tmpvar_5.w = 1.0;
					  tmpvar_5.xyz = _glesVertex.xyz;
					  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
					  highp vec3 tmpvar_6;
					  highp vec4 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex);
					  tmpvar_6 = tmpvar_7.xyz;
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].x;
					  v_8.y = unity_WorldToObject[1].x;
					  v_8.z = unity_WorldToObject[2].x;
					  v_8.w = unity_WorldToObject[3].x;
					  highp vec4 v_9;
					  v_9.x = unity_WorldToObject[0].y;
					  v_9.y = unity_WorldToObject[1].y;
					  v_9.z = unity_WorldToObject[2].y;
					  v_9.w = unity_WorldToObject[3].y;
					  highp vec4 v_10;
					  v_10.x = unity_WorldToObject[0].z;
					  v_10.y = unity_WorldToObject[1].z;
					  v_10.z = unity_WorldToObject[2].z;
					  v_10.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = normalize(((
					    (v_8.xyz * _glesNormal.x)
					   + 
					    (v_9.xyz * _glesNormal.y)
					  ) + (v_10.xyz * _glesNormal.z)));
					  worldNormal_3 = tmpvar_11;
					  highp mat3 tmpvar_12;
					  tmpvar_12[0] = unity_ObjectToWorld[0].xyz;
					  tmpvar_12[1] = unity_ObjectToWorld[1].xyz;
					  tmpvar_12[2] = unity_ObjectToWorld[2].xyz;
					  highp vec3 tmpvar_13;
					  tmpvar_13 = normalize((tmpvar_12 * _glesTANGENT.xyz));
					  worldTangent_2 = tmpvar_13;
					  highp float tmpvar_14;
					  tmpvar_14 = (_glesTANGENT.w * unity_WorldTransformParams.w);
					  tangentSign_1 = tmpvar_14;
					  lowp vec3 tmpvar_15;
					  tmpvar_15 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
					  highp vec4 tmpvar_16;
					  tmpvar_16.x = worldTangent_2.x;
					  tmpvar_16.y = tmpvar_15.x;
					  tmpvar_16.z = worldNormal_3.x;
					  tmpvar_16.w = tmpvar_6.x;
					  highp vec4 tmpvar_17;
					  tmpvar_17.x = worldTangent_2.y;
					  tmpvar_17.y = tmpvar_15.y;
					  tmpvar_17.z = worldNormal_3.y;
					  tmpvar_17.w = tmpvar_6.y;
					  highp vec4 tmpvar_18;
					  tmpvar_18.x = worldTangent_2.z;
					  tmpvar_18.y = tmpvar_15.z;
					  tmpvar_18.z = worldNormal_3.z;
					  tmpvar_18.w = tmpvar_6.z;
					  mediump vec3 normal_19;
					  normal_19 = worldNormal_3;
					  mediump vec4 tmpvar_20;
					  tmpvar_20.w = 1.0;
					  tmpvar_20.xyz = normal_19;
					  mediump vec3 res_21;
					  mediump vec3 x_22;
					  x_22.x = dot (unity_SHAr, tmpvar_20);
					  x_22.y = dot (unity_SHAg, tmpvar_20);
					  x_22.z = dot (unity_SHAb, tmpvar_20);
					  mediump vec3 x1_23;
					  mediump vec4 tmpvar_24;
					  tmpvar_24 = (normal_19.xyzz * normal_19.yzzx);
					  x1_23.x = dot (unity_SHBr, tmpvar_24);
					  x1_23.y = dot (unity_SHBg, tmpvar_24);
					  x1_23.z = dot (unity_SHBb, tmpvar_24);
					  res_21 = (x_22 + (x1_23 + (unity_SHC.xyz * 
					    ((normal_19.x * normal_19.x) - (normal_19.y * normal_19.y))
					  )));
					  res_21 = max (((1.055 * 
					    pow (max (res_21, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  gl_Position = (glstate_matrix_mvp * tmpvar_5);
					  xlv_TEXCOORD0 = tmpvar_4;
					  xlv_TEXCOORD1 = tmpvar_16;
					  xlv_TEXCOORD2 = tmpvar_17;
					  xlv_TEXCOORD3 = tmpvar_18;
					  xlv_TEXCOORD4 = max (vec3(0.0, 0.0, 0.0), res_21);
					  xlv_TEXCOORD5 = (unity_WorldToShadow[0] * tmpvar_7);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform mediump vec4 _LightShadowData;
					uniform lowp vec4 _LightColor0;
					uniform highp sampler2D _ShadowMapTexture;
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying highp vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump float tmpvar_1;
					  mediump vec3 tmpvar_2;
					  mediump vec3 tmpvar_3;
					  lowp vec3 worldN_4;
					  lowp vec4 c_5;
					  lowp vec3 tmpvar_6;
					  lowp vec3 lightDir_7;
					  mediump vec3 tmpvar_8;
					  tmpvar_8 = _WorldSpaceLightPos0.xyz;
					  lightDir_7 = tmpvar_8;
					  lowp vec3 tmpvar_9;
					  lowp vec3 tmpvar_10;
					  lowp float tmpvar_11;
					  tmpvar_10 = tmpvar_6;
					  highp vec4 tempNorm_12;
					  lowp vec4 tmpvar_13;
					  tmpvar_13 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  lowp vec4 tmpvar_14;
					  tmpvar_14.w = 0.0;
					  tmpvar_14.xyz = tmpvar_13.xyz;
					  highp vec3 tmpvar_15;
					  tmpvar_15 = mix (tmpvar_14, _Color, vec4(_Fade)).xyz;
					  tmpvar_9 = tmpvar_15;
					  tmpvar_11 = tmpvar_13.w;
					  lowp vec4 tmpvar_16;
					  tmpvar_16 = texture2D (_BumpMap, xlv_TEXCOORD0.zw);
					  tempNorm_12 = tmpvar_16;
					  if ((_Inv == 1.0)) {
					    tempNorm_12.xy = (vec2(1.0, 1.0) - tempNorm_12.xy);
					  };
					  highp vec3 tmpvar_17;
					  tmpvar_17 = ((tempNorm_12 * 2.0) - 1.0).xyz;
					  tmpvar_10 = tmpvar_17;
					  lowp float tmpvar_18;
					  highp float lightShadowDataX_19;
					  mediump float tmpvar_20;
					  tmpvar_20 = _LightShadowData.x;
					  lightShadowDataX_19 = tmpvar_20;
					  highp float tmpvar_21;
					  tmpvar_21 = max (float((texture2D (_ShadowMapTexture, xlv_TEXCOORD5.xy).x > xlv_TEXCOORD5.z)), lightShadowDataX_19);
					  tmpvar_18 = tmpvar_21;
					  highp float tmpvar_22;
					  tmpvar_22 = dot (xlv_TEXCOORD1.xyz, tmpvar_10);
					  worldN_4.x = tmpvar_22;
					  highp float tmpvar_23;
					  tmpvar_23 = dot (xlv_TEXCOORD2.xyz, tmpvar_10);
					  worldN_4.y = tmpvar_23;
					  highp float tmpvar_24;
					  tmpvar_24 = dot (xlv_TEXCOORD3.xyz, tmpvar_10);
					  worldN_4.z = tmpvar_24;
					  tmpvar_6 = worldN_4;
					  tmpvar_2 = _LightColor0.xyz;
					  tmpvar_3 = lightDir_7;
					  tmpvar_1 = tmpvar_18;
					  mediump vec3 tmpvar_25;
					  tmpvar_25 = (tmpvar_2 * tmpvar_1);
					  tmpvar_2 = tmpvar_25;
					  lowp vec4 c_26;
					  lowp vec4 c_27;
					  lowp float diff_28;
					  mediump float tmpvar_29;
					  tmpvar_29 = max (0.0, dot (worldN_4, tmpvar_3));
					  diff_28 = tmpvar_29;
					  c_27.xyz = ((tmpvar_9 * tmpvar_25) * diff_28);
					  c_27.w = tmpvar_11;
					  c_26.w = c_27.w;
					  c_26.xyz = (c_27.xyz + (tmpvar_9 * xlv_TEXCOORD4));
					  c_5.xyz = c_26.xyz;
					  c_5.w = 1.0;
					  gl_FragData[0] = c_5;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesTANGENT;
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
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
					uniform highp vec4 unity_WorldTransformParams;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _BumpMap_ST;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					void main ()
					{
					  lowp float tangentSign_1;
					  lowp vec3 worldTangent_2;
					  lowp vec3 worldNormal_3;
					  highp vec4 tmpvar_4;
					  mediump vec3 tmpvar_5;
					  highp vec4 tmpvar_6;
					  tmpvar_6.w = 1.0;
					  tmpvar_6.xyz = _glesVertex.xyz;
					  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
					  highp vec3 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex).xyz;
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].x;
					  v_8.y = unity_WorldToObject[1].x;
					  v_8.z = unity_WorldToObject[2].x;
					  v_8.w = unity_WorldToObject[3].x;
					  highp vec4 v_9;
					  v_9.x = unity_WorldToObject[0].y;
					  v_9.y = unity_WorldToObject[1].y;
					  v_9.z = unity_WorldToObject[2].y;
					  v_9.w = unity_WorldToObject[3].y;
					  highp vec4 v_10;
					  v_10.x = unity_WorldToObject[0].z;
					  v_10.y = unity_WorldToObject[1].z;
					  v_10.z = unity_WorldToObject[2].z;
					  v_10.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = normalize(((
					    (v_8.xyz * _glesNormal.x)
					   + 
					    (v_9.xyz * _glesNormal.y)
					  ) + (v_10.xyz * _glesNormal.z)));
					  worldNormal_3 = tmpvar_11;
					  highp mat3 tmpvar_12;
					  tmpvar_12[0] = unity_ObjectToWorld[0].xyz;
					  tmpvar_12[1] = unity_ObjectToWorld[1].xyz;
					  tmpvar_12[2] = unity_ObjectToWorld[2].xyz;
					  highp vec3 tmpvar_13;
					  tmpvar_13 = normalize((tmpvar_12 * _glesTANGENT.xyz));
					  worldTangent_2 = tmpvar_13;
					  highp float tmpvar_14;
					  tmpvar_14 = (_glesTANGENT.w * unity_WorldTransformParams.w);
					  tangentSign_1 = tmpvar_14;
					  lowp vec3 tmpvar_15;
					  tmpvar_15 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
					  highp vec4 tmpvar_16;
					  tmpvar_16.x = worldTangent_2.x;
					  tmpvar_16.y = tmpvar_15.x;
					  tmpvar_16.z = worldNormal_3.x;
					  tmpvar_16.w = tmpvar_7.x;
					  highp vec4 tmpvar_17;
					  tmpvar_17.x = worldTangent_2.y;
					  tmpvar_17.y = tmpvar_15.y;
					  tmpvar_17.z = worldNormal_3.y;
					  tmpvar_17.w = tmpvar_7.y;
					  highp vec4 tmpvar_18;
					  tmpvar_18.x = worldTangent_2.z;
					  tmpvar_18.y = tmpvar_15.z;
					  tmpvar_18.z = worldNormal_3.z;
					  tmpvar_18.w = tmpvar_7.z;
					  highp vec3 lightColor0_19;
					  lightColor0_19 = unity_LightColor[0].xyz;
					  highp vec3 lightColor1_20;
					  lightColor1_20 = unity_LightColor[1].xyz;
					  highp vec3 lightColor2_21;
					  lightColor2_21 = unity_LightColor[2].xyz;
					  highp vec3 lightColor3_22;
					  lightColor3_22 = unity_LightColor[3].xyz;
					  highp vec4 lightAttenSq_23;
					  lightAttenSq_23 = unity_4LightAtten0;
					  highp vec3 normal_24;
					  normal_24 = worldNormal_3;
					  highp vec3 col_25;
					  highp vec4 ndotl_26;
					  highp vec4 lengthSq_27;
					  highp vec4 tmpvar_28;
					  tmpvar_28 = (unity_4LightPosX0 - tmpvar_7.x);
					  highp vec4 tmpvar_29;
					  tmpvar_29 = (unity_4LightPosY0 - tmpvar_7.y);
					  highp vec4 tmpvar_30;
					  tmpvar_30 = (unity_4LightPosZ0 - tmpvar_7.z);
					  lengthSq_27 = (tmpvar_28 * tmpvar_28);
					  lengthSq_27 = (lengthSq_27 + (tmpvar_29 * tmpvar_29));
					  lengthSq_27 = (lengthSq_27 + (tmpvar_30 * tmpvar_30));
					  ndotl_26 = (tmpvar_28 * normal_24.x);
					  ndotl_26 = (ndotl_26 + (tmpvar_29 * normal_24.y));
					  ndotl_26 = (ndotl_26 + (tmpvar_30 * normal_24.z));
					  highp vec4 tmpvar_31;
					  tmpvar_31 = max (vec4(0.0, 0.0, 0.0, 0.0), (ndotl_26 * inversesqrt(lengthSq_27)));
					  ndotl_26 = tmpvar_31;
					  highp vec4 tmpvar_32;
					  tmpvar_32 = (tmpvar_31 * (1.0/((1.0 + 
					    (lengthSq_27 * lightAttenSq_23)
					  ))));
					  col_25 = (lightColor0_19 * tmpvar_32.x);
					  col_25 = (col_25 + (lightColor1_20 * tmpvar_32.y));
					  col_25 = (col_25 + (lightColor2_21 * tmpvar_32.z));
					  col_25 = (col_25 + (lightColor3_22 * tmpvar_32.w));
					  tmpvar_5 = col_25;
					  mediump vec3 normal_33;
					  normal_33 = worldNormal_3;
					  mediump vec3 ambient_34;
					  mediump vec4 tmpvar_35;
					  tmpvar_35.w = 1.0;
					  tmpvar_35.xyz = normal_33;
					  mediump vec3 res_36;
					  mediump vec3 x_37;
					  x_37.x = dot (unity_SHAr, tmpvar_35);
					  x_37.y = dot (unity_SHAg, tmpvar_35);
					  x_37.z = dot (unity_SHAb, tmpvar_35);
					  mediump vec3 x1_38;
					  mediump vec4 tmpvar_39;
					  tmpvar_39 = (normal_33.xyzz * normal_33.yzzx);
					  x1_38.x = dot (unity_SHBr, tmpvar_39);
					  x1_38.y = dot (unity_SHBg, tmpvar_39);
					  x1_38.z = dot (unity_SHBb, tmpvar_39);
					  res_36 = (x_37 + (x1_38 + (unity_SHC.xyz * 
					    ((normal_33.x * normal_33.x) - (normal_33.y * normal_33.y))
					  )));
					  res_36 = max (((1.055 * 
					    pow (max (res_36, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  ambient_34 = (tmpvar_5 + max (vec3(0.0, 0.0, 0.0), res_36));
					  tmpvar_5 = ambient_34;
					  gl_Position = (glstate_matrix_mvp * tmpvar_6);
					  xlv_TEXCOORD0 = tmpvar_4;
					  xlv_TEXCOORD1 = tmpvar_16;
					  xlv_TEXCOORD2 = tmpvar_17;
					  xlv_TEXCOORD3 = tmpvar_18;
					  xlv_TEXCOORD4 = ambient_34;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  mediump vec3 tmpvar_2;
					  lowp vec3 worldN_3;
					  lowp vec4 c_4;
					  lowp vec3 tmpvar_5;
					  lowp vec3 lightDir_6;
					  mediump vec3 tmpvar_7;
					  tmpvar_7 = _WorldSpaceLightPos0.xyz;
					  lightDir_6 = tmpvar_7;
					  lowp vec3 tmpvar_8;
					  lowp vec3 tmpvar_9;
					  lowp float tmpvar_10;
					  tmpvar_9 = tmpvar_5;
					  highp vec4 tempNorm_11;
					  lowp vec4 tmpvar_12;
					  tmpvar_12 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  lowp vec4 tmpvar_13;
					  tmpvar_13.w = 0.0;
					  tmpvar_13.xyz = tmpvar_12.xyz;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = mix (tmpvar_13, _Color, vec4(_Fade)).xyz;
					  tmpvar_8 = tmpvar_14;
					  tmpvar_10 = tmpvar_12.w;
					  lowp vec4 tmpvar_15;
					  tmpvar_15 = texture2D (_BumpMap, xlv_TEXCOORD0.zw);
					  tempNorm_11 = tmpvar_15;
					  if ((_Inv == 1.0)) {
					    tempNorm_11.xy = (vec2(1.0, 1.0) - tempNorm_11.xy);
					  };
					  highp vec3 tmpvar_16;
					  tmpvar_16 = ((tempNorm_11 * 2.0) - 1.0).xyz;
					  tmpvar_9 = tmpvar_16;
					  highp float tmpvar_17;
					  tmpvar_17 = dot (xlv_TEXCOORD1.xyz, tmpvar_9);
					  worldN_3.x = tmpvar_17;
					  highp float tmpvar_18;
					  tmpvar_18 = dot (xlv_TEXCOORD2.xyz, tmpvar_9);
					  worldN_3.y = tmpvar_18;
					  highp float tmpvar_19;
					  tmpvar_19 = dot (xlv_TEXCOORD3.xyz, tmpvar_9);
					  worldN_3.z = tmpvar_19;
					  tmpvar_5 = worldN_3;
					  tmpvar_1 = _LightColor0.xyz;
					  tmpvar_2 = lightDir_6;
					  lowp vec4 c_20;
					  lowp vec4 c_21;
					  lowp float diff_22;
					  mediump float tmpvar_23;
					  tmpvar_23 = max (0.0, dot (worldN_3, tmpvar_2));
					  diff_22 = tmpvar_23;
					  c_21.xyz = ((tmpvar_8 * tmpvar_1) * diff_22);
					  c_21.w = tmpvar_10;
					  c_20.w = c_21.w;
					  c_20.xyz = (c_21.xyz + (tmpvar_8 * xlv_TEXCOORD4));
					  c_4.xyz = c_20.xyz;
					  c_4.w = 1.0;
					  gl_FragData[0] = c_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesTANGENT;
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
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
					uniform highp vec4 unity_WorldTransformParams;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _BumpMap_ST;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					void main ()
					{
					  lowp float tangentSign_1;
					  lowp vec3 worldTangent_2;
					  lowp vec3 worldNormal_3;
					  highp vec4 tmpvar_4;
					  mediump vec3 tmpvar_5;
					  highp vec4 tmpvar_6;
					  tmpvar_6.w = 1.0;
					  tmpvar_6.xyz = _glesVertex.xyz;
					  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
					  highp vec3 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex).xyz;
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].x;
					  v_8.y = unity_WorldToObject[1].x;
					  v_8.z = unity_WorldToObject[2].x;
					  v_8.w = unity_WorldToObject[3].x;
					  highp vec4 v_9;
					  v_9.x = unity_WorldToObject[0].y;
					  v_9.y = unity_WorldToObject[1].y;
					  v_9.z = unity_WorldToObject[2].y;
					  v_9.w = unity_WorldToObject[3].y;
					  highp vec4 v_10;
					  v_10.x = unity_WorldToObject[0].z;
					  v_10.y = unity_WorldToObject[1].z;
					  v_10.z = unity_WorldToObject[2].z;
					  v_10.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = normalize(((
					    (v_8.xyz * _glesNormal.x)
					   + 
					    (v_9.xyz * _glesNormal.y)
					  ) + (v_10.xyz * _glesNormal.z)));
					  worldNormal_3 = tmpvar_11;
					  highp mat3 tmpvar_12;
					  tmpvar_12[0] = unity_ObjectToWorld[0].xyz;
					  tmpvar_12[1] = unity_ObjectToWorld[1].xyz;
					  tmpvar_12[2] = unity_ObjectToWorld[2].xyz;
					  highp vec3 tmpvar_13;
					  tmpvar_13 = normalize((tmpvar_12 * _glesTANGENT.xyz));
					  worldTangent_2 = tmpvar_13;
					  highp float tmpvar_14;
					  tmpvar_14 = (_glesTANGENT.w * unity_WorldTransformParams.w);
					  tangentSign_1 = tmpvar_14;
					  lowp vec3 tmpvar_15;
					  tmpvar_15 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
					  highp vec4 tmpvar_16;
					  tmpvar_16.x = worldTangent_2.x;
					  tmpvar_16.y = tmpvar_15.x;
					  tmpvar_16.z = worldNormal_3.x;
					  tmpvar_16.w = tmpvar_7.x;
					  highp vec4 tmpvar_17;
					  tmpvar_17.x = worldTangent_2.y;
					  tmpvar_17.y = tmpvar_15.y;
					  tmpvar_17.z = worldNormal_3.y;
					  tmpvar_17.w = tmpvar_7.y;
					  highp vec4 tmpvar_18;
					  tmpvar_18.x = worldTangent_2.z;
					  tmpvar_18.y = tmpvar_15.z;
					  tmpvar_18.z = worldNormal_3.z;
					  tmpvar_18.w = tmpvar_7.z;
					  highp vec3 lightColor0_19;
					  lightColor0_19 = unity_LightColor[0].xyz;
					  highp vec3 lightColor1_20;
					  lightColor1_20 = unity_LightColor[1].xyz;
					  highp vec3 lightColor2_21;
					  lightColor2_21 = unity_LightColor[2].xyz;
					  highp vec3 lightColor3_22;
					  lightColor3_22 = unity_LightColor[3].xyz;
					  highp vec4 lightAttenSq_23;
					  lightAttenSq_23 = unity_4LightAtten0;
					  highp vec3 normal_24;
					  normal_24 = worldNormal_3;
					  highp vec3 col_25;
					  highp vec4 ndotl_26;
					  highp vec4 lengthSq_27;
					  highp vec4 tmpvar_28;
					  tmpvar_28 = (unity_4LightPosX0 - tmpvar_7.x);
					  highp vec4 tmpvar_29;
					  tmpvar_29 = (unity_4LightPosY0 - tmpvar_7.y);
					  highp vec4 tmpvar_30;
					  tmpvar_30 = (unity_4LightPosZ0 - tmpvar_7.z);
					  lengthSq_27 = (tmpvar_28 * tmpvar_28);
					  lengthSq_27 = (lengthSq_27 + (tmpvar_29 * tmpvar_29));
					  lengthSq_27 = (lengthSq_27 + (tmpvar_30 * tmpvar_30));
					  ndotl_26 = (tmpvar_28 * normal_24.x);
					  ndotl_26 = (ndotl_26 + (tmpvar_29 * normal_24.y));
					  ndotl_26 = (ndotl_26 + (tmpvar_30 * normal_24.z));
					  highp vec4 tmpvar_31;
					  tmpvar_31 = max (vec4(0.0, 0.0, 0.0, 0.0), (ndotl_26 * inversesqrt(lengthSq_27)));
					  ndotl_26 = tmpvar_31;
					  highp vec4 tmpvar_32;
					  tmpvar_32 = (tmpvar_31 * (1.0/((1.0 + 
					    (lengthSq_27 * lightAttenSq_23)
					  ))));
					  col_25 = (lightColor0_19 * tmpvar_32.x);
					  col_25 = (col_25 + (lightColor1_20 * tmpvar_32.y));
					  col_25 = (col_25 + (lightColor2_21 * tmpvar_32.z));
					  col_25 = (col_25 + (lightColor3_22 * tmpvar_32.w));
					  tmpvar_5 = col_25;
					  mediump vec3 normal_33;
					  normal_33 = worldNormal_3;
					  mediump vec3 ambient_34;
					  mediump vec4 tmpvar_35;
					  tmpvar_35.w = 1.0;
					  tmpvar_35.xyz = normal_33;
					  mediump vec3 res_36;
					  mediump vec3 x_37;
					  x_37.x = dot (unity_SHAr, tmpvar_35);
					  x_37.y = dot (unity_SHAg, tmpvar_35);
					  x_37.z = dot (unity_SHAb, tmpvar_35);
					  mediump vec3 x1_38;
					  mediump vec4 tmpvar_39;
					  tmpvar_39 = (normal_33.xyzz * normal_33.yzzx);
					  x1_38.x = dot (unity_SHBr, tmpvar_39);
					  x1_38.y = dot (unity_SHBg, tmpvar_39);
					  x1_38.z = dot (unity_SHBb, tmpvar_39);
					  res_36 = (x_37 + (x1_38 + (unity_SHC.xyz * 
					    ((normal_33.x * normal_33.x) - (normal_33.y * normal_33.y))
					  )));
					  res_36 = max (((1.055 * 
					    pow (max (res_36, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  ambient_34 = (tmpvar_5 + max (vec3(0.0, 0.0, 0.0), res_36));
					  tmpvar_5 = ambient_34;
					  gl_Position = (glstate_matrix_mvp * tmpvar_6);
					  xlv_TEXCOORD0 = tmpvar_4;
					  xlv_TEXCOORD1 = tmpvar_16;
					  xlv_TEXCOORD2 = tmpvar_17;
					  xlv_TEXCOORD3 = tmpvar_18;
					  xlv_TEXCOORD4 = ambient_34;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  mediump vec3 tmpvar_2;
					  lowp vec3 worldN_3;
					  lowp vec4 c_4;
					  lowp vec3 tmpvar_5;
					  lowp vec3 lightDir_6;
					  mediump vec3 tmpvar_7;
					  tmpvar_7 = _WorldSpaceLightPos0.xyz;
					  lightDir_6 = tmpvar_7;
					  lowp vec3 tmpvar_8;
					  lowp vec3 tmpvar_9;
					  lowp float tmpvar_10;
					  tmpvar_9 = tmpvar_5;
					  highp vec4 tempNorm_11;
					  lowp vec4 tmpvar_12;
					  tmpvar_12 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  lowp vec4 tmpvar_13;
					  tmpvar_13.w = 0.0;
					  tmpvar_13.xyz = tmpvar_12.xyz;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = mix (tmpvar_13, _Color, vec4(_Fade)).xyz;
					  tmpvar_8 = tmpvar_14;
					  tmpvar_10 = tmpvar_12.w;
					  lowp vec4 tmpvar_15;
					  tmpvar_15 = texture2D (_BumpMap, xlv_TEXCOORD0.zw);
					  tempNorm_11 = tmpvar_15;
					  if ((_Inv == 1.0)) {
					    tempNorm_11.xy = (vec2(1.0, 1.0) - tempNorm_11.xy);
					  };
					  highp vec3 tmpvar_16;
					  tmpvar_16 = ((tempNorm_11 * 2.0) - 1.0).xyz;
					  tmpvar_9 = tmpvar_16;
					  highp float tmpvar_17;
					  tmpvar_17 = dot (xlv_TEXCOORD1.xyz, tmpvar_9);
					  worldN_3.x = tmpvar_17;
					  highp float tmpvar_18;
					  tmpvar_18 = dot (xlv_TEXCOORD2.xyz, tmpvar_9);
					  worldN_3.y = tmpvar_18;
					  highp float tmpvar_19;
					  tmpvar_19 = dot (xlv_TEXCOORD3.xyz, tmpvar_9);
					  worldN_3.z = tmpvar_19;
					  tmpvar_5 = worldN_3;
					  tmpvar_1 = _LightColor0.xyz;
					  tmpvar_2 = lightDir_6;
					  lowp vec4 c_20;
					  lowp vec4 c_21;
					  lowp float diff_22;
					  mediump float tmpvar_23;
					  tmpvar_23 = max (0.0, dot (worldN_3, tmpvar_2));
					  diff_22 = tmpvar_23;
					  c_21.xyz = ((tmpvar_8 * tmpvar_1) * diff_22);
					  c_21.w = tmpvar_10;
					  c_20.w = c_21.w;
					  c_20.xyz = (c_21.xyz + (tmpvar_8 * xlv_TEXCOORD4));
					  c_4.xyz = c_20.xyz;
					  c_4.w = 1.0;
					  gl_FragData[0] = c_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesTANGENT;
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
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
					uniform highp vec4 unity_WorldTransformParams;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _BumpMap_ST;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					void main ()
					{
					  lowp float tangentSign_1;
					  lowp vec3 worldTangent_2;
					  lowp vec3 worldNormal_3;
					  highp vec4 tmpvar_4;
					  mediump vec3 tmpvar_5;
					  highp vec4 tmpvar_6;
					  tmpvar_6.w = 1.0;
					  tmpvar_6.xyz = _glesVertex.xyz;
					  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
					  highp vec3 tmpvar_7;
					  tmpvar_7 = (unity_ObjectToWorld * _glesVertex).xyz;
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].x;
					  v_8.y = unity_WorldToObject[1].x;
					  v_8.z = unity_WorldToObject[2].x;
					  v_8.w = unity_WorldToObject[3].x;
					  highp vec4 v_9;
					  v_9.x = unity_WorldToObject[0].y;
					  v_9.y = unity_WorldToObject[1].y;
					  v_9.z = unity_WorldToObject[2].y;
					  v_9.w = unity_WorldToObject[3].y;
					  highp vec4 v_10;
					  v_10.x = unity_WorldToObject[0].z;
					  v_10.y = unity_WorldToObject[1].z;
					  v_10.z = unity_WorldToObject[2].z;
					  v_10.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = normalize(((
					    (v_8.xyz * _glesNormal.x)
					   + 
					    (v_9.xyz * _glesNormal.y)
					  ) + (v_10.xyz * _glesNormal.z)));
					  worldNormal_3 = tmpvar_11;
					  highp mat3 tmpvar_12;
					  tmpvar_12[0] = unity_ObjectToWorld[0].xyz;
					  tmpvar_12[1] = unity_ObjectToWorld[1].xyz;
					  tmpvar_12[2] = unity_ObjectToWorld[2].xyz;
					  highp vec3 tmpvar_13;
					  tmpvar_13 = normalize((tmpvar_12 * _glesTANGENT.xyz));
					  worldTangent_2 = tmpvar_13;
					  highp float tmpvar_14;
					  tmpvar_14 = (_glesTANGENT.w * unity_WorldTransformParams.w);
					  tangentSign_1 = tmpvar_14;
					  lowp vec3 tmpvar_15;
					  tmpvar_15 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
					  highp vec4 tmpvar_16;
					  tmpvar_16.x = worldTangent_2.x;
					  tmpvar_16.y = tmpvar_15.x;
					  tmpvar_16.z = worldNormal_3.x;
					  tmpvar_16.w = tmpvar_7.x;
					  highp vec4 tmpvar_17;
					  tmpvar_17.x = worldTangent_2.y;
					  tmpvar_17.y = tmpvar_15.y;
					  tmpvar_17.z = worldNormal_3.y;
					  tmpvar_17.w = tmpvar_7.y;
					  highp vec4 tmpvar_18;
					  tmpvar_18.x = worldTangent_2.z;
					  tmpvar_18.y = tmpvar_15.z;
					  tmpvar_18.z = worldNormal_3.z;
					  tmpvar_18.w = tmpvar_7.z;
					  highp vec3 lightColor0_19;
					  lightColor0_19 = unity_LightColor[0].xyz;
					  highp vec3 lightColor1_20;
					  lightColor1_20 = unity_LightColor[1].xyz;
					  highp vec3 lightColor2_21;
					  lightColor2_21 = unity_LightColor[2].xyz;
					  highp vec3 lightColor3_22;
					  lightColor3_22 = unity_LightColor[3].xyz;
					  highp vec4 lightAttenSq_23;
					  lightAttenSq_23 = unity_4LightAtten0;
					  highp vec3 normal_24;
					  normal_24 = worldNormal_3;
					  highp vec3 col_25;
					  highp vec4 ndotl_26;
					  highp vec4 lengthSq_27;
					  highp vec4 tmpvar_28;
					  tmpvar_28 = (unity_4LightPosX0 - tmpvar_7.x);
					  highp vec4 tmpvar_29;
					  tmpvar_29 = (unity_4LightPosY0 - tmpvar_7.y);
					  highp vec4 tmpvar_30;
					  tmpvar_30 = (unity_4LightPosZ0 - tmpvar_7.z);
					  lengthSq_27 = (tmpvar_28 * tmpvar_28);
					  lengthSq_27 = (lengthSq_27 + (tmpvar_29 * tmpvar_29));
					  lengthSq_27 = (lengthSq_27 + (tmpvar_30 * tmpvar_30));
					  ndotl_26 = (tmpvar_28 * normal_24.x);
					  ndotl_26 = (ndotl_26 + (tmpvar_29 * normal_24.y));
					  ndotl_26 = (ndotl_26 + (tmpvar_30 * normal_24.z));
					  highp vec4 tmpvar_31;
					  tmpvar_31 = max (vec4(0.0, 0.0, 0.0, 0.0), (ndotl_26 * inversesqrt(lengthSq_27)));
					  ndotl_26 = tmpvar_31;
					  highp vec4 tmpvar_32;
					  tmpvar_32 = (tmpvar_31 * (1.0/((1.0 + 
					    (lengthSq_27 * lightAttenSq_23)
					  ))));
					  col_25 = (lightColor0_19 * tmpvar_32.x);
					  col_25 = (col_25 + (lightColor1_20 * tmpvar_32.y));
					  col_25 = (col_25 + (lightColor2_21 * tmpvar_32.z));
					  col_25 = (col_25 + (lightColor3_22 * tmpvar_32.w));
					  tmpvar_5 = col_25;
					  mediump vec3 normal_33;
					  normal_33 = worldNormal_3;
					  mediump vec3 ambient_34;
					  mediump vec4 tmpvar_35;
					  tmpvar_35.w = 1.0;
					  tmpvar_35.xyz = normal_33;
					  mediump vec3 res_36;
					  mediump vec3 x_37;
					  x_37.x = dot (unity_SHAr, tmpvar_35);
					  x_37.y = dot (unity_SHAg, tmpvar_35);
					  x_37.z = dot (unity_SHAb, tmpvar_35);
					  mediump vec3 x1_38;
					  mediump vec4 tmpvar_39;
					  tmpvar_39 = (normal_33.xyzz * normal_33.yzzx);
					  x1_38.x = dot (unity_SHBr, tmpvar_39);
					  x1_38.y = dot (unity_SHBg, tmpvar_39);
					  x1_38.z = dot (unity_SHBb, tmpvar_39);
					  res_36 = (x_37 + (x1_38 + (unity_SHC.xyz * 
					    ((normal_33.x * normal_33.x) - (normal_33.y * normal_33.y))
					  )));
					  res_36 = max (((1.055 * 
					    pow (max (res_36, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  ambient_34 = (tmpvar_5 + max (vec3(0.0, 0.0, 0.0), res_36));
					  tmpvar_5 = ambient_34;
					  gl_Position = (glstate_matrix_mvp * tmpvar_6);
					  xlv_TEXCOORD0 = tmpvar_4;
					  xlv_TEXCOORD1 = tmpvar_16;
					  xlv_TEXCOORD2 = tmpvar_17;
					  xlv_TEXCOORD3 = tmpvar_18;
					  xlv_TEXCOORD4 = ambient_34;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  mediump vec3 tmpvar_2;
					  lowp vec3 worldN_3;
					  lowp vec4 c_4;
					  lowp vec3 tmpvar_5;
					  lowp vec3 lightDir_6;
					  mediump vec3 tmpvar_7;
					  tmpvar_7 = _WorldSpaceLightPos0.xyz;
					  lightDir_6 = tmpvar_7;
					  lowp vec3 tmpvar_8;
					  lowp vec3 tmpvar_9;
					  lowp float tmpvar_10;
					  tmpvar_9 = tmpvar_5;
					  highp vec4 tempNorm_11;
					  lowp vec4 tmpvar_12;
					  tmpvar_12 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  lowp vec4 tmpvar_13;
					  tmpvar_13.w = 0.0;
					  tmpvar_13.xyz = tmpvar_12.xyz;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = mix (tmpvar_13, _Color, vec4(_Fade)).xyz;
					  tmpvar_8 = tmpvar_14;
					  tmpvar_10 = tmpvar_12.w;
					  lowp vec4 tmpvar_15;
					  tmpvar_15 = texture2D (_BumpMap, xlv_TEXCOORD0.zw);
					  tempNorm_11 = tmpvar_15;
					  if ((_Inv == 1.0)) {
					    tempNorm_11.xy = (vec2(1.0, 1.0) - tempNorm_11.xy);
					  };
					  highp vec3 tmpvar_16;
					  tmpvar_16 = ((tempNorm_11 * 2.0) - 1.0).xyz;
					  tmpvar_9 = tmpvar_16;
					  highp float tmpvar_17;
					  tmpvar_17 = dot (xlv_TEXCOORD1.xyz, tmpvar_9);
					  worldN_3.x = tmpvar_17;
					  highp float tmpvar_18;
					  tmpvar_18 = dot (xlv_TEXCOORD2.xyz, tmpvar_9);
					  worldN_3.y = tmpvar_18;
					  highp float tmpvar_19;
					  tmpvar_19 = dot (xlv_TEXCOORD3.xyz, tmpvar_9);
					  worldN_3.z = tmpvar_19;
					  tmpvar_5 = worldN_3;
					  tmpvar_1 = _LightColor0.xyz;
					  tmpvar_2 = lightDir_6;
					  lowp vec4 c_20;
					  lowp vec4 c_21;
					  lowp float diff_22;
					  mediump float tmpvar_23;
					  tmpvar_23 = max (0.0, dot (worldN_3, tmpvar_2));
					  diff_22 = tmpvar_23;
					  c_21.xyz = ((tmpvar_8 * tmpvar_1) * diff_22);
					  c_21.w = tmpvar_10;
					  c_20.w = c_21.w;
					  c_20.xyz = (c_21.xyz + (tmpvar_8 * xlv_TEXCOORD4));
					  c_4.xyz = c_20.xyz;
					  c_4.w = 1.0;
					  gl_FragData[0] = c_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesTANGENT;
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
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
					uniform highp vec4 unity_WorldTransformParams;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _BumpMap_ST;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying highp vec4 xlv_TEXCOORD5;
					void main ()
					{
					  lowp float tangentSign_1;
					  lowp vec3 worldTangent_2;
					  lowp vec3 worldNormal_3;
					  highp vec4 tmpvar_4;
					  mediump vec3 tmpvar_5;
					  highp vec4 tmpvar_6;
					  tmpvar_6.w = 1.0;
					  tmpvar_6.xyz = _glesVertex.xyz;
					  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
					  highp vec3 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  tmpvar_7 = tmpvar_8.xyz;
					  highp vec4 v_9;
					  v_9.x = unity_WorldToObject[0].x;
					  v_9.y = unity_WorldToObject[1].x;
					  v_9.z = unity_WorldToObject[2].x;
					  v_9.w = unity_WorldToObject[3].x;
					  highp vec4 v_10;
					  v_10.x = unity_WorldToObject[0].y;
					  v_10.y = unity_WorldToObject[1].y;
					  v_10.z = unity_WorldToObject[2].y;
					  v_10.w = unity_WorldToObject[3].y;
					  highp vec4 v_11;
					  v_11.x = unity_WorldToObject[0].z;
					  v_11.y = unity_WorldToObject[1].z;
					  v_11.z = unity_WorldToObject[2].z;
					  v_11.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_12;
					  tmpvar_12 = normalize(((
					    (v_9.xyz * _glesNormal.x)
					   + 
					    (v_10.xyz * _glesNormal.y)
					  ) + (v_11.xyz * _glesNormal.z)));
					  worldNormal_3 = tmpvar_12;
					  highp mat3 tmpvar_13;
					  tmpvar_13[0] = unity_ObjectToWorld[0].xyz;
					  tmpvar_13[1] = unity_ObjectToWorld[1].xyz;
					  tmpvar_13[2] = unity_ObjectToWorld[2].xyz;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_13 * _glesTANGENT.xyz));
					  worldTangent_2 = tmpvar_14;
					  highp float tmpvar_15;
					  tmpvar_15 = (_glesTANGENT.w * unity_WorldTransformParams.w);
					  tangentSign_1 = tmpvar_15;
					  lowp vec3 tmpvar_16;
					  tmpvar_16 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
					  highp vec4 tmpvar_17;
					  tmpvar_17.x = worldTangent_2.x;
					  tmpvar_17.y = tmpvar_16.x;
					  tmpvar_17.z = worldNormal_3.x;
					  tmpvar_17.w = tmpvar_7.x;
					  highp vec4 tmpvar_18;
					  tmpvar_18.x = worldTangent_2.y;
					  tmpvar_18.y = tmpvar_16.y;
					  tmpvar_18.z = worldNormal_3.y;
					  tmpvar_18.w = tmpvar_7.y;
					  highp vec4 tmpvar_19;
					  tmpvar_19.x = worldTangent_2.z;
					  tmpvar_19.y = tmpvar_16.z;
					  tmpvar_19.z = worldNormal_3.z;
					  tmpvar_19.w = tmpvar_7.z;
					  highp vec3 lightColor0_20;
					  lightColor0_20 = unity_LightColor[0].xyz;
					  highp vec3 lightColor1_21;
					  lightColor1_21 = unity_LightColor[1].xyz;
					  highp vec3 lightColor2_22;
					  lightColor2_22 = unity_LightColor[2].xyz;
					  highp vec3 lightColor3_23;
					  lightColor3_23 = unity_LightColor[3].xyz;
					  highp vec4 lightAttenSq_24;
					  lightAttenSq_24 = unity_4LightAtten0;
					  highp vec3 normal_25;
					  normal_25 = worldNormal_3;
					  highp vec3 col_26;
					  highp vec4 ndotl_27;
					  highp vec4 lengthSq_28;
					  highp vec4 tmpvar_29;
					  tmpvar_29 = (unity_4LightPosX0 - tmpvar_8.x);
					  highp vec4 tmpvar_30;
					  tmpvar_30 = (unity_4LightPosY0 - tmpvar_8.y);
					  highp vec4 tmpvar_31;
					  tmpvar_31 = (unity_4LightPosZ0 - tmpvar_8.z);
					  lengthSq_28 = (tmpvar_29 * tmpvar_29);
					  lengthSq_28 = (lengthSq_28 + (tmpvar_30 * tmpvar_30));
					  lengthSq_28 = (lengthSq_28 + (tmpvar_31 * tmpvar_31));
					  ndotl_27 = (tmpvar_29 * normal_25.x);
					  ndotl_27 = (ndotl_27 + (tmpvar_30 * normal_25.y));
					  ndotl_27 = (ndotl_27 + (tmpvar_31 * normal_25.z));
					  highp vec4 tmpvar_32;
					  tmpvar_32 = max (vec4(0.0, 0.0, 0.0, 0.0), (ndotl_27 * inversesqrt(lengthSq_28)));
					  ndotl_27 = tmpvar_32;
					  highp vec4 tmpvar_33;
					  tmpvar_33 = (tmpvar_32 * (1.0/((1.0 + 
					    (lengthSq_28 * lightAttenSq_24)
					  ))));
					  col_26 = (lightColor0_20 * tmpvar_33.x);
					  col_26 = (col_26 + (lightColor1_21 * tmpvar_33.y));
					  col_26 = (col_26 + (lightColor2_22 * tmpvar_33.z));
					  col_26 = (col_26 + (lightColor3_23 * tmpvar_33.w));
					  tmpvar_5 = col_26;
					  mediump vec3 normal_34;
					  normal_34 = worldNormal_3;
					  mediump vec3 ambient_35;
					  mediump vec4 tmpvar_36;
					  tmpvar_36.w = 1.0;
					  tmpvar_36.xyz = normal_34;
					  mediump vec3 res_37;
					  mediump vec3 x_38;
					  x_38.x = dot (unity_SHAr, tmpvar_36);
					  x_38.y = dot (unity_SHAg, tmpvar_36);
					  x_38.z = dot (unity_SHAb, tmpvar_36);
					  mediump vec3 x1_39;
					  mediump vec4 tmpvar_40;
					  tmpvar_40 = (normal_34.xyzz * normal_34.yzzx);
					  x1_39.x = dot (unity_SHBr, tmpvar_40);
					  x1_39.y = dot (unity_SHBg, tmpvar_40);
					  x1_39.z = dot (unity_SHBb, tmpvar_40);
					  res_37 = (x_38 + (x1_39 + (unity_SHC.xyz * 
					    ((normal_34.x * normal_34.x) - (normal_34.y * normal_34.y))
					  )));
					  res_37 = max (((1.055 * 
					    pow (max (res_37, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  ambient_35 = (tmpvar_5 + max (vec3(0.0, 0.0, 0.0), res_37));
					  tmpvar_5 = ambient_35;
					  gl_Position = (glstate_matrix_mvp * tmpvar_6);
					  xlv_TEXCOORD0 = tmpvar_4;
					  xlv_TEXCOORD1 = tmpvar_17;
					  xlv_TEXCOORD2 = tmpvar_18;
					  xlv_TEXCOORD3 = tmpvar_19;
					  xlv_TEXCOORD4 = ambient_35;
					  xlv_TEXCOORD5 = (unity_WorldToShadow[0] * tmpvar_8);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform mediump vec4 _LightShadowData;
					uniform lowp vec4 _LightColor0;
					uniform highp sampler2D _ShadowMapTexture;
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying highp vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump float tmpvar_1;
					  mediump vec3 tmpvar_2;
					  mediump vec3 tmpvar_3;
					  lowp vec3 worldN_4;
					  lowp vec4 c_5;
					  lowp vec3 tmpvar_6;
					  lowp vec3 lightDir_7;
					  mediump vec3 tmpvar_8;
					  tmpvar_8 = _WorldSpaceLightPos0.xyz;
					  lightDir_7 = tmpvar_8;
					  lowp vec3 tmpvar_9;
					  lowp vec3 tmpvar_10;
					  lowp float tmpvar_11;
					  tmpvar_10 = tmpvar_6;
					  highp vec4 tempNorm_12;
					  lowp vec4 tmpvar_13;
					  tmpvar_13 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  lowp vec4 tmpvar_14;
					  tmpvar_14.w = 0.0;
					  tmpvar_14.xyz = tmpvar_13.xyz;
					  highp vec3 tmpvar_15;
					  tmpvar_15 = mix (tmpvar_14, _Color, vec4(_Fade)).xyz;
					  tmpvar_9 = tmpvar_15;
					  tmpvar_11 = tmpvar_13.w;
					  lowp vec4 tmpvar_16;
					  tmpvar_16 = texture2D (_BumpMap, xlv_TEXCOORD0.zw);
					  tempNorm_12 = tmpvar_16;
					  if ((_Inv == 1.0)) {
					    tempNorm_12.xy = (vec2(1.0, 1.0) - tempNorm_12.xy);
					  };
					  highp vec3 tmpvar_17;
					  tmpvar_17 = ((tempNorm_12 * 2.0) - 1.0).xyz;
					  tmpvar_10 = tmpvar_17;
					  lowp float tmpvar_18;
					  highp float lightShadowDataX_19;
					  mediump float tmpvar_20;
					  tmpvar_20 = _LightShadowData.x;
					  lightShadowDataX_19 = tmpvar_20;
					  highp float tmpvar_21;
					  tmpvar_21 = max (float((texture2D (_ShadowMapTexture, xlv_TEXCOORD5.xy).x > xlv_TEXCOORD5.z)), lightShadowDataX_19);
					  tmpvar_18 = tmpvar_21;
					  highp float tmpvar_22;
					  tmpvar_22 = dot (xlv_TEXCOORD1.xyz, tmpvar_10);
					  worldN_4.x = tmpvar_22;
					  highp float tmpvar_23;
					  tmpvar_23 = dot (xlv_TEXCOORD2.xyz, tmpvar_10);
					  worldN_4.y = tmpvar_23;
					  highp float tmpvar_24;
					  tmpvar_24 = dot (xlv_TEXCOORD3.xyz, tmpvar_10);
					  worldN_4.z = tmpvar_24;
					  tmpvar_6 = worldN_4;
					  tmpvar_2 = _LightColor0.xyz;
					  tmpvar_3 = lightDir_7;
					  tmpvar_1 = tmpvar_18;
					  mediump vec3 tmpvar_25;
					  tmpvar_25 = (tmpvar_2 * tmpvar_1);
					  tmpvar_2 = tmpvar_25;
					  lowp vec4 c_26;
					  lowp vec4 c_27;
					  lowp float diff_28;
					  mediump float tmpvar_29;
					  tmpvar_29 = max (0.0, dot (worldN_4, tmpvar_3));
					  diff_28 = tmpvar_29;
					  c_27.xyz = ((tmpvar_9 * tmpvar_25) * diff_28);
					  c_27.w = tmpvar_11;
					  c_26.w = c_27.w;
					  c_26.xyz = (c_27.xyz + (tmpvar_9 * xlv_TEXCOORD4));
					  c_5.xyz = c_26.xyz;
					  c_5.w = 1.0;
					  gl_FragData[0] = c_5;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesTANGENT;
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
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
					uniform highp vec4 unity_WorldTransformParams;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _BumpMap_ST;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying highp vec4 xlv_TEXCOORD5;
					void main ()
					{
					  lowp float tangentSign_1;
					  lowp vec3 worldTangent_2;
					  lowp vec3 worldNormal_3;
					  highp vec4 tmpvar_4;
					  mediump vec3 tmpvar_5;
					  highp vec4 tmpvar_6;
					  tmpvar_6.w = 1.0;
					  tmpvar_6.xyz = _glesVertex.xyz;
					  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
					  highp vec3 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  tmpvar_7 = tmpvar_8.xyz;
					  highp vec4 v_9;
					  v_9.x = unity_WorldToObject[0].x;
					  v_9.y = unity_WorldToObject[1].x;
					  v_9.z = unity_WorldToObject[2].x;
					  v_9.w = unity_WorldToObject[3].x;
					  highp vec4 v_10;
					  v_10.x = unity_WorldToObject[0].y;
					  v_10.y = unity_WorldToObject[1].y;
					  v_10.z = unity_WorldToObject[2].y;
					  v_10.w = unity_WorldToObject[3].y;
					  highp vec4 v_11;
					  v_11.x = unity_WorldToObject[0].z;
					  v_11.y = unity_WorldToObject[1].z;
					  v_11.z = unity_WorldToObject[2].z;
					  v_11.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_12;
					  tmpvar_12 = normalize(((
					    (v_9.xyz * _glesNormal.x)
					   + 
					    (v_10.xyz * _glesNormal.y)
					  ) + (v_11.xyz * _glesNormal.z)));
					  worldNormal_3 = tmpvar_12;
					  highp mat3 tmpvar_13;
					  tmpvar_13[0] = unity_ObjectToWorld[0].xyz;
					  tmpvar_13[1] = unity_ObjectToWorld[1].xyz;
					  tmpvar_13[2] = unity_ObjectToWorld[2].xyz;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_13 * _glesTANGENT.xyz));
					  worldTangent_2 = tmpvar_14;
					  highp float tmpvar_15;
					  tmpvar_15 = (_glesTANGENT.w * unity_WorldTransformParams.w);
					  tangentSign_1 = tmpvar_15;
					  lowp vec3 tmpvar_16;
					  tmpvar_16 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
					  highp vec4 tmpvar_17;
					  tmpvar_17.x = worldTangent_2.x;
					  tmpvar_17.y = tmpvar_16.x;
					  tmpvar_17.z = worldNormal_3.x;
					  tmpvar_17.w = tmpvar_7.x;
					  highp vec4 tmpvar_18;
					  tmpvar_18.x = worldTangent_2.y;
					  tmpvar_18.y = tmpvar_16.y;
					  tmpvar_18.z = worldNormal_3.y;
					  tmpvar_18.w = tmpvar_7.y;
					  highp vec4 tmpvar_19;
					  tmpvar_19.x = worldTangent_2.z;
					  tmpvar_19.y = tmpvar_16.z;
					  tmpvar_19.z = worldNormal_3.z;
					  tmpvar_19.w = tmpvar_7.z;
					  highp vec3 lightColor0_20;
					  lightColor0_20 = unity_LightColor[0].xyz;
					  highp vec3 lightColor1_21;
					  lightColor1_21 = unity_LightColor[1].xyz;
					  highp vec3 lightColor2_22;
					  lightColor2_22 = unity_LightColor[2].xyz;
					  highp vec3 lightColor3_23;
					  lightColor3_23 = unity_LightColor[3].xyz;
					  highp vec4 lightAttenSq_24;
					  lightAttenSq_24 = unity_4LightAtten0;
					  highp vec3 normal_25;
					  normal_25 = worldNormal_3;
					  highp vec3 col_26;
					  highp vec4 ndotl_27;
					  highp vec4 lengthSq_28;
					  highp vec4 tmpvar_29;
					  tmpvar_29 = (unity_4LightPosX0 - tmpvar_8.x);
					  highp vec4 tmpvar_30;
					  tmpvar_30 = (unity_4LightPosY0 - tmpvar_8.y);
					  highp vec4 tmpvar_31;
					  tmpvar_31 = (unity_4LightPosZ0 - tmpvar_8.z);
					  lengthSq_28 = (tmpvar_29 * tmpvar_29);
					  lengthSq_28 = (lengthSq_28 + (tmpvar_30 * tmpvar_30));
					  lengthSq_28 = (lengthSq_28 + (tmpvar_31 * tmpvar_31));
					  ndotl_27 = (tmpvar_29 * normal_25.x);
					  ndotl_27 = (ndotl_27 + (tmpvar_30 * normal_25.y));
					  ndotl_27 = (ndotl_27 + (tmpvar_31 * normal_25.z));
					  highp vec4 tmpvar_32;
					  tmpvar_32 = max (vec4(0.0, 0.0, 0.0, 0.0), (ndotl_27 * inversesqrt(lengthSq_28)));
					  ndotl_27 = tmpvar_32;
					  highp vec4 tmpvar_33;
					  tmpvar_33 = (tmpvar_32 * (1.0/((1.0 + 
					    (lengthSq_28 * lightAttenSq_24)
					  ))));
					  col_26 = (lightColor0_20 * tmpvar_33.x);
					  col_26 = (col_26 + (lightColor1_21 * tmpvar_33.y));
					  col_26 = (col_26 + (lightColor2_22 * tmpvar_33.z));
					  col_26 = (col_26 + (lightColor3_23 * tmpvar_33.w));
					  tmpvar_5 = col_26;
					  mediump vec3 normal_34;
					  normal_34 = worldNormal_3;
					  mediump vec3 ambient_35;
					  mediump vec4 tmpvar_36;
					  tmpvar_36.w = 1.0;
					  tmpvar_36.xyz = normal_34;
					  mediump vec3 res_37;
					  mediump vec3 x_38;
					  x_38.x = dot (unity_SHAr, tmpvar_36);
					  x_38.y = dot (unity_SHAg, tmpvar_36);
					  x_38.z = dot (unity_SHAb, tmpvar_36);
					  mediump vec3 x1_39;
					  mediump vec4 tmpvar_40;
					  tmpvar_40 = (normal_34.xyzz * normal_34.yzzx);
					  x1_39.x = dot (unity_SHBr, tmpvar_40);
					  x1_39.y = dot (unity_SHBg, tmpvar_40);
					  x1_39.z = dot (unity_SHBb, tmpvar_40);
					  res_37 = (x_38 + (x1_39 + (unity_SHC.xyz * 
					    ((normal_34.x * normal_34.x) - (normal_34.y * normal_34.y))
					  )));
					  res_37 = max (((1.055 * 
					    pow (max (res_37, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  ambient_35 = (tmpvar_5 + max (vec3(0.0, 0.0, 0.0), res_37));
					  tmpvar_5 = ambient_35;
					  gl_Position = (glstate_matrix_mvp * tmpvar_6);
					  xlv_TEXCOORD0 = tmpvar_4;
					  xlv_TEXCOORD1 = tmpvar_17;
					  xlv_TEXCOORD2 = tmpvar_18;
					  xlv_TEXCOORD3 = tmpvar_19;
					  xlv_TEXCOORD4 = ambient_35;
					  xlv_TEXCOORD5 = (unity_WorldToShadow[0] * tmpvar_8);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform mediump vec4 _LightShadowData;
					uniform lowp vec4 _LightColor0;
					uniform highp sampler2D _ShadowMapTexture;
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying highp vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump float tmpvar_1;
					  mediump vec3 tmpvar_2;
					  mediump vec3 tmpvar_3;
					  lowp vec3 worldN_4;
					  lowp vec4 c_5;
					  lowp vec3 tmpvar_6;
					  lowp vec3 lightDir_7;
					  mediump vec3 tmpvar_8;
					  tmpvar_8 = _WorldSpaceLightPos0.xyz;
					  lightDir_7 = tmpvar_8;
					  lowp vec3 tmpvar_9;
					  lowp vec3 tmpvar_10;
					  lowp float tmpvar_11;
					  tmpvar_10 = tmpvar_6;
					  highp vec4 tempNorm_12;
					  lowp vec4 tmpvar_13;
					  tmpvar_13 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  lowp vec4 tmpvar_14;
					  tmpvar_14.w = 0.0;
					  tmpvar_14.xyz = tmpvar_13.xyz;
					  highp vec3 tmpvar_15;
					  tmpvar_15 = mix (tmpvar_14, _Color, vec4(_Fade)).xyz;
					  tmpvar_9 = tmpvar_15;
					  tmpvar_11 = tmpvar_13.w;
					  lowp vec4 tmpvar_16;
					  tmpvar_16 = texture2D (_BumpMap, xlv_TEXCOORD0.zw);
					  tempNorm_12 = tmpvar_16;
					  if ((_Inv == 1.0)) {
					    tempNorm_12.xy = (vec2(1.0, 1.0) - tempNorm_12.xy);
					  };
					  highp vec3 tmpvar_17;
					  tmpvar_17 = ((tempNorm_12 * 2.0) - 1.0).xyz;
					  tmpvar_10 = tmpvar_17;
					  lowp float tmpvar_18;
					  highp float lightShadowDataX_19;
					  mediump float tmpvar_20;
					  tmpvar_20 = _LightShadowData.x;
					  lightShadowDataX_19 = tmpvar_20;
					  highp float tmpvar_21;
					  tmpvar_21 = max (float((texture2D (_ShadowMapTexture, xlv_TEXCOORD5.xy).x > xlv_TEXCOORD5.z)), lightShadowDataX_19);
					  tmpvar_18 = tmpvar_21;
					  highp float tmpvar_22;
					  tmpvar_22 = dot (xlv_TEXCOORD1.xyz, tmpvar_10);
					  worldN_4.x = tmpvar_22;
					  highp float tmpvar_23;
					  tmpvar_23 = dot (xlv_TEXCOORD2.xyz, tmpvar_10);
					  worldN_4.y = tmpvar_23;
					  highp float tmpvar_24;
					  tmpvar_24 = dot (xlv_TEXCOORD3.xyz, tmpvar_10);
					  worldN_4.z = tmpvar_24;
					  tmpvar_6 = worldN_4;
					  tmpvar_2 = _LightColor0.xyz;
					  tmpvar_3 = lightDir_7;
					  tmpvar_1 = tmpvar_18;
					  mediump vec3 tmpvar_25;
					  tmpvar_25 = (tmpvar_2 * tmpvar_1);
					  tmpvar_2 = tmpvar_25;
					  lowp vec4 c_26;
					  lowp vec4 c_27;
					  lowp float diff_28;
					  mediump float tmpvar_29;
					  tmpvar_29 = max (0.0, dot (worldN_4, tmpvar_3));
					  diff_28 = tmpvar_29;
					  c_27.xyz = ((tmpvar_9 * tmpvar_25) * diff_28);
					  c_27.w = tmpvar_11;
					  c_26.w = c_27.w;
					  c_26.xyz = (c_27.xyz + (tmpvar_9 * xlv_TEXCOORD4));
					  c_5.xyz = c_26.xyz;
					  c_5.w = 1.0;
					  gl_FragData[0] = c_5;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesTANGENT;
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
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
					uniform highp vec4 unity_WorldTransformParams;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _BumpMap_ST;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying highp vec4 xlv_TEXCOORD5;
					void main ()
					{
					  lowp float tangentSign_1;
					  lowp vec3 worldTangent_2;
					  lowp vec3 worldNormal_3;
					  highp vec4 tmpvar_4;
					  mediump vec3 tmpvar_5;
					  highp vec4 tmpvar_6;
					  tmpvar_6.w = 1.0;
					  tmpvar_6.xyz = _glesVertex.xyz;
					  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
					  highp vec3 tmpvar_7;
					  highp vec4 tmpvar_8;
					  tmpvar_8 = (unity_ObjectToWorld * _glesVertex);
					  tmpvar_7 = tmpvar_8.xyz;
					  highp vec4 v_9;
					  v_9.x = unity_WorldToObject[0].x;
					  v_9.y = unity_WorldToObject[1].x;
					  v_9.z = unity_WorldToObject[2].x;
					  v_9.w = unity_WorldToObject[3].x;
					  highp vec4 v_10;
					  v_10.x = unity_WorldToObject[0].y;
					  v_10.y = unity_WorldToObject[1].y;
					  v_10.z = unity_WorldToObject[2].y;
					  v_10.w = unity_WorldToObject[3].y;
					  highp vec4 v_11;
					  v_11.x = unity_WorldToObject[0].z;
					  v_11.y = unity_WorldToObject[1].z;
					  v_11.z = unity_WorldToObject[2].z;
					  v_11.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_12;
					  tmpvar_12 = normalize(((
					    (v_9.xyz * _glesNormal.x)
					   + 
					    (v_10.xyz * _glesNormal.y)
					  ) + (v_11.xyz * _glesNormal.z)));
					  worldNormal_3 = tmpvar_12;
					  highp mat3 tmpvar_13;
					  tmpvar_13[0] = unity_ObjectToWorld[0].xyz;
					  tmpvar_13[1] = unity_ObjectToWorld[1].xyz;
					  tmpvar_13[2] = unity_ObjectToWorld[2].xyz;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize((tmpvar_13 * _glesTANGENT.xyz));
					  worldTangent_2 = tmpvar_14;
					  highp float tmpvar_15;
					  tmpvar_15 = (_glesTANGENT.w * unity_WorldTransformParams.w);
					  tangentSign_1 = tmpvar_15;
					  lowp vec3 tmpvar_16;
					  tmpvar_16 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
					  highp vec4 tmpvar_17;
					  tmpvar_17.x = worldTangent_2.x;
					  tmpvar_17.y = tmpvar_16.x;
					  tmpvar_17.z = worldNormal_3.x;
					  tmpvar_17.w = tmpvar_7.x;
					  highp vec4 tmpvar_18;
					  tmpvar_18.x = worldTangent_2.y;
					  tmpvar_18.y = tmpvar_16.y;
					  tmpvar_18.z = worldNormal_3.y;
					  tmpvar_18.w = tmpvar_7.y;
					  highp vec4 tmpvar_19;
					  tmpvar_19.x = worldTangent_2.z;
					  tmpvar_19.y = tmpvar_16.z;
					  tmpvar_19.z = worldNormal_3.z;
					  tmpvar_19.w = tmpvar_7.z;
					  highp vec3 lightColor0_20;
					  lightColor0_20 = unity_LightColor[0].xyz;
					  highp vec3 lightColor1_21;
					  lightColor1_21 = unity_LightColor[1].xyz;
					  highp vec3 lightColor2_22;
					  lightColor2_22 = unity_LightColor[2].xyz;
					  highp vec3 lightColor3_23;
					  lightColor3_23 = unity_LightColor[3].xyz;
					  highp vec4 lightAttenSq_24;
					  lightAttenSq_24 = unity_4LightAtten0;
					  highp vec3 normal_25;
					  normal_25 = worldNormal_3;
					  highp vec3 col_26;
					  highp vec4 ndotl_27;
					  highp vec4 lengthSq_28;
					  highp vec4 tmpvar_29;
					  tmpvar_29 = (unity_4LightPosX0 - tmpvar_8.x);
					  highp vec4 tmpvar_30;
					  tmpvar_30 = (unity_4LightPosY0 - tmpvar_8.y);
					  highp vec4 tmpvar_31;
					  tmpvar_31 = (unity_4LightPosZ0 - tmpvar_8.z);
					  lengthSq_28 = (tmpvar_29 * tmpvar_29);
					  lengthSq_28 = (lengthSq_28 + (tmpvar_30 * tmpvar_30));
					  lengthSq_28 = (lengthSq_28 + (tmpvar_31 * tmpvar_31));
					  ndotl_27 = (tmpvar_29 * normal_25.x);
					  ndotl_27 = (ndotl_27 + (tmpvar_30 * normal_25.y));
					  ndotl_27 = (ndotl_27 + (tmpvar_31 * normal_25.z));
					  highp vec4 tmpvar_32;
					  tmpvar_32 = max (vec4(0.0, 0.0, 0.0, 0.0), (ndotl_27 * inversesqrt(lengthSq_28)));
					  ndotl_27 = tmpvar_32;
					  highp vec4 tmpvar_33;
					  tmpvar_33 = (tmpvar_32 * (1.0/((1.0 + 
					    (lengthSq_28 * lightAttenSq_24)
					  ))));
					  col_26 = (lightColor0_20 * tmpvar_33.x);
					  col_26 = (col_26 + (lightColor1_21 * tmpvar_33.y));
					  col_26 = (col_26 + (lightColor2_22 * tmpvar_33.z));
					  col_26 = (col_26 + (lightColor3_23 * tmpvar_33.w));
					  tmpvar_5 = col_26;
					  mediump vec3 normal_34;
					  normal_34 = worldNormal_3;
					  mediump vec3 ambient_35;
					  mediump vec4 tmpvar_36;
					  tmpvar_36.w = 1.0;
					  tmpvar_36.xyz = normal_34;
					  mediump vec3 res_37;
					  mediump vec3 x_38;
					  x_38.x = dot (unity_SHAr, tmpvar_36);
					  x_38.y = dot (unity_SHAg, tmpvar_36);
					  x_38.z = dot (unity_SHAb, tmpvar_36);
					  mediump vec3 x1_39;
					  mediump vec4 tmpvar_40;
					  tmpvar_40 = (normal_34.xyzz * normal_34.yzzx);
					  x1_39.x = dot (unity_SHBr, tmpvar_40);
					  x1_39.y = dot (unity_SHBg, tmpvar_40);
					  x1_39.z = dot (unity_SHBb, tmpvar_40);
					  res_37 = (x_38 + (x1_39 + (unity_SHC.xyz * 
					    ((normal_34.x * normal_34.x) - (normal_34.y * normal_34.y))
					  )));
					  res_37 = max (((1.055 * 
					    pow (max (res_37, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  ambient_35 = (tmpvar_5 + max (vec3(0.0, 0.0, 0.0), res_37));
					  tmpvar_5 = ambient_35;
					  gl_Position = (glstate_matrix_mvp * tmpvar_6);
					  xlv_TEXCOORD0 = tmpvar_4;
					  xlv_TEXCOORD1 = tmpvar_17;
					  xlv_TEXCOORD2 = tmpvar_18;
					  xlv_TEXCOORD3 = tmpvar_19;
					  xlv_TEXCOORD4 = ambient_35;
					  xlv_TEXCOORD5 = (unity_WorldToShadow[0] * tmpvar_8);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform mediump vec4 _LightShadowData;
					uniform lowp vec4 _LightColor0;
					uniform highp sampler2D _ShadowMapTexture;
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying mediump vec3 xlv_TEXCOORD4;
					varying highp vec4 xlv_TEXCOORD5;
					void main ()
					{
					  mediump float tmpvar_1;
					  mediump vec3 tmpvar_2;
					  mediump vec3 tmpvar_3;
					  lowp vec3 worldN_4;
					  lowp vec4 c_5;
					  lowp vec3 tmpvar_6;
					  lowp vec3 lightDir_7;
					  mediump vec3 tmpvar_8;
					  tmpvar_8 = _WorldSpaceLightPos0.xyz;
					  lightDir_7 = tmpvar_8;
					  lowp vec3 tmpvar_9;
					  lowp vec3 tmpvar_10;
					  lowp float tmpvar_11;
					  tmpvar_10 = tmpvar_6;
					  highp vec4 tempNorm_12;
					  lowp vec4 tmpvar_13;
					  tmpvar_13 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  lowp vec4 tmpvar_14;
					  tmpvar_14.w = 0.0;
					  tmpvar_14.xyz = tmpvar_13.xyz;
					  highp vec3 tmpvar_15;
					  tmpvar_15 = mix (tmpvar_14, _Color, vec4(_Fade)).xyz;
					  tmpvar_9 = tmpvar_15;
					  tmpvar_11 = tmpvar_13.w;
					  lowp vec4 tmpvar_16;
					  tmpvar_16 = texture2D (_BumpMap, xlv_TEXCOORD0.zw);
					  tempNorm_12 = tmpvar_16;
					  if ((_Inv == 1.0)) {
					    tempNorm_12.xy = (vec2(1.0, 1.0) - tempNorm_12.xy);
					  };
					  highp vec3 tmpvar_17;
					  tmpvar_17 = ((tempNorm_12 * 2.0) - 1.0).xyz;
					  tmpvar_10 = tmpvar_17;
					  lowp float tmpvar_18;
					  highp float lightShadowDataX_19;
					  mediump float tmpvar_20;
					  tmpvar_20 = _LightShadowData.x;
					  lightShadowDataX_19 = tmpvar_20;
					  highp float tmpvar_21;
					  tmpvar_21 = max (float((texture2D (_ShadowMapTexture, xlv_TEXCOORD5.xy).x > xlv_TEXCOORD5.z)), lightShadowDataX_19);
					  tmpvar_18 = tmpvar_21;
					  highp float tmpvar_22;
					  tmpvar_22 = dot (xlv_TEXCOORD1.xyz, tmpvar_10);
					  worldN_4.x = tmpvar_22;
					  highp float tmpvar_23;
					  tmpvar_23 = dot (xlv_TEXCOORD2.xyz, tmpvar_10);
					  worldN_4.y = tmpvar_23;
					  highp float tmpvar_24;
					  tmpvar_24 = dot (xlv_TEXCOORD3.xyz, tmpvar_10);
					  worldN_4.z = tmpvar_24;
					  tmpvar_6 = worldN_4;
					  tmpvar_2 = _LightColor0.xyz;
					  tmpvar_3 = lightDir_7;
					  tmpvar_1 = tmpvar_18;
					  mediump vec3 tmpvar_25;
					  tmpvar_25 = (tmpvar_2 * tmpvar_1);
					  tmpvar_2 = tmpvar_25;
					  lowp vec4 c_26;
					  lowp vec4 c_27;
					  lowp float diff_28;
					  mediump float tmpvar_29;
					  tmpvar_29 = max (0.0, dot (worldN_4, tmpvar_3));
					  diff_28 = tmpvar_29;
					  c_27.xyz = ((tmpvar_9 * tmpvar_25) * diff_28);
					  c_27.w = tmpvar_11;
					  c_26.w = c_27.w;
					  c_26.xyz = (c_27.xyz + (tmpvar_9 * xlv_TEXCOORD4));
					  c_5.xyz = c_26.xyz;
					  c_5.w = 1.0;
					  gl_FragData[0] = c_5;
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
  Name "FORWARD"
  Tags { "LIGHTMODE"="ForwardAdd" "RenderType"="Opaque" }
  ZWrite Off
  Blend One One
  GpuProgramID 125713
Program "vp" {
SubProgram "gles hw_tier01 " {
Keywords { "POINT" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesTANGENT;
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 unity_WorldTransformParams;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _BumpMap_ST;
					varying highp vec4 xlv_TEXCOORD0;
					varying lowp vec3 xlv_TEXCOORD1;
					varying lowp vec3 xlv_TEXCOORD2;
					varying lowp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  lowp float tangentSign_1;
					  lowp vec3 worldTangent_2;
					  lowp vec3 worldNormal_3;
					  highp vec4 tmpvar_4;
					  highp vec4 tmpvar_5;
					  tmpvar_5.w = 1.0;
					  tmpvar_5.xyz = _glesVertex.xyz;
					  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
					  highp vec4 v_6;
					  v_6.x = unity_WorldToObject[0].x;
					  v_6.y = unity_WorldToObject[1].x;
					  v_6.z = unity_WorldToObject[2].x;
					  v_6.w = unity_WorldToObject[3].x;
					  highp vec4 v_7;
					  v_7.x = unity_WorldToObject[0].y;
					  v_7.y = unity_WorldToObject[1].y;
					  v_7.z = unity_WorldToObject[2].y;
					  v_7.w = unity_WorldToObject[3].y;
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].z;
					  v_8.y = unity_WorldToObject[1].z;
					  v_8.z = unity_WorldToObject[2].z;
					  v_8.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_9;
					  tmpvar_9 = normalize(((
					    (v_6.xyz * _glesNormal.x)
					   + 
					    (v_7.xyz * _glesNormal.y)
					  ) + (v_8.xyz * _glesNormal.z)));
					  worldNormal_3 = tmpvar_9;
					  highp mat3 tmpvar_10;
					  tmpvar_10[0] = unity_ObjectToWorld[0].xyz;
					  tmpvar_10[1] = unity_ObjectToWorld[1].xyz;
					  tmpvar_10[2] = unity_ObjectToWorld[2].xyz;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = normalize((tmpvar_10 * _glesTANGENT.xyz));
					  worldTangent_2 = tmpvar_11;
					  highp float tmpvar_12;
					  tmpvar_12 = (_glesTANGENT.w * unity_WorldTransformParams.w);
					  tangentSign_1 = tmpvar_12;
					  lowp vec3 tmpvar_13;
					  tmpvar_13 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
					  lowp vec3 tmpvar_14;
					  tmpvar_14.x = worldTangent_2.x;
					  tmpvar_14.y = tmpvar_13.x;
					  tmpvar_14.z = worldNormal_3.x;
					  lowp vec3 tmpvar_15;
					  tmpvar_15.x = worldTangent_2.y;
					  tmpvar_15.y = tmpvar_13.y;
					  tmpvar_15.z = worldNormal_3.y;
					  lowp vec3 tmpvar_16;
					  tmpvar_16.x = worldTangent_2.z;
					  tmpvar_16.y = tmpvar_13.z;
					  tmpvar_16.z = worldNormal_3.z;
					  gl_Position = (glstate_matrix_mvp * tmpvar_5);
					  xlv_TEXCOORD0 = tmpvar_4;
					  xlv_TEXCOORD1 = tmpvar_14;
					  xlv_TEXCOORD2 = tmpvar_15;
					  xlv_TEXCOORD3 = tmpvar_16;
					  xlv_TEXCOORD4 = (unity_ObjectToWorld * _glesVertex).xyz;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D _LightTexture0;
					uniform highp mat4 unity_WorldToLight;
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					varying highp vec4 xlv_TEXCOORD0;
					varying lowp vec3 xlv_TEXCOORD1;
					varying lowp vec3 xlv_TEXCOORD2;
					varying lowp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  mediump vec3 tmpvar_2;
					  lowp vec3 worldN_3;
					  lowp vec4 c_4;
					  lowp vec3 tmpvar_5;
					  lowp vec3 lightDir_6;
					  highp vec3 tmpvar_7;
					  tmpvar_7 = normalize((_WorldSpaceLightPos0.xyz - xlv_TEXCOORD4));
					  lightDir_6 = tmpvar_7;
					  lowp vec3 tmpvar_8;
					  lowp vec3 tmpvar_9;
					  lowp float tmpvar_10;
					  tmpvar_9 = tmpvar_5;
					  highp vec4 tempNorm_11;
					  lowp vec4 tmpvar_12;
					  tmpvar_12 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  lowp vec4 tmpvar_13;
					  tmpvar_13.w = 0.0;
					  tmpvar_13.xyz = tmpvar_12.xyz;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = mix (tmpvar_13, _Color, vec4(_Fade)).xyz;
					  tmpvar_8 = tmpvar_14;
					  tmpvar_10 = tmpvar_12.w;
					  lowp vec4 tmpvar_15;
					  tmpvar_15 = texture2D (_BumpMap, xlv_TEXCOORD0.zw);
					  tempNorm_11 = tmpvar_15;
					  if ((_Inv == 1.0)) {
					    tempNorm_11.xy = (vec2(1.0, 1.0) - tempNorm_11.xy);
					  };
					  highp vec3 tmpvar_16;
					  tmpvar_16 = ((tempNorm_11 * 2.0) - 1.0).xyz;
					  tmpvar_9 = tmpvar_16;
					  highp vec4 tmpvar_17;
					  tmpvar_17.w = 1.0;
					  tmpvar_17.xyz = xlv_TEXCOORD4;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = (unity_WorldToLight * tmpvar_17).xyz;
					  highp float tmpvar_19;
					  tmpvar_19 = dot (tmpvar_18, tmpvar_18);
					  lowp float tmpvar_20;
					  tmpvar_20 = texture2D (_LightTexture0, vec2(tmpvar_19)).w;
					  worldN_3.x = dot (xlv_TEXCOORD1, tmpvar_9);
					  worldN_3.y = dot (xlv_TEXCOORD2, tmpvar_9);
					  worldN_3.z = dot (xlv_TEXCOORD3, tmpvar_9);
					  tmpvar_5 = worldN_3;
					  tmpvar_1 = _LightColor0.xyz;
					  tmpvar_2 = lightDir_6;
					  tmpvar_1 = (tmpvar_1 * tmpvar_20);
					  lowp vec4 c_21;
					  lowp vec4 c_22;
					  lowp float diff_23;
					  mediump float tmpvar_24;
					  tmpvar_24 = max (0.0, dot (worldN_3, tmpvar_2));
					  diff_23 = tmpvar_24;
					  c_22.xyz = ((tmpvar_8 * tmpvar_1) * diff_23);
					  c_22.w = tmpvar_10;
					  c_21.w = c_22.w;
					  c_21.xyz = c_22.xyz;
					  c_4.xyz = c_21.xyz;
					  c_4.w = 1.0;
					  gl_FragData[0] = c_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "POINT" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesTANGENT;
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 unity_WorldTransformParams;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _BumpMap_ST;
					varying highp vec4 xlv_TEXCOORD0;
					varying lowp vec3 xlv_TEXCOORD1;
					varying lowp vec3 xlv_TEXCOORD2;
					varying lowp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  lowp float tangentSign_1;
					  lowp vec3 worldTangent_2;
					  lowp vec3 worldNormal_3;
					  highp vec4 tmpvar_4;
					  highp vec4 tmpvar_5;
					  tmpvar_5.w = 1.0;
					  tmpvar_5.xyz = _glesVertex.xyz;
					  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
					  highp vec4 v_6;
					  v_6.x = unity_WorldToObject[0].x;
					  v_6.y = unity_WorldToObject[1].x;
					  v_6.z = unity_WorldToObject[2].x;
					  v_6.w = unity_WorldToObject[3].x;
					  highp vec4 v_7;
					  v_7.x = unity_WorldToObject[0].y;
					  v_7.y = unity_WorldToObject[1].y;
					  v_7.z = unity_WorldToObject[2].y;
					  v_7.w = unity_WorldToObject[3].y;
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].z;
					  v_8.y = unity_WorldToObject[1].z;
					  v_8.z = unity_WorldToObject[2].z;
					  v_8.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_9;
					  tmpvar_9 = normalize(((
					    (v_6.xyz * _glesNormal.x)
					   + 
					    (v_7.xyz * _glesNormal.y)
					  ) + (v_8.xyz * _glesNormal.z)));
					  worldNormal_3 = tmpvar_9;
					  highp mat3 tmpvar_10;
					  tmpvar_10[0] = unity_ObjectToWorld[0].xyz;
					  tmpvar_10[1] = unity_ObjectToWorld[1].xyz;
					  tmpvar_10[2] = unity_ObjectToWorld[2].xyz;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = normalize((tmpvar_10 * _glesTANGENT.xyz));
					  worldTangent_2 = tmpvar_11;
					  highp float tmpvar_12;
					  tmpvar_12 = (_glesTANGENT.w * unity_WorldTransformParams.w);
					  tangentSign_1 = tmpvar_12;
					  lowp vec3 tmpvar_13;
					  tmpvar_13 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
					  lowp vec3 tmpvar_14;
					  tmpvar_14.x = worldTangent_2.x;
					  tmpvar_14.y = tmpvar_13.x;
					  tmpvar_14.z = worldNormal_3.x;
					  lowp vec3 tmpvar_15;
					  tmpvar_15.x = worldTangent_2.y;
					  tmpvar_15.y = tmpvar_13.y;
					  tmpvar_15.z = worldNormal_3.y;
					  lowp vec3 tmpvar_16;
					  tmpvar_16.x = worldTangent_2.z;
					  tmpvar_16.y = tmpvar_13.z;
					  tmpvar_16.z = worldNormal_3.z;
					  gl_Position = (glstate_matrix_mvp * tmpvar_5);
					  xlv_TEXCOORD0 = tmpvar_4;
					  xlv_TEXCOORD1 = tmpvar_14;
					  xlv_TEXCOORD2 = tmpvar_15;
					  xlv_TEXCOORD3 = tmpvar_16;
					  xlv_TEXCOORD4 = (unity_ObjectToWorld * _glesVertex).xyz;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D _LightTexture0;
					uniform highp mat4 unity_WorldToLight;
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					varying highp vec4 xlv_TEXCOORD0;
					varying lowp vec3 xlv_TEXCOORD1;
					varying lowp vec3 xlv_TEXCOORD2;
					varying lowp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  mediump vec3 tmpvar_2;
					  lowp vec3 worldN_3;
					  lowp vec4 c_4;
					  lowp vec3 tmpvar_5;
					  lowp vec3 lightDir_6;
					  highp vec3 tmpvar_7;
					  tmpvar_7 = normalize((_WorldSpaceLightPos0.xyz - xlv_TEXCOORD4));
					  lightDir_6 = tmpvar_7;
					  lowp vec3 tmpvar_8;
					  lowp vec3 tmpvar_9;
					  lowp float tmpvar_10;
					  tmpvar_9 = tmpvar_5;
					  highp vec4 tempNorm_11;
					  lowp vec4 tmpvar_12;
					  tmpvar_12 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  lowp vec4 tmpvar_13;
					  tmpvar_13.w = 0.0;
					  tmpvar_13.xyz = tmpvar_12.xyz;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = mix (tmpvar_13, _Color, vec4(_Fade)).xyz;
					  tmpvar_8 = tmpvar_14;
					  tmpvar_10 = tmpvar_12.w;
					  lowp vec4 tmpvar_15;
					  tmpvar_15 = texture2D (_BumpMap, xlv_TEXCOORD0.zw);
					  tempNorm_11 = tmpvar_15;
					  if ((_Inv == 1.0)) {
					    tempNorm_11.xy = (vec2(1.0, 1.0) - tempNorm_11.xy);
					  };
					  highp vec3 tmpvar_16;
					  tmpvar_16 = ((tempNorm_11 * 2.0) - 1.0).xyz;
					  tmpvar_9 = tmpvar_16;
					  highp vec4 tmpvar_17;
					  tmpvar_17.w = 1.0;
					  tmpvar_17.xyz = xlv_TEXCOORD4;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = (unity_WorldToLight * tmpvar_17).xyz;
					  highp float tmpvar_19;
					  tmpvar_19 = dot (tmpvar_18, tmpvar_18);
					  lowp float tmpvar_20;
					  tmpvar_20 = texture2D (_LightTexture0, vec2(tmpvar_19)).w;
					  worldN_3.x = dot (xlv_TEXCOORD1, tmpvar_9);
					  worldN_3.y = dot (xlv_TEXCOORD2, tmpvar_9);
					  worldN_3.z = dot (xlv_TEXCOORD3, tmpvar_9);
					  tmpvar_5 = worldN_3;
					  tmpvar_1 = _LightColor0.xyz;
					  tmpvar_2 = lightDir_6;
					  tmpvar_1 = (tmpvar_1 * tmpvar_20);
					  lowp vec4 c_21;
					  lowp vec4 c_22;
					  lowp float diff_23;
					  mediump float tmpvar_24;
					  tmpvar_24 = max (0.0, dot (worldN_3, tmpvar_2));
					  diff_23 = tmpvar_24;
					  c_22.xyz = ((tmpvar_8 * tmpvar_1) * diff_23);
					  c_22.w = tmpvar_10;
					  c_21.w = c_22.w;
					  c_21.xyz = c_22.xyz;
					  c_4.xyz = c_21.xyz;
					  c_4.w = 1.0;
					  gl_FragData[0] = c_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "POINT" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesTANGENT;
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 unity_WorldTransformParams;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _BumpMap_ST;
					varying highp vec4 xlv_TEXCOORD0;
					varying lowp vec3 xlv_TEXCOORD1;
					varying lowp vec3 xlv_TEXCOORD2;
					varying lowp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  lowp float tangentSign_1;
					  lowp vec3 worldTangent_2;
					  lowp vec3 worldNormal_3;
					  highp vec4 tmpvar_4;
					  highp vec4 tmpvar_5;
					  tmpvar_5.w = 1.0;
					  tmpvar_5.xyz = _glesVertex.xyz;
					  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
					  highp vec4 v_6;
					  v_6.x = unity_WorldToObject[0].x;
					  v_6.y = unity_WorldToObject[1].x;
					  v_6.z = unity_WorldToObject[2].x;
					  v_6.w = unity_WorldToObject[3].x;
					  highp vec4 v_7;
					  v_7.x = unity_WorldToObject[0].y;
					  v_7.y = unity_WorldToObject[1].y;
					  v_7.z = unity_WorldToObject[2].y;
					  v_7.w = unity_WorldToObject[3].y;
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].z;
					  v_8.y = unity_WorldToObject[1].z;
					  v_8.z = unity_WorldToObject[2].z;
					  v_8.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_9;
					  tmpvar_9 = normalize(((
					    (v_6.xyz * _glesNormal.x)
					   + 
					    (v_7.xyz * _glesNormal.y)
					  ) + (v_8.xyz * _glesNormal.z)));
					  worldNormal_3 = tmpvar_9;
					  highp mat3 tmpvar_10;
					  tmpvar_10[0] = unity_ObjectToWorld[0].xyz;
					  tmpvar_10[1] = unity_ObjectToWorld[1].xyz;
					  tmpvar_10[2] = unity_ObjectToWorld[2].xyz;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = normalize((tmpvar_10 * _glesTANGENT.xyz));
					  worldTangent_2 = tmpvar_11;
					  highp float tmpvar_12;
					  tmpvar_12 = (_glesTANGENT.w * unity_WorldTransformParams.w);
					  tangentSign_1 = tmpvar_12;
					  lowp vec3 tmpvar_13;
					  tmpvar_13 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
					  lowp vec3 tmpvar_14;
					  tmpvar_14.x = worldTangent_2.x;
					  tmpvar_14.y = tmpvar_13.x;
					  tmpvar_14.z = worldNormal_3.x;
					  lowp vec3 tmpvar_15;
					  tmpvar_15.x = worldTangent_2.y;
					  tmpvar_15.y = tmpvar_13.y;
					  tmpvar_15.z = worldNormal_3.y;
					  lowp vec3 tmpvar_16;
					  tmpvar_16.x = worldTangent_2.z;
					  tmpvar_16.y = tmpvar_13.z;
					  tmpvar_16.z = worldNormal_3.z;
					  gl_Position = (glstate_matrix_mvp * tmpvar_5);
					  xlv_TEXCOORD0 = tmpvar_4;
					  xlv_TEXCOORD1 = tmpvar_14;
					  xlv_TEXCOORD2 = tmpvar_15;
					  xlv_TEXCOORD3 = tmpvar_16;
					  xlv_TEXCOORD4 = (unity_ObjectToWorld * _glesVertex).xyz;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D _LightTexture0;
					uniform highp mat4 unity_WorldToLight;
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					varying highp vec4 xlv_TEXCOORD0;
					varying lowp vec3 xlv_TEXCOORD1;
					varying lowp vec3 xlv_TEXCOORD2;
					varying lowp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  mediump vec3 tmpvar_2;
					  lowp vec3 worldN_3;
					  lowp vec4 c_4;
					  lowp vec3 tmpvar_5;
					  lowp vec3 lightDir_6;
					  highp vec3 tmpvar_7;
					  tmpvar_7 = normalize((_WorldSpaceLightPos0.xyz - xlv_TEXCOORD4));
					  lightDir_6 = tmpvar_7;
					  lowp vec3 tmpvar_8;
					  lowp vec3 tmpvar_9;
					  lowp float tmpvar_10;
					  tmpvar_9 = tmpvar_5;
					  highp vec4 tempNorm_11;
					  lowp vec4 tmpvar_12;
					  tmpvar_12 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  lowp vec4 tmpvar_13;
					  tmpvar_13.w = 0.0;
					  tmpvar_13.xyz = tmpvar_12.xyz;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = mix (tmpvar_13, _Color, vec4(_Fade)).xyz;
					  tmpvar_8 = tmpvar_14;
					  tmpvar_10 = tmpvar_12.w;
					  lowp vec4 tmpvar_15;
					  tmpvar_15 = texture2D (_BumpMap, xlv_TEXCOORD0.zw);
					  tempNorm_11 = tmpvar_15;
					  if ((_Inv == 1.0)) {
					    tempNorm_11.xy = (vec2(1.0, 1.0) - tempNorm_11.xy);
					  };
					  highp vec3 tmpvar_16;
					  tmpvar_16 = ((tempNorm_11 * 2.0) - 1.0).xyz;
					  tmpvar_9 = tmpvar_16;
					  highp vec4 tmpvar_17;
					  tmpvar_17.w = 1.0;
					  tmpvar_17.xyz = xlv_TEXCOORD4;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = (unity_WorldToLight * tmpvar_17).xyz;
					  highp float tmpvar_19;
					  tmpvar_19 = dot (tmpvar_18, tmpvar_18);
					  lowp float tmpvar_20;
					  tmpvar_20 = texture2D (_LightTexture0, vec2(tmpvar_19)).w;
					  worldN_3.x = dot (xlv_TEXCOORD1, tmpvar_9);
					  worldN_3.y = dot (xlv_TEXCOORD2, tmpvar_9);
					  worldN_3.z = dot (xlv_TEXCOORD3, tmpvar_9);
					  tmpvar_5 = worldN_3;
					  tmpvar_1 = _LightColor0.xyz;
					  tmpvar_2 = lightDir_6;
					  tmpvar_1 = (tmpvar_1 * tmpvar_20);
					  lowp vec4 c_21;
					  lowp vec4 c_22;
					  lowp float diff_23;
					  mediump float tmpvar_24;
					  tmpvar_24 = max (0.0, dot (worldN_3, tmpvar_2));
					  diff_23 = tmpvar_24;
					  c_22.xyz = ((tmpvar_8 * tmpvar_1) * diff_23);
					  c_22.w = tmpvar_10;
					  c_21.w = c_22.w;
					  c_21.xyz = c_22.xyz;
					  c_4.xyz = c_21.xyz;
					  c_4.w = 1.0;
					  gl_FragData[0] = c_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesTANGENT;
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 unity_WorldTransformParams;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _BumpMap_ST;
					varying highp vec4 xlv_TEXCOORD0;
					varying lowp vec3 xlv_TEXCOORD1;
					varying lowp vec3 xlv_TEXCOORD2;
					varying lowp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  lowp float tangentSign_1;
					  lowp vec3 worldTangent_2;
					  lowp vec3 worldNormal_3;
					  highp vec4 tmpvar_4;
					  highp vec4 tmpvar_5;
					  tmpvar_5.w = 1.0;
					  tmpvar_5.xyz = _glesVertex.xyz;
					  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
					  highp vec4 v_6;
					  v_6.x = unity_WorldToObject[0].x;
					  v_6.y = unity_WorldToObject[1].x;
					  v_6.z = unity_WorldToObject[2].x;
					  v_6.w = unity_WorldToObject[3].x;
					  highp vec4 v_7;
					  v_7.x = unity_WorldToObject[0].y;
					  v_7.y = unity_WorldToObject[1].y;
					  v_7.z = unity_WorldToObject[2].y;
					  v_7.w = unity_WorldToObject[3].y;
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].z;
					  v_8.y = unity_WorldToObject[1].z;
					  v_8.z = unity_WorldToObject[2].z;
					  v_8.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_9;
					  tmpvar_9 = normalize(((
					    (v_6.xyz * _glesNormal.x)
					   + 
					    (v_7.xyz * _glesNormal.y)
					  ) + (v_8.xyz * _glesNormal.z)));
					  worldNormal_3 = tmpvar_9;
					  highp mat3 tmpvar_10;
					  tmpvar_10[0] = unity_ObjectToWorld[0].xyz;
					  tmpvar_10[1] = unity_ObjectToWorld[1].xyz;
					  tmpvar_10[2] = unity_ObjectToWorld[2].xyz;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = normalize((tmpvar_10 * _glesTANGENT.xyz));
					  worldTangent_2 = tmpvar_11;
					  highp float tmpvar_12;
					  tmpvar_12 = (_glesTANGENT.w * unity_WorldTransformParams.w);
					  tangentSign_1 = tmpvar_12;
					  lowp vec3 tmpvar_13;
					  tmpvar_13 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
					  lowp vec3 tmpvar_14;
					  tmpvar_14.x = worldTangent_2.x;
					  tmpvar_14.y = tmpvar_13.x;
					  tmpvar_14.z = worldNormal_3.x;
					  lowp vec3 tmpvar_15;
					  tmpvar_15.x = worldTangent_2.y;
					  tmpvar_15.y = tmpvar_13.y;
					  tmpvar_15.z = worldNormal_3.y;
					  lowp vec3 tmpvar_16;
					  tmpvar_16.x = worldTangent_2.z;
					  tmpvar_16.y = tmpvar_13.z;
					  tmpvar_16.z = worldNormal_3.z;
					  gl_Position = (glstate_matrix_mvp * tmpvar_5);
					  xlv_TEXCOORD0 = tmpvar_4;
					  xlv_TEXCOORD1 = tmpvar_14;
					  xlv_TEXCOORD2 = tmpvar_15;
					  xlv_TEXCOORD3 = tmpvar_16;
					  xlv_TEXCOORD4 = (unity_ObjectToWorld * _glesVertex).xyz;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					varying highp vec4 xlv_TEXCOORD0;
					varying lowp vec3 xlv_TEXCOORD1;
					varying lowp vec3 xlv_TEXCOORD2;
					varying lowp vec3 xlv_TEXCOORD3;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  mediump vec3 tmpvar_2;
					  lowp vec3 worldN_3;
					  lowp vec4 c_4;
					  lowp vec3 tmpvar_5;
					  lowp vec3 lightDir_6;
					  mediump vec3 tmpvar_7;
					  tmpvar_7 = _WorldSpaceLightPos0.xyz;
					  lightDir_6 = tmpvar_7;
					  lowp vec3 tmpvar_8;
					  lowp vec3 tmpvar_9;
					  lowp float tmpvar_10;
					  tmpvar_9 = tmpvar_5;
					  highp vec4 tempNorm_11;
					  lowp vec4 tmpvar_12;
					  tmpvar_12 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  lowp vec4 tmpvar_13;
					  tmpvar_13.w = 0.0;
					  tmpvar_13.xyz = tmpvar_12.xyz;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = mix (tmpvar_13, _Color, vec4(_Fade)).xyz;
					  tmpvar_8 = tmpvar_14;
					  tmpvar_10 = tmpvar_12.w;
					  lowp vec4 tmpvar_15;
					  tmpvar_15 = texture2D (_BumpMap, xlv_TEXCOORD0.zw);
					  tempNorm_11 = tmpvar_15;
					  if ((_Inv == 1.0)) {
					    tempNorm_11.xy = (vec2(1.0, 1.0) - tempNorm_11.xy);
					  };
					  highp vec3 tmpvar_16;
					  tmpvar_16 = ((tempNorm_11 * 2.0) - 1.0).xyz;
					  tmpvar_9 = tmpvar_16;
					  worldN_3.x = dot (xlv_TEXCOORD1, tmpvar_9);
					  worldN_3.y = dot (xlv_TEXCOORD2, tmpvar_9);
					  worldN_3.z = dot (xlv_TEXCOORD3, tmpvar_9);
					  tmpvar_5 = worldN_3;
					  tmpvar_1 = _LightColor0.xyz;
					  tmpvar_2 = lightDir_6;
					  lowp vec4 c_17;
					  lowp vec4 c_18;
					  lowp float diff_19;
					  mediump float tmpvar_20;
					  tmpvar_20 = max (0.0, dot (worldN_3, tmpvar_2));
					  diff_19 = tmpvar_20;
					  c_18.xyz = ((tmpvar_8 * tmpvar_1) * diff_19);
					  c_18.w = tmpvar_10;
					  c_17.w = c_18.w;
					  c_17.xyz = c_18.xyz;
					  c_4.xyz = c_17.xyz;
					  c_4.w = 1.0;
					  gl_FragData[0] = c_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesTANGENT;
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 unity_WorldTransformParams;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _BumpMap_ST;
					varying highp vec4 xlv_TEXCOORD0;
					varying lowp vec3 xlv_TEXCOORD1;
					varying lowp vec3 xlv_TEXCOORD2;
					varying lowp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  lowp float tangentSign_1;
					  lowp vec3 worldTangent_2;
					  lowp vec3 worldNormal_3;
					  highp vec4 tmpvar_4;
					  highp vec4 tmpvar_5;
					  tmpvar_5.w = 1.0;
					  tmpvar_5.xyz = _glesVertex.xyz;
					  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
					  highp vec4 v_6;
					  v_6.x = unity_WorldToObject[0].x;
					  v_6.y = unity_WorldToObject[1].x;
					  v_6.z = unity_WorldToObject[2].x;
					  v_6.w = unity_WorldToObject[3].x;
					  highp vec4 v_7;
					  v_7.x = unity_WorldToObject[0].y;
					  v_7.y = unity_WorldToObject[1].y;
					  v_7.z = unity_WorldToObject[2].y;
					  v_7.w = unity_WorldToObject[3].y;
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].z;
					  v_8.y = unity_WorldToObject[1].z;
					  v_8.z = unity_WorldToObject[2].z;
					  v_8.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_9;
					  tmpvar_9 = normalize(((
					    (v_6.xyz * _glesNormal.x)
					   + 
					    (v_7.xyz * _glesNormal.y)
					  ) + (v_8.xyz * _glesNormal.z)));
					  worldNormal_3 = tmpvar_9;
					  highp mat3 tmpvar_10;
					  tmpvar_10[0] = unity_ObjectToWorld[0].xyz;
					  tmpvar_10[1] = unity_ObjectToWorld[1].xyz;
					  tmpvar_10[2] = unity_ObjectToWorld[2].xyz;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = normalize((tmpvar_10 * _glesTANGENT.xyz));
					  worldTangent_2 = tmpvar_11;
					  highp float tmpvar_12;
					  tmpvar_12 = (_glesTANGENT.w * unity_WorldTransformParams.w);
					  tangentSign_1 = tmpvar_12;
					  lowp vec3 tmpvar_13;
					  tmpvar_13 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
					  lowp vec3 tmpvar_14;
					  tmpvar_14.x = worldTangent_2.x;
					  tmpvar_14.y = tmpvar_13.x;
					  tmpvar_14.z = worldNormal_3.x;
					  lowp vec3 tmpvar_15;
					  tmpvar_15.x = worldTangent_2.y;
					  tmpvar_15.y = tmpvar_13.y;
					  tmpvar_15.z = worldNormal_3.y;
					  lowp vec3 tmpvar_16;
					  tmpvar_16.x = worldTangent_2.z;
					  tmpvar_16.y = tmpvar_13.z;
					  tmpvar_16.z = worldNormal_3.z;
					  gl_Position = (glstate_matrix_mvp * tmpvar_5);
					  xlv_TEXCOORD0 = tmpvar_4;
					  xlv_TEXCOORD1 = tmpvar_14;
					  xlv_TEXCOORD2 = tmpvar_15;
					  xlv_TEXCOORD3 = tmpvar_16;
					  xlv_TEXCOORD4 = (unity_ObjectToWorld * _glesVertex).xyz;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					varying highp vec4 xlv_TEXCOORD0;
					varying lowp vec3 xlv_TEXCOORD1;
					varying lowp vec3 xlv_TEXCOORD2;
					varying lowp vec3 xlv_TEXCOORD3;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  mediump vec3 tmpvar_2;
					  lowp vec3 worldN_3;
					  lowp vec4 c_4;
					  lowp vec3 tmpvar_5;
					  lowp vec3 lightDir_6;
					  mediump vec3 tmpvar_7;
					  tmpvar_7 = _WorldSpaceLightPos0.xyz;
					  lightDir_6 = tmpvar_7;
					  lowp vec3 tmpvar_8;
					  lowp vec3 tmpvar_9;
					  lowp float tmpvar_10;
					  tmpvar_9 = tmpvar_5;
					  highp vec4 tempNorm_11;
					  lowp vec4 tmpvar_12;
					  tmpvar_12 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  lowp vec4 tmpvar_13;
					  tmpvar_13.w = 0.0;
					  tmpvar_13.xyz = tmpvar_12.xyz;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = mix (tmpvar_13, _Color, vec4(_Fade)).xyz;
					  tmpvar_8 = tmpvar_14;
					  tmpvar_10 = tmpvar_12.w;
					  lowp vec4 tmpvar_15;
					  tmpvar_15 = texture2D (_BumpMap, xlv_TEXCOORD0.zw);
					  tempNorm_11 = tmpvar_15;
					  if ((_Inv == 1.0)) {
					    tempNorm_11.xy = (vec2(1.0, 1.0) - tempNorm_11.xy);
					  };
					  highp vec3 tmpvar_16;
					  tmpvar_16 = ((tempNorm_11 * 2.0) - 1.0).xyz;
					  tmpvar_9 = tmpvar_16;
					  worldN_3.x = dot (xlv_TEXCOORD1, tmpvar_9);
					  worldN_3.y = dot (xlv_TEXCOORD2, tmpvar_9);
					  worldN_3.z = dot (xlv_TEXCOORD3, tmpvar_9);
					  tmpvar_5 = worldN_3;
					  tmpvar_1 = _LightColor0.xyz;
					  tmpvar_2 = lightDir_6;
					  lowp vec4 c_17;
					  lowp vec4 c_18;
					  lowp float diff_19;
					  mediump float tmpvar_20;
					  tmpvar_20 = max (0.0, dot (worldN_3, tmpvar_2));
					  diff_19 = tmpvar_20;
					  c_18.xyz = ((tmpvar_8 * tmpvar_1) * diff_19);
					  c_18.w = tmpvar_10;
					  c_17.w = c_18.w;
					  c_17.xyz = c_18.xyz;
					  c_4.xyz = c_17.xyz;
					  c_4.w = 1.0;
					  gl_FragData[0] = c_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesTANGENT;
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 unity_WorldTransformParams;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _BumpMap_ST;
					varying highp vec4 xlv_TEXCOORD0;
					varying lowp vec3 xlv_TEXCOORD1;
					varying lowp vec3 xlv_TEXCOORD2;
					varying lowp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  lowp float tangentSign_1;
					  lowp vec3 worldTangent_2;
					  lowp vec3 worldNormal_3;
					  highp vec4 tmpvar_4;
					  highp vec4 tmpvar_5;
					  tmpvar_5.w = 1.0;
					  tmpvar_5.xyz = _glesVertex.xyz;
					  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
					  highp vec4 v_6;
					  v_6.x = unity_WorldToObject[0].x;
					  v_6.y = unity_WorldToObject[1].x;
					  v_6.z = unity_WorldToObject[2].x;
					  v_6.w = unity_WorldToObject[3].x;
					  highp vec4 v_7;
					  v_7.x = unity_WorldToObject[0].y;
					  v_7.y = unity_WorldToObject[1].y;
					  v_7.z = unity_WorldToObject[2].y;
					  v_7.w = unity_WorldToObject[3].y;
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].z;
					  v_8.y = unity_WorldToObject[1].z;
					  v_8.z = unity_WorldToObject[2].z;
					  v_8.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_9;
					  tmpvar_9 = normalize(((
					    (v_6.xyz * _glesNormal.x)
					   + 
					    (v_7.xyz * _glesNormal.y)
					  ) + (v_8.xyz * _glesNormal.z)));
					  worldNormal_3 = tmpvar_9;
					  highp mat3 tmpvar_10;
					  tmpvar_10[0] = unity_ObjectToWorld[0].xyz;
					  tmpvar_10[1] = unity_ObjectToWorld[1].xyz;
					  tmpvar_10[2] = unity_ObjectToWorld[2].xyz;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = normalize((tmpvar_10 * _glesTANGENT.xyz));
					  worldTangent_2 = tmpvar_11;
					  highp float tmpvar_12;
					  tmpvar_12 = (_glesTANGENT.w * unity_WorldTransformParams.w);
					  tangentSign_1 = tmpvar_12;
					  lowp vec3 tmpvar_13;
					  tmpvar_13 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
					  lowp vec3 tmpvar_14;
					  tmpvar_14.x = worldTangent_2.x;
					  tmpvar_14.y = tmpvar_13.x;
					  tmpvar_14.z = worldNormal_3.x;
					  lowp vec3 tmpvar_15;
					  tmpvar_15.x = worldTangent_2.y;
					  tmpvar_15.y = tmpvar_13.y;
					  tmpvar_15.z = worldNormal_3.y;
					  lowp vec3 tmpvar_16;
					  tmpvar_16.x = worldTangent_2.z;
					  tmpvar_16.y = tmpvar_13.z;
					  tmpvar_16.z = worldNormal_3.z;
					  gl_Position = (glstate_matrix_mvp * tmpvar_5);
					  xlv_TEXCOORD0 = tmpvar_4;
					  xlv_TEXCOORD1 = tmpvar_14;
					  xlv_TEXCOORD2 = tmpvar_15;
					  xlv_TEXCOORD3 = tmpvar_16;
					  xlv_TEXCOORD4 = (unity_ObjectToWorld * _glesVertex).xyz;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					varying highp vec4 xlv_TEXCOORD0;
					varying lowp vec3 xlv_TEXCOORD1;
					varying lowp vec3 xlv_TEXCOORD2;
					varying lowp vec3 xlv_TEXCOORD3;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  mediump vec3 tmpvar_2;
					  lowp vec3 worldN_3;
					  lowp vec4 c_4;
					  lowp vec3 tmpvar_5;
					  lowp vec3 lightDir_6;
					  mediump vec3 tmpvar_7;
					  tmpvar_7 = _WorldSpaceLightPos0.xyz;
					  lightDir_6 = tmpvar_7;
					  lowp vec3 tmpvar_8;
					  lowp vec3 tmpvar_9;
					  lowp float tmpvar_10;
					  tmpvar_9 = tmpvar_5;
					  highp vec4 tempNorm_11;
					  lowp vec4 tmpvar_12;
					  tmpvar_12 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  lowp vec4 tmpvar_13;
					  tmpvar_13.w = 0.0;
					  tmpvar_13.xyz = tmpvar_12.xyz;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = mix (tmpvar_13, _Color, vec4(_Fade)).xyz;
					  tmpvar_8 = tmpvar_14;
					  tmpvar_10 = tmpvar_12.w;
					  lowp vec4 tmpvar_15;
					  tmpvar_15 = texture2D (_BumpMap, xlv_TEXCOORD0.zw);
					  tempNorm_11 = tmpvar_15;
					  if ((_Inv == 1.0)) {
					    tempNorm_11.xy = (vec2(1.0, 1.0) - tempNorm_11.xy);
					  };
					  highp vec3 tmpvar_16;
					  tmpvar_16 = ((tempNorm_11 * 2.0) - 1.0).xyz;
					  tmpvar_9 = tmpvar_16;
					  worldN_3.x = dot (xlv_TEXCOORD1, tmpvar_9);
					  worldN_3.y = dot (xlv_TEXCOORD2, tmpvar_9);
					  worldN_3.z = dot (xlv_TEXCOORD3, tmpvar_9);
					  tmpvar_5 = worldN_3;
					  tmpvar_1 = _LightColor0.xyz;
					  tmpvar_2 = lightDir_6;
					  lowp vec4 c_17;
					  lowp vec4 c_18;
					  lowp float diff_19;
					  mediump float tmpvar_20;
					  tmpvar_20 = max (0.0, dot (worldN_3, tmpvar_2));
					  diff_19 = tmpvar_20;
					  c_18.xyz = ((tmpvar_8 * tmpvar_1) * diff_19);
					  c_18.w = tmpvar_10;
					  c_17.w = c_18.w;
					  c_17.xyz = c_18.xyz;
					  c_4.xyz = c_17.xyz;
					  c_4.w = 1.0;
					  gl_FragData[0] = c_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "SPOT" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesTANGENT;
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 unity_WorldTransformParams;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _BumpMap_ST;
					varying highp vec4 xlv_TEXCOORD0;
					varying lowp vec3 xlv_TEXCOORD1;
					varying lowp vec3 xlv_TEXCOORD2;
					varying lowp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  lowp float tangentSign_1;
					  lowp vec3 worldTangent_2;
					  lowp vec3 worldNormal_3;
					  highp vec4 tmpvar_4;
					  highp vec4 tmpvar_5;
					  tmpvar_5.w = 1.0;
					  tmpvar_5.xyz = _glesVertex.xyz;
					  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
					  highp vec4 v_6;
					  v_6.x = unity_WorldToObject[0].x;
					  v_6.y = unity_WorldToObject[1].x;
					  v_6.z = unity_WorldToObject[2].x;
					  v_6.w = unity_WorldToObject[3].x;
					  highp vec4 v_7;
					  v_7.x = unity_WorldToObject[0].y;
					  v_7.y = unity_WorldToObject[1].y;
					  v_7.z = unity_WorldToObject[2].y;
					  v_7.w = unity_WorldToObject[3].y;
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].z;
					  v_8.y = unity_WorldToObject[1].z;
					  v_8.z = unity_WorldToObject[2].z;
					  v_8.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_9;
					  tmpvar_9 = normalize(((
					    (v_6.xyz * _glesNormal.x)
					   + 
					    (v_7.xyz * _glesNormal.y)
					  ) + (v_8.xyz * _glesNormal.z)));
					  worldNormal_3 = tmpvar_9;
					  highp mat3 tmpvar_10;
					  tmpvar_10[0] = unity_ObjectToWorld[0].xyz;
					  tmpvar_10[1] = unity_ObjectToWorld[1].xyz;
					  tmpvar_10[2] = unity_ObjectToWorld[2].xyz;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = normalize((tmpvar_10 * _glesTANGENT.xyz));
					  worldTangent_2 = tmpvar_11;
					  highp float tmpvar_12;
					  tmpvar_12 = (_glesTANGENT.w * unity_WorldTransformParams.w);
					  tangentSign_1 = tmpvar_12;
					  lowp vec3 tmpvar_13;
					  tmpvar_13 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
					  lowp vec3 tmpvar_14;
					  tmpvar_14.x = worldTangent_2.x;
					  tmpvar_14.y = tmpvar_13.x;
					  tmpvar_14.z = worldNormal_3.x;
					  lowp vec3 tmpvar_15;
					  tmpvar_15.x = worldTangent_2.y;
					  tmpvar_15.y = tmpvar_13.y;
					  tmpvar_15.z = worldNormal_3.y;
					  lowp vec3 tmpvar_16;
					  tmpvar_16.x = worldTangent_2.z;
					  tmpvar_16.y = tmpvar_13.z;
					  tmpvar_16.z = worldNormal_3.z;
					  gl_Position = (glstate_matrix_mvp * tmpvar_5);
					  xlv_TEXCOORD0 = tmpvar_4;
					  xlv_TEXCOORD1 = tmpvar_14;
					  xlv_TEXCOORD2 = tmpvar_15;
					  xlv_TEXCOORD3 = tmpvar_16;
					  xlv_TEXCOORD4 = (unity_ObjectToWorld * _glesVertex).xyz;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D _LightTexture0;
					uniform highp mat4 unity_WorldToLight;
					uniform sampler2D _LightTextureB0;
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					varying highp vec4 xlv_TEXCOORD0;
					varying lowp vec3 xlv_TEXCOORD1;
					varying lowp vec3 xlv_TEXCOORD2;
					varying lowp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  mediump vec3 tmpvar_2;
					  lowp vec3 worldN_3;
					  lowp vec4 c_4;
					  lowp float atten_5;
					  lowp vec3 tmpvar_6;
					  lowp vec3 lightDir_7;
					  highp vec3 tmpvar_8;
					  tmpvar_8 = normalize((_WorldSpaceLightPos0.xyz - xlv_TEXCOORD4));
					  lightDir_7 = tmpvar_8;
					  lowp vec3 tmpvar_9;
					  lowp vec3 tmpvar_10;
					  lowp float tmpvar_11;
					  tmpvar_10 = tmpvar_6;
					  highp vec4 tempNorm_12;
					  lowp vec4 tmpvar_13;
					  tmpvar_13 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  lowp vec4 tmpvar_14;
					  tmpvar_14.w = 0.0;
					  tmpvar_14.xyz = tmpvar_13.xyz;
					  highp vec3 tmpvar_15;
					  tmpvar_15 = mix (tmpvar_14, _Color, vec4(_Fade)).xyz;
					  tmpvar_9 = tmpvar_15;
					  tmpvar_11 = tmpvar_13.w;
					  lowp vec4 tmpvar_16;
					  tmpvar_16 = texture2D (_BumpMap, xlv_TEXCOORD0.zw);
					  tempNorm_12 = tmpvar_16;
					  if ((_Inv == 1.0)) {
					    tempNorm_12.xy = (vec2(1.0, 1.0) - tempNorm_12.xy);
					  };
					  highp vec3 tmpvar_17;
					  tmpvar_17 = ((tempNorm_12 * 2.0) - 1.0).xyz;
					  tmpvar_10 = tmpvar_17;
					  highp vec4 tmpvar_18;
					  tmpvar_18.w = 1.0;
					  tmpvar_18.xyz = xlv_TEXCOORD4;
					  highp vec4 tmpvar_19;
					  tmpvar_19 = (unity_WorldToLight * tmpvar_18);
					  lowp vec4 tmpvar_20;
					  highp vec2 P_21;
					  P_21 = ((tmpvar_19.xy / tmpvar_19.w) + 0.5);
					  tmpvar_20 = texture2D (_LightTexture0, P_21);
					  highp float tmpvar_22;
					  tmpvar_22 = dot (tmpvar_19.xyz, tmpvar_19.xyz);
					  lowp vec4 tmpvar_23;
					  tmpvar_23 = texture2D (_LightTextureB0, vec2(tmpvar_22));
					  highp float tmpvar_24;
					  tmpvar_24 = ((float(
					    (tmpvar_19.z > 0.0)
					  ) * tmpvar_20.w) * tmpvar_23.w);
					  atten_5 = tmpvar_24;
					  worldN_3.x = dot (xlv_TEXCOORD1, tmpvar_10);
					  worldN_3.y = dot (xlv_TEXCOORD2, tmpvar_10);
					  worldN_3.z = dot (xlv_TEXCOORD3, tmpvar_10);
					  tmpvar_6 = worldN_3;
					  tmpvar_1 = _LightColor0.xyz;
					  tmpvar_2 = lightDir_7;
					  tmpvar_1 = (tmpvar_1 * atten_5);
					  lowp vec4 c_25;
					  lowp vec4 c_26;
					  lowp float diff_27;
					  mediump float tmpvar_28;
					  tmpvar_28 = max (0.0, dot (worldN_3, tmpvar_2));
					  diff_27 = tmpvar_28;
					  c_26.xyz = ((tmpvar_9 * tmpvar_1) * diff_27);
					  c_26.w = tmpvar_11;
					  c_25.w = c_26.w;
					  c_25.xyz = c_26.xyz;
					  c_4.xyz = c_25.xyz;
					  c_4.w = 1.0;
					  gl_FragData[0] = c_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "SPOT" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesTANGENT;
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 unity_WorldTransformParams;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _BumpMap_ST;
					varying highp vec4 xlv_TEXCOORD0;
					varying lowp vec3 xlv_TEXCOORD1;
					varying lowp vec3 xlv_TEXCOORD2;
					varying lowp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  lowp float tangentSign_1;
					  lowp vec3 worldTangent_2;
					  lowp vec3 worldNormal_3;
					  highp vec4 tmpvar_4;
					  highp vec4 tmpvar_5;
					  tmpvar_5.w = 1.0;
					  tmpvar_5.xyz = _glesVertex.xyz;
					  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
					  highp vec4 v_6;
					  v_6.x = unity_WorldToObject[0].x;
					  v_6.y = unity_WorldToObject[1].x;
					  v_6.z = unity_WorldToObject[2].x;
					  v_6.w = unity_WorldToObject[3].x;
					  highp vec4 v_7;
					  v_7.x = unity_WorldToObject[0].y;
					  v_7.y = unity_WorldToObject[1].y;
					  v_7.z = unity_WorldToObject[2].y;
					  v_7.w = unity_WorldToObject[3].y;
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].z;
					  v_8.y = unity_WorldToObject[1].z;
					  v_8.z = unity_WorldToObject[2].z;
					  v_8.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_9;
					  tmpvar_9 = normalize(((
					    (v_6.xyz * _glesNormal.x)
					   + 
					    (v_7.xyz * _glesNormal.y)
					  ) + (v_8.xyz * _glesNormal.z)));
					  worldNormal_3 = tmpvar_9;
					  highp mat3 tmpvar_10;
					  tmpvar_10[0] = unity_ObjectToWorld[0].xyz;
					  tmpvar_10[1] = unity_ObjectToWorld[1].xyz;
					  tmpvar_10[2] = unity_ObjectToWorld[2].xyz;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = normalize((tmpvar_10 * _glesTANGENT.xyz));
					  worldTangent_2 = tmpvar_11;
					  highp float tmpvar_12;
					  tmpvar_12 = (_glesTANGENT.w * unity_WorldTransformParams.w);
					  tangentSign_1 = tmpvar_12;
					  lowp vec3 tmpvar_13;
					  tmpvar_13 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
					  lowp vec3 tmpvar_14;
					  tmpvar_14.x = worldTangent_2.x;
					  tmpvar_14.y = tmpvar_13.x;
					  tmpvar_14.z = worldNormal_3.x;
					  lowp vec3 tmpvar_15;
					  tmpvar_15.x = worldTangent_2.y;
					  tmpvar_15.y = tmpvar_13.y;
					  tmpvar_15.z = worldNormal_3.y;
					  lowp vec3 tmpvar_16;
					  tmpvar_16.x = worldTangent_2.z;
					  tmpvar_16.y = tmpvar_13.z;
					  tmpvar_16.z = worldNormal_3.z;
					  gl_Position = (glstate_matrix_mvp * tmpvar_5);
					  xlv_TEXCOORD0 = tmpvar_4;
					  xlv_TEXCOORD1 = tmpvar_14;
					  xlv_TEXCOORD2 = tmpvar_15;
					  xlv_TEXCOORD3 = tmpvar_16;
					  xlv_TEXCOORD4 = (unity_ObjectToWorld * _glesVertex).xyz;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D _LightTexture0;
					uniform highp mat4 unity_WorldToLight;
					uniform sampler2D _LightTextureB0;
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					varying highp vec4 xlv_TEXCOORD0;
					varying lowp vec3 xlv_TEXCOORD1;
					varying lowp vec3 xlv_TEXCOORD2;
					varying lowp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  mediump vec3 tmpvar_2;
					  lowp vec3 worldN_3;
					  lowp vec4 c_4;
					  lowp float atten_5;
					  lowp vec3 tmpvar_6;
					  lowp vec3 lightDir_7;
					  highp vec3 tmpvar_8;
					  tmpvar_8 = normalize((_WorldSpaceLightPos0.xyz - xlv_TEXCOORD4));
					  lightDir_7 = tmpvar_8;
					  lowp vec3 tmpvar_9;
					  lowp vec3 tmpvar_10;
					  lowp float tmpvar_11;
					  tmpvar_10 = tmpvar_6;
					  highp vec4 tempNorm_12;
					  lowp vec4 tmpvar_13;
					  tmpvar_13 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  lowp vec4 tmpvar_14;
					  tmpvar_14.w = 0.0;
					  tmpvar_14.xyz = tmpvar_13.xyz;
					  highp vec3 tmpvar_15;
					  tmpvar_15 = mix (tmpvar_14, _Color, vec4(_Fade)).xyz;
					  tmpvar_9 = tmpvar_15;
					  tmpvar_11 = tmpvar_13.w;
					  lowp vec4 tmpvar_16;
					  tmpvar_16 = texture2D (_BumpMap, xlv_TEXCOORD0.zw);
					  tempNorm_12 = tmpvar_16;
					  if ((_Inv == 1.0)) {
					    tempNorm_12.xy = (vec2(1.0, 1.0) - tempNorm_12.xy);
					  };
					  highp vec3 tmpvar_17;
					  tmpvar_17 = ((tempNorm_12 * 2.0) - 1.0).xyz;
					  tmpvar_10 = tmpvar_17;
					  highp vec4 tmpvar_18;
					  tmpvar_18.w = 1.0;
					  tmpvar_18.xyz = xlv_TEXCOORD4;
					  highp vec4 tmpvar_19;
					  tmpvar_19 = (unity_WorldToLight * tmpvar_18);
					  lowp vec4 tmpvar_20;
					  highp vec2 P_21;
					  P_21 = ((tmpvar_19.xy / tmpvar_19.w) + 0.5);
					  tmpvar_20 = texture2D (_LightTexture0, P_21);
					  highp float tmpvar_22;
					  tmpvar_22 = dot (tmpvar_19.xyz, tmpvar_19.xyz);
					  lowp vec4 tmpvar_23;
					  tmpvar_23 = texture2D (_LightTextureB0, vec2(tmpvar_22));
					  highp float tmpvar_24;
					  tmpvar_24 = ((float(
					    (tmpvar_19.z > 0.0)
					  ) * tmpvar_20.w) * tmpvar_23.w);
					  atten_5 = tmpvar_24;
					  worldN_3.x = dot (xlv_TEXCOORD1, tmpvar_10);
					  worldN_3.y = dot (xlv_TEXCOORD2, tmpvar_10);
					  worldN_3.z = dot (xlv_TEXCOORD3, tmpvar_10);
					  tmpvar_6 = worldN_3;
					  tmpvar_1 = _LightColor0.xyz;
					  tmpvar_2 = lightDir_7;
					  tmpvar_1 = (tmpvar_1 * atten_5);
					  lowp vec4 c_25;
					  lowp vec4 c_26;
					  lowp float diff_27;
					  mediump float tmpvar_28;
					  tmpvar_28 = max (0.0, dot (worldN_3, tmpvar_2));
					  diff_27 = tmpvar_28;
					  c_26.xyz = ((tmpvar_9 * tmpvar_1) * diff_27);
					  c_26.w = tmpvar_11;
					  c_25.w = c_26.w;
					  c_25.xyz = c_26.xyz;
					  c_4.xyz = c_25.xyz;
					  c_4.w = 1.0;
					  gl_FragData[0] = c_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "SPOT" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesTANGENT;
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 unity_WorldTransformParams;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _BumpMap_ST;
					varying highp vec4 xlv_TEXCOORD0;
					varying lowp vec3 xlv_TEXCOORD1;
					varying lowp vec3 xlv_TEXCOORD2;
					varying lowp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  lowp float tangentSign_1;
					  lowp vec3 worldTangent_2;
					  lowp vec3 worldNormal_3;
					  highp vec4 tmpvar_4;
					  highp vec4 tmpvar_5;
					  tmpvar_5.w = 1.0;
					  tmpvar_5.xyz = _glesVertex.xyz;
					  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
					  highp vec4 v_6;
					  v_6.x = unity_WorldToObject[0].x;
					  v_6.y = unity_WorldToObject[1].x;
					  v_6.z = unity_WorldToObject[2].x;
					  v_6.w = unity_WorldToObject[3].x;
					  highp vec4 v_7;
					  v_7.x = unity_WorldToObject[0].y;
					  v_7.y = unity_WorldToObject[1].y;
					  v_7.z = unity_WorldToObject[2].y;
					  v_7.w = unity_WorldToObject[3].y;
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].z;
					  v_8.y = unity_WorldToObject[1].z;
					  v_8.z = unity_WorldToObject[2].z;
					  v_8.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_9;
					  tmpvar_9 = normalize(((
					    (v_6.xyz * _glesNormal.x)
					   + 
					    (v_7.xyz * _glesNormal.y)
					  ) + (v_8.xyz * _glesNormal.z)));
					  worldNormal_3 = tmpvar_9;
					  highp mat3 tmpvar_10;
					  tmpvar_10[0] = unity_ObjectToWorld[0].xyz;
					  tmpvar_10[1] = unity_ObjectToWorld[1].xyz;
					  tmpvar_10[2] = unity_ObjectToWorld[2].xyz;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = normalize((tmpvar_10 * _glesTANGENT.xyz));
					  worldTangent_2 = tmpvar_11;
					  highp float tmpvar_12;
					  tmpvar_12 = (_glesTANGENT.w * unity_WorldTransformParams.w);
					  tangentSign_1 = tmpvar_12;
					  lowp vec3 tmpvar_13;
					  tmpvar_13 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
					  lowp vec3 tmpvar_14;
					  tmpvar_14.x = worldTangent_2.x;
					  tmpvar_14.y = tmpvar_13.x;
					  tmpvar_14.z = worldNormal_3.x;
					  lowp vec3 tmpvar_15;
					  tmpvar_15.x = worldTangent_2.y;
					  tmpvar_15.y = tmpvar_13.y;
					  tmpvar_15.z = worldNormal_3.y;
					  lowp vec3 tmpvar_16;
					  tmpvar_16.x = worldTangent_2.z;
					  tmpvar_16.y = tmpvar_13.z;
					  tmpvar_16.z = worldNormal_3.z;
					  gl_Position = (glstate_matrix_mvp * tmpvar_5);
					  xlv_TEXCOORD0 = tmpvar_4;
					  xlv_TEXCOORD1 = tmpvar_14;
					  xlv_TEXCOORD2 = tmpvar_15;
					  xlv_TEXCOORD3 = tmpvar_16;
					  xlv_TEXCOORD4 = (unity_ObjectToWorld * _glesVertex).xyz;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D _LightTexture0;
					uniform highp mat4 unity_WorldToLight;
					uniform sampler2D _LightTextureB0;
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					varying highp vec4 xlv_TEXCOORD0;
					varying lowp vec3 xlv_TEXCOORD1;
					varying lowp vec3 xlv_TEXCOORD2;
					varying lowp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  mediump vec3 tmpvar_2;
					  lowp vec3 worldN_3;
					  lowp vec4 c_4;
					  lowp float atten_5;
					  lowp vec3 tmpvar_6;
					  lowp vec3 lightDir_7;
					  highp vec3 tmpvar_8;
					  tmpvar_8 = normalize((_WorldSpaceLightPos0.xyz - xlv_TEXCOORD4));
					  lightDir_7 = tmpvar_8;
					  lowp vec3 tmpvar_9;
					  lowp vec3 tmpvar_10;
					  lowp float tmpvar_11;
					  tmpvar_10 = tmpvar_6;
					  highp vec4 tempNorm_12;
					  lowp vec4 tmpvar_13;
					  tmpvar_13 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  lowp vec4 tmpvar_14;
					  tmpvar_14.w = 0.0;
					  tmpvar_14.xyz = tmpvar_13.xyz;
					  highp vec3 tmpvar_15;
					  tmpvar_15 = mix (tmpvar_14, _Color, vec4(_Fade)).xyz;
					  tmpvar_9 = tmpvar_15;
					  tmpvar_11 = tmpvar_13.w;
					  lowp vec4 tmpvar_16;
					  tmpvar_16 = texture2D (_BumpMap, xlv_TEXCOORD0.zw);
					  tempNorm_12 = tmpvar_16;
					  if ((_Inv == 1.0)) {
					    tempNorm_12.xy = (vec2(1.0, 1.0) - tempNorm_12.xy);
					  };
					  highp vec3 tmpvar_17;
					  tmpvar_17 = ((tempNorm_12 * 2.0) - 1.0).xyz;
					  tmpvar_10 = tmpvar_17;
					  highp vec4 tmpvar_18;
					  tmpvar_18.w = 1.0;
					  tmpvar_18.xyz = xlv_TEXCOORD4;
					  highp vec4 tmpvar_19;
					  tmpvar_19 = (unity_WorldToLight * tmpvar_18);
					  lowp vec4 tmpvar_20;
					  highp vec2 P_21;
					  P_21 = ((tmpvar_19.xy / tmpvar_19.w) + 0.5);
					  tmpvar_20 = texture2D (_LightTexture0, P_21);
					  highp float tmpvar_22;
					  tmpvar_22 = dot (tmpvar_19.xyz, tmpvar_19.xyz);
					  lowp vec4 tmpvar_23;
					  tmpvar_23 = texture2D (_LightTextureB0, vec2(tmpvar_22));
					  highp float tmpvar_24;
					  tmpvar_24 = ((float(
					    (tmpvar_19.z > 0.0)
					  ) * tmpvar_20.w) * tmpvar_23.w);
					  atten_5 = tmpvar_24;
					  worldN_3.x = dot (xlv_TEXCOORD1, tmpvar_10);
					  worldN_3.y = dot (xlv_TEXCOORD2, tmpvar_10);
					  worldN_3.z = dot (xlv_TEXCOORD3, tmpvar_10);
					  tmpvar_6 = worldN_3;
					  tmpvar_1 = _LightColor0.xyz;
					  tmpvar_2 = lightDir_7;
					  tmpvar_1 = (tmpvar_1 * atten_5);
					  lowp vec4 c_25;
					  lowp vec4 c_26;
					  lowp float diff_27;
					  mediump float tmpvar_28;
					  tmpvar_28 = max (0.0, dot (worldN_3, tmpvar_2));
					  diff_27 = tmpvar_28;
					  c_26.xyz = ((tmpvar_9 * tmpvar_1) * diff_27);
					  c_26.w = tmpvar_11;
					  c_25.w = c_26.w;
					  c_25.xyz = c_26.xyz;
					  c_4.xyz = c_25.xyz;
					  c_4.w = 1.0;
					  gl_FragData[0] = c_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "POINT_COOKIE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesTANGENT;
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 unity_WorldTransformParams;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _BumpMap_ST;
					varying highp vec4 xlv_TEXCOORD0;
					varying lowp vec3 xlv_TEXCOORD1;
					varying lowp vec3 xlv_TEXCOORD2;
					varying lowp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  lowp float tangentSign_1;
					  lowp vec3 worldTangent_2;
					  lowp vec3 worldNormal_3;
					  highp vec4 tmpvar_4;
					  highp vec4 tmpvar_5;
					  tmpvar_5.w = 1.0;
					  tmpvar_5.xyz = _glesVertex.xyz;
					  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
					  highp vec4 v_6;
					  v_6.x = unity_WorldToObject[0].x;
					  v_6.y = unity_WorldToObject[1].x;
					  v_6.z = unity_WorldToObject[2].x;
					  v_6.w = unity_WorldToObject[3].x;
					  highp vec4 v_7;
					  v_7.x = unity_WorldToObject[0].y;
					  v_7.y = unity_WorldToObject[1].y;
					  v_7.z = unity_WorldToObject[2].y;
					  v_7.w = unity_WorldToObject[3].y;
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].z;
					  v_8.y = unity_WorldToObject[1].z;
					  v_8.z = unity_WorldToObject[2].z;
					  v_8.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_9;
					  tmpvar_9 = normalize(((
					    (v_6.xyz * _glesNormal.x)
					   + 
					    (v_7.xyz * _glesNormal.y)
					  ) + (v_8.xyz * _glesNormal.z)));
					  worldNormal_3 = tmpvar_9;
					  highp mat3 tmpvar_10;
					  tmpvar_10[0] = unity_ObjectToWorld[0].xyz;
					  tmpvar_10[1] = unity_ObjectToWorld[1].xyz;
					  tmpvar_10[2] = unity_ObjectToWorld[2].xyz;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = normalize((tmpvar_10 * _glesTANGENT.xyz));
					  worldTangent_2 = tmpvar_11;
					  highp float tmpvar_12;
					  tmpvar_12 = (_glesTANGENT.w * unity_WorldTransformParams.w);
					  tangentSign_1 = tmpvar_12;
					  lowp vec3 tmpvar_13;
					  tmpvar_13 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
					  lowp vec3 tmpvar_14;
					  tmpvar_14.x = worldTangent_2.x;
					  tmpvar_14.y = tmpvar_13.x;
					  tmpvar_14.z = worldNormal_3.x;
					  lowp vec3 tmpvar_15;
					  tmpvar_15.x = worldTangent_2.y;
					  tmpvar_15.y = tmpvar_13.y;
					  tmpvar_15.z = worldNormal_3.y;
					  lowp vec3 tmpvar_16;
					  tmpvar_16.x = worldTangent_2.z;
					  tmpvar_16.y = tmpvar_13.z;
					  tmpvar_16.z = worldNormal_3.z;
					  gl_Position = (glstate_matrix_mvp * tmpvar_5);
					  xlv_TEXCOORD0 = tmpvar_4;
					  xlv_TEXCOORD1 = tmpvar_14;
					  xlv_TEXCOORD2 = tmpvar_15;
					  xlv_TEXCOORD3 = tmpvar_16;
					  xlv_TEXCOORD4 = (unity_ObjectToWorld * _glesVertex).xyz;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform lowp vec4 _LightColor0;
					uniform lowp samplerCube _LightTexture0;
					uniform highp mat4 unity_WorldToLight;
					uniform sampler2D _LightTextureB0;
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					varying highp vec4 xlv_TEXCOORD0;
					varying lowp vec3 xlv_TEXCOORD1;
					varying lowp vec3 xlv_TEXCOORD2;
					varying lowp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  mediump vec3 tmpvar_2;
					  lowp vec3 worldN_3;
					  lowp vec4 c_4;
					  lowp vec3 tmpvar_5;
					  lowp vec3 lightDir_6;
					  highp vec3 tmpvar_7;
					  tmpvar_7 = normalize((_WorldSpaceLightPos0.xyz - xlv_TEXCOORD4));
					  lightDir_6 = tmpvar_7;
					  lowp vec3 tmpvar_8;
					  lowp vec3 tmpvar_9;
					  lowp float tmpvar_10;
					  tmpvar_9 = tmpvar_5;
					  highp vec4 tempNorm_11;
					  lowp vec4 tmpvar_12;
					  tmpvar_12 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  lowp vec4 tmpvar_13;
					  tmpvar_13.w = 0.0;
					  tmpvar_13.xyz = tmpvar_12.xyz;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = mix (tmpvar_13, _Color, vec4(_Fade)).xyz;
					  tmpvar_8 = tmpvar_14;
					  tmpvar_10 = tmpvar_12.w;
					  lowp vec4 tmpvar_15;
					  tmpvar_15 = texture2D (_BumpMap, xlv_TEXCOORD0.zw);
					  tempNorm_11 = tmpvar_15;
					  if ((_Inv == 1.0)) {
					    tempNorm_11.xy = (vec2(1.0, 1.0) - tempNorm_11.xy);
					  };
					  highp vec3 tmpvar_16;
					  tmpvar_16 = ((tempNorm_11 * 2.0) - 1.0).xyz;
					  tmpvar_9 = tmpvar_16;
					  highp vec4 tmpvar_17;
					  tmpvar_17.w = 1.0;
					  tmpvar_17.xyz = xlv_TEXCOORD4;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = (unity_WorldToLight * tmpvar_17).xyz;
					  highp float tmpvar_19;
					  tmpvar_19 = dot (tmpvar_18, tmpvar_18);
					  lowp float tmpvar_20;
					  tmpvar_20 = (texture2D (_LightTextureB0, vec2(tmpvar_19)).w * textureCube (_LightTexture0, tmpvar_18).w);
					  worldN_3.x = dot (xlv_TEXCOORD1, tmpvar_9);
					  worldN_3.y = dot (xlv_TEXCOORD2, tmpvar_9);
					  worldN_3.z = dot (xlv_TEXCOORD3, tmpvar_9);
					  tmpvar_5 = worldN_3;
					  tmpvar_1 = _LightColor0.xyz;
					  tmpvar_2 = lightDir_6;
					  tmpvar_1 = (tmpvar_1 * tmpvar_20);
					  lowp vec4 c_21;
					  lowp vec4 c_22;
					  lowp float diff_23;
					  mediump float tmpvar_24;
					  tmpvar_24 = max (0.0, dot (worldN_3, tmpvar_2));
					  diff_23 = tmpvar_24;
					  c_22.xyz = ((tmpvar_8 * tmpvar_1) * diff_23);
					  c_22.w = tmpvar_10;
					  c_21.w = c_22.w;
					  c_21.xyz = c_22.xyz;
					  c_4.xyz = c_21.xyz;
					  c_4.w = 1.0;
					  gl_FragData[0] = c_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "POINT_COOKIE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesTANGENT;
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 unity_WorldTransformParams;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _BumpMap_ST;
					varying highp vec4 xlv_TEXCOORD0;
					varying lowp vec3 xlv_TEXCOORD1;
					varying lowp vec3 xlv_TEXCOORD2;
					varying lowp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  lowp float tangentSign_1;
					  lowp vec3 worldTangent_2;
					  lowp vec3 worldNormal_3;
					  highp vec4 tmpvar_4;
					  highp vec4 tmpvar_5;
					  tmpvar_5.w = 1.0;
					  tmpvar_5.xyz = _glesVertex.xyz;
					  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
					  highp vec4 v_6;
					  v_6.x = unity_WorldToObject[0].x;
					  v_6.y = unity_WorldToObject[1].x;
					  v_6.z = unity_WorldToObject[2].x;
					  v_6.w = unity_WorldToObject[3].x;
					  highp vec4 v_7;
					  v_7.x = unity_WorldToObject[0].y;
					  v_7.y = unity_WorldToObject[1].y;
					  v_7.z = unity_WorldToObject[2].y;
					  v_7.w = unity_WorldToObject[3].y;
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].z;
					  v_8.y = unity_WorldToObject[1].z;
					  v_8.z = unity_WorldToObject[2].z;
					  v_8.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_9;
					  tmpvar_9 = normalize(((
					    (v_6.xyz * _glesNormal.x)
					   + 
					    (v_7.xyz * _glesNormal.y)
					  ) + (v_8.xyz * _glesNormal.z)));
					  worldNormal_3 = tmpvar_9;
					  highp mat3 tmpvar_10;
					  tmpvar_10[0] = unity_ObjectToWorld[0].xyz;
					  tmpvar_10[1] = unity_ObjectToWorld[1].xyz;
					  tmpvar_10[2] = unity_ObjectToWorld[2].xyz;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = normalize((tmpvar_10 * _glesTANGENT.xyz));
					  worldTangent_2 = tmpvar_11;
					  highp float tmpvar_12;
					  tmpvar_12 = (_glesTANGENT.w * unity_WorldTransformParams.w);
					  tangentSign_1 = tmpvar_12;
					  lowp vec3 tmpvar_13;
					  tmpvar_13 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
					  lowp vec3 tmpvar_14;
					  tmpvar_14.x = worldTangent_2.x;
					  tmpvar_14.y = tmpvar_13.x;
					  tmpvar_14.z = worldNormal_3.x;
					  lowp vec3 tmpvar_15;
					  tmpvar_15.x = worldTangent_2.y;
					  tmpvar_15.y = tmpvar_13.y;
					  tmpvar_15.z = worldNormal_3.y;
					  lowp vec3 tmpvar_16;
					  tmpvar_16.x = worldTangent_2.z;
					  tmpvar_16.y = tmpvar_13.z;
					  tmpvar_16.z = worldNormal_3.z;
					  gl_Position = (glstate_matrix_mvp * tmpvar_5);
					  xlv_TEXCOORD0 = tmpvar_4;
					  xlv_TEXCOORD1 = tmpvar_14;
					  xlv_TEXCOORD2 = tmpvar_15;
					  xlv_TEXCOORD3 = tmpvar_16;
					  xlv_TEXCOORD4 = (unity_ObjectToWorld * _glesVertex).xyz;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform lowp vec4 _LightColor0;
					uniform lowp samplerCube _LightTexture0;
					uniform highp mat4 unity_WorldToLight;
					uniform sampler2D _LightTextureB0;
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					varying highp vec4 xlv_TEXCOORD0;
					varying lowp vec3 xlv_TEXCOORD1;
					varying lowp vec3 xlv_TEXCOORD2;
					varying lowp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  mediump vec3 tmpvar_2;
					  lowp vec3 worldN_3;
					  lowp vec4 c_4;
					  lowp vec3 tmpvar_5;
					  lowp vec3 lightDir_6;
					  highp vec3 tmpvar_7;
					  tmpvar_7 = normalize((_WorldSpaceLightPos0.xyz - xlv_TEXCOORD4));
					  lightDir_6 = tmpvar_7;
					  lowp vec3 tmpvar_8;
					  lowp vec3 tmpvar_9;
					  lowp float tmpvar_10;
					  tmpvar_9 = tmpvar_5;
					  highp vec4 tempNorm_11;
					  lowp vec4 tmpvar_12;
					  tmpvar_12 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  lowp vec4 tmpvar_13;
					  tmpvar_13.w = 0.0;
					  tmpvar_13.xyz = tmpvar_12.xyz;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = mix (tmpvar_13, _Color, vec4(_Fade)).xyz;
					  tmpvar_8 = tmpvar_14;
					  tmpvar_10 = tmpvar_12.w;
					  lowp vec4 tmpvar_15;
					  tmpvar_15 = texture2D (_BumpMap, xlv_TEXCOORD0.zw);
					  tempNorm_11 = tmpvar_15;
					  if ((_Inv == 1.0)) {
					    tempNorm_11.xy = (vec2(1.0, 1.0) - tempNorm_11.xy);
					  };
					  highp vec3 tmpvar_16;
					  tmpvar_16 = ((tempNorm_11 * 2.0) - 1.0).xyz;
					  tmpvar_9 = tmpvar_16;
					  highp vec4 tmpvar_17;
					  tmpvar_17.w = 1.0;
					  tmpvar_17.xyz = xlv_TEXCOORD4;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = (unity_WorldToLight * tmpvar_17).xyz;
					  highp float tmpvar_19;
					  tmpvar_19 = dot (tmpvar_18, tmpvar_18);
					  lowp float tmpvar_20;
					  tmpvar_20 = (texture2D (_LightTextureB0, vec2(tmpvar_19)).w * textureCube (_LightTexture0, tmpvar_18).w);
					  worldN_3.x = dot (xlv_TEXCOORD1, tmpvar_9);
					  worldN_3.y = dot (xlv_TEXCOORD2, tmpvar_9);
					  worldN_3.z = dot (xlv_TEXCOORD3, tmpvar_9);
					  tmpvar_5 = worldN_3;
					  tmpvar_1 = _LightColor0.xyz;
					  tmpvar_2 = lightDir_6;
					  tmpvar_1 = (tmpvar_1 * tmpvar_20);
					  lowp vec4 c_21;
					  lowp vec4 c_22;
					  lowp float diff_23;
					  mediump float tmpvar_24;
					  tmpvar_24 = max (0.0, dot (worldN_3, tmpvar_2));
					  diff_23 = tmpvar_24;
					  c_22.xyz = ((tmpvar_8 * tmpvar_1) * diff_23);
					  c_22.w = tmpvar_10;
					  c_21.w = c_22.w;
					  c_21.xyz = c_22.xyz;
					  c_4.xyz = c_21.xyz;
					  c_4.w = 1.0;
					  gl_FragData[0] = c_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "POINT_COOKIE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesTANGENT;
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 unity_WorldTransformParams;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _BumpMap_ST;
					varying highp vec4 xlv_TEXCOORD0;
					varying lowp vec3 xlv_TEXCOORD1;
					varying lowp vec3 xlv_TEXCOORD2;
					varying lowp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  lowp float tangentSign_1;
					  lowp vec3 worldTangent_2;
					  lowp vec3 worldNormal_3;
					  highp vec4 tmpvar_4;
					  highp vec4 tmpvar_5;
					  tmpvar_5.w = 1.0;
					  tmpvar_5.xyz = _glesVertex.xyz;
					  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
					  highp vec4 v_6;
					  v_6.x = unity_WorldToObject[0].x;
					  v_6.y = unity_WorldToObject[1].x;
					  v_6.z = unity_WorldToObject[2].x;
					  v_6.w = unity_WorldToObject[3].x;
					  highp vec4 v_7;
					  v_7.x = unity_WorldToObject[0].y;
					  v_7.y = unity_WorldToObject[1].y;
					  v_7.z = unity_WorldToObject[2].y;
					  v_7.w = unity_WorldToObject[3].y;
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].z;
					  v_8.y = unity_WorldToObject[1].z;
					  v_8.z = unity_WorldToObject[2].z;
					  v_8.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_9;
					  tmpvar_9 = normalize(((
					    (v_6.xyz * _glesNormal.x)
					   + 
					    (v_7.xyz * _glesNormal.y)
					  ) + (v_8.xyz * _glesNormal.z)));
					  worldNormal_3 = tmpvar_9;
					  highp mat3 tmpvar_10;
					  tmpvar_10[0] = unity_ObjectToWorld[0].xyz;
					  tmpvar_10[1] = unity_ObjectToWorld[1].xyz;
					  tmpvar_10[2] = unity_ObjectToWorld[2].xyz;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = normalize((tmpvar_10 * _glesTANGENT.xyz));
					  worldTangent_2 = tmpvar_11;
					  highp float tmpvar_12;
					  tmpvar_12 = (_glesTANGENT.w * unity_WorldTransformParams.w);
					  tangentSign_1 = tmpvar_12;
					  lowp vec3 tmpvar_13;
					  tmpvar_13 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
					  lowp vec3 tmpvar_14;
					  tmpvar_14.x = worldTangent_2.x;
					  tmpvar_14.y = tmpvar_13.x;
					  tmpvar_14.z = worldNormal_3.x;
					  lowp vec3 tmpvar_15;
					  tmpvar_15.x = worldTangent_2.y;
					  tmpvar_15.y = tmpvar_13.y;
					  tmpvar_15.z = worldNormal_3.y;
					  lowp vec3 tmpvar_16;
					  tmpvar_16.x = worldTangent_2.z;
					  tmpvar_16.y = tmpvar_13.z;
					  tmpvar_16.z = worldNormal_3.z;
					  gl_Position = (glstate_matrix_mvp * tmpvar_5);
					  xlv_TEXCOORD0 = tmpvar_4;
					  xlv_TEXCOORD1 = tmpvar_14;
					  xlv_TEXCOORD2 = tmpvar_15;
					  xlv_TEXCOORD3 = tmpvar_16;
					  xlv_TEXCOORD4 = (unity_ObjectToWorld * _glesVertex).xyz;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform highp vec4 _WorldSpaceLightPos0;
					uniform lowp vec4 _LightColor0;
					uniform lowp samplerCube _LightTexture0;
					uniform highp mat4 unity_WorldToLight;
					uniform sampler2D _LightTextureB0;
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					varying highp vec4 xlv_TEXCOORD0;
					varying lowp vec3 xlv_TEXCOORD1;
					varying lowp vec3 xlv_TEXCOORD2;
					varying lowp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  mediump vec3 tmpvar_2;
					  lowp vec3 worldN_3;
					  lowp vec4 c_4;
					  lowp vec3 tmpvar_5;
					  lowp vec3 lightDir_6;
					  highp vec3 tmpvar_7;
					  tmpvar_7 = normalize((_WorldSpaceLightPos0.xyz - xlv_TEXCOORD4));
					  lightDir_6 = tmpvar_7;
					  lowp vec3 tmpvar_8;
					  lowp vec3 tmpvar_9;
					  lowp float tmpvar_10;
					  tmpvar_9 = tmpvar_5;
					  highp vec4 tempNorm_11;
					  lowp vec4 tmpvar_12;
					  tmpvar_12 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  lowp vec4 tmpvar_13;
					  tmpvar_13.w = 0.0;
					  tmpvar_13.xyz = tmpvar_12.xyz;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = mix (tmpvar_13, _Color, vec4(_Fade)).xyz;
					  tmpvar_8 = tmpvar_14;
					  tmpvar_10 = tmpvar_12.w;
					  lowp vec4 tmpvar_15;
					  tmpvar_15 = texture2D (_BumpMap, xlv_TEXCOORD0.zw);
					  tempNorm_11 = tmpvar_15;
					  if ((_Inv == 1.0)) {
					    tempNorm_11.xy = (vec2(1.0, 1.0) - tempNorm_11.xy);
					  };
					  highp vec3 tmpvar_16;
					  tmpvar_16 = ((tempNorm_11 * 2.0) - 1.0).xyz;
					  tmpvar_9 = tmpvar_16;
					  highp vec4 tmpvar_17;
					  tmpvar_17.w = 1.0;
					  tmpvar_17.xyz = xlv_TEXCOORD4;
					  highp vec3 tmpvar_18;
					  tmpvar_18 = (unity_WorldToLight * tmpvar_17).xyz;
					  highp float tmpvar_19;
					  tmpvar_19 = dot (tmpvar_18, tmpvar_18);
					  lowp float tmpvar_20;
					  tmpvar_20 = (texture2D (_LightTextureB0, vec2(tmpvar_19)).w * textureCube (_LightTexture0, tmpvar_18).w);
					  worldN_3.x = dot (xlv_TEXCOORD1, tmpvar_9);
					  worldN_3.y = dot (xlv_TEXCOORD2, tmpvar_9);
					  worldN_3.z = dot (xlv_TEXCOORD3, tmpvar_9);
					  tmpvar_5 = worldN_3;
					  tmpvar_1 = _LightColor0.xyz;
					  tmpvar_2 = lightDir_6;
					  tmpvar_1 = (tmpvar_1 * tmpvar_20);
					  lowp vec4 c_21;
					  lowp vec4 c_22;
					  lowp float diff_23;
					  mediump float tmpvar_24;
					  tmpvar_24 = max (0.0, dot (worldN_3, tmpvar_2));
					  diff_23 = tmpvar_24;
					  c_22.xyz = ((tmpvar_8 * tmpvar_1) * diff_23);
					  c_22.w = tmpvar_10;
					  c_21.w = c_22.w;
					  c_21.xyz = c_22.xyz;
					  c_4.xyz = c_21.xyz;
					  c_4.w = 1.0;
					  gl_FragData[0] = c_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "DIRECTIONAL_COOKIE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesTANGENT;
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 unity_WorldTransformParams;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _BumpMap_ST;
					varying highp vec4 xlv_TEXCOORD0;
					varying lowp vec3 xlv_TEXCOORD1;
					varying lowp vec3 xlv_TEXCOORD2;
					varying lowp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  lowp float tangentSign_1;
					  lowp vec3 worldTangent_2;
					  lowp vec3 worldNormal_3;
					  highp vec4 tmpvar_4;
					  highp vec4 tmpvar_5;
					  tmpvar_5.w = 1.0;
					  tmpvar_5.xyz = _glesVertex.xyz;
					  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
					  highp vec4 v_6;
					  v_6.x = unity_WorldToObject[0].x;
					  v_6.y = unity_WorldToObject[1].x;
					  v_6.z = unity_WorldToObject[2].x;
					  v_6.w = unity_WorldToObject[3].x;
					  highp vec4 v_7;
					  v_7.x = unity_WorldToObject[0].y;
					  v_7.y = unity_WorldToObject[1].y;
					  v_7.z = unity_WorldToObject[2].y;
					  v_7.w = unity_WorldToObject[3].y;
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].z;
					  v_8.y = unity_WorldToObject[1].z;
					  v_8.z = unity_WorldToObject[2].z;
					  v_8.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_9;
					  tmpvar_9 = normalize(((
					    (v_6.xyz * _glesNormal.x)
					   + 
					    (v_7.xyz * _glesNormal.y)
					  ) + (v_8.xyz * _glesNormal.z)));
					  worldNormal_3 = tmpvar_9;
					  highp mat3 tmpvar_10;
					  tmpvar_10[0] = unity_ObjectToWorld[0].xyz;
					  tmpvar_10[1] = unity_ObjectToWorld[1].xyz;
					  tmpvar_10[2] = unity_ObjectToWorld[2].xyz;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = normalize((tmpvar_10 * _glesTANGENT.xyz));
					  worldTangent_2 = tmpvar_11;
					  highp float tmpvar_12;
					  tmpvar_12 = (_glesTANGENT.w * unity_WorldTransformParams.w);
					  tangentSign_1 = tmpvar_12;
					  lowp vec3 tmpvar_13;
					  tmpvar_13 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
					  lowp vec3 tmpvar_14;
					  tmpvar_14.x = worldTangent_2.x;
					  tmpvar_14.y = tmpvar_13.x;
					  tmpvar_14.z = worldNormal_3.x;
					  lowp vec3 tmpvar_15;
					  tmpvar_15.x = worldTangent_2.y;
					  tmpvar_15.y = tmpvar_13.y;
					  tmpvar_15.z = worldNormal_3.y;
					  lowp vec3 tmpvar_16;
					  tmpvar_16.x = worldTangent_2.z;
					  tmpvar_16.y = tmpvar_13.z;
					  tmpvar_16.z = worldNormal_3.z;
					  gl_Position = (glstate_matrix_mvp * tmpvar_5);
					  xlv_TEXCOORD0 = tmpvar_4;
					  xlv_TEXCOORD1 = tmpvar_14;
					  xlv_TEXCOORD2 = tmpvar_15;
					  xlv_TEXCOORD3 = tmpvar_16;
					  xlv_TEXCOORD4 = (unity_ObjectToWorld * _glesVertex).xyz;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D _LightTexture0;
					uniform highp mat4 unity_WorldToLight;
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					varying highp vec4 xlv_TEXCOORD0;
					varying lowp vec3 xlv_TEXCOORD1;
					varying lowp vec3 xlv_TEXCOORD2;
					varying lowp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  mediump vec3 tmpvar_2;
					  lowp vec3 worldN_3;
					  lowp vec4 c_4;
					  lowp vec3 tmpvar_5;
					  lowp vec3 lightDir_6;
					  mediump vec3 tmpvar_7;
					  tmpvar_7 = _WorldSpaceLightPos0.xyz;
					  lightDir_6 = tmpvar_7;
					  lowp vec3 tmpvar_8;
					  lowp vec3 tmpvar_9;
					  lowp float tmpvar_10;
					  tmpvar_9 = tmpvar_5;
					  highp vec4 tempNorm_11;
					  lowp vec4 tmpvar_12;
					  tmpvar_12 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  lowp vec4 tmpvar_13;
					  tmpvar_13.w = 0.0;
					  tmpvar_13.xyz = tmpvar_12.xyz;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = mix (tmpvar_13, _Color, vec4(_Fade)).xyz;
					  tmpvar_8 = tmpvar_14;
					  tmpvar_10 = tmpvar_12.w;
					  lowp vec4 tmpvar_15;
					  tmpvar_15 = texture2D (_BumpMap, xlv_TEXCOORD0.zw);
					  tempNorm_11 = tmpvar_15;
					  if ((_Inv == 1.0)) {
					    tempNorm_11.xy = (vec2(1.0, 1.0) - tempNorm_11.xy);
					  };
					  highp vec3 tmpvar_16;
					  tmpvar_16 = ((tempNorm_11 * 2.0) - 1.0).xyz;
					  tmpvar_9 = tmpvar_16;
					  highp vec4 tmpvar_17;
					  tmpvar_17.w = 1.0;
					  tmpvar_17.xyz = xlv_TEXCOORD4;
					  highp vec2 tmpvar_18;
					  tmpvar_18 = (unity_WorldToLight * tmpvar_17).xy;
					  lowp float tmpvar_19;
					  tmpvar_19 = texture2D (_LightTexture0, tmpvar_18).w;
					  worldN_3.x = dot (xlv_TEXCOORD1, tmpvar_9);
					  worldN_3.y = dot (xlv_TEXCOORD2, tmpvar_9);
					  worldN_3.z = dot (xlv_TEXCOORD3, tmpvar_9);
					  tmpvar_5 = worldN_3;
					  tmpvar_1 = _LightColor0.xyz;
					  tmpvar_2 = lightDir_6;
					  tmpvar_1 = (tmpvar_1 * tmpvar_19);
					  lowp vec4 c_20;
					  lowp vec4 c_21;
					  lowp float diff_22;
					  mediump float tmpvar_23;
					  tmpvar_23 = max (0.0, dot (worldN_3, tmpvar_2));
					  diff_22 = tmpvar_23;
					  c_21.xyz = ((tmpvar_8 * tmpvar_1) * diff_22);
					  c_21.w = tmpvar_10;
					  c_20.w = c_21.w;
					  c_20.xyz = c_21.xyz;
					  c_4.xyz = c_20.xyz;
					  c_4.w = 1.0;
					  gl_FragData[0] = c_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "DIRECTIONAL_COOKIE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesTANGENT;
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 unity_WorldTransformParams;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _BumpMap_ST;
					varying highp vec4 xlv_TEXCOORD0;
					varying lowp vec3 xlv_TEXCOORD1;
					varying lowp vec3 xlv_TEXCOORD2;
					varying lowp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  lowp float tangentSign_1;
					  lowp vec3 worldTangent_2;
					  lowp vec3 worldNormal_3;
					  highp vec4 tmpvar_4;
					  highp vec4 tmpvar_5;
					  tmpvar_5.w = 1.0;
					  tmpvar_5.xyz = _glesVertex.xyz;
					  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
					  highp vec4 v_6;
					  v_6.x = unity_WorldToObject[0].x;
					  v_6.y = unity_WorldToObject[1].x;
					  v_6.z = unity_WorldToObject[2].x;
					  v_6.w = unity_WorldToObject[3].x;
					  highp vec4 v_7;
					  v_7.x = unity_WorldToObject[0].y;
					  v_7.y = unity_WorldToObject[1].y;
					  v_7.z = unity_WorldToObject[2].y;
					  v_7.w = unity_WorldToObject[3].y;
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].z;
					  v_8.y = unity_WorldToObject[1].z;
					  v_8.z = unity_WorldToObject[2].z;
					  v_8.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_9;
					  tmpvar_9 = normalize(((
					    (v_6.xyz * _glesNormal.x)
					   + 
					    (v_7.xyz * _glesNormal.y)
					  ) + (v_8.xyz * _glesNormal.z)));
					  worldNormal_3 = tmpvar_9;
					  highp mat3 tmpvar_10;
					  tmpvar_10[0] = unity_ObjectToWorld[0].xyz;
					  tmpvar_10[1] = unity_ObjectToWorld[1].xyz;
					  tmpvar_10[2] = unity_ObjectToWorld[2].xyz;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = normalize((tmpvar_10 * _glesTANGENT.xyz));
					  worldTangent_2 = tmpvar_11;
					  highp float tmpvar_12;
					  tmpvar_12 = (_glesTANGENT.w * unity_WorldTransformParams.w);
					  tangentSign_1 = tmpvar_12;
					  lowp vec3 tmpvar_13;
					  tmpvar_13 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
					  lowp vec3 tmpvar_14;
					  tmpvar_14.x = worldTangent_2.x;
					  tmpvar_14.y = tmpvar_13.x;
					  tmpvar_14.z = worldNormal_3.x;
					  lowp vec3 tmpvar_15;
					  tmpvar_15.x = worldTangent_2.y;
					  tmpvar_15.y = tmpvar_13.y;
					  tmpvar_15.z = worldNormal_3.y;
					  lowp vec3 tmpvar_16;
					  tmpvar_16.x = worldTangent_2.z;
					  tmpvar_16.y = tmpvar_13.z;
					  tmpvar_16.z = worldNormal_3.z;
					  gl_Position = (glstate_matrix_mvp * tmpvar_5);
					  xlv_TEXCOORD0 = tmpvar_4;
					  xlv_TEXCOORD1 = tmpvar_14;
					  xlv_TEXCOORD2 = tmpvar_15;
					  xlv_TEXCOORD3 = tmpvar_16;
					  xlv_TEXCOORD4 = (unity_ObjectToWorld * _glesVertex).xyz;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D _LightTexture0;
					uniform highp mat4 unity_WorldToLight;
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					varying highp vec4 xlv_TEXCOORD0;
					varying lowp vec3 xlv_TEXCOORD1;
					varying lowp vec3 xlv_TEXCOORD2;
					varying lowp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  mediump vec3 tmpvar_2;
					  lowp vec3 worldN_3;
					  lowp vec4 c_4;
					  lowp vec3 tmpvar_5;
					  lowp vec3 lightDir_6;
					  mediump vec3 tmpvar_7;
					  tmpvar_7 = _WorldSpaceLightPos0.xyz;
					  lightDir_6 = tmpvar_7;
					  lowp vec3 tmpvar_8;
					  lowp vec3 tmpvar_9;
					  lowp float tmpvar_10;
					  tmpvar_9 = tmpvar_5;
					  highp vec4 tempNorm_11;
					  lowp vec4 tmpvar_12;
					  tmpvar_12 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  lowp vec4 tmpvar_13;
					  tmpvar_13.w = 0.0;
					  tmpvar_13.xyz = tmpvar_12.xyz;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = mix (tmpvar_13, _Color, vec4(_Fade)).xyz;
					  tmpvar_8 = tmpvar_14;
					  tmpvar_10 = tmpvar_12.w;
					  lowp vec4 tmpvar_15;
					  tmpvar_15 = texture2D (_BumpMap, xlv_TEXCOORD0.zw);
					  tempNorm_11 = tmpvar_15;
					  if ((_Inv == 1.0)) {
					    tempNorm_11.xy = (vec2(1.0, 1.0) - tempNorm_11.xy);
					  };
					  highp vec3 tmpvar_16;
					  tmpvar_16 = ((tempNorm_11 * 2.0) - 1.0).xyz;
					  tmpvar_9 = tmpvar_16;
					  highp vec4 tmpvar_17;
					  tmpvar_17.w = 1.0;
					  tmpvar_17.xyz = xlv_TEXCOORD4;
					  highp vec2 tmpvar_18;
					  tmpvar_18 = (unity_WorldToLight * tmpvar_17).xy;
					  lowp float tmpvar_19;
					  tmpvar_19 = texture2D (_LightTexture0, tmpvar_18).w;
					  worldN_3.x = dot (xlv_TEXCOORD1, tmpvar_9);
					  worldN_3.y = dot (xlv_TEXCOORD2, tmpvar_9);
					  worldN_3.z = dot (xlv_TEXCOORD3, tmpvar_9);
					  tmpvar_5 = worldN_3;
					  tmpvar_1 = _LightColor0.xyz;
					  tmpvar_2 = lightDir_6;
					  tmpvar_1 = (tmpvar_1 * tmpvar_19);
					  lowp vec4 c_20;
					  lowp vec4 c_21;
					  lowp float diff_22;
					  mediump float tmpvar_23;
					  tmpvar_23 = max (0.0, dot (worldN_3, tmpvar_2));
					  diff_22 = tmpvar_23;
					  c_21.xyz = ((tmpvar_8 * tmpvar_1) * diff_22);
					  c_21.w = tmpvar_10;
					  c_20.w = c_21.w;
					  c_20.xyz = c_21.xyz;
					  c_4.xyz = c_20.xyz;
					  c_4.w = 1.0;
					  gl_FragData[0] = c_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "DIRECTIONAL_COOKIE" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesTANGENT;
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 unity_WorldTransformParams;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _BumpMap_ST;
					varying highp vec4 xlv_TEXCOORD0;
					varying lowp vec3 xlv_TEXCOORD1;
					varying lowp vec3 xlv_TEXCOORD2;
					varying lowp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  lowp float tangentSign_1;
					  lowp vec3 worldTangent_2;
					  lowp vec3 worldNormal_3;
					  highp vec4 tmpvar_4;
					  highp vec4 tmpvar_5;
					  tmpvar_5.w = 1.0;
					  tmpvar_5.xyz = _glesVertex.xyz;
					  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
					  highp vec4 v_6;
					  v_6.x = unity_WorldToObject[0].x;
					  v_6.y = unity_WorldToObject[1].x;
					  v_6.z = unity_WorldToObject[2].x;
					  v_6.w = unity_WorldToObject[3].x;
					  highp vec4 v_7;
					  v_7.x = unity_WorldToObject[0].y;
					  v_7.y = unity_WorldToObject[1].y;
					  v_7.z = unity_WorldToObject[2].y;
					  v_7.w = unity_WorldToObject[3].y;
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].z;
					  v_8.y = unity_WorldToObject[1].z;
					  v_8.z = unity_WorldToObject[2].z;
					  v_8.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_9;
					  tmpvar_9 = normalize(((
					    (v_6.xyz * _glesNormal.x)
					   + 
					    (v_7.xyz * _glesNormal.y)
					  ) + (v_8.xyz * _glesNormal.z)));
					  worldNormal_3 = tmpvar_9;
					  highp mat3 tmpvar_10;
					  tmpvar_10[0] = unity_ObjectToWorld[0].xyz;
					  tmpvar_10[1] = unity_ObjectToWorld[1].xyz;
					  tmpvar_10[2] = unity_ObjectToWorld[2].xyz;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = normalize((tmpvar_10 * _glesTANGENT.xyz));
					  worldTangent_2 = tmpvar_11;
					  highp float tmpvar_12;
					  tmpvar_12 = (_glesTANGENT.w * unity_WorldTransformParams.w);
					  tangentSign_1 = tmpvar_12;
					  lowp vec3 tmpvar_13;
					  tmpvar_13 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
					  lowp vec3 tmpvar_14;
					  tmpvar_14.x = worldTangent_2.x;
					  tmpvar_14.y = tmpvar_13.x;
					  tmpvar_14.z = worldNormal_3.x;
					  lowp vec3 tmpvar_15;
					  tmpvar_15.x = worldTangent_2.y;
					  tmpvar_15.y = tmpvar_13.y;
					  tmpvar_15.z = worldNormal_3.y;
					  lowp vec3 tmpvar_16;
					  tmpvar_16.x = worldTangent_2.z;
					  tmpvar_16.y = tmpvar_13.z;
					  tmpvar_16.z = worldNormal_3.z;
					  gl_Position = (glstate_matrix_mvp * tmpvar_5);
					  xlv_TEXCOORD0 = tmpvar_4;
					  xlv_TEXCOORD1 = tmpvar_14;
					  xlv_TEXCOORD2 = tmpvar_15;
					  xlv_TEXCOORD3 = tmpvar_16;
					  xlv_TEXCOORD4 = (unity_ObjectToWorld * _glesVertex).xyz;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform mediump vec4 _WorldSpaceLightPos0;
					uniform lowp vec4 _LightColor0;
					uniform sampler2D _LightTexture0;
					uniform highp mat4 unity_WorldToLight;
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					varying highp vec4 xlv_TEXCOORD0;
					varying lowp vec3 xlv_TEXCOORD1;
					varying lowp vec3 xlv_TEXCOORD2;
					varying lowp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  mediump vec3 tmpvar_1;
					  mediump vec3 tmpvar_2;
					  lowp vec3 worldN_3;
					  lowp vec4 c_4;
					  lowp vec3 tmpvar_5;
					  lowp vec3 lightDir_6;
					  mediump vec3 tmpvar_7;
					  tmpvar_7 = _WorldSpaceLightPos0.xyz;
					  lightDir_6 = tmpvar_7;
					  lowp vec3 tmpvar_8;
					  lowp vec3 tmpvar_9;
					  lowp float tmpvar_10;
					  tmpvar_9 = tmpvar_5;
					  highp vec4 tempNorm_11;
					  lowp vec4 tmpvar_12;
					  tmpvar_12 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
					  lowp vec4 tmpvar_13;
					  tmpvar_13.w = 0.0;
					  tmpvar_13.xyz = tmpvar_12.xyz;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = mix (tmpvar_13, _Color, vec4(_Fade)).xyz;
					  tmpvar_8 = tmpvar_14;
					  tmpvar_10 = tmpvar_12.w;
					  lowp vec4 tmpvar_15;
					  tmpvar_15 = texture2D (_BumpMap, xlv_TEXCOORD0.zw);
					  tempNorm_11 = tmpvar_15;
					  if ((_Inv == 1.0)) {
					    tempNorm_11.xy = (vec2(1.0, 1.0) - tempNorm_11.xy);
					  };
					  highp vec3 tmpvar_16;
					  tmpvar_16 = ((tempNorm_11 * 2.0) - 1.0).xyz;
					  tmpvar_9 = tmpvar_16;
					  highp vec4 tmpvar_17;
					  tmpvar_17.w = 1.0;
					  tmpvar_17.xyz = xlv_TEXCOORD4;
					  highp vec2 tmpvar_18;
					  tmpvar_18 = (unity_WorldToLight * tmpvar_17).xy;
					  lowp float tmpvar_19;
					  tmpvar_19 = texture2D (_LightTexture0, tmpvar_18).w;
					  worldN_3.x = dot (xlv_TEXCOORD1, tmpvar_9);
					  worldN_3.y = dot (xlv_TEXCOORD2, tmpvar_9);
					  worldN_3.z = dot (xlv_TEXCOORD3, tmpvar_9);
					  tmpvar_5 = worldN_3;
					  tmpvar_1 = _LightColor0.xyz;
					  tmpvar_2 = lightDir_6;
					  tmpvar_1 = (tmpvar_1 * tmpvar_19);
					  lowp vec4 c_20;
					  lowp vec4 c_21;
					  lowp float diff_22;
					  mediump float tmpvar_23;
					  tmpvar_23 = max (0.0, dot (worldN_3, tmpvar_2));
					  diff_22 = tmpvar_23;
					  c_21.xyz = ((tmpvar_8 * tmpvar_1) * diff_22);
					  c_21.w = tmpvar_10;
					  c_20.w = c_21.w;
					  c_20.xyz = c_21.xyz;
					  c_4.xyz = c_20.xyz;
					  c_4.w = 1.0;
					  gl_FragData[0] = c_4;
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
}
 }
 Pass {
  Name "PREPASS"
  Tags { "LIGHTMODE"="PrePassBase" "RenderType"="Opaque" }
  GpuProgramID 177107
Program "vp" {
SubProgram "gles hw_tier01 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesTANGENT;
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 unity_WorldTransformParams;
					uniform highp vec4 _BumpMap_ST;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					void main ()
					{
					  lowp float tangentSign_1;
					  lowp vec3 worldTangent_2;
					  lowp vec3 worldNormal_3;
					  highp vec4 tmpvar_4;
					  tmpvar_4.w = 1.0;
					  tmpvar_4.xyz = _glesVertex.xyz;
					  highp vec3 tmpvar_5;
					  tmpvar_5 = (unity_ObjectToWorld * _glesVertex).xyz;
					  highp vec4 v_6;
					  v_6.x = unity_WorldToObject[0].x;
					  v_6.y = unity_WorldToObject[1].x;
					  v_6.z = unity_WorldToObject[2].x;
					  v_6.w = unity_WorldToObject[3].x;
					  highp vec4 v_7;
					  v_7.x = unity_WorldToObject[0].y;
					  v_7.y = unity_WorldToObject[1].y;
					  v_7.z = unity_WorldToObject[2].y;
					  v_7.w = unity_WorldToObject[3].y;
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].z;
					  v_8.y = unity_WorldToObject[1].z;
					  v_8.z = unity_WorldToObject[2].z;
					  v_8.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_9;
					  tmpvar_9 = normalize(((
					    (v_6.xyz * _glesNormal.x)
					   + 
					    (v_7.xyz * _glesNormal.y)
					  ) + (v_8.xyz * _glesNormal.z)));
					  worldNormal_3 = tmpvar_9;
					  highp mat3 tmpvar_10;
					  tmpvar_10[0] = unity_ObjectToWorld[0].xyz;
					  tmpvar_10[1] = unity_ObjectToWorld[1].xyz;
					  tmpvar_10[2] = unity_ObjectToWorld[2].xyz;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = normalize((tmpvar_10 * _glesTANGENT.xyz));
					  worldTangent_2 = tmpvar_11;
					  highp float tmpvar_12;
					  tmpvar_12 = (_glesTANGENT.w * unity_WorldTransformParams.w);
					  tangentSign_1 = tmpvar_12;
					  lowp vec3 tmpvar_13;
					  tmpvar_13 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
					  highp vec4 tmpvar_14;
					  tmpvar_14.x = worldTangent_2.x;
					  tmpvar_14.y = tmpvar_13.x;
					  tmpvar_14.z = worldNormal_3.x;
					  tmpvar_14.w = tmpvar_5.x;
					  highp vec4 tmpvar_15;
					  tmpvar_15.x = worldTangent_2.y;
					  tmpvar_15.y = tmpvar_13.y;
					  tmpvar_15.z = worldNormal_3.y;
					  tmpvar_15.w = tmpvar_5.y;
					  highp vec4 tmpvar_16;
					  tmpvar_16.x = worldTangent_2.z;
					  tmpvar_16.y = tmpvar_13.z;
					  tmpvar_16.z = worldNormal_3.z;
					  tmpvar_16.w = tmpvar_5.z;
					  gl_Position = (glstate_matrix_mvp * tmpvar_4);
					  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
					  xlv_TEXCOORD1 = tmpvar_14;
					  xlv_TEXCOORD2 = tmpvar_15;
					  xlv_TEXCOORD3 = tmpvar_16;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _BumpMap;
					uniform highp float _Inv;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					void main ()
					{
					  lowp vec4 res_1;
					  lowp vec3 worldN_2;
					  lowp vec3 tmpvar_3;
					  lowp vec3 tmpvar_4;
					  tmpvar_4 = tmpvar_3;
					  highp vec4 tempNorm_5;
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (_BumpMap, xlv_TEXCOORD0);
					  tempNorm_5 = tmpvar_6;
					  if ((_Inv == 1.0)) {
					    tempNorm_5.xy = (vec2(1.0, 1.0) - tempNorm_5.xy);
					  };
					  highp vec3 tmpvar_7;
					  tmpvar_7 = ((tempNorm_5 * 2.0) - 1.0).xyz;
					  tmpvar_4 = tmpvar_7;
					  highp float tmpvar_8;
					  tmpvar_8 = dot (xlv_TEXCOORD1.xyz, tmpvar_4);
					  worldN_2.x = tmpvar_8;
					  highp float tmpvar_9;
					  tmpvar_9 = dot (xlv_TEXCOORD2.xyz, tmpvar_4);
					  worldN_2.y = tmpvar_9;
					  highp float tmpvar_10;
					  tmpvar_10 = dot (xlv_TEXCOORD3.xyz, tmpvar_4);
					  worldN_2.z = tmpvar_10;
					  tmpvar_3 = worldN_2;
					  res_1.xyz = ((worldN_2 * 0.5) + 0.5);
					  res_1.w = 0.0;
					  gl_FragData[0] = res_1;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesTANGENT;
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 unity_WorldTransformParams;
					uniform highp vec4 _BumpMap_ST;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					void main ()
					{
					  lowp float tangentSign_1;
					  lowp vec3 worldTangent_2;
					  lowp vec3 worldNormal_3;
					  highp vec4 tmpvar_4;
					  tmpvar_4.w = 1.0;
					  tmpvar_4.xyz = _glesVertex.xyz;
					  highp vec3 tmpvar_5;
					  tmpvar_5 = (unity_ObjectToWorld * _glesVertex).xyz;
					  highp vec4 v_6;
					  v_6.x = unity_WorldToObject[0].x;
					  v_6.y = unity_WorldToObject[1].x;
					  v_6.z = unity_WorldToObject[2].x;
					  v_6.w = unity_WorldToObject[3].x;
					  highp vec4 v_7;
					  v_7.x = unity_WorldToObject[0].y;
					  v_7.y = unity_WorldToObject[1].y;
					  v_7.z = unity_WorldToObject[2].y;
					  v_7.w = unity_WorldToObject[3].y;
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].z;
					  v_8.y = unity_WorldToObject[1].z;
					  v_8.z = unity_WorldToObject[2].z;
					  v_8.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_9;
					  tmpvar_9 = normalize(((
					    (v_6.xyz * _glesNormal.x)
					   + 
					    (v_7.xyz * _glesNormal.y)
					  ) + (v_8.xyz * _glesNormal.z)));
					  worldNormal_3 = tmpvar_9;
					  highp mat3 tmpvar_10;
					  tmpvar_10[0] = unity_ObjectToWorld[0].xyz;
					  tmpvar_10[1] = unity_ObjectToWorld[1].xyz;
					  tmpvar_10[2] = unity_ObjectToWorld[2].xyz;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = normalize((tmpvar_10 * _glesTANGENT.xyz));
					  worldTangent_2 = tmpvar_11;
					  highp float tmpvar_12;
					  tmpvar_12 = (_glesTANGENT.w * unity_WorldTransformParams.w);
					  tangentSign_1 = tmpvar_12;
					  lowp vec3 tmpvar_13;
					  tmpvar_13 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
					  highp vec4 tmpvar_14;
					  tmpvar_14.x = worldTangent_2.x;
					  tmpvar_14.y = tmpvar_13.x;
					  tmpvar_14.z = worldNormal_3.x;
					  tmpvar_14.w = tmpvar_5.x;
					  highp vec4 tmpvar_15;
					  tmpvar_15.x = worldTangent_2.y;
					  tmpvar_15.y = tmpvar_13.y;
					  tmpvar_15.z = worldNormal_3.y;
					  tmpvar_15.w = tmpvar_5.y;
					  highp vec4 tmpvar_16;
					  tmpvar_16.x = worldTangent_2.z;
					  tmpvar_16.y = tmpvar_13.z;
					  tmpvar_16.z = worldNormal_3.z;
					  tmpvar_16.w = tmpvar_5.z;
					  gl_Position = (glstate_matrix_mvp * tmpvar_4);
					  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
					  xlv_TEXCOORD1 = tmpvar_14;
					  xlv_TEXCOORD2 = tmpvar_15;
					  xlv_TEXCOORD3 = tmpvar_16;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _BumpMap;
					uniform highp float _Inv;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					void main ()
					{
					  lowp vec4 res_1;
					  lowp vec3 worldN_2;
					  lowp vec3 tmpvar_3;
					  lowp vec3 tmpvar_4;
					  tmpvar_4 = tmpvar_3;
					  highp vec4 tempNorm_5;
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (_BumpMap, xlv_TEXCOORD0);
					  tempNorm_5 = tmpvar_6;
					  if ((_Inv == 1.0)) {
					    tempNorm_5.xy = (vec2(1.0, 1.0) - tempNorm_5.xy);
					  };
					  highp vec3 tmpvar_7;
					  tmpvar_7 = ((tempNorm_5 * 2.0) - 1.0).xyz;
					  tmpvar_4 = tmpvar_7;
					  highp float tmpvar_8;
					  tmpvar_8 = dot (xlv_TEXCOORD1.xyz, tmpvar_4);
					  worldN_2.x = tmpvar_8;
					  highp float tmpvar_9;
					  tmpvar_9 = dot (xlv_TEXCOORD2.xyz, tmpvar_4);
					  worldN_2.y = tmpvar_9;
					  highp float tmpvar_10;
					  tmpvar_10 = dot (xlv_TEXCOORD3.xyz, tmpvar_4);
					  worldN_2.z = tmpvar_10;
					  tmpvar_3 = worldN_2;
					  res_1.xyz = ((worldN_2 * 0.5) + 0.5);
					  res_1.w = 0.0;
					  gl_FragData[0] = res_1;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesTANGENT;
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 unity_WorldTransformParams;
					uniform highp vec4 _BumpMap_ST;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					void main ()
					{
					  lowp float tangentSign_1;
					  lowp vec3 worldTangent_2;
					  lowp vec3 worldNormal_3;
					  highp vec4 tmpvar_4;
					  tmpvar_4.w = 1.0;
					  tmpvar_4.xyz = _glesVertex.xyz;
					  highp vec3 tmpvar_5;
					  tmpvar_5 = (unity_ObjectToWorld * _glesVertex).xyz;
					  highp vec4 v_6;
					  v_6.x = unity_WorldToObject[0].x;
					  v_6.y = unity_WorldToObject[1].x;
					  v_6.z = unity_WorldToObject[2].x;
					  v_6.w = unity_WorldToObject[3].x;
					  highp vec4 v_7;
					  v_7.x = unity_WorldToObject[0].y;
					  v_7.y = unity_WorldToObject[1].y;
					  v_7.z = unity_WorldToObject[2].y;
					  v_7.w = unity_WorldToObject[3].y;
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].z;
					  v_8.y = unity_WorldToObject[1].z;
					  v_8.z = unity_WorldToObject[2].z;
					  v_8.w = unity_WorldToObject[3].z;
					  highp vec3 tmpvar_9;
					  tmpvar_9 = normalize(((
					    (v_6.xyz * _glesNormal.x)
					   + 
					    (v_7.xyz * _glesNormal.y)
					  ) + (v_8.xyz * _glesNormal.z)));
					  worldNormal_3 = tmpvar_9;
					  highp mat3 tmpvar_10;
					  tmpvar_10[0] = unity_ObjectToWorld[0].xyz;
					  tmpvar_10[1] = unity_ObjectToWorld[1].xyz;
					  tmpvar_10[2] = unity_ObjectToWorld[2].xyz;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = normalize((tmpvar_10 * _glesTANGENT.xyz));
					  worldTangent_2 = tmpvar_11;
					  highp float tmpvar_12;
					  tmpvar_12 = (_glesTANGENT.w * unity_WorldTransformParams.w);
					  tangentSign_1 = tmpvar_12;
					  lowp vec3 tmpvar_13;
					  tmpvar_13 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
					  highp vec4 tmpvar_14;
					  tmpvar_14.x = worldTangent_2.x;
					  tmpvar_14.y = tmpvar_13.x;
					  tmpvar_14.z = worldNormal_3.x;
					  tmpvar_14.w = tmpvar_5.x;
					  highp vec4 tmpvar_15;
					  tmpvar_15.x = worldTangent_2.y;
					  tmpvar_15.y = tmpvar_13.y;
					  tmpvar_15.z = worldNormal_3.y;
					  tmpvar_15.w = tmpvar_5.y;
					  highp vec4 tmpvar_16;
					  tmpvar_16.x = worldTangent_2.z;
					  tmpvar_16.y = tmpvar_13.z;
					  tmpvar_16.z = worldNormal_3.z;
					  tmpvar_16.w = tmpvar_5.z;
					  gl_Position = (glstate_matrix_mvp * tmpvar_4);
					  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
					  xlv_TEXCOORD1 = tmpvar_14;
					  xlv_TEXCOORD2 = tmpvar_15;
					  xlv_TEXCOORD3 = tmpvar_16;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _BumpMap;
					uniform highp float _Inv;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					void main ()
					{
					  lowp vec4 res_1;
					  lowp vec3 worldN_2;
					  lowp vec3 tmpvar_3;
					  lowp vec3 tmpvar_4;
					  tmpvar_4 = tmpvar_3;
					  highp vec4 tempNorm_5;
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (_BumpMap, xlv_TEXCOORD0);
					  tempNorm_5 = tmpvar_6;
					  if ((_Inv == 1.0)) {
					    tempNorm_5.xy = (vec2(1.0, 1.0) - tempNorm_5.xy);
					  };
					  highp vec3 tmpvar_7;
					  tmpvar_7 = ((tempNorm_5 * 2.0) - 1.0).xyz;
					  tmpvar_4 = tmpvar_7;
					  highp float tmpvar_8;
					  tmpvar_8 = dot (xlv_TEXCOORD1.xyz, tmpvar_4);
					  worldN_2.x = tmpvar_8;
					  highp float tmpvar_9;
					  tmpvar_9 = dot (xlv_TEXCOORD2.xyz, tmpvar_4);
					  worldN_2.y = tmpvar_9;
					  highp float tmpvar_10;
					  tmpvar_10 = dot (xlv_TEXCOORD3.xyz, tmpvar_4);
					  worldN_2.z = tmpvar_10;
					  tmpvar_3 = worldN_2;
					  res_1.xyz = ((worldN_2 * 0.5) + 0.5);
					  res_1.w = 0.0;
					  gl_FragData[0] = res_1;
					}
					
					
					#endif
}
}
Program "fp" {
SubProgram "gles hw_tier01 " {

}
SubProgram "gles hw_tier02 " {

}
SubProgram "gles hw_tier03 " {

}
}
 }
 Pass {
  Name "PREPASS"
  Tags { "LIGHTMODE"="PrePassFinal" "RenderType"="Opaque" }
  ZWrite Off
  GpuProgramID 209854
Program "vp" {
SubProgram "gles hw_tier01 " {
Keywords { "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp vec4 _ProjectionParams;
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
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  highp vec4 tmpvar_1;
					  highp vec3 tmpvar_2;
					  highp vec4 tmpvar_3;
					  highp vec4 tmpvar_4;
					  tmpvar_4.w = 1.0;
					  tmpvar_4.xyz = _glesVertex.xyz;
					  tmpvar_3 = (glstate_matrix_mvp * tmpvar_4);
					  highp vec4 o_5;
					  highp vec4 tmpvar_6;
					  tmpvar_6 = (tmpvar_3 * 0.5);
					  highp vec2 tmpvar_7;
					  tmpvar_7.x = tmpvar_6.x;
					  tmpvar_7.y = (tmpvar_6.y * _ProjectionParams.x);
					  o_5.xy = (tmpvar_7 + tmpvar_6.w);
					  o_5.zw = tmpvar_3.zw;
					  tmpvar_1.zw = vec2(0.0, 0.0);
					  tmpvar_1.xy = vec2(0.0, 0.0);
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].x;
					  v_8.y = unity_WorldToObject[1].x;
					  v_8.z = unity_WorldToObject[2].x;
					  v_8.w = unity_WorldToObject[3].x;
					  highp vec4 v_9;
					  v_9.x = unity_WorldToObject[0].y;
					  v_9.y = unity_WorldToObject[1].y;
					  v_9.z = unity_WorldToObject[2].y;
					  v_9.w = unity_WorldToObject[3].y;
					  highp vec4 v_10;
					  v_10.x = unity_WorldToObject[0].z;
					  v_10.y = unity_WorldToObject[1].z;
					  v_10.z = unity_WorldToObject[2].z;
					  v_10.w = unity_WorldToObject[3].z;
					  highp vec4 tmpvar_11;
					  tmpvar_11.w = 1.0;
					  tmpvar_11.xyz = normalize(((
					    (v_8.xyz * _glesNormal.x)
					   + 
					    (v_9.xyz * _glesNormal.y)
					  ) + (v_10.xyz * _glesNormal.z)));
					  mediump vec4 normal_12;
					  normal_12 = tmpvar_11;
					  mediump vec3 res_13;
					  mediump vec3 x_14;
					  x_14.x = dot (unity_SHAr, normal_12);
					  x_14.y = dot (unity_SHAg, normal_12);
					  x_14.z = dot (unity_SHAb, normal_12);
					  mediump vec3 x1_15;
					  mediump vec4 tmpvar_16;
					  tmpvar_16 = (normal_12.xyzz * normal_12.yzzx);
					  x1_15.x = dot (unity_SHBr, tmpvar_16);
					  x1_15.y = dot (unity_SHBg, tmpvar_16);
					  x1_15.z = dot (unity_SHBb, tmpvar_16);
					  res_13 = (x_14 + (x1_15 + (unity_SHC.xyz * 
					    ((normal_12.x * normal_12.x) - (normal_12.y * normal_12.y))
					  )));
					  res_13 = max (((1.055 * 
					    pow (max (res_13, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  tmpvar_2 = res_13;
					  gl_Position = tmpvar_3;
					  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  xlv_TEXCOORD1 = (unity_ObjectToWorld * _glesVertex).xyz;
					  xlv_TEXCOORD2 = o_5;
					  xlv_TEXCOORD3 = tmpvar_1;
					  xlv_TEXCOORD4 = tmpvar_2;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					uniform sampler2D _LightBuffer;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  mediump vec4 c_2;
					  mediump vec4 light_3;
					  lowp vec3 tmpvar_4;
					  highp vec2 tmpvar_5;
					  tmpvar_5.x = 1.0;
					  lowp vec3 tmpvar_6;
					  lowp vec3 tmpvar_7;
					  lowp float tmpvar_8;
					  tmpvar_7 = tmpvar_4;
					  highp vec4 tempNorm_9;
					  lowp vec4 tmpvar_10;
					  tmpvar_10 = texture2D (_MainTex, xlv_TEXCOORD0);
					  lowp vec4 tmpvar_11;
					  tmpvar_11.w = 0.0;
					  tmpvar_11.xyz = tmpvar_10.xyz;
					  highp vec3 tmpvar_12;
					  tmpvar_12 = mix (tmpvar_11, _Color, vec4(_Fade)).xyz;
					  tmpvar_6 = tmpvar_12;
					  tmpvar_8 = tmpvar_10.w;
					  lowp vec4 tmpvar_13;
					  tmpvar_13 = texture2D (_BumpMap, tmpvar_5);
					  tempNorm_9 = tmpvar_13;
					  if ((_Inv == 1.0)) {
					    tempNorm_9.xy = (vec2(1.0, 1.0) - tempNorm_9.xy);
					  };
					  highp vec3 tmpvar_14;
					  tmpvar_14 = ((tempNorm_9 * 2.0) - 1.0).xyz;
					  tmpvar_7 = tmpvar_14;
					  tmpvar_4 = tmpvar_7;
					  lowp vec4 tmpvar_15;
					  tmpvar_15 = texture2DProj (_LightBuffer, xlv_TEXCOORD2);
					  light_3 = tmpvar_15;
					  light_3 = -(log2(max (light_3, vec4(0.001, 0.001, 0.001, 0.001))));
					  light_3.xyz = (light_3.xyz + xlv_TEXCOORD4);
					  lowp vec4 c_16;
					  c_16.xyz = (tmpvar_6 * light_3.xyz);
					  c_16.w = tmpvar_8;
					  c_2.xyz = c_16.xyz;
					  c_2.w = 1.0;
					  tmpvar_1 = c_2;
					  gl_FragData[0] = tmpvar_1;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp vec4 _ProjectionParams;
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
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  highp vec4 tmpvar_1;
					  highp vec3 tmpvar_2;
					  highp vec4 tmpvar_3;
					  highp vec4 tmpvar_4;
					  tmpvar_4.w = 1.0;
					  tmpvar_4.xyz = _glesVertex.xyz;
					  tmpvar_3 = (glstate_matrix_mvp * tmpvar_4);
					  highp vec4 o_5;
					  highp vec4 tmpvar_6;
					  tmpvar_6 = (tmpvar_3 * 0.5);
					  highp vec2 tmpvar_7;
					  tmpvar_7.x = tmpvar_6.x;
					  tmpvar_7.y = (tmpvar_6.y * _ProjectionParams.x);
					  o_5.xy = (tmpvar_7 + tmpvar_6.w);
					  o_5.zw = tmpvar_3.zw;
					  tmpvar_1.zw = vec2(0.0, 0.0);
					  tmpvar_1.xy = vec2(0.0, 0.0);
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].x;
					  v_8.y = unity_WorldToObject[1].x;
					  v_8.z = unity_WorldToObject[2].x;
					  v_8.w = unity_WorldToObject[3].x;
					  highp vec4 v_9;
					  v_9.x = unity_WorldToObject[0].y;
					  v_9.y = unity_WorldToObject[1].y;
					  v_9.z = unity_WorldToObject[2].y;
					  v_9.w = unity_WorldToObject[3].y;
					  highp vec4 v_10;
					  v_10.x = unity_WorldToObject[0].z;
					  v_10.y = unity_WorldToObject[1].z;
					  v_10.z = unity_WorldToObject[2].z;
					  v_10.w = unity_WorldToObject[3].z;
					  highp vec4 tmpvar_11;
					  tmpvar_11.w = 1.0;
					  tmpvar_11.xyz = normalize(((
					    (v_8.xyz * _glesNormal.x)
					   + 
					    (v_9.xyz * _glesNormal.y)
					  ) + (v_10.xyz * _glesNormal.z)));
					  mediump vec4 normal_12;
					  normal_12 = tmpvar_11;
					  mediump vec3 res_13;
					  mediump vec3 x_14;
					  x_14.x = dot (unity_SHAr, normal_12);
					  x_14.y = dot (unity_SHAg, normal_12);
					  x_14.z = dot (unity_SHAb, normal_12);
					  mediump vec3 x1_15;
					  mediump vec4 tmpvar_16;
					  tmpvar_16 = (normal_12.xyzz * normal_12.yzzx);
					  x1_15.x = dot (unity_SHBr, tmpvar_16);
					  x1_15.y = dot (unity_SHBg, tmpvar_16);
					  x1_15.z = dot (unity_SHBb, tmpvar_16);
					  res_13 = (x_14 + (x1_15 + (unity_SHC.xyz * 
					    ((normal_12.x * normal_12.x) - (normal_12.y * normal_12.y))
					  )));
					  res_13 = max (((1.055 * 
					    pow (max (res_13, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  tmpvar_2 = res_13;
					  gl_Position = tmpvar_3;
					  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  xlv_TEXCOORD1 = (unity_ObjectToWorld * _glesVertex).xyz;
					  xlv_TEXCOORD2 = o_5;
					  xlv_TEXCOORD3 = tmpvar_1;
					  xlv_TEXCOORD4 = tmpvar_2;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					uniform sampler2D _LightBuffer;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  mediump vec4 c_2;
					  mediump vec4 light_3;
					  lowp vec3 tmpvar_4;
					  highp vec2 tmpvar_5;
					  tmpvar_5.x = 1.0;
					  lowp vec3 tmpvar_6;
					  lowp vec3 tmpvar_7;
					  lowp float tmpvar_8;
					  tmpvar_7 = tmpvar_4;
					  highp vec4 tempNorm_9;
					  lowp vec4 tmpvar_10;
					  tmpvar_10 = texture2D (_MainTex, xlv_TEXCOORD0);
					  lowp vec4 tmpvar_11;
					  tmpvar_11.w = 0.0;
					  tmpvar_11.xyz = tmpvar_10.xyz;
					  highp vec3 tmpvar_12;
					  tmpvar_12 = mix (tmpvar_11, _Color, vec4(_Fade)).xyz;
					  tmpvar_6 = tmpvar_12;
					  tmpvar_8 = tmpvar_10.w;
					  lowp vec4 tmpvar_13;
					  tmpvar_13 = texture2D (_BumpMap, tmpvar_5);
					  tempNorm_9 = tmpvar_13;
					  if ((_Inv == 1.0)) {
					    tempNorm_9.xy = (vec2(1.0, 1.0) - tempNorm_9.xy);
					  };
					  highp vec3 tmpvar_14;
					  tmpvar_14 = ((tempNorm_9 * 2.0) - 1.0).xyz;
					  tmpvar_7 = tmpvar_14;
					  tmpvar_4 = tmpvar_7;
					  lowp vec4 tmpvar_15;
					  tmpvar_15 = texture2DProj (_LightBuffer, xlv_TEXCOORD2);
					  light_3 = tmpvar_15;
					  light_3 = -(log2(max (light_3, vec4(0.001, 0.001, 0.001, 0.001))));
					  light_3.xyz = (light_3.xyz + xlv_TEXCOORD4);
					  lowp vec4 c_16;
					  c_16.xyz = (tmpvar_6 * light_3.xyz);
					  c_16.w = tmpvar_8;
					  c_2.xyz = c_16.xyz;
					  c_2.w = 1.0;
					  tmpvar_1 = c_2;
					  gl_FragData[0] = tmpvar_1;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp vec4 _ProjectionParams;
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
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  highp vec4 tmpvar_1;
					  highp vec3 tmpvar_2;
					  highp vec4 tmpvar_3;
					  highp vec4 tmpvar_4;
					  tmpvar_4.w = 1.0;
					  tmpvar_4.xyz = _glesVertex.xyz;
					  tmpvar_3 = (glstate_matrix_mvp * tmpvar_4);
					  highp vec4 o_5;
					  highp vec4 tmpvar_6;
					  tmpvar_6 = (tmpvar_3 * 0.5);
					  highp vec2 tmpvar_7;
					  tmpvar_7.x = tmpvar_6.x;
					  tmpvar_7.y = (tmpvar_6.y * _ProjectionParams.x);
					  o_5.xy = (tmpvar_7 + tmpvar_6.w);
					  o_5.zw = tmpvar_3.zw;
					  tmpvar_1.zw = vec2(0.0, 0.0);
					  tmpvar_1.xy = vec2(0.0, 0.0);
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].x;
					  v_8.y = unity_WorldToObject[1].x;
					  v_8.z = unity_WorldToObject[2].x;
					  v_8.w = unity_WorldToObject[3].x;
					  highp vec4 v_9;
					  v_9.x = unity_WorldToObject[0].y;
					  v_9.y = unity_WorldToObject[1].y;
					  v_9.z = unity_WorldToObject[2].y;
					  v_9.w = unity_WorldToObject[3].y;
					  highp vec4 v_10;
					  v_10.x = unity_WorldToObject[0].z;
					  v_10.y = unity_WorldToObject[1].z;
					  v_10.z = unity_WorldToObject[2].z;
					  v_10.w = unity_WorldToObject[3].z;
					  highp vec4 tmpvar_11;
					  tmpvar_11.w = 1.0;
					  tmpvar_11.xyz = normalize(((
					    (v_8.xyz * _glesNormal.x)
					   + 
					    (v_9.xyz * _glesNormal.y)
					  ) + (v_10.xyz * _glesNormal.z)));
					  mediump vec4 normal_12;
					  normal_12 = tmpvar_11;
					  mediump vec3 res_13;
					  mediump vec3 x_14;
					  x_14.x = dot (unity_SHAr, normal_12);
					  x_14.y = dot (unity_SHAg, normal_12);
					  x_14.z = dot (unity_SHAb, normal_12);
					  mediump vec3 x1_15;
					  mediump vec4 tmpvar_16;
					  tmpvar_16 = (normal_12.xyzz * normal_12.yzzx);
					  x1_15.x = dot (unity_SHBr, tmpvar_16);
					  x1_15.y = dot (unity_SHBg, tmpvar_16);
					  x1_15.z = dot (unity_SHBb, tmpvar_16);
					  res_13 = (x_14 + (x1_15 + (unity_SHC.xyz * 
					    ((normal_12.x * normal_12.x) - (normal_12.y * normal_12.y))
					  )));
					  res_13 = max (((1.055 * 
					    pow (max (res_13, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  tmpvar_2 = res_13;
					  gl_Position = tmpvar_3;
					  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  xlv_TEXCOORD1 = (unity_ObjectToWorld * _glesVertex).xyz;
					  xlv_TEXCOORD2 = o_5;
					  xlv_TEXCOORD3 = tmpvar_1;
					  xlv_TEXCOORD4 = tmpvar_2;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					uniform sampler2D _LightBuffer;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  mediump vec4 c_2;
					  mediump vec4 light_3;
					  lowp vec3 tmpvar_4;
					  highp vec2 tmpvar_5;
					  tmpvar_5.x = 1.0;
					  lowp vec3 tmpvar_6;
					  lowp vec3 tmpvar_7;
					  lowp float tmpvar_8;
					  tmpvar_7 = tmpvar_4;
					  highp vec4 tempNorm_9;
					  lowp vec4 tmpvar_10;
					  tmpvar_10 = texture2D (_MainTex, xlv_TEXCOORD0);
					  lowp vec4 tmpvar_11;
					  tmpvar_11.w = 0.0;
					  tmpvar_11.xyz = tmpvar_10.xyz;
					  highp vec3 tmpvar_12;
					  tmpvar_12 = mix (tmpvar_11, _Color, vec4(_Fade)).xyz;
					  tmpvar_6 = tmpvar_12;
					  tmpvar_8 = tmpvar_10.w;
					  lowp vec4 tmpvar_13;
					  tmpvar_13 = texture2D (_BumpMap, tmpvar_5);
					  tempNorm_9 = tmpvar_13;
					  if ((_Inv == 1.0)) {
					    tempNorm_9.xy = (vec2(1.0, 1.0) - tempNorm_9.xy);
					  };
					  highp vec3 tmpvar_14;
					  tmpvar_14 = ((tempNorm_9 * 2.0) - 1.0).xyz;
					  tmpvar_7 = tmpvar_14;
					  tmpvar_4 = tmpvar_7;
					  lowp vec4 tmpvar_15;
					  tmpvar_15 = texture2DProj (_LightBuffer, xlv_TEXCOORD2);
					  light_3 = tmpvar_15;
					  light_3 = -(log2(max (light_3, vec4(0.001, 0.001, 0.001, 0.001))));
					  light_3.xyz = (light_3.xyz + xlv_TEXCOORD4);
					  lowp vec4 c_16;
					  c_16.xyz = (tmpvar_6 * light_3.xyz);
					  c_16.w = tmpvar_8;
					  c_2.xyz = c_16.xyz;
					  c_2.w = 1.0;
					  tmpvar_1 = c_2;
					  gl_FragData[0] = tmpvar_1;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "UNITY_HDR_ON" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp vec4 _ProjectionParams;
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
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  highp vec4 tmpvar_1;
					  highp vec3 tmpvar_2;
					  highp vec4 tmpvar_3;
					  highp vec4 tmpvar_4;
					  tmpvar_4.w = 1.0;
					  tmpvar_4.xyz = _glesVertex.xyz;
					  tmpvar_3 = (glstate_matrix_mvp * tmpvar_4);
					  highp vec4 o_5;
					  highp vec4 tmpvar_6;
					  tmpvar_6 = (tmpvar_3 * 0.5);
					  highp vec2 tmpvar_7;
					  tmpvar_7.x = tmpvar_6.x;
					  tmpvar_7.y = (tmpvar_6.y * _ProjectionParams.x);
					  o_5.xy = (tmpvar_7 + tmpvar_6.w);
					  o_5.zw = tmpvar_3.zw;
					  tmpvar_1.zw = vec2(0.0, 0.0);
					  tmpvar_1.xy = vec2(0.0, 0.0);
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].x;
					  v_8.y = unity_WorldToObject[1].x;
					  v_8.z = unity_WorldToObject[2].x;
					  v_8.w = unity_WorldToObject[3].x;
					  highp vec4 v_9;
					  v_9.x = unity_WorldToObject[0].y;
					  v_9.y = unity_WorldToObject[1].y;
					  v_9.z = unity_WorldToObject[2].y;
					  v_9.w = unity_WorldToObject[3].y;
					  highp vec4 v_10;
					  v_10.x = unity_WorldToObject[0].z;
					  v_10.y = unity_WorldToObject[1].z;
					  v_10.z = unity_WorldToObject[2].z;
					  v_10.w = unity_WorldToObject[3].z;
					  highp vec4 tmpvar_11;
					  tmpvar_11.w = 1.0;
					  tmpvar_11.xyz = normalize(((
					    (v_8.xyz * _glesNormal.x)
					   + 
					    (v_9.xyz * _glesNormal.y)
					  ) + (v_10.xyz * _glesNormal.z)));
					  mediump vec4 normal_12;
					  normal_12 = tmpvar_11;
					  mediump vec3 res_13;
					  mediump vec3 x_14;
					  x_14.x = dot (unity_SHAr, normal_12);
					  x_14.y = dot (unity_SHAg, normal_12);
					  x_14.z = dot (unity_SHAb, normal_12);
					  mediump vec3 x1_15;
					  mediump vec4 tmpvar_16;
					  tmpvar_16 = (normal_12.xyzz * normal_12.yzzx);
					  x1_15.x = dot (unity_SHBr, tmpvar_16);
					  x1_15.y = dot (unity_SHBg, tmpvar_16);
					  x1_15.z = dot (unity_SHBb, tmpvar_16);
					  res_13 = (x_14 + (x1_15 + (unity_SHC.xyz * 
					    ((normal_12.x * normal_12.x) - (normal_12.y * normal_12.y))
					  )));
					  res_13 = max (((1.055 * 
					    pow (max (res_13, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  tmpvar_2 = res_13;
					  gl_Position = tmpvar_3;
					  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  xlv_TEXCOORD1 = (unity_ObjectToWorld * _glesVertex).xyz;
					  xlv_TEXCOORD2 = o_5;
					  xlv_TEXCOORD3 = tmpvar_1;
					  xlv_TEXCOORD4 = tmpvar_2;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					uniform sampler2D _LightBuffer;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  mediump vec4 c_2;
					  mediump vec4 light_3;
					  lowp vec3 tmpvar_4;
					  highp vec2 tmpvar_5;
					  tmpvar_5.x = 1.0;
					  lowp vec3 tmpvar_6;
					  lowp vec3 tmpvar_7;
					  lowp float tmpvar_8;
					  tmpvar_7 = tmpvar_4;
					  highp vec4 tempNorm_9;
					  lowp vec4 tmpvar_10;
					  tmpvar_10 = texture2D (_MainTex, xlv_TEXCOORD0);
					  lowp vec4 tmpvar_11;
					  tmpvar_11.w = 0.0;
					  tmpvar_11.xyz = tmpvar_10.xyz;
					  highp vec3 tmpvar_12;
					  tmpvar_12 = mix (tmpvar_11, _Color, vec4(_Fade)).xyz;
					  tmpvar_6 = tmpvar_12;
					  tmpvar_8 = tmpvar_10.w;
					  lowp vec4 tmpvar_13;
					  tmpvar_13 = texture2D (_BumpMap, tmpvar_5);
					  tempNorm_9 = tmpvar_13;
					  if ((_Inv == 1.0)) {
					    tempNorm_9.xy = (vec2(1.0, 1.0) - tempNorm_9.xy);
					  };
					  highp vec3 tmpvar_14;
					  tmpvar_14 = ((tempNorm_9 * 2.0) - 1.0).xyz;
					  tmpvar_7 = tmpvar_14;
					  tmpvar_4 = tmpvar_7;
					  lowp vec4 tmpvar_15;
					  tmpvar_15 = texture2DProj (_LightBuffer, xlv_TEXCOORD2);
					  light_3 = tmpvar_15;
					  mediump vec4 tmpvar_16;
					  tmpvar_16 = max (light_3, vec4(0.001, 0.001, 0.001, 0.001));
					  light_3.w = tmpvar_16.w;
					  light_3.xyz = (tmpvar_16.xyz + xlv_TEXCOORD4);
					  lowp vec4 c_17;
					  c_17.xyz = (tmpvar_6 * light_3.xyz);
					  c_17.w = tmpvar_8;
					  c_2.xyz = c_17.xyz;
					  c_2.w = 1.0;
					  tmpvar_1 = c_2;
					  gl_FragData[0] = tmpvar_1;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "UNITY_HDR_ON" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp vec4 _ProjectionParams;
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
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  highp vec4 tmpvar_1;
					  highp vec3 tmpvar_2;
					  highp vec4 tmpvar_3;
					  highp vec4 tmpvar_4;
					  tmpvar_4.w = 1.0;
					  tmpvar_4.xyz = _glesVertex.xyz;
					  tmpvar_3 = (glstate_matrix_mvp * tmpvar_4);
					  highp vec4 o_5;
					  highp vec4 tmpvar_6;
					  tmpvar_6 = (tmpvar_3 * 0.5);
					  highp vec2 tmpvar_7;
					  tmpvar_7.x = tmpvar_6.x;
					  tmpvar_7.y = (tmpvar_6.y * _ProjectionParams.x);
					  o_5.xy = (tmpvar_7 + tmpvar_6.w);
					  o_5.zw = tmpvar_3.zw;
					  tmpvar_1.zw = vec2(0.0, 0.0);
					  tmpvar_1.xy = vec2(0.0, 0.0);
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].x;
					  v_8.y = unity_WorldToObject[1].x;
					  v_8.z = unity_WorldToObject[2].x;
					  v_8.w = unity_WorldToObject[3].x;
					  highp vec4 v_9;
					  v_9.x = unity_WorldToObject[0].y;
					  v_9.y = unity_WorldToObject[1].y;
					  v_9.z = unity_WorldToObject[2].y;
					  v_9.w = unity_WorldToObject[3].y;
					  highp vec4 v_10;
					  v_10.x = unity_WorldToObject[0].z;
					  v_10.y = unity_WorldToObject[1].z;
					  v_10.z = unity_WorldToObject[2].z;
					  v_10.w = unity_WorldToObject[3].z;
					  highp vec4 tmpvar_11;
					  tmpvar_11.w = 1.0;
					  tmpvar_11.xyz = normalize(((
					    (v_8.xyz * _glesNormal.x)
					   + 
					    (v_9.xyz * _glesNormal.y)
					  ) + (v_10.xyz * _glesNormal.z)));
					  mediump vec4 normal_12;
					  normal_12 = tmpvar_11;
					  mediump vec3 res_13;
					  mediump vec3 x_14;
					  x_14.x = dot (unity_SHAr, normal_12);
					  x_14.y = dot (unity_SHAg, normal_12);
					  x_14.z = dot (unity_SHAb, normal_12);
					  mediump vec3 x1_15;
					  mediump vec4 tmpvar_16;
					  tmpvar_16 = (normal_12.xyzz * normal_12.yzzx);
					  x1_15.x = dot (unity_SHBr, tmpvar_16);
					  x1_15.y = dot (unity_SHBg, tmpvar_16);
					  x1_15.z = dot (unity_SHBb, tmpvar_16);
					  res_13 = (x_14 + (x1_15 + (unity_SHC.xyz * 
					    ((normal_12.x * normal_12.x) - (normal_12.y * normal_12.y))
					  )));
					  res_13 = max (((1.055 * 
					    pow (max (res_13, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  tmpvar_2 = res_13;
					  gl_Position = tmpvar_3;
					  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  xlv_TEXCOORD1 = (unity_ObjectToWorld * _glesVertex).xyz;
					  xlv_TEXCOORD2 = o_5;
					  xlv_TEXCOORD3 = tmpvar_1;
					  xlv_TEXCOORD4 = tmpvar_2;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					uniform sampler2D _LightBuffer;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  mediump vec4 c_2;
					  mediump vec4 light_3;
					  lowp vec3 tmpvar_4;
					  highp vec2 tmpvar_5;
					  tmpvar_5.x = 1.0;
					  lowp vec3 tmpvar_6;
					  lowp vec3 tmpvar_7;
					  lowp float tmpvar_8;
					  tmpvar_7 = tmpvar_4;
					  highp vec4 tempNorm_9;
					  lowp vec4 tmpvar_10;
					  tmpvar_10 = texture2D (_MainTex, xlv_TEXCOORD0);
					  lowp vec4 tmpvar_11;
					  tmpvar_11.w = 0.0;
					  tmpvar_11.xyz = tmpvar_10.xyz;
					  highp vec3 tmpvar_12;
					  tmpvar_12 = mix (tmpvar_11, _Color, vec4(_Fade)).xyz;
					  tmpvar_6 = tmpvar_12;
					  tmpvar_8 = tmpvar_10.w;
					  lowp vec4 tmpvar_13;
					  tmpvar_13 = texture2D (_BumpMap, tmpvar_5);
					  tempNorm_9 = tmpvar_13;
					  if ((_Inv == 1.0)) {
					    tempNorm_9.xy = (vec2(1.0, 1.0) - tempNorm_9.xy);
					  };
					  highp vec3 tmpvar_14;
					  tmpvar_14 = ((tempNorm_9 * 2.0) - 1.0).xyz;
					  tmpvar_7 = tmpvar_14;
					  tmpvar_4 = tmpvar_7;
					  lowp vec4 tmpvar_15;
					  tmpvar_15 = texture2DProj (_LightBuffer, xlv_TEXCOORD2);
					  light_3 = tmpvar_15;
					  mediump vec4 tmpvar_16;
					  tmpvar_16 = max (light_3, vec4(0.001, 0.001, 0.001, 0.001));
					  light_3.w = tmpvar_16.w;
					  light_3.xyz = (tmpvar_16.xyz + xlv_TEXCOORD4);
					  lowp vec4 c_17;
					  c_17.xyz = (tmpvar_6 * light_3.xyz);
					  c_17.w = tmpvar_8;
					  c_2.xyz = c_17.xyz;
					  c_2.w = 1.0;
					  tmpvar_1 = c_2;
					  gl_FragData[0] = tmpvar_1;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "UNITY_HDR_ON" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp vec4 _ProjectionParams;
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
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD1;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec4 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  highp vec4 tmpvar_1;
					  highp vec3 tmpvar_2;
					  highp vec4 tmpvar_3;
					  highp vec4 tmpvar_4;
					  tmpvar_4.w = 1.0;
					  tmpvar_4.xyz = _glesVertex.xyz;
					  tmpvar_3 = (glstate_matrix_mvp * tmpvar_4);
					  highp vec4 o_5;
					  highp vec4 tmpvar_6;
					  tmpvar_6 = (tmpvar_3 * 0.5);
					  highp vec2 tmpvar_7;
					  tmpvar_7.x = tmpvar_6.x;
					  tmpvar_7.y = (tmpvar_6.y * _ProjectionParams.x);
					  o_5.xy = (tmpvar_7 + tmpvar_6.w);
					  o_5.zw = tmpvar_3.zw;
					  tmpvar_1.zw = vec2(0.0, 0.0);
					  tmpvar_1.xy = vec2(0.0, 0.0);
					  highp vec4 v_8;
					  v_8.x = unity_WorldToObject[0].x;
					  v_8.y = unity_WorldToObject[1].x;
					  v_8.z = unity_WorldToObject[2].x;
					  v_8.w = unity_WorldToObject[3].x;
					  highp vec4 v_9;
					  v_9.x = unity_WorldToObject[0].y;
					  v_9.y = unity_WorldToObject[1].y;
					  v_9.z = unity_WorldToObject[2].y;
					  v_9.w = unity_WorldToObject[3].y;
					  highp vec4 v_10;
					  v_10.x = unity_WorldToObject[0].z;
					  v_10.y = unity_WorldToObject[1].z;
					  v_10.z = unity_WorldToObject[2].z;
					  v_10.w = unity_WorldToObject[3].z;
					  highp vec4 tmpvar_11;
					  tmpvar_11.w = 1.0;
					  tmpvar_11.xyz = normalize(((
					    (v_8.xyz * _glesNormal.x)
					   + 
					    (v_9.xyz * _glesNormal.y)
					  ) + (v_10.xyz * _glesNormal.z)));
					  mediump vec4 normal_12;
					  normal_12 = tmpvar_11;
					  mediump vec3 res_13;
					  mediump vec3 x_14;
					  x_14.x = dot (unity_SHAr, normal_12);
					  x_14.y = dot (unity_SHAg, normal_12);
					  x_14.z = dot (unity_SHAb, normal_12);
					  mediump vec3 x1_15;
					  mediump vec4 tmpvar_16;
					  tmpvar_16 = (normal_12.xyzz * normal_12.yzzx);
					  x1_15.x = dot (unity_SHBr, tmpvar_16);
					  x1_15.y = dot (unity_SHBg, tmpvar_16);
					  x1_15.z = dot (unity_SHBb, tmpvar_16);
					  res_13 = (x_14 + (x1_15 + (unity_SHC.xyz * 
					    ((normal_12.x * normal_12.x) - (normal_12.y * normal_12.y))
					  )));
					  res_13 = max (((1.055 * 
					    pow (max (res_13, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
					  ) - 0.055), vec3(0.0, 0.0, 0.0));
					  tmpvar_2 = res_13;
					  gl_Position = tmpvar_3;
					  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  xlv_TEXCOORD1 = (unity_ObjectToWorld * _glesVertex).xyz;
					  xlv_TEXCOORD2 = o_5;
					  xlv_TEXCOORD3 = tmpvar_1;
					  xlv_TEXCOORD4 = tmpvar_2;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					uniform sampler2D _BumpMap;
					uniform lowp vec4 _Color;
					uniform highp float _Inv;
					uniform highp float _Fade;
					uniform sampler2D _LightBuffer;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD2;
					varying highp vec3 xlv_TEXCOORD4;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  mediump vec4 c_2;
					  mediump vec4 light_3;
					  lowp vec3 tmpvar_4;
					  highp vec2 tmpvar_5;
					  tmpvar_5.x = 1.0;
					  lowp vec3 tmpvar_6;
					  lowp vec3 tmpvar_7;
					  lowp float tmpvar_8;
					  tmpvar_7 = tmpvar_4;
					  highp vec4 tempNorm_9;
					  lowp vec4 tmpvar_10;
					  tmpvar_10 = texture2D (_MainTex, xlv_TEXCOORD0);
					  lowp vec4 tmpvar_11;
					  tmpvar_11.w = 0.0;
					  tmpvar_11.xyz = tmpvar_10.xyz;
					  highp vec3 tmpvar_12;
					  tmpvar_12 = mix (tmpvar_11, _Color, vec4(_Fade)).xyz;
					  tmpvar_6 = tmpvar_12;
					  tmpvar_8 = tmpvar_10.w;
					  lowp vec4 tmpvar_13;
					  tmpvar_13 = texture2D (_BumpMap, tmpvar_5);
					  tempNorm_9 = tmpvar_13;
					  if ((_Inv == 1.0)) {
					    tempNorm_9.xy = (vec2(1.0, 1.0) - tempNorm_9.xy);
					  };
					  highp vec3 tmpvar_14;
					  tmpvar_14 = ((tempNorm_9 * 2.0) - 1.0).xyz;
					  tmpvar_7 = tmpvar_14;
					  tmpvar_4 = tmpvar_7;
					  lowp vec4 tmpvar_15;
					  tmpvar_15 = texture2DProj (_LightBuffer, xlv_TEXCOORD2);
					  light_3 = tmpvar_15;
					  mediump vec4 tmpvar_16;
					  tmpvar_16 = max (light_3, vec4(0.001, 0.001, 0.001, 0.001));
					  light_3.w = tmpvar_16.w;
					  light_3.xyz = (tmpvar_16.xyz + xlv_TEXCOORD4);
					  lowp vec4 c_17;
					  c_17.xyz = (tmpvar_6 * light_3.xyz);
					  c_17.w = tmpvar_8;
					  c_2.xyz = c_17.xyz;
					  c_2.w = 1.0;
					  tmpvar_1 = c_2;
					  gl_FragData[0] = tmpvar_1;
					}
					
					
					#endif
}
}
Program "fp" {
SubProgram "gles hw_tier01 " {
Keywords { "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
					
}
SubProgram "gles hw_tier01 " {
Keywords { "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "UNITY_HDR_ON" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "UNITY_HDR_ON" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "LIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "UNITY_HDR_ON" }
					
}
}
 }
}
Fallback "Diffuse"
}