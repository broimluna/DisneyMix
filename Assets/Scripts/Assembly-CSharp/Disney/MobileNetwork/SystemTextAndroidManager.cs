using System.Text;
using UnityEngine;

namespace Disney.MobileNetwork
{
	public class SystemTextAndroidManager : SystemTextManager
	{
		private AndroidJavaObject androidPlugin;

		protected override void Init()
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.disney.mobilenetwork.plugins.SystemTextPlugin"))
			{
				if (androidJavaClass != null)
				{
					androidPlugin = androidJavaClass.CallStatic<AndroidJavaObject>("getInstance", new object[0]);
				}
			}
		}

		public override int GetImageHeightForString(string aString, string aFont, int aFontSize, Color32 aFontColor, Color32 aFontBGColor, float aMaxWidth, bool aWordWrap)
		{
			string text = "#FFFFFF";
			if (!aWordWrap)
			{
				aString = aString.Replace("\n", " ... ");
			}
			return androidPlugin.Call<int>("GetImageHeightForString", new object[6]
			{
				Encoding.UTF32.GetBytes(aString),
				aFont,
				aFontSize,
				text,
				aMaxWidth,
				aWordWrap
			});
		}

		public override string GenerateImageForString(string aString, string aFont, int aFontSize, Color32 aFontColor, Color32 aFontBGColor, float aMaxWidth, bool aWordWrap)
		{
			string text = "#" + aFontColor.r.ToString("X2") + aFontColor.g.ToString("X2") + aFontColor.b.ToString("X2");
			if (!aWordWrap)
			{
				aString = aString.Replace("\n", " ... ");
			}
			return androidPlugin.Call<string>("GenerateImageForString", new object[6]
			{
				Encoding.UTF32.GetBytes(aString),
				aFont,
				aFontSize,
				text,
				aMaxWidth,
				aWordWrap
			});
		}

		public override int GetIndexInTextAtPoint(string aString, string aFont, int aFontSize, float aMaxWidth, bool aWordWrap, float xPos, float yPos)
		{
			if (!aWordWrap)
			{
				aString = aString.Replace("\n", " ... ");
			}
			return androidPlugin.Call<int>("GetIndexAtPressPoint", new object[7]
			{
				Encoding.UTF32.GetBytes(aString),
				aFont,
				aFontSize,
				(int)aMaxWidth,
				aWordWrap,
				(int)xPos,
				(int)yPos
			});
		}
	}
}
