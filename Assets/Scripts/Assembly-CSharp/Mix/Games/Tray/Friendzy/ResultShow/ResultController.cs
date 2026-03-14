using Mix.Games.Tray.Friendzy.Data;
using Mix.Games.Tray.Friendzy.LoadAsset;

namespace Mix.Games.Tray.Friendzy.ResultShow
{
	public class ResultController
	{
		private FriendzyGame mFriendzyGame;

		private ResultStage mResultStage;

		private Quiz mQuizReference;

		public ResultEntry FriendzyResultEntry { get; set; }

		public ResultController(FriendzyGame inFriendzyGame)
		{
			mFriendzyGame = inFriendzyGame;
			mResultStage = mFriendzyGame.FriendzyResultStage;
			mResultStage.SetResultController(this);
		}

		public void ReceiveMessage(ref ResultMessage aResultMessage)
		{
			switch (aResultMessage.Type)
			{
			case ResultMessageType.START_THE_SHOW:
				mResultStage.ReceiveMessage(ref aResultMessage);
				break;
			case ResultMessageType.END_THE_SHOW:
			{
				FriendzyMessage message = new FriendzyMessage
				{
					type = FriendzyMessageType.FRIENDZY_FINISHED,
					resultEntry = aResultMessage.FriendzyResultEntry
				};
				mFriendzyGame.ReceiveMessage(ref message);
				break;
			}
			case ResultMessageType.LOAD_RESULT_ASSETS:
				LoadResultAssets(aResultMessage.CategoryReference, aResultMessage.QuizReference);
				break;
			}
		}

		private void LoadResultAssets(Category aCategory, Quiz aQuiz)
		{
			Job job = new Job();
			job.CallBackWhenFinishedJob = JobHasFinished;
			FriendzyMessage message = new FriendzyMessage
			{
				type = FriendzyMessageType.LOAD_DATA,
				jobToDo = job
			};
			job.NumberOfItems = aQuiz.ResultTable.Length;
			job.ObjectsToLoad = new object[job.NumberOfItems];
			for (int i = 0; i < aQuiz.ResultTable.Length; i++)
			{
				aQuiz.ResultTable[i].Pic.SetPictureType(PictureType.SPRITE);
				job.ObjectsToLoad[i] = aQuiz.ResultTable[i].Pic;
			}
			mQuizReference = aQuiz;
			mFriendzyGame.ReceiveMessage(ref message);
		}

		private void JobHasFinished(Job aJob)
		{
			mResultStage.SetResultSprites(mQuizReference.ResultTable);
		}
	}
}
