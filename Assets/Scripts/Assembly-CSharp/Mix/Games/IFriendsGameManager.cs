using System.Collections.Generic;
using UnityEngine;

namespace Mix.Games
{
	public interface IFriendsGameManager
	{
		string GetCurrentSwid();

		string[] GetPlayerIds(int aPlayerCount, string aCurrentPlayerSwid);

		void GetPlayerDna(List<GameObject> aFriendContainers, string[] aPlayerSwids, IMixFriendsGame aMixFriendsGame);
	}
}
