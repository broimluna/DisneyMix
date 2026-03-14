namespace Mix.AvatarInternal
{
	public class AvatarTexturingResponseParams
	{
		public string Id;

		public string DnaHash;

		public bool Refresh;

		public AvatarTexturingResponseParams(string aId, string aDnaHash, bool aRefresh)
		{
			Id = aId;
			DnaHash = aDnaHash;
			Refresh = aRefresh;
		}
	}
}
