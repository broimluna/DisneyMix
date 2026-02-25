using System.Collections.Generic;
using ZXing.Common;
using ZXing.QrCode.Internal;

namespace ZXing.Multi.QrCode.Internal
{
	public sealed class MultiDetector : Detector
	{
		private static readonly DetectorResult[] EMPTY_DETECTOR_RESULTS = new DetectorResult[0];

		public MultiDetector(BitMatrix image)
			: base(image)
		{
		}

		public DetectorResult[] detectMulti(IDictionary<DecodeHintType, object> hints)
		{
			BitMatrix bitMatrix = Image;
			ResultPointCallback resultPointCallback = ((hints != null && hints.ContainsKey(DecodeHintType.NEED_RESULT_POINT_CALLBACK)) ? ((ResultPointCallback)hints[DecodeHintType.NEED_RESULT_POINT_CALLBACK]) : null);
			MultiFinderPatternFinder multiFinderPatternFinder = new MultiFinderPatternFinder(bitMatrix, resultPointCallback);
			FinderPatternInfo[] array = multiFinderPatternFinder.findMulti(hints);
			if (array.Length == 0)
			{
				return EMPTY_DETECTOR_RESULTS;
			}
			List<DetectorResult> list = new List<DetectorResult>();
			FinderPatternInfo[] array2 = array;
			foreach (FinderPatternInfo info in array2)
			{
				DetectorResult detectorResult = processFinderPatternInfo(info);
				if (detectorResult != null)
				{
					list.Add(detectorResult);
				}
			}
			if (list.Count == 0)
			{
				return EMPTY_DETECTOR_RESULTS;
			}
			return list.ToArray();
		}
	}
}
