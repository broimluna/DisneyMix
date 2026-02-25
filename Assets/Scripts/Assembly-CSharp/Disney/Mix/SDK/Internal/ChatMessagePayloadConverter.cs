using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public static class ChatMessagePayloadConverter
	{
		public static PhotoMessageDocumentPayload ConvertPhotoChatMessageToDocumentPayload(PhotoChatMessage photoMessage)
		{
			List<PhotoFlavorDocumentPayload> photoFlavors = photoMessage.PhotoFlavors.Select((Disney.Mix.SDK.Internal.MixDomain.PhotoFlavor f) => new PhotoFlavorDocumentPayload
			{
				MediaId = photoMessage.PhotoId,
				PhotoFlavorId = f.PhotoFlavorId,
				Url = f.Url,
				Width = f.Width.Value,
				Height = f.Height.Value,
				Encoding = f.Encoding
			}).ToList();
			PhotoMessageDocumentPayload photoMessageDocumentPayload = new PhotoMessageDocumentPayload();
			photoMessageDocumentPayload.PhotoId = photoMessage.PhotoId;
			photoMessageDocumentPayload.Caption = photoMessage.Caption;
			photoMessageDocumentPayload.PhotoFlavors = photoFlavors;
			return photoMessageDocumentPayload;
		}

		public static VideoMessageDocumentPayload ConvertVideoChatMessageToDocumentPayload(VideoChatMessage videoMessage)
		{
			List<VideoFlavorDocumentPayload> videoFlavors = videoMessage.VideoFlavors.Select((Disney.Mix.SDK.Internal.MixDomain.VideoFlavor f) => new VideoFlavorDocumentPayload
			{
				VideoId = videoMessage.VideoId,
				VideoFlavorId = f.VideoFlavorId,
				Format = f.Format,
				BitRate = f.Bitrate.Value,
				Width = f.Width.Value,
				Height = f.Height.Value
			}).ToList();
			VideoMessageDocumentPayload videoMessageDocumentPayload = new VideoMessageDocumentPayload();
			videoMessageDocumentPayload.VideoId = videoMessage.VideoId;
			videoMessageDocumentPayload.Caption = videoMessage.Caption;
			videoMessageDocumentPayload.Duration = videoMessage.Duration.Value;
			videoMessageDocumentPayload.Thumbnail = new PhotoFlavorDocumentPayload
			{
				MediaId = videoMessage.VideoId,
				PhotoFlavorId = videoMessage.Thumbnail.PhotoFlavorId,
				Url = videoMessage.Thumbnail.Url,
				Width = videoMessage.Thumbnail.Width.Value,
				Height = videoMessage.Thumbnail.Height.Value,
				Encoding = videoMessage.Thumbnail.Encoding
			};
			videoMessageDocumentPayload.VideoFlavors = videoFlavors;
			return videoMessageDocumentPayload;
		}
	}
}
