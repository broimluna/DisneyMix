using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class CountryItem : IScrollItem
	{
		public interface ICountryListener
		{
			void OnCountryClicked(string aCountryName, string aCountryCode);
		}

		private long id;

		private GameObject item;

		private string countryName;

		private string countryCode;

		private ICountryListener currentListener;

		private GameObject inst;

		public long Id
		{
			get
			{
				return id;
			}
			set
			{
				id = value;
			}
		}

		public CountryItem(GameObject aCountryItem, string aName, string aCode, ICountryListener aListener)
		{
			item = aCountryItem;
			countryName = aName;
			countryCode = aCode;
			currentListener = aListener;
		}

		GameObject IScrollItem.GenerateGameObject(bool aGenerateForHeightOnly)
		{
			inst = Object.Instantiate(item);
			inst.transform.Find("CountryText").GetComponent<Text>().text = countryName;
			UnityAction call = onClicked;
			inst.GetComponent<Button>().onClick.AddListener(call);
			return inst;
		}

		void IScrollItem.Destroy()
		{
		}

		private void onClicked()
		{
			currentListener.OnCountryClicked(countryCode, countryName);
		}
	}
}
