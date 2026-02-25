Shader "Skybox/6 Sided" {
Properties {
 _Tint ("Tint Color", Color) = (0.500000,0.500000,0.500000,0.500000)
[Gamma]  _Exposure ("Exposure", Range(0.000000,8.000000)) = 1.000000
 _Rotation ("Rotation", Range(0.000000,360.000000)) = 0.000000
[NoScaleOffset]  _FrontTex ("Front [+Z]   (HDR)", 2D) = "grey" { }
[NoScaleOffset]  _BackTex ("Back [-Z]   (HDR)", 2D) = "grey" { }
[NoScaleOffset]  _LeftTex ("Left [+X]   (HDR)", 2D) = "grey" { }
[NoScaleOffset]  _RightTex ("Right [-X]   (HDR)", 2D) = "grey" { }
[NoScaleOffset]  _UpTex ("Up [+Y]   (HDR)", 2D) = "grey" { }
[NoScaleOffset]  _DownTex ("Down [-Y]   (HDR)", 2D) = "grey" { }
}
SubShader { 
 Tags { "QUEUE"="Background" "RenderType"="Background" "PreviewType"="Skybox" }
 Pass {
  Tags { "QUEUE"="Background" "RenderType"="Background" "PreviewType"="Skybox" }
  ZWrite Off
  Cull Off
  GpuProgramID 36425
Program "vp" {
SubProgram "gles hw_tier01 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp float _Rotation;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  highp float tmpvar_1;
					  tmpvar_1 = ((_Rotation * 3.141593) / 180.0);
					  highp float tmpvar_2;
					  tmpvar_2 = sin(tmpvar_1);
					  highp float tmpvar_3;
					  tmpvar_3 = cos(tmpvar_1);
					  highp mat2 tmpvar_4;
					  tmpvar_4[0].x = tmpvar_3;
					  tmpvar_4[0].y = tmpvar_2;
					  tmpvar_4[1].x = -(tmpvar_2);
					  tmpvar_4[1].y = tmpvar_3;
					  highp vec3 tmpvar_5;
					  tmpvar_5.xy = (tmpvar_4 * _glesVertex.xz);
					  tmpvar_5.z = _glesVertex.y;
					  highp vec4 tmpvar_6;
					  tmpvar_6.w = 1.0;
					  tmpvar_6.xyz = tmpvar_5.xzy;
					  gl_Position = (glstate_matrix_mvp * tmpvar_6);
					  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform lowp vec4 unity_ColorSpaceDouble;
					uniform mediump vec4 _Tint;
					uniform mediump float _Exposure;
					uniform sampler2D _FrontTex;
					uniform mediump vec4 _FrontTex_HDR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_FrontTex, xlv_TEXCOORD0);
					  mediump vec4 tmpvar_3;
					  tmpvar_3 = tmpvar_2;
					  c_1 = (((
					    (_FrontTex_HDR.x * tmpvar_3.w)
					   * tmpvar_3.xyz) * _Tint.xyz) * unity_ColorSpaceDouble.xyz);
					  c_1 = (c_1 * _Exposure);
					  mediump vec4 tmpvar_4;
					  tmpvar_4.w = 1.0;
					  tmpvar_4.xyz = c_1;
					  gl_FragData[0] = tmpvar_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp float _Rotation;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  highp float tmpvar_1;
					  tmpvar_1 = ((_Rotation * 3.141593) / 180.0);
					  highp float tmpvar_2;
					  tmpvar_2 = sin(tmpvar_1);
					  highp float tmpvar_3;
					  tmpvar_3 = cos(tmpvar_1);
					  highp mat2 tmpvar_4;
					  tmpvar_4[0].x = tmpvar_3;
					  tmpvar_4[0].y = tmpvar_2;
					  tmpvar_4[1].x = -(tmpvar_2);
					  tmpvar_4[1].y = tmpvar_3;
					  highp vec3 tmpvar_5;
					  tmpvar_5.xy = (tmpvar_4 * _glesVertex.xz);
					  tmpvar_5.z = _glesVertex.y;
					  highp vec4 tmpvar_6;
					  tmpvar_6.w = 1.0;
					  tmpvar_6.xyz = tmpvar_5.xzy;
					  gl_Position = (glstate_matrix_mvp * tmpvar_6);
					  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform lowp vec4 unity_ColorSpaceDouble;
					uniform mediump vec4 _Tint;
					uniform mediump float _Exposure;
					uniform sampler2D _FrontTex;
					uniform mediump vec4 _FrontTex_HDR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_FrontTex, xlv_TEXCOORD0);
					  mediump vec4 tmpvar_3;
					  tmpvar_3 = tmpvar_2;
					  c_1 = (((
					    (_FrontTex_HDR.x * tmpvar_3.w)
					   * tmpvar_3.xyz) * _Tint.xyz) * unity_ColorSpaceDouble.xyz);
					  c_1 = (c_1 * _Exposure);
					  mediump vec4 tmpvar_4;
					  tmpvar_4.w = 1.0;
					  tmpvar_4.xyz = c_1;
					  gl_FragData[0] = tmpvar_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp float _Rotation;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  highp float tmpvar_1;
					  tmpvar_1 = ((_Rotation * 3.141593) / 180.0);
					  highp float tmpvar_2;
					  tmpvar_2 = sin(tmpvar_1);
					  highp float tmpvar_3;
					  tmpvar_3 = cos(tmpvar_1);
					  highp mat2 tmpvar_4;
					  tmpvar_4[0].x = tmpvar_3;
					  tmpvar_4[0].y = tmpvar_2;
					  tmpvar_4[1].x = -(tmpvar_2);
					  tmpvar_4[1].y = tmpvar_3;
					  highp vec3 tmpvar_5;
					  tmpvar_5.xy = (tmpvar_4 * _glesVertex.xz);
					  tmpvar_5.z = _glesVertex.y;
					  highp vec4 tmpvar_6;
					  tmpvar_6.w = 1.0;
					  tmpvar_6.xyz = tmpvar_5.xzy;
					  gl_Position = (glstate_matrix_mvp * tmpvar_6);
					  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform lowp vec4 unity_ColorSpaceDouble;
					uniform mediump vec4 _Tint;
					uniform mediump float _Exposure;
					uniform sampler2D _FrontTex;
					uniform mediump vec4 _FrontTex_HDR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_FrontTex, xlv_TEXCOORD0);
					  mediump vec4 tmpvar_3;
					  tmpvar_3 = tmpvar_2;
					  c_1 = (((
					    (_FrontTex_HDR.x * tmpvar_3.w)
					   * tmpvar_3.xyz) * _Tint.xyz) * unity_ColorSpaceDouble.xyz);
					  c_1 = (c_1 * _Exposure);
					  mediump vec4 tmpvar_4;
					  tmpvar_4.w = 1.0;
					  tmpvar_4.xyz = c_1;
					  gl_FragData[0] = tmpvar_4;
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
  Tags { "QUEUE"="Background" "RenderType"="Background" "PreviewType"="Skybox" }
  ZWrite Off
  Cull Off
  GpuProgramID 84173
Program "vp" {
SubProgram "gles hw_tier01 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp float _Rotation;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  highp float tmpvar_1;
					  tmpvar_1 = ((_Rotation * 3.141593) / 180.0);
					  highp float tmpvar_2;
					  tmpvar_2 = sin(tmpvar_1);
					  highp float tmpvar_3;
					  tmpvar_3 = cos(tmpvar_1);
					  highp mat2 tmpvar_4;
					  tmpvar_4[0].x = tmpvar_3;
					  tmpvar_4[0].y = tmpvar_2;
					  tmpvar_4[1].x = -(tmpvar_2);
					  tmpvar_4[1].y = tmpvar_3;
					  highp vec3 tmpvar_5;
					  tmpvar_5.xy = (tmpvar_4 * _glesVertex.xz);
					  tmpvar_5.z = _glesVertex.y;
					  highp vec4 tmpvar_6;
					  tmpvar_6.w = 1.0;
					  tmpvar_6.xyz = tmpvar_5.xzy;
					  gl_Position = (glstate_matrix_mvp * tmpvar_6);
					  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform lowp vec4 unity_ColorSpaceDouble;
					uniform mediump vec4 _Tint;
					uniform mediump float _Exposure;
					uniform sampler2D _BackTex;
					uniform mediump vec4 _BackTex_HDR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_BackTex, xlv_TEXCOORD0);
					  mediump vec4 tmpvar_3;
					  tmpvar_3 = tmpvar_2;
					  c_1 = (((
					    (_BackTex_HDR.x * tmpvar_3.w)
					   * tmpvar_3.xyz) * _Tint.xyz) * unity_ColorSpaceDouble.xyz);
					  c_1 = (c_1 * _Exposure);
					  mediump vec4 tmpvar_4;
					  tmpvar_4.w = 1.0;
					  tmpvar_4.xyz = c_1;
					  gl_FragData[0] = tmpvar_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp float _Rotation;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  highp float tmpvar_1;
					  tmpvar_1 = ((_Rotation * 3.141593) / 180.0);
					  highp float tmpvar_2;
					  tmpvar_2 = sin(tmpvar_1);
					  highp float tmpvar_3;
					  tmpvar_3 = cos(tmpvar_1);
					  highp mat2 tmpvar_4;
					  tmpvar_4[0].x = tmpvar_3;
					  tmpvar_4[0].y = tmpvar_2;
					  tmpvar_4[1].x = -(tmpvar_2);
					  tmpvar_4[1].y = tmpvar_3;
					  highp vec3 tmpvar_5;
					  tmpvar_5.xy = (tmpvar_4 * _glesVertex.xz);
					  tmpvar_5.z = _glesVertex.y;
					  highp vec4 tmpvar_6;
					  tmpvar_6.w = 1.0;
					  tmpvar_6.xyz = tmpvar_5.xzy;
					  gl_Position = (glstate_matrix_mvp * tmpvar_6);
					  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform lowp vec4 unity_ColorSpaceDouble;
					uniform mediump vec4 _Tint;
					uniform mediump float _Exposure;
					uniform sampler2D _BackTex;
					uniform mediump vec4 _BackTex_HDR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_BackTex, xlv_TEXCOORD0);
					  mediump vec4 tmpvar_3;
					  tmpvar_3 = tmpvar_2;
					  c_1 = (((
					    (_BackTex_HDR.x * tmpvar_3.w)
					   * tmpvar_3.xyz) * _Tint.xyz) * unity_ColorSpaceDouble.xyz);
					  c_1 = (c_1 * _Exposure);
					  mediump vec4 tmpvar_4;
					  tmpvar_4.w = 1.0;
					  tmpvar_4.xyz = c_1;
					  gl_FragData[0] = tmpvar_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp float _Rotation;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  highp float tmpvar_1;
					  tmpvar_1 = ((_Rotation * 3.141593) / 180.0);
					  highp float tmpvar_2;
					  tmpvar_2 = sin(tmpvar_1);
					  highp float tmpvar_3;
					  tmpvar_3 = cos(tmpvar_1);
					  highp mat2 tmpvar_4;
					  tmpvar_4[0].x = tmpvar_3;
					  tmpvar_4[0].y = tmpvar_2;
					  tmpvar_4[1].x = -(tmpvar_2);
					  tmpvar_4[1].y = tmpvar_3;
					  highp vec3 tmpvar_5;
					  tmpvar_5.xy = (tmpvar_4 * _glesVertex.xz);
					  tmpvar_5.z = _glesVertex.y;
					  highp vec4 tmpvar_6;
					  tmpvar_6.w = 1.0;
					  tmpvar_6.xyz = tmpvar_5.xzy;
					  gl_Position = (glstate_matrix_mvp * tmpvar_6);
					  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform lowp vec4 unity_ColorSpaceDouble;
					uniform mediump vec4 _Tint;
					uniform mediump float _Exposure;
					uniform sampler2D _BackTex;
					uniform mediump vec4 _BackTex_HDR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_BackTex, xlv_TEXCOORD0);
					  mediump vec4 tmpvar_3;
					  tmpvar_3 = tmpvar_2;
					  c_1 = (((
					    (_BackTex_HDR.x * tmpvar_3.w)
					   * tmpvar_3.xyz) * _Tint.xyz) * unity_ColorSpaceDouble.xyz);
					  c_1 = (c_1 * _Exposure);
					  mediump vec4 tmpvar_4;
					  tmpvar_4.w = 1.0;
					  tmpvar_4.xyz = c_1;
					  gl_FragData[0] = tmpvar_4;
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
  Tags { "QUEUE"="Background" "RenderType"="Background" "PreviewType"="Skybox" }
  ZWrite Off
  Cull Off
  GpuProgramID 149710
Program "vp" {
SubProgram "gles hw_tier01 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp float _Rotation;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  highp float tmpvar_1;
					  tmpvar_1 = ((_Rotation * 3.141593) / 180.0);
					  highp float tmpvar_2;
					  tmpvar_2 = sin(tmpvar_1);
					  highp float tmpvar_3;
					  tmpvar_3 = cos(tmpvar_1);
					  highp mat2 tmpvar_4;
					  tmpvar_4[0].x = tmpvar_3;
					  tmpvar_4[0].y = tmpvar_2;
					  tmpvar_4[1].x = -(tmpvar_2);
					  tmpvar_4[1].y = tmpvar_3;
					  highp vec3 tmpvar_5;
					  tmpvar_5.xy = (tmpvar_4 * _glesVertex.xz);
					  tmpvar_5.z = _glesVertex.y;
					  highp vec4 tmpvar_6;
					  tmpvar_6.w = 1.0;
					  tmpvar_6.xyz = tmpvar_5.xzy;
					  gl_Position = (glstate_matrix_mvp * tmpvar_6);
					  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform lowp vec4 unity_ColorSpaceDouble;
					uniform mediump vec4 _Tint;
					uniform mediump float _Exposure;
					uniform sampler2D _LeftTex;
					uniform mediump vec4 _LeftTex_HDR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_LeftTex, xlv_TEXCOORD0);
					  mediump vec4 tmpvar_3;
					  tmpvar_3 = tmpvar_2;
					  c_1 = (((
					    (_LeftTex_HDR.x * tmpvar_3.w)
					   * tmpvar_3.xyz) * _Tint.xyz) * unity_ColorSpaceDouble.xyz);
					  c_1 = (c_1 * _Exposure);
					  mediump vec4 tmpvar_4;
					  tmpvar_4.w = 1.0;
					  tmpvar_4.xyz = c_1;
					  gl_FragData[0] = tmpvar_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp float _Rotation;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  highp float tmpvar_1;
					  tmpvar_1 = ((_Rotation * 3.141593) / 180.0);
					  highp float tmpvar_2;
					  tmpvar_2 = sin(tmpvar_1);
					  highp float tmpvar_3;
					  tmpvar_3 = cos(tmpvar_1);
					  highp mat2 tmpvar_4;
					  tmpvar_4[0].x = tmpvar_3;
					  tmpvar_4[0].y = tmpvar_2;
					  tmpvar_4[1].x = -(tmpvar_2);
					  tmpvar_4[1].y = tmpvar_3;
					  highp vec3 tmpvar_5;
					  tmpvar_5.xy = (tmpvar_4 * _glesVertex.xz);
					  tmpvar_5.z = _glesVertex.y;
					  highp vec4 tmpvar_6;
					  tmpvar_6.w = 1.0;
					  tmpvar_6.xyz = tmpvar_5.xzy;
					  gl_Position = (glstate_matrix_mvp * tmpvar_6);
					  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform lowp vec4 unity_ColorSpaceDouble;
					uniform mediump vec4 _Tint;
					uniform mediump float _Exposure;
					uniform sampler2D _LeftTex;
					uniform mediump vec4 _LeftTex_HDR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_LeftTex, xlv_TEXCOORD0);
					  mediump vec4 tmpvar_3;
					  tmpvar_3 = tmpvar_2;
					  c_1 = (((
					    (_LeftTex_HDR.x * tmpvar_3.w)
					   * tmpvar_3.xyz) * _Tint.xyz) * unity_ColorSpaceDouble.xyz);
					  c_1 = (c_1 * _Exposure);
					  mediump vec4 tmpvar_4;
					  tmpvar_4.w = 1.0;
					  tmpvar_4.xyz = c_1;
					  gl_FragData[0] = tmpvar_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp float _Rotation;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  highp float tmpvar_1;
					  tmpvar_1 = ((_Rotation * 3.141593) / 180.0);
					  highp float tmpvar_2;
					  tmpvar_2 = sin(tmpvar_1);
					  highp float tmpvar_3;
					  tmpvar_3 = cos(tmpvar_1);
					  highp mat2 tmpvar_4;
					  tmpvar_4[0].x = tmpvar_3;
					  tmpvar_4[0].y = tmpvar_2;
					  tmpvar_4[1].x = -(tmpvar_2);
					  tmpvar_4[1].y = tmpvar_3;
					  highp vec3 tmpvar_5;
					  tmpvar_5.xy = (tmpvar_4 * _glesVertex.xz);
					  tmpvar_5.z = _glesVertex.y;
					  highp vec4 tmpvar_6;
					  tmpvar_6.w = 1.0;
					  tmpvar_6.xyz = tmpvar_5.xzy;
					  gl_Position = (glstate_matrix_mvp * tmpvar_6);
					  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform lowp vec4 unity_ColorSpaceDouble;
					uniform mediump vec4 _Tint;
					uniform mediump float _Exposure;
					uniform sampler2D _LeftTex;
					uniform mediump vec4 _LeftTex_HDR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_LeftTex, xlv_TEXCOORD0);
					  mediump vec4 tmpvar_3;
					  tmpvar_3 = tmpvar_2;
					  c_1 = (((
					    (_LeftTex_HDR.x * tmpvar_3.w)
					   * tmpvar_3.xyz) * _Tint.xyz) * unity_ColorSpaceDouble.xyz);
					  c_1 = (c_1 * _Exposure);
					  mediump vec4 tmpvar_4;
					  tmpvar_4.w = 1.0;
					  tmpvar_4.xyz = c_1;
					  gl_FragData[0] = tmpvar_4;
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
  Tags { "QUEUE"="Background" "RenderType"="Background" "PreviewType"="Skybox" }
  ZWrite Off
  Cull Off
  GpuProgramID 257205
Program "vp" {
SubProgram "gles hw_tier01 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp float _Rotation;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  highp float tmpvar_1;
					  tmpvar_1 = ((_Rotation * 3.141593) / 180.0);
					  highp float tmpvar_2;
					  tmpvar_2 = sin(tmpvar_1);
					  highp float tmpvar_3;
					  tmpvar_3 = cos(tmpvar_1);
					  highp mat2 tmpvar_4;
					  tmpvar_4[0].x = tmpvar_3;
					  tmpvar_4[0].y = tmpvar_2;
					  tmpvar_4[1].x = -(tmpvar_2);
					  tmpvar_4[1].y = tmpvar_3;
					  highp vec3 tmpvar_5;
					  tmpvar_5.xy = (tmpvar_4 * _glesVertex.xz);
					  tmpvar_5.z = _glesVertex.y;
					  highp vec4 tmpvar_6;
					  tmpvar_6.w = 1.0;
					  tmpvar_6.xyz = tmpvar_5.xzy;
					  gl_Position = (glstate_matrix_mvp * tmpvar_6);
					  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform lowp vec4 unity_ColorSpaceDouble;
					uniform mediump vec4 _Tint;
					uniform mediump float _Exposure;
					uniform sampler2D _RightTex;
					uniform mediump vec4 _RightTex_HDR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_RightTex, xlv_TEXCOORD0);
					  mediump vec4 tmpvar_3;
					  tmpvar_3 = tmpvar_2;
					  c_1 = (((
					    (_RightTex_HDR.x * tmpvar_3.w)
					   * tmpvar_3.xyz) * _Tint.xyz) * unity_ColorSpaceDouble.xyz);
					  c_1 = (c_1 * _Exposure);
					  mediump vec4 tmpvar_4;
					  tmpvar_4.w = 1.0;
					  tmpvar_4.xyz = c_1;
					  gl_FragData[0] = tmpvar_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp float _Rotation;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  highp float tmpvar_1;
					  tmpvar_1 = ((_Rotation * 3.141593) / 180.0);
					  highp float tmpvar_2;
					  tmpvar_2 = sin(tmpvar_1);
					  highp float tmpvar_3;
					  tmpvar_3 = cos(tmpvar_1);
					  highp mat2 tmpvar_4;
					  tmpvar_4[0].x = tmpvar_3;
					  tmpvar_4[0].y = tmpvar_2;
					  tmpvar_4[1].x = -(tmpvar_2);
					  tmpvar_4[1].y = tmpvar_3;
					  highp vec3 tmpvar_5;
					  tmpvar_5.xy = (tmpvar_4 * _glesVertex.xz);
					  tmpvar_5.z = _glesVertex.y;
					  highp vec4 tmpvar_6;
					  tmpvar_6.w = 1.0;
					  tmpvar_6.xyz = tmpvar_5.xzy;
					  gl_Position = (glstate_matrix_mvp * tmpvar_6);
					  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform lowp vec4 unity_ColorSpaceDouble;
					uniform mediump vec4 _Tint;
					uniform mediump float _Exposure;
					uniform sampler2D _RightTex;
					uniform mediump vec4 _RightTex_HDR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_RightTex, xlv_TEXCOORD0);
					  mediump vec4 tmpvar_3;
					  tmpvar_3 = tmpvar_2;
					  c_1 = (((
					    (_RightTex_HDR.x * tmpvar_3.w)
					   * tmpvar_3.xyz) * _Tint.xyz) * unity_ColorSpaceDouble.xyz);
					  c_1 = (c_1 * _Exposure);
					  mediump vec4 tmpvar_4;
					  tmpvar_4.w = 1.0;
					  tmpvar_4.xyz = c_1;
					  gl_FragData[0] = tmpvar_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp float _Rotation;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  highp float tmpvar_1;
					  tmpvar_1 = ((_Rotation * 3.141593) / 180.0);
					  highp float tmpvar_2;
					  tmpvar_2 = sin(tmpvar_1);
					  highp float tmpvar_3;
					  tmpvar_3 = cos(tmpvar_1);
					  highp mat2 tmpvar_4;
					  tmpvar_4[0].x = tmpvar_3;
					  tmpvar_4[0].y = tmpvar_2;
					  tmpvar_4[1].x = -(tmpvar_2);
					  tmpvar_4[1].y = tmpvar_3;
					  highp vec3 tmpvar_5;
					  tmpvar_5.xy = (tmpvar_4 * _glesVertex.xz);
					  tmpvar_5.z = _glesVertex.y;
					  highp vec4 tmpvar_6;
					  tmpvar_6.w = 1.0;
					  tmpvar_6.xyz = tmpvar_5.xzy;
					  gl_Position = (glstate_matrix_mvp * tmpvar_6);
					  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform lowp vec4 unity_ColorSpaceDouble;
					uniform mediump vec4 _Tint;
					uniform mediump float _Exposure;
					uniform sampler2D _RightTex;
					uniform mediump vec4 _RightTex_HDR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_RightTex, xlv_TEXCOORD0);
					  mediump vec4 tmpvar_3;
					  tmpvar_3 = tmpvar_2;
					  c_1 = (((
					    (_RightTex_HDR.x * tmpvar_3.w)
					   * tmpvar_3.xyz) * _Tint.xyz) * unity_ColorSpaceDouble.xyz);
					  c_1 = (c_1 * _Exposure);
					  mediump vec4 tmpvar_4;
					  tmpvar_4.w = 1.0;
					  tmpvar_4.xyz = c_1;
					  gl_FragData[0] = tmpvar_4;
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
  Tags { "QUEUE"="Background" "RenderType"="Background" "PreviewType"="Skybox" }
  ZWrite Off
  Cull Off
  GpuProgramID 327194
Program "vp" {
SubProgram "gles hw_tier01 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp float _Rotation;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  highp float tmpvar_1;
					  tmpvar_1 = ((_Rotation * 3.141593) / 180.0);
					  highp float tmpvar_2;
					  tmpvar_2 = sin(tmpvar_1);
					  highp float tmpvar_3;
					  tmpvar_3 = cos(tmpvar_1);
					  highp mat2 tmpvar_4;
					  tmpvar_4[0].x = tmpvar_3;
					  tmpvar_4[0].y = tmpvar_2;
					  tmpvar_4[1].x = -(tmpvar_2);
					  tmpvar_4[1].y = tmpvar_3;
					  highp vec3 tmpvar_5;
					  tmpvar_5.xy = (tmpvar_4 * _glesVertex.xz);
					  tmpvar_5.z = _glesVertex.y;
					  highp vec4 tmpvar_6;
					  tmpvar_6.w = 1.0;
					  tmpvar_6.xyz = tmpvar_5.xzy;
					  gl_Position = (glstate_matrix_mvp * tmpvar_6);
					  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform lowp vec4 unity_ColorSpaceDouble;
					uniform mediump vec4 _Tint;
					uniform mediump float _Exposure;
					uniform sampler2D _UpTex;
					uniform mediump vec4 _UpTex_HDR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_UpTex, xlv_TEXCOORD0);
					  mediump vec4 tmpvar_3;
					  tmpvar_3 = tmpvar_2;
					  c_1 = (((
					    (_UpTex_HDR.x * tmpvar_3.w)
					   * tmpvar_3.xyz) * _Tint.xyz) * unity_ColorSpaceDouble.xyz);
					  c_1 = (c_1 * _Exposure);
					  mediump vec4 tmpvar_4;
					  tmpvar_4.w = 1.0;
					  tmpvar_4.xyz = c_1;
					  gl_FragData[0] = tmpvar_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp float _Rotation;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  highp float tmpvar_1;
					  tmpvar_1 = ((_Rotation * 3.141593) / 180.0);
					  highp float tmpvar_2;
					  tmpvar_2 = sin(tmpvar_1);
					  highp float tmpvar_3;
					  tmpvar_3 = cos(tmpvar_1);
					  highp mat2 tmpvar_4;
					  tmpvar_4[0].x = tmpvar_3;
					  tmpvar_4[0].y = tmpvar_2;
					  tmpvar_4[1].x = -(tmpvar_2);
					  tmpvar_4[1].y = tmpvar_3;
					  highp vec3 tmpvar_5;
					  tmpvar_5.xy = (tmpvar_4 * _glesVertex.xz);
					  tmpvar_5.z = _glesVertex.y;
					  highp vec4 tmpvar_6;
					  tmpvar_6.w = 1.0;
					  tmpvar_6.xyz = tmpvar_5.xzy;
					  gl_Position = (glstate_matrix_mvp * tmpvar_6);
					  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform lowp vec4 unity_ColorSpaceDouble;
					uniform mediump vec4 _Tint;
					uniform mediump float _Exposure;
					uniform sampler2D _UpTex;
					uniform mediump vec4 _UpTex_HDR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_UpTex, xlv_TEXCOORD0);
					  mediump vec4 tmpvar_3;
					  tmpvar_3 = tmpvar_2;
					  c_1 = (((
					    (_UpTex_HDR.x * tmpvar_3.w)
					   * tmpvar_3.xyz) * _Tint.xyz) * unity_ColorSpaceDouble.xyz);
					  c_1 = (c_1 * _Exposure);
					  mediump vec4 tmpvar_4;
					  tmpvar_4.w = 1.0;
					  tmpvar_4.xyz = c_1;
					  gl_FragData[0] = tmpvar_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp float _Rotation;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  highp float tmpvar_1;
					  tmpvar_1 = ((_Rotation * 3.141593) / 180.0);
					  highp float tmpvar_2;
					  tmpvar_2 = sin(tmpvar_1);
					  highp float tmpvar_3;
					  tmpvar_3 = cos(tmpvar_1);
					  highp mat2 tmpvar_4;
					  tmpvar_4[0].x = tmpvar_3;
					  tmpvar_4[0].y = tmpvar_2;
					  tmpvar_4[1].x = -(tmpvar_2);
					  tmpvar_4[1].y = tmpvar_3;
					  highp vec3 tmpvar_5;
					  tmpvar_5.xy = (tmpvar_4 * _glesVertex.xz);
					  tmpvar_5.z = _glesVertex.y;
					  highp vec4 tmpvar_6;
					  tmpvar_6.w = 1.0;
					  tmpvar_6.xyz = tmpvar_5.xzy;
					  gl_Position = (glstate_matrix_mvp * tmpvar_6);
					  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform lowp vec4 unity_ColorSpaceDouble;
					uniform mediump vec4 _Tint;
					uniform mediump float _Exposure;
					uniform sampler2D _UpTex;
					uniform mediump vec4 _UpTex_HDR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_UpTex, xlv_TEXCOORD0);
					  mediump vec4 tmpvar_3;
					  tmpvar_3 = tmpvar_2;
					  c_1 = (((
					    (_UpTex_HDR.x * tmpvar_3.w)
					   * tmpvar_3.xyz) * _Tint.xyz) * unity_ColorSpaceDouble.xyz);
					  c_1 = (c_1 * _Exposure);
					  mediump vec4 tmpvar_4;
					  tmpvar_4.w = 1.0;
					  tmpvar_4.xyz = c_1;
					  gl_FragData[0] = tmpvar_4;
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
  Tags { "QUEUE"="Background" "RenderType"="Background" "PreviewType"="Skybox" }
  ZWrite Off
  Cull Off
  GpuProgramID 350638
Program "vp" {
SubProgram "gles hw_tier01 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp float _Rotation;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  highp float tmpvar_1;
					  tmpvar_1 = ((_Rotation * 3.141593) / 180.0);
					  highp float tmpvar_2;
					  tmpvar_2 = sin(tmpvar_1);
					  highp float tmpvar_3;
					  tmpvar_3 = cos(tmpvar_1);
					  highp mat2 tmpvar_4;
					  tmpvar_4[0].x = tmpvar_3;
					  tmpvar_4[0].y = tmpvar_2;
					  tmpvar_4[1].x = -(tmpvar_2);
					  tmpvar_4[1].y = tmpvar_3;
					  highp vec3 tmpvar_5;
					  tmpvar_5.xy = (tmpvar_4 * _glesVertex.xz);
					  tmpvar_5.z = _glesVertex.y;
					  highp vec4 tmpvar_6;
					  tmpvar_6.w = 1.0;
					  tmpvar_6.xyz = tmpvar_5.xzy;
					  gl_Position = (glstate_matrix_mvp * tmpvar_6);
					  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform lowp vec4 unity_ColorSpaceDouble;
					uniform mediump vec4 _Tint;
					uniform mediump float _Exposure;
					uniform sampler2D _DownTex;
					uniform mediump vec4 _DownTex_HDR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_DownTex, xlv_TEXCOORD0);
					  mediump vec4 tmpvar_3;
					  tmpvar_3 = tmpvar_2;
					  c_1 = (((
					    (_DownTex_HDR.x * tmpvar_3.w)
					   * tmpvar_3.xyz) * _Tint.xyz) * unity_ColorSpaceDouble.xyz);
					  c_1 = (c_1 * _Exposure);
					  mediump vec4 tmpvar_4;
					  tmpvar_4.w = 1.0;
					  tmpvar_4.xyz = c_1;
					  gl_FragData[0] = tmpvar_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp float _Rotation;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  highp float tmpvar_1;
					  tmpvar_1 = ((_Rotation * 3.141593) / 180.0);
					  highp float tmpvar_2;
					  tmpvar_2 = sin(tmpvar_1);
					  highp float tmpvar_3;
					  tmpvar_3 = cos(tmpvar_1);
					  highp mat2 tmpvar_4;
					  tmpvar_4[0].x = tmpvar_3;
					  tmpvar_4[0].y = tmpvar_2;
					  tmpvar_4[1].x = -(tmpvar_2);
					  tmpvar_4[1].y = tmpvar_3;
					  highp vec3 tmpvar_5;
					  tmpvar_5.xy = (tmpvar_4 * _glesVertex.xz);
					  tmpvar_5.z = _glesVertex.y;
					  highp vec4 tmpvar_6;
					  tmpvar_6.w = 1.0;
					  tmpvar_6.xyz = tmpvar_5.xzy;
					  gl_Position = (glstate_matrix_mvp * tmpvar_6);
					  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform lowp vec4 unity_ColorSpaceDouble;
					uniform mediump vec4 _Tint;
					uniform mediump float _Exposure;
					uniform sampler2D _DownTex;
					uniform mediump vec4 _DownTex_HDR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_DownTex, xlv_TEXCOORD0);
					  mediump vec4 tmpvar_3;
					  tmpvar_3 = tmpvar_2;
					  c_1 = (((
					    (_DownTex_HDR.x * tmpvar_3.w)
					   * tmpvar_3.xyz) * _Tint.xyz) * unity_ColorSpaceDouble.xyz);
					  c_1 = (c_1 * _Exposure);
					  mediump vec4 tmpvar_4;
					  tmpvar_4.w = 1.0;
					  tmpvar_4.xyz = c_1;
					  gl_FragData[0] = tmpvar_4;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp float _Rotation;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  highp float tmpvar_1;
					  tmpvar_1 = ((_Rotation * 3.141593) / 180.0);
					  highp float tmpvar_2;
					  tmpvar_2 = sin(tmpvar_1);
					  highp float tmpvar_3;
					  tmpvar_3 = cos(tmpvar_1);
					  highp mat2 tmpvar_4;
					  tmpvar_4[0].x = tmpvar_3;
					  tmpvar_4[0].y = tmpvar_2;
					  tmpvar_4[1].x = -(tmpvar_2);
					  tmpvar_4[1].y = tmpvar_3;
					  highp vec3 tmpvar_5;
					  tmpvar_5.xy = (tmpvar_4 * _glesVertex.xz);
					  tmpvar_5.z = _glesVertex.y;
					  highp vec4 tmpvar_6;
					  tmpvar_6.w = 1.0;
					  tmpvar_6.xyz = tmpvar_5.xzy;
					  gl_Position = (glstate_matrix_mvp * tmpvar_6);
					  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform lowp vec4 unity_ColorSpaceDouble;
					uniform mediump vec4 _Tint;
					uniform mediump float _Exposure;
					uniform sampler2D _DownTex;
					uniform mediump vec4 _DownTex_HDR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  mediump vec3 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_DownTex, xlv_TEXCOORD0);
					  mediump vec4 tmpvar_3;
					  tmpvar_3 = tmpvar_2;
					  c_1 = (((
					    (_DownTex_HDR.x * tmpvar_3.w)
					   * tmpvar_3.xyz) * _Tint.xyz) * unity_ColorSpaceDouble.xyz);
					  c_1 = (c_1 * _Exposure);
					  mediump vec4 tmpvar_4;
					  tmpvar_4.w = 1.0;
					  tmpvar_4.xyz = c_1;
					  gl_FragData[0] = tmpvar_4;
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