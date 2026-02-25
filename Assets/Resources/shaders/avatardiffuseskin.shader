Shader "AvatarDiffuseSkin" {
Properties {
 SkinColor ("Skin Color", Color) = (1.000000,1.000000,1.000000,1.000000)
 EyesColor ("Eye Color", Color) = (1.000000,1.000000,1.000000,1.000000)
 SkinXOffset ("Skin Augment Offset X", Float) = 0.000000
 SkinYOffset ("Skin Augment Offset Y", Float) = 0.000000
 MouthXOffset ("Mouth Offset X", Float) = 0.000000
 MouthYOffset ("Mouth Offset Y", Float) = 0.000000
 NoseXOffset ("Nose Offset X", Float) = 0.000000
 NoseYOffset ("Nose Offset Y", Float) = 0.000000
 EyesXOffset ("Eye Offset X", Float) = 0.000000
 EyesYOffset ("Eye Offset Y", Float) = 0.000000
 SkinBlendMode00 ("Skin Augment Blend Mode", Float) = 0.000000
 MouthBlendMode00 ("Mouth Blend Mode", Float) = 0.000000
 MouthBlendMode01 ("Mouth Dimple Blend Mode", Float) = 0.000000
 NoseBlendMode00 ("Nose Blend Mode", Float) = 0.000000
 NoseBlendMode01 ("Nose Highlight Blend Mode", Float) = 0.000000
 EyesBlendMode00 ("Eyes Blend Mode", Float) = 0.000000
 EyesBlendMode01 ("Eye Shadow Blend Mode", Float) = 0.000000
 EyesBlendMode02 ("Iris Blend Mode", Float) = 0.000000
 SkinTex00 ("Skin Augment", 2D) = "alpha" { }
 MouthTex00 ("Mouth", 2D) = "alpha" { }
 MouthTex01 ("Cheek", 2D) = "alpha" { }
 NoseTex00 ("Nose", 2D) = "alpha" { }
 NoseTex01 ("Nose Highlight", 2D) = "alpha" { }
 EyesTex00 ("Eyes", 2D) = "alpha" { }
 EyesTex01 ("Eye Shadow", 2D) = "alpha" { }
 EyesTex02 ("Iris", 2D) = "alpha" { }
 EyesMask ("Eye Mask", 2D) = "alpha" { }
 _MainTex ("Main Texture", 2D) = "white" { }
}
SubShader { 
 Pass {
  GpuProgramID 31092
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
					uniform lowp vec4 EyesColor;
					uniform highp float SkinCrop;
					uniform highp float MouthCrop;
					uniform highp float NoseCrop;
					uniform highp float EyesCrop;
					uniform highp float SkinXOffset;
					uniform highp float SkinYOffset;
					uniform highp float MouthXOffset;
					uniform highp float MouthYOffset;
					uniform highp float NoseXOffset;
					uniform highp float NoseYOffset;
					uniform highp float EyesXOffset;
					uniform highp float EyesYOffset;
					uniform highp int SkinBlendMode00;
					uniform highp int MouthBlendMode00;
					uniform highp int MouthBlendMode01;
					uniform highp int NoseBlendMode00;
					uniform sampler2D SkinTex00;
					uniform sampler2D MouthTex00;
					uniform sampler2D MouthTex01;
					uniform sampler2D NoseTex00;
					uniform sampler2D NoseTex01;
					uniform sampler2D EyesTex00;
					uniform sampler2D EyesMask;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 outputColor_1;
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
					  highp vec2 tmpvar_8;
					  tmpvar_8.x = MouthXOffset;
					  tmpvar_8.y = MouthYOffset;
					  highp vec2 tmpvar_9;
					  highp vec2 tmpvar_10;
					  tmpvar_10 = (((
					    (xlv_TEXCOORD0.xy + tmpvar_8)
					   * vec2(MouthCrop)) - vec2(MouthCrop)) + vec2(1.0, 1.0));
					  if (((tmpvar_10.x < 0.0) || (tmpvar_10.y < 0.0))) {
					    tmpvar_9 = vec2(0.0, 0.0);
					  } else {
					    tmpvar_9 = tmpvar_10;
					  };
					  if ((MouthBlendMode00 < 4)) {
					    lowp vec4 tmpvar_11;
					    tmpvar_11 = texture2D (MouthTex00, tmpvar_9);
					    lowp vec4 tmpvar_12;
					    if ((MouthBlendMode00 == 0)) {
					      tmpvar_12 = mix (tmpvar_6, tmpvar_11, tmpvar_11.wwww);
					    } else {
					      if ((MouthBlendMode00 == 1)) {
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
					    outputColor_1 = tmpvar_12;
					    lowp vec4 tmpvar_14;
					    tmpvar_14 = texture2D (MouthTex01, tmpvar_9);
					    lowp vec4 tmpvar_15;
					    if ((MouthBlendMode01 == 1)) {
					      lowp vec4 tmpvar_16;
					      if ((tmpvar_14.w == 0.0)) {
					        tmpvar_16 = tmpvar_12;
					      } else {
					        tmpvar_16 = (tmpvar_14 * tmpvar_12);
					      };
					      tmpvar_15 = mix (tmpvar_12, tmpvar_16, tmpvar_14.wwww);
					    } else {
					      tmpvar_15 = tmpvar_12;
					    };
					    outputColor_1 = tmpvar_15;
					  };
					  highp vec2 tmpvar_17;
					  tmpvar_17.x = NoseXOffset;
					  tmpvar_17.y = NoseYOffset;
					  highp vec2 tmpvar_18;
					  highp vec2 tmpvar_19;
					  tmpvar_19 = (((
					    (xlv_TEXCOORD0.xy + tmpvar_17)
					   * vec2(NoseCrop)) - vec2(NoseCrop)) + vec2(1.0, 1.0));
					  if (((tmpvar_19.x < 0.0) || (tmpvar_19.y < 0.0))) {
					    tmpvar_18 = vec2(0.0, 0.0);
					  } else {
					    tmpvar_18 = tmpvar_19;
					  };
					  if ((NoseBlendMode00 < 4)) {
					    lowp vec4 tmpvar_20;
					    tmpvar_20 = texture2D (NoseTex00, tmpvar_18);
					    lowp vec4 tmpvar_21;
					    if ((NoseBlendMode00 == 0)) {
					      tmpvar_21 = mix (outputColor_1, tmpvar_20, tmpvar_20.wwww);
					    } else {
					      if ((NoseBlendMode00 == 1)) {
					        lowp vec4 tmpvar_22;
					        if ((tmpvar_20.w == 0.0)) {
					          tmpvar_22 = outputColor_1;
					        } else {
					          tmpvar_22 = (tmpvar_20 * outputColor_1);
					        };
					        tmpvar_21 = mix (outputColor_1, tmpvar_22, tmpvar_20.wwww);
					      } else {
					        tmpvar_21 = outputColor_1;
					      };
					    };
					    lowp vec4 tmpvar_23;
					    tmpvar_23 = texture2D (NoseTex01, tmpvar_18);
					    outputColor_1 = mix (tmpvar_21, (1.0 - (
					      (1.0 - tmpvar_23)
					     * 
					      (1.0 - tmpvar_21)
					    )), tmpvar_23.wwww);
					  };
					  highp vec2 tmpvar_24;
					  tmpvar_24.x = EyesXOffset;
					  tmpvar_24.y = EyesYOffset;
					  highp vec2 tmpvar_25;
					  highp vec2 tmpvar_26;
					  tmpvar_26 = (((
					    (xlv_TEXCOORD0.xy + tmpvar_24)
					   * vec2(EyesCrop)) - vec2(EyesCrop)) + vec2(1.0, 1.0));
					  if (((tmpvar_26.x < 0.0) || (tmpvar_26.y < 0.0))) {
					    tmpvar_25 = vec2(0.0, 0.0);
					  } else {
					    tmpvar_25 = tmpvar_26;
					  };
					  lowp vec4 tmpvar_27;
					  tmpvar_27 = texture2D (EyesTex00, tmpvar_25);
					  lowp vec4 tmpvar_28;
					  tmpvar_28 = mix (outputColor_1, tmpvar_27, tmpvar_27.wwww);
					  outputColor_1 = tmpvar_28;
					  lowp vec4 tmpvar_29;
					  tmpvar_29 = texture2D (EyesMask, tmpvar_25);
					  lowp vec4 finalColor_30;
					  finalColor_30 = tmpvar_27;
					  if (((tmpvar_27.w > 0.0) && (tmpvar_29.w > 0.001))) {
					    lowp float value_31;
					    lowp vec3 finalHsv_32;
					    lowp vec3 tintHsv_33;
					    highp vec3 rgbColor_34;
					    rgbColor_34 = EyesColor.xyz;
					    highp vec4 hsv_35;
					    hsv_35.z = max (max (rgbColor_34.x, rgbColor_34.y), rgbColor_34.z);
					    highp float tmpvar_36;
					    tmpvar_36 = (hsv_35.z - min (min (rgbColor_34.x, rgbColor_34.y), rgbColor_34.z));
					    if ((tmpvar_36 != 0.0)) {
					      highp vec3 delta2_37;
					      hsv_35.y = (tmpvar_36 / hsv_35.z);
					      highp vec3 tmpvar_38;
					      tmpvar_38 = ((hsv_35.z - rgbColor_34) / tmpvar_36);
					      delta2_37 = (tmpvar_38 - tmpvar_38.zxy);
					      delta2_37.xy = (delta2_37.xy + vec2(2.0, 4.0));
					      if ((rgbColor_34.x >= hsv_35.z)) {
					        hsv_35.x = delta2_37.z;
					      } else {
					        if ((rgbColor_34.y >= hsv_35.z)) {
					          hsv_35.x = delta2_37.x;
					        } else {
					          hsv_35.x = delta2_37.y;
					        };
					      };
					      hsv_35.x = fract((hsv_35.x / 6.0));
					    } else {
					      hsv_35.xy = vec2(0.0, 0.0);
					    };
					    highp vec3 tmpvar_39;
					    tmpvar_39 = hsv_35.xyz;
					    tintHsv_33 = tmpvar_39;
					    lowp vec3 tmpvar_40;
					    tmpvar_40 = tmpvar_27.xyz;
					    highp vec3 rgbColor_41;
					    rgbColor_41 = tmpvar_40;
					    highp float tmpvar_42;
					    tmpvar_42 = max (max (rgbColor_41.x, rgbColor_41.y), rgbColor_41.z);
					    value_31 = tmpvar_42;
					    if ((value_31 > 0.5)) {
					      lowp vec3 tmpvar_43;
					      tmpvar_43.x = tintHsv_33.x;
					      tmpvar_43.y = ((1.0 - (
					        (value_31 * 2.0)
					       - 1.0)) * tintHsv_33.y);
					      tmpvar_43.z = mix (tintHsv_33.z, 1.0, ((value_31 * 2.0) - 1.0));
					      finalHsv_32 = tmpvar_43;
					    } else {
					      lowp vec3 tmpvar_44;
					      tmpvar_44.xy = tintHsv_33.xy;
					      tmpvar_44.z = ((value_31 * 2.0) * tintHsv_33.z);
					      finalHsv_32 = tmpvar_44;
					    };
					    highp vec3 tmpvar_45;
					    highp vec3 hsv_46;
					    hsv_46 = finalHsv_32;
					    highp vec3 tmpvar_47;
					    tmpvar_47.x = (abs((
					      (hsv_46.x * 6.0)
					     - 3.0)) - 1.0);
					    tmpvar_47.y = (2.0 - abs((
					      (hsv_46.x * 6.0)
					     - 2.0)));
					    tmpvar_47.z = (2.0 - abs((
					      (hsv_46.x * 6.0)
					     - 4.0)));
					    tmpvar_45 = (((
					      (clamp (tmpvar_47, 0.0, 1.0) - 1.0)
					     * hsv_46.y) + 1.0) * hsv_46.z);
					    finalColor_30.xyz = tmpvar_45;
					    finalColor_30 = mix (tmpvar_27, finalColor_30, tmpvar_29.wwww);
					  };
					  lowp vec4 tmpvar_48;
					  tmpvar_48 = mix (tmpvar_28, finalColor_30, finalColor_30.wwww);
					  outputColor_1 = tmpvar_48;
					  gl_FragData[0] = tmpvar_48;
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
					uniform lowp vec4 EyesColor;
					uniform highp float SkinCrop;
					uniform highp float MouthCrop;
					uniform highp float NoseCrop;
					uniform highp float EyesCrop;
					uniform highp float SkinXOffset;
					uniform highp float SkinYOffset;
					uniform highp float MouthXOffset;
					uniform highp float MouthYOffset;
					uniform highp float NoseXOffset;
					uniform highp float NoseYOffset;
					uniform highp float EyesXOffset;
					uniform highp float EyesYOffset;
					uniform highp int SkinBlendMode00;
					uniform highp int MouthBlendMode00;
					uniform highp int MouthBlendMode01;
					uniform highp int NoseBlendMode00;
					uniform sampler2D SkinTex00;
					uniform sampler2D MouthTex00;
					uniform sampler2D MouthTex01;
					uniform sampler2D NoseTex00;
					uniform sampler2D NoseTex01;
					uniform sampler2D EyesTex00;
					uniform sampler2D EyesMask;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 outputColor_1;
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
					  highp vec2 tmpvar_8;
					  tmpvar_8.x = MouthXOffset;
					  tmpvar_8.y = MouthYOffset;
					  highp vec2 tmpvar_9;
					  highp vec2 tmpvar_10;
					  tmpvar_10 = (((
					    (xlv_TEXCOORD0.xy + tmpvar_8)
					   * vec2(MouthCrop)) - vec2(MouthCrop)) + vec2(1.0, 1.0));
					  if (((tmpvar_10.x < 0.0) || (tmpvar_10.y < 0.0))) {
					    tmpvar_9 = vec2(0.0, 0.0);
					  } else {
					    tmpvar_9 = tmpvar_10;
					  };
					  if ((MouthBlendMode00 < 4)) {
					    lowp vec4 tmpvar_11;
					    tmpvar_11 = texture2D (MouthTex00, tmpvar_9);
					    lowp vec4 tmpvar_12;
					    if ((MouthBlendMode00 == 0)) {
					      tmpvar_12 = mix (tmpvar_6, tmpvar_11, tmpvar_11.wwww);
					    } else {
					      if ((MouthBlendMode00 == 1)) {
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
					    outputColor_1 = tmpvar_12;
					    lowp vec4 tmpvar_14;
					    tmpvar_14 = texture2D (MouthTex01, tmpvar_9);
					    lowp vec4 tmpvar_15;
					    if ((MouthBlendMode01 == 1)) {
					      lowp vec4 tmpvar_16;
					      if ((tmpvar_14.w == 0.0)) {
					        tmpvar_16 = tmpvar_12;
					      } else {
					        tmpvar_16 = (tmpvar_14 * tmpvar_12);
					      };
					      tmpvar_15 = mix (tmpvar_12, tmpvar_16, tmpvar_14.wwww);
					    } else {
					      tmpvar_15 = tmpvar_12;
					    };
					    outputColor_1 = tmpvar_15;
					  };
					  highp vec2 tmpvar_17;
					  tmpvar_17.x = NoseXOffset;
					  tmpvar_17.y = NoseYOffset;
					  highp vec2 tmpvar_18;
					  highp vec2 tmpvar_19;
					  tmpvar_19 = (((
					    (xlv_TEXCOORD0.xy + tmpvar_17)
					   * vec2(NoseCrop)) - vec2(NoseCrop)) + vec2(1.0, 1.0));
					  if (((tmpvar_19.x < 0.0) || (tmpvar_19.y < 0.0))) {
					    tmpvar_18 = vec2(0.0, 0.0);
					  } else {
					    tmpvar_18 = tmpvar_19;
					  };
					  if ((NoseBlendMode00 < 4)) {
					    lowp vec4 tmpvar_20;
					    tmpvar_20 = texture2D (NoseTex00, tmpvar_18);
					    lowp vec4 tmpvar_21;
					    if ((NoseBlendMode00 == 0)) {
					      tmpvar_21 = mix (outputColor_1, tmpvar_20, tmpvar_20.wwww);
					    } else {
					      if ((NoseBlendMode00 == 1)) {
					        lowp vec4 tmpvar_22;
					        if ((tmpvar_20.w == 0.0)) {
					          tmpvar_22 = outputColor_1;
					        } else {
					          tmpvar_22 = (tmpvar_20 * outputColor_1);
					        };
					        tmpvar_21 = mix (outputColor_1, tmpvar_22, tmpvar_20.wwww);
					      } else {
					        tmpvar_21 = outputColor_1;
					      };
					    };
					    lowp vec4 tmpvar_23;
					    tmpvar_23 = texture2D (NoseTex01, tmpvar_18);
					    outputColor_1 = mix (tmpvar_21, (1.0 - (
					      (1.0 - tmpvar_23)
					     * 
					      (1.0 - tmpvar_21)
					    )), tmpvar_23.wwww);
					  };
					  highp vec2 tmpvar_24;
					  tmpvar_24.x = EyesXOffset;
					  tmpvar_24.y = EyesYOffset;
					  highp vec2 tmpvar_25;
					  highp vec2 tmpvar_26;
					  tmpvar_26 = (((
					    (xlv_TEXCOORD0.xy + tmpvar_24)
					   * vec2(EyesCrop)) - vec2(EyesCrop)) + vec2(1.0, 1.0));
					  if (((tmpvar_26.x < 0.0) || (tmpvar_26.y < 0.0))) {
					    tmpvar_25 = vec2(0.0, 0.0);
					  } else {
					    tmpvar_25 = tmpvar_26;
					  };
					  lowp vec4 tmpvar_27;
					  tmpvar_27 = texture2D (EyesTex00, tmpvar_25);
					  lowp vec4 tmpvar_28;
					  tmpvar_28 = mix (outputColor_1, tmpvar_27, tmpvar_27.wwww);
					  outputColor_1 = tmpvar_28;
					  lowp vec4 tmpvar_29;
					  tmpvar_29 = texture2D (EyesMask, tmpvar_25);
					  lowp vec4 finalColor_30;
					  finalColor_30 = tmpvar_27;
					  if (((tmpvar_27.w > 0.0) && (tmpvar_29.w > 0.001))) {
					    lowp float value_31;
					    lowp vec3 finalHsv_32;
					    lowp vec3 tintHsv_33;
					    highp vec3 rgbColor_34;
					    rgbColor_34 = EyesColor.xyz;
					    highp vec4 hsv_35;
					    hsv_35.z = max (max (rgbColor_34.x, rgbColor_34.y), rgbColor_34.z);
					    highp float tmpvar_36;
					    tmpvar_36 = (hsv_35.z - min (min (rgbColor_34.x, rgbColor_34.y), rgbColor_34.z));
					    if ((tmpvar_36 != 0.0)) {
					      highp vec3 delta2_37;
					      hsv_35.y = (tmpvar_36 / hsv_35.z);
					      highp vec3 tmpvar_38;
					      tmpvar_38 = ((hsv_35.z - rgbColor_34) / tmpvar_36);
					      delta2_37 = (tmpvar_38 - tmpvar_38.zxy);
					      delta2_37.xy = (delta2_37.xy + vec2(2.0, 4.0));
					      if ((rgbColor_34.x >= hsv_35.z)) {
					        hsv_35.x = delta2_37.z;
					      } else {
					        if ((rgbColor_34.y >= hsv_35.z)) {
					          hsv_35.x = delta2_37.x;
					        } else {
					          hsv_35.x = delta2_37.y;
					        };
					      };
					      hsv_35.x = fract((hsv_35.x / 6.0));
					    } else {
					      hsv_35.xy = vec2(0.0, 0.0);
					    };
					    highp vec3 tmpvar_39;
					    tmpvar_39 = hsv_35.xyz;
					    tintHsv_33 = tmpvar_39;
					    lowp vec3 tmpvar_40;
					    tmpvar_40 = tmpvar_27.xyz;
					    highp vec3 rgbColor_41;
					    rgbColor_41 = tmpvar_40;
					    highp float tmpvar_42;
					    tmpvar_42 = max (max (rgbColor_41.x, rgbColor_41.y), rgbColor_41.z);
					    value_31 = tmpvar_42;
					    if ((value_31 > 0.5)) {
					      lowp vec3 tmpvar_43;
					      tmpvar_43.x = tintHsv_33.x;
					      tmpvar_43.y = ((1.0 - (
					        (value_31 * 2.0)
					       - 1.0)) * tintHsv_33.y);
					      tmpvar_43.z = mix (tintHsv_33.z, 1.0, ((value_31 * 2.0) - 1.0));
					      finalHsv_32 = tmpvar_43;
					    } else {
					      lowp vec3 tmpvar_44;
					      tmpvar_44.xy = tintHsv_33.xy;
					      tmpvar_44.z = ((value_31 * 2.0) * tintHsv_33.z);
					      finalHsv_32 = tmpvar_44;
					    };
					    highp vec3 tmpvar_45;
					    highp vec3 hsv_46;
					    hsv_46 = finalHsv_32;
					    highp vec3 tmpvar_47;
					    tmpvar_47.x = (abs((
					      (hsv_46.x * 6.0)
					     - 3.0)) - 1.0);
					    tmpvar_47.y = (2.0 - abs((
					      (hsv_46.x * 6.0)
					     - 2.0)));
					    tmpvar_47.z = (2.0 - abs((
					      (hsv_46.x * 6.0)
					     - 4.0)));
					    tmpvar_45 = (((
					      (clamp (tmpvar_47, 0.0, 1.0) - 1.0)
					     * hsv_46.y) + 1.0) * hsv_46.z);
					    finalColor_30.xyz = tmpvar_45;
					    finalColor_30 = mix (tmpvar_27, finalColor_30, tmpvar_29.wwww);
					  };
					  lowp vec4 tmpvar_48;
					  tmpvar_48 = mix (tmpvar_28, finalColor_30, finalColor_30.wwww);
					  outputColor_1 = tmpvar_48;
					  gl_FragData[0] = tmpvar_48;
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
					uniform lowp vec4 EyesColor;
					uniform highp float SkinCrop;
					uniform highp float MouthCrop;
					uniform highp float NoseCrop;
					uniform highp float EyesCrop;
					uniform highp float SkinXOffset;
					uniform highp float SkinYOffset;
					uniform highp float MouthXOffset;
					uniform highp float MouthYOffset;
					uniform highp float NoseXOffset;
					uniform highp float NoseYOffset;
					uniform highp float EyesXOffset;
					uniform highp float EyesYOffset;
					uniform highp int SkinBlendMode00;
					uniform highp int MouthBlendMode00;
					uniform highp int MouthBlendMode01;
					uniform highp int NoseBlendMode00;
					uniform sampler2D SkinTex00;
					uniform sampler2D MouthTex00;
					uniform sampler2D MouthTex01;
					uniform sampler2D NoseTex00;
					uniform sampler2D NoseTex01;
					uniform sampler2D EyesTex00;
					uniform sampler2D EyesMask;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 outputColor_1;
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
					  highp vec2 tmpvar_8;
					  tmpvar_8.x = MouthXOffset;
					  tmpvar_8.y = MouthYOffset;
					  highp vec2 tmpvar_9;
					  highp vec2 tmpvar_10;
					  tmpvar_10 = (((
					    (xlv_TEXCOORD0.xy + tmpvar_8)
					   * vec2(MouthCrop)) - vec2(MouthCrop)) + vec2(1.0, 1.0));
					  if (((tmpvar_10.x < 0.0) || (tmpvar_10.y < 0.0))) {
					    tmpvar_9 = vec2(0.0, 0.0);
					  } else {
					    tmpvar_9 = tmpvar_10;
					  };
					  if ((MouthBlendMode00 < 4)) {
					    lowp vec4 tmpvar_11;
					    tmpvar_11 = texture2D (MouthTex00, tmpvar_9);
					    lowp vec4 tmpvar_12;
					    if ((MouthBlendMode00 == 0)) {
					      tmpvar_12 = mix (tmpvar_6, tmpvar_11, tmpvar_11.wwww);
					    } else {
					      if ((MouthBlendMode00 == 1)) {
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
					    outputColor_1 = tmpvar_12;
					    lowp vec4 tmpvar_14;
					    tmpvar_14 = texture2D (MouthTex01, tmpvar_9);
					    lowp vec4 tmpvar_15;
					    if ((MouthBlendMode01 == 1)) {
					      lowp vec4 tmpvar_16;
					      if ((tmpvar_14.w == 0.0)) {
					        tmpvar_16 = tmpvar_12;
					      } else {
					        tmpvar_16 = (tmpvar_14 * tmpvar_12);
					      };
					      tmpvar_15 = mix (tmpvar_12, tmpvar_16, tmpvar_14.wwww);
					    } else {
					      tmpvar_15 = tmpvar_12;
					    };
					    outputColor_1 = tmpvar_15;
					  };
					  highp vec2 tmpvar_17;
					  tmpvar_17.x = NoseXOffset;
					  tmpvar_17.y = NoseYOffset;
					  highp vec2 tmpvar_18;
					  highp vec2 tmpvar_19;
					  tmpvar_19 = (((
					    (xlv_TEXCOORD0.xy + tmpvar_17)
					   * vec2(NoseCrop)) - vec2(NoseCrop)) + vec2(1.0, 1.0));
					  if (((tmpvar_19.x < 0.0) || (tmpvar_19.y < 0.0))) {
					    tmpvar_18 = vec2(0.0, 0.0);
					  } else {
					    tmpvar_18 = tmpvar_19;
					  };
					  if ((NoseBlendMode00 < 4)) {
					    lowp vec4 tmpvar_20;
					    tmpvar_20 = texture2D (NoseTex00, tmpvar_18);
					    lowp vec4 tmpvar_21;
					    if ((NoseBlendMode00 == 0)) {
					      tmpvar_21 = mix (outputColor_1, tmpvar_20, tmpvar_20.wwww);
					    } else {
					      if ((NoseBlendMode00 == 1)) {
					        lowp vec4 tmpvar_22;
					        if ((tmpvar_20.w == 0.0)) {
					          tmpvar_22 = outputColor_1;
					        } else {
					          tmpvar_22 = (tmpvar_20 * outputColor_1);
					        };
					        tmpvar_21 = mix (outputColor_1, tmpvar_22, tmpvar_20.wwww);
					      } else {
					        tmpvar_21 = outputColor_1;
					      };
					    };
					    lowp vec4 tmpvar_23;
					    tmpvar_23 = texture2D (NoseTex01, tmpvar_18);
					    outputColor_1 = mix (tmpvar_21, (1.0 - (
					      (1.0 - tmpvar_23)
					     * 
					      (1.0 - tmpvar_21)
					    )), tmpvar_23.wwww);
					  };
					  highp vec2 tmpvar_24;
					  tmpvar_24.x = EyesXOffset;
					  tmpvar_24.y = EyesYOffset;
					  highp vec2 tmpvar_25;
					  highp vec2 tmpvar_26;
					  tmpvar_26 = (((
					    (xlv_TEXCOORD0.xy + tmpvar_24)
					   * vec2(EyesCrop)) - vec2(EyesCrop)) + vec2(1.0, 1.0));
					  if (((tmpvar_26.x < 0.0) || (tmpvar_26.y < 0.0))) {
					    tmpvar_25 = vec2(0.0, 0.0);
					  } else {
					    tmpvar_25 = tmpvar_26;
					  };
					  lowp vec4 tmpvar_27;
					  tmpvar_27 = texture2D (EyesTex00, tmpvar_25);
					  lowp vec4 tmpvar_28;
					  tmpvar_28 = mix (outputColor_1, tmpvar_27, tmpvar_27.wwww);
					  outputColor_1 = tmpvar_28;
					  lowp vec4 tmpvar_29;
					  tmpvar_29 = texture2D (EyesMask, tmpvar_25);
					  lowp vec4 finalColor_30;
					  finalColor_30 = tmpvar_27;
					  if (((tmpvar_27.w > 0.0) && (tmpvar_29.w > 0.001))) {
					    lowp float value_31;
					    lowp vec3 finalHsv_32;
					    lowp vec3 tintHsv_33;
					    highp vec3 rgbColor_34;
					    rgbColor_34 = EyesColor.xyz;
					    highp vec4 hsv_35;
					    hsv_35.z = max (max (rgbColor_34.x, rgbColor_34.y), rgbColor_34.z);
					    highp float tmpvar_36;
					    tmpvar_36 = (hsv_35.z - min (min (rgbColor_34.x, rgbColor_34.y), rgbColor_34.z));
					    if ((tmpvar_36 != 0.0)) {
					      highp vec3 delta2_37;
					      hsv_35.y = (tmpvar_36 / hsv_35.z);
					      highp vec3 tmpvar_38;
					      tmpvar_38 = ((hsv_35.z - rgbColor_34) / tmpvar_36);
					      delta2_37 = (tmpvar_38 - tmpvar_38.zxy);
					      delta2_37.xy = (delta2_37.xy + vec2(2.0, 4.0));
					      if ((rgbColor_34.x >= hsv_35.z)) {
					        hsv_35.x = delta2_37.z;
					      } else {
					        if ((rgbColor_34.y >= hsv_35.z)) {
					          hsv_35.x = delta2_37.x;
					        } else {
					          hsv_35.x = delta2_37.y;
					        };
					      };
					      hsv_35.x = fract((hsv_35.x / 6.0));
					    } else {
					      hsv_35.xy = vec2(0.0, 0.0);
					    };
					    highp vec3 tmpvar_39;
					    tmpvar_39 = hsv_35.xyz;
					    tintHsv_33 = tmpvar_39;
					    lowp vec3 tmpvar_40;
					    tmpvar_40 = tmpvar_27.xyz;
					    highp vec3 rgbColor_41;
					    rgbColor_41 = tmpvar_40;
					    highp float tmpvar_42;
					    tmpvar_42 = max (max (rgbColor_41.x, rgbColor_41.y), rgbColor_41.z);
					    value_31 = tmpvar_42;
					    if ((value_31 > 0.5)) {
					      lowp vec3 tmpvar_43;
					      tmpvar_43.x = tintHsv_33.x;
					      tmpvar_43.y = ((1.0 - (
					        (value_31 * 2.0)
					       - 1.0)) * tintHsv_33.y);
					      tmpvar_43.z = mix (tintHsv_33.z, 1.0, ((value_31 * 2.0) - 1.0));
					      finalHsv_32 = tmpvar_43;
					    } else {
					      lowp vec3 tmpvar_44;
					      tmpvar_44.xy = tintHsv_33.xy;
					      tmpvar_44.z = ((value_31 * 2.0) * tintHsv_33.z);
					      finalHsv_32 = tmpvar_44;
					    };
					    highp vec3 tmpvar_45;
					    highp vec3 hsv_46;
					    hsv_46 = finalHsv_32;
					    highp vec3 tmpvar_47;
					    tmpvar_47.x = (abs((
					      (hsv_46.x * 6.0)
					     - 3.0)) - 1.0);
					    tmpvar_47.y = (2.0 - abs((
					      (hsv_46.x * 6.0)
					     - 2.0)));
					    tmpvar_47.z = (2.0 - abs((
					      (hsv_46.x * 6.0)
					     - 4.0)));
					    tmpvar_45 = (((
					      (clamp (tmpvar_47, 0.0, 1.0) - 1.0)
					     * hsv_46.y) + 1.0) * hsv_46.z);
					    finalColor_30.xyz = tmpvar_45;
					    finalColor_30 = mix (tmpvar_27, finalColor_30, tmpvar_29.wwww);
					  };
					  lowp vec4 tmpvar_48;
					  tmpvar_48 = mix (tmpvar_28, finalColor_30, finalColor_30.wwww);
					  outputColor_1 = tmpvar_48;
					  gl_FragData[0] = tmpvar_48;
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