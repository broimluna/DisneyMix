Shader "Projector/Multiply" {
Properties {
 _ShadowTex ("Cookie", 2D) = "gray" { }
 _FalloffTex ("FallOff", 2D) = "white" { }
}
SubShader { 
 Tags { "QUEUE"="Transparent" }
 Pass {
  Tags { "QUEUE"="Transparent" }
  ZWrite Off
  Blend DstColor Zero
  ColorMask RGB
  Offset -1.000000, -1.000000
  GpuProgramID 20733
Program "vp" {
SubProgram "gles hw_tier01 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_Projector;
					uniform highp mat4 unity_ProjectorClip;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					void main ()
					{
					  xlv_TEXCOORD0 = (unity_Projector * _glesVertex);
					  xlv_TEXCOORD1 = (unity_ProjectorClip * _glesVertex);
					  gl_Position = (glstate_matrix_mvp * _glesVertex);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _ShadowTex;
					uniform sampler2D _FalloffTex;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					void main ()
					{
					  lowp vec4 texS_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2DProj (_ShadowTex, xlv_TEXCOORD0);
					  texS_1.xyz = tmpvar_2.xyz;
					  texS_1.w = (1.0 - tmpvar_2.w);
					  lowp vec4 tmpvar_3;
					  tmpvar_3 = mix (vec4(1.0, 1.0, 1.0, 0.0), texS_1, texture2DProj (_FalloffTex, xlv_TEXCOORD1).wwww);
					  gl_FragData[0] = tmpvar_3;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_Projector;
					uniform highp mat4 unity_ProjectorClip;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					void main ()
					{
					  xlv_TEXCOORD0 = (unity_Projector * _glesVertex);
					  xlv_TEXCOORD1 = (unity_ProjectorClip * _glesVertex);
					  gl_Position = (glstate_matrix_mvp * _glesVertex);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _ShadowTex;
					uniform sampler2D _FalloffTex;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					void main ()
					{
					  lowp vec4 texS_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2DProj (_ShadowTex, xlv_TEXCOORD0);
					  texS_1.xyz = tmpvar_2.xyz;
					  texS_1.w = (1.0 - tmpvar_2.w);
					  lowp vec4 tmpvar_3;
					  tmpvar_3 = mix (vec4(1.0, 1.0, 1.0, 0.0), texS_1, texture2DProj (_FalloffTex, xlv_TEXCOORD1).wwww);
					  gl_FragData[0] = tmpvar_3;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp mat4 unity_Projector;
					uniform highp mat4 unity_ProjectorClip;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					void main ()
					{
					  xlv_TEXCOORD0 = (unity_Projector * _glesVertex);
					  xlv_TEXCOORD1 = (unity_ProjectorClip * _glesVertex);
					  gl_Position = (glstate_matrix_mvp * _glesVertex);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _ShadowTex;
					uniform sampler2D _FalloffTex;
					varying highp vec4 xlv_TEXCOORD0;
					varying highp vec4 xlv_TEXCOORD1;
					void main ()
					{
					  lowp vec4 texS_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2DProj (_ShadowTex, xlv_TEXCOORD0);
					  texS_1.xyz = tmpvar_2.xyz;
					  texS_1.w = (1.0 - tmpvar_2.w);
					  lowp vec4 tmpvar_3;
					  tmpvar_3 = mix (vec4(1.0, 1.0, 1.0, 0.0), texS_1, texture2DProj (_FalloffTex, xlv_TEXCOORD1).wwww);
					  gl_FragData[0] = tmpvar_3;
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