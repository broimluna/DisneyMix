using System.Collections.Generic;
using UnityEngine;

namespace Mix.Ui
{
	public interface INavigationManagerListener
	{
		void OnRequestComplete(NavigationRequest aRequest);

		List<ScreenHolder> GetCurrentGameObjects();

		ScreenHolder GetLastScreenHolder();

		GameObject GetCachedScreen(string aPrefabPath);
	}
}
