using System;
using System.Collections.Generic;

namespace Disney.Mix.SDK
{
	public interface IPhotoMessage : IChatMessage
	{
		IEnumerable<IPhotoFlavor> PhotoFlavors { get; }

		string Caption { get; }

		MediaStatus MediaStatus { get; }

		event EventHandler<AbstractChatThreadPhotoMessageFailedToSendEventArgs> OnSendFailed;
	}
}
