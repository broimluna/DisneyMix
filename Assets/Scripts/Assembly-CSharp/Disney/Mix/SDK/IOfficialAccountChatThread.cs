using System;

namespace Disney.Mix.SDK
{
	public interface IOfficialAccountChatThread : IChatThread
	{
		IOfficialAccount OfficialAccount { get; }

		event EventHandler<AbstractChatHistoryClearedEventArgs> OnChatHistoryCleared;

		void ClearChatHistory(Action<IClearChatHistoryResult> callback);

		void OfficialAccountTextMessageSend(string text, Action<IOfficialAccountTextMessageSenderResult> callback);

		void OfficialAccountPhotoMessageSend(string caption, Action<IOfficialAccountPhotoMessageSenderResult> callback);

		void OfficialAccountVideoMessageSend(string caption, Action<IOfficialAccountVideoMessageSenderResult> callback);
	}
}
