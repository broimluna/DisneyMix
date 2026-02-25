using System.Collections.Generic;
using Disney.Mix.SDK;
using Mix.DeviceDb;
using UnityEngine;

namespace Mix.Ui
{
	public class AvatarClosetPanel : MonoBehaviour
	{
		public IAvatar currentDna;

		public List<AvatarClosetItem> ClosetItems;

		public GameObject tooltip;

		private PanelCallback callback;

		[HideInInspector]
		public bool showTooltip;

		public void Show(IAvatar aDna, PanelCallback aCallback)
		{
			currentDna = aDna;
			callback = aCallback;
			for (int i = 0; i < ClosetItems.Count; i++)
			{
				ClosetItems[i].LoadSlot(i);
			}
			showTooltip = Singleton<MixDocumentCollections>.Instance.keyValDocumentCollectionApi.LoadUserValueAsBool("avatar.slots.used", true);
			if (showTooltip)
			{
				tooltip.SetActive(true);
			}
		}

		public void SetTooltip(bool state)
		{
			Singleton<MixDocumentCollections>.Instance.keyValDocumentCollectionApi.SaveUserValueFromBool("avatar.slots.used", false);
			showTooltip = state;
			tooltip.SetActive(state);
		}

		public void OnClosetSlotClicked(AvatarClosetItem item)
		{
			if (showTooltip)
			{
				if (ClosetItems.IndexOf(item) == 0)
				{
					SetTooltip(false);
					item.SaveSlot(0, currentDna);
				}
				return;
			}
			IAvatar slotDna = currentDna;
			if (item.IsOpenSlot)
			{
				int num = ClosetItems.IndexOf(item);
				if (num >= 0)
				{
					item.SaveSlot(num, currentDna);
					return;
				}
			}
			else if (item.slotDna != null)
			{
				slotDna = item.slotDna;
			}
			if (callback != null)
			{
				callback(slotDna);
			}
		}

		public void OnRemoveClosetClicked(AvatarClosetItem item)
		{
			if (!item.IsOpenSlot)
			{
				int num = ClosetItems.IndexOf(item);
				if (num >= 0)
				{
					item.DeleteSlot(num);
				}
			}
		}
	}
}
