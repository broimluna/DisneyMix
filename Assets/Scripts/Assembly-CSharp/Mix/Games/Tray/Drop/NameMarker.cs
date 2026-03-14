using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.Drop
{
	public class NameMarker : MonoBehaviour
	{
		[Tooltip("If the target player is less than this distance to the main player then hide to tooltip")]
		public float MinDistanceToMainPlayer;

		public Vector3 TargetOffset;

		[Space(10f)]
		public float ScaleInTime;

		public Ease ScaleInEaseType;

		[Space(5f)]
		public float ScaleOutTime;

		public Ease ScaleOutEaseType;

		[Header("Internal References")]
		public Text NameText;

		private DropGame game;

		private DropPlayer target;

		private bool isShowing;

		private Vector3 distanceFromMainPlayer;

		public DropPlayer TargetPlayer
		{
			get
			{
				return target;
			}
			set
			{
				if (target != null)
				{
					DropPlayer dropPlayer = target;
					dropPlayer.OnDie = (Action)Delegate.Remove(dropPlayer.OnDie, new Action(OnTargetDie));
				}
				target = value;
				if (target != null)
				{
					DropPlayer dropPlayer2 = target;
					dropPlayer2.OnDie = (Action)Delegate.Combine(dropPlayer2.OnDie, new Action(OnTargetDie));
				}
			}
		}

		public DropPlayer MainPlayer { get; set; }

		private void Awake()
		{
			game = DropGame.Instance;
			DropGame dropGame = game;
			dropGame.OnGameStop = (Action)Delegate.Combine(dropGame.OnGameStop, new Action(OnTargetDie));
			base.transform.localScale = Vector3.zero;
			isShowing = false;
		}

		private void OnTargetDie()
		{
			TargetPlayer = null;
			DropGame dropGame = game;
			dropGame.OnGameStop = (Action)Delegate.Remove(dropGame.OnGameStop, new Action(OnTargetDie));
			Hide();
			UnityEngine.Object.Destroy(base.gameObject, ScaleOutTime);
		}

		private void LateUpdate()
		{
			if (!TargetPlayer)
			{
				return;
			}
			if (isShowing)
			{
				Vector3 position = game.GameController.MixGameCamera.WorldToScreenPoint(TargetPlayer.transform.position);
				position.z = game.UIManager.UICanvas.planeDistance;
				base.transform.position = game.UICamera.ScreenToWorldPoint(position) + TargetOffset;
			}
			if (MainPlayer != null)
			{
				distanceFromMainPlayer = MainPlayer.transform.position - TargetPlayer.transform.position;
				if (distanceFromMainPlayer.sqrMagnitude > MinDistanceToMainPlayer * MinDistanceToMainPlayer)
				{
					Show();
				}
				else
				{
					Hide();
				}
			}
		}

		private void Show()
		{
			if (!isShowing)
			{
				DOTween.Kill(base.gameObject);
				base.transform.DOScale(Vector3.one, ScaleInTime).SetEase(ScaleInEaseType);
				isShowing = true;
			}
		}

		private void Hide()
		{
			if (isShowing)
			{
				DOTween.Kill(base.gameObject);
				base.transform.DOScale(Vector3.zero, ScaleOutTime).SetEase(ScaleOutEaseType);
				isShowing = false;
			}
		}
	}
}
