using UnityEngine;

namespace Mix.Ui
{
	public interface IScrollItem
	{
		GameObject GenerateGameObject(bool aGenerateForHeightOnly);

		void Destroy();
	}
}
