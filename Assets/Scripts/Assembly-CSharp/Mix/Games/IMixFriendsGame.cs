using System.Collections.Generic;
using UnityEngine;

namespace Mix.Games
{
	public interface IMixFriendsGame
	{
		void OnFriendsLoaded(List<Material> aAvatarMaterials);
	}
}
