Shader "NewAvatarGeo" {
Properties {
 AccessoryColor ("Accessory Color", Color) = (1.000000,1.000000,1.000000,1.000000)
 AccessoryXOffset ("Accessory X Offset", Float) = 0.000000
 AccessoryYOffset ("Accessory Y Offset", Float) = 0.000000
 AccessoryBlendMode00 ("Prop Blend Mode", Float) = 0.000000
 AccessoryBlendMode01 ("Prop Mask Mode", Float) = 0.000000
 AccessoryCrop ("Accessory crop", Float) = 1.000000
 AccessoryTex00 ("Prop", 2D) = "alpha" { }
 AccessoryTex01 ("Prop Diffuse Layer", 2D) = "alpha" { }
 AccessoryMask ("Prop Mask", 2D) = "alpha" { }
 _MainTex ("Main Texture", 2D) = "alpha" { }
}
SubShader { 
 Pass {
  GpuProgramID 41879
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
					uniform lowp vec4 AccessoryColor;
					uniform highp int AccessoryBlendMode00;
					uniform highp int AccessoryBlendMode01;
					uniform sampler2D AccessoryTex00;
					uniform sampler2D AccessoryTex01;
					uniform sampler2D AccessoryMask;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 outputColor_1;
					  outputColor_1 = vec4(0.0, 0.0, 0.0, 0.0);
					  if ((AccessoryBlendMode00 != 5)) {
					    if ((AccessoryBlendMode01 == 1)) {
					      lowp vec4 tmpvar_2;
					      tmpvar_2 = texture2D (AccessoryTex00, xlv_TEXCOORD0.xy);
					      lowp vec4 tmpvar_3;
					      tmpvar_3 = texture2D (AccessoryTex01, xlv_TEXCOORD0.xy);
					      lowp vec4 maskColor_4;
					      maskColor_4.xyz = (tmpvar_2.xyz * AccessoryColor.xyz);
					      maskColor_4.w = tmpvar_2.w;
					      lowp vec4 tmpvar_5;
					      tmpvar_5 = (maskColor_4 * tmpvar_2.wwww);
					      outputColor_1 = tmpvar_5;
					      lowp vec4 tmpvar_6;
					      if ((tmpvar_3.w == 0.0)) {
					        tmpvar_6 = tmpvar_5;
					      } else {
					        tmpvar_6 = (tmpvar_3 * tmpvar_5);
					      };
					      outputColor_1 = mix (tmpvar_5, tmpvar_6, tmpvar_3.wwww);
					    } else {
					      if ((AccessoryBlendMode01 == 3)) {
					        lowp vec4 tmpvar_7;
					        tmpvar_7 = texture2D (AccessoryTex00, xlv_TEXCOORD0.xy);
					        lowp vec4 tmpvar_8;
					        tmpvar_8 = texture2D (AccessoryTex01, xlv_TEXCOORD0.xy);
					        lowp vec4 maskColor_9;
					        maskColor_9.xyz = (tmpvar_7.xyz * AccessoryColor.xyz);
					        maskColor_9.w = tmpvar_7.w;
					        outputColor_1 = mix (mix (outputColor_1, maskColor_9, tmpvar_7.wwww), tmpvar_8, tmpvar_8.wwww);
					      } else {
					        lowp vec4 tmpvar_10;
					        tmpvar_10 = texture2D (AccessoryTex00, xlv_TEXCOORD0.xy);
					        lowp vec4 tmpvar_11;
					        tmpvar_11 = texture2D (AccessoryMask, xlv_TEXCOORD0.xy);
					        if ((tmpvar_10.w > 0.01)) {
					          lowp vec4 finalColor_12;
					          finalColor_12 = tmpvar_10;
					          if (((tmpvar_10.w > 0.0) && (tmpvar_11.w > 0.001))) {
					            highp vec3 rgbColor_13;
					            rgbColor_13 = AccessoryColor.xyz;
					            highp float tmpvar_14;
					            tmpvar_14 = (((0.596 * rgbColor_13.x) - (0.275 * rgbColor_13.y)) - (0.321 * rgbColor_13.z));
					            highp float tmpvar_15;
					            tmpvar_15 = (((0.212 * rgbColor_13.x) - (0.523 * rgbColor_13.y)) + (0.311 * rgbColor_13.z));
					            highp float tmpvar_16;
					            highp float tmpvar_17;
					            tmpvar_17 = (min (abs(
					              (tmpvar_15 / tmpvar_14)
					            ), 1.0) / max (abs(
					              (tmpvar_15 / tmpvar_14)
					            ), 1.0));
					            highp float tmpvar_18;
					            tmpvar_18 = (tmpvar_17 * tmpvar_17);
					            tmpvar_18 = (((
					              ((((
					                ((((-0.01213232 * tmpvar_18) + 0.05368138) * tmpvar_18) - 0.1173503)
					               * tmpvar_18) + 0.1938925) * tmpvar_18) - 0.3326756)
					             * tmpvar_18) + 0.9999793) * tmpvar_17);
					            tmpvar_18 = (tmpvar_18 + (float(
					              (abs((tmpvar_15 / tmpvar_14)) > 1.0)
					            ) * (
					              (tmpvar_18 * -2.0)
					             + 1.570796)));
					            tmpvar_16 = (tmpvar_18 * sign((tmpvar_15 / tmpvar_14)));
					            if ((abs(tmpvar_14) > (1e-08 * abs(tmpvar_15)))) {
					              if ((tmpvar_14 < 0.0)) {
					                if ((tmpvar_15 >= 0.0)) {
					                  tmpvar_16 += 3.141593;
					                } else {
					                  tmpvar_16 = (tmpvar_16 - 3.141593);
					                };
					              };
					            } else {
					              tmpvar_16 = (sign(tmpvar_15) * 1.570796);
					            };
					            highp vec3 tmpvar_19;
					            tmpvar_19.x = tmpvar_16;
					            tmpvar_19.y = sqrt(((tmpvar_14 * tmpvar_14) + (tmpvar_15 * tmpvar_15)));
					            tmpvar_19.z = (((0.299 * rgbColor_13.x) + (0.587 * rgbColor_13.y)) + (0.114 * rgbColor_13.z));
					            highp vec3 rgbColor_20;
					            rgbColor_20 = tmpvar_10.xyz;
					            highp float tmpvar_21;
					            tmpvar_21 = (((0.596 * rgbColor_20.x) - (0.275 * rgbColor_20.y)) - (0.321 * rgbColor_20.z));
					            highp float tmpvar_22;
					            tmpvar_22 = (((0.212 * rgbColor_20.x) - (0.523 * rgbColor_20.y)) + (0.311 * rgbColor_20.z));
					            highp float tmpvar_23;
					            highp float tmpvar_24;
					            tmpvar_24 = (min (abs(
					              (tmpvar_22 / tmpvar_21)
					            ), 1.0) / max (abs(
					              (tmpvar_22 / tmpvar_21)
					            ), 1.0));
					            highp float tmpvar_25;
					            tmpvar_25 = (tmpvar_24 * tmpvar_24);
					            tmpvar_25 = (((
					              ((((
					                ((((-0.01213232 * tmpvar_25) + 0.05368138) * tmpvar_25) - 0.1173503)
					               * tmpvar_25) + 0.1938925) * tmpvar_25) - 0.3326756)
					             * tmpvar_25) + 0.9999793) * tmpvar_24);
					            tmpvar_25 = (tmpvar_25 + (float(
					              (abs((tmpvar_22 / tmpvar_21)) > 1.0)
					            ) * (
					              (tmpvar_25 * -2.0)
					             + 1.570796)));
					            tmpvar_23 = (tmpvar_25 * sign((tmpvar_22 / tmpvar_21)));
					            if ((abs(tmpvar_21) > (1e-08 * abs(tmpvar_22)))) {
					              if ((tmpvar_21 < 0.0)) {
					                if ((tmpvar_22 >= 0.0)) {
					                  tmpvar_23 += 3.141593;
					                } else {
					                  tmpvar_23 = (tmpvar_23 - 3.141593);
					                };
					              };
					            } else {
					              tmpvar_23 = (sign(tmpvar_22) * 1.570796);
					            };
					            highp vec3 tmpvar_26;
					            tmpvar_26.x = tmpvar_23;
					            tmpvar_26.y = sqrt(((tmpvar_21 * tmpvar_21) + (tmpvar_22 * tmpvar_22)));
					            tmpvar_26.z = (((0.299 * rgbColor_20.x) + (0.587 * rgbColor_20.y)) + (0.114 * rgbColor_20.z));
					            highp float tmpvar_27;
					            tmpvar_27 = (tmpvar_19.y * sin(tmpvar_16));
					            highp float tmpvar_28;
					            tmpvar_28 = (tmpvar_19.y * cos(tmpvar_16));
					            highp vec3 tmpvar_29;
					            tmpvar_29.x = ((tmpvar_26.z + (0.956 * tmpvar_28)) + (0.621 * tmpvar_27));
					            tmpvar_29.y = ((tmpvar_26.z - (0.272 * tmpvar_28)) - (0.647 * tmpvar_27));
					            tmpvar_29.z = ((tmpvar_26.z - (1.107 * tmpvar_28)) + (1.704 * tmpvar_27));
					            finalColor_12.xyz = tmpvar_29;
					            finalColor_12 = mix (tmpvar_10, finalColor_12, tmpvar_11.wwww);
					          };
					          outputColor_1 = mix (tmpvar_10, finalColor_12, finalColor_12.wwww);
					        };
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
					uniform lowp vec4 AccessoryColor;
					uniform highp int AccessoryBlendMode00;
					uniform highp int AccessoryBlendMode01;
					uniform sampler2D AccessoryTex00;
					uniform sampler2D AccessoryTex01;
					uniform sampler2D AccessoryMask;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 outputColor_1;
					  outputColor_1 = vec4(0.0, 0.0, 0.0, 0.0);
					  if ((AccessoryBlendMode00 != 5)) {
					    if ((AccessoryBlendMode01 == 1)) {
					      lowp vec4 tmpvar_2;
					      tmpvar_2 = texture2D (AccessoryTex00, xlv_TEXCOORD0.xy);
					      lowp vec4 tmpvar_3;
					      tmpvar_3 = texture2D (AccessoryTex01, xlv_TEXCOORD0.xy);
					      lowp vec4 maskColor_4;
					      maskColor_4.xyz = (tmpvar_2.xyz * AccessoryColor.xyz);
					      maskColor_4.w = tmpvar_2.w;
					      lowp vec4 tmpvar_5;
					      tmpvar_5 = (maskColor_4 * tmpvar_2.wwww);
					      outputColor_1 = tmpvar_5;
					      lowp vec4 tmpvar_6;
					      if ((tmpvar_3.w == 0.0)) {
					        tmpvar_6 = tmpvar_5;
					      } else {
					        tmpvar_6 = (tmpvar_3 * tmpvar_5);
					      };
					      outputColor_1 = mix (tmpvar_5, tmpvar_6, tmpvar_3.wwww);
					    } else {
					      if ((AccessoryBlendMode01 == 3)) {
					        lowp vec4 tmpvar_7;
					        tmpvar_7 = texture2D (AccessoryTex00, xlv_TEXCOORD0.xy);
					        lowp vec4 tmpvar_8;
					        tmpvar_8 = texture2D (AccessoryTex01, xlv_TEXCOORD0.xy);
					        lowp vec4 maskColor_9;
					        maskColor_9.xyz = (tmpvar_7.xyz * AccessoryColor.xyz);
					        maskColor_9.w = tmpvar_7.w;
					        outputColor_1 = mix (mix (outputColor_1, maskColor_9, tmpvar_7.wwww), tmpvar_8, tmpvar_8.wwww);
					      } else {
					        lowp vec4 tmpvar_10;
					        tmpvar_10 = texture2D (AccessoryTex00, xlv_TEXCOORD0.xy);
					        lowp vec4 tmpvar_11;
					        tmpvar_11 = texture2D (AccessoryMask, xlv_TEXCOORD0.xy);
					        if ((tmpvar_10.w > 0.01)) {
					          lowp vec4 finalColor_12;
					          finalColor_12 = tmpvar_10;
					          if (((tmpvar_10.w > 0.0) && (tmpvar_11.w > 0.001))) {
					            highp vec3 rgbColor_13;
					            rgbColor_13 = AccessoryColor.xyz;
					            highp float tmpvar_14;
					            tmpvar_14 = (((0.596 * rgbColor_13.x) - (0.275 * rgbColor_13.y)) - (0.321 * rgbColor_13.z));
					            highp float tmpvar_15;
					            tmpvar_15 = (((0.212 * rgbColor_13.x) - (0.523 * rgbColor_13.y)) + (0.311 * rgbColor_13.z));
					            highp float tmpvar_16;
					            highp float tmpvar_17;
					            tmpvar_17 = (min (abs(
					              (tmpvar_15 / tmpvar_14)
					            ), 1.0) / max (abs(
					              (tmpvar_15 / tmpvar_14)
					            ), 1.0));
					            highp float tmpvar_18;
					            tmpvar_18 = (tmpvar_17 * tmpvar_17);
					            tmpvar_18 = (((
					              ((((
					                ((((-0.01213232 * tmpvar_18) + 0.05368138) * tmpvar_18) - 0.1173503)
					               * tmpvar_18) + 0.1938925) * tmpvar_18) - 0.3326756)
					             * tmpvar_18) + 0.9999793) * tmpvar_17);
					            tmpvar_18 = (tmpvar_18 + (float(
					              (abs((tmpvar_15 / tmpvar_14)) > 1.0)
					            ) * (
					              (tmpvar_18 * -2.0)
					             + 1.570796)));
					            tmpvar_16 = (tmpvar_18 * sign((tmpvar_15 / tmpvar_14)));
					            if ((abs(tmpvar_14) > (1e-08 * abs(tmpvar_15)))) {
					              if ((tmpvar_14 < 0.0)) {
					                if ((tmpvar_15 >= 0.0)) {
					                  tmpvar_16 += 3.141593;
					                } else {
					                  tmpvar_16 = (tmpvar_16 - 3.141593);
					                };
					              };
					            } else {
					              tmpvar_16 = (sign(tmpvar_15) * 1.570796);
					            };
					            highp vec3 tmpvar_19;
					            tmpvar_19.x = tmpvar_16;
					            tmpvar_19.y = sqrt(((tmpvar_14 * tmpvar_14) + (tmpvar_15 * tmpvar_15)));
					            tmpvar_19.z = (((0.299 * rgbColor_13.x) + (0.587 * rgbColor_13.y)) + (0.114 * rgbColor_13.z));
					            highp vec3 rgbColor_20;
					            rgbColor_20 = tmpvar_10.xyz;
					            highp float tmpvar_21;
					            tmpvar_21 = (((0.596 * rgbColor_20.x) - (0.275 * rgbColor_20.y)) - (0.321 * rgbColor_20.z));
					            highp float tmpvar_22;
					            tmpvar_22 = (((0.212 * rgbColor_20.x) - (0.523 * rgbColor_20.y)) + (0.311 * rgbColor_20.z));
					            highp float tmpvar_23;
					            highp float tmpvar_24;
					            tmpvar_24 = (min (abs(
					              (tmpvar_22 / tmpvar_21)
					            ), 1.0) / max (abs(
					              (tmpvar_22 / tmpvar_21)
					            ), 1.0));
					            highp float tmpvar_25;
					            tmpvar_25 = (tmpvar_24 * tmpvar_24);
					            tmpvar_25 = (((
					              ((((
					                ((((-0.01213232 * tmpvar_25) + 0.05368138) * tmpvar_25) - 0.1173503)
					               * tmpvar_25) + 0.1938925) * tmpvar_25) - 0.3326756)
					             * tmpvar_25) + 0.9999793) * tmpvar_24);
					            tmpvar_25 = (tmpvar_25 + (float(
					              (abs((tmpvar_22 / tmpvar_21)) > 1.0)
					            ) * (
					              (tmpvar_25 * -2.0)
					             + 1.570796)));
					            tmpvar_23 = (tmpvar_25 * sign((tmpvar_22 / tmpvar_21)));
					            if ((abs(tmpvar_21) > (1e-08 * abs(tmpvar_22)))) {
					              if ((tmpvar_21 < 0.0)) {
					                if ((tmpvar_22 >= 0.0)) {
					                  tmpvar_23 += 3.141593;
					                } else {
					                  tmpvar_23 = (tmpvar_23 - 3.141593);
					                };
					              };
					            } else {
					              tmpvar_23 = (sign(tmpvar_22) * 1.570796);
					            };
					            highp vec3 tmpvar_26;
					            tmpvar_26.x = tmpvar_23;
					            tmpvar_26.y = sqrt(((tmpvar_21 * tmpvar_21) + (tmpvar_22 * tmpvar_22)));
					            tmpvar_26.z = (((0.299 * rgbColor_20.x) + (0.587 * rgbColor_20.y)) + (0.114 * rgbColor_20.z));
					            highp float tmpvar_27;
					            tmpvar_27 = (tmpvar_19.y * sin(tmpvar_16));
					            highp float tmpvar_28;
					            tmpvar_28 = (tmpvar_19.y * cos(tmpvar_16));
					            highp vec3 tmpvar_29;
					            tmpvar_29.x = ((tmpvar_26.z + (0.956 * tmpvar_28)) + (0.621 * tmpvar_27));
					            tmpvar_29.y = ((tmpvar_26.z - (0.272 * tmpvar_28)) - (0.647 * tmpvar_27));
					            tmpvar_29.z = ((tmpvar_26.z - (1.107 * tmpvar_28)) + (1.704 * tmpvar_27));
					            finalColor_12.xyz = tmpvar_29;
					            finalColor_12 = mix (tmpvar_10, finalColor_12, tmpvar_11.wwww);
					          };
					          outputColor_1 = mix (tmpvar_10, finalColor_12, finalColor_12.wwww);
					        };
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
					uniform lowp vec4 AccessoryColor;
					uniform highp int AccessoryBlendMode00;
					uniform highp int AccessoryBlendMode01;
					uniform sampler2D AccessoryTex00;
					uniform sampler2D AccessoryTex01;
					uniform sampler2D AccessoryMask;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 outputColor_1;
					  outputColor_1 = vec4(0.0, 0.0, 0.0, 0.0);
					  if ((AccessoryBlendMode00 != 5)) {
					    if ((AccessoryBlendMode01 == 1)) {
					      lowp vec4 tmpvar_2;
					      tmpvar_2 = texture2D (AccessoryTex00, xlv_TEXCOORD0.xy);
					      lowp vec4 tmpvar_3;
					      tmpvar_3 = texture2D (AccessoryTex01, xlv_TEXCOORD0.xy);
					      lowp vec4 maskColor_4;
					      maskColor_4.xyz = (tmpvar_2.xyz * AccessoryColor.xyz);
					      maskColor_4.w = tmpvar_2.w;
					      lowp vec4 tmpvar_5;
					      tmpvar_5 = (maskColor_4 * tmpvar_2.wwww);
					      outputColor_1 = tmpvar_5;
					      lowp vec4 tmpvar_6;
					      if ((tmpvar_3.w == 0.0)) {
					        tmpvar_6 = tmpvar_5;
					      } else {
					        tmpvar_6 = (tmpvar_3 * tmpvar_5);
					      };
					      outputColor_1 = mix (tmpvar_5, tmpvar_6, tmpvar_3.wwww);
					    } else {
					      if ((AccessoryBlendMode01 == 3)) {
					        lowp vec4 tmpvar_7;
					        tmpvar_7 = texture2D (AccessoryTex00, xlv_TEXCOORD0.xy);
					        lowp vec4 tmpvar_8;
					        tmpvar_8 = texture2D (AccessoryTex01, xlv_TEXCOORD0.xy);
					        lowp vec4 maskColor_9;
					        maskColor_9.xyz = (tmpvar_7.xyz * AccessoryColor.xyz);
					        maskColor_9.w = tmpvar_7.w;
					        outputColor_1 = mix (mix (outputColor_1, maskColor_9, tmpvar_7.wwww), tmpvar_8, tmpvar_8.wwww);
					      } else {
					        lowp vec4 tmpvar_10;
					        tmpvar_10 = texture2D (AccessoryTex00, xlv_TEXCOORD0.xy);
					        lowp vec4 tmpvar_11;
					        tmpvar_11 = texture2D (AccessoryMask, xlv_TEXCOORD0.xy);
					        if ((tmpvar_10.w > 0.01)) {
					          lowp vec4 finalColor_12;
					          finalColor_12 = tmpvar_10;
					          if (((tmpvar_10.w > 0.0) && (tmpvar_11.w > 0.001))) {
					            highp vec3 rgbColor_13;
					            rgbColor_13 = AccessoryColor.xyz;
					            highp float tmpvar_14;
					            tmpvar_14 = (((0.596 * rgbColor_13.x) - (0.275 * rgbColor_13.y)) - (0.321 * rgbColor_13.z));
					            highp float tmpvar_15;
					            tmpvar_15 = (((0.212 * rgbColor_13.x) - (0.523 * rgbColor_13.y)) + (0.311 * rgbColor_13.z));
					            highp float tmpvar_16;
					            highp float tmpvar_17;
					            tmpvar_17 = (min (abs(
					              (tmpvar_15 / tmpvar_14)
					            ), 1.0) / max (abs(
					              (tmpvar_15 / tmpvar_14)
					            ), 1.0));
					            highp float tmpvar_18;
					            tmpvar_18 = (tmpvar_17 * tmpvar_17);
					            tmpvar_18 = (((
					              ((((
					                ((((-0.01213232 * tmpvar_18) + 0.05368138) * tmpvar_18) - 0.1173503)
					               * tmpvar_18) + 0.1938925) * tmpvar_18) - 0.3326756)
					             * tmpvar_18) + 0.9999793) * tmpvar_17);
					            tmpvar_18 = (tmpvar_18 + (float(
					              (abs((tmpvar_15 / tmpvar_14)) > 1.0)
					            ) * (
					              (tmpvar_18 * -2.0)
					             + 1.570796)));
					            tmpvar_16 = (tmpvar_18 * sign((tmpvar_15 / tmpvar_14)));
					            if ((abs(tmpvar_14) > (1e-08 * abs(tmpvar_15)))) {
					              if ((tmpvar_14 < 0.0)) {
					                if ((tmpvar_15 >= 0.0)) {
					                  tmpvar_16 += 3.141593;
					                } else {
					                  tmpvar_16 = (tmpvar_16 - 3.141593);
					                };
					              };
					            } else {
					              tmpvar_16 = (sign(tmpvar_15) * 1.570796);
					            };
					            highp vec3 tmpvar_19;
					            tmpvar_19.x = tmpvar_16;
					            tmpvar_19.y = sqrt(((tmpvar_14 * tmpvar_14) + (tmpvar_15 * tmpvar_15)));
					            tmpvar_19.z = (((0.299 * rgbColor_13.x) + (0.587 * rgbColor_13.y)) + (0.114 * rgbColor_13.z));
					            highp vec3 rgbColor_20;
					            rgbColor_20 = tmpvar_10.xyz;
					            highp float tmpvar_21;
					            tmpvar_21 = (((0.596 * rgbColor_20.x) - (0.275 * rgbColor_20.y)) - (0.321 * rgbColor_20.z));
					            highp float tmpvar_22;
					            tmpvar_22 = (((0.212 * rgbColor_20.x) - (0.523 * rgbColor_20.y)) + (0.311 * rgbColor_20.z));
					            highp float tmpvar_23;
					            highp float tmpvar_24;
					            tmpvar_24 = (min (abs(
					              (tmpvar_22 / tmpvar_21)
					            ), 1.0) / max (abs(
					              (tmpvar_22 / tmpvar_21)
					            ), 1.0));
					            highp float tmpvar_25;
					            tmpvar_25 = (tmpvar_24 * tmpvar_24);
					            tmpvar_25 = (((
					              ((((
					                ((((-0.01213232 * tmpvar_25) + 0.05368138) * tmpvar_25) - 0.1173503)
					               * tmpvar_25) + 0.1938925) * tmpvar_25) - 0.3326756)
					             * tmpvar_25) + 0.9999793) * tmpvar_24);
					            tmpvar_25 = (tmpvar_25 + (float(
					              (abs((tmpvar_22 / tmpvar_21)) > 1.0)
					            ) * (
					              (tmpvar_25 * -2.0)
					             + 1.570796)));
					            tmpvar_23 = (tmpvar_25 * sign((tmpvar_22 / tmpvar_21)));
					            if ((abs(tmpvar_21) > (1e-08 * abs(tmpvar_22)))) {
					              if ((tmpvar_21 < 0.0)) {
					                if ((tmpvar_22 >= 0.0)) {
					                  tmpvar_23 += 3.141593;
					                } else {
					                  tmpvar_23 = (tmpvar_23 - 3.141593);
					                };
					              };
					            } else {
					              tmpvar_23 = (sign(tmpvar_22) * 1.570796);
					            };
					            highp vec3 tmpvar_26;
					            tmpvar_26.x = tmpvar_23;
					            tmpvar_26.y = sqrt(((tmpvar_21 * tmpvar_21) + (tmpvar_22 * tmpvar_22)));
					            tmpvar_26.z = (((0.299 * rgbColor_20.x) + (0.587 * rgbColor_20.y)) + (0.114 * rgbColor_20.z));
					            highp float tmpvar_27;
					            tmpvar_27 = (tmpvar_19.y * sin(tmpvar_16));
					            highp float tmpvar_28;
					            tmpvar_28 = (tmpvar_19.y * cos(tmpvar_16));
					            highp vec3 tmpvar_29;
					            tmpvar_29.x = ((tmpvar_26.z + (0.956 * tmpvar_28)) + (0.621 * tmpvar_27));
					            tmpvar_29.y = ((tmpvar_26.z - (0.272 * tmpvar_28)) - (0.647 * tmpvar_27));
					            tmpvar_29.z = ((tmpvar_26.z - (1.107 * tmpvar_28)) + (1.704 * tmpvar_27));
					            finalColor_12.xyz = tmpvar_29;
					            finalColor_12 = mix (tmpvar_10, finalColor_12, tmpvar_11.wwww);
					          };
					          outputColor_1 = mix (tmpvar_10, finalColor_12, finalColor_12.wwww);
					        };
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