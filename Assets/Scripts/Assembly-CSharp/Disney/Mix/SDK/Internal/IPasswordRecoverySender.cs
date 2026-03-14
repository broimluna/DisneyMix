using System;

namespace Disney.Mix.SDK.Internal
{
	public interface IPasswordRecoverySender
	{
		void Send(string lookupValue, Action<ISendPasswordRecoveryResult> callback);
	}
}
