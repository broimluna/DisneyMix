using System;
using System.Collections.Generic;
using Avatar;
using Disney.Mix.SDK;
using Mix.Avatar;
using Mix.Session;
using Mix.Session.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class FriendResultPanel : BasePanel
	{
		public enum Location
		{
			FindFriend = 0,
			GroupRosterFriend = 1,
			RecommendedFriend = 2
		}

		public Text DisplayName;

		public Text FirstName;

		public Text HeaderTitle;

		public MonoBehaviour SharingParent;

		public Toggle SharingToggle;

		public Button PendingButton;

		public Button InviteButton;

		public AvatarObjectSpawner AvatarSpawner;

		private bool IsBusy;

		private IInvitableUser searchedUser;

		private Location fromLocation;

		private Action<IOutgoingFriendInvitation> onFriendInvited;

		private SdkActions actionGenerator = new SdkActions();

		public void Init(bool isInviting, IInvitableUser searchedUser, Location fromLocation, Action<IOutgoingFriendInvitation> aOnFriendInvited = null)
		{
			this.searchedUser = searchedUser;
			this.fromLocation = fromLocation;
			onFriendInvited = aOnFriendInvited;
			string text = searchedUser.FirstName;
			if (string.IsNullOrEmpty(searchedUser.FirstName))
			{
				text = searchedUser.DisplayName.Text;
				DisplayName.gameObject.SetActive(false);
			}
			else
			{
				DisplayName.text = MixChat.FormatDisplayName(searchedUser.DisplayName.Text);
			}
			HeaderTitle.text = text;
			FirstName.text = text;
			PendingButton.gameObject.SetActive(!isInviting);
			InviteButton.gameObject.SetActive(isInviting);
			GameObject cubeRig = AvatarSpawner.Init();
			cubeRig.SetActive(false);
			MonoSingleton<AvatarManager>.Instance.SkinAvatar(cubeRig, searchedUser.Avatar, (AvatarFlags)0, delegate
			{
				if (!this.IsNullOrDisposed() && cubeRig != null)
				{
					cubeRig.SetActive(true);
				}
			});
			if (LoadingController.HideManagePC)
			{
				SharingToggle.isOn = MixSession.User.AgeBandType != AgeBandType.Child;
				SharingParent.gameObject.SetActive(isInviting && MixSession.User.AgeBandType != AgeBandType.Child);
			}
			else
			{
				ILocalUser me = MixSession.User;
				if (me.AgeBandType != AgeBandType.Child)
				{
					SharingToggle.isOn = true;
					SharingParent.gameObject.SetActive(isInviting);
				}
				else
				{
					SharingParent.gameObject.SetActive(false);
					if (isInviting)
					{
						LocalLinkedUser localLinkedUser = new LocalLinkedUser();
						localLinkedUser.Swid = me.Id;
						localLinkedUser.AgeBand = me.AgeBandType;
						MixSession.User.GetChildTrustPermission(localLinkedUser, delegate(IPermissionResult permissionResult)
						{
							if (permissionResult.Success)
							{
								me.GetAdultVerificationRequirements(delegate(IGetAdultVerificationRequirementsResult getAdultVerificationRequirements)
								{
									if (getAdultVerificationRequirements.Success && SharingToggle != null && SharingParent != null)
									{
										if (!getAdultVerificationRequirements.IsRequired)
										{
											if (permissionResult.Status == ActivityApprovalStatus.Approved)
											{
												SharingToggle.isOn = true;
												SharingParent.gameObject.SetActive(true);
											}
										}
										else
										{
											me.GetLinkedGuardians(delegate(IGetLinkedUsersResult getLinkedUsersResult)
											{
												if (getLinkedUsersResult.Success)
												{
													IEnumerable<ILinkedUser> linkedUsers = getLinkedUsersResult.LinkedUsers;
													IEnumerator<ILinkedUser> enumerator = linkedUsers.GetEnumerator();
													if (enumerator.MoveNext())
													{
														if (permissionResult.Status == ActivityApprovalStatus.Approved)
														{
															SharingToggle.isOn = true;
															SharingParent.gameObject.SetActive(true);
														}
														else if (permissionResult.Status != ActivityApprovalStatus.Denied)
														{
															SharingToggle.isOn = false;
															SharingToggle.interactable = false;
															SharingParent.gameObject.SetActive(true);
															base.transform.Find("PanelBase/SetupPC").gameObject.SetActive(true);
															base.transform.Find("PanelBase/SetupPC/SetupPCBtn").GetComponent<Button>().onClick.AddListener(OnSetupPC);
														}
													}
													else
													{
														SharingToggle.isOn = false;
														SharingToggle.interactable = false;
														SharingParent.gameObject.SetActive(true);
														base.transform.Find("PanelBase/SetupPC").gameObject.SetActive(true);
														base.transform.Find("PanelBase/SetupPC/SetupPCBtn").GetComponent<Button>().onClick.AddListener(OnSetupPC);
													}
												}
											});
										}
									}
								});
							}
						});
					}
				}
			}
			if (!isInviting)
			{
				Analytics.LogInviteAlreadySentPageView();
			}
			IsBusy = false;
		}

		public void OnSetupPC()
		{
			NavigationRequest navigationRequest = new NavigationRequest("Prefabs/Screens/Settings/SettingsScreen", new TransitionAnimations());
			navigationRequest.AddData("setupPC", true);
			MonoSingleton<NavigationManager>.Instance.AddRequest(navigationRequest);
		}

		public void OnSendFriendRequest()
		{
			if (!IsBusy)
			{
				IsBusy = true;
				MixSession.User.SendFriendInvitation(searchedUser, SharingToggle.isOn, actionGenerator.CreateAction(delegate(ISendFriendInvitationResult result)
				{
					if (!this.IsNullOrDisposed() && !(base.gameObject == null))
					{
						ClosePanel();
						IsBusy = false;
						if (!result.Success)
						{
							if (result is ISendFriendInvitationAlreadyExistsResult)
							{
								OnFriendsError("customtokens.friends.friend_invite_pending");
							}
							else
							{
								OnFriendsError("customtokens.global.generic_error");
							}
						}
						else if (onFriendInvited != null)
						{
							onFriendInvited(result.Invitation);
						}
					}
				}));
			}
			InviteButton.interactable = false;
			if (fromLocation == Location.RecommendedFriend)
			{
				Analytics.LogFriendOfFriendRequestSent(searchedUser.DisplayName.Text, SharingToggle.isOn);
			}
			else if (fromLocation == Location.GroupRosterFriend)
			{
				Analytics.LogGroupFriendRequestSent(searchedUser.DisplayName.Text, SharingToggle.isOn);
			}
			else
			{
				Analytics.LogFriendRequestSent(searchedUser.DisplayName.Text, SharingToggle.isOn);
			}
			Singleton<SoundManager>.Instance.PlaySoundEvent("MainApp/UI/SFX_2_SendDisney");
		}

		public void OnFriendsError(string aLocToken)
		{
			GenericPanel genericPanel = (GenericPanel)Singleton<PanelManager>.Instance.ShowPanel(Panels.GENERIC);
			Singleton<PanelManager>.Instance.SetupTokens(new Dictionary<Text, string>
			{
				{ genericPanel.TitleText, null },
				{ genericPanel.MessageText, aLocToken }
			});
			Singleton<PanelManager>.Instance.SetupButtons(new Dictionary<Button, Action>
			{
				{
					genericPanel.ButtonOne,
					delegate
					{
					}
				},
				{ genericPanel.ButtonTwo, null }
			});
		}
	}
}
