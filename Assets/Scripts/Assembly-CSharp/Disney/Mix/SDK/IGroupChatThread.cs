using System;

namespace Disney.Mix.SDK
{
	public interface IGroupChatThread : IChatThread
	{
		IChatThreadNickname Nickname { get; }

		event EventHandler<AbstractChatThreadNicknameChangedEventArgs> OnNicknameChanged;

		event EventHandler<AbstractChatThreadGagMessageAddedEventArgs> OnGagMessageAdded;

		IChatThreadNickname SetNickname(string nickname, Action<ISetChatThreadNicknameResult> callback);

		void RemoveNickname(Action<IRemoveChatThreadNicknameResult> callback);

		IGagMessage SendGagMessage(string contentId, IRemoteChatMember targetUser, Action<ISendGagMessageResult> callback);
	}
}
