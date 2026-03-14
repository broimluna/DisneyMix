using System;

namespace Disney.Mix.SDK.Internal
{
	public static class ChatThreadTypeConverter
	{
		public const string GroupChatServerType = "GROUP";

		public const string OneOnOneChatServerType = "ONE_ON_ONE";

		public const string OfficialAccountChatServerType = "OFFICIAL_ACCOUNT";

		public static string ConvertDatabaseTypeToServerType(ChatThreadDatabaseType databaseType)
		{
			object result;
			switch (databaseType)
			{
			case ChatThreadDatabaseType.GroupChatType:
				result = "GROUP";
				break;
			case ChatThreadDatabaseType.OfficialAccountChatType:
				result = "OFFICIAL_ACCOUNT";
				break;
			default:
				result = "ONE_ON_ONE";
				break;
			}
			return (string)result;
		}

		public static ChatThreadDatabaseType ConvertServerTypeToDatabaseType(string serverType)
		{
			switch (serverType)
			{
			case "GROUP":
				return ChatThreadDatabaseType.GroupChatType;
			case "ONE_ON_ONE":
				return ChatThreadDatabaseType.OneOnOneChatType;
			case "OFFICIAL_ACCOUNT":
				return ChatThreadDatabaseType.OfficialAccountChatType;
			default:
				throw new ArgumentException();
			}
		}

		public static bool ValidateServerType(string serverType)
		{
			return serverType == "GROUP" || serverType == "ONE_ON_ONE" || serverType == "OFFICIAL_ACCOUNT";
		}
	}
}
