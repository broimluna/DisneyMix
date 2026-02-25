using System;
using System.Collections.Generic;
using Disney.Mix.SDK;
using Mix.Localization;
using Mix.Native;
using Mix.Session;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class FriendsActionBar : MonoBehaviour
	{
		public FindFriend FindFriendPanel;

		public Animator FindFriendAnimator;

		public ScrollView ScrollView;

		private bool preOpenQRPanel;

		private InviteItem.IInviteListener inviteListener;

		public event EventHandler OnPanelOpened = delegate
		{
		};

		public event EventHandler OnPanelClosed = delegate
		{
		};

		public event EventHandler<FriendInvitedEventArgs> OnFriendInvited = delegate
		{
		};

		private void Update()
		{
			MonoSingleton<FakeFriendManager>.Instance.HighlightAnimator(FakeFriendManager.TYPE_ADD_FRIEND, FindFriendAnimator, false);
			if (preOpenQRPanel)
			{
				OnQRCodeClicked();
				preOpenQRPanel = false;
			}
		}

		public void OnFindFriendClicked()
		{
			if (!Singleton<PanelManager>.Instance.IsPanelOpen())
			{
				FindFriendPanel.Show();
				this.OnPanelOpened(this, new EventArgs());
			}
		}

		public void OnCopyInviteLink()
		{
			if (!Singleton<PanelManager>.Instance.IsPanelOpen())
			{
				GenericPanel genericPanel = (GenericPanel)Singleton<PanelManager>.Instance.ShowPanel(Panels.GENERIC);
				string newValue = ((!MixSession.IsValidSession) ? string.Empty : MixSession.User.DisplayName.Text);
				string value = Singleton<Localizer>.Instance.getString("customtokens.friends.download_mix_text_with_displayname").Replace("#displayname#", newValue);
				string clipboardText = Singleton<Localizer>.Instance.getString("customtokens.friends.download_mix_text_with_displayname_clipboard").Replace("#displayname#", newValue);
				Singleton<PanelManager>.Instance.SetupTokens(new Dictionary<Text, string>
				{
					{ genericPanel.TitleText, "customtokens.friends.download_mix_header_text" },
					{ genericPanel.MessageText, value },
					{ genericPanel.ButtonOneText, "customtokens.panels.button_ok" }
				});
				Analytics.LogCopyInviteLink();
				MonoSingleton<NativeKeyboardManager>.Instance.Keyboard.SetClipboardText(clipboardText);
			}
		}

		public void OnQRCodeClicked()
		{
			if (Singleton<PanelManager>.Instance.IsPanelOpen())
			{
				return;
			}
			MonoSingleton<NativeKeyboardManager>.Instance.Keyboard.Hide();
			QRCodePanel qRCodePanel = (QRCodePanel)Singleton<PanelManager>.Instance.ShowPanel(Panels.QR_CODE);
			qRCodePanel.OnSearchForUser += delegate(object sender, SearchForUserEventArgs result)
			{
				if (!this.IsNullOrDisposed() && !FindFriendPanel.IsNullOrDisposed())
				{
					FindFriendPanel.SearchForFriendByDisplayName(result.DisplayName);
				}
			};
			if (ScrollView != null)
			{
				ScrollView.Hide();
			}
			qRCodePanel.PanelClosingEvent += OnQRPanelClosed;
			qRCodePanel.Init(inviteListener, true);
		}

		private void OnQRPanelClosed(BasePanel aPanel)
		{
			aPanel.PanelClosingEvent -= OnQRPanelClosed;
			if (ScrollView != null)
			{
				ScrollView.Show();
			}
		}

		private void OnFriendInvitedAction(IOutgoingFriendInvitation aInvite)
		{
			this.OnFriendInvited(this, new FriendInvitedEventArgs(aInvite));
		}

		public void PreOpenQRCodePanel()
		{
			preOpenQRPanel = true;
		}

		public void Init(IFriendListItem aListener, InviteItem.IInviteListener aInviteListener)
		{
			inviteListener = aInviteListener;
			FindFriendPanel.Init(aListener, OnFriendInvitedAction);
			FindFriendPanel.OnFindFriendShown += delegate
			{
				this.OnPanelOpened(this, new EventArgs());
			};
			FindFriendPanel.OnFindFriendHidden += delegate
			{
				this.OnPanelClosed(this, new EventArgs());
			};
		}
	}
}
