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
			Object.DontDestroyOnLoad(base.gameObject);
		}

		protected virtual void Init()
		{
		}

		public void UpdateSystemText(Image aImage, string aText, int aFontSize, float aMaxWidth, Color32 aFontColor, Color32 aBgColor, bool aWordWrap)
		{
			aText = ((!aText.Equals(string.Empty)) ? aText : "  ");
			string path = Service.Get<SystemTextManager>().GenerateImageForString(aText, string.Empty, aFontSize, aFontColor, aBgColor, aMaxWidth, aWordWrap);
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
			GameObject gameObject = aText.gameObject;
			Object.DestroyImmediate(aText);
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
			return string.Empty;
		}

		public virtual int GetImageHeightForString(string aString, string aFont, int aFontSize, Color32 aFontColor, Color32 aFontBGColor, float aMaxWidth, bool aWordWrap)
		{
			return 100;
		}

		public virtual int GetIndexInTextAtPoint(string aString, string aFont, int aFontSize, float aMaxWidth, bool aWordWrap, float xPos, float yPos)
		{
			return -1;
		}
	}
}
