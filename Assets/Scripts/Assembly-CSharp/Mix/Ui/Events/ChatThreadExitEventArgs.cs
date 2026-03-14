using System;
using Disney.Mix.SDK;
using Mix.Games;

namespace Mix.Ui.Events
{
	public class ChatThreadExitEventArgs : EventArgs
	{
		public IChatThread ChatThread { get; protected set; }

		public IGameTray GameTray { get; protected set; }

		public ChatThreadExitEventArgs(IChatThread chatThread, IGameTray gameTray)
		{
			ChatThread = chatThread;
			GameTray = gameTray;
		}
	}
}
