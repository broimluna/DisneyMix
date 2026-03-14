using System;
using Disney.Mix.SDK.Internal.GuestControllerDomain;

namespace Disney.Mix.SDK.Internal
{
	public interface ILegalMarketingErrorsBuilder
	{
		void BuildErrors(ISession session, GuestApiErrorCollection errors, Action<BuildLegalMarketingErrorsResult> successCallback, Action failureCallback);
	}
}
