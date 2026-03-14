using System.Collections;
using System.Collections.Generic;
using Avatar;
using Disney.Mix.SDK;
using Mix.Avatar;
using Mix.Localization;
using Mix.Session;
using Mix.Session.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class ReportAPlayerPanel : BasePanel
	{
		public const byte SN = 129;

		public Text FriendName;

		public Text DisplayName;

		public Animator ReasonAnimator;

		public Text ReasonText;

		public AvatarObjectSpawner AvatarSpawner;

		private IRemoteChatMember user;

		private string biContext;

		private string reasonTextToken;

		private IShowNotification listener;

		private Hashtable reasonStrings = new Hashtable();

		private ReportUserReason reportUserReason;

		private SdkActions actionGenerator = new SdkActions();

		private void Start()
		{
			reasonStrings.Add(ReportUserReason.ChatInappropriate.ToString(), "customtokens.reportplayer.chat_inappropriate");
			reasonStrings.Add(ReportUserReason.ChatRequestingPii.ToString(), "customtokens.reportplayer.chat_requesting_pii");
			reasonStrings.Add(ReportUserReason.ChatDisclosingPii.ToString(), "customtokens.reportplayer.chat_disclosing_pii");
			reasonStrings.Add(ReportUserReason.ChatEmbeddedMedia.ToString(), "customtokens.reportplayer.chat_embedded_media");
			reasonStrings.Add(ReportUserReason.ChatBullying.ToString(), "customtokens.reportplayer.chat_bullying");
			reasonStrings.Add(ReportUserReason.PlayerBadName.ToString(), "customtokens.reportplayer.player_bad_name");
			reasonTextToken = ReasonText.GetComponent<LocalizedText>().token;
		}

		public void Init(IRemoteChatMember aUser, string aBiContext, IShowNotification aListener)
		{
			user = aUser;
			biContext = aBiContext;
			listener = aListener;
			FriendName.text = aUser.NickFirstOrDisplayName();
			if (string.IsNullOrEmpty(FriendName.text))
			{
				DisplayName.gameObject.SetActive(false);
			}
			else
			{
				DisplayName.text = MixChat.FormatDisplayName(aUser.DisplayName.Text);
			}
			GameObject avatarCube = AvatarSpawner.Init();
			avatarCube.SetActive(false);
			MonoSingleton<AvatarManager>.Instance.SkinAvatar(avatarCube, user.Avatar, (AvatarFlags)0, delegate
			{
				if (!this.IsNullOrDisposed() && !avatarCube.IsNullOrDisposed())
				{
					avatarCube.SetActive(true);
				}
			});
			base.gameObject.SetActive(true);
		}

		public void OnCancelClicked()
		{
			if (reportUserReason == ReportUserReason.None)
			{
				ClosePanel();
				return;
			}
			ReasonAnimator.Play("Reasons_In");
			reportUserReason = ReportUserReason.None;
		}

		public void OnReasonClicked(string aReason)
		{
			reportUserReason = ReportUserReason.ChatNotAppropriate;
			if (aReason.Equals(ReportUserReason.ChatInappropriate.ToString()))
			{
				reportUserReason = ReportUserReason.ChatInappropriate;
			}
			else if (aReason.Equals(ReportUserReason.ChatRequestingPii.ToString()))
			{
				reportUserReason = ReportUserReason.ChatRequestingPii;
			}
			else if (aReason.Equals(ReportUserReason.ChatDisclosingPii.ToString()))
			{
				reportUserReason = ReportUserReason.ChatDisclosingPii;
			}
			else if (aReason.Equals(ReportUserReason.ChatBullying.ToString()))
			{
				reportUserReason = ReportUserReason.ChatBullying;
			}
			else if (aReason.Equals(ReportUserReason.PlayerBadName.ToString()))
			{
				reportUserReason = ReportUserReason.PlayerBadName;
			}
			else if (aReason.Equals(ReportUserReason.ChatEmbeddedMedia.ToString()))
			{
				reportUserReason = ReportUserReason.ChatEmbeddedMedia;
			}
			ReasonText.text = Singleton<Localizer>.Instance.getString(reasonTextToken).Replace("#reasonType#", Singleton<Localizer>.Instance.getString((string)reasonStrings[aReason]));
			ReasonAnimator.Play("Reasons_Out");
		}

		public void OnReportPlayer()
		{
			MixSession.User.ReportUser(user, reportUserReason, actionGenerator.CreateAction(delegate(IReportUserResult result)
			{
				if (!this.IsNullOrDisposed() && !(base.gameObject == null) && !(FriendName == null) && Singleton<SoundManager>.Instance != null && Singleton<Localizer>.Instance != null)
				{
					if (result != null && result.Success)
					{
						Singleton<SoundManager>.Instance.PlaySoundEvent("MainApp/UI/SFX_2_SendDisney");
						ShowResults(Singleton<Localizer>.Instance.getString("customtokens.reportplayer.report_success"), FriendName.text);
						Analytics.LogReportChat(biContext, reportUserReason.ToString(), user);
					}
					else
					{
						Singleton<SoundManager>.Instance.PlaySoundEvent("MainApp/UI/SFX_1_Error");
						ShowResults(Singleton<Localizer>.Instance.getString("customtokens.reportplayer.report_error"), FriendName.text);
					}
					ClosePanel();
				}
			}));
		}

		private void ShowResults(string aToken, string aFriendName)
		{
			if (listener != null)
			{
				listener.OnShowNotification(aToken);
				return;
			}
			GenericPanel genericPanel = (GenericPanel)Singleton<PanelManager>.Instance.ShowPanel(Panels.GENERIC);
			Singleton<PanelManager>.Instance.SetupTokens(new Dictionary<Text, string>
			{
				{ genericPanel.TitleText, aFriendName },
				{ genericPanel.MessageText, aToken },
				{ genericPanel.ButtonOneText, "customtokens.panels.button_ok" }
			});
		}
	}
}
