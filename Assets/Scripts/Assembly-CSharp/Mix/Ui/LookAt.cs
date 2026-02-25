using UnityEngine;

namespace Mix.Ui
{
	public class LookAt : MonoBehaviour
	{
		public string TargetName;

		public Vector3 Rotation;

		public bool ReverseDirection;

		private Vector3 lookAwayRotation = new Vector3(0f, 180f, 0f);

		private Transform target;

		private void Start()
		{
			GameObject gameObject = GameObject.Find(TargetName);
			if ((bool)gameObject)
			{
				target = gameObject.transform;
			}
		}

		private void LateUpdate()
		{
			if ((bool)target)
			{
				base.transform.LookAt(target);
				if (ReverseDirection)
				{
					base.transform.Rotate(lookAwayRotation, Space.Self);
				}
				base.transform.Rotate(Rotation);
			}
		}
	}
}
