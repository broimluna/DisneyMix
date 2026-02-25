using System;

namespace Mix.Ui
{
	public class CategoryToggledEventArgs : EventArgs
	{
		public CategoryItem CategoryItem;

		public bool State;

		public CategoryToggledEventArgs(CategoryItem aItem, bool aState)
		{
			CategoryItem = aItem;
			State = aState;
		}
	}
}
