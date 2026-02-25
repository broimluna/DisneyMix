Shader "Sprites/Default" {
Properties {
[PerRendererData]  _MainTex ("Sprite Texture", 2D) = "white" { }
 _Color ("Tint", Color) = (1.000000,1.000000,1.000000,1.000000)
[MaterialToggle]  PixelSnap ("Pixel snap", Float) = 0.000000
}
SubShader { 
 Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="true" }
 Pass {
  Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="true" }
  ZWrite Off
  Cull Off
  Blend One OneMinusSrcAlpha
  GpuProgramID 35710
Program "vp" {
SubProgram "gles hw_tier01 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesColor;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform lowp vec4 _Color;
					varying lowp vec4 xlv_COLOR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  highp vec4 tmpvar_2;
					  tmpvar_2.w = 1.0;
					  tmpvar_2.xyz = _glesVertex.xyz;
					  tmpvar_1 = (_glesColor * _Color);
					  gl_Position = (glstate_matrix_mvp * tmpvar_2);
					  xlv_COLOR = tmpvar_1;
					  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					varying lowp vec4 xlv_COLOR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = (texture2D (_MainTex, xlv_TEXCOORD0) * xlv_COLOR);
					  c_1.w = tmpvar_2.w;
					  c_1.xyz = (tmpvar_2.xyz * tmpvar_2.w);
					  gl_FragData[0] = c_1;
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
					uniform highp mat4 glstate_matrix_mvp;
					uniform lowp vec4 _Color;
					varying lowp vec4 xlv_COLOR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  highp vec4 tmpvar_2;
					  tmpvar_2.w = 1.0;
					  tmpvar_2.xyz = _glesVertex.xyz;
					  tmpvar_1 = (_glesColor * _Color);
					  gl_Position = (glstate_matrix_mvp * tmpvar_2);
					  xlv_COLOR = tmpvar_1;
					  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					varying lowp vec4 xlv_COLOR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = (texture2D (_MainTex, xlv_TEXCOORD0) * xlv_COLOR);
					  c_1.w = tmpvar_2.w;
					  c_1.xyz = (tmpvar_2.xyz * tmpvar_2.w);
					  gl_FragData[0] = c_1;
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
					uniform highp mat4 glstate_matrix_mvp;
					uniform lowp vec4 _Color;
					varying lowp vec4 xlv_COLOR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  highp vec4 tmpvar_2;
					  tmpvar_2.w = 1.0;
					  tmpvar_2.xyz = _glesVertex.xyz;
					  tmpvar_1 = (_glesColor * _Color);
					  gl_Position = (glstate_matrix_mvp * tmpvar_2);
					  xlv_COLOR = tmpvar_1;
					  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					varying lowp vec4 xlv_COLOR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = (texture2D (_MainTex, xlv_TEXCOORD0) * xlv_COLOR);
					  c_1.w = tmpvar_2.w;
					  c_1.xyz = (tmpvar_2.xyz * tmpvar_2.w);
					  gl_FragData[0] = c_1;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "ETC1_EXTERNAL_ALPHA" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesColor;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform lowp vec4 _Color;
					varying lowp vec4 xlv_COLOR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  highp vec4 tmpvar_2;
					  tmpvar_2.w = 1.0;
					  tmpvar_2.xyz = _glesVertex.xyz;
					  tmpvar_1 = (_glesColor * _Color);
					  gl_Position = (glstate_matrix_mvp * tmpvar_2);
					  xlv_COLOR = tmpvar_1;
					  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					uniform sampler2D _AlphaTex;
					varying lowp vec4 xlv_COLOR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 c_1;
					  lowp vec4 color_2;
					  color_2.xyz = texture2D (_MainTex, xlv_TEXCOORD0).xyz;
					  color_2.w = texture2D (_AlphaTex, xlv_TEXCOORD0).x;
					  lowp vec4 tmpvar_3;
					  tmpvar_3 = (color_2 * xlv_COLOR);
					  c_1.w = tmpvar_3.w;
					  c_1.xyz = (tmpvar_3.xyz * tmpvar_3.w);
					  gl_FragData[0] = c_1;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "ETC1_EXTERNAL_ALPHA" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesColor;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform lowp vec4 _Color;
					varying lowp vec4 xlv_COLOR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  highp vec4 tmpvar_2;
					  tmpvar_2.w = 1.0;
					  tmpvar_2.xyz = _glesVertex.xyz;
					  tmpvar_1 = (_glesColor * _Color);
					  gl_Position = (glstate_matrix_mvp * tmpvar_2);
					  xlv_COLOR = tmpvar_1;
					  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					uniform sampler2D _AlphaTex;
					varying lowp vec4 xlv_COLOR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 c_1;
					  lowp vec4 color_2;
					  color_2.xyz = texture2D (_MainTex, xlv_TEXCOORD0).xyz;
					  color_2.w = texture2D (_AlphaTex, xlv_TEXCOORD0).x;
					  lowp vec4 tmpvar_3;
					  tmpvar_3 = (color_2 * xlv_COLOR);
					  c_1.w = tmpvar_3.w;
					  c_1.xyz = (tmpvar_3.xyz * tmpvar_3.w);
					  gl_FragData[0] = c_1;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "ETC1_EXTERNAL_ALPHA" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesColor;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform lowp vec4 _Color;
					varying lowp vec4 xlv_COLOR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  highp vec4 tmpvar_2;
					  tmpvar_2.w = 1.0;
					  tmpvar_2.xyz = _glesVertex.xyz;
					  tmpvar_1 = (_glesColor * _Color);
					  gl_Position = (glstate_matrix_mvp * tmpvar_2);
					  xlv_COLOR = tmpvar_1;
					  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					uniform sampler2D _AlphaTex;
					varying lowp vec4 xlv_COLOR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 c_1;
					  lowp vec4 color_2;
					  color_2.xyz = texture2D (_MainTex, xlv_TEXCOORD0).xyz;
					  color_2.w = texture2D (_AlphaTex, xlv_TEXCOORD0).x;
					  lowp vec4 tmpvar_3;
					  tmpvar_3 = (color_2 * xlv_COLOR);
					  c_1.w = tmpvar_3.w;
					  c_1.xyz = (tmpvar_3.xyz * tmpvar_3.w);
					  gl_FragData[0] = c_1;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "PIXELSNAP_ON" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesColor;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp vec4 _ScreenParams;
					uniform highp mat4 glstate_matrix_mvp;
					uniform lowp vec4 _Color;
					varying lowp vec4 xlv_COLOR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  highp vec4 tmpvar_2;
					  highp vec4 tmpvar_3;
					  tmpvar_3.w = 1.0;
					  tmpvar_3.xyz = _glesVertex.xyz;
					  tmpvar_2 = (glstate_matrix_mvp * tmpvar_3);
					  tmpvar_1 = (_glesColor * _Color);
					  highp vec4 pos_4;
					  pos_4.zw = tmpvar_2.zw;
					  highp vec2 tmpvar_5;
					  tmpvar_5 = (_ScreenParams.xy * 0.5);
					  pos_4.xy = ((floor(
					    (((tmpvar_2.xy / tmpvar_2.w) * tmpvar_5) + vec2(0.5, 0.5))
					  ) / tmpvar_5) * tmpvar_2.w);
					  gl_Position = pos_4;
					  xlv_COLOR = tmpvar_1;
					  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					varying lowp vec4 xlv_COLOR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = (texture2D (_MainTex, xlv_TEXCOORD0) * xlv_COLOR);
					  c_1.w = tmpvar_2.w;
					  c_1.xyz = (tmpvar_2.xyz * tmpvar_2.w);
					  gl_FragData[0] = c_1;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "PIXELSNAP_ON" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesColor;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp vec4 _ScreenParams;
					uniform highp mat4 glstate_matrix_mvp;
					uniform lowp vec4 _Color;
					varying lowp vec4 xlv_COLOR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  highp vec4 tmpvar_2;
					  highp vec4 tmpvar_3;
					  tmpvar_3.w = 1.0;
					  tmpvar_3.xyz = _glesVertex.xyz;
					  tmpvar_2 = (glstate_matrix_mvp * tmpvar_3);
					  tmpvar_1 = (_glesColor * _Color);
					  highp vec4 pos_4;
					  pos_4.zw = tmpvar_2.zw;
					  highp vec2 tmpvar_5;
					  tmpvar_5 = (_ScreenParams.xy * 0.5);
					  pos_4.xy = ((floor(
					    (((tmpvar_2.xy / tmpvar_2.w) * tmpvar_5) + vec2(0.5, 0.5))
					  ) / tmpvar_5) * tmpvar_2.w);
					  gl_Position = pos_4;
					  xlv_COLOR = tmpvar_1;
					  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					varying lowp vec4 xlv_COLOR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = (texture2D (_MainTex, xlv_TEXCOORD0) * xlv_COLOR);
					  c_1.w = tmpvar_2.w;
					  c_1.xyz = (tmpvar_2.xyz * tmpvar_2.w);
					  gl_FragData[0] = c_1;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "PIXELSNAP_ON" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesColor;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp vec4 _ScreenParams;
					uniform highp mat4 glstate_matrix_mvp;
					uniform lowp vec4 _Color;
					varying lowp vec4 xlv_COLOR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  highp vec4 tmpvar_2;
					  highp vec4 tmpvar_3;
					  tmpvar_3.w = 1.0;
					  tmpvar_3.xyz = _glesVertex.xyz;
					  tmpvar_2 = (glstate_matrix_mvp * tmpvar_3);
					  tmpvar_1 = (_glesColor * _Color);
					  highp vec4 pos_4;
					  pos_4.zw = tmpvar_2.zw;
					  highp vec2 tmpvar_5;
					  tmpvar_5 = (_ScreenParams.xy * 0.5);
					  pos_4.xy = ((floor(
					    (((tmpvar_2.xy / tmpvar_2.w) * tmpvar_5) + vec2(0.5, 0.5))
					  ) / tmpvar_5) * tmpvar_2.w);
					  gl_Position = pos_4;
					  xlv_COLOR = tmpvar_1;
					  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					varying lowp vec4 xlv_COLOR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 c_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = (texture2D (_MainTex, xlv_TEXCOORD0) * xlv_COLOR);
					  c_1.w = tmpvar_2.w;
					  c_1.xyz = (tmpvar_2.xyz * tmpvar_2.w);
					  gl_FragData[0] = c_1;
					}
					
					
					#endif
}
SubProgram "gles hw_tier01 " {
Keywords { "ETC1_EXTERNAL_ALPHA" "PIXELSNAP_ON" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesColor;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp vec4 _ScreenParams;
					uniform highp mat4 glstate_matrix_mvp;
					uniform lowp vec4 _Color;
					varying lowp vec4 xlv_COLOR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  highp vec4 tmpvar_2;
					  highp vec4 tmpvar_3;
					  tmpvar_3.w = 1.0;
					  tmpvar_3.xyz = _glesVertex.xyz;
					  tmpvar_2 = (glstate_matrix_mvp * tmpvar_3);
					  tmpvar_1 = (_glesColor * _Color);
					  highp vec4 pos_4;
					  pos_4.zw = tmpvar_2.zw;
					  highp vec2 tmpvar_5;
					  tmpvar_5 = (_ScreenParams.xy * 0.5);
					  pos_4.xy = ((floor(
					    (((tmpvar_2.xy / tmpvar_2.w) * tmpvar_5) + vec2(0.5, 0.5))
					  ) / tmpvar_5) * tmpvar_2.w);
					  gl_Position = pos_4;
					  xlv_COLOR = tmpvar_1;
					  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					uniform sampler2D _AlphaTex;
					varying lowp vec4 xlv_COLOR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 c_1;
					  lowp vec4 color_2;
					  color_2.xyz = texture2D (_MainTex, xlv_TEXCOORD0).xyz;
					  color_2.w = texture2D (_AlphaTex, xlv_TEXCOORD0).x;
					  lowp vec4 tmpvar_3;
					  tmpvar_3 = (color_2 * xlv_COLOR);
					  c_1.w = tmpvar_3.w;
					  c_1.xyz = (tmpvar_3.xyz * tmpvar_3.w);
					  gl_FragData[0] = c_1;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {
Keywords { "ETC1_EXTERNAL_ALPHA" "PIXELSNAP_ON" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesColor;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp vec4 _ScreenParams;
					uniform highp mat4 glstate_matrix_mvp;
					uniform lowp vec4 _Color;
					varying lowp vec4 xlv_COLOR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  highp vec4 tmpvar_2;
					  highp vec4 tmpvar_3;
					  tmpvar_3.w = 1.0;
					  tmpvar_3.xyz = _glesVertex.xyz;
					  tmpvar_2 = (glstate_matrix_mvp * tmpvar_3);
					  tmpvar_1 = (_glesColor * _Color);
					  highp vec4 pos_4;
					  pos_4.zw = tmpvar_2.zw;
					  highp vec2 tmpvar_5;
					  tmpvar_5 = (_ScreenParams.xy * 0.5);
					  pos_4.xy = ((floor(
					    (((tmpvar_2.xy / tmpvar_2.w) * tmpvar_5) + vec2(0.5, 0.5))
					  ) / tmpvar_5) * tmpvar_2.w);
					  gl_Position = pos_4;
					  xlv_COLOR = tmpvar_1;
					  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					uniform sampler2D _AlphaTex;
					varying lowp vec4 xlv_COLOR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 c_1;
					  lowp vec4 color_2;
					  color_2.xyz = texture2D (_MainTex, xlv_TEXCOORD0).xyz;
					  color_2.w = texture2D (_AlphaTex, xlv_TEXCOORD0).x;
					  lowp vec4 tmpvar_3;
					  tmpvar_3 = (color_2 * xlv_COLOR);
					  c_1.w = tmpvar_3.w;
					  c_1.xyz = (tmpvar_3.xyz * tmpvar_3.w);
					  gl_FragData[0] = c_1;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {
Keywords { "ETC1_EXTERNAL_ALPHA" "PIXELSNAP_ON" }
					
					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesColor;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp vec4 _ScreenParams;
					uniform highp mat4 glstate_matrix_mvp;
					uniform lowp vec4 _Color;
					varying lowp vec4 xlv_COLOR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  highp vec4 tmpvar_2;
					  highp vec4 tmpvar_3;
					  tmpvar_3.w = 1.0;
					  tmpvar_3.xyz = _glesVertex.xyz;
					  tmpvar_2 = (glstate_matrix_mvp * tmpvar_3);
					  tmpvar_1 = (_glesColor * _Color);
					  highp vec4 pos_4;
					  pos_4.zw = tmpvar_2.zw;
					  highp vec2 tmpvar_5;
					  tmpvar_5 = (_ScreenParams.xy * 0.5);
					  pos_4.xy = ((floor(
					    (((tmpvar_2.xy / tmpvar_2.w) * tmpvar_5) + vec2(0.5, 0.5))
					  ) / tmpvar_5) * tmpvar_2.w);
					  gl_Position = pos_4;
					  xlv_COLOR = tmpvar_1;
					  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					uniform sampler2D _AlphaTex;
					varying lowp vec4 xlv_COLOR;
					varying highp vec2 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 c_1;
					  lowp vec4 color_2;
					  color_2.xyz = texture2D (_MainTex, xlv_TEXCOORD0).xyz;
					  color_2.w = texture2D (_AlphaTex, xlv_TEXCOORD0).x;
					  lowp vec4 tmpvar_3;
					  tmpvar_3 = (color_2 * xlv_COLOR);
					  c_1.w = tmpvar_3.w;
					  c_1.xyz = (tmpvar_3.xyz * tmpvar_3.w);
					  gl_FragData[0] = c_1;
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
Keywords { "ETC1_EXTERNAL_ALPHA" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "ETC1_EXTERNAL_ALPHA" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "ETC1_EXTERNAL_ALPHA" }
					
}
SubProgram "gles hw_tier01 " {
Keywords { "PIXELSNAP_ON" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "PIXELSNAP_ON" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "PIXELSNAP_ON" }
					
}
SubProgram "gles hw_tier01 " {
Keywords { "ETC1_EXTERNAL_ALPHA" "PIXELSNAP_ON" }
					
}
SubProgram "gles hw_tier02 " {
Keywords { "ETC1_EXTERNAL_ALPHA" "PIXELSNAP_ON" }
					
}
SubProgram "gles hw_tier03 " {
Keywords { "ETC1_EXTERNAL_ALPHA" "PIXELSNAP_ON" }
					
}
}
 }
}
}