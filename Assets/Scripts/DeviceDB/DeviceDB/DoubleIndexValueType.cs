using System;

namespace DeviceDB
{
	internal class DoubleIndexValueType : AbstractFixedIndexValueType<double>
	{
		private readonly byte[] buffer;

		public DoubleIndexValueType()
		{
			buffer = new byte[8];
		}

		protected override byte[] Serialize(double entryValue)
		{
			var bytes = BitConverter.GetBytes(entryValue);
			Buffer.BlockCopy(bytes, 0, buffer, 0, 8);
			return buffer;
		}

		protected override double Deserialize(byte[] bytes)
		{
			return BitConverter.ToDouble(bytes, 0);
		}
	}
}
