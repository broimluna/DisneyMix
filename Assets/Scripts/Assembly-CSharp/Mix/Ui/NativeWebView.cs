using Mix.Localization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class NativeWebView
	{
		private string url;

		private GameObject overlay;

		private GameObject holder;

		private int headerHeight;

		public NativeWebView(string aUrl, string aTitleToken = "")
		{
			url = aUrl;
			overlay = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Ui/WebviewOverlay"));
			holder = GameObject.Find("Overlay_Holder");
			overlay.name = "NativeWebView";
			overlay.transform.SetParent(holder.transform, false);
			RectTransform component = overlay.transform.Find("Header").GetComponent<RectTransform>();
			headerHeight = (int)Util.GetRectInPhysicalScreenSpace(component).height;
			GameObject gameObject = overlay.transform.Find("Header/CloseBtn").gameObject;
			UnityAction call = Close;
			gameObject.GetComponent<Button>().onClick.AddListener(call);
			GameObject gameObject2 = overlay.transform.Find("Header/HeaderTitleText").gameObject;
			gameObject2.GetComponent<Text>().text = Singleton<Localizer>.Instance.getString(aTitleToken);
			if (!aTitleToken.Equals(string.Empty))
			{
				gameObject2.SetActive(true);
			}
			Show();
		}

		public void Show()
		{
			EtceteraAndroid.inlineWebViewShow(url, 0, headerHeight, Singleton<SettingsManager>.Instance.GetScreenWidth(), Singleton<SettingsManager>.Instance.GetScreenHeight() - headerHeight);
		}

		public void Close()
		{
			EtceteraAndroid.inlineWebViewClose();
			Object.Destroy(overlay);
		}
	}
}
