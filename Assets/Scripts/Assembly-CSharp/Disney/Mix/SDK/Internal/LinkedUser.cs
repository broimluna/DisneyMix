using System;

namespace Disney.Mix.SDK.Internal
{
	public class LinkedUser : IInternalLinkedUser, ILinkedUser
	{
		public string Username { get; private set; }

		public string FirstName { get; private set; }

		public string LastName { get; private set; }

		public IDisplayName DisplayName { get; private set; }

		public IAvatar Avatar { get; private set; }

		public string Email { get; private set; }

		public string ParentEmail { get; private set; }

		public AgeBandType AgeBand { get; private set; }

		public DateTime? DateOfBirth { get; private set; }

		public string Swid { get; private set; }

		public LinkedUser(string username, string firstName, string lastName, IDisplayName displayName, IAvatar avatar, string email, string parentEmail, AgeBandType ageBand, DateTime? dateOfBirth, string swid)
		{
			Username = username;
			FirstName = firstName;
			LastName = lastName;
			DisplayName = displayName;
			Avatar = avatar;
			Email = email;
			ParentEmail = parentEmail;
			AgeBand = ageBand;
			DateOfBirth = dateOfBirth;
			Swid = swid;
		}
	}
}
