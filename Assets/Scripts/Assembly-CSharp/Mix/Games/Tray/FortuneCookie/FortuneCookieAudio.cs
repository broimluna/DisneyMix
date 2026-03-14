namespace Mix.Games.Tray.FortuneCookie
{
	public class FortuneCookieAudio : GameAudio
	{
		public const string MainTheme = "Main";

		public const string Cancel = "CancelXButton";

		public const string CookieDrop = "CookieDropsToPlate";

		public const string PickUp1 = "CookiePickUp_1";

		public const string PickUp2 = "CookiePickUp_2";

		public const string PickUp3 = "CookiePickUp_3";

		public const string PickUp4 = "CookiePickUp_4";

		public const string PickUp5 = "CookiePickUp_5";

		public const string PlateSet = "PlateSet";

		public const string SceneChange = "SceneChange";

		public const string SelectFriend = "SelectFriend";

		public const string CookieBreak = "CookieBreak";

		public const string Invitation = "CookieInvitation";

		public const string PushNotification = "PushNotification";

		public const string RevealFortune = "CookieMessageRevealed";

		protected override string GAME_PREFIX
		{
			get
			{
				return "DailyFortune_";
			}
		}
	}
}
