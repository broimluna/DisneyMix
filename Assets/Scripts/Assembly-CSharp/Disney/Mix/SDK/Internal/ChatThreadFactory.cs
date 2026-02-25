using System;
using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal
{
	public class ChatThreadFactory : IChatThreadFactory
	{
		private readonly AbstractLogger logger;

		private readonly IChatMessageRetrieverFactory chatMessageRetrieverFactory;

		private readonly IChatMessageSender messageSender;

		private readonly IChatMessageFactory chatMessageFactory;

		private readonly INotificationQueue notificationQueue;

		public ChatThreadFactory(AbstractLogger logger, IChatMessageRetrieverFactory chatMessageRetrieverFactory, IChatMessageSender messageSender, IChatMessageFactory chatMessageFactory, INotificationQueue notificationQueue)
		{
			this.logger = logger;
			this.chatMessageRetrieverFactory = chatMessageRetrieverFactory;
			this.messageSender = messageSender;
			this.chatMessageFactory = chatMessageFactory;
			this.notificationQueue = notificationQueue;
		}

		public IInternalOneOnOneChatThread CreateOneOnOneChatThread(IMixWebCallFactory mixWebCallFactory, long chatThreadId, IChatThreadTrustLevel trustLevel, string localUserId, long latestSequenceNumber, bool areSequenceNumbersIndexed, IList<IInternalRemoteChatMember> members, IUserDatabase userDatabase, IChatMessageParser chatMessageParser, IChatMessageHandler chatMessageHandler)
		{
			string hashedChatThreadId = IdHasher.HashId(chatThreadId.ToString());
			return new OneOnOneChatThread(logger, chatMessageRetrieverFactory, chatThreadId, hashedChatThreadId, messageSender, members, trustLevel, localUserId, latestSequenceNumber, areSequenceNumbersIndexed, chatMessageFactory, userDatabase, chatMessageParser, chatMessageHandler, mixWebCallFactory, notificationQueue);
		}

		public IInternalGroupChatThread CreateGroupChatThread(IMixWebCallFactory mixWebCallFactory, long chatThreadId, IChatThreadTrustLevel trustLevel, string localUserId, long latestSequenceNumber, bool areSequenceNumbersIndexed, IList<IInternalRemoteChatMember> members, IUserDatabase userDatabase, IChatMessageParser chatMessageParser, IChatMessageHandler chatMessageHandler)
		{
			string hashedChatThreadId = IdHasher.HashId(chatThreadId.ToString());
			return new GroupChatThread(logger, chatMessageRetrieverFactory, chatThreadId, hashedChatThreadId, messageSender, members, trustLevel, localUserId, latestSequenceNumber, areSequenceNumbersIndexed, chatMessageFactory, userDatabase, chatMessageParser, chatMessageHandler, mixWebCallFactory, notificationQueue);
		}

		public IInternalOfficialAccountChatThread CreateOfficialAccountChatThread(IMixWebCallFactory mixWebCallFactory, long chatThreadId, IChatThreadTrustLevel trustLevel, string localUserId, string officialAccountId, long latestSequenceNumber, bool areSequenceNumbersIndexed, IList<IInternalRemoteChatMember> members, IUserDatabase userDatabase, IChatMessageParser chatMessageParser, IChatMessageHandler chatMessageHandler)
		{
			string hashedChatThreadId = IdHasher.HashId(chatThreadId.ToString());
			IOfficialAccount officialAccount = null;
			if (localUserId != officialAccountId)
			{
				OfficialAccountDocument officialAccount2 = userDatabase.GetOfficialAccount(officialAccountId);
				if (officialAccount2 == null)
				{
					throw new ArgumentException("Unknown official account: " + officialAccountId);
				}
				officialAccount = OfficialAccountFactory.CreateOfficialAccount(officialAccount2.AccountId, officialAccount2.DisplayName, officialAccount2.IsAvailable, officialAccount2.CanUnfollow);
			}
			return new OfficialAccountChatThread(logger, chatMessageRetrieverFactory, chatThreadId, hashedChatThreadId, messageSender, officialAccount, members, trustLevel, localUserId, latestSequenceNumber, areSequenceNumbersIndexed, chatMessageFactory, userDatabase, chatMessageParser, chatMessageHandler, mixWebCallFactory, notificationQueue);
		}
	}
}
