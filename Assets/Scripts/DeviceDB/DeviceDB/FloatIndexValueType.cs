using System;

namespace DeviceDB
{
	internal class FloatIndexValueType : AbstractFixedIndexValueType<float>
	{
		private readonly byte[] buffer;

		public FloatIndexValueType()
		{
			buffer = new byte[4];
		}

		protected override byte[] Serialize(float entryValue)
		{
			var bytes = BitConverter.GetBytes(entryValue);
			buffer[0] = bytes[0];
			buffer[1] = bytes[1];
			buffer[2] = bytes[2];
			buffer[3] = bytes[3];
			return buffer;
		}

		protected override float Deserialize(byte[] bytes)
		{
			return BitConverter.ToSingle(bytes, 0);
		}
	}
}
