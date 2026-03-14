using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Disney.MobileNetwork
{
	public class SystemTextManager : MonoBehaviour
	{
		public void Awake()
		{
			Service.Set(this);
			Init();
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		protected virtual void Init()
		{
		}

		public void UpdateSystemText(Image aImage, string aText, int aFontSize, float aMaxWidth, Color32 aFontColor, Color32 aBgColor, bool aWordWrap)
		{
			aText = ((!aText.Equals(string.Empty)) ? aText : "  ");
			string path = Service.Get<SystemTextManager>().GenerateImageForString(aText, string.Empty, aFontSize, aFontColor, aBgColor, aMaxWidth, aWordWrap);
			if (string.IsNullOrEmpty(path) || !File.Exists(path))
			{
				return;
			}
			GameObject gameObject = aImage.gameObject;
			Texture2D texture2D = new Texture2D(4, 4, TextureFormat.ARGB32, false);
			texture2D.LoadImage(File.ReadAllBytes(path));
			File.Delete(path);
			Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
			aImage.sprite = sprite;
			aImage.preserveAspect = true;
			gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x, texture2D.height);
		}

		public Image GenerateSystemText(Text aText, Color32 aBgColor, float aMaxWidth = 0f, bool aMultipleLines = false)
		{
			float width = aText.GetComponent<RectTransform>().rect.width;
			width = ((aMaxWidth == 0f) ? (width * 0.8f) : aMaxWidth);
			string path = Service.Get<SystemTextManager>().GenerateImageForString((!aText.text.Equals(string.Empty)) ? aText.text : "  ", string.Empty, aText.fontSize, aText.color, aBgColor, width, aMultipleLines);
			if (string.IsNullOrEmpty(path) || !File.Exists(path))
			{
				return null;
			}
			GameObject gameObject = aText.gameObject;
			UnityEngine.Object.DestroyImmediate(aText);
			Texture2D texture2D = new Texture2D(4, 4, TextureFormat.ARGB32, false);
			texture2D.LoadImage(File.ReadAllBytes(path));
			File.Delete(path);
			Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
			Image image = gameObject.AddComponent<Image>();
			image.sprite = sprite;
			image.preserveAspect = true;
			gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x, texture2D.height);
			return image;
		}

		public virtual string GenerateImageForString(string aString, string aFont, int aFontSize, Color32 aFontColor, Color32 aFontBGColor, float aMaxWidth, bool aWordWrap)
		{
			// Pure C# Unity fallback for text-to-image rendering
			Font fontToUse = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
			if (!string.IsNullOrEmpty(aFont))
			{
				Font loadedFont = Resources.Load<Font>(aFont);
				if (loadedFont != null) fontToUse = loadedFont;
			}

			// Handle custom newlines as multi-line
			aString = aString.Replace("\\n", "\n");

			// Generate text layout to know required texture dimensions
			TextGenerationSettings settings = new TextGenerationSettings();
			settings.textAnchor = TextAnchor.UpperLeft;
			settings.color = aFontColor;
			settings.generationExtents = new Vector2(aMaxWidth > 0f ? aMaxWidth : 10000f, 10000f);
			settings.pivot = Vector2.zero;
			settings.font = fontToUse;
			settings.fontSize = aFontSize;
			settings.fontStyle = FontStyle.Normal;
			settings.lineSpacing = 1f;
			settings.verticalOverflow = VerticalWrapMode.Overflow;
			settings.horizontalOverflow = aWordWrap ? HorizontalWrapMode.Wrap : HorizontalWrapMode.Overflow;
			settings.scaleFactor = 1f;

			TextGenerator textGen = new TextGenerator();
			float calcWidth = textGen.GetPreferredWidth(aString, settings);
			float calcHeight = textGen.GetPreferredHeight(aString, settings);

			int renderWidth = Mathf.Max(1, aMaxWidth > 0f ? (int)aMaxWidth : (int)Mathf.Ceil(calcWidth));
			// Add slight pixel padding to height to prevent descenders clipping off
			int renderHeight = Mathf.Max(1, (int)Mathf.Ceil(calcHeight + (aFontSize * 0.25f)));

			// Setup off-screen camera & render texture
			RenderTexture rt = RenderTexture.GetTemporary(renderWidth, renderHeight, 24);
			
			GameObject camObj = new GameObject("TempTextRenderCam");
			Camera cam = camObj.AddComponent<Camera>();
			cam.clearFlags = CameraClearFlags.SolidColor;
			cam.backgroundColor = aFontBGColor;
			cam.orthographic = true;
			cam.orthographicSize = renderHeight / 2f;
			cam.targetTexture = rt;
			cam.transform.position = new Vector3(renderWidth / 2f, renderHeight / 2f, -10f);

			// Setup UI Canvas rendered by our camera
			GameObject canvasObj = new GameObject("TempTextCanvas");
			Canvas canvas = canvasObj.AddComponent<Canvas>();
			canvas.renderMode = RenderMode.ScreenSpaceCamera;
			canvas.worldCamera = cam;

			// Insert Text component
			GameObject textObj = new GameObject("TempTextNode");
			textObj.transform.SetParent(canvasObj.transform, false);
			Text uiText = textObj.AddComponent<Text>();
			uiText.font = fontToUse;
			uiText.text = aString;
			uiText.fontSize = aFontSize;
			uiText.color = aFontColor;
			uiText.alignment = TextAnchor.UpperLeft;
			uiText.horizontalOverflow = settings.horizontalOverflow;
			uiText.verticalOverflow = settings.verticalOverflow;
			uiText.lineSpacing = 1f;

			RectTransform rtText = uiText.rectTransform;
			rtText.anchorMin = new Vector2(0, 1);
			rtText.anchorMax = new Vector2(0, 1);
			rtText.pivot = new Vector2(0, 1);
			rtText.sizeDelta = new Vector2(renderWidth, renderHeight);
			rtText.anchoredPosition = Vector2.zero;

			// Force a render pass to draw the text
			cam.Render();

			// Read pixels out of the GPU
			RenderTexture.active = rt;
			Texture2D tex2D = new Texture2D(renderWidth, renderHeight, TextureFormat.ARGB32, false);
			tex2D.ReadPixels(new Rect(0, 0, renderWidth, renderHeight), 0, 0);
			tex2D.Apply();
			RenderTexture.active = null;

			byte[] pngBytes = tex2D.EncodeToPNG();

			// Cleanup Unity Objects
			RenderTexture.ReleaseTemporary(rt);
			UnityEngine.Object.DestroyImmediate(textObj);
			UnityEngine.Object.DestroyImmediate(canvasObj);
			UnityEngine.Object.DestroyImmediate(camObj);
			UnityEngine.Object.DestroyImmediate(tex2D);

			// Save to disk and return
			string tempPath = Path.Combine(Application.temporaryCachePath, "SystemText_" + Guid.NewGuid().ToString() + ".png");
			File.WriteAllBytes(tempPath, pngBytes);

			return tempPath;
		}

		public virtual int GetImageHeightForString(string aString, string aFont, int aFontSize, Color32 aFontColor, Color32 aFontBGColor, float aMaxWidth, bool aWordWrap)
		{
			Font fontToUse = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
			if (!string.IsNullOrEmpty(aFont))
			{
				Font loadedFont = Resources.Load<Font>(aFont);
				if (loadedFont != null) fontToUse = loadedFont;
			}
			
			TextGenerationSettings settings = new TextGenerationSettings();
			settings.textAnchor = TextAnchor.UpperLeft;
			settings.color = aFontColor;
			settings.generationExtents = new Vector2(aMaxWidth > 0f ? aMaxWidth : 10000f, 10000f);
			settings.pivot = Vector2.zero;
			settings.font = fontToUse;
			settings.fontSize = aFontSize;
			settings.fontStyle = FontStyle.Normal;
			settings.verticalOverflow = VerticalWrapMode.Overflow;
			settings.horizontalOverflow = aWordWrap ? HorizontalWrapMode.Wrap : HorizontalWrapMode.Overflow;
			settings.scaleFactor = 1f;

			TextGenerator textGen = new TextGenerator();
			float height = textGen.GetPreferredHeight(aString, settings);
			
			// Add 10-15% padding to account for descenders (`y`, `g`, `p`) that Unity TextGenerator often misses
			// or simply an absolute pixel buffer.
			float padding = aFontSize * 0.25f;
			
			return (int)Mathf.Ceil(height + padding);
		}

		public virtual int GetIndexInTextAtPoint(string aString, string aFont, int aFontSize, float aMaxWidth, bool aWordWrap, float xPos, float yPos)
		{
			return -1;
		}
	}
}
