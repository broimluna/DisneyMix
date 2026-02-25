using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mix.Games.Tray.Drop
{
	[RequireComponent(typeof(DropPlayer))]
	public class TapToJump : MonoBehaviour
	{
		[Range(0f, 1f)]
		public float SwipeDistAsPercentOfScreenWidth;

		public float MinTapDistanceWorldUnits;

		[Tooltip("Amount input is biased towards the correct direction")]
		public float InputBiasAmount;

		private int moveXAnimatorId;

		private int moveYAnimatorId;

		private Plane touchPlane;

		private DropPlayer player;

		private Coordinate2D moveDirection;

		private bool inputEnabled;

		private void Start()
		{
			player = GetComponent<DropPlayer>();
			touchPlane = new Plane(Vector3.up, Vector3.zero);
			moveDirection = new Coordinate2D(0, 0);
			moveXAnimatorId = Animator.StringToHash("MoveX");
			moveYAnimatorId = Animator.StringToHash("MoveY");
			DropInputHandler inputHandler = player.Game.InputHandler;
			inputHandler.OnTouchBegin = (Action<PointerEventData>)Delegate.Combine(inputHandler.OnTouchBegin, new Action<PointerEventData>(OnTouchBegin));
			DropGame game = player.Game;
			game.OnGameStart = (Action)Delegate.Combine(game.OnGameStart, (Action)delegate
			{
				inputEnabled = true;
			});
			DropGame game2 = player.Game;
			game2.OnGameStop = (Action)Delegate.Combine(game2.OnGameStop, (Action)delegate
			{
				inputEnabled = false;
			});
		}

		private void Update()
		{
			if (inputEnabled)
			{
				if (moveDirection.x != 0 || moveDirection.y != 0)
				{
					player.PlayerLogicAnimator.SetInteger(moveXAnimatorId, moveDirection.x);
					player.PlayerLogicAnimator.SetInteger(moveYAnimatorId, moveDirection.y);
					moveDirection.x = 0;
					moveDirection.y = 0;
					if ((bool)player.Game.GhostPlayer && player.CurrentPlatform == player.Game.GhostPlayer.CurrentPlatform && player.Game.IsInTutorial)
					{
						Invoke("JumpToNextPlatform", 0.2f);
					}
				}
			}
			else
			{
				moveDirection.x = 0;
				moveDirection.y = 0;
			}
		}

		private void JumpToNextPlatform()
		{
			if (player != null && player.Game != null)
			{
				player.Game.GhostPlayer.JumpToNextPlatform();
			}
		}

		private Vector2 GetInputBias()
		{
			Vector2 result = Vector3.zero;
			Platform platform = player.CurrentPlatform;
			if (player.JumpTargetPlatform != null)
			{
				platform = player.JumpTargetPlatform;
			}
			if (platform != null && platform.Configuration != null && platform.Configuration.NextPlatform != null)
			{
				result = (platform.Configuration.NextPlatform.Configuration.GridCoordinates - platform.Configuration.GridCoordinates) * InputBiasAmount;
			}
			return result;
		}

		private void OnTouchBegin(PointerEventData eventData)
		{
			Vector3 touchOnTouchPlane = GetTouchOnTouchPlane(eventData.position);
			Vector2 vector = new Vector2(touchOnTouchPlane.x - player.transform.position.x, touchOnTouchPlane.z - player.transform.position.z);
			vector += GetInputBias();
			if (vector.sqrMagnitude > MinTapDistanceWorldUnits * MinTapDistanceWorldUnits)
			{
				float num = Mathf.Max(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
				moveDirection.x = (int)(vector.x / num);
				moveDirection.y = (int)(vector.y / num);
			}
		}

		private Vector3 GetTouchOnTouchPlane(Vector2 screenCoords)
		{
			Ray ray = player.Game.GameController.MixGameCamera.ScreenPointToRay(screenCoords);
			float enter = 0f;
			if (touchPlane.Raycast(ray, out enter))
			{
				return ray.GetPoint(enter);
			}
			return Vector3.zero;
		}
	}
}
