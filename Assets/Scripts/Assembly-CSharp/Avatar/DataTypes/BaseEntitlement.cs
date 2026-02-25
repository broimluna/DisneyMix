namespace Avatar.DataTypes
{
	public abstract class BaseEntitlement
	{
		public string Id { get; protected set; }

		public string Name { get; protected set; }

		public string Sd { get; protected set; }

		public string Hd { get; protected set; }

		public string Thumb { get; protected set; }
	}
}
