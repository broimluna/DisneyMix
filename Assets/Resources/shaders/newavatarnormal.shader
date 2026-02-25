Shader "NewAvatarNormal" {
Properties {
 NmlColor ("Base Normal Color", Color) = (0.500000,0.500000,1.000000,1.000000)
 AccessoryXOffset ("Accessory Normal Offset X", Float) = 0.000000
 AccessoryYOffset ("Accessory Normal Offset Y", Float) = 0.000000
 HairXOffset ("Hair Normal Offset X", Float) = 0.000000
 HairYOffset ("Hair Normal Offset Y", Float) = 0.000000
 AccessoryBlendMode00 ("Accessory Blend Mode", Float) = 4.000000
 HairBlendMode00 ("Hair Blend Mode", Float) = 4.000000
 AccessoryCrop ("Accessory Crop", Float) = 1.000000
 AccessoryTex00 ("Accessory", 2D) = "white" { }
 HairTex00 ("Hair", 2D) = "white" { }
 _MainTex ("Main Texture", 2D) = "white" { }
}
SubShader { 
 Pass {
  GpuProgramID 17671
Program "vp" {
SubProgram "gles hw_tier01 " {

					//ShaderGLESExporter
					#version 100
					
					#ifdef VERTEX
					attribute vec4 _glesVertex;
					attribute vec4 _glesMultiTexCoord0;
					uniform highp mat4 glstate_matrix_mvp;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  gl_Position = (glstate_matrix_mvp * _glesVertex);
					  xlv_TEXCOORD0 = _glesMultiTexCoord0;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform lowp vec4 NmlColor;
					uniform highp float AccessoryXOffset;
					uniform highp float AccessoryYOffset;
					uniform highp float HairXOffset;
					uniform highp float HairYOffset;
					uniform highp float AccessoryCrop;
					uniform highp int AccessoryBlendMode00;
					uniform highp int HairBlendMode00;
					uniform sampler2D AccessoryTex00;
					uniform sampler2D HairTex00;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  highp vec2 tmpvar_1;
					  tmpvar_1.x = HairXOffset;
					  tmpvar_1.y = HairYOffset;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = (xlv_TEXCOORD0.xy + tmpvar_1);
					  lowp vec4 texColor_3;
					  texColor_3 = texture2D (HairTex00, tmpvar_2);
					  lowp vec4 tmpvar_4;
					  if ((HairBlendMode00 == 0)) {
					    tmpvar_4 = mix (NmlColor, texColor_3, texColor_3.wwww);
					  } else {
					    tmpvar_4 = NmlColor;
					  };
					  highp vec2 tmpvar_5;
					  tmpvar_5.x = AccessoryXOffset;
					  tmpvar_5.y = AccessoryYOffset;
					  highp vec2 tmpvar_6;
					  highp vec2 tmpvar_7;
					  tmpvar_7 = (((
					    (xlv_TEXCOORD0.xy + tmpvar_5)
					   * vec2(AccessoryCrop)) - vec2(AccessoryCrop)) + vec2(1.0, 1.0));
					  if (((tmpvar_7.x < 0.0) || (tmpvar_7.y < 0.0))) {
					    tmpvar_6 = vec2(0.0, 0.0);
					  } else {
					    tmpvar_6 = tmpvar_7;
					  };
					  lowp vec4 tmpvar_8;
					  tmpvar_8 = texture2D (AccessoryTex00, tmpvar_6);
					  lowp vec4 tmpvar_9;
					  if ((AccessoryBlendMode00 == 0)) {
					    tmpvar_9 = mix (tmpvar_4, tmpvar_8, tmpvar_8.wwww);
					  } else {
					    tmpvar_9 = tmpvar_4;
					  };
					  gl_FragData[0] = tmpvar_9;
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
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  gl_Position = (glstate_matrix_mvp * _glesVertex);
					  xlv_TEXCOORD0 = _glesMultiTexCoord0;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform lowp vec4 NmlColor;
					uniform highp float AccessoryXOffset;
					uniform highp float AccessoryYOffset;
					uniform highp float HairXOffset;
					uniform highp float HairYOffset;
					uniform highp float AccessoryCrop;
					uniform highp int AccessoryBlendMode00;
					uniform highp int HairBlendMode00;
					uniform sampler2D AccessoryTex00;
					uniform sampler2D HairTex00;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  highp vec2 tmpvar_1;
					  tmpvar_1.x = HairXOffset;
					  tmpvar_1.y = HairYOffset;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = (xlv_TEXCOORD0.xy + tmpvar_1);
					  lowp vec4 texColor_3;
					  texColor_3 = texture2D (HairTex00, tmpvar_2);
					  lowp vec4 tmpvar_4;
					  if ((HairBlendMode00 == 0)) {
					    tmpvar_4 = mix (NmlColor, texColor_3, texColor_3.wwww);
					  } else {
					    tmpvar_4 = NmlColor;
					  };
					  highp vec2 tmpvar_5;
					  tmpvar_5.x = AccessoryXOffset;
					  tmpvar_5.y = AccessoryYOffset;
					  highp vec2 tmpvar_6;
					  highp vec2 tmpvar_7;
					  tmpvar_7 = (((
					    (xlv_TEXCOORD0.xy + tmpvar_5)
					   * vec2(AccessoryCrop)) - vec2(AccessoryCrop)) + vec2(1.0, 1.0));
					  if (((tmpvar_7.x < 0.0) || (tmpvar_7.y < 0.0))) {
					    tmpvar_6 = vec2(0.0, 0.0);
					  } else {
					    tmpvar_6 = tmpvar_7;
					  };
					  lowp vec4 tmpvar_8;
					  tmpvar_8 = texture2D (AccessoryTex00, tmpvar_6);
					  lowp vec4 tmpvar_9;
					  if ((AccessoryBlendMode00 == 0)) {
					    tmpvar_9 = mix (tmpvar_4, tmpvar_8, tmpvar_8.wwww);
					  } else {
					    tmpvar_9 = tmpvar_4;
					  };
					  gl_FragData[0] = tmpvar_9;
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
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  gl_Position = (glstate_matrix_mvp * _glesVertex);
					  xlv_TEXCOORD0 = _glesMultiTexCoord0;
					}
					
					
					#endif
					#ifdef FRAGMENT
					uniform lowp vec4 NmlColor;
					uniform highp float AccessoryXOffset;
					uniform highp float AccessoryYOffset;
					uniform highp float HairXOffset;
					uniform highp float HairYOffset;
					uniform highp float AccessoryCrop;
					uniform highp int AccessoryBlendMode00;
					uniform highp int HairBlendMode00;
					uniform sampler2D AccessoryTex00;
					uniform sampler2D HairTex00;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  highp vec2 tmpvar_1;
					  tmpvar_1.x = HairXOffset;
					  tmpvar_1.y = HairYOffset;
					  highp vec2 tmpvar_2;
					  tmpvar_2 = (xlv_TEXCOORD0.xy + tmpvar_1);
					  lowp vec4 texColor_3;
					  texColor_3 = texture2D (HairTex00, tmpvar_2);
					  lowp vec4 tmpvar_4;
					  if ((HairBlendMode00 == 0)) {
					    tmpvar_4 = mix (NmlColor, texColor_3, texColor_3.wwww);
					  } else {
					    tmpvar_4 = NmlColor;
					  };
					  highp vec2 tmpvar_5;
					  tmpvar_5.x = AccessoryXOffset;
					  tmpvar_5.y = AccessoryYOffset;
					  highp vec2 tmpvar_6;
					  highp vec2 tmpvar_7;
					  tmpvar_7 = (((
					    (xlv_TEXCOORD0.xy + tmpvar_5)
					   * vec2(AccessoryCrop)) - vec2(AccessoryCrop)) + vec2(1.0, 1.0));
					  if (((tmpvar_7.x < 0.0) || (tmpvar_7.y < 0.0))) {
					    tmpvar_6 = vec2(0.0, 0.0);
					  } else {
					    tmpvar_6 = tmpvar_7;
					  };
					  lowp vec4 tmpvar_8;
					  tmpvar_8 = texture2D (AccessoryTex00, tmpvar_6);
					  lowp vec4 tmpvar_9;
					  if ((AccessoryBlendMode00 == 0)) {
					    tmpvar_9 = mix (tmpvar_4, tmpvar_8, tmpvar_8.wwww);
					  } else {
					    tmpvar_9 = tmpvar_4;
					  };
					  gl_FragData[0] = tmpvar_9;
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