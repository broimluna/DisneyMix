using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal.MixDomain
{
	public class GetStateResponse : BaseResponse
	{
		public List<User> Users;

		public List<UserNickname> UserNicknames;

		public List<Friendship> Friendships;

		public List<string> OfficialAccounts;

		public List<ChatThread> ChatThreads;

		public List<ChatThreadNickname> ChatThreadNicknames;

		public List<ChatThreadUnreadMessageCount> ChatThreadUnreadMessageCount;

		public List<long?> ChatThreadLatestMessageSequenceNumbers;

		public List<long?> ChatThreadLastSeenMessageSequenceNumbers;

		public List<FriendshipInvitation> FriendshipInvitations;

		public List<GameStateChatMessage> GameStateChatMessages;

		public List<Alert> Alerts;

		public List<int?> PollIntervals;

		public List<int?> PokeIntervals;

		public long? Timestamp;

		public long? NotificationSequenceCounter;

		public int? NotificationSequenceThreshold;

		public int? NotificationIntervalsJitter;
	}
}
