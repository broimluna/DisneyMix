using System;
using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal
{
	internal class PhotoMessage : ChatMessage, IInternalChatMessage, IInternalPhotoMessage, IChatMessage, IPhotoMessage
	{
		public string PhotoId { get; private set; }

		public string Caption { get; private set; }

		public MediaStatus MediaStatus { get; private set; }

		public IEnumerable<IPhotoFlavor> PhotoFlavors { get; private set; }

		public event EventHandler<AbstractChatThreadPhotoMessageFailedToSendEventArgs> OnSendFailed = delegate
		{
		};

		public PhotoMessage(bool sent, long sequenceNumber, string senderId, IEpochTime epochTime, long localChatMessageId, string photoId, string caption, IEnumerable<IPhotoFlavor> photoFlavors)
			: base(sent, sequenceNumber, senderId, epochTime, localChatMessageId)
		{
			PhotoId = photoId;
			Caption = caption;
			PhotoFlavors = photoFlavors;
		}

		public void SendComplete(string photoId, long id, long timeSent, long sequenceNumber)
		{
			SendCompleteWithOffsetTime(id, timeSent, sequenceNumber);
			PhotoId = photoId;
		}
	}
}
