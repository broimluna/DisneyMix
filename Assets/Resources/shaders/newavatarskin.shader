Shader "NewAvatarSkin" {
Properties {
 SkinColor ("Skin Color", Color) = (1.000000,1.000000,1.000000,1.000000)
 SkinXOffset ("Skin Augment Offset X", Float) = 0.000000
 SkinYOffset ("Skin Augment Offset Y", Float) = 0.000000
 SkinBlendMode00 ("Skin Augment Blend Mode", Float) = 0.000000
 CostumeBlendMode00 ("Costume Blend Mode", Float) = 5.000000
 SkinTex00 ("Skin Augment", 2D) = "alpha" { }
 CostumeTex00 ("Costume", 2D) = "alpha" { }
 _MainTex ("Main Texture", 2D) = "white" { }
}
SubShader { 
 Pass {
  GpuProgramID 64999
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
					uniform lowp vec4 SkinColor;
					uniform highp float SkinCrop;
					uniform highp float SkinXOffset;
					uniform highp float SkinYOffset;
					uniform highp int SkinBlendMode00;
					uniform highp int CostumeBlendMode00;
					uniform sampler2D SkinTex00;
					uniform sampler2D CostumeTex00;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 outputColor_1;
					  if ((CostumeBlendMode00 != 5)) {
					    outputColor_1 = texture2D (CostumeTex00, xlv_TEXCOORD0.xy);
					  } else {
					    outputColor_1 = SkinColor;
					    highp vec2 tmpvar_2;
					    tmpvar_2.x = SkinXOffset;
					    tmpvar_2.y = SkinYOffset;
					    highp vec2 tmpvar_3;
					    highp vec2 tmpvar_4;
					    tmpvar_4 = (((
					      (xlv_TEXCOORD0.xy + tmpvar_2)
					     * vec2(SkinCrop)) - vec2(SkinCrop)) + vec2(1.0, 1.0));
					    if (((tmpvar_4.x < 0.0) || (tmpvar_4.y < 0.0))) {
					      tmpvar_3 = vec2(0.0, 0.0);
					    } else {
					      tmpvar_3 = tmpvar_4;
					    };
					    lowp vec4 texColor_5;
					    texColor_5 = texture2D (SkinTex00, tmpvar_3);
					    lowp vec4 tmpvar_6;
					    if ((SkinBlendMode00 == 0)) {
					      tmpvar_6 = mix (SkinColor, texColor_5, texColor_5.wwww);
					    } else {
					      if ((SkinBlendMode00 == 1)) {
					        lowp vec4 tmpvar_7;
					        if ((texColor_5.w == 0.0)) {
					          tmpvar_7 = SkinColor;
					        } else {
					          tmpvar_7 = (texColor_5 * SkinColor);
					        };
					        tmpvar_6 = mix (SkinColor, tmpvar_7, texColor_5.wwww);
					      } else {
					        if ((SkinBlendMode00 == 2)) {
					          tmpvar_6 = mix (SkinColor, (1.0 - (
					            (1.0 - texColor_5)
					           * 
					            (1.0 - SkinColor)
					          )), texColor_5.wwww);
					        } else {
					          tmpvar_6 = SkinColor;
					        };
					      };
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
					uniform lowp vec4 SkinColor;
					uniform highp float SkinCrop;
					uniform highp float SkinXOffset;
					uniform highp float SkinYOffset;
					uniform highp int SkinBlendMode00;
					uniform highp int CostumeBlendMode00;
					uniform sampler2D SkinTex00;
					uniform sampler2D CostumeTex00;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 outputColor_1;
					  if ((CostumeBlendMode00 != 5)) {
					    outputColor_1 = texture2D (CostumeTex00, xlv_TEXCOORD0.xy);
					  } else {
					    outputColor_1 = SkinColor;
					    highp vec2 tmpvar_2;
					    tmpvar_2.x = SkinXOffset;
					    tmpvar_2.y = SkinYOffset;
					    highp vec2 tmpvar_3;
					    highp vec2 tmpvar_4;
					    tmpvar_4 = (((
					      (xlv_TEXCOORD0.xy + tmpvar_2)
					     * vec2(SkinCrop)) - vec2(SkinCrop)) + vec2(1.0, 1.0));
					    if (((tmpvar_4.x < 0.0) || (tmpvar_4.y < 0.0))) {
					      tmpvar_3 = vec2(0.0, 0.0);
					    } else {
					      tmpvar_3 = tmpvar_4;
					    };
					    lowp vec4 texColor_5;
					    texColor_5 = texture2D (SkinTex00, tmpvar_3);
					    lowp vec4 tmpvar_6;
					    if ((SkinBlendMode00 == 0)) {
					      tmpvar_6 = mix (SkinColor, texColor_5, texColor_5.wwww);
					    } else {
					      if ((SkinBlendMode00 == 1)) {
					        lowp vec4 tmpvar_7;
					        if ((texColor_5.w == 0.0)) {
					          tmpvar_7 = SkinColor;
					        } else {
					          tmpvar_7 = (texColor_5 * SkinColor);
					        };
					        tmpvar_6 = mix (SkinColor, tmpvar_7, texColor_5.wwww);
					      } else {
					        if ((SkinBlendMode00 == 2)) {
					          tmpvar_6 = mix (SkinColor, (1.0 - (
					            (1.0 - texColor_5)
					           * 
					            (1.0 - SkinColor)
					          )), texColor_5.wwww);
					        } else {
					          tmpvar_6 = SkinColor;
					        };
					      };
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
					uniform lowp vec4 SkinColor;
					uniform highp float SkinCrop;
					uniform highp float SkinXOffset;
					uniform highp float SkinYOffset;
					uniform highp int SkinBlendMode00;
					uniform highp int CostumeBlendMode00;
					uniform sampler2D SkinTex00;
					uniform sampler2D CostumeTex00;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 outputColor_1;
					  if ((CostumeBlendMode00 != 5)) {
					    outputColor_1 = texture2D (CostumeTex00, xlv_TEXCOORD0.xy);
					  } else {
					    outputColor_1 = SkinColor;
					    highp vec2 tmpvar_2;
					    tmpvar_2.x = SkinXOffset;
					    tmpvar_2.y = SkinYOffset;
					    highp vec2 tmpvar_3;
					    highp vec2 tmpvar_4;
					    tmpvar_4 = (((
					      (xlv_TEXCOORD0.xy + tmpvar_2)
					     * vec2(SkinCrop)) - vec2(SkinCrop)) + vec2(1.0, 1.0));
					    if (((tmpvar_4.x < 0.0) || (tmpvar_4.y < 0.0))) {
					      tmpvar_3 = vec2(0.0, 0.0);
					    } else {
					      tmpvar_3 = tmpvar_4;
					    };
					    lowp vec4 texColor_5;
					    texColor_5 = texture2D (SkinTex00, tmpvar_3);
					    lowp vec4 tmpvar_6;
					    if ((SkinBlendMode00 == 0)) {
					      tmpvar_6 = mix (SkinColor, texColor_5, texColor_5.wwww);
					    } else {
					      if ((SkinBlendMode00 == 1)) {
					        lowp vec4 tmpvar_7;
					        if ((texColor_5.w == 0.0)) {
					          tmpvar_7 = SkinColor;
					        } else {
					          tmpvar_7 = (texColor_5 * SkinColor);
					        };
					        tmpvar_6 = mix (SkinColor, tmpvar_7, texColor_5.wwww);
					      } else {
					        if ((SkinBlendMode00 == 2)) {
					          tmpvar_6 = mix (SkinColor, (1.0 - (
					            (1.0 - texColor_5)
					           * 
					            (1.0 - SkinColor)
					          )), texColor_5.wwww);
					        } else {
					          tmpvar_6 = SkinColor;
					        };
					      };
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