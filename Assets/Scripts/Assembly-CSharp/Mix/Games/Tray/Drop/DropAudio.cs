namespace Mix.Games.Tray.Drop
{
	public class DropAudio
	{
		public const string AudioPrefix = "Drop/";

		public const string PlayerJump = "SFX/Gameplay/Player/Jump";

		public const string PlayerLand = "SFX/Gameplay/Player/Land";

		public const string PlayerLandOnObstacle = "SFX/Gameplay/Player/LandOnObstacle";

		public const string PlayerLandOnFloor = "SFX/Gameplay/Player/LandOnGround";

		public const string BonusEarned = "SFX/Gameplay/Player/BonusEarned";

		public const string NewHighScore = "SFX/Gameplay/Player/NewHighScore";

		public const string PlatformAppearAnticipation = "SFX/Gameplay/Platform/AppearAnticipation";

		public const string PlatformAppear = "SFX/Gameplay/Platform/Appear";

		public const string PlatformDisappearWarning = "SFX/Gameplay/Platform/DisappearWarning";

		public const string PlatformDisappear = "SFX/Gameplay/Platform/Disappear";

		public const string UIMaskOpen = "SFX/UI/MaskOpen";

		public const string UIMaskClose = "SFX/UI/MaskClose";

		public const string UIButtonPress = "SFX/UI/ButtonPress";

		public const string UIFinalScoreBannerAppear = "SFX/UI/FinalScoreBannerAppear";

		public const string UIFinalScoreCounterTick = "SFX/UI/FinalScoreCounterTick";

		public const string UIFinalScoreCounterPunch = "SFX/UI/FinalScoreCounterPunch";

		public const string UIHighScoreBannerAppear = "SFX/UI/HighScoreBannerAppear";

		public const string IntroCutscene = "SFX/IntroCutScene";

		public const string Music = "MUS/Music";

		public const string MusicGameplaySwitch = "Gameplay";

		public const string MusicResultsSwitch = "Results";

		public static DropGame Game { get; set; }

		public static void PlaySound(string soundEventName)
		{
			BaseGameController.Instance.Session.SessionSounds.PlaySoundEvent("Drop/" + soundEventName, Game.gameObject);
		}

		public static void StopSound(string soundEventName)
		{
			BaseGameController.Instance.Session.SessionSounds.StopSoundEvent("Drop/" + soundEventName, Game.gameObject);
		}

		public static void PauseSound(string soundEventName)
		{
			BaseGameController.Instance.Session.SessionSounds.PauseSoundEvent("Drop/" + soundEventName, Game.gameObject);
		}

		public static void ResumeSound(string soundEventName)
		{
			BaseGameController.Instance.Session.SessionSounds.UnpauseSoundEvent("Drop/" + soundEventName, Game.gameObject);
		}

		public static void SwitchSound(string soundEventName, string switchName)
		{
			BaseGameController.Instance.Session.SessionSounds.SetSwitchEvent("Drop/" + soundEventName, switchName, Game.gameObject);
		}
	}
}
