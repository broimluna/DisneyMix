using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal.MixDomain
{
	public class GetChatThreadSequenceNumberResponse : BaseResponse
	{
		public List<long?> SequenceNumbers;
	}
}
