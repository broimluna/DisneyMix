using System.Collections.Generic;

namespace ZXing.PDF417.Internal
{
	public sealed class BarcodeValue
	{
		private readonly IDictionary<int, int> values = new Dictionary<int, int>();

		public void setValue(int value)
		{
			int value2;
			values.TryGetValue(value, out value2);
			value2++;
			values[value] = value2;
		}

		public int[] getValue()
		{
			int num = -1;
			List<int> list = new List<int>();
			foreach (KeyValuePair<int, int> value in values)
			{
				if (value.Value > num)
				{
					num = value.Value;
					list.Clear();
					list.Add(value.Key);
				}
				else if (value.Value == num)
				{
					list.Add(value.Key);
				}
			}
			return list.ToArray();
		}

		public int getConfidence(int barcodeValue)
		{
			return values.ContainsKey(barcodeValue) ? values[barcodeValue] : 0;
		}
	}
}
