Shader "AvatarDiffuseGlow" {
Properties {
 CostumeBlendMode00 ("Costume Blend Mode", Float) = 0.000000
 GlowBlendMode00 ("Glow Blend Mode", Float) = 0.000000
 AvatarDiffuseBase ("Avatar Diffuse Base", 2D) = "white" { }
 CostumeTex00 ("Costume", 2D) = "alpha" { }
 GlowTex00 ("Glow", 2D) = "alpha" { }
 _MainTex ("Main Texture", 2D) = "white" { }
}
SubShader { 
 Pass {
  GpuProgramID 51221
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
					uniform highp int CostumeBlendMode00;
					uniform sampler2D AvatarDiffuseBase;
					uniform sampler2D CostumeTex00;
					uniform sampler2D GlowTex00;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 outputColor_1;
					  outputColor_1 = texture2D (AvatarDiffuseBase, xlv_TEXCOORD0.xy);
					  if ((CostumeBlendMode00 != 5)) {
					    outputColor_1 = texture2D (CostumeTex00, xlv_TEXCOORD0.xy);
					  };
					  lowp vec4 texColor_2;
					  texColor_2 = texture2D (GlowTex00, xlv_TEXCOORD0.xy);
					  lowp vec4 tmpvar_3;
					  lowp float value_4;
					  lowp vec3 tmpvar_5;
					  tmpvar_5 = outputColor_1.xyz;
					  highp vec3 rgbColor_6;
					  rgbColor_6 = tmpvar_5;
					  highp float tmpvar_7;
					  tmpvar_7 = max (max (rgbColor_6.x, rgbColor_6.y), rgbColor_6.z);
					  value_4 = tmpvar_7;
					  if ((value_4 <= 0.5)) {
					    lowp vec4 tmpvar_8;
					    if ((texColor_2.w == 0.0)) {
					      tmpvar_8 = outputColor_1;
					    } else {
					      tmpvar_8 = ((texColor_2 * outputColor_1) * vec4(2.0, 2.0, 2.0, 2.0));
					    };
					    tmpvar_3 = mix (outputColor_1, tmpvar_8, texColor_2.wwww);
					  } else {
					    tmpvar_3 = mix (outputColor_1, (1.0 - (
					      (vec4(2.0, 2.0, 2.0, 2.0) * (1.0 - texColor_2))
					     * 
					      (1.0 - outputColor_1)
					    )), texColor_2.wwww);
					  };
					  outputColor_1 = tmpvar_3;
					  gl_FragData[0] = tmpvar_3;
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
					uniform highp int CostumeBlendMode00;
					uniform sampler2D AvatarDiffuseBase;
					uniform sampler2D CostumeTex00;
					uniform sampler2D GlowTex00;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 outputColor_1;
					  outputColor_1 = texture2D (AvatarDiffuseBase, xlv_TEXCOORD0.xy);
					  if ((CostumeBlendMode00 != 5)) {
					    outputColor_1 = texture2D (CostumeTex00, xlv_TEXCOORD0.xy);
					  };
					  lowp vec4 texColor_2;
					  texColor_2 = texture2D (GlowTex00, xlv_TEXCOORD0.xy);
					  lowp vec4 tmpvar_3;
					  lowp float value_4;
					  lowp vec3 tmpvar_5;
					  tmpvar_5 = outputColor_1.xyz;
					  highp vec3 rgbColor_6;
					  rgbColor_6 = tmpvar_5;
					  highp float tmpvar_7;
					  tmpvar_7 = max (max (rgbColor_6.x, rgbColor_6.y), rgbColor_6.z);
					  value_4 = tmpvar_7;
					  if ((value_4 <= 0.5)) {
					    lowp vec4 tmpvar_8;
					    if ((texColor_2.w == 0.0)) {
					      tmpvar_8 = outputColor_1;
					    } else {
					      tmpvar_8 = ((texColor_2 * outputColor_1) * vec4(2.0, 2.0, 2.0, 2.0));
					    };
					    tmpvar_3 = mix (outputColor_1, tmpvar_8, texColor_2.wwww);
					  } else {
					    tmpvar_3 = mix (outputColor_1, (1.0 - (
					      (vec4(2.0, 2.0, 2.0, 2.0) * (1.0 - texColor_2))
					     * 
					      (1.0 - outputColor_1)
					    )), texColor_2.wwww);
					  };
					  outputColor_1 = tmpvar_3;
					  gl_FragData[0] = tmpvar_3;
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
					uniform highp int CostumeBlendMode00;
					uniform sampler2D AvatarDiffuseBase;
					uniform sampler2D CostumeTex00;
					uniform sampler2D GlowTex00;
					varying highp vec4 xlv_TEXCOORD0;
					void main ()
					{
					  lowp vec4 outputColor_1;
					  outputColor_1 = texture2D (AvatarDiffuseBase, xlv_TEXCOORD0.xy);
					  if ((CostumeBlendMode00 != 5)) {
					    outputColor_1 = texture2D (CostumeTex00, xlv_TEXCOORD0.xy);
					  };
					  lowp vec4 texColor_2;
					  texColor_2 = texture2D (GlowTex00, xlv_TEXCOORD0.xy);
					  lowp vec4 tmpvar_3;
					  lowp float value_4;
					  lowp vec3 tmpvar_5;
					  tmpvar_5 = outputColor_1.xyz;
					  highp vec3 rgbColor_6;
					  rgbColor_6 = tmpvar_5;
					  highp float tmpvar_7;
					  tmpvar_7 = max (max (rgbColor_6.x, rgbColor_6.y), rgbColor_6.z);
					  value_4 = tmpvar_7;
					  if ((value_4 <= 0.5)) {
					    lowp vec4 tmpvar_8;
					    if ((texColor_2.w == 0.0)) {
					      tmpvar_8 = outputColor_1;
					    } else {
					      tmpvar_8 = ((texColor_2 * outputColor_1) * vec4(2.0, 2.0, 2.0, 2.0));
					    };
					    tmpvar_3 = mix (outputColor_1, tmpvar_8, texColor_2.wwww);
					  } else {
					    tmpvar_3 = mix (outputColor_1, (1.0 - (
					      (vec4(2.0, 2.0, 2.0, 2.0) * (1.0 - texColor_2))
					     * 
					      (1.0 - outputColor_1)
					    )), texColor_2.wwww);
					  };
					  outputColor_1 = tmpvar_3;
					  gl_FragData[0] = tmpvar_3;
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