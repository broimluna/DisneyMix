using System;

namespace Mix
{
	public class ConfigurationInitEventArgs : EventArgs
	{
		public enum InitStatus
		{
			Success = 0,
			NonFatalError = 1,
			FatalError = 2
		}

		public InitStatus Status { get; protected set; }

		public ConfigurationInitEventArgs(InitStatus status)
		{
			Status = status;
		}
	}
}
