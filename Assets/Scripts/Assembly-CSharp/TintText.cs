using Mix;
using Mix.Data;
using Mix.DeviceDb;
using Mix.Session;
using UnityEngine;
using UnityEngine.UI;

public class TintText : MonoBehaviour
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

	public void Awake()
	{
		OnColorThemeChanged();
	}

	public void OnColorThemeChanged()
	{
		Text component = base.gameObject.GetComponent<Text>();
		if (component == null)
		{
			return;
		}
		string text = null;
		string text2 = null;
		if (officialAccount != null && !OfficialAccountTint.Equals(TintOptions.None))
		{
			if (OfficialAccountTint.Equals(TintOptions.PrimaryColor))
			{
				component.color = Util.HexToColor(officialAccount.GetTintColor1());
			}
			else if (OfficialAccountTint.Equals(TintOptions.SecondaryColor))
			{
				component.color = Util.HexToColor(officialAccount.GetTintColor2());
			}
			else
			{
				component.color = Util.HexToColor(officialAccount.GetTintColor3());
			}
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
			component.color = Util.HexToColor(string.IsNullOrEmpty(text) ? "1C97D4" : text);
		}
		else if (EnableTint.Equals(TintOptions.SecondaryColor))
		{
			component.color = Util.HexToColor(string.IsNullOrEmpty(text) ? "ED374B" : text2);
		}
	}
}
