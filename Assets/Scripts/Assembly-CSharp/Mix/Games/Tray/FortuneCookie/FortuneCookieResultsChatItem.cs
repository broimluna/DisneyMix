using System.Collections.Generic;
using LitJson;
using Mix.Games.Chat;
using Mix.Games.Data;
using Mix.Games.Message;
using Mix.Ui;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.FortuneCookie
{
	public class FortuneCookieResultsChatItem : BaseGameChatController
	{
		private const string COOKIE_OPEN_BOOL = "OpenCookie";

		private const string COOKIE_OPEN_COMPLETE_BOOL = "OpenComplete";

		private const string TEXT_REVEAL_BOOL = "RevealFortune";

		private const string TEXT_REVEAL_COMPLETE_BOOL = "RevealComplete";

		public Animator CookieAnimator;

		public Animator FortuneTextAnimator;

		public Text FortuneText;

		private AnimationEvents CookieAnimEvents;

		private void Start()
		{
			CookieAnimEvents = CookieAnimator.gameObject.GetComponent<AnimationEvents>();
			CookieAnimEvents.OnAnimationEnd += OnCookieOpened;
			CookieAnimEvents.OnAnimEvent += OnPaperRevealed;
			CookieAnimEvents.OnAnimationStart += OnAnimationStart;
			CookieAnimator.SetBool("OpenCookie", true);
		}

		private void OnEnabled()
		{
			if (CookieAnimEvents != null)
			{
				CookieAnimEvents.OnAnimationEnd += OnCookieOpened;
				CookieAnimEvents.OnAnimEvent += OnPaperRevealed;
			}
		}

		private void OnDisabled()
		{
			if (CookieAnimEvents != null)
			{
				CookieAnimEvents.OnAnimationEnd -= OnCookieOpened;
				CookieAnimEvents.OnAnimEvent -= OnPaperRevealed;
			}
		}

		private void OnAnimationStart()
		{
			Singleton<SoundManager>.Instance.PlaySoundEvent("MainApp/TrayGames/DailyFortune/CookieMessageReveal");
		}

		private void OnPaperRevealed()
		{
			FortuneTextAnimator.SetBool("RevealFortune", true);
		}

		private void OnCookieOpened()
		{
			CookieAnimator.SetBool("OpenComplete", true);
		}

		public override void SetupGameData(string aGameDataJson)
		{
			mGameResponseData = JsonMapper.ToObject<FortuneCookieResponse>(aGameDataJson);
			Dictionary<string, object> state = (mSession.MessageData as IGameEventMessageData).State;
			string json = (string)state["GameData"];
			mGameData = JsonMapper.ToObject<FortuneCookieData>(json);
			mResponses = (mGameData as IMixGameData).GetResponses();
			FortuneText.text = (mGameData as FortuneCookieData).Fortune;
		}

		protected override void SetupView()
		{
			base.SetupView();
			mHolder.ContextualLoader.gameObject.SetActive(false);
			Button[] componentsInChildren = mHolder.GetComponentsInChildren<Button>();
			Button[] array = componentsInChildren;
			foreach (Button button in array)
			{
				button.interactable = false;
			}
		}
	}
}
