using System;
using DG.Tweening;
using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	public class DropPlayer : MonoBehaviour
	{
		public enum ReasonForDeath
		{
			NONE = 0,
			TOO_SLOW = 1,
			HIT_FLOOR = 2,
			STUCK_IN_SNOW = 3
		}

		public Action<int> OnLandCountUpdated = delegate
		{
		};

		public Action<int> OnJumpCountUpdated = delegate
		{
		};

		public Action<int> OnBonusCountUpdated;

		public Action<Platform> OnPlatformUpdated = delegate
		{
		};

		public Action OnDie;

		public Animator PlayerLogicAnimator;

		public GameObject PlayerMesh;

		public GameObject PlayerSkin;

		public Collider PlayerCollider;

		public Rigidbody PlayerRigidBody;

		public CustomGravity PlayerCustomGravity;

		[Space(10f)]
		public ParticleSystem HitFloorParticles;

		public ParticleSystem HitSnowParticles;

		public ParticleSystem LandParticles;

		[Tooltip("The Transform that gets lerped up and down during a jump")]
		public Transform JumpRoot;

		[Header("Jump Properties")]
		[Tooltip("The height the player jumps when moving between platforms.")]
		public float JumpHeight;

		[Tooltip("The height the player hops if a fence is in the way.")]
		public float HopHeight;

		public float JumpTime;

		public float HopTime;

		[Range(0f, 2f)]
		public float StandingPositionLerpTime;

		private int landCount;

		private int jumpCount;

		private int bonusCount;

		private int activeAnimatorId;

		private int hitFloorAnimatorId;

		private int stuckInSnowAnimatorId;

		private Vector3 standingPositionLerpVelocity = Vector3.zero;

		private Platform currentPlatform;

		public DropGame Game { get; private set; }

		public ReasonForDeath DeathType { get; set; }

		public int LandCount
		{
			get
			{
				return landCount;
			}
			set
			{
				if (landCount != value)
				{
					landCount = value;
					OnLandCountUpdated(landCount);
				}
			}
		}

		public int JumpCount
		{
			get
			{
				return jumpCount;
			}
			set
			{
				if (value != jumpCount)
				{
					jumpCount = value;
					OnJumpCountUpdated(jumpCount);
				}
			}
		}

		public int BonusCount
		{
			get
			{
				return bonusCount;
			}
			set
			{
				if (bonusCount != value)
				{
					bonusCount = value;
					if (OnBonusCountUpdated != null)
					{
						OnBonusCountUpdated(bonusCount);
					}
				}
			}
		}

		public bool Active
		{
			get
			{
				return PlayerLogicAnimator.GetBool(activeAnimatorId);
			}
			set
			{
				PlayerLogicAnimator.SetBool(activeAnimatorId, value);
			}
		}

		public Platform CurrentPlatform
		{
			get
			{
				return currentPlatform;
			}
			set
			{
				if (value != currentPlatform)
				{
					if ((bool)currentPlatform)
					{
						Platform platform = currentPlatform;
						platform.OnKillOccupants = (Action)Delegate.Remove(platform.OnKillOccupants, new Action(OnMyPlatformExit));
					}
					currentPlatform = value;
					if ((bool)currentPlatform)
					{
						Platform platform2 = currentPlatform;
						platform2.OnKillOccupants = (Action)Delegate.Combine(platform2.OnKillOccupants, new Action(OnMyPlatformExit));
						OnPlatformUpdated(currentPlatform);
					}
				}
			}
		}

		public Platform JumpTargetPlatform { get; set; }

		public float LastLandTime { get; protected set; }

		public bool ShouldLerpToStandingPosition { get; set; }

		private void OnEnable()
		{
			DropPlayerBehaviour[] behaviours = PlayerLogicAnimator.GetBehaviours<DropPlayerBehaviour>();
			for (int i = 0; i < behaviours.Length; i++)
			{
				behaviours[i].Player = this;
			}
		}

		private void Awake()
		{
			OnAwake();
		}

		private void Update()
		{
			if (Active && CurrentPlatform != null)
			{
				LerpToStandingPosition();
			}
		}

		public void OnAwake()
		{
			Game = DropGame.Instance;
			PlayerCollider.enabled = false;
			activeAnimatorId = Animator.StringToHash("Active");
			hitFloorAnimatorId = Animator.StringToHash("LandOnFloor");
			stuckInSnowAnimatorId = Animator.StringToHash("StuckInSnow");
			jumpCount = 0;
			landCount = 0;
		}

		private void OnDestroy()
		{
			CurrentPlatform = null;
		}

		private void LerpToStandingPosition()
		{
			if (ShouldLerpToStandingPosition && CurrentPlatform != null)
			{
				if (this is GhostDropPlayer)
				{
					base.transform.position = Vector3.SmoothDamp(base.transform.position, CurrentPlatform.GhostStandingPosition, ref standingPositionLerpVelocity, StandingPositionLerpTime);
				}
				else
				{
					base.transform.position = Vector3.SmoothDamp(base.transform.position, CurrentPlatform.StandingPosition, ref standingPositionLerpVelocity, StandingPositionLerpTime);
				}
			}
			else
			{
				standingPositionLerpVelocity = Vector3.zero;
			}
		}

		public void HitFloor()
		{
			PlayerLogicAnimator.SetTrigger(hitFloorAnimatorId);
			CurrentPlatform = null;
			if (DeathType != ReasonForDeath.TOO_SLOW)
			{
				DeathType = ReasonForDeath.HIT_FLOOR;
			}
		}

		public void GetStuckInSnow()
		{
			PlayerLogicAnimator.SetTrigger(stuckInSnowAnimatorId);
			CurrentPlatform = null;
			DeathType = ReasonForDeath.STUCK_IN_SNOW;
		}

		public void AttachToPlatformInPlace(Platform platform)
		{
			CurrentPlatform = platform;
		}

		public virtual void ArriveAtPlatform(Platform platform)
		{
			if (platform != null)
			{
				if (platform.IsSolid)
				{
					CurrentPlatform = platform;
					LastLandTime = Game.GameTime;
					platform.PlayerArrivedAtPlatform(this);
					LandParticles.Play(true);
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
			JumpTargetPlatform = null;
		}

		public void SnapToPlatform(Platform platform, float time = -1f)
		{
			if (platform != null)
			{
				if (time < 0f)
				{
					if (this is GhostDropPlayer)
					{
						base.transform.position = platform.GhostStandingPosition;
					}
					else
					{
						base.transform.position = platform.StandingPosition;
					}
				}
				else
				{
					base.transform.position = platform.GetStandingPositionForTime(time, base.transform.position, this is GhostDropPlayer);
				}
			}
			ArriveAtPlatform(platform);
		}

		public float GetJumpTime()
		{
			float magnitude = PlayerCustomGravity.Gravity.magnitude;
			float num = Mathf.Sqrt(2f * magnitude * JumpHeight);
			return 2f * num / magnitude;
		}

		private void OnDrawGizmos()
		{
			if (CurrentPlatform != null)
			{
				Gizmos.DrawIcon(CurrentPlatform.StandingPosition, "Games/Tray/Drop/CurrentPlatform.png", true);
				Gizmos.color = Color.cyan;
				Gizmos.DrawLine(base.transform.position, CurrentPlatform.StandingPosition);
			}
		}

		private void OnMyPlatformExit()
		{
			if ((bool)PlayerLogicAnimator)
			{
				PlayerLogicAnimator.SetTrigger("Hop");
				DeathType = ReasonForDeath.TOO_SLOW;
			}
		}

		private void OnPlatformExit(Platform platform)
		{
			if (platform == CurrentPlatform)
			{
				PlayerLogicAnimator.SetTrigger("Hop");
				DeathType = ReasonForDeath.TOO_SLOW;
			}
		}

		public void PauseAllTweens()
		{
			DOTween.Pause(this);
		}

		public void ResumeAllTweens()
		{
			DOTween.Play(this);
		}
	}
}
