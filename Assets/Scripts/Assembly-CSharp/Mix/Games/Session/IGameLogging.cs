using UnityEngine;

namespace Mix.Games.Session
{
	public interface IGameLogging
	{
		void Log(object message);

		void Log(object message, Object context);

		void LogError(object message);

		void LogError(object message, Object context);

		void LogWarning(object message);

		void LogWarning(object message, Object context);
	}
}
