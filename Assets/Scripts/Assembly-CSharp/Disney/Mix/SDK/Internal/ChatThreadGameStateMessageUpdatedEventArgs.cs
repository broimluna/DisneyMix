namespace Disney.Mix.SDK.Internal
{
	internal class ChatThreadGameStateMessageUpdatedEventArgs : AbstractChatThreadGameStateMessageUpdatedEventArgs
	{
		public override string Result { get; protected set; }

		public ChatThreadGameStateMessageUpdatedEventArgs(string result)
		{
			Result = result;
		}
	}
}
