using Mix.Games.Session;
using UnityEngine;

namespace Mix.Games.Tray.MemeGenerator
{
	public interface IMemeImageOptionSelected
	{
		void DestroyImageOption(string aPath, Object aObject);

		Object GetImage(string aPath);

		void LoadImage(IGameAsset aSessionAsset, string aPath);

		void OnImageSelected(MemeImageOption aOption);
	}
}
