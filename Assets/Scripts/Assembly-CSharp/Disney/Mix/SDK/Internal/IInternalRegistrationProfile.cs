using System.Collections.Generic;
using Disney.Mix.SDK.Internal.GuestControllerDomain;

namespace Disney.Mix.SDK.Internal
{
	public interface IInternalRegistrationProfile : IRegistrationProfile
	{
		new bool IsAdultVerified { get; set; }

		void Update(Profile profile, Disney.Mix.SDK.Internal.GuestControllerDomain.DisplayName displayName, IEnumerable<Disney.Mix.SDK.Internal.GuestControllerDomain.MarketingItem> marketing);

		void UpdateDisplayName(string displayName);
	}
}
