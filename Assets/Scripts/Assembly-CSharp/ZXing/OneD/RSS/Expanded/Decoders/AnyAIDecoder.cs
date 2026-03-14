using System.Text;
using ZXing.Common;

namespace ZXing.OneD.RSS.Expanded.Decoders
{
	internal sealed class AnyAIDecoder : AbstractExpandedDecoder
	{
		private static int HEADER_SIZE = 5;

		internal AnyAIDecoder(BitArray information)
			: base(information)
		{
		}

		public override string parseInformation()
		{
			StringBuilder buff = new StringBuilder();
			return getGeneralDecoder().decodeAllCodes(buff, HEADER_SIZE);
		}
	}
}
