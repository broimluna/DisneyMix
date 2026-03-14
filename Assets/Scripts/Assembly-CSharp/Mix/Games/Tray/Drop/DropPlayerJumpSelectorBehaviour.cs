using DG.Tweening;
using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	public class DropPlayerJumpSelectorBehaviour : DropPlayerBehaviour
	{
		private int moveXAnimatorId;

		private int moveYAnimatorId;

		private int destinationXAnimatorId;

		private int destinationYAnimatorId;

		private int jumpTriggerAnimatorId;

		private int jumpToDeathTriggerAnimatorId;

		private int hopTriggerAnimatorId;

		private int hopToDeathTriggerAnimatorId;

		private int landTriggerAnimatorId;

		private void OnEnable()
		{
			moveXAnimatorId = Animator.StringToHash("MoveX");
			moveYAnimatorId = Animator.StringToHash("MoveY");
			destinationXAnimatorId = Animator.StringToHash("DestinationX");
			destinationYAnimatorId = Animator.StringToHash("DestinationY");
			jumpTriggerAnimatorId = Animator.StringToHash("Jump");
			jumpToDeathTriggerAnimatorId = Animator.StringToHash("JumpToDeath");
			hopTriggerAnimatorId = Animator.StringToHash("Hop");
			hopToDeathTriggerAnimatorId = Animator.StringToHash("HopToDeath");
			landTriggerAnimatorId = Animator.StringToHash("Landed");
		}

		public override void OnStateEnterOnce(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			Coordinate2D coordinate2D = new Coordinate2D(animator.GetInteger(moveXAnimatorId), animator.GetInteger(moveYAnimatorId));
			Coordinate2D coordinate2D2 = base.Player.CurrentPlatform.Configuration.GridCoordinates + coordinate2D;
			bool flag = coordinate2D.x == 0 && coordinate2D.y == 0;
			flag |= base.Player.Game.ColumnGenerator.IsColumnAtGridCoordinate(coordinate2D2);
			if (!(flag | !base.Player.CurrentPlatform.CanJumpInDirection(coordinate2D)))
			{
				Platform platformAtCoords = base.Player.Game.LevelGenerator.GetPlatformAtCoords(coordinate2D2);
				bool flag2 = !base.Player.Game.IsInTutorial || !platformAtCoords || !platformAtCoords.HasPlayerVisitedPlatform(base.Player);
				float landingTime = ((!flag2) ? base.Player.Game.GameTime : (base.Player.Game.GameTime + base.Player.JumpTime));
				if (platformAtCoords != null && WillPlayerMakeJump(platformAtCoords, landingTime, false))
				{
					DoJump(platformAtCoords, landingTime, animator);
				}
				else
				{
					DoJumpToDeath(base.Player.Game.LevelGenerator.GetPositionForCoords(coordinate2D2), animator);
				}
				animator.SetInteger(destinationXAnimatorId, coordinate2D2.x);
				animator.SetInteger(destinationYAnimatorId, coordinate2D2.y);
				if (base.Player.CurrentPlatform.Configuration.NextPlatform != platformAtCoords && flag2)
				{
					base.Player.Game.JumpsOffPath.Add(new DropDirection(base.Player.JumpCount, coordinate2D2.x, coordinate2D2.y));
				}
				if (flag2)
				{
					base.Player.JumpCount++;
				}
			}
			else
			{
				float landingTime2 = ((base.Player.Game.IsInTutorial && base.Player.CurrentPlatform.HasPlayerVisitedPlatform(base.Player)) ? base.Player.Game.GameTime : (base.Player.Game.GameTime + base.Player.JumpTime));
				if (WillPlayerMakeJump(base.Player.CurrentPlatform, landingTime2, true))
				{
					DoHop(landingTime2, animator);
				}
				else
				{
					DoHopToDeath(base.Player.Game.LevelGenerator.GetPositionForCoords(coordinate2D2), animator);
				}
				animator.SetInteger(destinationXAnimatorId, base.Player.CurrentPlatform.Configuration.GridCoordinates.x);
				animator.SetInteger(destinationYAnimatorId, base.Player.CurrentPlatform.Configuration.GridCoordinates.y);
			}
			animator.SetInteger(moveXAnimatorId, 0);
			animator.SetInteger(moveYAnimatorId, 0);
			base.Player.CurrentPlatform = null;
			DropAudio.PlaySound("SFX/Gameplay/Player/Jump");
		}

		private bool WillPlayerMakeJump(Platform destinationPlatform, float landingTime, bool isHop)
		{
			if (base.Player is GhostDropPlayer && !isHop)
			{
				return true;
			}
			return landingTime > destinationPlatform.EnterTimeWithEarlyJumpAllowance && landingTime <= destinationPlatform.ExitTime + base.Player.Game.LateJumpAllowance;
		}

		private void DoJump(Platform destinationPlatform, float landingTime, Animator animator)
		{
			float num = base.Player.Game.GameTime - base.Player.LastLandTime;
			if (num > 0f && num < base.Player.Game.QuickJumpBonusTime && !(base.Player is GhostDropPlayer))
			{
				base.Player.Game.EarnBonus();
			}
			base.Player.transform.DOJump(destinationPlatform.GetStandingPositionForTime(landingTime, base.Player.transform.position, base.Player is GhostDropPlayer), base.Player.JumpHeight, 1, base.Player.JumpTime).OnComplete(delegate
			{
				JumpOrHopComplete(animator);
			}).SetId(base.Player);
			animator.SetTrigger(jumpTriggerAnimatorId);
			base.Player.JumpTargetPlatform = destinationPlatform;
		}

		private void DoJumpToDeath(Vector3 destinationPosition, Animator animator)
		{
			base.Player.transform.DOJump(destinationPosition, base.Player.JumpHeight, 1, base.Player.JumpTime).OnComplete(delegate
			{
				JumpOrHopComplete(animator);
			}).SetId(base.Player);
			animator.SetTrigger(jumpToDeathTriggerAnimatorId);
		}

		private void DoHop(float landingTime, Animator animator)
		{
			base.Player.transform.DOJump(base.Player.CurrentPlatform.GetStandingPositionForTime(landingTime, base.Player.transform.position, base.Player is GhostDropPlayer), base.Player.HopHeight, 1, base.Player.HopTime).OnComplete(delegate
			{
				JumpOrHopComplete(animator);
			}).SetId(base.Player);
			animator.SetTrigger(hopTriggerAnimatorId);
			base.Player.JumpTargetPlatform = base.Player.CurrentPlatform;
		}

		private void DoHopToDeath(Vector3 destinationPosition, Animator animator)
		{
			base.Player.transform.DOJump(destinationPosition, base.Player.HopHeight, 1, base.Player.HopTime).OnComplete(delegate
			{
				JumpOrHopComplete(animator);
			}).SetId(base.Player);
			animator.SetTrigger(hopToDeathTriggerAnimatorId);
		}

		private void JumpOrHopComplete(Animator animator)
		{
			animator.SetTrigger(landTriggerAnimatorId);
		}
	}
}
