using System.Collections.Generic;
using UnityEngine;

namespace Mix.Games
{
	public class FriendsLoader
	{
		private IFriendsGameManager mFriendsGameManager;

		private int mAvatarsToCreate;

		private IMixFriendsGame mMixFriendsGame;

		public int NumberAvatarsToCreate
		{
			get
			{
				return mAvatarsToCreate;
			}
		}

		public FriendsLoader(IFriendsGameManager aFriendsGameManager)
		{
			mFriendsGameManager = aFriendsGameManager;
		}

		public void GetFriends(List<GameObject> aFriendContainers, IMixFriendsGame aFriendsGame)
		{
			string currentSwid = mFriendsGameManager.GetCurrentSwid();
			mAvatarsToCreate = aFriendContainers.Count;
			mMixFriendsGame = aFriendsGame;
			string[] playerIds = mFriendsGameManager.GetPlayerIds(mAvatarsToCreate, currentSwid);
			mFriendsGameManager.GetPlayerDna(aFriendContainers, playerIds, mMixFriendsGame);
		}
	}
}
