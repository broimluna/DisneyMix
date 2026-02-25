using Mix.Games.Tray.Friendzy.Data;
using Mix.Games.Tray.Friendzy.Message;

namespace Mix.Games.Tray.Friendzy.ResultShow
{
	public class ResultMessage : IMessage
	{
		public ResultMessageType Type;

		public ResultEntry FriendzyResultEntry;

		public ResultEntry FriendzyOtherPlayerResultEntry;

		public Category CategoryReference;

		public Quiz QuizReference;

		public Picture FriendzyOtherPlayerResultPicture;

		public RelationshipState FriendzyRelationshipState;

		public ResultMessage()
		{
		}

		public ResultMessage(ResultMessageType aType, Category aCategoryReference, Quiz aQuizReference)
		{
			Type = aType;
			CategoryReference = aCategoryReference;
			QuizReference = aQuizReference;
		}

		public ResultMessage(ResultMessageType aType, ResultEntry aFriendzyResultEntry, PLAYER aWhatPlayerIsPlaying, Category aCategoryReference, Quiz aQuizReference, ResultEntry initialPlayerRE = null)
		{
			Type = aType;
			FriendzyResultEntry = aFriendzyResultEntry;
			FriendzyResultEntry.PlayerNumber = aWhatPlayerIsPlaying;
			CategoryReference = aCategoryReference;
			QuizReference = aQuizReference;
			FriendzyOtherPlayerResultEntry = initialPlayerRE;
		}
	}
}
