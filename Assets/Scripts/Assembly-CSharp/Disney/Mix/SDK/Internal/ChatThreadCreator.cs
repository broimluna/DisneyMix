using System;
using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class ChatThreadCreator : IChatThreadCreator
	{
		private readonly AbstractLogger logger;

		private readonly IMixWebCallFactory webCallFactory;

		private readonly IChatThreadFactory chatThreadFactory;

		private readonly string localUserId;

		private readonly IUserDatabase userDatabase;

		private readonly IChatMessageParser chatMessageParser;

		private readonly IChatMessageHandler chatMessageHandler;

		private readonly INotificationQueue notificationQueue;

		public ChatThreadCreator(AbstractLogger logger, IMixWebCallFactory webCallFactory, IChatThreadFactory chatThreadFactory, string localUserId, IUserDatabase userDatabase, IChatMessageParser chatMessageParser, IChatMessageHandler chatMessageHandler, INotificationQueue notificationQueue)
		{
			this.logger = logger;
			this.webCallFactory = webCallFactory;
			this.chatThreadFactory = chatThreadFactory;
			this.localUserId = localUserId;
			this.userDatabase = userDatabase;
			this.chatMessageParser = chatMessageParser;
			this.chatMessageHandler = chatMessageHandler;
			this.notificationQueue = notificationQueue;
		}

		private void CreateChatThread(string chatThreadType, IEnumerable<string> memberSwids, Action<ChatThread> successCallback, Action failureCallback)
		{
			try
			{
				AddChatThreadRequest addChatThreadRequest = new AddChatThreadRequest();
				addChatThreadRequest.ChatThreadType = chatThreadType;
				addChatThreadRequest.MemberUserIds = memberSwids.ToList();
				AddChatThreadRequest request = addChatThreadRequest;
				IWebCall<AddChatThreadRequest, AddChatThreadResponse> webCall = webCallFactory.ChatThreadPut(request);
				webCall.OnResponse += delegate(object sender, WebCallEventArgs<AddChatThreadResponse> e)
				{
					AddChatThreadResponse response = e.Response;
					if (NotificationValidator.Validate(response.Notification))
					{
						notificationQueue.Dispatch(response.Notification, delegate
						{
							successCallback(response.Notification.ChatThread);
						}, failureCallback);
					}
					else
					{
						logger.Critical("Failed to validate add chat thread response: " + JsonParser.ToJson(response));
						failureCallback();
					}
				};
				webCall.OnError += delegate
				{
					failureCallback();
				};
				webCall.Execute();
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				failureCallback();
			}
		}

		public void CreateOneOnOneChatThread(string memberSwid, Action<ChatThread> successCallback, Action failureCallback)
		{
			CreateChatThread("ONE_ON_ONE", new string[1] { memberSwid }, successCallback, failureCallback);
		}

		public void CreateGroupChatThread(IEnumerable<string> memberSwids, Action<ChatThread> successCallback, Action failureCallback)
		{
			CreateChatThread("GROUP", memberSwids, successCallback, failureCallback);
		}

		public IInternalOneOnOneChatThread CreateLocalOneOnOneChatThread(IMixWebCallFactory mixWebCallFactory, ChatThread chatThreadResponse)
		{
			ChatThreadTrustLevel trustLevel = new ChatThreadTrustLevel(chatThreadResponse.IsTrusted.Value);
			IList<IInternalRemoteChatMember> members = CreateChatThreadMembers(chatThreadResponse.Members);
			return chatThreadFactory.CreateOneOnOneChatThread(mixWebCallFactory, chatThreadResponse.ChatThreadId.Value, trustLevel, localUserId, 0L, true, members, userDatabase, chatMessageParser, chatMessageHandler);
		}

		public IInternalGroupChatThread CreateLocalGroupChatThread(IMixWebCallFactory mixWebCallFactory, ChatThread chatThreadResponse)
		{
			ChatThreadTrustLevel trustLevel = new ChatThreadTrustLevel(chatThreadResponse.IsTrusted.Value);
			IList<IInternalRemoteChatMember> members = CreateChatThreadMembers(chatThreadResponse.Members);
			return chatThreadFactory.CreateGroupChatThread(mixWebCallFactory, chatThreadResponse.ChatThreadId.Value, trustLevel, localUserId, 0L, true, members, userDatabase, chatMessageParser, chatMessageHandler);
		}

		public IInternalOfficialAccountChatThread CreateLocalOfficialAccountChatThread(IMixWebCallFactory mixWebCallFactory, long chatThreadId, string officialAccountId)
		{
			ChatThreadTrustLevel trustLevel = new ChatThreadTrustLevel(false);
			List<IInternalRemoteChatMember> members = new List<IInternalRemoteChatMember>();
			return chatThreadFactory.CreateOfficialAccountChatThread(mixWebCallFactory, chatThreadId, trustLevel, localUserId, officialAccountId, 0L, true, members, userDatabase, chatMessageParser, chatMessageHandler);
		}

		private IList<IInternalRemoteChatMember> CreateChatThreadMembers(IEnumerable<User> members)
		{
			List<IInternalRemoteChatMember> list = new List<IInternalRemoteChatMember>();
			foreach (User member in members)
			{
				if (!(member.UserId == localUserId))
				{
					IInternalRemoteChatMember item = RemoteUserFactory.CreateRemoteChatMember(member.UserId, member.DisplayName, member.FirstName, userDatabase);
					list.Add(item);
				}
			}
			return list;
		}
	}
}
