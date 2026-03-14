using System;
using System.Collections.Generic;

namespace Disney.Mix.SDK
{
	public interface IVideoMessage : IChatMessage
	{
		long Duration { get; }

		IEnumerable<IVideoFlavor> VideoFlavors { get; }

		IPhotoFlavor Thumbnail { get; }

		string Caption { get; }

		MediaStatus MediaStatus { get; }

		event EventHandler<AbstractChatThreadVideoMessageFailedToSendEventArgs> OnSendFailed;
	}
}
