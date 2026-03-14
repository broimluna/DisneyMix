using Mix.Games.Data;

namespace Mix.Games.Session
{
	public interface IGameModeration
	{
		void ModerateTextMessage(object thread, string textToModerate, IGameModerationResult game, IEntitlementGameData entitlement, object userData);
	}
}
