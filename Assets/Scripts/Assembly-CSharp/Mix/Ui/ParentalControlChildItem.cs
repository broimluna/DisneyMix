using System;
using Avatar;
using Disney.Mix.SDK;
using Mix.Avatar;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class ParentalControlChildItem : IScrollItem
	{
		public interface IParentalControlChildItemListener
		{
			void LinkClaimableChild(ILinkedUser Child, ParentalControlChildItem aItem);
		}

		private sealed class GenerateGameObject_003Ec__AnonStorey2D6
		{
			internal Transform imageTarget;

			internal EventHandler<AbstractAvatarChangedEventArgs> avatarUpdater;

			internal ParentalControlChildItem _003C_003Ef__this;

			internal void _003C_003Em__62E(bool success, Sprite sprite)
			{
				imageTarget = ((!MonoSingleton<AvatarManager>.Instance.AvatarHasGeo(_003C_003Ef__this.child.Avatar)) ? _003C_003Ef__this.inst.transform.Find("Content/AvatarImage/ImageTarget") : _003C_003Ef__this.inst.transform.Find("Content/AvatarImage/Mask/ImageTarget_Geo"));
				_003C_003Ef__this.imageTargetSize = (int)imageTarget.GetComponent<RectTransform>().rect.height;
				_003C_003Ef__this.cancelSnapshot = null;
				if (sprite != null && imageTarget != null && imageTarget.GetComponent<Image>() != null)
				{
					imageTarget.GetComponent<Image>().sprite = sprite;
					imageTarget.gameObject.SetActive(true);
				}
			}

			internal void _003C_003Em__62F(object sender, AbstractAvatarChangedEventArgs args)
			{
				Transform transform = ((!MonoSingleton<AvatarManager>.Instance.AvatarHasGeo(_003C_003Ef__this.child.Avatar)) ? _003C_003Ef__this.inst.transform.Find("Content/AvatarImage/ImageTarget") : _003C_003Ef__this.inst.transform.Find("Content/AvatarImage/Mask/ImageTarget_Geo"));
				if (!transform.Equals(imageTarget))
				{
					imageTarget.gameObject.SetActive(false);
				}
				imageTarget = transform;
				_003C_003Ef__this.imageTargetSize = (int)imageTarget.GetComponent<RectTransform>().rect.height;
				if (_003C_003Ef__this.cancelSnapshot != null)
				{
					_003C_003Ef__this.cancelSnapshot();
				}
				if (imageTarget != null && MonoSingleton<AvatarManager>.Instance != null)
				{
					_003C_003Ef__this.cancelSnapshot = MonoSingleton<AvatarManager>.Instance.GetSnapshotFromDna(args.Avatar, (AvatarFlags)0, _003C_003Ef__this.imageTargetSize, _003C_003Ef__this.snapshotDelegate);
				}
			}

			internal void _003C_003Em__630()
			{
				ParentalControlChildItem parentalControlChildItem = _003C_003Ef__this;
				parentalControlChildItem.OnAvatarChanged = (EventHandler<AbstractAvatarChangedEventArgs>)Delegate.Remove(parentalControlChildItem.OnAvatarChanged, _003C_003Ef__this.eventGenerator.GetEventHandler(_003C_003Ef__this.child, avatarUpdater));
			}
		}

		private long id;

		private GameObject item;

		private GameObject inst;

		private ILinkedUser child;

		private Toggle toggle;

		private bool check;

		private Action cancelSnapshot;

		private Action avatarUpdateCleanup;

		private int imageTargetSize;

		private SnapshotCallback snapshotDelegate;

		private SdkEvents eventGenerator = new SdkEvents();

		private IParentalControlChildItemListener currentListener;

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

		private event EventHandler<AbstractAvatarChangedEventArgs> OnAvatarChanged;

		public ParentalControlChildItem(GameObject aAddChildItem, ILinkedUser aChild, bool aCheck, IParentalControlChildItemListener aListener)
		{
			item = aAddChildItem;
			child = aChild;
			check = aCheck;
			currentListener = aListener;
		}

		GameObject IScrollItem.GenerateGameObject(bool aGenerateForHeightOnly)
		{
			GenerateGameObject_003Ec__AnonStorey2D6 CS_0024_003C_003E8__locals34 = new GenerateGameObject_003Ec__AnonStorey2D6();
			CS_0024_003C_003E8__locals34._003C_003Ef__this = this;
			inst = UnityEngine.Object.Instantiate(item);
			toggle = inst.transform.Find("Content/SelectedToggle").GetComponent<Toggle>();
			if (check)
			{
				toggle.isOn = true;
				toggle.interactable = false;
			}
			toggle.onValueChanged.AddListener(OnToggleValueChanged);
			CS_0024_003C_003E8__locals34.imageTarget = ((!MonoSingleton<AvatarManager>.Instance.AvatarHasGeo(child.Avatar)) ? inst.transform.Find("Content/AvatarImage/ImageTarget") : inst.transform.Find("Content/AvatarImage/Mask/ImageTarget_Geo"));
			imageTargetSize = (int)CS_0024_003C_003E8__locals34.imageTarget.GetComponent<RectTransform>().rect.height;
			snapshotDelegate = delegate(bool success, Sprite sprite)
			{
				CS_0024_003C_003E8__locals34.imageTarget = ((!MonoSingleton<AvatarManager>.Instance.AvatarHasGeo(CS_0024_003C_003E8__locals34._003C_003Ef__this.child.Avatar)) ? CS_0024_003C_003E8__locals34._003C_003Ef__this.inst.transform.Find("Content/AvatarImage/ImageTarget") : CS_0024_003C_003E8__locals34._003C_003Ef__this.inst.transform.Find("Content/AvatarImage/Mask/ImageTarget_Geo"));
				CS_0024_003C_003E8__locals34._003C_003Ef__this.imageTargetSize = (int)CS_0024_003C_003E8__locals34.imageTarget.GetComponent<RectTransform>().rect.height;
				CS_0024_003C_003E8__locals34._003C_003Ef__this.cancelSnapshot = null;
				if (sprite != null && CS_0024_003C_003E8__locals34.imageTarget != null && CS_0024_003C_003E8__locals34.imageTarget.GetComponent<Image>() != null)
				{
					CS_0024_003C_003E8__locals34.imageTarget.GetComponent<Image>().sprite = sprite;
					CS_0024_003C_003E8__locals34.imageTarget.gameObject.SetActive(true);
				}
			};
			cancelSnapshot = MonoSingleton<AvatarManager>.Instance.GetSnapshotFromDna(child.Avatar, (AvatarFlags)0, imageTargetSize, snapshotDelegate);
			CS_0024_003C_003E8__locals34.avatarUpdater = delegate(object sender, AbstractAvatarChangedEventArgs args)
			{
				Transform transform = ((!MonoSingleton<AvatarManager>.Instance.AvatarHasGeo(CS_0024_003C_003E8__locals34._003C_003Ef__this.child.Avatar)) ? CS_0024_003C_003E8__locals34._003C_003Ef__this.inst.transform.Find("Content/AvatarImage/ImageTarget") : CS_0024_003C_003E8__locals34._003C_003Ef__this.inst.transform.Find("Content/AvatarImage/Mask/ImageTarget_Geo"));
				if (!transform.Equals(CS_0024_003C_003E8__locals34.imageTarget))
				{
					CS_0024_003C_003E8__locals34.imageTarget.gameObject.SetActive(false);
				}
				CS_0024_003C_003E8__locals34.imageTarget = transform;
				CS_0024_003C_003E8__locals34._003C_003Ef__this.imageTargetSize = (int)CS_0024_003C_003E8__locals34.imageTarget.GetComponent<RectTransform>().rect.height;
				if (CS_0024_003C_003E8__locals34._003C_003Ef__this.cancelSnapshot != null)
				{
					CS_0024_003C_003E8__locals34._003C_003Ef__this.cancelSnapshot();
				}
				if (CS_0024_003C_003E8__locals34.imageTarget != null && MonoSingleton<AvatarManager>.Instance != null)
				{
					CS_0024_003C_003E8__locals34._003C_003Ef__this.cancelSnapshot = MonoSingleton<AvatarManager>.Instance.GetSnapshotFromDna(args.Avatar, (AvatarFlags)0, CS_0024_003C_003E8__locals34._003C_003Ef__this.imageTargetSize, CS_0024_003C_003E8__locals34._003C_003Ef__this.snapshotDelegate);
				}
			};
			this.OnAvatarChanged = (EventHandler<AbstractAvatarChangedEventArgs>)Delegate.Combine(this.OnAvatarChanged, eventGenerator.AddEventHandler(child, CS_0024_003C_003E8__locals34.avatarUpdater));
			avatarUpdateCleanup = delegate
			{
				ParentalControlChildItem _003C_003Ef__this = CS_0024_003C_003E8__locals34._003C_003Ef__this;
				_003C_003Ef__this.OnAvatarChanged = (EventHandler<AbstractAvatarChangedEventArgs>)Delegate.Remove(_003C_003Ef__this.OnAvatarChanged, CS_0024_003C_003E8__locals34._003C_003Ef__this.eventGenerator.GetEventHandler(CS_0024_003C_003E8__locals34._003C_003Ef__this.child, CS_0024_003C_003E8__locals34.avatarUpdater));
			};
			Text component = inst.transform.Find("Content/FirstNameText").GetComponent<Text>();
			component.text = child.FirstName;
			Text component2 = inst.transform.Find("Content/DisplayNameText").GetComponent<Text>();
			component2.text = child.DisplayName.Text;
			return inst;
		}

		void IScrollItem.Destroy()
		{
			if (cancelSnapshot != null)
			{
				cancelSnapshot();
			}
			if (avatarUpdateCleanup != null)
			{
				avatarUpdateCleanup();
			}
			toggle.onValueChanged.RemoveAllListeners();
		}

		public void DisableToggle()
		{
			toggle.interactable = false;
		}

		private void OnToggleValueChanged(bool isOn)
		{
			if (isOn)
			{
				currentListener.LinkClaimableChild(child, this);
			}
		}
	}
}
