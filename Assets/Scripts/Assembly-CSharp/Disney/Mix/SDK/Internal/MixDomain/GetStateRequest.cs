namespace Disney.Mix.SDK.Internal.MixDomain
{
	public class GetStateRequest : BaseUserRequest
	{
		public long? GameStateSince;

		public string ClientVersion;

		public long? AvatarSince;
	}
}
