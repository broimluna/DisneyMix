Shader "AvatarNormals" {
Properties {
 NmlColor ("Base Normal Color", Color) = (0.500000,0.500000,1.000000,1.000000)
 NoseXOffset ("Nose Normal Offset X", Float) = 0.000000
 NoseYOffset ("Nose Normal Offset Y", Float) = 0.000000
 EyesXOffset ("Eyes Normal Offset X", Float) = 0.000000
 EyesYOffset ("Eyes Normal Offset Y", Float) = 0.000000
 BrowXOffset ("Brow Normal Offset X", Float) = 0.000000
 BrowYOffset ("Brow Normal Offset Y", Float) = 0.000000
 AccessoryXOffset ("Accessory Normal Offset X", Float) = 0.000000
 AccessoryYOffset ("Accessory Normal Offset Y", Float) = 0.000000
 HairXOffset ("Hair Normal Offset X", Float) = 0.000000
 HairYOffset ("Hair Normal Offset Y", Float) = 0.000000
 NoseBlendMode00 ("Nose Blend Mode", Float) = 4.000000
 EyesBlendMode00 ("Eyes Blend Mode", Float) = 4.000000
 BrowBlendMode00 ("Brow Blend Mode", Float) = 4.000000
 AccessoryBlendMode00 ("Accessory Blend Mode", Float) = 4.000000
 HairBlendMode00 ("Hair Blend Mode", Float) = 4.000000
 CostumeBlendMode00 ("Costume Blend Mode", Float) = 4.000000
 NoseTex00 ("Nose", 2D) = "white" { }
 EyesTex00 ("Eyes", 2D) = "white" { }
 BrowTex00 ("Eyebrows", 2D) = "white" { }
 AccessoryTex00 ("Accessory", 2D) = "white" { }
 HairTex00 ("Hair", 2D) = "white" { }
 CostumeTex00 ("Costume", 2D) = "white" { }
 _MainTex ("Main Texture", 2D) = "white" { }
}
SubShader { 
 Pass {
  GpuProgramID 37408
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
					uniform highp float NoseXOffset;
					uniform highp float NoseYOffset;
					uniform highp float EyesXOffset;
					uniform highp float EyesYOffset;
					uniform highp float BrowXOffset;
					uniform highp float BrowYOffset;
					uniform highp float AccessoryXOffset;
					uniform highp float AccessoryYOffset;
					uniform highp float HairXOffset;
					uniform highp float HairYOffset;
					uniform highp float NoseCrop;
					uniform highp float EyesCrop;
					uniform highp float BrowCrop;
					uniform highp float AccessoryCrop;
					uniform highp int NoseBlendMode00;
					uniform highp int EyesBlendMode00;
					uniform highp int BrowBlendMode00;
					uniform highp int AccessoryBlendMode00;
					uniform highp int HairBlendMode00;
					uniform highp int CostumeBlendMode00;
					uniform sampler2D NoseTex00;
					uniform sampler2D EyesTex00;
					uniform sampler2D BrowTex00;
					uniform sampler2D AccessoryTex00;
					uniform sampler2D HairTex00;
					uniform sampler2D CostumeTex00;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 outputColor_1;
					  outputColor_1 = NmlColor;
					  if ((CostumeBlendMode00 == 5)) {
					    highp vec2 tmpvar_2;
					    tmpvar_2.x = NoseXOffset;
					    tmpvar_2.y = NoseYOffset;
					    highp vec2 tmpvar_3;
					    highp vec2 tmpvar_4;
					    tmpvar_4 = (((
					      (xlv_TEXCOORD0.xy + tmpvar_2)
					     * vec2(NoseCrop)) - vec2(NoseCrop)) + vec2(1.0, 1.0));
					    if (((tmpvar_4.x < 0.0) || (tmpvar_4.y < 0.0))) {
					      tmpvar_3 = vec2(0.0, 0.0);
					    } else {
					      tmpvar_3 = tmpvar_4;
					    };
					    lowp vec4 texColor_5;
					    texColor_5 = texture2D (NoseTex00, tmpvar_3);
					    lowp vec4 tmpvar_6;
					    if ((NoseBlendMode00 == 0)) {
					      tmpvar_6 = mix (NmlColor, texColor_5, texColor_5.wwww);
					    } else {
					      tmpvar_6 = NmlColor;
					    };
					    outputColor_1 = tmpvar_6;
					    highp vec2 tmpvar_7;
					    tmpvar_7.x = EyesXOffset;
					    tmpvar_7.y = EyesYOffset;
					    highp vec2 tmpvar_8;
					    highp vec2 tmpvar_9;
					    tmpvar_9 = (((
					      (xlv_TEXCOORD0.xy + tmpvar_7)
					     * vec2(EyesCrop)) - vec2(EyesCrop)) + vec2(1.0, 1.0));
					    if (((tmpvar_9.x < 0.0) || (tmpvar_9.y < 0.0))) {
					      tmpvar_8 = vec2(0.0, 0.0);
					    } else {
					      tmpvar_8 = tmpvar_9;
					    };
					    lowp vec4 tmpvar_10;
					    tmpvar_10 = texture2D (EyesTex00, tmpvar_8);
					    lowp vec4 tmpvar_11;
					    if ((EyesBlendMode00 == 0)) {
					      tmpvar_11 = mix (tmpvar_6, tmpvar_10, tmpvar_10.wwww);
					    } else {
					      tmpvar_11 = tmpvar_6;
					    };
					    outputColor_1 = tmpvar_11;
					    highp vec2 tmpvar_12;
					    tmpvar_12.x = BrowXOffset;
					    tmpvar_12.y = BrowYOffset;
					    highp vec2 tmpvar_13;
					    highp vec2 tmpvar_14;
					    tmpvar_14 = (((
					      (xlv_TEXCOORD0.xy + tmpvar_12)
					     * vec2(BrowCrop)) - vec2(BrowCrop)) + vec2(1.0, 1.0));
					    if (((tmpvar_14.x < 0.0) || (tmpvar_14.y < 0.0))) {
					      tmpvar_13 = vec2(0.0, 0.0);
					    } else {
					      tmpvar_13 = tmpvar_14;
					    };
					    lowp vec4 tmpvar_15;
					    tmpvar_15 = texture2D (BrowTex00, tmpvar_13);
					    lowp vec4 tmpvar_16;
					    if ((BrowBlendMode00 == 0)) {
					      tmpvar_16 = mix (tmpvar_11, tmpvar_15, tmpvar_15.wwww);
					    } else {
					      tmpvar_16 = tmpvar_11;
					    };
					    outputColor_1 = tmpvar_16;
					    highp vec2 tmpvar_17;
					    tmpvar_17.x = HairXOffset;
					    tmpvar_17.y = HairYOffset;
					    highp vec2 tmpvar_18;
					    tmpvar_18 = (xlv_TEXCOORD0.xy + tmpvar_17);
					    lowp vec4 tmpvar_19;
					    tmpvar_19 = texture2D (HairTex00, tmpvar_18);
					    lowp vec4 tmpvar_20;
					    if ((HairBlendMode00 == 0)) {
					      tmpvar_20 = mix (tmpvar_16, tmpvar_19, tmpvar_19.wwww);
					    } else {
					      tmpvar_20 = tmpvar_16;
					    };
					    outputColor_1 = tmpvar_20;
					    highp vec2 tmpvar_21;
					    tmpvar_21.x = AccessoryXOffset;
					    tmpvar_21.y = AccessoryYOffset;
					    highp vec2 tmpvar_22;
					    highp vec2 tmpvar_23;
					    tmpvar_23 = (((
					      (xlv_TEXCOORD0.xy + tmpvar_21)
					     * vec2(AccessoryCrop)) - vec2(AccessoryCrop)) + vec2(1.0, 1.0));
					    if (((tmpvar_23.x < 0.0) || (tmpvar_23.y < 0.0))) {
					      tmpvar_22 = vec2(0.0, 0.0);
					    } else {
					      tmpvar_22 = tmpvar_23;
					    };
					    lowp vec4 tmpvar_24;
					    tmpvar_24 = texture2D (AccessoryTex00, tmpvar_22);
					    lowp vec4 tmpvar_25;
					    if ((AccessoryBlendMode00 == 0)) {
					      tmpvar_25 = mix (tmpvar_20, tmpvar_24, tmpvar_24.wwww);
					    } else {
					      tmpvar_25 = tmpvar_20;
					    };
					    outputColor_1 = tmpvar_25;
					  } else {
					    outputColor_1 = texture2D (CostumeTex00, xlv_TEXCOORD0.xy);
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
					uniform lowp vec4 NmlColor;
					uniform highp float NoseXOffset;
					uniform highp float NoseYOffset;
					uniform highp float EyesXOffset;
					uniform highp float EyesYOffset;
					uniform highp float BrowXOffset;
					uniform highp float BrowYOffset;
					uniform highp float AccessoryXOffset;
					uniform highp float AccessoryYOffset;
					uniform highp float HairXOffset;
					uniform highp float HairYOffset;
					uniform highp float NoseCrop;
					uniform highp float EyesCrop;
					uniform highp float BrowCrop;
					uniform highp float AccessoryCrop;
					uniform highp int NoseBlendMode00;
					uniform highp int EyesBlendMode00;
					uniform highp int BrowBlendMode00;
					uniform highp int AccessoryBlendMode00;
					uniform highp int HairBlendMode00;
					uniform highp int CostumeBlendMode00;
					uniform sampler2D NoseTex00;
					uniform sampler2D EyesTex00;
					uniform sampler2D BrowTex00;
					uniform sampler2D AccessoryTex00;
					uniform sampler2D HairTex00;
					uniform sampler2D CostumeTex00;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 outputColor_1;
					  outputColor_1 = NmlColor;
					  if ((CostumeBlendMode00 == 5)) {
					    highp vec2 tmpvar_2;
					    tmpvar_2.x = NoseXOffset;
					    tmpvar_2.y = NoseYOffset;
					    highp vec2 tmpvar_3;
					    highp vec2 tmpvar_4;
					    tmpvar_4 = (((
					      (xlv_TEXCOORD0.xy + tmpvar_2)
					     * vec2(NoseCrop)) - vec2(NoseCrop)) + vec2(1.0, 1.0));
					    if (((tmpvar_4.x < 0.0) || (tmpvar_4.y < 0.0))) {
					      tmpvar_3 = vec2(0.0, 0.0);
					    } else {
					      tmpvar_3 = tmpvar_4;
					    };
					    lowp vec4 texColor_5;
					    texColor_5 = texture2D (NoseTex00, tmpvar_3);
					    lowp vec4 tmpvar_6;
					    if ((NoseBlendMode00 == 0)) {
					      tmpvar_6 = mix (NmlColor, texColor_5, texColor_5.wwww);
					    } else {
					      tmpvar_6 = NmlColor;
					    };
					    outputColor_1 = tmpvar_6;
					    highp vec2 tmpvar_7;
					    tmpvar_7.x = EyesXOffset;
					    tmpvar_7.y = EyesYOffset;
					    highp vec2 tmpvar_8;
					    highp vec2 tmpvar_9;
					    tmpvar_9 = (((
					      (xlv_TEXCOORD0.xy + tmpvar_7)
					     * vec2(EyesCrop)) - vec2(EyesCrop)) + vec2(1.0, 1.0));
					    if (((tmpvar_9.x < 0.0) || (tmpvar_9.y < 0.0))) {
					      tmpvar_8 = vec2(0.0, 0.0);
					    } else {
					      tmpvar_8 = tmpvar_9;
					    };
					    lowp vec4 tmpvar_10;
					    tmpvar_10 = texture2D (EyesTex00, tmpvar_8);
					    lowp vec4 tmpvar_11;
					    if ((EyesBlendMode00 == 0)) {
					      tmpvar_11 = mix (tmpvar_6, tmpvar_10, tmpvar_10.wwww);
					    } else {
					      tmpvar_11 = tmpvar_6;
					    };
					    outputColor_1 = tmpvar_11;
					    highp vec2 tmpvar_12;
					    tmpvar_12.x = BrowXOffset;
					    tmpvar_12.y = BrowYOffset;
					    highp vec2 tmpvar_13;
					    highp vec2 tmpvar_14;
					    tmpvar_14 = (((
					      (xlv_TEXCOORD0.xy + tmpvar_12)
					     * vec2(BrowCrop)) - vec2(BrowCrop)) + vec2(1.0, 1.0));
					    if (((tmpvar_14.x < 0.0) || (tmpvar_14.y < 0.0))) {
					      tmpvar_13 = vec2(0.0, 0.0);
					    } else {
					      tmpvar_13 = tmpvar_14;
					    };
					    lowp vec4 tmpvar_15;
					    tmpvar_15 = texture2D (BrowTex00, tmpvar_13);
					    lowp vec4 tmpvar_16;
					    if ((BrowBlendMode00 == 0)) {
					      tmpvar_16 = mix (tmpvar_11, tmpvar_15, tmpvar_15.wwww);
					    } else {
					      tmpvar_16 = tmpvar_11;
					    };
					    outputColor_1 = tmpvar_16;
					    highp vec2 tmpvar_17;
					    tmpvar_17.x = HairXOffset;
					    tmpvar_17.y = HairYOffset;
					    highp vec2 tmpvar_18;
					    tmpvar_18 = (xlv_TEXCOORD0.xy + tmpvar_17);
					    lowp vec4 tmpvar_19;
					    tmpvar_19 = texture2D (HairTex00, tmpvar_18);
					    lowp vec4 tmpvar_20;
					    if ((HairBlendMode00 == 0)) {
					      tmpvar_20 = mix (tmpvar_16, tmpvar_19, tmpvar_19.wwww);
					    } else {
					      tmpvar_20 = tmpvar_16;
					    };
					    outputColor_1 = tmpvar_20;
					    highp vec2 tmpvar_21;
					    tmpvar_21.x = AccessoryXOffset;
					    tmpvar_21.y = AccessoryYOffset;
					    highp vec2 tmpvar_22;
					    highp vec2 tmpvar_23;
					    tmpvar_23 = (((
					      (xlv_TEXCOORD0.xy + tmpvar_21)
					     * vec2(AccessoryCrop)) - vec2(AccessoryCrop)) + vec2(1.0, 1.0));
					    if (((tmpvar_23.x < 0.0) || (tmpvar_23.y < 0.0))) {
					      tmpvar_22 = vec2(0.0, 0.0);
					    } else {
					      tmpvar_22 = tmpvar_23;
					    };
					    lowp vec4 tmpvar_24;
					    tmpvar_24 = texture2D (AccessoryTex00, tmpvar_22);
					    lowp vec4 tmpvar_25;
					    if ((AccessoryBlendMode00 == 0)) {
					      tmpvar_25 = mix (tmpvar_20, tmpvar_24, tmpvar_24.wwww);
					    } else {
					      tmpvar_25 = tmpvar_20;
					    };
					    outputColor_1 = tmpvar_25;
					  } else {
					    outputColor_1 = texture2D (CostumeTex00, xlv_TEXCOORD0.xy);
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
					uniform lowp vec4 NmlColor;
					uniform highp float NoseXOffset;
					uniform highp float NoseYOffset;
					uniform highp float EyesXOffset;
					uniform highp float EyesYOffset;
					uniform highp float BrowXOffset;
					uniform highp float BrowYOffset;
					uniform highp float AccessoryXOffset;
					uniform highp float AccessoryYOffset;
					uniform highp float HairXOffset;
					uniform highp float HairYOffset;
					uniform highp float NoseCrop;
					uniform highp float EyesCrop;
					uniform highp float BrowCrop;
					uniform highp float AccessoryCrop;
					uniform highp int NoseBlendMode00;
					uniform highp int EyesBlendMode00;
					uniform highp int BrowBlendMode00;
					uniform highp int AccessoryBlendMode00;
					uniform highp int HairBlendMode00;
					uniform highp int CostumeBlendMode00;
					uniform sampler2D NoseTex00;
					uniform sampler2D EyesTex00;
					uniform sampler2D BrowTex00;
					uniform sampler2D AccessoryTex00;
					uniform sampler2D HairTex00;
					uniform sampler2D CostumeTex00;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 outputColor_1;
					  outputColor_1 = NmlColor;
					  if ((CostumeBlendMode00 == 5)) {
					    highp vec2 tmpvar_2;
					    tmpvar_2.x = NoseXOffset;
					    tmpvar_2.y = NoseYOffset;
					    highp vec2 tmpvar_3;
					    highp vec2 tmpvar_4;
					    tmpvar_4 = (((
					      (xlv_TEXCOORD0.xy + tmpvar_2)
					     * vec2(NoseCrop)) - vec2(NoseCrop)) + vec2(1.0, 1.0));
					    if (((tmpvar_4.x < 0.0) || (tmpvar_4.y < 0.0))) {
					      tmpvar_3 = vec2(0.0, 0.0);
					    } else {
					      tmpvar_3 = tmpvar_4;
					    };
					    lowp vec4 texColor_5;
					    texColor_5 = texture2D (NoseTex00, tmpvar_3);
					    lowp vec4 tmpvar_6;
					    if ((NoseBlendMode00 == 0)) {
					      tmpvar_6 = mix (NmlColor, texColor_5, texColor_5.wwww);
					    } else {
					      tmpvar_6 = NmlColor;
					    };
					    outputColor_1 = tmpvar_6;
					    highp vec2 tmpvar_7;
					    tmpvar_7.x = EyesXOffset;
					    tmpvar_7.y = EyesYOffset;
					    highp vec2 tmpvar_8;
					    highp vec2 tmpvar_9;
					    tmpvar_9 = (((
					      (xlv_TEXCOORD0.xy + tmpvar_7)
					     * vec2(EyesCrop)) - vec2(EyesCrop)) + vec2(1.0, 1.0));
					    if (((tmpvar_9.x < 0.0) || (tmpvar_9.y < 0.0))) {
					      tmpvar_8 = vec2(0.0, 0.0);
					    } else {
					      tmpvar_8 = tmpvar_9;
					    };
					    lowp vec4 tmpvar_10;
					    tmpvar_10 = texture2D (EyesTex00, tmpvar_8);
					    lowp vec4 tmpvar_11;
					    if ((EyesBlendMode00 == 0)) {
					      tmpvar_11 = mix (tmpvar_6, tmpvar_10, tmpvar_10.wwww);
					    } else {
					      tmpvar_11 = tmpvar_6;
					    };
					    outputColor_1 = tmpvar_11;
					    highp vec2 tmpvar_12;
					    tmpvar_12.x = BrowXOffset;
					    tmpvar_12.y = BrowYOffset;
					    highp vec2 tmpvar_13;
					    highp vec2 tmpvar_14;
					    tmpvar_14 = (((
					      (xlv_TEXCOORD0.xy + tmpvar_12)
					     * vec2(BrowCrop)) - vec2(BrowCrop)) + vec2(1.0, 1.0));
					    if (((tmpvar_14.x < 0.0) || (tmpvar_14.y < 0.0))) {
					      tmpvar_13 = vec2(0.0, 0.0);
					    } else {
					      tmpvar_13 = tmpvar_14;
					    };
					    lowp vec4 tmpvar_15;
					    tmpvar_15 = texture2D (BrowTex00, tmpvar_13);
					    lowp vec4 tmpvar_16;
					    if ((BrowBlendMode00 == 0)) {
					      tmpvar_16 = mix (tmpvar_11, tmpvar_15, tmpvar_15.wwww);
					    } else {
					      tmpvar_16 = tmpvar_11;
					    };
					    outputColor_1 = tmpvar_16;
					    highp vec2 tmpvar_17;
					    tmpvar_17.x = HairXOffset;
					    tmpvar_17.y = HairYOffset;
					    highp vec2 tmpvar_18;
					    tmpvar_18 = (xlv_TEXCOORD0.xy + tmpvar_17);
					    lowp vec4 tmpvar_19;
					    tmpvar_19 = texture2D (HairTex00, tmpvar_18);
					    lowp vec4 tmpvar_20;
					    if ((HairBlendMode00 == 0)) {
					      tmpvar_20 = mix (tmpvar_16, tmpvar_19, tmpvar_19.wwww);
					    } else {
					      tmpvar_20 = tmpvar_16;
					    };
					    outputColor_1 = tmpvar_20;
					    highp vec2 tmpvar_21;
					    tmpvar_21.x = AccessoryXOffset;
					    tmpvar_21.y = AccessoryYOffset;
					    highp vec2 tmpvar_22;
					    highp vec2 tmpvar_23;
					    tmpvar_23 = (((
					      (xlv_TEXCOORD0.xy + tmpvar_21)
					     * vec2(AccessoryCrop)) - vec2(AccessoryCrop)) + vec2(1.0, 1.0));
					    if (((tmpvar_23.x < 0.0) || (tmpvar_23.y < 0.0))) {
					      tmpvar_22 = vec2(0.0, 0.0);
					    } else {
					      tmpvar_22 = tmpvar_23;
					    };
					    lowp vec4 tmpvar_24;
					    tmpvar_24 = texture2D (AccessoryTex00, tmpvar_22);
					    lowp vec4 tmpvar_25;
					    if ((AccessoryBlendMode00 == 0)) {
					      tmpvar_25 = mix (tmpvar_20, tmpvar_24, tmpvar_24.wwww);
					    } else {
					      tmpvar_25 = tmpvar_20;
					    };
					    outputColor_1 = tmpvar_25;
					  } else {
					    outputColor_1 = texture2D (CostumeTex00, xlv_TEXCOORD0.xy);
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