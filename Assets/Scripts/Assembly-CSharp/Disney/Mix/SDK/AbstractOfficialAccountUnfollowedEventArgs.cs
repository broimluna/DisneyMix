using System;

namespace Disney.Mix.SDK
{
	public abstract class AbstractOfficialAccountUnfollowedEventArgs : EventArgs
	{
		public IOfficialAccount ExOfficialAccount { get; protected set; }
	}
}
