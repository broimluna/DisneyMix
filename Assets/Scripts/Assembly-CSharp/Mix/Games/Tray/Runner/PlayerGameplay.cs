using UnityEngine;

namespace Mix.Games.Tray.Runner
{
	public class PlayerGameplay : MonoBehaviour
	{
		public float jumpForce = 1f;

		public float gravity = 7.5f;

		public float damageLaunchForce = 10f;

		public float collisionResetHeight = 10f;

		public string[] jumpTransitions;

		public float respawnWait;

		public float preCameraMoveWait;

		public float damageWait;

		public float respawnInterpDuration;

		public float maxSpeed = 15f;

		public float acceleration = 0.5f;

		public float ACCEL_THRESHOLD = 0.5f;

		public float GetTotalDamageWait()
		{
			return damageWait;
		}
	}
}
