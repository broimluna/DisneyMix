namespace Disney.Mix.SDK.Internal
{
	public static class VideoFormatTypeConverter
	{
		public const string HlsServerType = "hls";

		public const string Mp4ServerType = "mp4";

		public const string WebmServerType = "webm";

		public const string UnknownServerType = "unknown";

		public static string ConvertDatabaseTypeToServerType(VideoFormat videoFormat)
		{
			object result;
			switch (videoFormat)
			{
			case VideoFormat.Hls:
				result = "hls";
				break;
			case VideoFormat.Mp4:
				result = "mp4";
				break;
			case VideoFormat.Webm:
				result = "webm";
				break;
			default:
				result = "unknown";
				break;
			}
			return (string)result;
		}

		public static VideoFormat ConvertServerTypeToDatabaseType(string serverType)
		{
			switch (serverType)
			{
			case "hls":
				return VideoFormat.Hls;
			case "mp4":
				return VideoFormat.Mp4;
			case "webm":
				return VideoFormat.Webm;
			default:
				return VideoFormat.Unknown;
			}
		}

		public static bool ValidateServerType(string serverType)
		{
			return serverType != null;
		}
	}
}
