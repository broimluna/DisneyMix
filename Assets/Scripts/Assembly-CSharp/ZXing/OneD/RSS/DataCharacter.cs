namespace ZXing.OneD.RSS
{
	public class DataCharacter
	{
		public int Value { get; private set; }

		public int ChecksumPortion { get; private set; }

		public DataCharacter(int value, int checksumPortion)
		{
			Value = value;
			ChecksumPortion = checksumPortion;
		}

		public override string ToString()
		{
			return Value + "(" + ChecksumPortion + ')';
		}

		public override bool Equals(object o)
		{
			if (!(o is DataCharacter))
			{
				return false;
			}
			DataCharacter dataCharacter = (DataCharacter)o;
			return Value == dataCharacter.Value && ChecksumPortion == dataCharacter.ChecksumPortion;
		}

		public override int GetHashCode()
		{
			return Value ^ ChecksumPortion;
		}
	}
}
