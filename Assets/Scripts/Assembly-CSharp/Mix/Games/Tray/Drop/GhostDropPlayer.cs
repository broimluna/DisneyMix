using System;
using System.Linq;
using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	public class GhostDropPlayer : DropPlayer
	{
		private string ghostName;

		private Platform nextPlatform;

		public DropResponse GhostResponse { get; set; }

		public NameMarker NameMarker { get; set; }

		private void Awake()
		{
			OnAwake();
		}

		private void OnDestroy()
		{
			if (nextPlatform != null)
			{
				Platform platform = nextPlatform;
				platform.OnEnter = (Action)Delegate.Remove(platform.OnEnter, new Action(OnNextPlatformEntered));
			}
		}

		private void OnNextPlatformEntered()
		{
			JumpToNextPlatform();
		}

		public override void ArriveAtPlatform(Platform platform)
		{
			if (platform != null)
			{
				if (platform.IsSolid)
				{
					base.CurrentPlatform = platform;
					base.LastLandTime = base.Game.GameTime;
					platform.PlayerArrivedAtPlatform(this);
					LandParticles.Play(true);
					nextPlatform = GetNextPlatform();
					if (nextPlatform == null)
					{
						JumpToDeath();
					}
					else if (nextPlatform.HasEntered)
					{
						if (!base.Game.IsInTutorial)
						{
							JumpToNextPlatform();
						}
					}
					else
					{
						Platform platform2 = nextPlatform;
						platform2.OnEnter = (Action)Delegate.Combine(platform2.OnEnter, new Action(OnNextPlatformEntered));
					}
				}
				else
				{
					HitFloor();
				}
			}
			else
			{
				HitFloor();
			}
			base.JumpTargetPlatform = null;
		}

		public void JumpToNextPlatform()
		{
			if (!(base.CurrentPlatform != null) || base.Game.GhostScore >= GhostResponse.Score || !(base.CurrentPlatform.Configuration.NextPlatform != null))
			{
				return;
			}
			Platform platform = nextPlatform;
			platform.OnEnter = (Action)Delegate.Remove(platform.OnEnter, new Action(OnNextPlatformEntered));
			Coordinate2D coordinate2D = nextPlatform.Configuration.GridCoordinates - base.CurrentPlatform.Configuration.GridCoordinates;
			int id = Animator.StringToHash("MoveX");
			int id2 = Animator.StringToHash("MoveY");
			int x = coordinate2D.x;
			int y = coordinate2D.y;
			float num = base.Game.GameTime + JumpTime;
			if (nextPlatform != null && num > nextPlatform.EnterTimeWithEarlyJumpAllowance && num < nextPlatform.ExitTime)
			{
				int[] bonusJumps = GhostResponse.BonusJumps;
				foreach (int num2 in bonusJumps)
				{
					if (num2 == base.JumpCount)
					{
						base.BonusCount++;
					}
				}
				PlayerLogicAnimator.SetInteger(id, x);
				PlayerLogicAnimator.SetInteger(id2, y);
			}
			else
			{
				PlayerLogicAnimator.SetInteger(id, x);
				PlayerLogicAnimator.SetInteger(id2, y);
			}
		}

		public void JumpToDeath()
		{
			if (GhostResponse == null || GhostResponse.JumpsOffPath == null || GhostResponse.JumpsOffPath.Count() <= 0)
			{
				return;
			}
			DropDirection dropDirection = GhostResponse.JumpsOffPath[GhostResponse.JumpsOffPath.Length - 1];
			if (dropDirection != null && !base.CurrentPlatform.IsNullOrDisposed() && base.CurrentPlatform.Configuration != null)
			{
				Coordinate2D coordinate2D = new Coordinate2D(dropDirection.X, dropDirection.Y);
				coordinate2D -= base.CurrentPlatform.Configuration.GridCoordinates;
				if (PlayerLogicAnimator != null)
				{
					PlayerLogicAnimator.SetInteger("MoveX", coordinate2D.x);
					PlayerLogicAnimator.SetInteger("MoveY", coordinate2D.y);
				}
			}
		}

		public Platform GetNextPlatform()
		{
			if (base.CurrentPlatform != null && base.CurrentPlatform.Configuration.NextPlatform != null)
			{
				Platform platformAtCoords = base.CurrentPlatform.Configuration.NextPlatform;
				DropDirection[] jumpsOffPath = GhostResponse.JumpsOffPath;
				foreach (DropDirection dropDirection in jumpsOffPath)
				{
					if (dropDirection.JumpCount == base.JumpCount)
					{
						platformAtCoords = base.Game.LevelGenerator.GetPlatformAtCoords(new Coordinate2D(dropDirection.X, dropDirection.Y));
						break;
					}
				}
				return platformAtCoords;
			}
			return null;
		}

		public void UpdateGhostScore(int score)
		{
			if (NameMarker != null && score > 0)
			{
				if (ghostName == null)
				{
					ghostName = base.Game.GameController.GetGhostName(GhostResponse.PlayerSwid);
				}
				NameMarker.NameText.text = score + Environment.NewLine + ghostName;
			}
		}
	}
}
