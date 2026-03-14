using UnityEngine;

namespace Mix.Games.Tray.Runner
{
	public class GhostController : AvatarController
	{
		public RunnerRecorder ghostData;

		private int ghostDataIndex;

		private float ghostOffsetZ;

		private void FixedUpdate()
		{
			if (mainController.mode == GameMode.PLAY || mainController.mode == GameMode.DAMAGE || mainController.mode == GameMode.END)
			{
				if (ghostDataIndex < ghostData.GetLength() && elapsedTicks >= ghostData.GetByIndex(ghostDataIndex).ticks)
				{
					ProcessCommand(ghostData.GetByIndex(ghostDataIndex));
					ghostDataIndex++;
				}
				if (!isPausingFromSpawn)
				{
					Move();
					DecrementDamageTimer();
					CheckCollisionFreeFall();
					ManageAcceleration();
				}
				else
				{
					DecrementSpawnPauseTimer();
				}
				elapsedTicks++;
				elapsedPlayTime += Time.fixedDeltaTime;
			}
		}

		private void ProcessCommand(RunnerGhostData data)
		{
			base.transform.localPosition = data.position + new Vector3(0f, 0f, ghostOffsetZ);
			switch (data.type)
			{
			case GhostDataType.RESPAWN:
				RespawnGhost(data.position, true);
				break;
			case GhostDataType.SPAWN:
				RespawnGhost(data.position);
				break;
			case GhostDataType.JUMP:
				Jump();
				break;
			case GhostDataType.DIE:
				HitObstacle();
				break;
			case GhostDataType.END:
				End();
				break;
			case GhostDataType.LAUNCH:
				break;
			}
		}

		public void RespawnGhost(Vector3 pos, bool respawn = false)
		{
			if (respawn)
			{
				Respawn();
			}
			base.transform.localPosition = pos + new Vector3(0f, 0f, ghostOffsetZ);
		}

		public void PlaceGhostOnZ(float aPos)
		{
			ghostOffsetZ = aPos;
			base.transform.localPosition += new Vector3(0f, 0f, aPos);
		}
	}
}
