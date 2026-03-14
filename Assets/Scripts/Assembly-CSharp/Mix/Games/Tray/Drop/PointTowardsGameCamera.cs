using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	public class PointTowardsGameCamera : MonoBehaviour
	{
		public Transform Target { get; set; }

		private void Start()
		{
			if (Target == null)
			{
				Target = DropGame.Instance.GameController.MixGameCamera.transform;
			}
		}

		private void LateUpdate()
		{
			base.transform.LookAt(Target.position);
		}
	}
}
