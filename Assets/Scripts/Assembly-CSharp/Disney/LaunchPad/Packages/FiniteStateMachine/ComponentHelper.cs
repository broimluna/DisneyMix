using UnityEngine;

namespace Disney.LaunchPad.Packages.FiniteStateMachine
{
	public static class ComponentHelper
	{
		public static string GetSafeName(this MonoBehaviour obj)
		{
			if (obj != null)
			{
				return obj.name;
			}
			return "(null)";
		}
	}
}
