using System;
using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK;

namespace Mix.Session.Local.Messages
{
	public class LocalPhotoMessage : LocalChatMessage<IPhotoMessage>, ILocalMessageReference<IPhotoMessage>, IChatMessage, IPhotoMessage
	{
		private IPhotoMessage cachedPhotoMessage;

		private IEnumerable<IPhotoFlavor> photoFlavors;

		public string OAMessageId { get; set; }

		public IPhotoMessage SdkReference { get; set; }

		public string Caption
		{
			get
			{
				if (GetPhotoMessage() == null)
				{
					return string.Empty;
				}
				return GetPhotoMessage().Caption;
			}
		}

		public MediaStatus MediaStatus
		{
			get
			{
				if (GetPhotoMessage() == null)
				{
					return MediaStatus.Sending;
				}
				return GetPhotoMessage().MediaStatus;
			}
		}

		public IEnumerable<IPhotoFlavor> PhotoFlavors
		{
			get
			{
				if (photoFlavors != null)
				{
					return photoFlavors;
				}
				if (GetPhotoMessage() == null)
				{
					return new List<IPhotoFlavor>();
				}
				return GetPhotoMessage().PhotoFlavors;
			}
			private set
			{
				photoFlavors = value;
			}
		}

		public event EventHandler<AbstractChatThreadPhotoMessageFailedToSendEventArgs> OnSendFailed = delegate
		{
		};

		public LocalPhotoMessage(string photoPath, PhotoEncoding encoding, int width, int height, string senderId)
		{
			base.SenderId = senderId;
			base.TimeSent = DateTime.UtcNow;
			base.Sent = false;
			base.Read = false;
			base.Id = LocalUserWrapper.fakeObjectIds.ToString();
			LocalUserWrapper.fakeObjectIds++;
			PhotoFlavors = new List<IPhotoFlavor>
			{
				new LocalPhotoFlavor(photoPath, encoding, width, height, Id)
			};
		}

		public LocalPhotoMessage(string photoPath, string encoding, int width, int height, string senderId, DateTime timeSent, bool sent, bool read, string id)
		{
			PhotoFlavors = new List<IPhotoFlavor>
			{
				new LocalPhotoFlavor(photoPath, (PhotoEncoding)(int)Enum.Parse(typeof(PhotoEncoding), encoding, true), width, height, id)
			};
			base.SenderId = senderId;
			base.TimeSent = timeSent;
			base.Id = id;
			base.Sent = sent;
			base.Read = read;
		}

		public LocalPhotoMessage(string aMessageId, string aSenderId, DateTime? timeSent = null)
		{
			OAMessageId = aMessageId;
			base.SenderId = aSenderId;
			base.TimeSent = ((!timeSent.HasValue) ? DateTime.UtcNow : timeSent.Value);
			base.Sent = false;
			base.Id = LocalUserWrapper.fakeObjectIds.ToString();
			LocalUserWrapper.fakeObjectIds++;
		}

		public LocalPhotoMessage(string aMessageId, string senderId, DateTime timeSent, bool sent, bool read, string id)
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
			if (other != null && other is LocalPhotoMessage && SenderId == other.SenderId && TimeSent.Equals(other.TimeSent))
			{
				LocalPhotoMessage localPhotoMessage = other as LocalPhotoMessage;
				return localPhotoMessage.OAMessageId == OAMessageId;
			}
			return false;
		}

		private IPhotoMessage GetPhotoMessage()
		{
			if (cachedPhotoMessage != null)
			{
				return cachedPhotoMessage;
			}
			IOfficialAccountChatThread officialAccountChatThread = MixSession.User.OfficialAccountChatThreads.FirstOrDefault((IOfficialAccountChatThread x) => x.OfficialAccount.AccountId.Equals(FakeFriendManager.OAID));
			if (officialAccountChatThread == null)
			{
				return null;
			}
			IChatMessage chatMessage = officialAccountChatThread.ChatMessages.FirstOrDefault((IChatMessage x) => x.Id == OAMessageId);
			if (chatMessage is IPhotoMessage)
			{
				cachedPhotoMessage = (IPhotoMessage)chatMessage;
				cachedPhotoMessage.OnSendFailed += delegate(object sender, AbstractChatThreadPhotoMessageFailedToSendEventArgs e)
				{
					this.OnSendFailed(this, e);
				};
				return cachedPhotoMessage;
			}
			return null;
		}
	}
}
