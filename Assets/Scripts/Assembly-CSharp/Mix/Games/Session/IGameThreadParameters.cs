using System.Collections.Generic;

namespace Mix.Games.Session
{
	public interface IGameThreadParameters
	{
		bool IsGroupSession { get; }

		bool IsOneOnOneSession { get; }

		bool IsOneOnOneFriend { get; }

		bool IsFakeThread { get; }

		IEnumerable<string> Members { get; }

		IEnumerable<string> FormerMembers { get; }

		object Thread { get; }

		string ThreadId { get; }

		string GetSenderName(string senderId);
	}
}
