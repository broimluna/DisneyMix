Shader "AvatarDiffuseFeatures" {
Properties {
 HairColor ("Hair Color", Color) = (1.000000,1.000000,1.000000,1.000000)
 AccessoryColor ("Accessory Color", Color) = (1.000000,1.000000,1.000000,1.000000)
 BrowXOffset ("Eyebrows X Offset", Float) = 0.000000
 BrowYOffset ("Eyebrows Y Offset", Float) = 0.000000
 AccessoryXOffset ("Accessories X Offset", Float) = 0.000000
 AccessoryYOffset ("Accessories Y Offset", Float) = 0.000000
 HairXOffset ("Hair X Offset", Float) = 0.000000
 HairYOffset ("Hair Y Offset", Float) = 0.000000
 BrowBlendMode00 ("Brow Blend Mode", Float) = 0.000000
 AccessoryBlendMode00 ("Accessory Blend Mode", Float) = 0.000000
 AccessoryBlendMode01 ("Accessory Blend Mode", Float) = 0.000000
 HairBlendMode00 ("Hair Blend Mode", Float) = 0.000000
 HairBlendMode01 ("Hair Highlight Blend Mode", Float) = 0.000000
 HairBlendMode02 ("Hair Shadow Blend Mode", Float) = 0.000000
 AvatarDiffuseBase ("Avatar Diffuse Base", 2D) = "white" { }
 BrowTex00 ("Eyebrows", 2D) = "alpha" { }
 AccessoryTex00 ("Accessory", 2D) = "alpha" { }
 AccessoryMask ("Accessory Mask", 2D) = "alpha" { }
 HairTex00 ("Hair", 2D) = "alpha" { }
 HairTex01 ("Hair Highlights", 2D) = "alpha" { }
 HairTex02 ("Hair Shadow", 2D) = "alpha" { }
 _MainTex ("Main Texture", 2D) = "white" { }
}
SubShader { 
 Pass {
  GpuProgramID 42008
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
					uniform highp float AccessoryCrop;
					uniform highp float BrowXOffset;
					uniform highp float BrowYOffset;
					uniform highp float AccessoryXOffset;
					uniform highp float AccessoryYOffset;
					uniform highp float HairXOffset;
					uniform highp float HairYOffset;
					uniform lowp vec4 HairColor;
					uniform lowp vec4 AccessoryColor;
					uniform highp int BrowBlendMode00;
					uniform highp int AccessoryBlendMode00;
					uniform highp int AccessoryBlendMode01;
					uniform highp int HairBlendMode00;
					uniform sampler2D AvatarDiffuseBase;
					uniform sampler2D BrowTex00;
					uniform sampler2D AccessoryTex00;
					uniform sampler2D AccessoryMask;
					uniform sampler2D HairTex00;
					uniform sampler2D HairTex01;
					uniform sampler2D HairTex02;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 outputColor_1;
					  outputColor_1 = texture2D (AvatarDiffuseBase, xlv_TEXCOORD0.xy);
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
					  lowp vec4 texColor_5;
					  texColor_5 = texture2D (BrowTex00, tmpvar_3);
					  lowp vec4 tmpvar_6;
					  if ((BrowBlendMode00 == 0)) {
					    lowp vec4 texColor_7;
					    texColor_7.w = texColor_5.w;
					    texColor_7.xyz = (texColor_5.xyz * HairColor.xyz);
					    tmpvar_6 = mix (outputColor_1, texColor_7, texColor_5.wwww);
					  } else {
					    tmpvar_6 = outputColor_1;
					  };
					  outputColor_1 = tmpvar_6;
					  if ((HairBlendMode00 == 0)) {
					    highp vec2 tmpvar_8;
					    tmpvar_8.x = HairXOffset;
					    tmpvar_8.y = HairYOffset;
					    highp vec2 tmpvar_9;
					    tmpvar_9 = (xlv_TEXCOORD0.xy + tmpvar_8);
					    lowp vec4 tmpvar_10;
					    tmpvar_10 = texture2D (HairTex00, tmpvar_9);
					    lowp vec4 texColor_11;
					    texColor_11.w = tmpvar_10.w;
					    texColor_11.xyz = (tmpvar_10.xyz * HairColor.xyz);
					    lowp vec4 tmpvar_12;
					    tmpvar_12 = mix (tmpvar_6, texColor_11, tmpvar_10.wwww);
					    lowp vec4 tmpvar_13;
					    tmpvar_13 = texture2D (HairTex01, tmpvar_9);
					    lowp vec4 tmpvar_14;
					    tmpvar_14 = mix (tmpvar_12, (1.0 - (
					      (1.0 - tmpvar_13)
					     * 
					      (1.0 - tmpvar_12)
					    )), tmpvar_13.wwww);
					    outputColor_1 = tmpvar_14;
					    lowp vec4 tmpvar_15;
					    tmpvar_15 = texture2D (HairTex02, tmpvar_9);
					    lowp vec4 tmpvar_16;
					    if ((tmpvar_15.w == 0.0)) {
					      tmpvar_16 = tmpvar_14;
					    } else {
					      tmpvar_16 = (tmpvar_15 * tmpvar_14);
					    };
					    outputColor_1 = mix (tmpvar_14, tmpvar_16, tmpvar_15.wwww);
					  };
					  highp vec2 tmpvar_17;
					  tmpvar_17.x = AccessoryXOffset;
					  tmpvar_17.y = AccessoryYOffset;
					  highp vec2 tmpvar_18;
					  highp vec2 tmpvar_19;
					  tmpvar_19 = (((
					    (xlv_TEXCOORD0.xy + tmpvar_17)
					   * vec2(AccessoryCrop)) - vec2(AccessoryCrop)) + vec2(1.0, 1.0));
					  if (((tmpvar_19.x < 0.0) || (tmpvar_19.y < 0.0))) {
					    tmpvar_18 = vec2(0.0, 0.0);
					  } else {
					    tmpvar_18 = tmpvar_19;
					  };
					  lowp vec4 tmpvar_20;
					  tmpvar_20 = texture2D (AccessoryTex00, tmpvar_18);
					  lowp vec4 tmpvar_21;
					  tmpvar_21 = texture2D (AccessoryMask, tmpvar_18);
					  if ((AccessoryBlendMode00 != 5)) {
					    lowp vec4 tmpvar_22;
					    if ((AccessoryBlendMode01 == 6)) {
					      lowp vec4 maskColor_23;
					      maskColor_23 = tmpvar_21;
					      if ((tmpvar_21.w > 0.1)) {
					        maskColor_23.xyz = (tmpvar_20.xyz * AccessoryColor.xyz);
					      } else {
					        maskColor_23.xyz = tmpvar_20.xyz;
					      };
					      maskColor_23.w = tmpvar_20.w;
					      tmpvar_22 = mix (outputColor_1, maskColor_23, tmpvar_20.wwww);
					    } else {
					      if ((AccessoryBlendMode01 == 4)) {
					        lowp vec4 finalColor_24;
					        finalColor_24 = tmpvar_20;
					        if (((tmpvar_20.w > 0.0) && (tmpvar_21.w > 0.001))) {
					          highp vec3 rgbColor_25;
					          rgbColor_25 = AccessoryColor.xyz;
					          highp float tmpvar_26;
					          tmpvar_26 = (((0.596 * rgbColor_25.x) - (0.275 * rgbColor_25.y)) - (0.321 * rgbColor_25.z));
					          highp float tmpvar_27;
					          tmpvar_27 = (((0.212 * rgbColor_25.x) - (0.523 * rgbColor_25.y)) + (0.311 * rgbColor_25.z));
					          highp float tmpvar_28;
					          highp float tmpvar_29;
					          tmpvar_29 = (min (abs(
					            (tmpvar_27 / tmpvar_26)
					          ), 1.0) / max (abs(
					            (tmpvar_27 / tmpvar_26)
					          ), 1.0));
					          highp float tmpvar_30;
					          tmpvar_30 = (tmpvar_29 * tmpvar_29);
					          tmpvar_30 = (((
					            ((((
					              ((((-0.01213232 * tmpvar_30) + 0.05368138) * tmpvar_30) - 0.1173503)
					             * tmpvar_30) + 0.1938925) * tmpvar_30) - 0.3326756)
					           * tmpvar_30) + 0.9999793) * tmpvar_29);
					          tmpvar_30 = (tmpvar_30 + (float(
					            (abs((tmpvar_27 / tmpvar_26)) > 1.0)
					          ) * (
					            (tmpvar_30 * -2.0)
					           + 1.570796)));
					          tmpvar_28 = (tmpvar_30 * sign((tmpvar_27 / tmpvar_26)));
					          if ((abs(tmpvar_26) > (1e-08 * abs(tmpvar_27)))) {
					            if ((tmpvar_26 < 0.0)) {
					              if ((tmpvar_27 >= 0.0)) {
					                tmpvar_28 += 3.141593;
					              } else {
					                tmpvar_28 = (tmpvar_28 - 3.141593);
					              };
					            };
					          } else {
					            tmpvar_28 = (sign(tmpvar_27) * 1.570796);
					          };
					          highp vec3 tmpvar_31;
					          tmpvar_31.x = tmpvar_28;
					          tmpvar_31.y = sqrt(((tmpvar_26 * tmpvar_26) + (tmpvar_27 * tmpvar_27)));
					          tmpvar_31.z = (((0.299 * rgbColor_25.x) + (0.587 * rgbColor_25.y)) + (0.114 * rgbColor_25.z));
					          highp vec3 rgbColor_32;
					          rgbColor_32 = tmpvar_20.xyz;
					          highp float tmpvar_33;
					          tmpvar_33 = (((0.596 * rgbColor_32.x) - (0.275 * rgbColor_32.y)) - (0.321 * rgbColor_32.z));
					          highp float tmpvar_34;
					          tmpvar_34 = (((0.212 * rgbColor_32.x) - (0.523 * rgbColor_32.y)) + (0.311 * rgbColor_32.z));
					          highp float tmpvar_35;
					          highp float tmpvar_36;
					          tmpvar_36 = (min (abs(
					            (tmpvar_34 / tmpvar_33)
					          ), 1.0) / max (abs(
					            (tmpvar_34 / tmpvar_33)
					          ), 1.0));
					          highp float tmpvar_37;
					          tmpvar_37 = (tmpvar_36 * tmpvar_36);
					          tmpvar_37 = (((
					            ((((
					              ((((-0.01213232 * tmpvar_37) + 0.05368138) * tmpvar_37) - 0.1173503)
					             * tmpvar_37) + 0.1938925) * tmpvar_37) - 0.3326756)
					           * tmpvar_37) + 0.9999793) * tmpvar_36);
					          tmpvar_37 = (tmpvar_37 + (float(
					            (abs((tmpvar_34 / tmpvar_33)) > 1.0)
					          ) * (
					            (tmpvar_37 * -2.0)
					           + 1.570796)));
					          tmpvar_35 = (tmpvar_37 * sign((tmpvar_34 / tmpvar_33)));
					          if ((abs(tmpvar_33) > (1e-08 * abs(tmpvar_34)))) {
					            if ((tmpvar_33 < 0.0)) {
					              if ((tmpvar_34 >= 0.0)) {
					                tmpvar_35 += 3.141593;
					              } else {
					                tmpvar_35 = (tmpvar_35 - 3.141593);
					              };
					            };
					          } else {
					            tmpvar_35 = (sign(tmpvar_34) * 1.570796);
					          };
					          highp vec3 tmpvar_38;
					          tmpvar_38.x = tmpvar_35;
					          tmpvar_38.y = sqrt(((tmpvar_33 * tmpvar_33) + (tmpvar_34 * tmpvar_34)));
					          tmpvar_38.z = (((0.299 * rgbColor_32.x) + (0.587 * rgbColor_32.y)) + (0.114 * rgbColor_32.z));
					          highp float tmpvar_39;
					          tmpvar_39 = (tmpvar_31.y * sin(tmpvar_28));
					          highp float tmpvar_40;
					          tmpvar_40 = (tmpvar_31.y * cos(tmpvar_28));
					          highp vec3 tmpvar_41;
					          tmpvar_41.x = ((tmpvar_38.z + (0.956 * tmpvar_40)) + (0.621 * tmpvar_39));
					          tmpvar_41.y = ((tmpvar_38.z - (0.272 * tmpvar_40)) - (0.647 * tmpvar_39));
					          tmpvar_41.z = ((tmpvar_38.z - (1.107 * tmpvar_40)) + (1.704 * tmpvar_39));
					          finalColor_24.xyz = tmpvar_41;
					          finalColor_24 = mix (tmpvar_20, finalColor_24, tmpvar_21.wwww);
					        };
					        tmpvar_22 = mix (outputColor_1, finalColor_24, finalColor_24.wwww);
					      } else {
					        tmpvar_22 = tmpvar_20;
					      };
					    };
					    outputColor_1 = tmpvar_22;
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
					uniform highp float AccessoryCrop;
					uniform highp float BrowXOffset;
					uniform highp float BrowYOffset;
					uniform highp float AccessoryXOffset;
					uniform highp float AccessoryYOffset;
					uniform highp float HairXOffset;
					uniform highp float HairYOffset;
					uniform lowp vec4 HairColor;
					uniform lowp vec4 AccessoryColor;
					uniform highp int BrowBlendMode00;
					uniform highp int AccessoryBlendMode00;
					uniform highp int AccessoryBlendMode01;
					uniform highp int HairBlendMode00;
					uniform sampler2D AvatarDiffuseBase;
					uniform sampler2D BrowTex00;
					uniform sampler2D AccessoryTex00;
					uniform sampler2D AccessoryMask;
					uniform sampler2D HairTex00;
					uniform sampler2D HairTex01;
					uniform sampler2D HairTex02;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 outputColor_1;
					  outputColor_1 = texture2D (AvatarDiffuseBase, xlv_TEXCOORD0.xy);
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
					  lowp vec4 texColor_5;
					  texColor_5 = texture2D (BrowTex00, tmpvar_3);
					  lowp vec4 tmpvar_6;
					  if ((BrowBlendMode00 == 0)) {
					    lowp vec4 texColor_7;
					    texColor_7.w = texColor_5.w;
					    texColor_7.xyz = (texColor_5.xyz * HairColor.xyz);
					    tmpvar_6 = mix (outputColor_1, texColor_7, texColor_5.wwww);
					  } else {
					    tmpvar_6 = outputColor_1;
					  };
					  outputColor_1 = tmpvar_6;
					  if ((HairBlendMode00 == 0)) {
					    highp vec2 tmpvar_8;
					    tmpvar_8.x = HairXOffset;
					    tmpvar_8.y = HairYOffset;
					    highp vec2 tmpvar_9;
					    tmpvar_9 = (xlv_TEXCOORD0.xy + tmpvar_8);
					    lowp vec4 tmpvar_10;
					    tmpvar_10 = texture2D (HairTex00, tmpvar_9);
					    lowp vec4 texColor_11;
					    texColor_11.w = tmpvar_10.w;
					    texColor_11.xyz = (tmpvar_10.xyz * HairColor.xyz);
					    lowp vec4 tmpvar_12;
					    tmpvar_12 = mix (tmpvar_6, texColor_11, tmpvar_10.wwww);
					    lowp vec4 tmpvar_13;
					    tmpvar_13 = texture2D (HairTex01, tmpvar_9);
					    lowp vec4 tmpvar_14;
					    tmpvar_14 = mix (tmpvar_12, (1.0 - (
					      (1.0 - tmpvar_13)
					     * 
					      (1.0 - tmpvar_12)
					    )), tmpvar_13.wwww);
					    outputColor_1 = tmpvar_14;
					    lowp vec4 tmpvar_15;
					    tmpvar_15 = texture2D (HairTex02, tmpvar_9);
					    lowp vec4 tmpvar_16;
					    if ((tmpvar_15.w == 0.0)) {
					      tmpvar_16 = tmpvar_14;
					    } else {
					      tmpvar_16 = (tmpvar_15 * tmpvar_14);
					    };
					    outputColor_1 = mix (tmpvar_14, tmpvar_16, tmpvar_15.wwww);
					  };
					  highp vec2 tmpvar_17;
					  tmpvar_17.x = AccessoryXOffset;
					  tmpvar_17.y = AccessoryYOffset;
					  highp vec2 tmpvar_18;
					  highp vec2 tmpvar_19;
					  tmpvar_19 = (((
					    (xlv_TEXCOORD0.xy + tmpvar_17)
					   * vec2(AccessoryCrop)) - vec2(AccessoryCrop)) + vec2(1.0, 1.0));
					  if (((tmpvar_19.x < 0.0) || (tmpvar_19.y < 0.0))) {
					    tmpvar_18 = vec2(0.0, 0.0);
					  } else {
					    tmpvar_18 = tmpvar_19;
					  };
					  lowp vec4 tmpvar_20;
					  tmpvar_20 = texture2D (AccessoryTex00, tmpvar_18);
					  lowp vec4 tmpvar_21;
					  tmpvar_21 = texture2D (AccessoryMask, tmpvar_18);
					  if ((AccessoryBlendMode00 != 5)) {
					    lowp vec4 tmpvar_22;
					    if ((AccessoryBlendMode01 == 6)) {
					      lowp vec4 maskColor_23;
					      maskColor_23 = tmpvar_21;
					      if ((tmpvar_21.w > 0.1)) {
					        maskColor_23.xyz = (tmpvar_20.xyz * AccessoryColor.xyz);
					      } else {
					        maskColor_23.xyz = tmpvar_20.xyz;
					      };
					      maskColor_23.w = tmpvar_20.w;
					      tmpvar_22 = mix (outputColor_1, maskColor_23, tmpvar_20.wwww);
					    } else {
					      if ((AccessoryBlendMode01 == 4)) {
					        lowp vec4 finalColor_24;
					        finalColor_24 = tmpvar_20;
					        if (((tmpvar_20.w > 0.0) && (tmpvar_21.w > 0.001))) {
					          highp vec3 rgbColor_25;
					          rgbColor_25 = AccessoryColor.xyz;
					          highp float tmpvar_26;
					          tmpvar_26 = (((0.596 * rgbColor_25.x) - (0.275 * rgbColor_25.y)) - (0.321 * rgbColor_25.z));
					          highp float tmpvar_27;
					          tmpvar_27 = (((0.212 * rgbColor_25.x) - (0.523 * rgbColor_25.y)) + (0.311 * rgbColor_25.z));
					          highp float tmpvar_28;
					          highp float tmpvar_29;
					          tmpvar_29 = (min (abs(
					            (tmpvar_27 / tmpvar_26)
					          ), 1.0) / max (abs(
					            (tmpvar_27 / tmpvar_26)
					          ), 1.0));
					          highp float tmpvar_30;
					          tmpvar_30 = (tmpvar_29 * tmpvar_29);
					          tmpvar_30 = (((
					            ((((
					              ((((-0.01213232 * tmpvar_30) + 0.05368138) * tmpvar_30) - 0.1173503)
					             * tmpvar_30) + 0.1938925) * tmpvar_30) - 0.3326756)
					           * tmpvar_30) + 0.9999793) * tmpvar_29);
					          tmpvar_30 = (tmpvar_30 + (float(
					            (abs((tmpvar_27 / tmpvar_26)) > 1.0)
					          ) * (
					            (tmpvar_30 * -2.0)
					           + 1.570796)));
					          tmpvar_28 = (tmpvar_30 * sign((tmpvar_27 / tmpvar_26)));
					          if ((abs(tmpvar_26) > (1e-08 * abs(tmpvar_27)))) {
					            if ((tmpvar_26 < 0.0)) {
					              if ((tmpvar_27 >= 0.0)) {
					                tmpvar_28 += 3.141593;
					              } else {
					                tmpvar_28 = (tmpvar_28 - 3.141593);
					              };
					            };
					          } else {
					            tmpvar_28 = (sign(tmpvar_27) * 1.570796);
					          };
					          highp vec3 tmpvar_31;
					          tmpvar_31.x = tmpvar_28;
					          tmpvar_31.y = sqrt(((tmpvar_26 * tmpvar_26) + (tmpvar_27 * tmpvar_27)));
					          tmpvar_31.z = (((0.299 * rgbColor_25.x) + (0.587 * rgbColor_25.y)) + (0.114 * rgbColor_25.z));
					          highp vec3 rgbColor_32;
					          rgbColor_32 = tmpvar_20.xyz;
					          highp float tmpvar_33;
					          tmpvar_33 = (((0.596 * rgbColor_32.x) - (0.275 * rgbColor_32.y)) - (0.321 * rgbColor_32.z));
					          highp float tmpvar_34;
					          tmpvar_34 = (((0.212 * rgbColor_32.x) - (0.523 * rgbColor_32.y)) + (0.311 * rgbColor_32.z));
					          highp float tmpvar_35;
					          highp float tmpvar_36;
					          tmpvar_36 = (min (abs(
					            (tmpvar_34 / tmpvar_33)
					          ), 1.0) / max (abs(
					            (tmpvar_34 / tmpvar_33)
					          ), 1.0));
					          highp float tmpvar_37;
					          tmpvar_37 = (tmpvar_36 * tmpvar_36);
					          tmpvar_37 = (((
					            ((((
					              ((((-0.01213232 * tmpvar_37) + 0.05368138) * tmpvar_37) - 0.1173503)
					             * tmpvar_37) + 0.1938925) * tmpvar_37) - 0.3326756)
					           * tmpvar_37) + 0.9999793) * tmpvar_36);
					          tmpvar_37 = (tmpvar_37 + (float(
					            (abs((tmpvar_34 / tmpvar_33)) > 1.0)
					          ) * (
					            (tmpvar_37 * -2.0)
					           + 1.570796)));
					          tmpvar_35 = (tmpvar_37 * sign((tmpvar_34 / tmpvar_33)));
					          if ((abs(tmpvar_33) > (1e-08 * abs(tmpvar_34)))) {
					            if ((tmpvar_33 < 0.0)) {
					              if ((tmpvar_34 >= 0.0)) {
					                tmpvar_35 += 3.141593;
					              } else {
					                tmpvar_35 = (tmpvar_35 - 3.141593);
					              };
					            };
					          } else {
					            tmpvar_35 = (sign(tmpvar_34) * 1.570796);
					          };
					          highp vec3 tmpvar_38;
					          tmpvar_38.x = tmpvar_35;
					          tmpvar_38.y = sqrt(((tmpvar_33 * tmpvar_33) + (tmpvar_34 * tmpvar_34)));
					          tmpvar_38.z = (((0.299 * rgbColor_32.x) + (0.587 * rgbColor_32.y)) + (0.114 * rgbColor_32.z));
					          highp float tmpvar_39;
					          tmpvar_39 = (tmpvar_31.y * sin(tmpvar_28));
					          highp float tmpvar_40;
					          tmpvar_40 = (tmpvar_31.y * cos(tmpvar_28));
					          highp vec3 tmpvar_41;
					          tmpvar_41.x = ((tmpvar_38.z + (0.956 * tmpvar_40)) + (0.621 * tmpvar_39));
					          tmpvar_41.y = ((tmpvar_38.z - (0.272 * tmpvar_40)) - (0.647 * tmpvar_39));
					          tmpvar_41.z = ((tmpvar_38.z - (1.107 * tmpvar_40)) + (1.704 * tmpvar_39));
					          finalColor_24.xyz = tmpvar_41;
					          finalColor_24 = mix (tmpvar_20, finalColor_24, tmpvar_21.wwww);
					        };
					        tmpvar_22 = mix (outputColor_1, finalColor_24, finalColor_24.wwww);
					      } else {
					        tmpvar_22 = tmpvar_20;
					      };
					    };
					    outputColor_1 = tmpvar_22;
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
					uniform highp float AccessoryCrop;
					uniform highp float BrowXOffset;
					uniform highp float BrowYOffset;
					uniform highp float AccessoryXOffset;
					uniform highp float AccessoryYOffset;
					uniform highp float HairXOffset;
					uniform highp float HairYOffset;
					uniform lowp vec4 HairColor;
					uniform lowp vec4 AccessoryColor;
					uniform highp int BrowBlendMode00;
					uniform highp int AccessoryBlendMode00;
					uniform highp int AccessoryBlendMode01;
					uniform highp int HairBlendMode00;
					uniform sampler2D AvatarDiffuseBase;
					uniform sampler2D BrowTex00;
					uniform sampler2D AccessoryTex00;
					uniform sampler2D AccessoryMask;
					uniform sampler2D HairTex00;
					uniform sampler2D HairTex01;
					uniform sampler2D HairTex02;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 outputColor_1;
					  outputColor_1 = texture2D (AvatarDiffuseBase, xlv_TEXCOORD0.xy);
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
					  lowp vec4 texColor_5;
					  texColor_5 = texture2D (BrowTex00, tmpvar_3);
					  lowp vec4 tmpvar_6;
					  if ((BrowBlendMode00 == 0)) {
					    lowp vec4 texColor_7;
					    texColor_7.w = texColor_5.w;
					    texColor_7.xyz = (texColor_5.xyz * HairColor.xyz);
					    tmpvar_6 = mix (outputColor_1, texColor_7, texColor_5.wwww);
					  } else {
					    tmpvar_6 = outputColor_1;
					  };
					  outputColor_1 = tmpvar_6;
					  if ((HairBlendMode00 == 0)) {
					    highp vec2 tmpvar_8;
					    tmpvar_8.x = HairXOffset;
					    tmpvar_8.y = HairYOffset;
					    highp vec2 tmpvar_9;
					    tmpvar_9 = (xlv_TEXCOORD0.xy + tmpvar_8);
					    lowp vec4 tmpvar_10;
					    tmpvar_10 = texture2D (HairTex00, tmpvar_9);
					    lowp vec4 texColor_11;
					    texColor_11.w = tmpvar_10.w;
					    texColor_11.xyz = (tmpvar_10.xyz * HairColor.xyz);
					    lowp vec4 tmpvar_12;
					    tmpvar_12 = mix (tmpvar_6, texColor_11, tmpvar_10.wwww);
					    lowp vec4 tmpvar_13;
					    tmpvar_13 = texture2D (HairTex01, tmpvar_9);
					    lowp vec4 tmpvar_14;
					    tmpvar_14 = mix (tmpvar_12, (1.0 - (
					      (1.0 - tmpvar_13)
					     * 
					      (1.0 - tmpvar_12)
					    )), tmpvar_13.wwww);
					    outputColor_1 = tmpvar_14;
					    lowp vec4 tmpvar_15;
					    tmpvar_15 = texture2D (HairTex02, tmpvar_9);
					    lowp vec4 tmpvar_16;
					    if ((tmpvar_15.w == 0.0)) {
					      tmpvar_16 = tmpvar_14;
					    } else {
					      tmpvar_16 = (tmpvar_15 * tmpvar_14);
					    };
					    outputColor_1 = mix (tmpvar_14, tmpvar_16, tmpvar_15.wwww);
					  };
					  highp vec2 tmpvar_17;
					  tmpvar_17.x = AccessoryXOffset;
					  tmpvar_17.y = AccessoryYOffset;
					  highp vec2 tmpvar_18;
					  highp vec2 tmpvar_19;
					  tmpvar_19 = (((
					    (xlv_TEXCOORD0.xy + tmpvar_17)
					   * vec2(AccessoryCrop)) - vec2(AccessoryCrop)) + vec2(1.0, 1.0));
					  if (((tmpvar_19.x < 0.0) || (tmpvar_19.y < 0.0))) {
					    tmpvar_18 = vec2(0.0, 0.0);
					  } else {
					    tmpvar_18 = tmpvar_19;
					  };
					  lowp vec4 tmpvar_20;
					  tmpvar_20 = texture2D (AccessoryTex00, tmpvar_18);
					  lowp vec4 tmpvar_21;
					  tmpvar_21 = texture2D (AccessoryMask, tmpvar_18);
					  if ((AccessoryBlendMode00 != 5)) {
					    lowp vec4 tmpvar_22;
					    if ((AccessoryBlendMode01 == 6)) {
					      lowp vec4 maskColor_23;
					      maskColor_23 = tmpvar_21;
					      if ((tmpvar_21.w > 0.1)) {
					        maskColor_23.xyz = (tmpvar_20.xyz * AccessoryColor.xyz);
					      } else {
					        maskColor_23.xyz = tmpvar_20.xyz;
					      };
					      maskColor_23.w = tmpvar_20.w;
					      tmpvar_22 = mix (outputColor_1, maskColor_23, tmpvar_20.wwww);
					    } else {
					      if ((AccessoryBlendMode01 == 4)) {
					        lowp vec4 finalColor_24;
					        finalColor_24 = tmpvar_20;
					        if (((tmpvar_20.w > 0.0) && (tmpvar_21.w > 0.001))) {
					          highp vec3 rgbColor_25;
					          rgbColor_25 = AccessoryColor.xyz;
					          highp float tmpvar_26;
					          tmpvar_26 = (((0.596 * rgbColor_25.x) - (0.275 * rgbColor_25.y)) - (0.321 * rgbColor_25.z));
					          highp float tmpvar_27;
					          tmpvar_27 = (((0.212 * rgbColor_25.x) - (0.523 * rgbColor_25.y)) + (0.311 * rgbColor_25.z));
					          highp float tmpvar_28;
					          highp float tmpvar_29;
					          tmpvar_29 = (min (abs(
					            (tmpvar_27 / tmpvar_26)
					          ), 1.0) / max (abs(
					            (tmpvar_27 / tmpvar_26)
					          ), 1.0));
					          highp float tmpvar_30;
					          tmpvar_30 = (tmpvar_29 * tmpvar_29);
					          tmpvar_30 = (((
					            ((((
					              ((((-0.01213232 * tmpvar_30) + 0.05368138) * tmpvar_30) - 0.1173503)
					             * tmpvar_30) + 0.1938925) * tmpvar_30) - 0.3326756)
					           * tmpvar_30) + 0.9999793) * tmpvar_29);
					          tmpvar_30 = (tmpvar_30 + (float(
					            (abs((tmpvar_27 / tmpvar_26)) > 1.0)
					          ) * (
					            (tmpvar_30 * -2.0)
					           + 1.570796)));
					          tmpvar_28 = (tmpvar_30 * sign((tmpvar_27 / tmpvar_26)));
					          if ((abs(tmpvar_26) > (1e-08 * abs(tmpvar_27)))) {
					            if ((tmpvar_26 < 0.0)) {
					              if ((tmpvar_27 >= 0.0)) {
					                tmpvar_28 += 3.141593;
					              } else {
					                tmpvar_28 = (tmpvar_28 - 3.141593);
					              };
					            };
					          } else {
					            tmpvar_28 = (sign(tmpvar_27) * 1.570796);
					          };
					          highp vec3 tmpvar_31;
					          tmpvar_31.x = tmpvar_28;
					          tmpvar_31.y = sqrt(((tmpvar_26 * tmpvar_26) + (tmpvar_27 * tmpvar_27)));
					          tmpvar_31.z = (((0.299 * rgbColor_25.x) + (0.587 * rgbColor_25.y)) + (0.114 * rgbColor_25.z));
					          highp vec3 rgbColor_32;
					          rgbColor_32 = tmpvar_20.xyz;
					          highp float tmpvar_33;
					          tmpvar_33 = (((0.596 * rgbColor_32.x) - (0.275 * rgbColor_32.y)) - (0.321 * rgbColor_32.z));
					          highp float tmpvar_34;
					          tmpvar_34 = (((0.212 * rgbColor_32.x) - (0.523 * rgbColor_32.y)) + (0.311 * rgbColor_32.z));
					          highp float tmpvar_35;
					          highp float tmpvar_36;
					          tmpvar_36 = (min (abs(
					            (tmpvar_34 / tmpvar_33)
					          ), 1.0) / max (abs(
					            (tmpvar_34 / tmpvar_33)
					          ), 1.0));
					          highp float tmpvar_37;
					          tmpvar_37 = (tmpvar_36 * tmpvar_36);
					          tmpvar_37 = (((
					            ((((
					              ((((-0.01213232 * tmpvar_37) + 0.05368138) * tmpvar_37) - 0.1173503)
					             * tmpvar_37) + 0.1938925) * tmpvar_37) - 0.3326756)
					           * tmpvar_37) + 0.9999793) * tmpvar_36);
					          tmpvar_37 = (tmpvar_37 + (float(
					            (abs((tmpvar_34 / tmpvar_33)) > 1.0)
					          ) * (
					            (tmpvar_37 * -2.0)
					           + 1.570796)));
					          tmpvar_35 = (tmpvar_37 * sign((tmpvar_34 / tmpvar_33)));
					          if ((abs(tmpvar_33) > (1e-08 * abs(tmpvar_34)))) {
					            if ((tmpvar_33 < 0.0)) {
					              if ((tmpvar_34 >= 0.0)) {
					                tmpvar_35 += 3.141593;
					              } else {
					                tmpvar_35 = (tmpvar_35 - 3.141593);
					              };
					            };
					          } else {
					            tmpvar_35 = (sign(tmpvar_34) * 1.570796);
					          };
					          highp vec3 tmpvar_38;
					          tmpvar_38.x = tmpvar_35;
					          tmpvar_38.y = sqrt(((tmpvar_33 * tmpvar_33) + (tmpvar_34 * tmpvar_34)));
					          tmpvar_38.z = (((0.299 * rgbColor_32.x) + (0.587 * rgbColor_32.y)) + (0.114 * rgbColor_32.z));
					          highp float tmpvar_39;
					          tmpvar_39 = (tmpvar_31.y * sin(tmpvar_28));
					          highp float tmpvar_40;
					          tmpvar_40 = (tmpvar_31.y * cos(tmpvar_28));
					          highp vec3 tmpvar_41;
					          tmpvar_41.x = ((tmpvar_38.z + (0.956 * tmpvar_40)) + (0.621 * tmpvar_39));
					          tmpvar_41.y = ((tmpvar_38.z - (0.272 * tmpvar_40)) - (0.647 * tmpvar_39));
					          tmpvar_41.z = ((tmpvar_38.z - (1.107 * tmpvar_40)) + (1.704 * tmpvar_39));
					          finalColor_24.xyz = tmpvar_41;
					          finalColor_24 = mix (tmpvar_20, finalColor_24, tmpvar_21.wwww);
					        };
					        tmpvar_22 = mix (outputColor_1, finalColor_24, finalColor_24.wwww);
					      } else {
					        tmpvar_22 = tmpvar_20;
					      };
					    };
					    outputColor_1 = tmpvar_22;
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