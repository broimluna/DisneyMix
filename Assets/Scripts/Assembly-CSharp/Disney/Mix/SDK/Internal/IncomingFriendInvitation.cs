using System;

namespace Disney.Mix.SDK.Internal
{
	public class IncomingFriendInvitation : AbstractFriendInvitation, IInternalIncomingFriendInvitation, IIncomingFriendInvitation
	{
		private readonly IInternalUnidentifiedUser inviter;

		private readonly IInternalLocalUser invitee;

		public IUnidentifiedUser Inviter
		{
			get
			{
				return inviter;
			}
		}

		public ILocalUser Invitee
		{
			get
			{
				return invitee;
			}
		}

		public IInternalUnidentifiedUser InternalInviter
		{
			get
			{
				return inviter;
			}
		}

		public IInternalLocalUser InternalInvitee
		{
			get
			{
				return invitee;
			}
		}

		 bool IInternalIncomingFriendInvitation.RequestTrust
		{
			get
			{
				return base.RequestTrust;
			}
			set
			{
				base.RequestTrust = value;
			}
		}

		 long IInternalIncomingFriendInvitation.InvitationId
		{
			get
			{
				return base.InvitationId;
			}
		}

		 bool IIncomingFriendInvitation.RequestTrust
		{
			get
			{
				return base.RequestTrust;
			}
		}

		 bool IIncomingFriendInvitation.Sent
		{
			get
			{
				return base.Sent;
			}
		}

		 string IIncomingFriendInvitation.Id
		{
			get
			{
				return base.Id;
			}
		}

		public IncomingFriendInvitation(IInternalUnidentifiedUser inviter, IInternalLocalUser invitee, bool requestTrust)
			: base(requestTrust)
		{
			this.inviter = inviter;
			this.invitee = invitee;
		}

		 void IInternalIncomingFriendInvitation.SendComplete(long id)
		{
			SendComplete(id);
		}

		 void IInternalIncomingFriendInvitation.Accepted(bool trustAccepted, IInternalFriend friend)
		{
			Accepted(trustAccepted, friend);
		}

		 void IInternalIncomingFriendInvitation.Rejected()
		{
			Rejected();
		}

		 event EventHandler<AbstractFriendInvitationAcceptedEventArgs> IIncomingFriendInvitation.OnAccepted
		{
			add { base.OnAccepted += value; }
			remove { base.OnAccepted -= value; }
		}

		 event EventHandler<AbstractFriendInvitationRejectedEventArgs> IIncomingFriendInvitation.OnRejected
		{
			add { base.OnRejected += value; }
			remove { base.OnRejected -= value; }
		}
	}
}
