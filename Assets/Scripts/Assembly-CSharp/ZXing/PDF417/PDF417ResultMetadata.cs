namespace ZXing.PDF417
{
	public sealed class PDF417ResultMetadata
	{
		public int SegmentIndex { get; set; }

		public string FileId { get; set; }

		public int[] OptionalData { get; set; }

		public bool IsLastSegment { get; set; }
	}
}
