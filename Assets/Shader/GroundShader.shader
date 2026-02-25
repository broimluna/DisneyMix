Shader "Custom/Drop/GroundShader" {
Properties {
 _MainTex ("Main", 2D) = "white" { }
 _SnowTex ("Snow", 2D) = "white" { }
 _RippleMaskTex ("Snow Ripple", 2D) = "white" { }
 _MaskTex ("Mask", 2D) = "black" { }
 _Min ("Min", Range(0.000000,1.000000)) = 0.000000
 _Mid ("Mid", Range(0.000000,1.000000)) = 0.500000
 _Max ("Max", Range(0.000000,1.000000)) = 1.000000
}
SubShader { 
 LOD 100
 Tags { "RenderType"="Opaque" }
 Pass {
  Tags { "RenderType"="Opaque" }
  GpuProgramID 37850
Program "vp" {
SubProgram "gles hw_tier01 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _RippleMaskTex_ST;
					uniform highp vec4 _MaskTex_ST;
					uniform highp vec4 _SnowTex_ST;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec2 xlv_TEXCOORD2;
					varying highp vec2 xlv_TEXCOORD3;
					void main ()
					{
					  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  xlv_TEXCOORD1 = ((_glesMultiTexCoord0.xy * _RippleMaskTex_ST.xy) + _RippleMaskTex_ST.zw);
					  xlv_TEXCOORD2 = ((_glesMultiTexCoord0.xy * _MaskTex_ST.xy) + _MaskTex_ST.zw);
					  xlv_TEXCOORD3 = ((_glesMultiTexCoord0.xy * _SnowTex_ST.xy) + _SnowTex_ST.zw);
					  gl_Position = (glstate_matrix_mvp * _glesVertex);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					uniform sampler2D _RippleMaskTex;
					uniform sampler2D _MaskTex;
					uniform sampler2D _SnowTex;
					uniform highp float _Min;
					uniform highp float _Mid;
					uniform highp float _Max;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec2 xlv_TEXCOORD2;
					varying highp vec2 xlv_TEXCOORD3;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0);
					  lowp vec4 tmpvar_3;
					  tmpvar_3 = texture2D (_RippleMaskTex, xlv_TEXCOORD1);
					  lowp vec4 tmpvar_4;
					  tmpvar_4 = texture2D (_MaskTex, xlv_TEXCOORD2);
					  lowp vec4 tmpvar_5;
					  tmpvar_5 = texture2D (_SnowTex, xlv_TEXCOORD3);
					  highp vec4 tmpvar_6;
					  tmpvar_6 = mix (tmpvar_2, tmpvar_5, vec4(min (1.0, (
					    ((max (0.0, (tmpvar_4.x - _Min)) / (_Mid - _Min)) * tmpvar_3)
					  .x + 
					    (max (0.0, (tmpvar_4.x - _Mid)) / (_Max - _Mid))
					  ))));
					  tmpvar_1 = tmpvar_6;
					  gl_FragData[0] = tmpvar_1;
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
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _RippleMaskTex_ST;
					uniform highp vec4 _MaskTex_ST;
					uniform highp vec4 _SnowTex_ST;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec2 xlv_TEXCOORD2;
					varying highp vec2 xlv_TEXCOORD3;
					void main ()
					{
					  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  xlv_TEXCOORD1 = ((_glesMultiTexCoord0.xy * _RippleMaskTex_ST.xy) + _RippleMaskTex_ST.zw);
					  xlv_TEXCOORD2 = ((_glesMultiTexCoord0.xy * _MaskTex_ST.xy) + _MaskTex_ST.zw);
					  xlv_TEXCOORD3 = ((_glesMultiTexCoord0.xy * _SnowTex_ST.xy) + _SnowTex_ST.zw);
					  gl_Position = (glstate_matrix_mvp * _glesVertex);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					uniform sampler2D _RippleMaskTex;
					uniform sampler2D _MaskTex;
					uniform sampler2D _SnowTex;
					uniform highp float _Min;
					uniform highp float _Mid;
					uniform highp float _Max;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec2 xlv_TEXCOORD2;
					varying highp vec2 xlv_TEXCOORD3;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0);
					  lowp vec4 tmpvar_3;
					  tmpvar_3 = texture2D (_RippleMaskTex, xlv_TEXCOORD1);
					  lowp vec4 tmpvar_4;
					  tmpvar_4 = texture2D (_MaskTex, xlv_TEXCOORD2);
					  lowp vec4 tmpvar_5;
					  tmpvar_5 = texture2D (_SnowTex, xlv_TEXCOORD3);
					  highp vec4 tmpvar_6;
					  tmpvar_6 = mix (tmpvar_2, tmpvar_5, vec4(min (1.0, (
					    ((max (0.0, (tmpvar_4.x - _Min)) / (_Mid - _Min)) * tmpvar_3)
					  .x + 
					    (max (0.0, (tmpvar_4.x - _Mid)) / (_Max - _Mid))
					  ))));
					  tmpvar_1 = tmpvar_6;
					  gl_FragData[0] = tmpvar_1;
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
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _RippleMaskTex_ST;
					uniform highp vec4 _MaskTex_ST;
					uniform highp vec4 _SnowTex_ST;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec2 xlv_TEXCOORD2;
					varying highp vec2 xlv_TEXCOORD3;
					void main ()
					{
					  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  xlv_TEXCOORD1 = ((_glesMultiTexCoord0.xy * _RippleMaskTex_ST.xy) + _RippleMaskTex_ST.zw);
					  xlv_TEXCOORD2 = ((_glesMultiTexCoord0.xy * _MaskTex_ST.xy) + _MaskTex_ST.zw);
					  xlv_TEXCOORD3 = ((_glesMultiTexCoord0.xy * _SnowTex_ST.xy) + _SnowTex_ST.zw);
					  gl_Position = (glstate_matrix_mvp * _glesVertex);
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					uniform sampler2D _RippleMaskTex;
					uniform sampler2D _MaskTex;
					uniform sampler2D _SnowTex;
					uniform highp float _Min;
					uniform highp float _Mid;
					uniform highp float _Max;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					varying highp vec2 xlv_TEXCOORD2;
					varying highp vec2 xlv_TEXCOORD3;
					void main ()
					{
					  lowp vec4 tmpvar_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0);
					  lowp vec4 tmpvar_3;
					  tmpvar_3 = texture2D (_RippleMaskTex, xlv_TEXCOORD1);
					  lowp vec4 tmpvar_4;
					  tmpvar_4 = texture2D (_MaskTex, xlv_TEXCOORD2);
					  lowp vec4 tmpvar_5;
					  tmpvar_5 = texture2D (_SnowTex, xlv_TEXCOORD3);
					  highp vec4 tmpvar_6;
					  tmpvar_6 = mix (tmpvar_2, tmpvar_5, vec4(min (1.0, (
					    ((max (0.0, (tmpvar_4.x - _Min)) / (_Mid - _Min)) * tmpvar_3)
					  .x + 
					    (max (0.0, (tmpvar_4.x - _Mid)) / (_Max - _Mid))
					  ))));
					  tmpvar_1 = tmpvar_6;
					  gl_FragData[0] = tmpvar_1;
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