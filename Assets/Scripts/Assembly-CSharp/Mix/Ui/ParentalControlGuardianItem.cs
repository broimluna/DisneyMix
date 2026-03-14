using Disney.Mix.SDK;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class ParentalControlGuardianItem : IScrollItem
	{
		public interface IParentalControlGuardianItemListener
		{
			void Toggled(long aId);
		}

		private long id;

		private GameObject item;

		private GameObject inst;

		private ILinkedUser guardian;

		private bool check;

		private Toggle toggle;

		private bool turningOff;

		private IParentalControlGuardianItemListener currentListener;

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

		public bool isChecked
		{
			get
			{
				return toggle.isOn;
			}
		}

		public string Email
		{
			get
			{
				return guardian.Email;
			}
		}

		public ParentalControlGuardianItem(GameObject aGuardianItem, ILinkedUser aGuardian, bool aCheck, IParentalControlGuardianItemListener aListener)
		{
			item = aGuardianItem;
			guardian = aGuardian;
			check = aCheck;
			currentListener = aListener;
			turningOff = false;
		}

		GameObject IScrollItem.GenerateGameObject(bool aGenerateForHeightOnly)
		{
			inst = Object.Instantiate(item);
			toggle = inst.transform.Find("SelectedToggle").GetComponent<Toggle>();
			if (check)
			{
				toggle.isOn = true;
				toggle.interactable = false;
			}
			toggle.onValueChanged.AddListener(OnToggleValueChanged);
			Text component = inst.transform.Find("EmailText").GetComponent<Text>();
			component.text = guardian.Email;
			return inst;
		}

		void IScrollItem.Destroy()
		{
			toggle.onValueChanged.RemoveAllListeners();
		}

		public void TurnOffCheck()
		{
			turningOff = true;
			toggle.isOn = false;
			toggle.interactable = true;
			turningOff = false;
		}

		private void OnToggleValueChanged(bool isOn)
		{
			if (!turningOff)
			{
				toggle.interactable = false;
				currentListener.Toggled(Id);
			}
		}
	}
}
