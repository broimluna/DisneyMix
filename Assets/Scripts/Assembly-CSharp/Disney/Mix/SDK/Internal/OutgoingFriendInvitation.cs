using System;

namespace Disney.Mix.SDK.Internal
{
	public class OutgoingFriendInvitation : AbstractFriendInvitation, IInternalOutgoingFriendInvitation, IOutgoingFriendInvitation
	{
		private readonly IInternalLocalUser inviter;

		private readonly IInternalUnidentifiedUser invitee;

		public ILocalUser Inviter
		{
			get
			{
				return inviter;
			}
		}

		public IUnidentifiedUser Invitee
		{
			get
			{
				return invitee;
			}
		}

		public IInternalLocalUser InternalInviter
		{
			get
			{
				return inviter;
			}
		}

		public IInternalUnidentifiedUser InternalInvitee
		{
			get
			{
				return invitee;
			}
		}

		 bool IInternalOutgoingFriendInvitation.RequestTrust
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

		 long IInternalOutgoingFriendInvitation.InvitationId
		{
			get
			{
				return base.InvitationId;
			}
		}

		 bool IOutgoingFriendInvitation.RequestTrust
		{
			get
			{
				return base.RequestTrust;
			}
		}

		 bool IOutgoingFriendInvitation.Sent
		{
			get
			{
				return base.Sent;
			}
		}

		 string IOutgoingFriendInvitation.Id
		{
			get
			{
				return base.Id;
			}
		}

		public OutgoingFriendInvitation(IInternalLocalUser inviter, IInternalUnidentifiedUser invitee, bool requestTrust)
			: base(requestTrust)
		{
			this.inviter = inviter;
			this.invitee = invitee;
		}

		 void IInternalOutgoingFriendInvitation.SendComplete(long id)
		{
			SendComplete(id);
		}

		 void IInternalOutgoingFriendInvitation.Accepted(bool trustAccepted, IInternalFriend friend)
		{
			Accepted(trustAccepted, friend);
		}

		 void IInternalOutgoingFriendInvitation.Rejected()
		{
			Rejected();
		}

		 event EventHandler<AbstractFriendInvitationAcceptedEventArgs> IOutgoingFriendInvitation.OnAccepted
		{
			add { base.OnAccepted += value; }
			remove { base.OnAccepted -= value; }
		}

		 event EventHandler<AbstractFriendInvitationRejectedEventArgs> IOutgoingFriendInvitation.OnRejected
		{
			add { base.OnRejected += value; }
			remove { base.OnRejected -= value; }
		}
	}
}
