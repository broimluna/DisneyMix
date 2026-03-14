using System.Collections.Generic;

namespace Mix.Assets.Worker
{
	public interface IGetHeader
	{
		void OnGetHeader(Dictionary<string, string> aHeader, object aUserData);
	}
}
