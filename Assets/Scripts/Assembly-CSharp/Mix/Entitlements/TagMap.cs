namespace Mix.Entitlements
{
	public struct TagMap
	{
		public string text;

		public string uids;

		public int order;

		public TagMap(string aText, string aUids, int aOrder)
		{
			text = aText;
			uids = aUids;
			order = aOrder;
		}
	}
}
