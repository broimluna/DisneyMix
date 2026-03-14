using UnityEngine;

namespace Mix.Games.Tray.Runner
{
	public class PlayerTrigger : MonoBehaviour
	{
		public enum TriggerType
		{
			DEATH = 0,
			JUMP = 1,
			COLLECTOR = 2,
			MAGNET = 3,
			GHOST = 4
		}

		public TriggerType triggerType;

		public AvatarController avatarController;

		private GameObject main;

		private MainRunnerGame mainController;

		private void Awake()
		{
			main = GameObject.Find("main");
			if (main == null)
			{
				Debug.LogWarning("Cannot find main game object");
				return;
			}
			mainController = main.GetComponent<MainRunnerGame>();
			if (mainController == null)
			{
				Debug.LogWarning("Cannot find main game script on main game object");
			}
		}

		private void Update()
		{
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.IsNullOrDisposed() || other.gameObject.IsNullOrDisposed() || mainController.IsNullOrDisposed() || avatarController.IsNullOrDisposed())
			{
				return;
			}
			ColliderTag component = other.gameObject.GetComponent<ColliderTag>();
			if (component.IsNullOrDisposed())
			{
				return;
			}
			if (triggerType == TriggerType.COLLECTOR)
			{
				if (component.collisionType == CollisionTypes.COLLECTIBLE)
				{
					mainController.Collect(component.gameObject);
				}
				else if (component.collisionType == CollisionTypes.END)
				{
					mainController.CrossFinishLine();
				}
				else if (component.collisionType == CollisionTypes.CHECKPOINT)
				{
					RunnerController runnerController = avatarController as RunnerController;
					runnerController.SetCheckpoint(component.gameObject);
				}
				else
				{
					if (component.collisionType != CollisionTypes.CHECKPOINT_MARKER)
					{
						return;
					}
					bool flag = false;
					CheckpointMarkerController component2 = other.gameObject.GetComponent<CheckpointMarkerController>();
					if (!component2.HasHead(avatarController.GetSwid()))
					{
						component2.AddHead(avatarController.GetSwid());
						if (!avatarController.isDamaged && !flag)
						{
							mainController.PassCheckpoint();
							component2.FlyGemToUI();
						}
					}
				}
			}
			else if (triggerType == TriggerType.DEATH)
			{
				if (component.collisionType == CollisionTypes.DEATH)
				{
					avatarController.HitObstacle();
				}
			}
			else if (triggerType == TriggerType.JUMP)
			{
				if (component.collisionType == CollisionTypes.DEATH)
				{
					avatarController.HitObstacle();
				}
				else if (component.collisionType == CollisionTypes.JUMPABLE)
				{
					RunnerController runnerController2 = avatarController as RunnerController;
					runnerController2.AddGroundedCount();
				}
			}
			else if (triggerType == TriggerType.GHOST && component.collisionType == CollisionTypes.CHECKPOINT_MARKER)
			{
				CheckpointMarkerController component3 = other.gameObject.GetComponent<CheckpointMarkerController>();
				component3.AddHead(avatarController.GetSwid());
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (other == null || other.gameObject == null)
			{
				return;
			}
			ColliderTag component = other.gameObject.GetComponent<ColliderTag>();
			if (!(component == null) && component.collisionType == CollisionTypes.JUMPABLE && triggerType == TriggerType.JUMP)
			{
				RunnerController runnerController = avatarController as RunnerController;
				if (runnerController != null)
				{
					runnerController.SubtractGroundedCount();
				}
			}
		}
	}
}
