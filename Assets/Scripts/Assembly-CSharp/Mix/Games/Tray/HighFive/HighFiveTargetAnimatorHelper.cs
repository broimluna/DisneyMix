using UnityEngine;

namespace Mix.Games.Tray.HighFive
{
	public class HighFiveTargetAnimatorHelper : MonoBehaviour
	{
		public HighFiveTargetTentacle Target;

		public void StartSweetSpot()
		{
			Target.StartSweetSpot();
		}

		public void EndSweetSpot()
		{
			Target.EndSweetSpot();
		}

		public void EndGoodWindow()
		{
			Target.EndGoodWindow();
		}

		public void EndLateWindow()
		{
			Target.EndLateWindow();
		}

		public void EndAnimation()
		{
			Target.EndAnimation();
		}
	}
}
