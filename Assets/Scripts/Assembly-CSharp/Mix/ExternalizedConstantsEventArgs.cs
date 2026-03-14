using System;

namespace Mix
{
	public class ExternalizedConstantsEventArgs : EventArgs
	{
		public enum RefreshStatus
		{
			Success = 0,
			NonFatalError = 1,
			FatalError = 2
		}

		public RefreshStatus Status { get; protected set; }

		public ExternalizedConstantsEventArgs(RefreshStatus status)
		{
			Status = status;
		}
	}
}
