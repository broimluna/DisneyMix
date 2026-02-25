using System;
using Disney.Mix.SDK;

namespace Mix.Ui
{
	public class IWritableVerifyAdultFormUnitedStates : IVerifyAdultForm, IVerifyAdultFormUnitedStates
	{
		public string AddressLine1 { get; set; }

		public string PostalCode { get; set; }

		public DateTime DateOfBirth { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Ssn { get; set; }
	}
}
