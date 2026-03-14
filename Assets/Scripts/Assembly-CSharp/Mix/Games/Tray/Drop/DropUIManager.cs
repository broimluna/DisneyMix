using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	[RequireComponent(typeof(Canvas))]
	public class DropUIManager : MonoBehaviour
	{
		[HideInInspector]
		public List<DropUIScreen> LoadedUIScreens;

		public Canvas UICanvas { get; private set; }

		private void Awake()
		{
			LoadedUIScreens = new List<DropUIScreen>();
			UICanvas = GetComponent<Canvas>();
		}

		public bool IsUILoaded(DropUIScreen uiPrefab)
		{
			bool result = false;
			for (int i = 0; i < LoadedUIScreens.Count; i++)
			{
				if (LoadedUIScreens[i].name == uiPrefab.name)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		public DropUIScreen GetUI(DropUIScreen uiPrefab)
		{
			return LoadedUIScreens.Where((DropUIScreen x) => x.name.Equals(uiPrefab.name)).FirstOrDefault();
		}

		public DropUIScreen LoadUI(DropUIScreen uiPrefab)
		{
			DropUIScreen dropUIScreen = Object.Instantiate(uiPrefab);
			dropUIScreen.name = uiPrefab.name;
			dropUIScreen.transform.SetParent(base.transform, false);
			if (dropUIScreen.SpawnBehindCommonUI)
			{
				dropUIScreen.transform.SetAsFirstSibling();
			}
			LoadedUIScreens.Add(dropUIScreen);
			return dropUIScreen;
		}

		public void UnloadUI(DropUIScreen uiPrefab)
		{
			for (int i = 0; i < LoadedUIScreens.Count; i++)
			{
				if (LoadedUIScreens[i].name == uiPrefab.name)
				{
					LoadedUIScreens[i].HideAndDestroy();
					LoadedUIScreens.RemoveAt(i);
					break;
				}
			}
		}
	}
}
