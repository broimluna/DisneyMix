Shader "NewAvatarMouth" {
Properties {
 MouthColor ("Mouth Color", Color) = (1.000000,1.000000,1.000000,1.000000)
 MouthXOffset ("Mouth Offset X", Float) = 0.000000
 MouthYOffset ("Mouth Offset Y", Float) = 0.000000
 MouthBlendMode00 ("Mouth Blend Mode", Float) = 0.000000
 MouthCrop ("Mouth crop", Float) = 1.000000
 MouthTex00 ("Mouth", 2D) = "alpha" { }
 _MainTex ("Main Texture", 2D) = "alpha" { }
}
SubShader { 
 Pass {
  GpuProgramID 57756
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
					uniform lowp vec4 MouthColor;
					uniform highp float MouthCrop;
					uniform highp float MouthXOffset;
					uniform highp float MouthYOffset;
					uniform highp int MouthBlendMode00;
					uniform highp int MouthBlendMode01;
					uniform sampler2D MouthTex00;
					uniform sampler2D MouthTex01;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 outputColor_1;
					  outputColor_1 = vec4(0.0, 0.0, 0.0, 0.0);
					  highp vec2 tmpvar_2;
					  tmpvar_2.x = MouthXOffset;
					  tmpvar_2.y = MouthYOffset;
					  highp vec2 tmpvar_3;
					  highp vec2 tmpvar_4;
					  tmpvar_4 = (((
					    (xlv_TEXCOORD0.xy + tmpvar_2)
					   * vec2(MouthCrop)) - vec2(MouthCrop)) + vec2(1.0, 1.0));
					  if (((tmpvar_4.x < 0.0) || (tmpvar_4.y < 0.0))) {
					    tmpvar_3 = vec2(0.0, 0.0);
					  } else {
					    tmpvar_3 = tmpvar_4;
					  };
					  if ((MouthBlendMode00 < 4)) {
					    if ((MouthBlendMode01 == 5)) {
					      outputColor_1 = texture2D (MouthTex00, tmpvar_3);
					    } else {
					      outputColor_1 = MouthColor;
					      lowp vec4 tmpvar_5;
					      tmpvar_5 = texture2D (MouthTex00, tmpvar_3);
					      lowp vec4 tmpvar_6;
					      tmpvar_6 = texture2D (MouthTex01, tmpvar_3);
					      if (((tmpvar_6.w > 0.01) || (tmpvar_5.w > 0.01))) {
					        if (((tmpvar_6.w > 0.01) && (tmpvar_5.w < 0.01))) {
					          outputColor_1.w = tmpvar_6.w;
					        };
					        lowp vec4 tmpvar_7;
					        if ((MouthBlendMode00 == 0)) {
					          tmpvar_7 = mix (outputColor_1, tmpvar_5, tmpvar_5.wwww);
					        } else {
					          if ((MouthBlendMode00 == 1)) {
					            lowp vec4 tmpvar_8;
					            if ((tmpvar_5.w == 0.0)) {
					              tmpvar_8 = outputColor_1;
					            } else {
					              tmpvar_8 = (tmpvar_5 * outputColor_1);
					            };
					            tmpvar_7 = mix (outputColor_1, tmpvar_8, tmpvar_5.wwww);
					          } else {
					            tmpvar_7 = outputColor_1;
					          };
					        };
					        outputColor_1 = tmpvar_7;
					        lowp vec4 tmpvar_9;
					        if ((MouthBlendMode01 == 1)) {
					          lowp vec4 tmpvar_10;
					          if ((tmpvar_6.w == 0.0)) {
					            tmpvar_10 = tmpvar_7;
					          } else {
					            tmpvar_10 = (tmpvar_6 * tmpvar_7);
					          };
					          tmpvar_9 = mix (tmpvar_7, tmpvar_10, tmpvar_6.wwww);
					        } else {
					          tmpvar_9 = tmpvar_7;
					        };
					        outputColor_1 = tmpvar_9;
					      } else {
					        outputColor_1 = tmpvar_5;
					      };
					    };
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
					uniform lowp vec4 MouthColor;
					uniform highp float MouthCrop;
					uniform highp float MouthXOffset;
					uniform highp float MouthYOffset;
					uniform highp int MouthBlendMode00;
					uniform highp int MouthBlendMode01;
					uniform sampler2D MouthTex00;
					uniform sampler2D MouthTex01;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 outputColor_1;
					  outputColor_1 = vec4(0.0, 0.0, 0.0, 0.0);
					  highp vec2 tmpvar_2;
					  tmpvar_2.x = MouthXOffset;
					  tmpvar_2.y = MouthYOffset;
					  highp vec2 tmpvar_3;
					  highp vec2 tmpvar_4;
					  tmpvar_4 = (((
					    (xlv_TEXCOORD0.xy + tmpvar_2)
					   * vec2(MouthCrop)) - vec2(MouthCrop)) + vec2(1.0, 1.0));
					  if (((tmpvar_4.x < 0.0) || (tmpvar_4.y < 0.0))) {
					    tmpvar_3 = vec2(0.0, 0.0);
					  } else {
					    tmpvar_3 = tmpvar_4;
					  };
					  if ((MouthBlendMode00 < 4)) {
					    if ((MouthBlendMode01 == 5)) {
					      outputColor_1 = texture2D (MouthTex00, tmpvar_3);
					    } else {
					      outputColor_1 = MouthColor;
					      lowp vec4 tmpvar_5;
					      tmpvar_5 = texture2D (MouthTex00, tmpvar_3);
					      lowp vec4 tmpvar_6;
					      tmpvar_6 = texture2D (MouthTex01, tmpvar_3);
					      if (((tmpvar_6.w > 0.01) || (tmpvar_5.w > 0.01))) {
					        if (((tmpvar_6.w > 0.01) && (tmpvar_5.w < 0.01))) {
					          outputColor_1.w = tmpvar_6.w;
					        };
					        lowp vec4 tmpvar_7;
					        if ((MouthBlendMode00 == 0)) {
					          tmpvar_7 = mix (outputColor_1, tmpvar_5, tmpvar_5.wwww);
					        } else {
					          if ((MouthBlendMode00 == 1)) {
					            lowp vec4 tmpvar_8;
					            if ((tmpvar_5.w == 0.0)) {
					              tmpvar_8 = outputColor_1;
					            } else {
					              tmpvar_8 = (tmpvar_5 * outputColor_1);
					            };
					            tmpvar_7 = mix (outputColor_1, tmpvar_8, tmpvar_5.wwww);
					          } else {
					            tmpvar_7 = outputColor_1;
					          };
					        };
					        outputColor_1 = tmpvar_7;
					        lowp vec4 tmpvar_9;
					        if ((MouthBlendMode01 == 1)) {
					          lowp vec4 tmpvar_10;
					          if ((tmpvar_6.w == 0.0)) {
					            tmpvar_10 = tmpvar_7;
					          } else {
					            tmpvar_10 = (tmpvar_6 * tmpvar_7);
					          };
					          tmpvar_9 = mix (tmpvar_7, tmpvar_10, tmpvar_6.wwww);
					        } else {
					          tmpvar_9 = tmpvar_7;
					        };
					        outputColor_1 = tmpvar_9;
					      } else {
					        outputColor_1 = tmpvar_5;
					      };
					    };
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
					uniform lowp vec4 MouthColor;
					uniform highp float MouthCrop;
					uniform highp float MouthXOffset;
					uniform highp float MouthYOffset;
					uniform highp int MouthBlendMode00;
					uniform highp int MouthBlendMode01;
					uniform sampler2D MouthTex00;
					uniform sampler2D MouthTex01;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 outputColor_1;
					  outputColor_1 = vec4(0.0, 0.0, 0.0, 0.0);
					  highp vec2 tmpvar_2;
					  tmpvar_2.x = MouthXOffset;
					  tmpvar_2.y = MouthYOffset;
					  highp vec2 tmpvar_3;
					  highp vec2 tmpvar_4;
					  tmpvar_4 = (((
					    (xlv_TEXCOORD0.xy + tmpvar_2)
					   * vec2(MouthCrop)) - vec2(MouthCrop)) + vec2(1.0, 1.0));
					  if (((tmpvar_4.x < 0.0) || (tmpvar_4.y < 0.0))) {
					    tmpvar_3 = vec2(0.0, 0.0);
					  } else {
					    tmpvar_3 = tmpvar_4;
					  };
					  if ((MouthBlendMode00 < 4)) {
					    if ((MouthBlendMode01 == 5)) {
					      outputColor_1 = texture2D (MouthTex00, tmpvar_3);
					    } else {
					      outputColor_1 = MouthColor;
					      lowp vec4 tmpvar_5;
					      tmpvar_5 = texture2D (MouthTex00, tmpvar_3);
					      lowp vec4 tmpvar_6;
					      tmpvar_6 = texture2D (MouthTex01, tmpvar_3);
					      if (((tmpvar_6.w > 0.01) || (tmpvar_5.w > 0.01))) {
					        if (((tmpvar_6.w > 0.01) && (tmpvar_5.w < 0.01))) {
					          outputColor_1.w = tmpvar_6.w;
					        };
					        lowp vec4 tmpvar_7;
					        if ((MouthBlendMode00 == 0)) {
					          tmpvar_7 = mix (outputColor_1, tmpvar_5, tmpvar_5.wwww);
					        } else {
					          if ((MouthBlendMode00 == 1)) {
					            lowp vec4 tmpvar_8;
					            if ((tmpvar_5.w == 0.0)) {
					              tmpvar_8 = outputColor_1;
					            } else {
					              tmpvar_8 = (tmpvar_5 * outputColor_1);
					            };
					            tmpvar_7 = mix (outputColor_1, tmpvar_8, tmpvar_5.wwww);
					          } else {
					            tmpvar_7 = outputColor_1;
					          };
					        };
					        outputColor_1 = tmpvar_7;
					        lowp vec4 tmpvar_9;
					        if ((MouthBlendMode01 == 1)) {
					          lowp vec4 tmpvar_10;
					          if ((tmpvar_6.w == 0.0)) {
					            tmpvar_10 = tmpvar_7;
					          } else {
					            tmpvar_10 = (tmpvar_6 * tmpvar_7);
					          };
					          tmpvar_9 = mix (tmpvar_7, tmpvar_10, tmpvar_6.wwww);
					        } else {
					          tmpvar_9 = tmpvar_7;
					        };
					        outputColor_1 = tmpvar_9;
					      } else {
					        outputColor_1 = tmpvar_5;
					      };
					    };
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