namespace Mix.Games.Data
{
	public class MixGameContentData
	{
		public string uid { get; set; }

		public bool hidden { get; set; }

		public bool preview { get; set; }

		public bool isNew { get; set; }

		public string tags { get; set; }

		public string pushNote { get; set; }

		public int order { get; set; }

		public virtual string worksheetName
		{
			get
			{
				return null;
			}
		}
	}
}
