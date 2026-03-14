using System;
using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK;

namespace Mix.Session.Local.Messages
{
	public class LocalVideoMessage : LocalChatMessage<IVideoMessage>, ILocalMessageReference<IVideoMessage>, IChatMessage, IVideoMessage
	{
		private IVideoMessage cachedVideoMessage;

		private IEnumerable<IVideoFlavor> videoFlavors;

		private IPhotoFlavor thumbnail;

		public string OAMessageId { get; set; }

		public IVideoMessage SdkReference { get; set; }

		public IPhotoFlavor Thumbnail
		{
			get
			{
				if (thumbnail != null)
				{
					return thumbnail;
				}
				if (GetVideoMessage() == null)
				{
					return null;
				}
				return GetVideoMessage().Thumbnail;
			}
			private set
			{
				thumbnail = value;
			}
		}

		public long Duration
		{
			get
			{
				if (GetVideoMessage() == null)
				{
					return 0L;
				}
				return GetVideoMessage().Duration;
			}
		}

		public string Caption
		{
			get
			{
				if (GetVideoMessage() == null)
				{
					return string.Empty;
				}
				return GetVideoMessage().Caption;
			}
		}

		public MediaStatus MediaStatus
		{
			get
			{
				if (GetVideoMessage() == null)
				{
					return MediaStatus.Sending;
				}
				return GetVideoMessage().MediaStatus;
			}
		}

		public IEnumerable<IVideoFlavor> VideoFlavors
		{
			get
			{
				if (videoFlavors != null)
				{
					return videoFlavors;
				}
				if (GetVideoMessage() == null)
				{
					return new List<IVideoFlavor>();
				}
				return GetVideoMessage().VideoFlavors;
			}
			private set
			{
				videoFlavors = value;
			}
		}

		public event EventHandler<AbstractChatThreadVideoMessageFailedToSendEventArgs> OnSendFailed = delegate
		{
		};

		public LocalVideoMessage(string videoPath, VideoFormat format, int bitrate, int duration, int videoWidth, int videoHeight, string thumbnailPath, PhotoEncoding thumbnailEncoding, int thumbnailWidth, int thumbnailHeight, string senderId)
		{
			base.SenderId = senderId;
			base.TimeSent = DateTime.UtcNow;
			base.Sent = false;
			base.Read = false;
			base.Id = LocalUserWrapper.fakeObjectIds.ToString();
			LocalUserWrapper.fakeObjectIds++;
			VideoFlavors = new List<IVideoFlavor>
			{
				new LocalVideoFlavor(videoPath, format, bitrate, duration, videoWidth, videoHeight)
			};
			Thumbnail = new LocalPhotoFlavor(thumbnailPath, thumbnailEncoding, thumbnailWidth, thumbnailHeight, Id);
		}

		public LocalVideoMessage(string videoPath, string format, int bitrate, int duration, int videoWidth, int videoHeight, string thumbnailPath, string thumbnailEncoding, int thumbnailWidth, int thumbnailHeight, string senderId, DateTime timeSent, bool sent, bool read, string id)
		{
			VideoFlavors = new List<IVideoFlavor>
			{
				new LocalVideoFlavor(videoPath, (VideoFormat)(int)Enum.Parse(typeof(VideoFormat), format, true), bitrate, duration, videoWidth, videoHeight)
			};
			Thumbnail = new LocalPhotoFlavor(thumbnailPath, (PhotoEncoding)(int)Enum.Parse(typeof(PhotoEncoding), thumbnailEncoding, true), thumbnailWidth, thumbnailHeight, id);
			base.SenderId = senderId;
			base.TimeSent = timeSent;
			base.Id = id;
			base.Sent = sent;
			base.Read = read;
		}

		public LocalVideoMessage(string aMessageId, string aSenderId, DateTime? timeSent = null)
		{
			OAMessageId = aMessageId;
			base.SenderId = aSenderId;
			base.TimeSent = ((!timeSent.HasValue) ? DateTime.UtcNow : timeSent.Value);
			base.Sent = false;
			base.Id = LocalUserWrapper.fakeObjectIds.ToString();
			LocalUserWrapper.fakeObjectIds++;
		}

		public LocalVideoMessage(string aMessageId, string senderId, DateTime timeSent, bool sent, bool read, string id)
		{
			base.SenderId = senderId;
			base.TimeSent = timeSent;
			OAMessageId = aMessageId;
			base.Id = id;
			base.Sent = sent;
			base.Read = read;
		}

		public override bool Equals(IChatMessage other)
		{
			if (other != null && other is LocalVideoMessage && SenderId == other.SenderId && TimeSent.Equals(other.TimeSent))
			{
				LocalVideoMessage localVideoMessage = other as LocalVideoMessage;
				return localVideoMessage.OAMessageId == OAMessageId;
			}
			return false;
		}

		private IVideoMessage GetVideoMessage()
		{
			if (cachedVideoMessage != null)
			{
				return cachedVideoMessage;
			}
			IOfficialAccountChatThread officialAccountChatThread = MixSession.User.OfficialAccountChatThreads.FirstOrDefault((IOfficialAccountChatThread x) => x.OfficialAccount.AccountId.Equals(FakeFriendManager.OAID));
			if (officialAccountChatThread == null)
			{
				return null;
			}
			IChatMessage chatMessage = officialAccountChatThread.ChatMessages.FirstOrDefault((IChatMessage x) => x.Id == OAMessageId);
			if (chatMessage is IVideoMessage)
			{
				cachedVideoMessage = (IVideoMessage)chatMessage;
				cachedVideoMessage.OnSendFailed += delegate(object sender, AbstractChatThreadVideoMessageFailedToSendEventArgs e)
				{
					this.OnSendFailed(this, e);
				};
				return cachedVideoMessage;
			}
			return null;
		}
	}
}
