Shader "NewAvatarHair" {
Properties {
 HairColor ("Hair Color", Color) = (1.000000,1.000000,1.000000,1.000000)
 SkinColor ("Skin Color", Color) = (1.000000,1.000000,1.000000,1.000000)
 SkinXOffset ("Skin Augment Offset X", Float) = 0.000000
 SkinYOffset ("Skin Augment Offset Y", Float) = 0.000000
 NoseXOffset ("Nose Offset X", Float) = 0.000000
 NoseYOffset ("Nose Offset Y", Float) = 0.000000
 HairXOffset ("Hair X Offset", Float) = 0.000000
 HairYOffset ("Hair Y Offset", Float) = 0.000000
 SkinBlendMode00 ("Skin Augment Blend Mode", Float) = 0.000000
 CostumeBlendMode00 ("Costume Blend Mode", Float) = 5.000000
 NoseBlendMode00 ("Nose Blend Mode", Float) = 0.000000
 NoseBlendMode01 ("Nose Screen Blend Mode", Float) = 0.000000
 HairBlendMode00 ("Hair Blend Mode", Float) = 0.000000
 HairBlendMode01 ("Hair Highlight Blend Mode", Float) = 0.000000
 HairBlendMode02 ("Hair Shadow Blend Mode", Float) = 0.000000
 NoseCrop ("Nose crop", Float) = 1.000000
 SkinTex00 ("Skin Augment", 2D) = "alpha" { }
 CostumeTex00 ("Costume", 2D) = "alpha" { }
 NoseTex00 ("Nose", 2D) = "alpha" { }
 NoseTex01 ("Nose Screen", 2D) = "alpha" { }
 HairTex00 ("Hair", 2D) = "alpha" { }
 HairTex01 ("Hair Highlights", 2D) = "alpha" { }
 HairTex02 ("Hair Shadow", 2D) = "alpha" { }
 _MainTex ("Main Texture", 2D) = "alpha" { }
}
SubShader { 
 Pass {
  GpuProgramID 60101
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
					uniform highp float NoseCrop;
					uniform highp float SkinCrop;
					uniform highp float SkinXOffset;
					uniform highp float SkinYOffset;
					uniform highp float NoseXOffset;
					uniform highp float NoseYOffset;
					uniform highp float HairXOffset;
					uniform highp float HairYOffset;
					uniform lowp vec4 SkinColor;
					uniform lowp vec4 HairColor;
					uniform highp int SkinBlendMode00;
					uniform highp int CostumeBlendMode00;
					uniform highp int NoseBlendMode00;
					uniform highp int HairBlendMode00;
					uniform highp int HairBlendMode01;
					uniform highp int HairBlendMode02;
					uniform sampler2D SkinTex00;
					uniform sampler2D CostumeTex00;
					uniform sampler2D NoseTex00;
					uniform sampler2D NoseTex01;
					uniform sampler2D HairTex00;
					uniform sampler2D HairTex01;
					uniform sampler2D HairTex02;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 outputColor_1;
					  outputColor_1 = SkinColor;
					  if ((CostumeBlendMode00 != 5)) {
					    outputColor_1 = texture2D (CostumeTex00, xlv_TEXCOORD0.xy);
					  } else {
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
					      tmpvar_6 = mix (outputColor_1, texColor_5, texColor_5.wwww);
					    } else {
					      if ((SkinBlendMode00 == 1)) {
					        lowp vec4 tmpvar_7;
					        if ((texColor_5.w == 0.0)) {
					          tmpvar_7 = outputColor_1;
					        } else {
					          tmpvar_7 = (texColor_5 * outputColor_1);
					        };
					        tmpvar_6 = mix (outputColor_1, tmpvar_7, texColor_5.wwww);
					      } else {
					        if ((SkinBlendMode00 == 2)) {
					          tmpvar_6 = mix (outputColor_1, (1.0 - (
					            (1.0 - texColor_5)
					           * 
					            (1.0 - outputColor_1)
					          )), texColor_5.wwww);
					        } else {
					          tmpvar_6 = outputColor_1;
					        };
					      };
					    };
					    outputColor_1 = tmpvar_6;
					    if ((NoseBlendMode00 != 5)) {
					      highp vec2 tmpvar_8;
					      tmpvar_8.x = NoseXOffset;
					      tmpvar_8.y = NoseYOffset;
					      highp vec2 tmpvar_9;
					      highp vec2 tmpvar_10;
					      tmpvar_10 = (((
					        (xlv_TEXCOORD0.xy + tmpvar_8)
					       * vec2(NoseCrop)) - vec2(NoseCrop)) + vec2(1.0, 1.0));
					      if (((tmpvar_10.x < 0.0) || (tmpvar_10.y < 0.0))) {
					        tmpvar_9 = vec2(0.0, 0.0);
					      } else {
					        tmpvar_9 = tmpvar_10;
					      };
					      lowp vec4 tmpvar_11;
					      tmpvar_11 = texture2D (NoseTex00, tmpvar_9);
					      lowp vec4 tmpvar_12;
					      if ((NoseBlendMode00 == 0)) {
					        tmpvar_12 = mix (tmpvar_6, tmpvar_11, tmpvar_11.wwww);
					      } else {
					        if ((NoseBlendMode00 == 1)) {
					          lowp vec4 tmpvar_13;
					          if ((tmpvar_11.w == 0.0)) {
					            tmpvar_13 = tmpvar_6;
					          } else {
					            tmpvar_13 = (tmpvar_11 * tmpvar_6);
					          };
					          tmpvar_12 = mix (tmpvar_6, tmpvar_13, tmpvar_11.wwww);
					        } else {
					          tmpvar_12 = tmpvar_6;
					        };
					      };
					      lowp vec4 tmpvar_14;
					      tmpvar_14 = texture2D (NoseTex01, tmpvar_9);
					      outputColor_1 = mix (tmpvar_12, (1.0 - (
					        (1.0 - tmpvar_14)
					       * 
					        (1.0 - tmpvar_12)
					      )), tmpvar_14.wwww);
					    };
					    highp vec2 tmpvar_15;
					    tmpvar_15.x = HairXOffset;
					    tmpvar_15.y = HairYOffset;
					    highp vec2 tmpvar_16;
					    tmpvar_16 = (xlv_TEXCOORD0.xy + tmpvar_15);
					    lowp vec4 tmpvar_17;
					    tmpvar_17 = texture2D (HairTex00, tmpvar_16);
					    if ((HairBlendMode01 != 5)) {
					      lowp vec4 tmpvar_18;
					      if ((HairBlendMode00 == 0)) {
					        lowp vec4 texColor_19;
					        texColor_19.w = tmpvar_17.w;
					        texColor_19.xyz = (tmpvar_17.xyz * HairColor.xyz);
					        tmpvar_18 = mix (outputColor_1, texColor_19, tmpvar_17.wwww);
					      } else {
					        tmpvar_18 = outputColor_1;
					      };
					      outputColor_1 = tmpvar_18;
					      lowp vec4 tmpvar_20;
					      tmpvar_20 = texture2D (HairTex01, tmpvar_16);
					      lowp vec4 tmpvar_21;
					      if ((HairBlendMode01 == 2)) {
					        tmpvar_21 = mix (tmpvar_18, (1.0 - (
					          (1.0 - tmpvar_20)
					         * 
					          (1.0 - tmpvar_18)
					        )), tmpvar_20.wwww);
					      } else {
					        tmpvar_21 = tmpvar_18;
					      };
					      outputColor_1 = tmpvar_21;
					      lowp vec4 tmpvar_22;
					      tmpvar_22 = texture2D (HairTex02, tmpvar_16);
					      lowp vec4 tmpvar_23;
					      if ((HairBlendMode02 == 1)) {
					        lowp vec4 tmpvar_24;
					        if ((tmpvar_22.w == 0.0)) {
					          tmpvar_24 = tmpvar_21;
					        } else {
					          tmpvar_24 = (tmpvar_22 * tmpvar_21);
					        };
					        tmpvar_23 = mix (tmpvar_21, tmpvar_24, tmpvar_22.wwww);
					      } else {
					        tmpvar_23 = tmpvar_21;
					      };
					      outputColor_1 = tmpvar_23;
					    } else {
					      if ((HairBlendMode00 != 5)) {
					        outputColor_1 = texture2D (HairTex00, tmpvar_16);
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
					uniform highp float NoseCrop;
					uniform highp float SkinCrop;
					uniform highp float SkinXOffset;
					uniform highp float SkinYOffset;
					uniform highp float NoseXOffset;
					uniform highp float NoseYOffset;
					uniform highp float HairXOffset;
					uniform highp float HairYOffset;
					uniform lowp vec4 SkinColor;
					uniform lowp vec4 HairColor;
					uniform highp int SkinBlendMode00;
					uniform highp int CostumeBlendMode00;
					uniform highp int NoseBlendMode00;
					uniform highp int HairBlendMode00;
					uniform highp int HairBlendMode01;
					uniform highp int HairBlendMode02;
					uniform sampler2D SkinTex00;
					uniform sampler2D CostumeTex00;
					uniform sampler2D NoseTex00;
					uniform sampler2D NoseTex01;
					uniform sampler2D HairTex00;
					uniform sampler2D HairTex01;
					uniform sampler2D HairTex02;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 outputColor_1;
					  outputColor_1 = SkinColor;
					  if ((CostumeBlendMode00 != 5)) {
					    outputColor_1 = texture2D (CostumeTex00, xlv_TEXCOORD0.xy);
					  } else {
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
					      tmpvar_6 = mix (outputColor_1, texColor_5, texColor_5.wwww);
					    } else {
					      if ((SkinBlendMode00 == 1)) {
					        lowp vec4 tmpvar_7;
					        if ((texColor_5.w == 0.0)) {
					          tmpvar_7 = outputColor_1;
					        } else {
					          tmpvar_7 = (texColor_5 * outputColor_1);
					        };
					        tmpvar_6 = mix (outputColor_1, tmpvar_7, texColor_5.wwww);
					      } else {
					        if ((SkinBlendMode00 == 2)) {
					          tmpvar_6 = mix (outputColor_1, (1.0 - (
					            (1.0 - texColor_5)
					           * 
					            (1.0 - outputColor_1)
					          )), texColor_5.wwww);
					        } else {
					          tmpvar_6 = outputColor_1;
					        };
					      };
					    };
					    outputColor_1 = tmpvar_6;
					    if ((NoseBlendMode00 != 5)) {
					      highp vec2 tmpvar_8;
					      tmpvar_8.x = NoseXOffset;
					      tmpvar_8.y = NoseYOffset;
					      highp vec2 tmpvar_9;
					      highp vec2 tmpvar_10;
					      tmpvar_10 = (((
					        (xlv_TEXCOORD0.xy + tmpvar_8)
					       * vec2(NoseCrop)) - vec2(NoseCrop)) + vec2(1.0, 1.0));
					      if (((tmpvar_10.x < 0.0) || (tmpvar_10.y < 0.0))) {
					        tmpvar_9 = vec2(0.0, 0.0);
					      } else {
					        tmpvar_9 = tmpvar_10;
					      };
					      lowp vec4 tmpvar_11;
					      tmpvar_11 = texture2D (NoseTex00, tmpvar_9);
					      lowp vec4 tmpvar_12;
					      if ((NoseBlendMode00 == 0)) {
					        tmpvar_12 = mix (tmpvar_6, tmpvar_11, tmpvar_11.wwww);
					      } else {
					        if ((NoseBlendMode00 == 1)) {
					          lowp vec4 tmpvar_13;
					          if ((tmpvar_11.w == 0.0)) {
					            tmpvar_13 = tmpvar_6;
					          } else {
					            tmpvar_13 = (tmpvar_11 * tmpvar_6);
					          };
					          tmpvar_12 = mix (tmpvar_6, tmpvar_13, tmpvar_11.wwww);
					        } else {
					          tmpvar_12 = tmpvar_6;
					        };
					      };
					      lowp vec4 tmpvar_14;
					      tmpvar_14 = texture2D (NoseTex01, tmpvar_9);
					      outputColor_1 = mix (tmpvar_12, (1.0 - (
					        (1.0 - tmpvar_14)
					       * 
					        (1.0 - tmpvar_12)
					      )), tmpvar_14.wwww);
					    };
					    highp vec2 tmpvar_15;
					    tmpvar_15.x = HairXOffset;
					    tmpvar_15.y = HairYOffset;
					    highp vec2 tmpvar_16;
					    tmpvar_16 = (xlv_TEXCOORD0.xy + tmpvar_15);
					    lowp vec4 tmpvar_17;
					    tmpvar_17 = texture2D (HairTex00, tmpvar_16);
					    if ((HairBlendMode01 != 5)) {
					      lowp vec4 tmpvar_18;
					      if ((HairBlendMode00 == 0)) {
					        lowp vec4 texColor_19;
					        texColor_19.w = tmpvar_17.w;
					        texColor_19.xyz = (tmpvar_17.xyz * HairColor.xyz);
					        tmpvar_18 = mix (outputColor_1, texColor_19, tmpvar_17.wwww);
					      } else {
					        tmpvar_18 = outputColor_1;
					      };
					      outputColor_1 = tmpvar_18;
					      lowp vec4 tmpvar_20;
					      tmpvar_20 = texture2D (HairTex01, tmpvar_16);
					      lowp vec4 tmpvar_21;
					      if ((HairBlendMode01 == 2)) {
					        tmpvar_21 = mix (tmpvar_18, (1.0 - (
					          (1.0 - tmpvar_20)
					         * 
					          (1.0 - tmpvar_18)
					        )), tmpvar_20.wwww);
					      } else {
					        tmpvar_21 = tmpvar_18;
					      };
					      outputColor_1 = tmpvar_21;
					      lowp vec4 tmpvar_22;
					      tmpvar_22 = texture2D (HairTex02, tmpvar_16);
					      lowp vec4 tmpvar_23;
					      if ((HairBlendMode02 == 1)) {
					        lowp vec4 tmpvar_24;
					        if ((tmpvar_22.w == 0.0)) {
					          tmpvar_24 = tmpvar_21;
					        } else {
					          tmpvar_24 = (tmpvar_22 * tmpvar_21);
					        };
					        tmpvar_23 = mix (tmpvar_21, tmpvar_24, tmpvar_22.wwww);
					      } else {
					        tmpvar_23 = tmpvar_21;
					      };
					      outputColor_1 = tmpvar_23;
					    } else {
					      if ((HairBlendMode00 != 5)) {
					        outputColor_1 = texture2D (HairTex00, tmpvar_16);
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
					uniform highp float NoseCrop;
					uniform highp float SkinCrop;
					uniform highp float SkinXOffset;
					uniform highp float SkinYOffset;
					uniform highp float NoseXOffset;
					uniform highp float NoseYOffset;
					uniform highp float HairXOffset;
					uniform highp float HairYOffset;
					uniform lowp vec4 SkinColor;
					uniform lowp vec4 HairColor;
					uniform highp int SkinBlendMode00;
					uniform highp int CostumeBlendMode00;
					uniform highp int NoseBlendMode00;
					uniform highp int HairBlendMode00;
					uniform highp int HairBlendMode01;
					uniform highp int HairBlendMode02;
					uniform sampler2D SkinTex00;
					uniform sampler2D CostumeTex00;
					uniform sampler2D NoseTex00;
					uniform sampler2D NoseTex01;
					uniform sampler2D HairTex00;
					uniform sampler2D HairTex01;
					uniform sampler2D HairTex02;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 outputColor_1;
					  outputColor_1 = SkinColor;
					  if ((CostumeBlendMode00 != 5)) {
					    outputColor_1 = texture2D (CostumeTex00, xlv_TEXCOORD0.xy);
					  } else {
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
					      tmpvar_6 = mix (outputColor_1, texColor_5, texColor_5.wwww);
					    } else {
					      if ((SkinBlendMode00 == 1)) {
					        lowp vec4 tmpvar_7;
					        if ((texColor_5.w == 0.0)) {
					          tmpvar_7 = outputColor_1;
					        } else {
					          tmpvar_7 = (texColor_5 * outputColor_1);
					        };
					        tmpvar_6 = mix (outputColor_1, tmpvar_7, texColor_5.wwww);
					      } else {
					        if ((SkinBlendMode00 == 2)) {
					          tmpvar_6 = mix (outputColor_1, (1.0 - (
					            (1.0 - texColor_5)
					           * 
					            (1.0 - outputColor_1)
					          )), texColor_5.wwww);
					        } else {
					          tmpvar_6 = outputColor_1;
					        };
					      };
					    };
					    outputColor_1 = tmpvar_6;
					    if ((NoseBlendMode00 != 5)) {
					      highp vec2 tmpvar_8;
					      tmpvar_8.x = NoseXOffset;
					      tmpvar_8.y = NoseYOffset;
					      highp vec2 tmpvar_9;
					      highp vec2 tmpvar_10;
					      tmpvar_10 = (((
					        (xlv_TEXCOORD0.xy + tmpvar_8)
					       * vec2(NoseCrop)) - vec2(NoseCrop)) + vec2(1.0, 1.0));
					      if (((tmpvar_10.x < 0.0) || (tmpvar_10.y < 0.0))) {
					        tmpvar_9 = vec2(0.0, 0.0);
					      } else {
					        tmpvar_9 = tmpvar_10;
					      };
					      lowp vec4 tmpvar_11;
					      tmpvar_11 = texture2D (NoseTex00, tmpvar_9);
					      lowp vec4 tmpvar_12;
					      if ((NoseBlendMode00 == 0)) {
					        tmpvar_12 = mix (tmpvar_6, tmpvar_11, tmpvar_11.wwww);
					      } else {
					        if ((NoseBlendMode00 == 1)) {
					          lowp vec4 tmpvar_13;
					          if ((tmpvar_11.w == 0.0)) {
					            tmpvar_13 = tmpvar_6;
					          } else {
					            tmpvar_13 = (tmpvar_11 * tmpvar_6);
					          };
					          tmpvar_12 = mix (tmpvar_6, tmpvar_13, tmpvar_11.wwww);
					        } else {
					          tmpvar_12 = tmpvar_6;
					        };
					      };
					      lowp vec4 tmpvar_14;
					      tmpvar_14 = texture2D (NoseTex01, tmpvar_9);
					      outputColor_1 = mix (tmpvar_12, (1.0 - (
					        (1.0 - tmpvar_14)
					       * 
					        (1.0 - tmpvar_12)
					      )), tmpvar_14.wwww);
					    };
					    highp vec2 tmpvar_15;
					    tmpvar_15.x = HairXOffset;
					    tmpvar_15.y = HairYOffset;
					    highp vec2 tmpvar_16;
					    tmpvar_16 = (xlv_TEXCOORD0.xy + tmpvar_15);
					    lowp vec4 tmpvar_17;
					    tmpvar_17 = texture2D (HairTex00, tmpvar_16);
					    if ((HairBlendMode01 != 5)) {
					      lowp vec4 tmpvar_18;
					      if ((HairBlendMode00 == 0)) {
					        lowp vec4 texColor_19;
					        texColor_19.w = tmpvar_17.w;
					        texColor_19.xyz = (tmpvar_17.xyz * HairColor.xyz);
					        tmpvar_18 = mix (outputColor_1, texColor_19, tmpvar_17.wwww);
					      } else {
					        tmpvar_18 = outputColor_1;
					      };
					      outputColor_1 = tmpvar_18;
					      lowp vec4 tmpvar_20;
					      tmpvar_20 = texture2D (HairTex01, tmpvar_16);
					      lowp vec4 tmpvar_21;
					      if ((HairBlendMode01 == 2)) {
					        tmpvar_21 = mix (tmpvar_18, (1.0 - (
					          (1.0 - tmpvar_20)
					         * 
					          (1.0 - tmpvar_18)
					        )), tmpvar_20.wwww);
					      } else {
					        tmpvar_21 = tmpvar_18;
					      };
					      outputColor_1 = tmpvar_21;
					      lowp vec4 tmpvar_22;
					      tmpvar_22 = texture2D (HairTex02, tmpvar_16);
					      lowp vec4 tmpvar_23;
					      if ((HairBlendMode02 == 1)) {
					        lowp vec4 tmpvar_24;
					        if ((tmpvar_22.w == 0.0)) {
					          tmpvar_24 = tmpvar_21;
					        } else {
					          tmpvar_24 = (tmpvar_22 * tmpvar_21);
					        };
					        tmpvar_23 = mix (tmpvar_21, tmpvar_24, tmpvar_22.wwww);
					      } else {
					        tmpvar_23 = tmpvar_21;
					      };
					      outputColor_1 = tmpvar_23;
					    } else {
					      if ((HairBlendMode00 != 5)) {
					        outputColor_1 = texture2D (HairTex00, tmpvar_16);
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