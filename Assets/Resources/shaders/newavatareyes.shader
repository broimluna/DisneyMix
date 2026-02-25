Shader "NewAvatarEyes" {
Properties {
 EyesColor ("Eye Color", Color) = (1.000000,1.000000,1.000000,1.000000)
 EyesXOffset ("Eye Offset X", Float) = 0.000000
 EyesYOffset ("Eye Offset Y", Float) = 0.000000
 EyesBlendMode00 ("Eyes Blend Mode", Float) = 0.000000
 EyesBlendMode02 ("Iris Blend Mode", Float) = 0.000000
 EyesCrop ("Eyes crop", Float) = 1.000000
 EyesTex00 ("Eyes", 2D) = "alpha" { }
 EyesMask ("Eye Mask", 2D) = "alpha" { }
 _MainTex ("Main Texture", 2D) = "alpha" { }
}
SubShader { 
 Pass {
  GpuProgramID 40352
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
					uniform lowp vec4 EyesColor;
					uniform highp float EyesCrop;
					uniform highp float EyesXOffset;
					uniform highp float EyesYOffset;
					uniform sampler2D EyesTex00;
					uniform sampler2D EyesMask;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  highp vec2 tmpvar_1;
					  tmpvar_1.x = EyesXOffset;
					  tmpvar_1.y = EyesYOffset;
					  highp vec2 tmpvar_2;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = (((
					    (xlv_TEXCOORD0.xy + tmpvar_1)
					   * vec2(EyesCrop)) - vec2(EyesCrop)) + vec2(1.0, 1.0));
					  if (((tmpvar_3.x < 0.0) || (tmpvar_3.y < 0.0))) {
					    tmpvar_2 = vec2(0.0, 0.0);
					  } else {
					    tmpvar_2 = tmpvar_3;
					  };
					  lowp vec4 tmpvar_4;
					  tmpvar_4 = texture2D (EyesTex00, tmpvar_2);
					  lowp vec4 tmpvar_5;
					  tmpvar_5 = (tmpvar_4 * tmpvar_4.wwww);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (EyesMask, tmpvar_2);
					  lowp vec4 finalColor_7;
					  finalColor_7 = tmpvar_4;
					  if (((tmpvar_4.w > 0.0) && (tmpvar_6.w > 0.001))) {
					    lowp float value_8;
					    lowp vec3 finalHsv_9;
					    lowp vec3 tintHsv_10;
					    highp vec3 rgbColor_11;
					    rgbColor_11 = EyesColor.xyz;
					    highp vec4 hsv_12;
					    hsv_12.z = max (max (rgbColor_11.x, rgbColor_11.y), rgbColor_11.z);
					    highp float tmpvar_13;
					    tmpvar_13 = (hsv_12.z - min (min (rgbColor_11.x, rgbColor_11.y), rgbColor_11.z));
					    if ((tmpvar_13 != 0.0)) {
					      highp vec3 delta2_14;
					      hsv_12.y = (tmpvar_13 / hsv_12.z);
					      highp vec3 tmpvar_15;
					      tmpvar_15 = ((hsv_12.z - rgbColor_11) / tmpvar_13);
					      delta2_14 = (tmpvar_15 - tmpvar_15.zxy);
					      delta2_14.xy = (delta2_14.xy + vec2(2.0, 4.0));
					      if ((rgbColor_11.x >= hsv_12.z)) {
					        hsv_12.x = delta2_14.z;
					      } else {
					        if ((rgbColor_11.y >= hsv_12.z)) {
					          hsv_12.x = delta2_14.x;
					        } else {
					          hsv_12.x = delta2_14.y;
					        };
					      };
					      hsv_12.x = fract((hsv_12.x / 6.0));
					    } else {
					      hsv_12.xy = vec2(0.0, 0.0);
					    };
					    highp vec3 tmpvar_16;
					    tmpvar_16 = hsv_12.xyz;
					    tintHsv_10 = tmpvar_16;
					    lowp vec3 tmpvar_17;
					    tmpvar_17 = tmpvar_4.xyz;
					    highp vec3 rgbColor_18;
					    rgbColor_18 = tmpvar_17;
					    highp float tmpvar_19;
					    tmpvar_19 = max (max (rgbColor_18.x, rgbColor_18.y), rgbColor_18.z);
					    value_8 = tmpvar_19;
					    if ((value_8 > 0.5)) {
					      lowp vec3 tmpvar_20;
					      tmpvar_20.x = tintHsv_10.x;
					      tmpvar_20.y = ((1.0 - (
					        (value_8 * 2.0)
					       - 1.0)) * tintHsv_10.y);
					      tmpvar_20.z = mix (tintHsv_10.z, 1.0, ((value_8 * 2.0) - 1.0));
					      finalHsv_9 = tmpvar_20;
					    } else {
					      lowp vec3 tmpvar_21;
					      tmpvar_21.xy = tintHsv_10.xy;
					      tmpvar_21.z = ((value_8 * 2.0) * tintHsv_10.z);
					      finalHsv_9 = tmpvar_21;
					    };
					    highp vec3 tmpvar_22;
					    highp vec3 hsv_23;
					    hsv_23 = finalHsv_9;
					    highp vec3 tmpvar_24;
					    tmpvar_24.x = (abs((
					      (hsv_23.x * 6.0)
					     - 3.0)) - 1.0);
					    tmpvar_24.y = (2.0 - abs((
					      (hsv_23.x * 6.0)
					     - 2.0)));
					    tmpvar_24.z = (2.0 - abs((
					      (hsv_23.x * 6.0)
					     - 4.0)));
					    tmpvar_22 = (((
					      (clamp (tmpvar_24, 0.0, 1.0) - 1.0)
					     * hsv_23.y) + 1.0) * hsv_23.z);
					    finalColor_7.xyz = tmpvar_22;
					    finalColor_7 = mix (tmpvar_4, finalColor_7, tmpvar_6.wwww);
					  };
					  lowp vec4 tmpvar_25;
					  tmpvar_25 = mix (tmpvar_5, finalColor_7, finalColor_7.wwww);
					  gl_FragData[0] = tmpvar_25;
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
					uniform lowp vec4 EyesColor;
					uniform highp float EyesCrop;
					uniform highp float EyesXOffset;
					uniform highp float EyesYOffset;
					uniform sampler2D EyesTex00;
					uniform sampler2D EyesMask;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  highp vec2 tmpvar_1;
					  tmpvar_1.x = EyesXOffset;
					  tmpvar_1.y = EyesYOffset;
					  highp vec2 tmpvar_2;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = (((
					    (xlv_TEXCOORD0.xy + tmpvar_1)
					   * vec2(EyesCrop)) - vec2(EyesCrop)) + vec2(1.0, 1.0));
					  if (((tmpvar_3.x < 0.0) || (tmpvar_3.y < 0.0))) {
					    tmpvar_2 = vec2(0.0, 0.0);
					  } else {
					    tmpvar_2 = tmpvar_3;
					  };
					  lowp vec4 tmpvar_4;
					  tmpvar_4 = texture2D (EyesTex00, tmpvar_2);
					  lowp vec4 tmpvar_5;
					  tmpvar_5 = (tmpvar_4 * tmpvar_4.wwww);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (EyesMask, tmpvar_2);
					  lowp vec4 finalColor_7;
					  finalColor_7 = tmpvar_4;
					  if (((tmpvar_4.w > 0.0) && (tmpvar_6.w > 0.001))) {
					    lowp float value_8;
					    lowp vec3 finalHsv_9;
					    lowp vec3 tintHsv_10;
					    highp vec3 rgbColor_11;
					    rgbColor_11 = EyesColor.xyz;
					    highp vec4 hsv_12;
					    hsv_12.z = max (max (rgbColor_11.x, rgbColor_11.y), rgbColor_11.z);
					    highp float tmpvar_13;
					    tmpvar_13 = (hsv_12.z - min (min (rgbColor_11.x, rgbColor_11.y), rgbColor_11.z));
					    if ((tmpvar_13 != 0.0)) {
					      highp vec3 delta2_14;
					      hsv_12.y = (tmpvar_13 / hsv_12.z);
					      highp vec3 tmpvar_15;
					      tmpvar_15 = ((hsv_12.z - rgbColor_11) / tmpvar_13);
					      delta2_14 = (tmpvar_15 - tmpvar_15.zxy);
					      delta2_14.xy = (delta2_14.xy + vec2(2.0, 4.0));
					      if ((rgbColor_11.x >= hsv_12.z)) {
					        hsv_12.x = delta2_14.z;
					      } else {
					        if ((rgbColor_11.y >= hsv_12.z)) {
					          hsv_12.x = delta2_14.x;
					        } else {
					          hsv_12.x = delta2_14.y;
					        };
					      };
					      hsv_12.x = fract((hsv_12.x / 6.0));
					    } else {
					      hsv_12.xy = vec2(0.0, 0.0);
					    };
					    highp vec3 tmpvar_16;
					    tmpvar_16 = hsv_12.xyz;
					    tintHsv_10 = tmpvar_16;
					    lowp vec3 tmpvar_17;
					    tmpvar_17 = tmpvar_4.xyz;
					    highp vec3 rgbColor_18;
					    rgbColor_18 = tmpvar_17;
					    highp float tmpvar_19;
					    tmpvar_19 = max (max (rgbColor_18.x, rgbColor_18.y), rgbColor_18.z);
					    value_8 = tmpvar_19;
					    if ((value_8 > 0.5)) {
					      lowp vec3 tmpvar_20;
					      tmpvar_20.x = tintHsv_10.x;
					      tmpvar_20.y = ((1.0 - (
					        (value_8 * 2.0)
					       - 1.0)) * tintHsv_10.y);
					      tmpvar_20.z = mix (tintHsv_10.z, 1.0, ((value_8 * 2.0) - 1.0));
					      finalHsv_9 = tmpvar_20;
					    } else {
					      lowp vec3 tmpvar_21;
					      tmpvar_21.xy = tintHsv_10.xy;
					      tmpvar_21.z = ((value_8 * 2.0) * tintHsv_10.z);
					      finalHsv_9 = tmpvar_21;
					    };
					    highp vec3 tmpvar_22;
					    highp vec3 hsv_23;
					    hsv_23 = finalHsv_9;
					    highp vec3 tmpvar_24;
					    tmpvar_24.x = (abs((
					      (hsv_23.x * 6.0)
					     - 3.0)) - 1.0);
					    tmpvar_24.y = (2.0 - abs((
					      (hsv_23.x * 6.0)
					     - 2.0)));
					    tmpvar_24.z = (2.0 - abs((
					      (hsv_23.x * 6.0)
					     - 4.0)));
					    tmpvar_22 = (((
					      (clamp (tmpvar_24, 0.0, 1.0) - 1.0)
					     * hsv_23.y) + 1.0) * hsv_23.z);
					    finalColor_7.xyz = tmpvar_22;
					    finalColor_7 = mix (tmpvar_4, finalColor_7, tmpvar_6.wwww);
					  };
					  lowp vec4 tmpvar_25;
					  tmpvar_25 = mix (tmpvar_5, finalColor_7, finalColor_7.wwww);
					  gl_FragData[0] = tmpvar_25;
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
					uniform lowp vec4 EyesColor;
					uniform highp float EyesCrop;
					uniform highp float EyesXOffset;
					uniform highp float EyesYOffset;
					uniform sampler2D EyesTex00;
					uniform sampler2D EyesMask;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  highp vec2 tmpvar_1;
					  tmpvar_1.x = EyesXOffset;
					  tmpvar_1.y = EyesYOffset;
					  highp vec2 tmpvar_2;
					  highp vec2 tmpvar_3;
					  tmpvar_3 = (((
					    (xlv_TEXCOORD0.xy + tmpvar_1)
					   * vec2(EyesCrop)) - vec2(EyesCrop)) + vec2(1.0, 1.0));
					  if (((tmpvar_3.x < 0.0) || (tmpvar_3.y < 0.0))) {
					    tmpvar_2 = vec2(0.0, 0.0);
					  } else {
					    tmpvar_2 = tmpvar_3;
					  };
					  lowp vec4 tmpvar_4;
					  tmpvar_4 = texture2D (EyesTex00, tmpvar_2);
					  lowp vec4 tmpvar_5;
					  tmpvar_5 = (tmpvar_4 * tmpvar_4.wwww);
					  lowp vec4 tmpvar_6;
					  tmpvar_6 = texture2D (EyesMask, tmpvar_2);
					  lowp vec4 finalColor_7;
					  finalColor_7 = tmpvar_4;
					  if (((tmpvar_4.w > 0.0) && (tmpvar_6.w > 0.001))) {
					    lowp float value_8;
					    lowp vec3 finalHsv_9;
					    lowp vec3 tintHsv_10;
					    highp vec3 rgbColor_11;
					    rgbColor_11 = EyesColor.xyz;
					    highp vec4 hsv_12;
					    hsv_12.z = max (max (rgbColor_11.x, rgbColor_11.y), rgbColor_11.z);
					    highp float tmpvar_13;
					    tmpvar_13 = (hsv_12.z - min (min (rgbColor_11.x, rgbColor_11.y), rgbColor_11.z));
					    if ((tmpvar_13 != 0.0)) {
					      highp vec3 delta2_14;
					      hsv_12.y = (tmpvar_13 / hsv_12.z);
					      highp vec3 tmpvar_15;
					      tmpvar_15 = ((hsv_12.z - rgbColor_11) / tmpvar_13);
					      delta2_14 = (tmpvar_15 - tmpvar_15.zxy);
					      delta2_14.xy = (delta2_14.xy + vec2(2.0, 4.0));
					      if ((rgbColor_11.x >= hsv_12.z)) {
					        hsv_12.x = delta2_14.z;
					      } else {
					        if ((rgbColor_11.y >= hsv_12.z)) {
					          hsv_12.x = delta2_14.x;
					        } else {
					          hsv_12.x = delta2_14.y;
					        };
					      };
					      hsv_12.x = fract((hsv_12.x / 6.0));
					    } else {
					      hsv_12.xy = vec2(0.0, 0.0);
					    };
					    highp vec3 tmpvar_16;
					    tmpvar_16 = hsv_12.xyz;
					    tintHsv_10 = tmpvar_16;
					    lowp vec3 tmpvar_17;
					    tmpvar_17 = tmpvar_4.xyz;
					    highp vec3 rgbColor_18;
					    rgbColor_18 = tmpvar_17;
					    highp float tmpvar_19;
					    tmpvar_19 = max (max (rgbColor_18.x, rgbColor_18.y), rgbColor_18.z);
					    value_8 = tmpvar_19;
					    if ((value_8 > 0.5)) {
					      lowp vec3 tmpvar_20;
					      tmpvar_20.x = tintHsv_10.x;
					      tmpvar_20.y = ((1.0 - (
					        (value_8 * 2.0)
					       - 1.0)) * tintHsv_10.y);
					      tmpvar_20.z = mix (tintHsv_10.z, 1.0, ((value_8 * 2.0) - 1.0));
					      finalHsv_9 = tmpvar_20;
					    } else {
					      lowp vec3 tmpvar_21;
					      tmpvar_21.xy = tintHsv_10.xy;
					      tmpvar_21.z = ((value_8 * 2.0) * tintHsv_10.z);
					      finalHsv_9 = tmpvar_21;
					    };
					    highp vec3 tmpvar_22;
					    highp vec3 hsv_23;
					    hsv_23 = finalHsv_9;
					    highp vec3 tmpvar_24;
					    tmpvar_24.x = (abs((
					      (hsv_23.x * 6.0)
					     - 3.0)) - 1.0);
					    tmpvar_24.y = (2.0 - abs((
					      (hsv_23.x * 6.0)
					     - 2.0)));
					    tmpvar_24.z = (2.0 - abs((
					      (hsv_23.x * 6.0)
					     - 4.0)));
					    tmpvar_22 = (((
					      (clamp (tmpvar_24, 0.0, 1.0) - 1.0)
					     * hsv_23.y) + 1.0) * hsv_23.z);
					    finalColor_7.xyz = tmpvar_22;
					    finalColor_7 = mix (tmpvar_4, finalColor_7, tmpvar_6.wwww);
					  };
					  lowp vec4 tmpvar_25;
					  tmpvar_25 = mix (tmpvar_5, finalColor_7, finalColor_7.wwww);
					  gl_FragData[0] = tmpvar_25;
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