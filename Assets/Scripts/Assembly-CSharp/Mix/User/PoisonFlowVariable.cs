using System;
using Disney.Mix.SDK;

namespace Mix.User
{
	public class PoisonFlowVariable : Singleton<PoisonFlowVariable>
	{
		private static DateTime childBirthDate = DateTime.MinValue;

		private static IAgeBand ageBand;

		public static DateTime GetBirthdate()
		{
			return childBirthDate;
		}

		public static IAgeBand GetAgeBandInfo()
		{
			return ageBand;
		}

		public static void SetBirthdate(int aYear, int aMonth, int aDay, IAgeBand aAgeBand)
		{
			if (aYear == 0 && aMonth == 0 && aDay == 0)
			{
				childBirthDate = DateTime.MinValue;
			}
			else
			{
				childBirthDate = new DateTime(aYear, aMonth, aDay);
			}
			ageBand = aAgeBand;
		}
	}
}
