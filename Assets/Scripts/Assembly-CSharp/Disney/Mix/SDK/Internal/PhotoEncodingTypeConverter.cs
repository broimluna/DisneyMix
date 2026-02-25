namespace Disney.Mix.SDK.Internal
{
	public static class PhotoEncodingTypeConverter
	{
		public const string GifServerType = "gif";

		public const string JpegServerType = "jpeg";

		public const string PngServerType = "png";

		public const string SvgServerType = "svg";

		public const string UnknownServerType = "unknown";

		public static string ConvertDatabaseTypeToServerType(PhotoEncoding photoEncoding)
		{
			object result;
			switch (photoEncoding)
			{
			case PhotoEncoding.Gif:
				result = "gif";
				break;
			case PhotoEncoding.Jpeg:
				result = "jpeg";
				break;
			case PhotoEncoding.Png:
				result = "png";
				break;
			case PhotoEncoding.Svg:
				result = "svg";
				break;
			default:
				result = "unknown";
				break;
			}
			return (string)result;
		}

		public static PhotoEncoding ConvertServerTypeToDatabaseType(string serverType)
		{
			switch (serverType)
			{
			case "gif":
				return PhotoEncoding.Gif;
			case "jpeg":
				return PhotoEncoding.Jpeg;
			case "png":
				return PhotoEncoding.Png;
			case "svg":
				return PhotoEncoding.Svg;
			default:
				return PhotoEncoding.Unknown;
			}
		}

		public static bool ValidateServerType(string serverType)
		{
			return serverType != null;
		}
	}
}
