using System;

namespace Disney.Mix.SDK.Internal
{
	public interface IInternalSession : IDisposable, ISession
	{
		IInternalLocalUser InternalLocalUser { get; }
	}
}
