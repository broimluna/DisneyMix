using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	public class MoveAndSnapEnvironment : MonoBehaviour
	{
		public float SnapDistance;

		public Vector3 Offset;

		public Transform Water;

		private Transform player;

		private void LateUpdate()
		{
			if (player == null)
			{
				DropGame instance = DropGame.Instance;
				if ((bool)instance && (bool)instance.Player)
				{
					player = instance.Player.transform;
				}
			}
			if (player != null)
			{
				Vector3 vector = base.transform.InverseTransformPoint(player.position);
				vector.y = 0f;
				vector.x = (float)Mathf.RoundToInt(vector.x / SnapDistance) * SnapDistance;
				vector.z = (float)Mathf.RoundToInt(vector.z / SnapDistance) * SnapDistance;
				Water.localPosition = vector + Offset;
			}
		}
	}
}
