namespace Mix
{
	public static class MixConstants
	{
		public static class Offline
		{
			public const string TYPE_AVATAR = "avatar";

			public const string TYPE_PUSH = "push";
		}

		public static class Prefs
		{
			public const string DEFAULT_PRIMARY_COLOR = "default_primary_color";

			public const string DEFAULT_SECONDARY_COLOR = "default_secondary_color";
		}

		public static class Kochava
		{
			public const string APP_ID = "mobilenetwork.kochava.appID";
		}

		public static class DMOAnalytics
		{
			public const string KEY = "mobilenetwork.dmoAnalytics.key";

			public const string SECRET = "mobilenetwork.dmoAnalytics.secret";
		}

		public static class HockeyApp
		{
			public const string BUILD_SETTINGS_APP_ID = "mobilenetwork.hockeyApp.appID";
		}

		public static class BI
		{
			public static class Type
			{
				public const string NORMAL = "normal";

				public const string TRUSTED = "trusted";

				public const string ADULT = "adult";

				public const string CHILD = "child";

				public const string SAFE = "safe";

				public const string OPEN = "open";

				public const string ONE_ON_ONE = "1:1";

				public const string GROUP = "group";

				public const string RECOMMENDED = "recommended";

				public const string GROUP_CHAT = "group_chat";

				public const string INVITE_SPARK = "invite_spark";
			}

			public static class Buttons
			{
				public const string CHAT = "chat";

				public const string FRIEND_LIST = "friend_list";

				public const byte OU = 106;
			}

			public static class Actions
			{
				public const string ACCEPT = "accept";

				public const string CHALLENGE = "challenge";

				public const string CLOSE_CHALLENGE = "close_challenge";

				public const string CREATE_TAKEN_ADULT = "id_taken_adult";

				public const string CREATE_TAKEN_CHILD = "id_taken_child";

				public const string CREATE_TAKEN_DISPLAYNAME_ADULT = "display_name_taken_adult";

				public const string CREATE_TAKEN_DISPLAYNAME_CHILD = "display_name_taken_child";

				public const string DECLINE = "decline";

				public const string END = "end";

				public const string END_CHALLENGE = "end_challenge";

				public const string FAIL = "fail";

				public const string FAIL_ADULT = "fail_adult";

				public const string FAIL_CHILD = "fail_child";

				public const string EXIT = "exit";

				public const string INSTRUCT = "instruct";

				public const string LOGOUT = "logout";

				public const string NO_PASS = "no_pass";

				public const string PASS = "pass";

				public const string PAUSE = "pause";

				public const string RECEIVE = "receive";

				public const string REMATCH = "rematch_challenge";

				public const string REPLAY = "replay";

				public const string REPLAY_GAG = "replay_gag";

				public const string RESEND = "resend";

				public const string RESET_PASSWORD = "reset_pwd";

				public const string SENT_REMINDER = "sent_reminder";

				public const string SKIP = "skip";

				public const string VIEW_FIRST = "view_first";

				public const string SUCCESS = "success";

				public const string SUCCESS_ADULT = "success_adult";

				public const string SUCCESS_CHILD = "success_child";

				public const string TITLE = "title";

				public const string VIEW_CHAT_INFO = "view_chat_info";

				public const string VIEW_RECENT = "view_recent";

				public const string VIEW_STICKERS = "view_stickers";

				public const string VIEW_GAGS = "view_gags";

				public const string VIEW_MEDIA = "view_photo_videos";

				public const string VIEW_GAMES = "view_games";

				public const string VIEW_PHOTOS = "view_photos";

				public const string INVITE_SPARK = "invite_spark";

				public const string INVITE_FOF = "invite_fof";

				public const string INVITE_GROUP = "invite_group";

				public const string INVITE = "invite";

				public const string LEAVE = "leave";

				public const string UNFRIEND = "unfriend";

				public const string REPORT = "report";

				public const string VIEW = "view";

				public const string CHAT = "chat";

				public const string CHAT_SUMMARY = "chat_summary";

				public const string OFFLINE_CHAT_SUMMARY = "offline_chat_summary";

				public const string CHOOSE_STICKER = "choose_sticker";

				public const string CHOOSE_PHOTO = "choose_photo";

				public const string CHOOSE_VIDEO = "choose_video";

				public const string CHOOSE_LIBRARY = "choose_library";

				public const string SEND_STICKER = "send_sticker";

				public const string SEND_PHOTO = "send_photo";

				public const string SEND_VIDEO = "send_video";

				public const string SEND_GAG = "send_gag";

				public const string ADULT_ADD_TRUST = "adult_add_trust";

				public const string ACCEPT_ADD_TRUST = "accept_add_trust";

				public const string DECLINE_ADD_TRUST = "decline_add_trust";

				public const string START_LIST = "start_list";

				public const string ADD_NICKNAME = "add_nickname";

				public const string ADD_FRIEND = "add_friend";

				public const string VIEW_GAG_TARGETS = "view_gag_targets";

				public const string CHAT_ARRIVED = "chat_arrived";

				public const string STICKER_ARRIVED = "sticker_arrived";

				public const string GAG_ARRIVED = "gag_arrived";

				public const string GAME_ARRIVED = "game_arrived";

				public const string PHOTO_ARRIVED = "photo_arrived";

				public const string VIDEO_ARRIVED = "video_arrived";

				public const string SAVE_OUTFIT = "save_outfit";

				public const string CHOOSE_OUTFIT = "choose_outfit";

				public const string DELETE_OUTFIT = "delete_outfit";

				public const string PASSWORD_MISMATCH = "password_mismatch";

				public const string PASSWORD_INVALID = "invalid_password_format";

				public const string PASSWORD_SIZE = "invalid_password_size";

				public const string PASSWORD_TOO_COMMON = "password_too_common";

				public const string PASSWORD_MATCHES_PROFILE = "password_matches_profile";

				public const string USERNAME_INVALID = "invalid_username";

				public const string EMAIL_INVALID = "invalid_email";

				public const string EMAIL_IN_USE = "email_in_use";

				public const string PARENT_EMAIL_INVALID = "invalid_parent_email";

				public const string FIRSTNAME_INVALID = "invalid_first_name";

				public const string LASTNAME_INVALID = "invalid_last_name";

				public const string TOU_UNSELECTED = "unselected_tou";

				public const string VIEW_PASSWORD_RULES = "view_password_rules";

				public const string VIEW_APP_RULES = "view_app_rules";

				public const string VIEW_TERMS_OF_USE = "view_terms_of_use";

				public const string VIEW_PRIVACY_POLICY = "view_privacy";

				public const string VIEW_COPP = "view_child_privacy";

				public const string RANDOMIZE = "randomize";

				public const string CREATED_DISPLAY_NAME = "created_display_name";

				public const string SHOWED_POPUP = "showed_popup";

				public const string SOUND_ON = "sound_on";

				public const string SOUND_OFF = "sound_off";

				public const string NOTIFY_ON = "notify_on";

				public const string NOTIFY_OFF = "notify_off";

				public const string CHAT_VOICE_OVER_ON = "chat_voice_over_on";

				public const string CHAT_VOICE_OVER_OFF = "chat_voice_over_off";

				public const string CHAT_MODERATED = "chat_moderated";

				public const string BATTERY_LIFE_ON = "battery_save_on";

				public const string BATTERY_LIFE_OFF = "battery_save_off";

				public const string FAIL_CONNECT = "fail_connect";

				public const string BANNED = "banned";

				public const string HALL_PASS_EXPIRED = "hallpass_expired";

				public const string REVOKED = "revoked";

				public const string UNAVAILABLE = "unavailable";

				public const string PUSH_VISIBLE = "visible";

				public const string PUSH_SILENT = "silent";

				public const string PUSH_NONE = "none";

				public const string OA_FOLLOW = "oa_follow";

				public const string OA_UNFOLLOW = "oa_unfollow";

				public const string OA_CLICKED = "oa_clicked";

				public const string OA_VIEWED = "oa_viewed";

				public const string OA_VIDEO = "oa_video";

				public const string HIGH_FIVE_SCORE = "high_five_score";
			}

			public static class Context
			{
				public const string CHAT = "chat";

				public const string OFFICIAL_ACCOUNT_CHAT = "oa_chat";

				public const string CHAT_GAME = "chat_game";

				public const string AGE_GATE = "age_gate";

				public const string CREATE_DISID = "create_disid";

				public const string LANGUAGE = "language";

				public const string LOCAL_NOTE_CLICK = "local_note_click";

				public const string LOGIN = "login";

				public const string LOGOUT = "logout";

				public const string FORGOT_USERNAME = "forgot_name_disid";

				public const string FORGOT_PASSWORD = "forgot_pwd_disid";

				public const string FRIEND = "friend";

				public const string GROUP = "group";

				public const string GROUP_CHAT = "group_chat";

				public const string SET_NOTIFY_MESSAGE = "set_notify_message";

				public const string SETTINGS = "settings";

				public const string EDIT_APP_COLORS = "edit_app_colors";

				public const string EDIT_AVATAR = "edit_avatar";

				public const string PUSH_NOTE = "push_note";

				public const string FPS_FRIENDS = "fps_friends";

				public const string FPS_CHATS = "fps_chats";

				public const string FPS_IN_CHAT = "fps_in_chat";

				public const string FPS_AVATAR = "fps_avatar";

				public const string TUTORIAL_VIDEO = "tutorial_video";

				public const string ONE_LOGIN = "one_login";

				public const string FORCE_UPDATE = "force_update";

				public const string PUSH_NOTE_SETTING = "push_note_settings";
			}

			public static class Keys
			{
				public const string CONTEXT = "context";

				public const string ACTION = "action";

				public const string TYPE = "type";

				public const string MESSAGE = "message";

				public const string LOCATION = "location";

				public const string RESULT = "result";

				public const string USER_ID = "user_id";

				public const string PLAYER_ID = "player_id";

				public const string DOMAIN = "user_id_domain";

				public const string FROM = "from";

				public const string FROM_LOCATION = "from_location";

				public const string BUTTON_PRESSED = "button_pressed";

				public const string CREATIVE = "creative";

				public const string PLACEMENT = "placement";

				public const string ELAPSED_TIME = "elapsed_time";

				public const string PATH_NAME = "path_name";

				public const string LEVEL = "level";
			}

			public static class Values
			{
				public const string BI_DOMAIN = "di";

				public const string CHAT = "chat";

				public const string CHAT_GAME = "chat_game";

				public const string OPEN = "open";

				public const string SAFE = "safe";

				public const string ON = "on";

				public const string OFF = "off";

				public const string MESSAGE_RECEIVED = "message_received";

				public const string FRIEND_REQUEST = "friend_request";

				public const string FRIEND_REQUEST_ACCEPTED = "friend_request_accepted";

				public const string PUSH_NOTIFICATION = "Push Notification";

				public const string MAIN_BUTTON = "Main_Button";

				public const string MORE_DISNEY = "More_Disney";

				public const string IMPRESSION = "impression";

				public const string PHOTO = "photo";

				public const string VIDEO = "video";

				public const string TEXT = "text";

				public const string STICKER = "sticker";

				public const string GAG = "gag";

				public const string GAME = "game";

				public const string MISSING = "missing";

				public const string VIDEO_COMPLETED = "video_completed_";
			}

			public static class Methods
			{
				public const string PAGE_VIEW = "page_view";

				public const string PLAYER_INFO = "player_info";

				public const string USER_INFO = "user_info";

				public const string NAVIGATION_ACTION = "navigation_action";

				public const string AD_ACTION = "ad_action";

				public const string TIMING_ACTION = "timing";

				public const byte RS = 15;
			}

			public static class Results
			{
				public const string CACHED = "cached";

				public const string UNCACHED = "uncached";
			}

			public static class Locations
			{
				public const string START = "start";

				public const string LOADING = "loading";

				public const string LOGIN = "login";

				public const string FORGOT_USERNAME = "forgot_name_disid";

				public const string FORGOT_PASSWORD = "forgot_pwd_disid";

				public const string AGE_GATE = "age_gate";

				public const string CREATE_CHILD = "create_disid_child";

				public const string CREATE_ADULT = "create_disid_adult";

				public const string SEND_MASE_EMAIL = "send_mase_email";

				public const string SEND_NRT_EMAIL = "send_nrt_email";

				public const string MISSING_BIRTHDAY = "missing_birthday";

				public const string MISSING_INFO_CHILD = "missing_info_child";

				public const string MISSING_INFO_ADULT = "missing_info_adult";

				public const string CREATE_DISPLAY_NAME = "create_display_name";

				public const string FOUND_FRIEND = "found_friend_spark";

				public const string ALREADY_FRIEND = "already_friend_spark";

				public const string ALREADY_SENT_FRIEND = "already_sent_friend_spark";

				public const string INVITE_PHONE = "invite_phone_num";

				public const string FRIEND_LIST = "friend_list";

				public const string CREATE_GROUP = "create_group";

				public const string INVITE_GROUP = "invite_group";

				public const string GROUP_POPUP = "group_popup";

				public const string FRIEND_FIND_FAILED = "search_fail_friend_spark";

				public const string QR_SCANNER = "qr_scanner";

				public const string QR_CODE = "qr_code";

				public const string SETTINGS_CHILD = "settings_child";

				public const string SETTINGS_PARENT = "settings_parent";

				public const string CONTACT = "contact";

				public const string PRIVACY = "privacy";

				public const string CHILD_PRIVACY = "child_privacy";

				public const string TERMS_OF_USE = "terms_of_use";

				public const string LICENSE_CREDITS = "license_credits";

				public const string APP_RULES = "app_rules";

				public const string EDIT_AVATAR = "edit_avatar";

				public const string CREATE_AVATAR = "create_avatar";

				public const string HISTORY = "history";

				public const string OOPS_NONTRUSTED_CHAT = "oops_nontrusted_chat";

				public const string PROFILE = "profile";

				public const string ABOUT = "about";

				public const string MAIN_NAV = "main_nav";

				public const string HOME = "home";

				public const string FRIENDS = "friends";

				public const string CHAT = "chat";

				public const string GROUP_CHAT = "group_chat";

				public const string COPY_INVITE = "copy_invite_link";

				public const string START_TO_LOGIN = "start_to_login";

				public const string FROM_BACKGROUND = "from_background";

				public const string START_TO_CHAT = "start_to_chat";

				public const string FORCE_UPDATE_DISPLAYED = "forced_update_v";
			}

			public static class Levels
			{
				public const int HARD = 1;

				public const int SOFT = 0;
			}

			public const byte GE = 44;
		}

		public const string LOADER_CONTEXTUAL = "Prefabs/Ui/loaders/ContextualLoader";

		public const string PUSH_PRE_POPUP = "spark.PushNotifications.MessageShown";

		public const string DISPLAY_NAME_APPROVED_SEEN = "displayname.approved.seen";

		public const string AVATAR_SLOTS_USED = "avatar.slots.used";

		public const string UNREAD_MESSAGE_COUNT = "bi.unreadMessageCount";

		public const string TOOLTIP_3DTOUCH_CONVO = "3dtouch.tooltip.seen.convo";

		public const string TOOLTIP_3DTOUCH_FRIENDS = "3dtouch.tooltip.seen.friends";

		public const string TOOLTIP_3DTOUCH_PROFILE = "3dtouch.tooltip.seen.profile";

		public const string DEFAULT_PRIMARY_COLOR = "1C97D4";

		public const string DEFAULT_SECONDARY_COLOR = "ED374B";

		public const int MAX_RECENT_ITEMS = 12;

		public const int MAX_UNREAD_MESSAGE_COUNT_TO_DISPLAY = 99;

		public const int MAX_FRIEND_INVITES_TO_DISPLAY = 99;

		public const byte TR = 95;

		public static float CANVAS_WIDTH { get; set; }

		public static float CANVAS_HEIGHT { get; set; }
	}
}
