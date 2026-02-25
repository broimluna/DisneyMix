Shader "Custom/MIXQRScannerShader" {
Properties {
 _MainTex ("Texture", 2D) = "white" { }
 _ScannerTex ("Scanner", 2D) = "white" { }
 _ScanOffset ("Scan Offset", Range(0.000000,1.000000)) = 0.500000
 _BandSize ("Band Size", Range(0.000000,1.000000)) = 0.100000
 _ScanColor ("Scan Color", Color) = (1.000000,1.000000,1.000000,1.000000)
}
SubShader { 
 LOD 100
 Tags { "RenderType"="Opaque" }
 Pass {
  Tags { "RenderType"="Opaque" }
  GpuProgramID 9825
Program "vp" {
SubProgram "gles hw_tier01 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _ScannerTex_ST;
					uniform highp mat4 _Rotation;
					uniform highp int _Invert;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					void main ()
					{
					  highp vec2 tmpvar_1;
					  highp vec4 tmpvar_2;
					  highp vec2 tmpvar_3;
					  tmpvar_2 = (glstate_matrix_mvp * (_Rotation * _glesVertex));
					  tmpvar_1 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  tmpvar_3 = ((_glesMultiTexCoord1.xy * _ScannerTex_ST.xy) + _ScannerTex_ST.zw);
					  if ((_Invert == 1)) {
					    tmpvar_1.x = (1.0 - tmpvar_1.x);
					  };
					  xlv_TEXCOORD0 = tmpvar_1;
					  gl_Position = tmpvar_2;
					  xlv_TEXCOORD1 = tmpvar_3;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					uniform sampler2D _ScannerTex;
					uniform highp float _ScanOffset;
					uniform highp float _BandSize;
					uniform lowp vec4 _ScanColor;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					void main ()
					{
					  lowp vec4 col_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0);
					  col_1.w = tmpvar_2.w;
					  lowp vec4 tmpvar_3;
					  tmpvar_3 = texture2D (_ScannerTex, xlv_TEXCOORD1);
					  highp float tmpvar_4;
					  highp vec4 col_5;
					  col_5 = tmpvar_2;
					  tmpvar_4 = (((
					    (col_5.x * 2.0)
					   + col_5.z) + (col_5.y * 3.0)) / 6.0);
					  highp float tmpvar_6;
					  tmpvar_6 = min (1.0, (tmpvar_2.x + (_ScanColor.x * 
					    (1.0 - clamp ((abs(
					      (((tmpvar_3.x + tmpvar_4) / 2.0) - _ScanOffset)
					    ) / _BandSize), 0.0, 1.0))
					  )));
					  col_1.x = tmpvar_6;
					  highp float tmpvar_7;
					  tmpvar_7 = min (1.0, (tmpvar_2.y + (_ScanColor.y * 
					    (1.0 - clamp ((abs(
					      (((tmpvar_3.y + tmpvar_4) / 2.0) - _ScanOffset)
					    ) / _BandSize), 0.0, 1.0))
					  )));
					  col_1.y = tmpvar_7;
					  highp float tmpvar_8;
					  tmpvar_8 = min (1.0, (tmpvar_2.z + (_ScanColor.z * 
					    (1.0 - clamp ((abs(
					      (((tmpvar_3.z + tmpvar_4) / 2.0) - _ScanOffset)
					    ) / _BandSize), 0.0, 1.0))
					  )));
					  col_1.z = tmpvar_8;
					  gl_FragData[0] = col_1;
					}
					
					
					#endif
}
SubProgram "gles hw_tier02 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _ScannerTex_ST;
					uniform highp mat4 _Rotation;
					uniform highp int _Invert;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					void main ()
					{
					  highp vec2 tmpvar_1;
					  highp vec4 tmpvar_2;
					  highp vec2 tmpvar_3;
					  tmpvar_2 = (glstate_matrix_mvp * (_Rotation * _glesVertex));
					  tmpvar_1 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  tmpvar_3 = ((_glesMultiTexCoord1.xy * _ScannerTex_ST.xy) + _ScannerTex_ST.zw);
					  if ((_Invert == 1)) {
					    tmpvar_1.x = (1.0 - tmpvar_1.x);
					  };
					  xlv_TEXCOORD0 = tmpvar_1;
					  gl_Position = tmpvar_2;
					  xlv_TEXCOORD1 = tmpvar_3;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					uniform sampler2D _ScannerTex;
					uniform highp float _ScanOffset;
					uniform highp float _BandSize;
					uniform lowp vec4 _ScanColor;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					void main ()
					{
					  lowp vec4 col_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0);
					  col_1.w = tmpvar_2.w;
					  lowp vec4 tmpvar_3;
					  tmpvar_3 = texture2D (_ScannerTex, xlv_TEXCOORD1);
					  highp float tmpvar_4;
					  highp vec4 col_5;
					  col_5 = tmpvar_2;
					  tmpvar_4 = (((
					    (col_5.x * 2.0)
					   + col_5.z) + (col_5.y * 3.0)) / 6.0);
					  highp float tmpvar_6;
					  tmpvar_6 = min (1.0, (tmpvar_2.x + (_ScanColor.x * 
					    (1.0 - clamp ((abs(
					      (((tmpvar_3.x + tmpvar_4) / 2.0) - _ScanOffset)
					    ) / _BandSize), 0.0, 1.0))
					  )));
					  col_1.x = tmpvar_6;
					  highp float tmpvar_7;
					  tmpvar_7 = min (1.0, (tmpvar_2.y + (_ScanColor.y * 
					    (1.0 - clamp ((abs(
					      (((tmpvar_3.y + tmpvar_4) / 2.0) - _ScanOffset)
					    ) / _BandSize), 0.0, 1.0))
					  )));
					  col_1.y = tmpvar_7;
					  highp float tmpvar_8;
					  tmpvar_8 = min (1.0, (tmpvar_2.z + (_ScanColor.z * 
					    (1.0 - clamp ((abs(
					      (((tmpvar_3.z + tmpvar_4) / 2.0) - _ScanOffset)
					    ) / _BandSize), 0.0, 1.0))
					  )));
					  col_1.z = tmpvar_8;
					  gl_FragData[0] = col_1;
					}
					
					
					#endif
}
SubProgram "gles hw_tier03 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesMultiTexCoord0;
					attribute vec4 _glesMultiTexCoord1;
					uniform highp mat4 glstate_matrix_mvp;
					uniform highp vec4 _MainTex_ST;
					uniform highp vec4 _ScannerTex_ST;
					uniform highp mat4 _Rotation;
					uniform highp int _Invert;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					void main ()
					{
					  highp vec2 tmpvar_1;
					  highp vec4 tmpvar_2;
					  highp vec2 tmpvar_3;
					  tmpvar_2 = (glstate_matrix_mvp * (_Rotation * _glesVertex));
					  tmpvar_1 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
					  tmpvar_3 = ((_glesMultiTexCoord1.xy * _ScannerTex_ST.xy) + _ScannerTex_ST.zw);
					  if ((_Invert == 1)) {
					    tmpvar_1.x = (1.0 - tmpvar_1.x);
					  };
					  xlv_TEXCOORD0 = tmpvar_1;
					  gl_Position = tmpvar_2;
					  xlv_TEXCOORD1 = tmpvar_3;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform sampler2D _MainTex;
					uniform sampler2D _ScannerTex;
					uniform highp float _ScanOffset;
					uniform highp float _BandSize;
					uniform lowp vec4 _ScanColor;
					varying highp vec2 xlv_TEXCOORD0;
					varying highp vec2 xlv_TEXCOORD1;
					void main ()
					{
					  lowp vec4 col_1;
					  lowp vec4 tmpvar_2;
					  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0);
					  col_1.w = tmpvar_2.w;
					  lowp vec4 tmpvar_3;
					  tmpvar_3 = texture2D (_ScannerTex, xlv_TEXCOORD1);
					  highp float tmpvar_4;
					  highp vec4 col_5;
					  col_5 = tmpvar_2;
					  tmpvar_4 = (((
					    (col_5.x * 2.0)
					   + col_5.z) + (col_5.y * 3.0)) / 6.0);
					  highp float tmpvar_6;
					  tmpvar_6 = min (1.0, (tmpvar_2.x + (_ScanColor.x * 
					    (1.0 - clamp ((abs(
					      (((tmpvar_3.x + tmpvar_4) / 2.0) - _ScanOffset)
					    ) / _BandSize), 0.0, 1.0))
					  )));
					  col_1.x = tmpvar_6;
					  highp float tmpvar_7;
					  tmpvar_7 = min (1.0, (tmpvar_2.y + (_ScanColor.y * 
					    (1.0 - clamp ((abs(
					      (((tmpvar_3.y + tmpvar_4) / 2.0) - _ScanOffset)
					    ) / _BandSize), 0.0, 1.0))
					  )));
					  col_1.y = tmpvar_7;
					  highp float tmpvar_8;
					  tmpvar_8 = min (1.0, (tmpvar_2.z + (_ScanColor.z * 
					    (1.0 - clamp ((abs(
					      (((tmpvar_3.z + tmpvar_4) / 2.0) - _ScanOffset)
					    ) / _BandSize), 0.0, 1.0))
					  )));
					  col_1.z = tmpvar_8;
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