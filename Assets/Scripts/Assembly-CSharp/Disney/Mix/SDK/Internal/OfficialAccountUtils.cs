namespace Disney.Mix.SDK.Internal
{
	public static class OfficialAccountUtils
	{
		public static bool IsOfficialAccountId(string id)
		{
			return id != null && id.StartsWith("OA-");
		}

		public static bool IsBotId(string id)
		{
			return id != null && id.StartsWith("bot-");
		}
	}
}
