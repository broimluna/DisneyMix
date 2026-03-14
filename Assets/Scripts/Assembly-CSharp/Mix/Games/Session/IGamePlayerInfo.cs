namespace Mix.Games.Session
{
	public interface IGamePlayerInfo
	{
		string Id { get; }

		string DisplayName { get; }

		string GetFriendDisplayName(string id);

		string GetFriendDisplayName(IGameThreadParameters threadParams);
	}
}
