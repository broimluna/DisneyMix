namespace ZXing.OneD
{
	public abstract class UPCEANWriter : OneDimensionalCodeWriter
	{
		public override int DefaultMargin
		{
			get
			{
				return UPCEANReader.START_END_PATTERN.Length;
			}
		}
	}
}
