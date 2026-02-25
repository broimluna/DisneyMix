Shader "Custom/gradientlight" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "white" { }
 _RampTex ("Lighting Ramp", 2D) = "white" { }
 _RimColor ("Rim Color", Color) = (1.000000,1.000000,1.000000,1.000000)
 _RimPower ("Rim Power", Float) = 2.000000
 _LightDir ("Lighting Direction", Vector) = (0.000000,0.000000,-1.000000,0.000000)
}
SubShader { 
 LOD 200
 Tags { "QUEUE"="Geometry" "RenderType"="Opaque" }
 Pass {
  Tags { "QUEUE"="Geometry" "RenderType"="Opaque" }
  GpuProgramID 12898
Program "vp" {
SubProgram "gles hw_tier01 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec3 _glesNormal;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp vec4 _MainTex_ST;
					varying mediump vec2 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec3 xlv_TEXCOORD2;
					void main ()
					{
					  mediump vec2 tmpvar_1;
					  mediump vec3 tmpvar_2;
					  mediump vec3 tmpvar_3;
					  tmpvar_1 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec4 tmpvar_4;
					  tmpvar_4.w = 0.0;
					  tmpvar_4.xyz = _glesNormal;
					  highp vec3 tmpvar_5;
					  tmpvar_5 = normalize((unity_ObjectToWorld * tmpvar_4).xyz);
					  tmpvar_2 = tmpvar_5;
					  highp vec3 tmpvar_6;
					  tmpvar_6 = normalize((_WorldSpaceCameraPos - (unity_ObjectToWorld * _glesVertex).xyz));
					  tmpvar_3 = tmpvar_6;
					  gl_Position = (glstate_matrix_mvp * _glesVertex);
					  xlv_TEXCOORD0 = tmpvar_1;
					  xlv_TEXCOORD1 = tmpvar_2;
					  xlv_TEXCOORD2 = tmpvar_3;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					uniform sampler2D _RampTex;
					uniform mediump vec4 _RimColor;
					uniform lowp float _RimPower;
					uniform lowp vec4 _LightDir;
					varying mediump vec2 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec3 xlv_TEXCOORD2;
					void main ()
					{
					  lowp vec4 col_1;
					  lowp vec3 rim_2;
					  lowp vec3 tmpvar_3;
					  tmpvar_3 = normalize(_LightDir.xyz);
					  mediump float tmpvar_4;
					  tmpvar_4 = clamp (dot (tmpvar_3, normalize(xlv_TEXCOORD1)), 0.0, 1.0);
					  lowp float tmpvar_5;
					  tmpvar_5 = ((tmpvar_4 * 0.5) + 0.5);
					  lowp vec2 tmpvar_6;
					  tmpvar_6.y = 0.5;
					  tmpvar_6.x = tmpvar_5;
					  mediump float tmpvar_7;
					  tmpvar_7 = clamp ((1.0 - dot (xlv_TEXCOORD1, 
					    normalize(xlv_TEXCOORD2)
					  )), 0.0, 1.0);
					  lowp float tmpvar_8;
					  tmpvar_8 = pow (tmpvar_7, _RimPower);
					  mediump vec3 tmpvar_9;
					  tmpvar_9 = ((tmpvar_8 * _RimColor.xyz) * _RimColor.w);
					  rim_2 = tmpvar_9;
					  col_1.xyz = ((texture2D (_MainTex, xlv_TEXCOORD0).xyz * (texture2D (_RampTex, tmpvar_6).xyz * 2.0)) + rim_2);
					  col_1.w = 1.0;
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
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp vec4 _MainTex_ST;
					varying mediump vec2 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec3 xlv_TEXCOORD2;
					void main ()
					{
					  mediump vec2 tmpvar_1;
					  mediump vec3 tmpvar_2;
					  mediump vec3 tmpvar_3;
					  tmpvar_1 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec4 tmpvar_4;
					  tmpvar_4.w = 0.0;
					  tmpvar_4.xyz = _glesNormal;
					  highp vec3 tmpvar_5;
					  tmpvar_5 = normalize((unity_ObjectToWorld * tmpvar_4).xyz);
					  tmpvar_2 = tmpvar_5;
					  highp vec3 tmpvar_6;
					  tmpvar_6 = normalize((_WorldSpaceCameraPos - (unity_ObjectToWorld * _glesVertex).xyz));
					  tmpvar_3 = tmpvar_6;
					  gl_Position = (glstate_matrix_mvp * _glesVertex);
					  xlv_TEXCOORD0 = tmpvar_1;
					  xlv_TEXCOORD1 = tmpvar_2;
					  xlv_TEXCOORD2 = tmpvar_3;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					uniform sampler2D _RampTex;
					uniform mediump vec4 _RimColor;
					uniform lowp float _RimPower;
					uniform lowp vec4 _LightDir;
					varying mediump vec2 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec3 xlv_TEXCOORD2;
					void main ()
					{
					  lowp vec4 col_1;
					  lowp vec3 rim_2;
					  lowp vec3 tmpvar_3;
					  tmpvar_3 = normalize(_LightDir.xyz);
					  mediump float tmpvar_4;
					  tmpvar_4 = clamp (dot (tmpvar_3, normalize(xlv_TEXCOORD1)), 0.0, 1.0);
					  lowp float tmpvar_5;
					  tmpvar_5 = ((tmpvar_4 * 0.5) + 0.5);
					  lowp vec2 tmpvar_6;
					  tmpvar_6.y = 0.5;
					  tmpvar_6.x = tmpvar_5;
					  mediump float tmpvar_7;
					  tmpvar_7 = clamp ((1.0 - dot (xlv_TEXCOORD1, 
					    normalize(xlv_TEXCOORD2)
					  )), 0.0, 1.0);
					  lowp float tmpvar_8;
					  tmpvar_8 = pow (tmpvar_7, _RimPower);
					  mediump vec3 tmpvar_9;
					  tmpvar_9 = ((tmpvar_8 * _RimColor.xyz) * _RimColor.w);
					  rim_2 = tmpvar_9;
					  col_1.xyz = ((texture2D (_MainTex, xlv_TEXCOORD0).xyz * (texture2D (_RampTex, tmpvar_6).xyz * 2.0)) + rim_2);
					  col_1.w = 1.0;
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
					uniform highp vec3 _WorldSpaceCameraPos;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_ObjectToWorld;
					uniform highp vec4 _MainTex_ST;
					varying mediump vec2 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec3 xlv_TEXCOORD2;
					void main ()
					{
					  mediump vec2 tmpvar_1;
					  mediump vec3 tmpvar_2;
					  mediump vec3 tmpvar_3;
					  tmpvar_1 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  highp vec4 tmpvar_4;
					  tmpvar_4.w = 0.0;
					  tmpvar_4.xyz = _glesNormal;
					  highp vec3 tmpvar_5;
					  tmpvar_5 = normalize((unity_ObjectToWorld * tmpvar_4).xyz);
					  tmpvar_2 = tmpvar_5;
					  highp vec3 tmpvar_6;
					  tmpvar_6 = normalize((_WorldSpaceCameraPos - (unity_ObjectToWorld * _glesVertex).xyz));
					  tmpvar_3 = tmpvar_6;
					  gl_Position = (glstate_matrix_mvp * _glesVertex);
					  xlv_TEXCOORD0 = tmpvar_1;
					  xlv_TEXCOORD1 = tmpvar_2;
					  xlv_TEXCOORD2 = tmpvar_3;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					uniform sampler2D _RampTex;
					uniform mediump vec4 _RimColor;
					uniform lowp float _RimPower;
					uniform lowp vec4 _LightDir;
					varying mediump vec2 xlv_TEXCOORD0;
					varying mediump vec3 xlv_TEXCOORD1;
					varying mediump vec3 xlv_TEXCOORD2;
					void main ()
					{
					  lowp vec4 col_1;
					  lowp vec3 rim_2;
					  lowp vec3 tmpvar_3;
					  tmpvar_3 = normalize(_LightDir.xyz);
					  mediump float tmpvar_4;
					  tmpvar_4 = clamp (dot (tmpvar_3, normalize(xlv_TEXCOORD1)), 0.0, 1.0);
					  lowp float tmpvar_5;
					  tmpvar_5 = ((tmpvar_4 * 0.5) + 0.5);
					  lowp vec2 tmpvar_6;
					  tmpvar_6.y = 0.5;
					  tmpvar_6.x = tmpvar_5;
					  mediump float tmpvar_7;
					  tmpvar_7 = clamp ((1.0 - dot (xlv_TEXCOORD1, 
					    normalize(xlv_TEXCOORD2)
					  )), 0.0, 1.0);
					  lowp float tmpvar_8;
					  tmpvar_8 = pow (tmpvar_7, _RimPower);
					  mediump vec3 tmpvar_9;
					  tmpvar_9 = ((tmpvar_8 * _RimColor.xyz) * _RimColor.w);
					  rim_2 = tmpvar_9;
					  col_1.xyz = ((texture2D (_MainTex, xlv_TEXCOORD0).xyz * (texture2D (_RampTex, tmpvar_6).xyz * 2.0)) + rim_2);
					  col_1.w = 1.0;
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
Fallback "Diffuse"
}