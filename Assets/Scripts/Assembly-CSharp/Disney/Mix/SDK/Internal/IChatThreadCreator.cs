using System;
using System.Collections.Generic;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public interface IChatThreadCreator
	{
		void CreateOneOnOneChatThread(string memberSwid, Action<ChatThread> successCallback, Action failureCallback);

		void CreateGroupChatThread(IEnumerable<string> memberSwids, Action<ChatThread> successCallback, Action failureCallback);

		IInternalOneOnOneChatThread CreateLocalOneOnOneChatThread(IMixWebCallFactory mixWebCallFactory, ChatThread chatThread);

		IInternalGroupChatThread CreateLocalGroupChatThread(IMixWebCallFactory mixWebCallFactory, ChatThread chatThread);

		IInternalOfficialAccountChatThread CreateLocalOfficialAccountChatThread(IMixWebCallFactory mixWebCallFactory, long chatThreadId, string officialAccountId);
	}
}
