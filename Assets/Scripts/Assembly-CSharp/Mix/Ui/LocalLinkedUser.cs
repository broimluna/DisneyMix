using System;
using Disney.Mix.SDK;
using Disney.Mix.SDK.Internal;

namespace Mix.Ui
{
	public class LocalLinkedUser : IInternalLinkedUser, ILinkedUser
	{
		public string Username { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public IDisplayName DisplayName { get; set; }

		public IAvatar Avatar { get; set; }

		public string Email { get; set; }

		public string ParentEmail { get; set; }

		public AgeBandType AgeBand { get; set; }

		public DateTime? DateOfBirth { get; set; }

		public string Swid { get; set; }
	}
}
