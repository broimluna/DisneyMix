using Mix.Games.Tray.Friendzy.Data;
using Mix.Games.Tray.Friendzy.ResultAnimator;
using UnityEngine;

namespace Mix.Games.Tray.Friendzy.ResultShow
{
	public class ResultStage : MonoBehaviour
	{
		private ResultController mResultController;

		private ResultsAnimator mResultsAnim;

		private ResultMessage mResultMessage;

		private void Awake()
		{
			mResultsAnim = GetComponent<ResultsAnimator>();
		}

		public void SetResultController(ResultController aResultController)
		{
			mResultController = aResultController;
		}

		public void SetResultSprites(Result[] aResults)
		{
			mResultsAnim.LightAnim.gameObject.SetActive(true);
			Picture[] array = new Picture[aResults.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = aResults[i].Pic;
			}
			mResultsAnim.SetResultSprites(array);
		}

		public void SetChosenSprite(Picture aChosenResult)
		{
			mResultsAnim.SetChosenSprite(aChosenResult);
		}

		public void Done()
		{
			FriendzyGame.PlaySound("ContinueUI", FriendzyGame.SOUND_PREFIX);
			mResultController.ReceiveMessage(ref mResultMessage);
		}

		public void ReceiveMessage(ref ResultMessage message)
		{
			switch (message.Type)
			{
			case ResultMessageType.START_THE_SHOW:
			{
				mResultMessage = new ResultMessage();
				mResultMessage.Type = ResultMessageType.END_THE_SHOW;
				mResultMessage.FriendzyResultEntry = message.FriendzyResultEntry;
				Picture pic = message.FriendzyResultEntry.mResult.Pic;
				SetChosenSprite(pic);
				mResultsAnim.ReceiveMessage(ref message);
				break;
			}
			}
		}
	}
}
