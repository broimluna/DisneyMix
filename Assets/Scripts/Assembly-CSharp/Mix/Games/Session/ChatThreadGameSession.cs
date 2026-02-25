using Mix.Games.Data;
using Mix.Games.Message;

namespace Mix.Games.Session
{
	public class ChatThreadGameSession : GameSession
	{
		protected IGameTray mGameManager;

		protected string mParentMessageSenderId;

		public bool IsGroupMessage
		{
			get
			{
				return mThreadParameters.IsGroupSession;
			}
		}

		public bool IsMessageMine
		{
			get
			{
				return mMessageData.IsMine;
			}
		}

		public string SenderId
		{
			get
			{
				return mMessageData.SenderId;
			}
		}

		public string SenderName
		{
			get
			{
				string aPlayerId = ((!(mMessageData is IGameEventMessageData)) ? (mMessageData as IGameStateMessageData).SenderId : (mMessageData as IGameEventMessageData).StateMessageSenderId);
				return GetPlayerName(aPlayerId);
			}
		}

		public ChatThreadGameSession(IGameSession aGameSessionHandler, IGameTray aGameManager, IGameThreadParameters aThreadParameters, IGameMessageData aMessageData)
		{
			mGameSessionHandler = aGameSessionHandler;
			mGameSessionAssetManager = aGameSessionHandler as IGameAssetManager;
			mGameSessionLocalization = aGameSessionHandler as IGameLocalization;
			mGameSessionPlayer = aGameSessionHandler as IGamePlayerInfo;
			mGameSessionSettings = aGameSessionHandler as IGameSessionSettings;
			mFriendsGameSessionHandler = aGameSessionHandler as IGameAvatar;
			mGameManager = aGameManager;
			mThreadParameters = aThreadParameters;
			mMessageData = aMessageData;
		}

		public void LoadGame(IEntitlementGameData aGameData)
		{
			mEntitlement = aGameData;
			mGameSessionHandler.LoadGame(this, mGameManager);
		}
	}
}
