using System;
using UnityEngine;

public class GlowPrepCamera : MonoBehaviour
{
	public Camera CameraToMatch;

	public Transform ImageQuadTransform;

	public Shader ReplacementShader;

	public float ExtraThickness = 0.1f;

	[Range(0f, 4f)]
	public float Intensity = 1f;

	public bool UseSimpleColor = true;

	public Color SimpleColor = Color.white;

	public Texture2D ColorTexture;

	public Texture2D m_SimpleColorTexture;

	private Color m_SimpleColorLastColor = Color.black;

	private Camera m_Camera;

	private void Start()
	{
		_setCameraShader();
		_initRenderTexture();
		_resizeImageQuadToFrustrum();
	}

	private void OnDestroy()
	{
		m_Camera.targetTexture = null;
		UnityEngine.Object.Destroy(RenderTexture.active);
		RenderTexture.active = null;
	}

	private void OnValidate()
	{
		_setCameraShader();
		_updateTexture();
	}

	private void _updateTexture()
	{
		if (UseSimpleColor)
		{
			bool flag = false;
			if (m_SimpleColorTexture == null)
			{
				m_SimpleColorTexture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
				m_SimpleColorTexture.filterMode = FilterMode.Point;
				flag = true;
			}
			if (m_SimpleColorLastColor != SimpleColor || flag)
			{
				Color[] pixels = new Color[4] { SimpleColor, SimpleColor, SimpleColor, SimpleColor };
				m_SimpleColorTexture.SetPixels(pixels);
				m_SimpleColorTexture.Apply();
				m_SimpleColorLastColor = SimpleColor;
			}
		}
		_updateShaderGlobals();
	}

	private void _setCameraShader()
	{
		if (m_Camera == null)
		{
			Camera component = GetComponent<Camera>();
			if ((bool)component)
			{
				m_Camera = component;
			}
		}
		if ((bool)m_Camera)
		{
			m_Camera.SetReplacementShader(ReplacementShader, string.Empty);
		}
	}

	private void _updateShaderGlobals()
	{
		Shader.SetGlobalTexture("_GlowPrepColorTexture", (!UseSimpleColor) ? ColorTexture : m_SimpleColorTexture);
		Shader.SetGlobalFloat("_GlowPrepExtraThickness", ExtraThickness);
		Shader.SetGlobalFloat("_GlowPrepIntensity", Intensity);
	}

	private void _resizeImageQuadToFrustrum()
	{
		float magnitude = (ImageQuadTransform.position - CameraToMatch.transform.position).magnitude;
		float num = 2f * magnitude * Mathf.Tan(CameraToMatch.fieldOfView * 0.5f * ((float)Math.PI / 180f));
		float x = num * CameraToMatch.aspect;
		m_Camera.aspect = CameraToMatch.aspect;
		ImageQuadTransform.localScale = new Vector3(x, num, 1f);
	}

	private void _initRenderTexture()
	{
		int pixelWidth = CameraToMatch.pixelWidth;
		int pixelHeight = CameraToMatch.pixelHeight;
		RenderTexture renderTexture = new RenderTexture(pixelWidth, pixelHeight, 24);
		m_Camera.targetTexture = renderTexture;
		RenderTexture.active = renderTexture;
		ImageQuadTransform.GetComponent<MeshRenderer>().material.mainTexture = renderTexture;
	}
}
