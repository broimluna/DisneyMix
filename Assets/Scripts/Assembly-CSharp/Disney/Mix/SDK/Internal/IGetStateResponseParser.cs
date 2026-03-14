using System.Collections.Generic;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public interface IGetStateResponseParser
	{
		IInternalAvatar ParseAvatar(GetStateResponse response, IUserDatabase userDatabase, string swid, string displayName);

		IList<IInternalFriend> ParseFriendships(GetStateResponse response, IUserDatabase userDatabase);

		void ParseChatThreads(IMixWebCallFactory mixWebCallFactory, GetStateResponse response, string swid, IUserDatabase userDatabase, IChatMessageParser chatMessageParser, IChatMessageHandler chatMessageHandler, out IList<IInternalOneOnOneChatThread> oneOnOneChatThreads, out IList<IInternalGroupChatThread> groupChatThreads, out IList<IInternalOfficialAccountChatThread> officialAccountChatThreads);

		void ParseFriendshipInvitations(GetStateResponse response, IUserDatabase userDatabase, IInternalLocalUser localUser, out IList<IInternalIncomingFriendInvitation> incomingFriendInvitations, out IList<IInternalOutgoingFriendInvitation> outgoingFriendInvitations);

		void ReconcileWithLocalUser(IMixWebCallFactory mixWebCallFactory, GetStateResponse response, IInternalLocalUser localUser, IUserDatabase userDatabase, IChatMessageParser chatMessageParser, IChatMessageHandler chatMessageHandler);

		void ParsePollIntervals(GetStateResponse response, out int[] pollIntervals, out int[] pokeIntervals);

		IList<IInternalAlert> ParseAlerts(GetStateResponse response);

		IList<IOfficialAccount> ParseOfficialAccounts(GetStateResponse response, IUserDatabase userDatabase);

		List<BaseNotification> CollectNotifications(GetNotificationsResponse response);
	}
}
