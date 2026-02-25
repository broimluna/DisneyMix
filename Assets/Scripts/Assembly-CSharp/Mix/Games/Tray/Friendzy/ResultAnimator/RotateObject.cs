using UnityEngine;

namespace Mix.Games.Tray.Friendzy.ResultAnimator
{
	public class RotateObject : MonoBehaviour
	{
		public float MOVEMENT_RATE;

		public float MOVEMENT_ANGLE;

		public bool HasFullRotation;

		private void Update()
		{
			if (HasFullRotation)
			{
				base.transform.RotateAround(base.transform.position, base.transform.forward, Time.deltaTime * MOVEMENT_RATE);
			}
			else
			{
				base.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, Mathf.Sin(Time.timeSinceLevelLoad * MOVEMENT_RATE) * MOVEMENT_ANGLE));
			}
		}
	}
}
