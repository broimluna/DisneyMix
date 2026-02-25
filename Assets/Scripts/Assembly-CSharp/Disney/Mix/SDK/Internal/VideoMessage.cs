using System;
using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal
{
	internal class VideoMessage : ChatMessage, IInternalChatMessage, IInternalVideoMessage, IChatMessage, IVideoMessage
	{
		public string VideoId { get; private set; }

		public string Caption { get; private set; }

		public MediaStatus MediaStatus { get; private set; }

		public long Duration { get; private set; }

		public IPhotoFlavor Thumbnail { get; private set; }

		public IEnumerable<IVideoFlavor> VideoFlavors { get; private set; }

		public event EventHandler<AbstractChatThreadVideoMessageFailedToSendEventArgs> OnSendFailed = delegate
		{
		};

		public VideoMessage(bool sent, long sequenceNumber, string senderId, IEpochTime epochTime, long localChatMessageId, string videoId, string caption, long duration, IPhotoFlavor thumbnail, IEnumerable<IVideoFlavor> videoFlavors)
			: base(sent, sequenceNumber, senderId, epochTime, localChatMessageId)
		{
			VideoId = videoId;
			Caption = caption;
			Duration = duration;
			Thumbnail = thumbnail;
			VideoFlavors = videoFlavors;
		}

		public void SendComplete(string videoId, long id, long timeSent, long sequenceNumber)
		{
			SendCompleteWithOffsetTime(id, timeSent, sequenceNumber);
			VideoId = videoId;
		}
	}
}
