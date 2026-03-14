using System;

namespace Mix.Ui
{
	public class SearchForUserEventArgs : EventArgs
	{
		public string DisplayName;

		public SearchForUserEventArgs(string aDisplayName)
		{
			DisplayName = aDisplayName;
		}
	}
}
