using System;
using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal
{
	public interface IInternalGroupChatThread : IInternalChatThread, IChatThread, IGroupChatThread
	{
		void AddMembers(IEnumerable<IInternalFriend> friends, Action<IAddChatThreadMemberResult> callback);

		void RemoveRemoteMember(IInternalRemoteChatMember member, Action<IRemoveChatThreadMemberResult> callback);

		void RemoveLocalUser(IInternalLocalUser member, Action<IRemoveChatThreadMemberResult> callback);
	}
}
