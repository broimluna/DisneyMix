namespace Fabric.ModularSynth
{
	public abstract class PinBase
	{
		public int ID { get; set; }

		public Module ParentModule { get; set; }

		public ePinType Type { get; private set; }

		public ePinDirection Direction { get; private set; }

		public string Name { get; set; }

		public PinBase(ePinType type, ePinDirection direction)
		{
			ID = 0;
			Type = type;
			Direction = direction;
			ParentModule = null;
		}

		public virtual bool IsConnected()
		{
			return false;
		}
	}
}
