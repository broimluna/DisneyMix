using Mix.Data;
using Mix.DeviceDb;
using Mix.Session;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class TintImage : Image
	{
		public enum TintOptions
		{
			None = 0,
			PrimaryColor = 1,
			SecondaryColor = 2,
			TertiaryColor = 3
		}

		public TintOptions OfficialAccountTint;

		public TintOptions EnableTint;

		[HideInInspector]
		public Official_Account officialAccount;

		protected override void Awake()
		{
			OnColorThemeChanged();
			base.Awake();
		}

		public void OnColorThemeChanged()
		{
			float a = color.a;
			string text = null;
			string text2 = null;
			if (officialAccount != null && !OfficialAccountTint.Equals(TintOptions.None))
			{
				if (OfficialAccountTint.Equals(TintOptions.PrimaryColor))
				{
					color = Util.HexToColor(officialAccount.GetTintColor1());
				}
				else if (OfficialAccountTint.Equals(TintOptions.SecondaryColor))
				{
					color = Util.HexToColor(officialAccount.GetTintColor2());
				}
				else
				{
					color = Util.HexToColor(officialAccount.GetTintColor3());
				}
				color = new Color(color.r, color.g, color.b, a);
				return;
			}
			if (MixSession.IsValidSession)
			{
				IKeyValDatabaseApi keyValDocumentCollectionApi = Singleton<MixDocumentCollections>.Instance.keyValDocumentCollectionApi;
				text = keyValDocumentCollectionApi.LoadUserValue("default_primary_color");
				text2 = keyValDocumentCollectionApi.LoadUserValue("default_secondary_color");
			}
			if (EnableTint.Equals(TintOptions.PrimaryColor))
			{
				color = Util.HexToColor(string.IsNullOrEmpty(text) ? "1C97D4" : text);
			}
			else if (EnableTint.Equals(TintOptions.SecondaryColor))
			{
				color = Util.HexToColor(string.IsNullOrEmpty(text2) ? "ED374B" : text2);
			}
			color = new Color(color.r, color.g, color.b, a);
		}
	}
}
