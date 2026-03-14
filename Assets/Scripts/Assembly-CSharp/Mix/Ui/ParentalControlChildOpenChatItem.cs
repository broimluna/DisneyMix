using System;
using Avatar;
using Disney.Mix.SDK;
using Mix.Avatar;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class ParentalControlChildOpenChatItem : IScrollItem
	{
		private sealed class GenerateGameObject_003Ec__AnonStorey2D7
		{
			internal Transform imageTarget;

			internal EventHandler<AbstractAvatarChangedEventArgs> avatarUpdater;

			internal ParentalControlChildOpenChatItem _003C_003Ef__this;

			internal void _003C_003Em__631(IPermissionResult permissinResult1)
			{
				if (!permissinResult1.Success)
				{
					return;
				}
				_003C_003Ef__this.status = permissinResult1.Status;
				_003C_003Ef__this.enabled = _003C_003Ef__this.status == ActivityApprovalStatus.Approved;
				if (_003C_003Ef__this.enabled)
				{
					_003C_003Ef__this.toggle.isOn = true;
				}
				_003C_003Ef__this.toggle.onValueChanged.AddListener(delegate(bool aState)
				{
					_003C_003Ef__this.toggle.interactable = false;
					_003C_003Ef__this.enabled = aState;
					if (_003C_003Ef__this.enabled)
					{
						if (_003C_003Ef__this.status == ActivityApprovalStatus.Pending)
						{
							_003C_003Ef__this.parent.ApproveChildTrustPermission(_003C_003Ef__this.child, ActivityApprovalStatus.Approved, delegate(IPermissionResult permissionResult)
							{
								_003C_003Ef__this.toggle.interactable = true;
								if (permissionResult.Success)
								{
								}
							});
						}
						else
						{
							_003C_003Ef__this.parent.RequestTrustPermissionForChild(_003C_003Ef__this.child, delegate(IPermissionResult permissionResult)
							{
								if (!permissionResult.Success)
								{
									_003C_003Ef__this.toggle.interactable = true;
								}
								else if (permissionResult.Status == ActivityApprovalStatus.Pending)
								{
									_003C_003Ef__this.parent.ApproveChildTrustPermission(_003C_003Ef__this.child, ActivityApprovalStatus.Approved, delegate(IPermissionResult permissionResult2)
									{
										_003C_003Ef__this.toggle.interactable = true;
										if (permissionResult2.Success)
										{
										}
									});
								}
							});
						}
					}
					else
					{
						_003C_003Ef__this.parent.ApproveChildTrustPermission(_003C_003Ef__this.child, ActivityApprovalStatus.Denied, delegate(IPermissionResult permissionResult)
						{
							_003C_003Ef__this.toggle.interactable = true;
							if (permissionResult.Success)
							{
							}
						});
					}
				});
				_003C_003Ef__this.toggle.interactable = true;
			}

			internal void _003C_003Em__632(bool success, Sprite sprite)
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

			internal void _003C_003Em__633(object sender, AbstractAvatarChangedEventArgs args)
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

			internal void _003C_003Em__634()
			{
				ParentalControlChildOpenChatItem parentalControlChildOpenChatItem = _003C_003Ef__this;
				parentalControlChildOpenChatItem.OnAvatarChanged = (EventHandler<AbstractAvatarChangedEventArgs>)Delegate.Remove(parentalControlChildOpenChatItem.OnAvatarChanged, _003C_003Ef__this.eventGenerator.GetEventHandler(_003C_003Ef__this.child, avatarUpdater));
			}

			internal void _003C_003Em__635(bool aState)
			{
				_003C_003Ef__this.toggle.interactable = false;
				_003C_003Ef__this.enabled = aState;
				if (_003C_003Ef__this.enabled)
				{
					if (_003C_003Ef__this.status == ActivityApprovalStatus.Pending)
					{
						_003C_003Ef__this.parent.ApproveChildTrustPermission(_003C_003Ef__this.child, ActivityApprovalStatus.Approved, delegate(IPermissionResult permissinResult3)
						{
							_003C_003Ef__this.toggle.interactable = true;
							if (permissinResult3.Success)
							{
							}
						});
						return;
					}
					_003C_003Ef__this.parent.RequestTrustPermissionForChild(_003C_003Ef__this.child, delegate(IPermissionResult permissinResult2)
					{
						if (!permissinResult2.Success)
						{
							_003C_003Ef__this.toggle.interactable = true;
						}
						else if (permissinResult2.Status == ActivityApprovalStatus.Pending)
						{
							_003C_003Ef__this.parent.ApproveChildTrustPermission(_003C_003Ef__this.child, ActivityApprovalStatus.Approved, delegate(IPermissionResult permissionResult)
							{
								_003C_003Ef__this.toggle.interactable = true;
								if (permissionResult.Success)
								{
								}
							});
						}
					});
					return;
				}
				_003C_003Ef__this.parent.ApproveChildTrustPermission(_003C_003Ef__this.child, ActivityApprovalStatus.Denied, delegate(IPermissionResult permissinResult2)
				{
					_003C_003Ef__this.toggle.interactable = true;
					if (permissinResult2.Success)
					{
					}
				});
			}

			internal void _003C_003Em__636(IPermissionResult permissinResult3)
			{
				_003C_003Ef__this.toggle.interactable = true;
				if (permissinResult3.Success)
				{
				}
			}

			internal void _003C_003Em__637(IPermissionResult permissinResult2)
			{
				if (!permissinResult2.Success)
				{
					_003C_003Ef__this.toggle.interactable = true;
				}
				else
				{
					if (permissinResult2.Status != ActivityApprovalStatus.Pending)
					{
						return;
					}
					_003C_003Ef__this.parent.ApproveChildTrustPermission(_003C_003Ef__this.child, ActivityApprovalStatus.Approved, delegate(IPermissionResult permissionResult)
					{
						_003C_003Ef__this.toggle.interactable = true;
						if (permissionResult.Success)
						{
						}
					});
				}
			}

			internal void _003C_003Em__638(IPermissionResult permissinResult2)
			{
				_003C_003Ef__this.toggle.interactable = true;
				if (permissinResult2.Success)
				{
				}
			}

			internal void _003C_003Em__639(IPermissionResult permissinResult3)
			{
				_003C_003Ef__this.toggle.interactable = true;
				if (permissinResult3.Success)
				{
				}
			}
		}

		private long id;

		private GameObject item;

		private Toggle toggle;

		private GameObject inst;

		private ILocalUser parent;

		private ILinkedUser child;

		private bool firstTime;

		private ActivityApprovalStatus status;

		private bool enabled;

		private Action cancelSnapshot;

		private Action avatarUpdateCleanup;

		private int imageTargetSize;

		private SnapshotCallback snapshotDelegate;

		private SdkEvents eventGenerator = new SdkEvents();

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

		public ParentalControlChildOpenChatItem(GameObject aChildOpenChatItem, ILocalUser aParent, ILinkedUser aChild)
		{
			item = aChildOpenChatItem;
			parent = aParent;
			child = aChild;
			firstTime = true;
		}

		GameObject IScrollItem.GenerateGameObject(bool aGenerateForHeightOnly)
		{
			GenerateGameObject_003Ec__AnonStorey2D7 CS_0024_003C_003E8__locals57 = new GenerateGameObject_003Ec__AnonStorey2D7();
			CS_0024_003C_003E8__locals57._003C_003Ef__this = this;
			inst = UnityEngine.Object.Instantiate(item);
			toggle = inst.transform.Find("Content/Toggle").GetComponent<Toggle>();
			toggle.interactable = false;
			toggle.isOn = false;
			if (firstTime)
			{
				firstTime = false;
			}
			else
			{
				parent.GetChildTrustPermission(child, delegate(IPermissionResult permissinResult1)
				{
					if (permissinResult1.Success)
					{
						CS_0024_003C_003E8__locals57._003C_003Ef__this.status = permissinResult1.Status;
						CS_0024_003C_003E8__locals57._003C_003Ef__this.enabled = CS_0024_003C_003E8__locals57._003C_003Ef__this.status == ActivityApprovalStatus.Approved;
						if (CS_0024_003C_003E8__locals57._003C_003Ef__this.enabled)
						{
							CS_0024_003C_003E8__locals57._003C_003Ef__this.toggle.isOn = true;
						}
						CS_0024_003C_003E8__locals57._003C_003Ef__this.toggle.onValueChanged.AddListener(delegate(bool aState)
						{
							CS_0024_003C_003E8__locals57._003C_003Ef__this.toggle.interactable = false;
							CS_0024_003C_003E8__locals57._003C_003Ef__this.enabled = aState;
							if (CS_0024_003C_003E8__locals57._003C_003Ef__this.enabled)
							{
								if (CS_0024_003C_003E8__locals57._003C_003Ef__this.status == ActivityApprovalStatus.Pending)
								{
									CS_0024_003C_003E8__locals57._003C_003Ef__this.parent.ApproveChildTrustPermission(CS_0024_003C_003E8__locals57._003C_003Ef__this.child, ActivityApprovalStatus.Approved, delegate(IPermissionResult permissionResult)
									{
										CS_0024_003C_003E8__locals57._003C_003Ef__this.toggle.interactable = true;
										if (permissionResult.Success)
										{
										}
									});
								}
								else
								{
									CS_0024_003C_003E8__locals57._003C_003Ef__this.parent.RequestTrustPermissionForChild(CS_0024_003C_003E8__locals57._003C_003Ef__this.child, delegate(IPermissionResult permissionResult)
									{
										if (!permissionResult.Success)
										{
											CS_0024_003C_003E8__locals57._003C_003Ef__this.toggle.interactable = true;
										}
										else if (permissionResult.Status == ActivityApprovalStatus.Pending)
										{
											CS_0024_003C_003E8__locals57._003C_003Ef__this.parent.ApproveChildTrustPermission(CS_0024_003C_003E8__locals57._003C_003Ef__this.child, ActivityApprovalStatus.Approved, delegate(IPermissionResult permissionResult2)
											{
												CS_0024_003C_003E8__locals57._003C_003Ef__this.toggle.interactable = true;
												if (permissionResult2.Success)
												{
												}
											});
										}
									});
								}
							}
							else
							{
								CS_0024_003C_003E8__locals57._003C_003Ef__this.parent.ApproveChildTrustPermission(CS_0024_003C_003E8__locals57._003C_003Ef__this.child, ActivityApprovalStatus.Denied, delegate(IPermissionResult permissionResult)
								{
									CS_0024_003C_003E8__locals57._003C_003Ef__this.toggle.interactable = true;
									if (permissionResult.Success)
									{
									}
								});
							}
						});
						CS_0024_003C_003E8__locals57._003C_003Ef__this.toggle.interactable = true;
					}
				});
			}
			CS_0024_003C_003E8__locals57.imageTarget = ((!MonoSingleton<AvatarManager>.Instance.AvatarHasGeo(child.Avatar)) ? inst.transform.Find("Content/AvatarImage/ImageTarget") : inst.transform.Find("Content/AvatarImage/Mask/ImageTarget_Geo"));
			imageTargetSize = (int)CS_0024_003C_003E8__locals57.imageTarget.GetComponent<RectTransform>().rect.height;
			snapshotDelegate = delegate(bool success, Sprite sprite)
			{
				CS_0024_003C_003E8__locals57.imageTarget = ((!MonoSingleton<AvatarManager>.Instance.AvatarHasGeo(CS_0024_003C_003E8__locals57._003C_003Ef__this.child.Avatar)) ? CS_0024_003C_003E8__locals57._003C_003Ef__this.inst.transform.Find("Content/AvatarImage/ImageTarget") : CS_0024_003C_003E8__locals57._003C_003Ef__this.inst.transform.Find("Content/AvatarImage/Mask/ImageTarget_Geo"));
				CS_0024_003C_003E8__locals57._003C_003Ef__this.imageTargetSize = (int)CS_0024_003C_003E8__locals57.imageTarget.GetComponent<RectTransform>().rect.height;
				CS_0024_003C_003E8__locals57._003C_003Ef__this.cancelSnapshot = null;
				if (sprite != null && CS_0024_003C_003E8__locals57.imageTarget != null && CS_0024_003C_003E8__locals57.imageTarget.GetComponent<Image>() != null)
				{
					CS_0024_003C_003E8__locals57.imageTarget.GetComponent<Image>().sprite = sprite;
					CS_0024_003C_003E8__locals57.imageTarget.gameObject.SetActive(true);
				}
			};
			cancelSnapshot = MonoSingleton<AvatarManager>.Instance.GetSnapshotFromDna(child.Avatar, (AvatarFlags)0, imageTargetSize, snapshotDelegate);
			CS_0024_003C_003E8__locals57.avatarUpdater = delegate(object sender, AbstractAvatarChangedEventArgs args)
			{
				Transform transform = ((!MonoSingleton<AvatarManager>.Instance.AvatarHasGeo(CS_0024_003C_003E8__locals57._003C_003Ef__this.child.Avatar)) ? CS_0024_003C_003E8__locals57._003C_003Ef__this.inst.transform.Find("Content/AvatarImage/ImageTarget") : CS_0024_003C_003E8__locals57._003C_003Ef__this.inst.transform.Find("Content/AvatarImage/Mask/ImageTarget_Geo"));
				if (!transform.Equals(CS_0024_003C_003E8__locals57.imageTarget))
				{
					CS_0024_003C_003E8__locals57.imageTarget.gameObject.SetActive(false);
				}
				CS_0024_003C_003E8__locals57.imageTarget = transform;
				CS_0024_003C_003E8__locals57._003C_003Ef__this.imageTargetSize = (int)CS_0024_003C_003E8__locals57.imageTarget.GetComponent<RectTransform>().rect.height;
				if (CS_0024_003C_003E8__locals57._003C_003Ef__this.cancelSnapshot != null)
				{
					CS_0024_003C_003E8__locals57._003C_003Ef__this.cancelSnapshot();
				}
				if (CS_0024_003C_003E8__locals57.imageTarget != null && MonoSingleton<AvatarManager>.Instance != null)
				{
					CS_0024_003C_003E8__locals57._003C_003Ef__this.cancelSnapshot = MonoSingleton<AvatarManager>.Instance.GetSnapshotFromDna(args.Avatar, (AvatarFlags)0, CS_0024_003C_003E8__locals57._003C_003Ef__this.imageTargetSize, CS_0024_003C_003E8__locals57._003C_003Ef__this.snapshotDelegate);
				}
			};
			this.OnAvatarChanged = (EventHandler<AbstractAvatarChangedEventArgs>)Delegate.Combine(this.OnAvatarChanged, eventGenerator.AddEventHandler(child, CS_0024_003C_003E8__locals57.avatarUpdater));
			avatarUpdateCleanup = delegate
			{
				ParentalControlChildOpenChatItem _003C_003Ef__this = CS_0024_003C_003E8__locals57._003C_003Ef__this;
				_003C_003Ef__this.OnAvatarChanged = (EventHandler<AbstractAvatarChangedEventArgs>)Delegate.Remove(_003C_003Ef__this.OnAvatarChanged, CS_0024_003C_003E8__locals57._003C_003Ef__this.eventGenerator.GetEventHandler(CS_0024_003C_003E8__locals57._003C_003Ef__this.child, CS_0024_003C_003E8__locals57.avatarUpdater));
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
	}
}
