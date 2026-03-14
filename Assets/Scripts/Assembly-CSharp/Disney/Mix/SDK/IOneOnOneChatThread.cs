using System;

namespace Disney.Mix.SDK
{
	public interface IOneOnOneChatThread : IChatThread
	{
		IChatThreadNickname Nickname { get; }

		event EventHandler<AbstractChatThreadNicknameChangedEventArgs> OnNicknameChanged;

		event EventHandler<AbstractChatThreadGagMessageAddedEventArgs> OnGagMessageAdded;

		event EventHandler<AbstractChatHistoryClearedEventArgs> OnChatHistoryCleared;

		IChatThreadNickname SetNickname(string nickname, Action<ISetChatThreadNicknameResult> callback);

		void RemoveNickname(Action<IRemoveChatThreadNicknameResult> callback);

		IGagMessage SendGagMessage(string contentId, IRemoteChatMember targetUser, Action<ISendGagMessageResult> callback);

		void ClearChatHistory(Action<IClearChatHistoryResult> callback);
	}
}
