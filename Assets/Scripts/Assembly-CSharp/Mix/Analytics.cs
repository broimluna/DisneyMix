using System.Collections;
using System.Collections.Generic;
using Disney.Mix.SDK;
using Disney.MobileNetwork;
using Mix.Avatar;
using Mix.Data;
using Mix.DeviceDb;
using Mix.Entitlements;
using Mix.Games.Data;
using Mix.Localization;
using Mix.Session;
using Mix.Session.Extensions;
using Mix.Ui;
using Mix.User;

namespace Mix
{
	public static class Analytics
	{
		public const string AB = "U8r";

		private static ArrayList gameDataQueue = new ArrayList();

		private static readonly object analytics_queue_lock = new object();

		public static bool IsAnalyticsReady()
		{
			return Service.IsSet<AnalyticsManager>();
		}

		public static void PushAnalyticsIfReady(AnalyticsData data)
		{
			lock (analytics_queue_lock)
			{
				if (IsAnalyticsReady())
				{
					data.LogAction();
					for (int i = 0; i < gameDataQueue.Count; i++)
					{
						AnalyticsData analyticsData = gameDataQueue[i] as AnalyticsData;
						if (analyticsData != null)
						{
							analyticsData.LogAction();
						}
					}
					gameDataQueue.Clear();
				}
				else
				{
					gameDataQueue.Add(data);
				}
			}
		}

		private static void AddPlayerId(IDictionary<string, object> gameData, string id = null)
		{
			if (id == null && MonoSingleton<LoginManager>.IsInstanceCreated() && MixSession.IsValidSession)
			{
				id = MixSession.User.Id;
			}
			if (id != null)
			{
				gameData.Add("player_id", id);
			}
		}

		public static void LogPageView(string location, string message = null, string type = null)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("location", location);
			AddPlayerId(dictionary);
			if (message != null)
			{
				dictionary.Add("message", message);
			}
			if (type != null)
			{
				dictionary.Add("type", type);
			}
			PushAnalyticsIfReady(new AnalyticsData("page_view", dictionary));
		}

		public static void LogLoadingPageView()
		{
			LogPageView("loading");
		}

		public static void LogStartPageView()
		{
			LogPageView("start");
		}

		public static void LogLoginPageView()
		{
			LogPageView("login");
		}

		public static void LogLoginPageViewAdAction()
		{
			LogAdAction("Main_Button", "More_Disney", "impression");
		}

		public static void LogForgotUsernamePageView()
		{
			LogPageView("forgot_name_disid");
		}

		public static void LogForgotPasswordPageView()
		{
			LogPageView("forgot_pwd_disid");
		}

		public static void LogAgeGatePageView()
		{
			LogPageView("age_gate");
		}

		public static void LogMaseEmailPageView()
		{
			LogPageView("send_mase_email");
		}

		public static void LogNrtEmailPageView()
		{
			LogPageView("send_nrt_email");
		}

		public static void LogCreateAdultPageView()
		{
			LogPageView("create_disid_adult");
		}

		public static void LogCreateChildPageView()
		{
			LogPageView("create_disid_child");
		}

		public static void LogCreateDisplayNamePageView()
		{
			LogPageView("create_display_name");
		}

		public static void LogSettingsPageView(AgeBandType ageBandType)
		{
			if (ageBandType == AgeBandType.Child)
			{
				LogPageView("settings_child");
			}
			else
			{
				LogPageView("settings_parent");
			}
		}

		public static void LogAppRulesPageView()
		{
			LogPageView("app_rules");
		}

		public static void LogNeedHelpPageView()
		{
			LogPageView("contact");
		}

		public static void LogTermsOfUsePageView()
		{
			LogPageView("terms_of_use");
		}

		public static void LogPrivacyPolicyPageView()
		{
			LogPageView("privacy");
		}

		public static void LogChildPrivacyPageView()
		{
			LogPageView("child_privacy");
		}

		public static void LogLicenseCreditsPageView()
		{
			LogPageView("license_credits");
		}

		public static void LogCreateAvatarPageView()
		{
			LogPageView("create_avatar");
		}

		public static void LogAvatarEditorPageView()
		{
			LogPageView("edit_avatar");
		}

		public static void LogProfilePageView()
		{
			LogPageView("profile");
		}

		public static void LogFoundFriendPageView(string friendDisid, string type)
		{
			LogPageView("found_friend_spark", friendDisid, type);
		}

		public static void LogFriendsListPageView()
		{
			LogPageView("friend_list");
		}

		public static void LogInviteAlreadySentPageView()
		{
			LogPageView("already_sent_friend_spark");
		}

		public static void LogQRScannerPageView()
		{
			LogPageView("qr_scanner");
		}

		public static void LogQRCodePageView()
		{
			LogPageView("qr_code");
		}

		public static void LogChatHistoryPageView()
		{
			LogPageView("history");
		}

		public static void LogUntrustedChatPageView()
		{
			LogPageView("oops_nontrusted_chat");
		}

		public static void LogForceUpdatePanelPageView(string version)
		{
			LogPageView("forced_update_v" + version);
		}

		public static void LogGameAction(string context, string action, string type = null, string message = null, string location = null, string userId = null, int level = -1)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("context", context);
			dictionary.Add("action", action);
			AddPlayerId(dictionary, userId);
			if (type != null)
			{
				dictionary.Add("type", type);
			}
			if (message != null)
			{
				dictionary.Add("message", message);
			}
			if (location != null)
			{
				dictionary.Add("location", location);
			}
			if (level != -1)
			{
				dictionary.Add("level", level);
			}
			PushAnalyticsIfReady(new AnalyticsData(string.Empty, dictionary));
		}

		public static void LogSendReminderUserName()
		{
			LogGameAction("forgot_name_disid", "sent_reminder");
		}

		public static void LogResetPassword()
		{
			LogGameAction("forgot_pwd_disid", "reset_pwd");
		}

		public static void LogSuccessfulLogin(AgeBandType ageBandType, int level)
		{
			string type = ((ageBandType != AgeBandType.Child) ? "adult" : "child");
			LogGameAction("login", "success", type, null, null, null, level);
			LogPushNotificationSetting();
		}

		public static void LogSuccessfulLoginLanguage()
		{
			LogGameAction("language", Localizer.GetLocale());
		}

		public static void LogFailedLogin(string error)
		{
			LogGameAction("login", "fail", error);
		}

		public static void LogAuthFailed(AgeBandType ageBandType, string reason)
		{
			string type = ((ageBandType != AgeBandType.Child) ? "adult" : "child");
			LogGameAction("one_login", reason, type);
		}

		public static void LogBirthdaySubmissionPass(int age)
		{
			LogGameAction("age_gate", "pass", age.ToString());
		}

		public static void LogBirthdaySubmissionFail(int age)
		{
			LogGameAction("age_gate", "no_pass", age.ToString());
		}

		public static void LogViewAppRules()
		{
			LogGameAction("create_disid", "view_app_rules");
		}

		public static void LogViewTermsOfUse()
		{
			LogGameAction("create_disid", "view_terms_of_use");
		}

		public static void LogViewPrivacyPolicy()
		{
			LogGameAction("create_disid", "view_privacy");
		}

		public static void LogViewCOPP()
		{
			LogGameAction("create_disid", "view_child_privacy");
		}

		public static void LogViewPasswordRules()
		{
			LogGameAction("create_disid", "view_password_rules");
		}

		public static void LogSuccessfulChildAccountCreation()
		{
			LogGameAction("create_disid", "success_child");
		}

		public static void LogSuccessfulAdultAccountCreation()
		{
			LogGameAction("create_disid", "success_adult");
		}

		public static void LogDisplaynameTaken(AgeBandType ageBandType)
		{
			if (ageBandType == AgeBandType.Child)
			{
				LogGameAction("create_disid", "display_name_taken_child");
			}
			else
			{
				LogGameAction("create_disid", "display_name_taken_adult");
			}
		}

		public static void LogUsernameTaken(AgeBandType ageBandType)
		{
			if (ageBandType == AgeBandType.Child)
			{
				LogGameAction("create_disid", "id_taken_child");
			}
			else
			{
				LogGameAction("create_disid", "id_taken_adult");
			}
		}

		public static void LogChildAccountCreationFailure(string reason)
		{
			LogGameAction("create_disid", "fail_child", reason);
		}

		public static void LogAdultAccountCreationFailure(string reason)
		{
			LogGameAction("create_disid", "fail_adult", reason);
		}

		public static void LogDisplayNameCreationSuccess()
		{
			LogGameAction("create_disid", "created_display_name");
		}

		public static void LogLogoutSuccess(string id, bool success)
		{
			LogGameAction("logout", (!success) ? "fail" : "success", null, null, null, id);
		}

		public static void LogSoundSettingsToggle(bool toggleValue)
		{
			LogGameAction("settings", (!toggleValue) ? "sound_off" : "sound_on");
		}

		public static void LogChatNotificationsToggle(bool toggleValue)
		{
			LogGameAction("settings", (!toggleValue) ? "notify_off" : "notify_on");
		}

		public static void LogChatVoiceOverToggle(bool toggleValue)
		{
			LogGameAction("settings", (!toggleValue) ? "chat_voice_over_off" : "chat_voice_over_on");
		}

		public static void LogBatteryLifeToggle(bool toggleValue)
		{
			LogGameAction("settings", (!toggleValue) ? "battery_save_off" : "battery_save_on");
		}

		public static void LogShowPushNotificationPopup()
		{
			LogGameAction("push_note", "showed_popup");
		}

		public static void LogPushNotificationSetting()
		{
			string action = "visible";
			if (!MonoSingleton<PushNotifications>.Instance.IsDeviceNotificationEnabled())
			{
				action = "silent";
			}
			else if (!Singleton<SettingsManager>.Instance.GetPushNotificationsSetting())
			{
				action = "silent";
			}
			LogGameAction("push_note_settings", action);
		}

		public static void LogAvatarOutfitSaveAction()
		{
			LogGameAction("edit_avatar", "save_outfit");
		}

		public static void LogAvatarOutfitChosenAction()
		{
			LogGameAction("edit_avatar", "choose_outfit");
		}

		public static void LogAvatarOutfitDeletedAction()
		{
			LogGameAction("edit_avatar", "delete_outfit");
		}

		public static void LogRandomizeAvatarAction(AvatarEditorController.EDITOR_MODES mode)
		{
			if (mode == AvatarEditorController.EDITOR_MODES.CREATOR)
			{
				LogGameAction("create_avatar", "randomize");
			}
			else
			{
				LogGameAction("edit_avatar", "randomize");
			}
		}

		public static void LogCreateAvatarSuccess(AvatarEditorController.EDITOR_MODES mode, IAvatar avatar)
		{
			if (avatar != null)
			{
				string message = MonoSingleton<AvatarManager>.Instance.api.SerializeAvatar(avatar, true);
				string context = ((mode != AvatarEditorController.EDITOR_MODES.CREATOR) ? "edit_avatar" : "create_avatar");
				LogGameAction(context, "success", avatar.Costume.SelectionKey, message);
			}
		}

		public static void LogAppColorChangeAction(string color)
		{
			LogGameAction("edit_app_colors", color);
		}

		public static void LogFriendRequestSent(string friendDisid, bool isTrusted)
		{
			if (isTrusted)
			{
				LogGameAction("friend", "invite_spark", "trusted", friendDisid);
			}
			else
			{
				LogGameAction("friend", "invite_spark", "normal", friendDisid);
			}
		}

		public static void LogFriendOfFriendRequestSent(string friendDisid, bool isTrusted)
		{
			if (isTrusted)
			{
				LogGameAction("friend", "invite_fof", "trusted", friendDisid);
			}
			else
			{
				LogGameAction("friend", "invite_fof", "normal", friendDisid);
			}
		}

		public static void LogGroupFriendRequestSent(string friendDisid, bool isTrusted)
		{
			if (isTrusted)
			{
				LogGameAction("friend", "invite_group", "trusted", friendDisid);
			}
			else
			{
				LogGameAction("friend", "invite_group", "normal", friendDisid);
			}
		}

		public static void LogAddNicknameAction(string friendDisid)
		{
			LogGameAction("friend", "add_nickname", null, null, friendDisid);
		}

		public static void LogNicknameGroupAction(IChatThread clientThread)
		{
			string type = ((!clientThread.TrustLevel.AllMembersTrustEachOther) ? "safe" : "open");
			LogGameAction(clientThread.GetAnalyticsContext(), "add_nickname", type, null, clientThread.GetAnalyticsId());
		}

		public static void LogUnfriendAction(string friendDisid)
		{
			LogGameAction("friend", "unfriend", null, friendDisid);
		}

		public static void LogAcceptNewFriendRequest(string friendDisid, bool trusted)
		{
			LogGameAction("friend", "accept", (!trusted) ? "normal" : "trusted", friendDisid);
		}

		public static void LogDeclineNewFriendRequest(string friendDisid, bool trusted)
		{
			LogGameAction("friend", "decline", (!trusted) ? "normal" : "trusted", friendDisid);
		}

		public static void LogAddTrustToExistingFriendship(string friendDisid)
		{
			LogGameAction("friend", "accept_add_trust", "trusted", friendDisid);
		}

		public static void LogDeclineTrustToExistingFriendship(string friendDisid)
		{
			LogGameAction("friend", "decline_add_trust", "trusted", friendDisid);
		}

		public static void LogViewGamesTray(IChatThread clientThread)
		{
			string type = ((!clientThread.TrustLevel.AllMembersTrustEachOther) ? "safe" : "open");
			LogGameAction(clientThread.GetAnalyticsContext(), "view_games", type, null, clientThread.GetAnalyticsId());
		}

		public static void LogViewStickersTray(IChatThread clientThread)
		{
			string type = ((!clientThread.TrustLevel.AllMembersTrustEachOther) ? "safe" : "open");
			LogGameAction(clientThread.GetAnalyticsContext(), "view_stickers", type, null, clientThread.GetAnalyticsId());
		}

		public static void LogViewGagsTray(IChatThread clientThread)
		{
			string type = ((!clientThread.TrustLevel.AllMembersTrustEachOther) ? "safe" : "open");
			LogGameAction(clientThread.GetAnalyticsContext(), "view_gags", type, null, clientThread.GetAnalyticsId());
		}

		public static void LogViewMediaTray(IChatThread clientThread)
		{
			string type = ((!clientThread.TrustLevel.AllMembersTrustEachOther) ? "safe" : "open");
			LogGameAction(clientThread.GetAnalyticsContext(), "view_photo_videos", type, null, clientThread.GetAnalyticsId());
		}

		public static void LogViewRecentTray(IChatThread clientThread)
		{
			string type = ((!clientThread.TrustLevel.AllMembersTrustEachOther) ? "safe" : "open");
			LogGameAction(clientThread.GetAnalyticsContext(), "view_recent", type, null, clientThread.GetAnalyticsId());
		}

		public static void LogStartChatButton()
		{
			LogGameAction("chat", "start_list");
		}

		public static void LogViewChat(IChatThread clientThread)
		{
			string type = ((!clientThread.TrustLevel.AllMembersTrustEachOther) ? "safe" : "open");
			LogGameAction(clientThread.GetAnalyticsContext(), "view", type, null, clientThread.GetAnalyticsId());
		}

		public static void LogSendChat(IChatThread clientThread)
		{
			string type = ((!clientThread.TrustLevel.AllMembersTrustEachOther) ? "safe" : "open");
			LogGameAction(clientThread.GetAnalyticsContext(), "chat", type, null, clientThread.GetAnalyticsId());
		}

		public static void LogChatFail(IChatThread clientThread)
		{
			string type = ((!clientThread.TrustLevel.AllMembersTrustEachOther) ? "safe" : "open");
			LogGameAction(clientThread.GetAnalyticsContext(), "fail_connect", type);
		}

		public static void LogSendSticker(IChatThread clientThread, string stickerName)
		{
			string type = ((!clientThread.TrustLevel.AllMembersTrustEachOther) ? "safe" : "open");
			LogGameAction(clientThread.GetAnalyticsContext(), "send_sticker", type, stickerName, clientThread.GetAnalyticsId());
		}

		public static void LogSendGag(IChatThread clientThread, string gagName)
		{
			string type = ((!clientThread.TrustLevel.AllMembersTrustEachOther) ? "safe" : "open");
			LogGameAction(clientThread.GetAnalyticsContext(), "send_gag", type, gagName, clientThread.GetAnalyticsId());
		}

		public static void LogReplayGag(MixGagItem gagItem, string gagName)
		{
			string type = ((!gagItem.thread.TrustLevel.AllMembersTrustEachOther) ? "safe" : "open");
			string analyticsId = gagItem.thread.GetAnalyticsId();
			LogGameAction(gagItem.thread.GetAnalyticsContext(), "replay_gag", type, gagName, analyticsId);
		}

		public static void LogReportChat(string context, string reason, IRemoteChatMember user)
		{
			LogGameAction(context, "report", reason, user.Id);
		}

		public static void LogGroupChatInfoAction(IChatThread clientThread)
		{
			string type = ((!clientThread.TrustLevel.AllMembersTrustEachOther) ? "safe" : "open");
			LogGameAction(clientThread.GetAnalyticsContext(), "view_chat_info", type, null, clientThread.GetAnalyticsId());
		}

		public static void LogAddFriendToGroupChat(IChatThread clientThread, IFriend friend)
		{
			string type = ((!clientThread.TrustLevel.AllMembersTrustEachOther) ? "safe" : "open");
			LogGameAction(clientThread.GetAnalyticsContext(), "add_friend", type, friend.Id, clientThread.GetAnalyticsId());
		}

		public static void LogUnreadChats()
		{
			uint totalUnreadMessageCount = MixChat.GetTotalUnreadMessageCount();
			int num = Singleton<MixDocumentCollections>.Instance.keyValDocumentCollectionApi.LoadUserValueAsInt("bi.unreadMessageCount");
			LogGameAction("chat", "offline_chat_summary", null, (totalUnreadMessageCount - num).ToString());
		}

		public static void LogLeaveChatPage(IChatThread clientThread, int numChatThreads)
		{
			string type = ((!clientThread.TrustLevel.AllMembersTrustEachOther) ? "safe" : "open");
			LogGameAction(clientThread.GetAnalyticsContext(), "chat_summary", type, numChatThreads.ToString(), clientThread.GetAnalyticsId());
		}

		public static void LogEnterGame(string gameName, IChatThread clientThread)
		{
			string action = "enter_" + gameName;
			string type = ((!clientThread.TrustLevel.AllMembersTrustEachOther) ? "safe" : "open");
			LogGameAction("chat_game", action, type, null, clientThread.GetAnalyticsId());
		}

		public static void LogGamePost(string gameName, MixGameData gameData, IChatThread clientThread)
		{
			string message = null;
			if (gameData is RunnerData)
			{
				RunnerData runnerData = gameData as RunnerData;
				if (runnerData.HasResponse(runnerData.Responses, MixSession.User.Id))
				{
					RunnerResponse myResponse = gameData.GetMyResponse(runnerData.Responses, MixSession.User.Id);
					message = myResponse.Checkpoints.ToString();
				}
			}
			string text = gameName.Trim().Replace(" ", "_");
			string type = ((!clientThread.TrustLevel.AllMembersTrustEachOther) ? "safe" : "open");
			LogGameAction("chat_game", "post_new_" + text, type, message, clientThread.GetAnalyticsId());
		}

		public static void LogResend(IChatThread clientThread)
		{
			string type = ((!clientThread.TrustLevel.AllMembersTrustEachOther) ? "safe" : "open");
			LogGameAction(clientThread.GetAnalyticsContext(), "resend", type);
		}

		public static void LogVideoSkip()
		{
			LogGameAction("tutorial_video", "skip");
		}

		public static void LogFirstPlayVideo()
		{
			LogGameAction("tutorial_video", "view_first");
		}

		public static void LogReplayPlayVideo()
		{
			LogGameAction("tutorial_video", "replay");
		}

		public static void LogGroupGagScreen(IChatThread clientThread)
		{
			string type = ((!clientThread.TrustLevel.AllMembersTrustEachOther) ? "safe" : "open");
			LogGameAction(clientThread.GetAnalyticsContext(), "view_gag_targets", type, null, clientThread.GetAnalyticsId());
		}

		public static void LogLocalNotificationClick(IChatMessage message)
		{
			string action = "chat_arrived";
			if (message is IGagMessage)
			{
				action = "gag_arrived";
			}
			else if (message is IStickerMessage)
			{
				action = "sticker_arrived";
			}
			else if (message is IPhotoMessage)
			{
				action = "photo_arrived";
			}
			else if (message is IVideoMessage)
			{
				action = "video_arrived";
			}
			else if (message is IGameEventMessage || message is IGameStateMessage)
			{
				action = "game_arrived";
			}
			LogGameAction("local_note_click", action);
		}

		public static void LogChatModerated(IChatThread clientThread)
		{
			string type = ((!clientThread.TrustLevel.AllMembersTrustEachOther) ? "safe" : "open");
			LogGameAction(clientThread.GetAnalyticsContext(), "chat_moderated", type, null, clientThread.GetAnalyticsId());
		}

		public static void LogForceUpdateClicked()
		{
			LogGameAction("force_update", "accept");
		}

		public static void LogUserInfoAction(string userId)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("user_id", userId.Replace("{", string.Empty).Replace("}", string.Empty));
			dictionary.Add("user_id_domain", "di");
			PushAnalyticsIfReady(new AnalyticsData("user_info", dictionary));
		}

		public static void LogNavigationAction(string from_location, string buttonPressed)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("from_location", from_location);
			dictionary.Add("button_pressed", buttonPressed);
			AddPlayerId(dictionary);
			PushAnalyticsIfReady(new AnalyticsData("navigation_action", dictionary));
		}

		public static void LogAdAction(string creative, string placement, string type)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("creative", creative);
			dictionary.Add("placement", placement);
			dictionary.Add("type", type);
			AddPlayerId(dictionary);
			PushAnalyticsIfReady(new AnalyticsData("ad_action", dictionary));
		}

		public static void LogPushNotificationAdAction(string creative)
		{
			LogAdAction(creative, "Push Notification", string.Empty);
		}

		public static void LogTimingAction(string context, string location, string elapsedTime, string pathName, string result = null)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("context", context);
			dictionary.Add("location", location);
			dictionary.Add("elapsed_time", elapsedTime);
			AddPlayerId(dictionary);
			if (result != null)
			{
				dictionary.Add("result", result);
			}
			if (pathName != null)
			{
				dictionary.Add("path_name", pathName);
			}
			PushAnalyticsIfReady(new AnalyticsData("timing", dictionary));
		}

		public static void LogLeaveChatPageTimingAction(int elapsedTime, IChatThread clientThread)
		{
			LogTimingAction(clientThread.GetAnalyticsContext(), clientThread.GetAnalyticsId(), elapsedTime.ToString(), null);
		}

		public static void LogCopyInviteLink()
		{
			LogPageView("copy_invite_link");
		}

		public static void LogFindFriendFailed()
		{
			LogPageView("search_fail_friend_spark");
		}

		public static void LogFollowOfficialAccount(IOfficialAccount account)
		{
			LogGameAction(account.AccountId, "oa_follow");
		}

		public static void LogUnFollowOfficialAccount(IOfficialAccount account)
		{
			LogGameAction(account.AccountId, "oa_unfollow");
		}

		public static void LogOAPhotoMessageClicked(IChatThread thread, IChatMessage message)
		{
			LogGameAction(thread.GetAnalyticsContext(), "oa_clicked", null, message.SenderId + "|photo|" + message.Id);
		}

		public static void LogOAVideoMessageClicked(IChatThread thread, IChatMessage message)
		{
			LogGameAction(thread.GetAnalyticsContext(), "oa_clicked", null, message.SenderId + "|video|" + message.Id);
		}

		public static void LogOAVideoClosed(IChatThread thread, IChatMessage message, int seconds)
		{
			LogGameAction(thread.GetAnalyticsContext(), "oa_video", null, message.SenderId + "|video_completed_true|" + seconds + "|" + message.Id);
		}

		public static void LogOAMessageRecieved(IChatThread thread, IChatMessage message)
		{
			string text;
			string text2;
			if (message is ITextMessage)
			{
				text = "text";
				text2 = string.Empty;
			}
			else if (message is IStickerMessage)
			{
				Sticker stickerData = Singleton<EntitlementsManager>.Instance.GetStickerData(((IStickerMessage)message).ContentId);
				text = "sticker";
				text2 = ((stickerData == null) ? "missing" : stickerData.GetName());
			}
			else if (message is IGagMessage)
			{
				Gag gagData = Singleton<EntitlementsManager>.Instance.GetGagData(((IGagMessage)message).ContentId);
				text = "gag";
				text2 = ((gagData == null) ? "missing" : gagData.GetName());
			}
			else if (message is IPhotoMessage)
			{
				text = "photo";
				text2 = message.Id;
			}
			else if (message is IVideoMessage)
			{
				text = "video";
				text2 = message.Id;
			}
			else
			{
				if (!(message is IGameStateMessage))
				{
					return;
				}
				text = "game";
				text2 = ((IGameStateMessage)message).GameName;
			}
			LogGameAction(thread.GetAnalyticsContext(), "oa_viewed", null, message.SenderId + "|" + text + "|" + text2);
		}
	}
}
