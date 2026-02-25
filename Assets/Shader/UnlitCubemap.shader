Shader "Custom/UnlitCubemap" {
Properties {
 _MainTex ("Texture", 2D) = "white" { }
 _Cube ("Cubemap", CUBE) = "" { }
 _Intensity ("Intensity", Range(0.000000,1.000000)) = 1.000000
}
SubShader { 
 LOD 100
 Tags { "RenderType"="Opaque" }
 Pass {
  Tags { "RenderType"="Opaque" }
  GpuProgramID 4141
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
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD2;
					void main ()
					{
					  highp vec4 tmpvar_1;
					  tmpvar_1.w = 0.0;
					  tmpvar_1.xyz = _glesNormal;
					  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  gl_Position = (glstate_matrix_mvp * _glesVertex);
					  xlv_TEXCOORD3 = normalize((tmpvar_1 * unity_WorldToObject).xyz);
					  xlv_TEXCOORD2 = ((unity_ObjectToWorld * _glesVertex).xyz - _WorldSpaceCameraPos);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					uniform lowp samplerCube _Cube;
					uniform highp float _Intensity;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD2;
					void main ()
					{
					  lowp vec4 col_1;
					  highp vec3 tmpvar_2;
					  tmpvar_2 = normalize(xlv_TEXCOORD3);
					  highp vec3 tmpvar_3;
					  tmpvar_3 = (xlv_TEXCOORD2 - (2.0 * (
					    dot (tmpvar_2, xlv_TEXCOORD2)
					   * tmpvar_2)));
					  col_1 = (texture2D (_MainTex, xlv_TEXCOORD0) + (textureCube (_Cube, tmpvar_3) * _Intensity));
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
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD2;
					void main ()
					{
					  highp vec4 tmpvar_1;
					  tmpvar_1.w = 0.0;
					  tmpvar_1.xyz = _glesNormal;
					  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  gl_Position = (glstate_matrix_mvp * _glesVertex);
					  xlv_TEXCOORD3 = normalize((tmpvar_1 * unity_WorldToObject).xyz);
					  xlv_TEXCOORD2 = ((unity_ObjectToWorld * _glesVertex).xyz - _WorldSpaceCameraPos);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					uniform lowp samplerCube _Cube;
					uniform highp float _Intensity;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD2;
					void main ()
					{
					  lowp vec4 col_1;
					  highp vec3 tmpvar_2;
					  tmpvar_2 = normalize(xlv_TEXCOORD3);
					  highp vec3 tmpvar_3;
					  tmpvar_3 = (xlv_TEXCOORD2 - (2.0 * (
					    dot (tmpvar_2, xlv_TEXCOORD2)
					   * tmpvar_2)));
					  col_1 = (texture2D (_MainTex, xlv_TEXCOORD0) + (textureCube (_Cube, tmpvar_3) * _Intensity));
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
					uniform highp mat4 unity_WorldToObject;
					uniform highp vec4 _MainTex_ST;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD2;
					void main ()
					{
					  highp vec4 tmpvar_1;
					  tmpvar_1.w = 0.0;
					  tmpvar_1.xyz = _glesNormal;
					  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  gl_Position = (glstate_matrix_mvp * _glesVertex);
					  xlv_TEXCOORD3 = normalize((tmpvar_1 * unity_WorldToObject).xyz);
					  xlv_TEXCOORD2 = ((unity_ObjectToWorld * _glesVertex).xyz - _WorldSpaceCameraPos);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					uniform lowp samplerCube _Cube;
					uniform highp float _Intensity;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec3 xlv_TEXCOORD3;
					varying highp vec3 xlv_TEXCOORD2;
					void main ()
					{
					  lowp vec4 col_1;
					  highp vec3 tmpvar_2;
					  tmpvar_2 = normalize(xlv_TEXCOORD3);
					  highp vec3 tmpvar_3;
					  tmpvar_3 = (xlv_TEXCOORD2 - (2.0 * (
					    dot (tmpvar_2, xlv_TEXCOORD2)
					   * tmpvar_2)));
					  col_1 = (texture2D (_MainTex, xlv_TEXCOORD0) + (textureCube (_Cube, tmpvar_3) * _Intensity));
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