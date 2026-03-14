using UnityEngine;

namespace Mix.Ui
{
	public interface ISubSelectorEntry<T>
	{
		T GetContent();

		void Clean();

		U GetThumbComponent<U>() where U : Component;
	}
}
