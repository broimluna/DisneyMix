using System.Collections.Generic;

namespace Disney.MobileNetwork
{
	public class HockeyAppManagerIOS : HockeyAppManager
	{
		public override void ForceCrash()
		{
		}

		protected override List<string> GetLogHeaders()
		{
			return new List<string>();
		}
	}
}
