using LitJson;
using Mix.Games.Chat;
using Mix.Games.Data;
using UnityEngine;

namespace Mix.Games.Tray.FortuneCookie
{
	public class FortuneCookiePostChatItem : BaseGameChatController
	{
		private const string COOKIE_FORTUNE_BOOL = "FortunePending";

		private const string COOKIE_RECEIVER_BOOL = "IsReceiver";

		public Animator CookieAnimator;

		public GameObject Paper;

		public GameObject PlayIcon;

		public override void SetupGameData(string aGameDataJson)
		{
			mGameData = JsonMapper.ToObject<FortuneCookieData>(aGameDataJson);
			mResponses = (mGameData as IMixGameData).GetResponses();
		}

		protected override void SetupView()
		{
			base.SetupView();
			mHolder.ContextualLoader.gameObject.SetActive(false);
			CookieAnimator.SetBool("IsReceiver", !mSession.IsMessageMine);
			int num = int.Parse(mEntitlement.GetAttempts());
			CookieAnimator.SetBool("FortunePending", mResponses.Count < num);
			bool active = CookieAnimator.GetBool("FortunePending") && CookieAnimator.GetBool("IsReceiver");
			Paper.SetActive(active);
			PlayIcon.SetActive(active);
		}

		public void OnClick()
		{
			if (!mSession.IsMessageMine)
			{
				int num = int.Parse(mEntitlement.GetAttempts());
				int count = (mGameData as FortuneCookieData).Responses.Count;
				if (count < num)
				{
					CookieAnimator.SetBool("FortunePending", false);
					FortuneCookieResponse myResponse = mGameData.GetMyResponse((mGameData as FortuneCookieData).Responses, mSession.PlayerId);
					mSession.UpdateSession(mGameData, myResponse);
					Paper.SetActive(false);
					PlayIcon.SetActive(false);
				}
			}
		}
	}
}
