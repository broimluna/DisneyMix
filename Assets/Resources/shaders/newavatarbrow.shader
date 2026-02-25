Shader "NewAvatarBrow" {
Properties {
 BrowColor ("Brow Color", Color) = (1.000000,1.000000,1.000000,1.000000)
 BrowXOffset ("Eyebrows X Offset", Float) = 0.000000
 BrowYOffset ("Eyebrows Y Offset", Float) = 0.000000
 BrowBlendMode00 ("Brow Blend Mode", Float) = 0.000000
 BrowCrop ("Brow crop", Float) = 1.000000
 BrowTex00 ("Eyebrows", 2D) = "alpha" { }
 _MainTex ("Main Texture", 2D) = "alpha" { }
}
SubShader { 
 Pass {
  GpuProgramID 42393
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
					uniform highp float BrowCrop;
					uniform highp float BrowXOffset;
					uniform highp float BrowYOffset;
					uniform lowp vec4 BrowColor;
					uniform highp int BrowBlendMode00;
					uniform sampler2D BrowTex00;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 outputColor_1;
					  outputColor_1 = vec4(0.0, 0.0, 0.0, 0.0);
					  highp vec2 tmpvar_2;
					  tmpvar_2.x = BrowXOffset;
					  tmpvar_2.y = BrowYOffset;
					  highp vec2 tmpvar_3;
					  highp vec2 tmpvar_4;
					  tmpvar_4 = (((
					    (xlv_TEXCOORD0.xy + tmpvar_2)
					   * vec2(BrowCrop)) - vec2(BrowCrop)) + vec2(1.0, 1.0));
					  if (((tmpvar_4.x < 0.0) || (tmpvar_4.y < 0.0))) {
					    tmpvar_3 = vec2(0.0, 0.0);
					  } else {
					    tmpvar_3 = tmpvar_4;
					  };
					  lowp vec4 tmpvar_5;
					  tmpvar_5 = texture2D (BrowTex00, tmpvar_3);
					  if ((BrowBlendMode00 == 3)) {
					    outputColor_1 = tmpvar_5;
					  } else {
					    lowp vec4 tmpvar_6;
					    if ((BrowBlendMode00 == 0)) {
					      lowp vec4 texColor_7;
					      texColor_7.w = tmpvar_5.w;
					      texColor_7.xyz = (tmpvar_5.xyz * BrowColor.xyz);
					      tmpvar_6 = mix (outputColor_1, texColor_7, tmpvar_5.wwww);
					    } else {
					      tmpvar_6 = outputColor_1;
					    };
					    outputColor_1 = tmpvar_6;
					  };
					  gl_FragData[0] = outputColor_1;
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
					uniform highp float BrowCrop;
					uniform highp float BrowXOffset;
					uniform highp float BrowYOffset;
					uniform lowp vec4 BrowColor;
					uniform highp int BrowBlendMode00;
					uniform sampler2D BrowTex00;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 outputColor_1;
					  outputColor_1 = vec4(0.0, 0.0, 0.0, 0.0);
					  highp vec2 tmpvar_2;
					  tmpvar_2.x = BrowXOffset;
					  tmpvar_2.y = BrowYOffset;
					  highp vec2 tmpvar_3;
					  highp vec2 tmpvar_4;
					  tmpvar_4 = (((
					    (xlv_TEXCOORD0.xy + tmpvar_2)
					   * vec2(BrowCrop)) - vec2(BrowCrop)) + vec2(1.0, 1.0));
					  if (((tmpvar_4.x < 0.0) || (tmpvar_4.y < 0.0))) {
					    tmpvar_3 = vec2(0.0, 0.0);
					  } else {
					    tmpvar_3 = tmpvar_4;
					  };
					  lowp vec4 tmpvar_5;
					  tmpvar_5 = texture2D (BrowTex00, tmpvar_3);
					  if ((BrowBlendMode00 == 3)) {
					    outputColor_1 = tmpvar_5;
					  } else {
					    lowp vec4 tmpvar_6;
					    if ((BrowBlendMode00 == 0)) {
					      lowp vec4 texColor_7;
					      texColor_7.w = tmpvar_5.w;
					      texColor_7.xyz = (tmpvar_5.xyz * BrowColor.xyz);
					      tmpvar_6 = mix (outputColor_1, texColor_7, tmpvar_5.wwww);
					    } else {
					      tmpvar_6 = outputColor_1;
					    };
					    outputColor_1 = tmpvar_6;
					  };
					  gl_FragData[0] = outputColor_1;
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
					uniform highp float BrowCrop;
					uniform highp float BrowXOffset;
					uniform highp float BrowYOffset;
					uniform lowp vec4 BrowColor;
					uniform highp int BrowBlendMode00;
					uniform sampler2D BrowTex00;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 outputColor_1;
					  outputColor_1 = vec4(0.0, 0.0, 0.0, 0.0);
					  highp vec2 tmpvar_2;
					  tmpvar_2.x = BrowXOffset;
					  tmpvar_2.y = BrowYOffset;
					  highp vec2 tmpvar_3;
					  highp vec2 tmpvar_4;
					  tmpvar_4 = (((
					    (xlv_TEXCOORD0.xy + tmpvar_2)
					   * vec2(BrowCrop)) - vec2(BrowCrop)) + vec2(1.0, 1.0));
					  if (((tmpvar_4.x < 0.0) || (tmpvar_4.y < 0.0))) {
					    tmpvar_3 = vec2(0.0, 0.0);
					  } else {
					    tmpvar_3 = tmpvar_4;
					  };
					  lowp vec4 tmpvar_5;
					  tmpvar_5 = texture2D (BrowTex00, tmpvar_3);
					  if ((BrowBlendMode00 == 3)) {
					    outputColor_1 = tmpvar_5;
					  } else {
					    lowp vec4 tmpvar_6;
					    if ((BrowBlendMode00 == 0)) {
					      lowp vec4 texColor_7;
					      texColor_7.w = tmpvar_5.w;
					      texColor_7.xyz = (tmpvar_5.xyz * BrowColor.xyz);
					      tmpvar_6 = mix (outputColor_1, texColor_7, tmpvar_5.wwww);
					    } else {
					      tmpvar_6 = outputColor_1;
					    };
					    outputColor_1 = tmpvar_6;
					  };
					  gl_FragData[0] = outputColor_1;
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