using System;

namespace Disney.Mix.SDK
{
	public abstract class AbstractOfficialAccountFollowedEventArgs : EventArgs
	{
		public IOfficialAccount OfficialAccount { get; protected set; }
	}
}
