Shader "Legacy Shaders/Transparent/VertexLit" {
Properties {
 _Color ("Main Color", Color) = (1.000000,1.000000,1.000000,1.000000)
 _SpecColor ("Spec Color", Color) = (1.000000,1.000000,1.000000,0.000000)
 _Emission ("Emissive Color", Color) = (0.000000,0.000000,0.000000,0.000000)
 _Shininess ("Shininess", Range(0.100000,1.000000)) = 0.700000
 _MainTex ("Base (RGB) Trans (A)", 2D) = "white" { }
}
SubShader { 
 LOD 100
 Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
 Pass {
  Tags { "LIGHTMODE"="Vertex" "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
  ZWrite Off
  Blend SrcAlpha OneMinusSrcAlpha
  ColorMask RGB
  GpuProgramID 57453
Program "vp" {
SubProgram "gles hw_tier01 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					uniform mediump vec4 unity_LightColor[8];
					uniform highp vec4 unity_LightPosition[8];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 glstate_matrix_modelview0;
					uniform highp mat4 glstate_matrix_invtrans_modelview0;
					uniform lowp vec4 glstate_lightmodel_ambient;
					uniform mediump vec4 _Color;
					uniform mediump vec4 _SpecColor;
					uniform mediump vec4 _Emission;
					uniform mediump float _Shininess;
					uniform highp vec4 _MainTex_ST;
					varying lowp vec4 xlv_COLOR0;
					varying lowp vec3 xlv_COLOR1;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  highp vec3 tmpvar_1;
					  tmpvar_1 = _glesVertex.xyz;
					  mediump float shininess_3;
					  mediump vec3 specColor_4;
					  mediump vec3 lcolor_5;
					  mediump vec3 viewDir_6;
					  mediump vec3 eyeNormal_7;
					  mediump vec4 color_8;
					  color_8 = vec4(0.0, 0.0, 0.0, 1.1);
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = tmpvar_1;
					  highp mat3 tmpvar_10;
					  tmpvar_10[0] = glstate_matrix_invtrans_modelview0[0].xyz;
					  tmpvar_10[1] = glstate_matrix_invtrans_modelview0[1].xyz;
					  tmpvar_10[2] = glstate_matrix_invtrans_modelview0[2].xyz;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = normalize((tmpvar_10 * _glesNormal));
					  eyeNormal_7 = tmpvar_11;
					  highp vec3 tmpvar_12;
					  tmpvar_12 = normalize((glstate_matrix_modelview0 * tmpvar_9).xyz);
					  viewDir_6 = -(tmpvar_12);
					  lcolor_5 = (_Emission.xyz + (_Color.xyz * glstate_lightmodel_ambient.xyz));
					  specColor_4 = vec3(0.0, 0.0, 0.0);
					  shininess_3 = (_Shininess * 128.0);
					  for (highp int il_2 = 0; il_2 < 8; il_2++) {
					    highp vec3 tmpvar_13;
					    tmpvar_13 = unity_LightPosition[il_2].xyz;
					    mediump vec3 dirToLight_14;
					    dirToLight_14 = tmpvar_13;
					    mediump vec3 specColor_15;
					    specColor_15 = specColor_4;
					    mediump float tmpvar_16;
					    tmpvar_16 = max (dot (eyeNormal_7, dirToLight_14), 0.0);
					    mediump vec3 tmpvar_17;
					    tmpvar_17 = ((tmpvar_16 * _Color.xyz) * unity_LightColor[il_2].xyz);
					    if ((tmpvar_16 > 0.0)) {
					      specColor_15 = (specColor_4 + ((0.5 * 
					        clamp (pow (max (dot (eyeNormal_7, 
					          normalize((dirToLight_14 + viewDir_6))
					        ), 0.0), shininess_3), 0.0, 1.0)
					      ) * unity_LightColor[il_2].xyz));
					    };
					    specColor_4 = specColor_15;
					    lcolor_5 = (lcolor_5 + min ((tmpvar_17 * 0.5), vec3(1.0, 1.0, 1.0)));
					  };
					  color_8.xyz = lcolor_5;
					  color_8.w = _Color.w;
					  specColor_4 = (specColor_4 * _SpecColor.xyz);
					  lowp vec4 tmpvar_18;
					  mediump vec4 tmpvar_19;
					  tmpvar_19 = clamp (color_8, 0.0, 1.0);
					  tmpvar_18 = tmpvar_19;
					  lowp vec3 tmpvar_20;
					  mediump vec3 tmpvar_21;
					  tmpvar_21 = clamp (specColor_4, 0.0, 1.0);
					  tmpvar_20 = tmpvar_21;
					  highp vec4 tmpvar_22;
					  tmpvar_22.w = 1.0;
					  tmpvar_22.xyz = tmpvar_1;
					  xlv_COLOR0 = tmpvar_18;
					  xlv_COLOR1 = tmpvar_20;
					  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  gl_Position = (glstate_matrix_mvp * tmpvar_22);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					varying lowp vec4 xlv_COLOR0;
					varying lowp vec3 xlv_COLOR1;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 col_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0);
					  col_1.xyz = (tmpvar_2 * xlv_COLOR0).xyz;
					  col_1.xyz = (col_1 * 2.0).xyz;
					  col_1.w = (tmpvar_2.w * xlv_COLOR0.w);
					  col_1.xyz = (col_1.xyz + xlv_COLOR1);
					  gl_FragData[0] = col_1;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					uniform mediump vec4 unity_LightColor[8];
					uniform highp vec4 unity_LightPosition[8];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 glstate_matrix_modelview0;
					uniform highp mat4 glstate_matrix_invtrans_modelview0;
					uniform lowp vec4 glstate_lightmodel_ambient;
					uniform mediump vec4 _Color;
					uniform mediump vec4 _SpecColor;
					uniform mediump vec4 _Emission;
					uniform mediump float _Shininess;
					uniform highp vec4 _MainTex_ST;
					varying lowp vec4 xlv_COLOR0;
					varying lowp vec3 xlv_COLOR1;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  highp vec3 tmpvar_1;
					  tmpvar_1 = _glesVertex.xyz;
					  mediump float shininess_3;
					  mediump vec3 specColor_4;
					  mediump vec3 lcolor_5;
					  mediump vec3 viewDir_6;
					  mediump vec3 eyeNormal_7;
					  mediump vec4 color_8;
					  color_8 = vec4(0.0, 0.0, 0.0, 1.1);
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = tmpvar_1;
					  highp mat3 tmpvar_10;
					  tmpvar_10[0] = glstate_matrix_invtrans_modelview0[0].xyz;
					  tmpvar_10[1] = glstate_matrix_invtrans_modelview0[1].xyz;
					  tmpvar_10[2] = glstate_matrix_invtrans_modelview0[2].xyz;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = normalize((tmpvar_10 * _glesNormal));
					  eyeNormal_7 = tmpvar_11;
					  highp vec3 tmpvar_12;
					  tmpvar_12 = normalize((glstate_matrix_modelview0 * tmpvar_9).xyz);
					  viewDir_6 = -(tmpvar_12);
					  lcolor_5 = (_Emission.xyz + (_Color.xyz * glstate_lightmodel_ambient.xyz));
					  specColor_4 = vec3(0.0, 0.0, 0.0);
					  shininess_3 = (_Shininess * 128.0);
					  for (highp int il_2 = 0; il_2 < 8; il_2++) {
					    highp vec3 tmpvar_13;
					    tmpvar_13 = unity_LightPosition[il_2].xyz;
					    mediump vec3 dirToLight_14;
					    dirToLight_14 = tmpvar_13;
					    mediump vec3 specColor_15;
					    specColor_15 = specColor_4;
					    mediump float tmpvar_16;
					    tmpvar_16 = max (dot (eyeNormal_7, dirToLight_14), 0.0);
					    mediump vec3 tmpvar_17;
					    tmpvar_17 = ((tmpvar_16 * _Color.xyz) * unity_LightColor[il_2].xyz);
					    if ((tmpvar_16 > 0.0)) {
					      specColor_15 = (specColor_4 + ((0.5 * 
					        clamp (pow (max (dot (eyeNormal_7, 
					          normalize((dirToLight_14 + viewDir_6))
					        ), 0.0), shininess_3), 0.0, 1.0)
					      ) * unity_LightColor[il_2].xyz));
					    };
					    specColor_4 = specColor_15;
					    lcolor_5 = (lcolor_5 + min ((tmpvar_17 * 0.5), vec3(1.0, 1.0, 1.0)));
					  };
					  color_8.xyz = lcolor_5;
					  color_8.w = _Color.w;
					  specColor_4 = (specColor_4 * _SpecColor.xyz);
					  lowp vec4 tmpvar_18;
					  mediump vec4 tmpvar_19;
					  tmpvar_19 = clamp (color_8, 0.0, 1.0);
					  tmpvar_18 = tmpvar_19;
					  lowp vec3 tmpvar_20;
					  mediump vec3 tmpvar_21;
					  tmpvar_21 = clamp (specColor_4, 0.0, 1.0);
					  tmpvar_20 = tmpvar_21;
					  highp vec4 tmpvar_22;
					  tmpvar_22.w = 1.0;
					  tmpvar_22.xyz = tmpvar_1;
					  xlv_COLOR0 = tmpvar_18;
					  xlv_COLOR1 = tmpvar_20;
					  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  gl_Position = (glstate_matrix_mvp * tmpvar_22);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					varying lowp vec4 xlv_COLOR0;
					varying lowp vec3 xlv_COLOR1;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 col_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0);
					  col_1.xyz = (tmpvar_2 * xlv_COLOR0).xyz;
					  col_1.xyz = (col_1 * 2.0).xyz;
					  col_1.w = (tmpvar_2.w * xlv_COLOR0.w);
					  col_1.xyz = (col_1.xyz + xlv_COLOR1);
					  gl_FragData[0] = col_1;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					uniform mediump vec4 unity_LightColor[8];
					uniform highp vec4 unity_LightPosition[8];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 glstate_matrix_modelview0;
					uniform highp mat4 glstate_matrix_invtrans_modelview0;
					uniform lowp vec4 glstate_lightmodel_ambient;
					uniform mediump vec4 _Color;
					uniform mediump vec4 _SpecColor;
					uniform mediump vec4 _Emission;
					uniform mediump float _Shininess;
					uniform highp vec4 _MainTex_ST;
					varying lowp vec4 xlv_COLOR0;
					varying lowp vec3 xlv_COLOR1;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  highp vec3 tmpvar_1;
					  tmpvar_1 = _glesVertex.xyz;
					  mediump float shininess_3;
					  mediump vec3 specColor_4;
					  mediump vec3 lcolor_5;
					  mediump vec3 viewDir_6;
					  mediump vec3 eyeNormal_7;
					  mediump vec4 color_8;
					  color_8 = vec4(0.0, 0.0, 0.0, 1.1);
					  highp vec4 tmpvar_9;
					  tmpvar_9.w = 1.0;
					  tmpvar_9.xyz = tmpvar_1;
					  highp mat3 tmpvar_10;
					  tmpvar_10[0] = glstate_matrix_invtrans_modelview0[0].xyz;
					  tmpvar_10[1] = glstate_matrix_invtrans_modelview0[1].xyz;
					  tmpvar_10[2] = glstate_matrix_invtrans_modelview0[2].xyz;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = normalize((tmpvar_10 * _glesNormal));
					  eyeNormal_7 = tmpvar_11;
					  highp vec3 tmpvar_12;
					  tmpvar_12 = normalize((glstate_matrix_modelview0 * tmpvar_9).xyz);
					  viewDir_6 = -(tmpvar_12);
					  lcolor_5 = (_Emission.xyz + (_Color.xyz * glstate_lightmodel_ambient.xyz));
					  specColor_4 = vec3(0.0, 0.0, 0.0);
					  shininess_3 = (_Shininess * 128.0);
					  for (highp int il_2 = 0; il_2 < 8; il_2++) {
					    highp vec3 tmpvar_13;
					    tmpvar_13 = unity_LightPosition[il_2].xyz;
					    mediump vec3 dirToLight_14;
					    dirToLight_14 = tmpvar_13;
					    mediump vec3 specColor_15;
					    specColor_15 = specColor_4;
					    mediump float tmpvar_16;
					    tmpvar_16 = max (dot (eyeNormal_7, dirToLight_14), 0.0);
					    mediump vec3 tmpvar_17;
					    tmpvar_17 = ((tmpvar_16 * _Color.xyz) * unity_LightColor[il_2].xyz);
					    if ((tmpvar_16 > 0.0)) {
					      specColor_15 = (specColor_4 + ((0.5 * 
					        clamp (pow (max (dot (eyeNormal_7, 
					          normalize((dirToLight_14 + viewDir_6))
					        ), 0.0), shininess_3), 0.0, 1.0)
					      ) * unity_LightColor[il_2].xyz));
					    };
					    specColor_4 = specColor_15;
					    lcolor_5 = (lcolor_5 + min ((tmpvar_17 * 0.5), vec3(1.0, 1.0, 1.0)));
					  };
					  color_8.xyz = lcolor_5;
					  color_8.w = _Color.w;
					  specColor_4 = (specColor_4 * _SpecColor.xyz);
					  lowp vec4 tmpvar_18;
					  mediump vec4 tmpvar_19;
					  tmpvar_19 = clamp (color_8, 0.0, 1.0);
					  tmpvar_18 = tmpvar_19;
					  lowp vec3 tmpvar_20;
					  mediump vec3 tmpvar_21;
					  tmpvar_21 = clamp (specColor_4, 0.0, 1.0);
					  tmpvar_20 = tmpvar_21;
					  highp vec4 tmpvar_22;
					  tmpvar_22.w = 1.0;
					  tmpvar_22.xyz = tmpvar_1;
					  xlv_COLOR0 = tmpvar_18;
					  xlv_COLOR1 = tmpvar_20;
					  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  gl_Position = (glstate_matrix_mvp * tmpvar_22);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					varying lowp vec4 xlv_COLOR0;
					varying lowp vec3 xlv_COLOR1;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 col_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0);
					  col_1.xyz = (tmpvar_2 * xlv_COLOR0).xyz;
					  col_1.xyz = (col_1 * 2.0).xyz;
					  col_1.w = (tmpvar_2.w * xlv_COLOR0.w);
					  col_1.xyz = (col_1.xyz + xlv_COLOR1);
					  gl_FragData[0] = col_1;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "POINT" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					uniform mediump vec4 unity_LightColor[8];
					uniform highp vec4 unity_LightPosition[8];
					uniform mediump vec4 unity_LightAtten[8];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 glstate_matrix_modelview0;
					uniform highp mat4 glstate_matrix_invtrans_modelview0;
					uniform lowp vec4 glstate_lightmodel_ambient;
					uniform mediump vec4 _Color;
					uniform mediump vec4 _SpecColor;
					uniform mediump vec4 _Emission;
					uniform mediump float _Shininess;
					uniform highp vec4 _MainTex_ST;
					varying lowp vec4 xlv_COLOR0;
					varying lowp vec3 xlv_COLOR1;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  highp vec3 tmpvar_1;
					  tmpvar_1 = _glesVertex.xyz;
					  mediump float shininess_3;
					  mediump vec3 specColor_4;
					  mediump vec3 lcolor_5;
					  mediump vec3 viewDir_6;
					  mediump vec3 eyeNormal_7;
					  highp vec3 eyePos_8;
					  mediump vec4 color_9;
					  color_9 = vec4(0.0, 0.0, 0.0, 1.1);
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = tmpvar_1;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = (glstate_matrix_modelview0 * tmpvar_10).xyz;
					  eyePos_8 = tmpvar_11;
					  highp mat3 tmpvar_12;
					  tmpvar_12[0] = glstate_matrix_invtrans_modelview0[0].xyz;
					  tmpvar_12[1] = glstate_matrix_invtrans_modelview0[1].xyz;
					  tmpvar_12[2] = glstate_matrix_invtrans_modelview0[2].xyz;
					  highp vec3 tmpvar_13;
					  tmpvar_13 = normalize((tmpvar_12 * _glesNormal));
					  eyeNormal_7 = tmpvar_13;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize(tmpvar_11);
					  viewDir_6 = -(tmpvar_14);
					  lcolor_5 = (_Emission.xyz + (_Color.xyz * glstate_lightmodel_ambient.xyz));
					  specColor_4 = vec3(0.0, 0.0, 0.0);
					  shininess_3 = (_Shininess * 128.0);
					  for (highp int il_2 = 0; il_2 < 8; il_2++) {
					    mediump float att_15;
					    highp vec3 dirToLight_16;
					    dirToLight_16 = (unity_LightPosition[il_2].xyz - (eyePos_8 * unity_LightPosition[il_2].w));
					    highp float tmpvar_17;
					    tmpvar_17 = dot (dirToLight_16, dirToLight_16);
					    att_15 = (1.0/((1.0 + (unity_LightAtten[il_2].z * tmpvar_17))));
					    if (((unity_LightPosition[il_2].w != 0.0) && (tmpvar_17 > unity_LightAtten[il_2].w))) {
					      att_15 = 0.0;
					    };
					    dirToLight_16 = (dirToLight_16 * inversesqrt(tmpvar_17));
					    att_15 = (att_15 * 0.5);
					    mediump vec3 dirToLight_18;
					    dirToLight_18 = dirToLight_16;
					    mediump vec3 specColor_19;
					    specColor_19 = specColor_4;
					    mediump float tmpvar_20;
					    tmpvar_20 = max (dot (eyeNormal_7, dirToLight_18), 0.0);
					    mediump vec3 tmpvar_21;
					    tmpvar_21 = ((tmpvar_20 * _Color.xyz) * unity_LightColor[il_2].xyz);
					    if ((tmpvar_20 > 0.0)) {
					      specColor_19 = (specColor_4 + ((att_15 * 
					        clamp (pow (max (dot (eyeNormal_7, 
					          normalize((dirToLight_18 + viewDir_6))
					        ), 0.0), shininess_3), 0.0, 1.0)
					      ) * unity_LightColor[il_2].xyz));
					    };
					    specColor_4 = specColor_19;
					    lcolor_5 = (lcolor_5 + min ((tmpvar_21 * att_15), vec3(1.0, 1.0, 1.0)));
					  };
					  color_9.xyz = lcolor_5;
					  color_9.w = _Color.w;
					  specColor_4 = (specColor_4 * _SpecColor.xyz);
					  lowp vec4 tmpvar_22;
					  mediump vec4 tmpvar_23;
					  tmpvar_23 = clamp (color_9, 0.0, 1.0);
					  tmpvar_22 = tmpvar_23;
					  lowp vec3 tmpvar_24;
					  mediump vec3 tmpvar_25;
					  tmpvar_25 = clamp (specColor_4, 0.0, 1.0);
					  tmpvar_24 = tmpvar_25;
					  highp vec4 tmpvar_26;
					  tmpvar_26.w = 1.0;
					  tmpvar_26.xyz = tmpvar_1;
					  xlv_COLOR0 = tmpvar_22;
					  xlv_COLOR1 = tmpvar_24;
					  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  gl_Position = (glstate_matrix_mvp * tmpvar_26);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					varying lowp vec4 xlv_COLOR0;
					varying lowp vec3 xlv_COLOR1;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 col_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0);
					  col_1.xyz = (tmpvar_2 * xlv_COLOR0).xyz;
					  col_1.xyz = (col_1 * 2.0).xyz;
					  col_1.w = (tmpvar_2.w * xlv_COLOR0.w);
					  col_1.xyz = (col_1.xyz + xlv_COLOR1);
					  gl_FragData[0] = col_1;
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
					uniform mediump vec4 unity_LightColor[8];
					uniform highp vec4 unity_LightPosition[8];
					uniform mediump vec4 unity_LightAtten[8];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 glstate_matrix_modelview0;
					uniform highp mat4 glstate_matrix_invtrans_modelview0;
					uniform lowp vec4 glstate_lightmodel_ambient;
					uniform mediump vec4 _Color;
					uniform mediump vec4 _SpecColor;
					uniform mediump vec4 _Emission;
					uniform mediump float _Shininess;
					uniform highp vec4 _MainTex_ST;
					varying lowp vec4 xlv_COLOR0;
					varying lowp vec3 xlv_COLOR1;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  highp vec3 tmpvar_1;
					  tmpvar_1 = _glesVertex.xyz;
					  mediump float shininess_3;
					  mediump vec3 specColor_4;
					  mediump vec3 lcolor_5;
					  mediump vec3 viewDir_6;
					  mediump vec3 eyeNormal_7;
					  highp vec3 eyePos_8;
					  mediump vec4 color_9;
					  color_9 = vec4(0.0, 0.0, 0.0, 1.1);
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = tmpvar_1;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = (glstate_matrix_modelview0 * tmpvar_10).xyz;
					  eyePos_8 = tmpvar_11;
					  highp mat3 tmpvar_12;
					  tmpvar_12[0] = glstate_matrix_invtrans_modelview0[0].xyz;
					  tmpvar_12[1] = glstate_matrix_invtrans_modelview0[1].xyz;
					  tmpvar_12[2] = glstate_matrix_invtrans_modelview0[2].xyz;
					  highp vec3 tmpvar_13;
					  tmpvar_13 = normalize((tmpvar_12 * _glesNormal));
					  eyeNormal_7 = tmpvar_13;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize(tmpvar_11);
					  viewDir_6 = -(tmpvar_14);
					  lcolor_5 = (_Emission.xyz + (_Color.xyz * glstate_lightmodel_ambient.xyz));
					  specColor_4 = vec3(0.0, 0.0, 0.0);
					  shininess_3 = (_Shininess * 128.0);
					  for (highp int il_2 = 0; il_2 < 8; il_2++) {
					    mediump float att_15;
					    highp vec3 dirToLight_16;
					    dirToLight_16 = (unity_LightPosition[il_2].xyz - (eyePos_8 * unity_LightPosition[il_2].w));
					    highp float tmpvar_17;
					    tmpvar_17 = dot (dirToLight_16, dirToLight_16);
					    att_15 = (1.0/((1.0 + (unity_LightAtten[il_2].z * tmpvar_17))));
					    if (((unity_LightPosition[il_2].w != 0.0) && (tmpvar_17 > unity_LightAtten[il_2].w))) {
					      att_15 = 0.0;
					    };
					    dirToLight_16 = (dirToLight_16 * inversesqrt(tmpvar_17));
					    att_15 = (att_15 * 0.5);
					    mediump vec3 dirToLight_18;
					    dirToLight_18 = dirToLight_16;
					    mediump vec3 specColor_19;
					    specColor_19 = specColor_4;
					    mediump float tmpvar_20;
					    tmpvar_20 = max (dot (eyeNormal_7, dirToLight_18), 0.0);
					    mediump vec3 tmpvar_21;
					    tmpvar_21 = ((tmpvar_20 * _Color.xyz) * unity_LightColor[il_2].xyz);
					    if ((tmpvar_20 > 0.0)) {
					      specColor_19 = (specColor_4 + ((att_15 * 
					        clamp (pow (max (dot (eyeNormal_7, 
					          normalize((dirToLight_18 + viewDir_6))
					        ), 0.0), shininess_3), 0.0, 1.0)
					      ) * unity_LightColor[il_2].xyz));
					    };
					    specColor_4 = specColor_19;
					    lcolor_5 = (lcolor_5 + min ((tmpvar_21 * att_15), vec3(1.0, 1.0, 1.0)));
					  };
					  color_9.xyz = lcolor_5;
					  color_9.w = _Color.w;
					  specColor_4 = (specColor_4 * _SpecColor.xyz);
					  lowp vec4 tmpvar_22;
					  mediump vec4 tmpvar_23;
					  tmpvar_23 = clamp (color_9, 0.0, 1.0);
					  tmpvar_22 = tmpvar_23;
					  lowp vec3 tmpvar_24;
					  mediump vec3 tmpvar_25;
					  tmpvar_25 = clamp (specColor_4, 0.0, 1.0);
					  tmpvar_24 = tmpvar_25;
					  highp vec4 tmpvar_26;
					  tmpvar_26.w = 1.0;
					  tmpvar_26.xyz = tmpvar_1;
					  xlv_COLOR0 = tmpvar_22;
					  xlv_COLOR1 = tmpvar_24;
					  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  gl_Position = (glstate_matrix_mvp * tmpvar_26);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					varying lowp vec4 xlv_COLOR0;
					varying lowp vec3 xlv_COLOR1;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 col_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0);
					  col_1.xyz = (tmpvar_2 * xlv_COLOR0).xyz;
					  col_1.xyz = (col_1 * 2.0).xyz;
					  col_1.w = (tmpvar_2.w * xlv_COLOR0.w);
					  col_1.xyz = (col_1.xyz + xlv_COLOR1);
					  gl_FragData[0] = col_1;
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
					uniform mediump vec4 unity_LightColor[8];
					uniform highp vec4 unity_LightPosition[8];
					uniform mediump vec4 unity_LightAtten[8];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 glstate_matrix_modelview0;
					uniform highp mat4 glstate_matrix_invtrans_modelview0;
					uniform lowp vec4 glstate_lightmodel_ambient;
					uniform mediump vec4 _Color;
					uniform mediump vec4 _SpecColor;
					uniform mediump vec4 _Emission;
					uniform mediump float _Shininess;
					uniform highp vec4 _MainTex_ST;
					varying lowp vec4 xlv_COLOR0;
					varying lowp vec3 xlv_COLOR1;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  highp vec3 tmpvar_1;
					  tmpvar_1 = _glesVertex.xyz;
					  mediump float shininess_3;
					  mediump vec3 specColor_4;
					  mediump vec3 lcolor_5;
					  mediump vec3 viewDir_6;
					  mediump vec3 eyeNormal_7;
					  highp vec3 eyePos_8;
					  mediump vec4 color_9;
					  color_9 = vec4(0.0, 0.0, 0.0, 1.1);
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = tmpvar_1;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = (glstate_matrix_modelview0 * tmpvar_10).xyz;
					  eyePos_8 = tmpvar_11;
					  highp mat3 tmpvar_12;
					  tmpvar_12[0] = glstate_matrix_invtrans_modelview0[0].xyz;
					  tmpvar_12[1] = glstate_matrix_invtrans_modelview0[1].xyz;
					  tmpvar_12[2] = glstate_matrix_invtrans_modelview0[2].xyz;
					  highp vec3 tmpvar_13;
					  tmpvar_13 = normalize((tmpvar_12 * _glesNormal));
					  eyeNormal_7 = tmpvar_13;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize(tmpvar_11);
					  viewDir_6 = -(tmpvar_14);
					  lcolor_5 = (_Emission.xyz + (_Color.xyz * glstate_lightmodel_ambient.xyz));
					  specColor_4 = vec3(0.0, 0.0, 0.0);
					  shininess_3 = (_Shininess * 128.0);
					  for (highp int il_2 = 0; il_2 < 8; il_2++) {
					    mediump float att_15;
					    highp vec3 dirToLight_16;
					    dirToLight_16 = (unity_LightPosition[il_2].xyz - (eyePos_8 * unity_LightPosition[il_2].w));
					    highp float tmpvar_17;
					    tmpvar_17 = dot (dirToLight_16, dirToLight_16);
					    att_15 = (1.0/((1.0 + (unity_LightAtten[il_2].z * tmpvar_17))));
					    if (((unity_LightPosition[il_2].w != 0.0) && (tmpvar_17 > unity_LightAtten[il_2].w))) {
					      att_15 = 0.0;
					    };
					    dirToLight_16 = (dirToLight_16 * inversesqrt(tmpvar_17));
					    att_15 = (att_15 * 0.5);
					    mediump vec3 dirToLight_18;
					    dirToLight_18 = dirToLight_16;
					    mediump vec3 specColor_19;
					    specColor_19 = specColor_4;
					    mediump float tmpvar_20;
					    tmpvar_20 = max (dot (eyeNormal_7, dirToLight_18), 0.0);
					    mediump vec3 tmpvar_21;
					    tmpvar_21 = ((tmpvar_20 * _Color.xyz) * unity_LightColor[il_2].xyz);
					    if ((tmpvar_20 > 0.0)) {
					      specColor_19 = (specColor_4 + ((att_15 * 
					        clamp (pow (max (dot (eyeNormal_7, 
					          normalize((dirToLight_18 + viewDir_6))
					        ), 0.0), shininess_3), 0.0, 1.0)
					      ) * unity_LightColor[il_2].xyz));
					    };
					    specColor_4 = specColor_19;
					    lcolor_5 = (lcolor_5 + min ((tmpvar_21 * att_15), vec3(1.0, 1.0, 1.0)));
					  };
					  color_9.xyz = lcolor_5;
					  color_9.w = _Color.w;
					  specColor_4 = (specColor_4 * _SpecColor.xyz);
					  lowp vec4 tmpvar_22;
					  mediump vec4 tmpvar_23;
					  tmpvar_23 = clamp (color_9, 0.0, 1.0);
					  tmpvar_22 = tmpvar_23;
					  lowp vec3 tmpvar_24;
					  mediump vec3 tmpvar_25;
					  tmpvar_25 = clamp (specColor_4, 0.0, 1.0);
					  tmpvar_24 = tmpvar_25;
					  highp vec4 tmpvar_26;
					  tmpvar_26.w = 1.0;
					  tmpvar_26.xyz = tmpvar_1;
					  xlv_COLOR0 = tmpvar_22;
					  xlv_COLOR1 = tmpvar_24;
					  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  gl_Position = (glstate_matrix_mvp * tmpvar_26);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					varying lowp vec4 xlv_COLOR0;
					varying lowp vec3 xlv_COLOR1;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 col_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0);
					  col_1.xyz = (tmpvar_2 * xlv_COLOR0).xyz;
					  col_1.xyz = (col_1 * 2.0).xyz;
					  col_1.w = (tmpvar_2.w * xlv_COLOR0.w);
					  col_1.xyz = (col_1.xyz + xlv_COLOR1);
					  gl_FragData[0] = col_1;
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
					uniform mediump vec4 unity_LightColor[8];
					uniform highp vec4 unity_LightPosition[8];
					uniform mediump vec4 unity_LightAtten[8];
					uniform highp vec4 unity_SpotDirection[8];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 glstate_matrix_modelview0;
					uniform highp mat4 glstate_matrix_invtrans_modelview0;
					uniform lowp vec4 glstate_lightmodel_ambient;
					uniform mediump vec4 _Color;
					uniform mediump vec4 _SpecColor;
					uniform mediump vec4 _Emission;
					uniform mediump float _Shininess;
					uniform highp vec4 _MainTex_ST;
					varying lowp vec4 xlv_COLOR0;
					varying lowp vec3 xlv_COLOR1;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  highp vec3 tmpvar_1;
					  tmpvar_1 = _glesVertex.xyz;
					  mediump float shininess_3;
					  mediump vec3 specColor_4;
					  mediump vec3 lcolor_5;
					  mediump vec3 viewDir_6;
					  mediump vec3 eyeNormal_7;
					  highp vec3 eyePos_8;
					  mediump vec4 color_9;
					  color_9 = vec4(0.0, 0.0, 0.0, 1.1);
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = tmpvar_1;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = (glstate_matrix_modelview0 * tmpvar_10).xyz;
					  eyePos_8 = tmpvar_11;
					  highp mat3 tmpvar_12;
					  tmpvar_12[0] = glstate_matrix_invtrans_modelview0[0].xyz;
					  tmpvar_12[1] = glstate_matrix_invtrans_modelview0[1].xyz;
					  tmpvar_12[2] = glstate_matrix_invtrans_modelview0[2].xyz;
					  highp vec3 tmpvar_13;
					  tmpvar_13 = normalize((tmpvar_12 * _glesNormal));
					  eyeNormal_7 = tmpvar_13;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize(tmpvar_11);
					  viewDir_6 = -(tmpvar_14);
					  lcolor_5 = (_Emission.xyz + (_Color.xyz * glstate_lightmodel_ambient.xyz));
					  specColor_4 = vec3(0.0, 0.0, 0.0);
					  shininess_3 = (_Shininess * 128.0);
					  for (highp int il_2 = 0; il_2 < 8; il_2++) {
					    mediump float rho_15;
					    mediump float att_16;
					    highp vec3 dirToLight_17;
					    dirToLight_17 = (unity_LightPosition[il_2].xyz - (eyePos_8 * unity_LightPosition[il_2].w));
					    highp float tmpvar_18;
					    tmpvar_18 = dot (dirToLight_17, dirToLight_17);
					    att_16 = (1.0/((1.0 + (unity_LightAtten[il_2].z * tmpvar_18))));
					    if (((unity_LightPosition[il_2].w != 0.0) && (tmpvar_18 > unity_LightAtten[il_2].w))) {
					      att_16 = 0.0;
					    };
					    dirToLight_17 = (dirToLight_17 * inversesqrt(tmpvar_18));
					    highp float tmpvar_19;
					    tmpvar_19 = max (dot (dirToLight_17, unity_SpotDirection[il_2].xyz), 0.0);
					    rho_15 = tmpvar_19;
					    att_16 = (att_16 * clamp ((
					      (rho_15 - unity_LightAtten[il_2].x)
					     * unity_LightAtten[il_2].y), 0.0, 1.0));
					    att_16 = (att_16 * 0.5);
					    mediump vec3 dirToLight_20;
					    dirToLight_20 = dirToLight_17;
					    mediump vec3 specColor_21;
					    specColor_21 = specColor_4;
					    mediump float tmpvar_22;
					    tmpvar_22 = max (dot (eyeNormal_7, dirToLight_20), 0.0);
					    mediump vec3 tmpvar_23;
					    tmpvar_23 = ((tmpvar_22 * _Color.xyz) * unity_LightColor[il_2].xyz);
					    if ((tmpvar_22 > 0.0)) {
					      specColor_21 = (specColor_4 + ((att_16 * 
					        clamp (pow (max (dot (eyeNormal_7, 
					          normalize((dirToLight_20 + viewDir_6))
					        ), 0.0), shininess_3), 0.0, 1.0)
					      ) * unity_LightColor[il_2].xyz));
					    };
					    specColor_4 = specColor_21;
					    lcolor_5 = (lcolor_5 + min ((tmpvar_23 * att_16), vec3(1.0, 1.0, 1.0)));
					  };
					  color_9.xyz = lcolor_5;
					  color_9.w = _Color.w;
					  specColor_4 = (specColor_4 * _SpecColor.xyz);
					  lowp vec4 tmpvar_24;
					  mediump vec4 tmpvar_25;
					  tmpvar_25 = clamp (color_9, 0.0, 1.0);
					  tmpvar_24 = tmpvar_25;
					  lowp vec3 tmpvar_26;
					  mediump vec3 tmpvar_27;
					  tmpvar_27 = clamp (specColor_4, 0.0, 1.0);
					  tmpvar_26 = tmpvar_27;
					  highp vec4 tmpvar_28;
					  tmpvar_28.w = 1.0;
					  tmpvar_28.xyz = tmpvar_1;
					  xlv_COLOR0 = tmpvar_24;
					  xlv_COLOR1 = tmpvar_26;
					  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  gl_Position = (glstate_matrix_mvp * tmpvar_28);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					varying lowp vec4 xlv_COLOR0;
					varying lowp vec3 xlv_COLOR1;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 col_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0);
					  col_1.xyz = (tmpvar_2 * xlv_COLOR0).xyz;
					  col_1.xyz = (col_1 * 2.0).xyz;
					  col_1.w = (tmpvar_2.w * xlv_COLOR0.w);
					  col_1.xyz = (col_1.xyz + xlv_COLOR1);
					  gl_FragData[0] = col_1;
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
					uniform mediump vec4 unity_LightColor[8];
					uniform highp vec4 unity_LightPosition[8];
					uniform mediump vec4 unity_LightAtten[8];
					uniform highp vec4 unity_SpotDirection[8];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 glstate_matrix_modelview0;
					uniform highp mat4 glstate_matrix_invtrans_modelview0;
					uniform lowp vec4 glstate_lightmodel_ambient;
					uniform mediump vec4 _Color;
					uniform mediump vec4 _SpecColor;
					uniform mediump vec4 _Emission;
					uniform mediump float _Shininess;
					uniform highp vec4 _MainTex_ST;
					varying lowp vec4 xlv_COLOR0;
					varying lowp vec3 xlv_COLOR1;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  highp vec3 tmpvar_1;
					  tmpvar_1 = _glesVertex.xyz;
					  mediump float shininess_3;
					  mediump vec3 specColor_4;
					  mediump vec3 lcolor_5;
					  mediump vec3 viewDir_6;
					  mediump vec3 eyeNormal_7;
					  highp vec3 eyePos_8;
					  mediump vec4 color_9;
					  color_9 = vec4(0.0, 0.0, 0.0, 1.1);
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = tmpvar_1;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = (glstate_matrix_modelview0 * tmpvar_10).xyz;
					  eyePos_8 = tmpvar_11;
					  highp mat3 tmpvar_12;
					  tmpvar_12[0] = glstate_matrix_invtrans_modelview0[0].xyz;
					  tmpvar_12[1] = glstate_matrix_invtrans_modelview0[1].xyz;
					  tmpvar_12[2] = glstate_matrix_invtrans_modelview0[2].xyz;
					  highp vec3 tmpvar_13;
					  tmpvar_13 = normalize((tmpvar_12 * _glesNormal));
					  eyeNormal_7 = tmpvar_13;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize(tmpvar_11);
					  viewDir_6 = -(tmpvar_14);
					  lcolor_5 = (_Emission.xyz + (_Color.xyz * glstate_lightmodel_ambient.xyz));
					  specColor_4 = vec3(0.0, 0.0, 0.0);
					  shininess_3 = (_Shininess * 128.0);
					  for (highp int il_2 = 0; il_2 < 8; il_2++) {
					    mediump float rho_15;
					    mediump float att_16;
					    highp vec3 dirToLight_17;
					    dirToLight_17 = (unity_LightPosition[il_2].xyz - (eyePos_8 * unity_LightPosition[il_2].w));
					    highp float tmpvar_18;
					    tmpvar_18 = dot (dirToLight_17, dirToLight_17);
					    att_16 = (1.0/((1.0 + (unity_LightAtten[il_2].z * tmpvar_18))));
					    if (((unity_LightPosition[il_2].w != 0.0) && (tmpvar_18 > unity_LightAtten[il_2].w))) {
					      att_16 = 0.0;
					    };
					    dirToLight_17 = (dirToLight_17 * inversesqrt(tmpvar_18));
					    highp float tmpvar_19;
					    tmpvar_19 = max (dot (dirToLight_17, unity_SpotDirection[il_2].xyz), 0.0);
					    rho_15 = tmpvar_19;
					    att_16 = (att_16 * clamp ((
					      (rho_15 - unity_LightAtten[il_2].x)
					     * unity_LightAtten[il_2].y), 0.0, 1.0));
					    att_16 = (att_16 * 0.5);
					    mediump vec3 dirToLight_20;
					    dirToLight_20 = dirToLight_17;
					    mediump vec3 specColor_21;
					    specColor_21 = specColor_4;
					    mediump float tmpvar_22;
					    tmpvar_22 = max (dot (eyeNormal_7, dirToLight_20), 0.0);
					    mediump vec3 tmpvar_23;
					    tmpvar_23 = ((tmpvar_22 * _Color.xyz) * unity_LightColor[il_2].xyz);
					    if ((tmpvar_22 > 0.0)) {
					      specColor_21 = (specColor_4 + ((att_16 * 
					        clamp (pow (max (dot (eyeNormal_7, 
					          normalize((dirToLight_20 + viewDir_6))
					        ), 0.0), shininess_3), 0.0, 1.0)
					      ) * unity_LightColor[il_2].xyz));
					    };
					    specColor_4 = specColor_21;
					    lcolor_5 = (lcolor_5 + min ((tmpvar_23 * att_16), vec3(1.0, 1.0, 1.0)));
					  };
					  color_9.xyz = lcolor_5;
					  color_9.w = _Color.w;
					  specColor_4 = (specColor_4 * _SpecColor.xyz);
					  lowp vec4 tmpvar_24;
					  mediump vec4 tmpvar_25;
					  tmpvar_25 = clamp (color_9, 0.0, 1.0);
					  tmpvar_24 = tmpvar_25;
					  lowp vec3 tmpvar_26;
					  mediump vec3 tmpvar_27;
					  tmpvar_27 = clamp (specColor_4, 0.0, 1.0);
					  tmpvar_26 = tmpvar_27;
					  highp vec4 tmpvar_28;
					  tmpvar_28.w = 1.0;
					  tmpvar_28.xyz = tmpvar_1;
					  xlv_COLOR0 = tmpvar_24;
					  xlv_COLOR1 = tmpvar_26;
					  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  gl_Position = (glstate_matrix_mvp * tmpvar_28);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					varying lowp vec4 xlv_COLOR0;
					varying lowp vec3 xlv_COLOR1;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 col_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0);
					  col_1.xyz = (tmpvar_2 * xlv_COLOR0).xyz;
					  col_1.xyz = (col_1 * 2.0).xyz;
					  col_1.w = (tmpvar_2.w * xlv_COLOR0.w);
					  col_1.xyz = (col_1.xyz + xlv_COLOR1);
					  gl_FragData[0] = col_1;
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
					uniform mediump vec4 unity_LightColor[8];
					uniform highp vec4 unity_LightPosition[8];
					uniform mediump vec4 unity_LightAtten[8];
					uniform highp vec4 unity_SpotDirection[8];
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 glstate_matrix_modelview0;
					uniform highp mat4 glstate_matrix_invtrans_modelview0;
					uniform lowp vec4 glstate_lightmodel_ambient;
					uniform mediump vec4 _Color;
					uniform mediump vec4 _SpecColor;
					uniform mediump vec4 _Emission;
					uniform mediump float _Shininess;
					uniform highp vec4 _MainTex_ST;
					varying lowp vec4 xlv_COLOR0;
					varying lowp vec3 xlv_COLOR1;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  highp vec3 tmpvar_1;
					  tmpvar_1 = _glesVertex.xyz;
					  mediump float shininess_3;
					  mediump vec3 specColor_4;
					  mediump vec3 lcolor_5;
					  mediump vec3 viewDir_6;
					  mediump vec3 eyeNormal_7;
					  highp vec3 eyePos_8;
					  mediump vec4 color_9;
					  color_9 = vec4(0.0, 0.0, 0.0, 1.1);
					  highp vec4 tmpvar_10;
					  tmpvar_10.w = 1.0;
					  tmpvar_10.xyz = tmpvar_1;
					  highp vec3 tmpvar_11;
					  tmpvar_11 = (glstate_matrix_modelview0 * tmpvar_10).xyz;
					  eyePos_8 = tmpvar_11;
					  highp mat3 tmpvar_12;
					  tmpvar_12[0] = glstate_matrix_invtrans_modelview0[0].xyz;
					  tmpvar_12[1] = glstate_matrix_invtrans_modelview0[1].xyz;
					  tmpvar_12[2] = glstate_matrix_invtrans_modelview0[2].xyz;
					  highp vec3 tmpvar_13;
					  tmpvar_13 = normalize((tmpvar_12 * _glesNormal));
					  eyeNormal_7 = tmpvar_13;
					  highp vec3 tmpvar_14;
					  tmpvar_14 = normalize(tmpvar_11);
					  viewDir_6 = -(tmpvar_14);
					  lcolor_5 = (_Emission.xyz + (_Color.xyz * glstate_lightmodel_ambient.xyz));
					  specColor_4 = vec3(0.0, 0.0, 0.0);
					  shininess_3 = (_Shininess * 128.0);
					  for (highp int il_2 = 0; il_2 < 8; il_2++) {
					    mediump float rho_15;
					    mediump float att_16;
					    highp vec3 dirToLight_17;
					    dirToLight_17 = (unity_LightPosition[il_2].xyz - (eyePos_8 * unity_LightPosition[il_2].w));
					    highp float tmpvar_18;
					    tmpvar_18 = dot (dirToLight_17, dirToLight_17);
					    att_16 = (1.0/((1.0 + (unity_LightAtten[il_2].z * tmpvar_18))));
					    if (((unity_LightPosition[il_2].w != 0.0) && (tmpvar_18 > unity_LightAtten[il_2].w))) {
					      att_16 = 0.0;
					    };
					    dirToLight_17 = (dirToLight_17 * inversesqrt(tmpvar_18));
					    highp float tmpvar_19;
					    tmpvar_19 = max (dot (dirToLight_17, unity_SpotDirection[il_2].xyz), 0.0);
					    rho_15 = tmpvar_19;
					    att_16 = (att_16 * clamp ((
					      (rho_15 - unity_LightAtten[il_2].x)
					     * unity_LightAtten[il_2].y), 0.0, 1.0));
					    att_16 = (att_16 * 0.5);
					    mediump vec3 dirToLight_20;
					    dirToLight_20 = dirToLight_17;
					    mediump vec3 specColor_21;
					    specColor_21 = specColor_4;
					    mediump float tmpvar_22;
					    tmpvar_22 = max (dot (eyeNormal_7, dirToLight_20), 0.0);
					    mediump vec3 tmpvar_23;
					    tmpvar_23 = ((tmpvar_22 * _Color.xyz) * unity_LightColor[il_2].xyz);
					    if ((tmpvar_22 > 0.0)) {
					      specColor_21 = (specColor_4 + ((att_16 * 
					        clamp (pow (max (dot (eyeNormal_7, 
					          normalize((dirToLight_20 + viewDir_6))
					        ), 0.0), shininess_3), 0.0, 1.0)
					      ) * unity_LightColor[il_2].xyz));
					    };
					    specColor_4 = specColor_21;
					    lcolor_5 = (lcolor_5 + min ((tmpvar_23 * att_16), vec3(1.0, 1.0, 1.0)));
					  };
					  color_9.xyz = lcolor_5;
					  color_9.w = _Color.w;
					  specColor_4 = (specColor_4 * _SpecColor.xyz);
					  lowp vec4 tmpvar_24;
					  mediump vec4 tmpvar_25;
					  tmpvar_25 = clamp (color_9, 0.0, 1.0);
					  tmpvar_24 = tmpvar_25;
					  lowp vec3 tmpvar_26;
					  mediump vec3 tmpvar_27;
					  tmpvar_27 = clamp (specColor_4, 0.0, 1.0);
					  tmpvar_26 = tmpvar_27;
					  highp vec4 tmpvar_28;
					  tmpvar_28.w = 1.0;
					  tmpvar_28.xyz = tmpvar_1;
					  xlv_COLOR0 = tmpvar_24;
					  xlv_COLOR1 = tmpvar_26;
					  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  gl_Position = (glstate_matrix_mvp * tmpvar_28);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					varying lowp vec4 xlv_COLOR0;
					varying lowp vec3 xlv_COLOR1;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 col_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0);
					  col_1.xyz = (tmpvar_2 * xlv_COLOR0).xyz;
					  col_1.xyz = (col_1 * 2.0).xyz;
					  col_1.w = (tmpvar_2.w * xlv_COLOR0.w);
					  col_1.xyz = (col_1.xyz + xlv_COLOR1);
					  gl_FragData[0] = col_1;
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
Keywords { "SPOT" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "SPOT" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "SPOT" }
					
}
}
 }
 Pass {
  Tags { "LIGHTMODE"="VertexLM" "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
  ZWrite Off
  Blend SrcAlpha OneMinusSrcAlpha
  ColorMask RGB
  GpuProgramID 74067
Program "vp" {
SubProgram "gles hw_tier01 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesColor;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp vec4 unity_LightmapST;
					uniform highp vec4 _MainTex_ST;
					varying lowp vec4 xlv_COLOR0;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  mediump vec4 tmpvar_2;
					  tmpvar_2 = clamp (_glesColor, 0.0, 1.0);
					  tmpvar_1 = tmpvar_2;
					  highp vec4 tmpvar_3;
					  tmpvar_3.w = 1.0;
					  tmpvar_3.xyz = _glesVertex.xyz;
					  xlv_COLOR0 = tmpvar_1;
					  xlv_TEXCOORD0 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
					  xlv_TEXCOORD1 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  gl_Position = (glstate_matrix_mvp * tmpvar_3);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D unity_Lightmap;
					uniform sampler2D _MainTex;
					uniform lowp vec4 _Color;
					varying lowp vec4 xlv_COLOR0;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					void main ()
					{
					  lowp vec4 col_1;
					  col_1 = (texture2D (unity_Lightmap, xlv_TEXCOORD0) * _Color);
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD1);
					  col_1.xyz = (tmpvar_2 * col_1).xyz;
					  col_1.xyz = (col_1 * 2.0).xyz;
					  col_1.w = (tmpvar_2.w * xlv_COLOR0.w);
					  gl_FragData[0] = col_1;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesColor;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp vec4 unity_LightmapST;
					uniform highp vec4 _MainTex_ST;
					varying lowp vec4 xlv_COLOR0;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  mediump vec4 tmpvar_2;
					  tmpvar_2 = clamp (_glesColor, 0.0, 1.0);
					  tmpvar_1 = tmpvar_2;
					  highp vec4 tmpvar_3;
					  tmpvar_3.w = 1.0;
					  tmpvar_3.xyz = _glesVertex.xyz;
					  xlv_COLOR0 = tmpvar_1;
					  xlv_TEXCOORD0 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
					  xlv_TEXCOORD1 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  gl_Position = (glstate_matrix_mvp * tmpvar_3);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D unity_Lightmap;
					uniform sampler2D _MainTex;
					uniform lowp vec4 _Color;
					varying lowp vec4 xlv_COLOR0;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					void main ()
					{
					  lowp vec4 col_1;
					  col_1 = (texture2D (unity_Lightmap, xlv_TEXCOORD0) * _Color);
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD1);
					  col_1.xyz = (tmpvar_2 * col_1).xyz;
					  col_1.xyz = (col_1 * 2.0).xyz;
					  col_1.w = (tmpvar_2.w * xlv_COLOR0.w);
					  gl_FragData[0] = col_1;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesColor;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp vec4 unity_LightmapST;
					uniform highp vec4 _MainTex_ST;
					varying lowp vec4 xlv_COLOR0;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  mediump vec4 tmpvar_2;
					  tmpvar_2 = clamp (_glesColor, 0.0, 1.0);
					  tmpvar_1 = tmpvar_2;
					  highp vec4 tmpvar_3;
					  tmpvar_3.w = 1.0;
					  tmpvar_3.xyz = _glesVertex.xyz;
					  xlv_COLOR0 = tmpvar_1;
					  xlv_TEXCOORD0 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
					  xlv_TEXCOORD1 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  gl_Position = (glstate_matrix_mvp * tmpvar_3);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D unity_Lightmap;
					uniform sampler2D _MainTex;
					uniform lowp vec4 _Color;
					varying lowp vec4 xlv_COLOR0;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					void main ()
					{
					  lowp vec4 col_1;
					  col_1 = (texture2D (unity_Lightmap, xlv_TEXCOORD0) * _Color);
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD1);
					  col_1.xyz = (tmpvar_2 * col_1).xyz;
					  col_1.xyz = (col_1 * 2.0).xyz;
					  col_1.w = (tmpvar_2.w * xlv_COLOR0.w);
					  gl_FragData[0] = col_1;
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
  Tags { "LIGHTMODE"="VertexLMRGBM" "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
  ZWrite Off
  Blend SrcAlpha OneMinusSrcAlpha
  ColorMask RGB
  GpuProgramID 177689
Program "vp" {
SubProgram "gles hw_tier01 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesColor;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp vec4 unity_LightmapST;
					uniform highp vec4 unity_Lightmap_ST;
					uniform highp vec4 _MainTex_ST;
					varying lowp vec4 xlv_COLOR0;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec2 xlv_TEXCOORD2;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  mediump vec4 tmpvar_2;
					  tmpvar_2 = clamp (_glesColor, 0.0, 1.0);
					  tmpvar_1 = tmpvar_2;
					  highp vec4 tmpvar_3;
					  tmpvar_3.w = 1.0;
					  tmpvar_3.xyz = _glesVertex.xyz;
					  xlv_COLOR0 = tmpvar_1;
					  xlv_TEXCOORD0 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
					  xlv_TEXCOORD1 = ((_glesMultiTexCoord1.xy * unity_Lightmap_ST.xy) + unity_Lightmap_ST.zw);
					  xlv_TEXCOORD2 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  gl_Position = (glstate_matrix_mvp * tmpvar_3);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D unity_Lightmap;
					uniform sampler2D _MainTex;
					uniform lowp vec4 _Color;
					varying lowp vec4 xlv_COLOR0;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD2;
					void main ()
					{
					  lowp vec4 col_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (unity_Lightmap, xlv_TEXCOORD0);
					  col_1 = (tmpvar_2 * tmpvar_2.w);
					  col_1 = (col_1 * 2.0);
					  col_1 = (col_1 * _Color);
					  lowp vec4 tmpvar_3;
					  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD2);
					  col_1.xyz = (tmpvar_3 * col_1).xyz;
					  col_1.xyz = (col_1 * 4.0).xyz;
					  col_1.w = (tmpvar_3.w * xlv_COLOR0.w);
					  gl_FragData[0] = col_1;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesColor;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp vec4 unity_LightmapST;
					uniform highp vec4 unity_Lightmap_ST;
					uniform highp vec4 _MainTex_ST;
					varying lowp vec4 xlv_COLOR0;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec2 xlv_TEXCOORD2;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  mediump vec4 tmpvar_2;
					  tmpvar_2 = clamp (_glesColor, 0.0, 1.0);
					  tmpvar_1 = tmpvar_2;
					  highp vec4 tmpvar_3;
					  tmpvar_3.w = 1.0;
					  tmpvar_3.xyz = _glesVertex.xyz;
					  xlv_COLOR0 = tmpvar_1;
					  xlv_TEXCOORD0 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
					  xlv_TEXCOORD1 = ((_glesMultiTexCoord1.xy * unity_Lightmap_ST.xy) + unity_Lightmap_ST.zw);
					  xlv_TEXCOORD2 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  gl_Position = (glstate_matrix_mvp * tmpvar_3);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D unity_Lightmap;
					uniform sampler2D _MainTex;
					uniform lowp vec4 _Color;
					varying lowp vec4 xlv_COLOR0;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD2;
					void main ()
					{
					  lowp vec4 col_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (unity_Lightmap, xlv_TEXCOORD0);
					  col_1 = (tmpvar_2 * tmpvar_2.w);
					  col_1 = (col_1 * 2.0);
					  col_1 = (col_1 * _Color);
					  lowp vec4 tmpvar_3;
					  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD2);
					  col_1.xyz = (tmpvar_3 * col_1).xyz;
					  col_1.xyz = (col_1 * 4.0).xyz;
					  col_1.w = (tmpvar_3.w * xlv_COLOR0.w);
					  gl_FragData[0] = col_1;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesColor;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp vec4 unity_LightmapST;
					uniform highp vec4 unity_Lightmap_ST;
					uniform highp vec4 _MainTex_ST;
					varying lowp vec4 xlv_COLOR0;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec2 xlv_TEXCOORD2;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  mediump vec4 tmpvar_2;
					  tmpvar_2 = clamp (_glesColor, 0.0, 1.0);
					  tmpvar_1 = tmpvar_2;
					  highp vec4 tmpvar_3;
					  tmpvar_3.w = 1.0;
					  tmpvar_3.xyz = _glesVertex.xyz;
					  xlv_COLOR0 = tmpvar_1;
					  xlv_TEXCOORD0 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
					  xlv_TEXCOORD1 = ((_glesMultiTexCoord1.xy * unity_Lightmap_ST.xy) + unity_Lightmap_ST.zw);
					  xlv_TEXCOORD2 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  gl_Position = (glstate_matrix_mvp * tmpvar_3);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D unity_Lightmap;
					uniform sampler2D _MainTex;
					uniform lowp vec4 _Color;
					varying lowp vec4 xlv_COLOR0;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD2;
					void main ()
					{
					  lowp vec4 col_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (unity_Lightmap, xlv_TEXCOORD0);
					  col_1 = (tmpvar_2 * tmpvar_2.w);
					  col_1 = (col_1 * 2.0);
					  col_1 = (col_1 * _Color);
					  lowp vec4 tmpvar_3;
					  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD2);
					  col_1.xyz = (tmpvar_3 * col_1).xyz;
					  col_1.xyz = (col_1 * 4.0).xyz;
					  col_1.w = (tmpvar_3.w * xlv_COLOR0.w);
					  gl_FragData[0] = col_1;
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
}
}