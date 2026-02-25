using System;
using Disney.Mix.SDK;
using Mix.Games;

namespace Mix.Ui.Events
{
	public class ChatThreadEnterEventArgs : EventArgs
	{
		public IChatThread ChatThread { get; protected set; }

		public IGameTray GameTray { get; protected set; }

		public ChatThreadEnterEventArgs(IChatThread chatThread, IGameTray gameTray)
		{
			ChatThread = chatThread;
			GameTray = gameTray;
		}
	}
}
