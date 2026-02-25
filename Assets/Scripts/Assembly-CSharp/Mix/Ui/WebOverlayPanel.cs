using Mix.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class WebOverlayPanel : BasePanel
	{
		public RectTransform Header;

		public Text Title;

		private string url;

		private int headerHeight;

		public void Init(string aUrl, string aTitleToken = "", string aTitle = "")
		{
			url = aUrl;
			headerHeight = (int)Util.GetRectInPhysicalScreenSpace(Header).height;
			if (!aTitle.Equals(string.Empty))
			{
				Title.text = aTitle;
				Title.gameObject.SetActive(true);
			}
			else
			{
				Title.text = Singleton<Localizer>.Instance.getString(aTitleToken);
				if (!aTitleToken.Equals(string.Empty))
				{
					Title.gameObject.SetActive(true);
				}
			}
			Invoke("ShowView", 0.5f);
		}

		private void OnDestroy()
		{
			EtceteraAndroid.inlineWebViewClose();
		}

		private void ShowView()
		{
			EtceteraAndroid.inlineWebViewShow(url, 0, headerHeight, Singleton<SettingsManager>.Instance.GetScreenWidth(), Singleton<SettingsManager>.Instance.GetScreenHeight() - headerHeight);
		}

		public override void ClosePanel()
		{
			EtceteraAndroid.inlineWebViewClose();
			base.ClosePanel();
		}
	}
}
