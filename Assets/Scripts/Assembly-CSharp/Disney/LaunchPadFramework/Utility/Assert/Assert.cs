using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Disney.LaunchPadFramework.Utility.Assert
{
	public class Assert
	{
		[Conditional("UNITY_EDITOR")]
		public static void That(bool comparison, string message)
		{
			if (!comparison)
			{
				Debug.LogWarning(message);
				Debug.Break();
			}
		}

		[Conditional("UNITY_EDITOR")]
		public static void Throw(string message)
		{
			Debug.LogWarning(message);
		}
	}
}
