using UnityEngine;

namespace Mix.Games.Tray.Common
{
	public class FollowCamera : MonoBehaviour
	{
		public Vector3 CameraOffset;

		public Vector3 LookAtOffset;

		public float PanTime;

		public float FollowTime;

		public Transform FollowTarget;

		private Vector3 panVelocity = Vector3.zero;

		private Vector3 followVelocity = Vector3.zero;

		private Vector3 panTarget;

		private void Start()
		{
			if ((bool)FollowTarget)
			{
				SetFollowTarget(FollowTarget, true);
			}
		}

		public void SetFollowTarget(Transform target, bool snap = false)
		{
			FollowTarget = target;
			if (snap)
			{
				panTarget = FollowTarget.transform.position + LookAtOffset;
			}
		}

		private void LateUpdate()
		{
			if ((bool)FollowTarget)
			{
				Vector3 target = FollowTarget.transform.position + CameraOffset;
				base.transform.position = Vector3.SmoothDamp(base.transform.position, target, ref followVelocity, FollowTime);
				panTarget = Vector3.SmoothDamp(panTarget, FollowTarget.transform.position + LookAtOffset, ref panVelocity, PanTime);
				base.transform.LookAt(panTarget);
			}
		}

		public void SnapToTarget()
		{
			if ((bool)FollowTarget)
			{
				base.transform.position = FollowTarget.transform.position + CameraOffset;
				base.transform.LookAt(FollowTarget.transform.position + LookAtOffset);
				followVelocity = Vector3.zero;
				panVelocity = Vector3.zero;
			}
		}
	}
}
