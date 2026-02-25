using System;
using System.Collections.Generic;
using Mix.Games.Tray.Common;
using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	[SelectionBase]
	public class Platform : MonoBehaviour
	{
		public Action OnAnticipation;

		public Action OnEnter;

		public Action OnWarning;

		public Action OnExit;

		public Action OnKillOccupants;

		public Transform PlatformMesh;

		public Transform SinkRoot;

		public PointTowardsGameCamera ColumnFadeCollider;

		public Transform StandingLocationParent;

		[Space(10f)]
		public Transform StandingLocation;

		public Transform GhostStandingLocation;

		public List<Transform> TallPlatformSplatLocations;

		[Space(10f)]
		public GameObject DefaultPlatform;

		public GameObject SpikePlatform;

		public GameObject QuickPlatform;

		public GameObject LongPlatform;

		[Space(10f)]
		public ParticleSystem AppearWarningParticles;

		public ParticleSystem SpikesAappearWarningParticles;

		[Space(10f)]
		public GameObject FencePosts;

		[Space(5f)]
		public GameObject FenceNorth;

		public GameObject FenceEast;

		public GameObject FenceSouth;

		public GameObject FenceWest;

		[Space(5f)]
		public Transform IceEffectBase;

		[Space(10f)]
		public float MinHeight;

		public float MaxHeight;

		public float HeightOverTime;

		[Space(10f)]
		public float MinIceEffectScale;

		public float MaxIceEffectScale;

		public float IceEffectScaleOverTime;

		[Space(10f)]
		public float EnterWarningTime;

		public float ExitWarningTime;

		private Animator animator;

		private DropGame dropGame;

		private HashSet<DropPlayer> visitedPlayers = new HashSet<DropPlayer>();

		private float currentTime = -1f;

		private float minTimeBetweenDrops;

		private List<Transform> allPlatforms;

		public int PatternNumber { get; set; }

		public bool IsFirstPlatform { get; set; }

		public bool IsSolid
		{
			get
			{
				return currentTime >= EnterTimeWithEarlyJumpAllowance && currentTime <= ExitTimeWithLateJumpAllowance;
			}
		}

		public bool HasEntered
		{
			get
			{
				return currentTime >= EnterTimeWithEarlyJumpAllowance;
			}
		}

		public bool IsInUse
		{
			get
			{
				if (Configuration == null)
				{
					return false;
				}
				return currentTime > Configuration.EnterTime && currentTime <= Configuration.ExitTime + minTimeBetweenDrops;
			}
		}

		public Vector3 StandingPosition
		{
			get
			{
				return StandingLocation.transform.position;
			}
		}

		public Vector3 GhostStandingPosition
		{
			get
			{
				return GhostStandingLocation.transform.position;
			}
		}

		public float CurrentTime
		{
			get
			{
				return currentTime;
			}
			set
			{
				if (UnityEngine.Application.isPlaying)
				{
					OnGameTimeUpdate(value);
				}
				else
				{
					currentTime = value;
				}
			}
		}

		public PlatformInfo Configuration { get; set; }

		public float EnterTimeWithEarlyJumpAllowance
		{
			get
			{
				if (Configuration != null && dropGame != null)
				{
					return Configuration.EnterTime - dropGame.EarlyJumpAllowance;
				}
				return 0f;
			}
		}

		public float EnterTime
		{
			get
			{
				if (Configuration == null)
				{
					return 0f;
				}
				return Configuration.EnterTime;
			}
		}

		public float ExitTimeWithLateJumpAllowance
		{
			get
			{
				if (Configuration != null && dropGame != null)
				{
					return Configuration.ExitTime + dropGame.LateJumpAllowance;
				}
				return 0f;
			}
		}

		public float ExitTime
		{
			get
			{
				if (Configuration == null)
				{
					return 0f;
				}
				return Configuration.ExitTime;
			}
		}

		public void SetDropGame(DropGame game)
		{
			dropGame = game;
		}

		private void OnEnable()
		{
			if (dropGame == null)
			{
				dropGame = DropGame.Instance;
			}
			DropGame obj = dropGame;
			obj.OnGameTimeUpdated = (Action<float>)Delegate.Combine(obj.OnGameTimeUpdated, new Action<float>(OnGameTimeUpdate));
			if (ColumnFadeCollider.Target == null)
			{
				ColumnFadeCollider.Target = dropGame.GameController.MixGameCamera.transform;
			}
		}

		private void OnDisable()
		{
			DropGame obj = dropGame;
			obj.OnGameTimeUpdated = (Action<float>)Delegate.Remove(obj.OnGameTimeUpdated, new Action<float>(OnGameTimeUpdate));
		}

		private void Awake()
		{
			if (allPlatforms == null)
			{
				BuildPlatformList();
			}
			for (int i = 0; i < allPlatforms.Count; i++)
			{
				allPlatforms[i].gameObject.SetActive(false);
			}
			Configuration = new PlatformInfo();
			visitedPlayers.Clear();
			IsFirstPlatform = false;
			currentTime = float.MinValue;
			if (animator == null)
			{
				animator = GetComponent<Animator>();
			}
			animator.SetTrigger("Reset");
			ColumnFadeCollider.gameObject.SetActive(false);
		}

		private void Start()
		{
		}

		public void ResetPlatformConfiguration()
		{
			Configuration = new PlatformInfo();
		}

		private void BuildPlatformList()
		{
			allPlatforms = new List<Transform>();
			allPlatforms.Add(DefaultPlatform.transform);
			allPlatforms.Add(QuickPlatform.transform);
			allPlatforms.Add(SpikePlatform.transform);
			allPlatforms.Add(LongPlatform.transform);
		}

		public void EnqueueAppearance(Coordinate2D position, float timeToEnter, float timeToSink, PlatformType type)
		{
			Configuration = new PlatformInfo(position, timeToEnter, timeToSink, type);
			ApplyPlatformConfiguration();
		}

		public void CopyConfiguration(PlatformInfo original)
		{
			Configuration = new PlatformInfo(original);
			ApplyPlatformConfiguration();
		}

		public void CopyConfiguration(PlatformInfo original, Coordinate2D origin, float timeOffset, float timeMultiplier)
		{
			Configuration = new PlatformInfo(original);
			Configuration.GridCoordinates += origin;
			Configuration.EnterTime = timeOffset + original.EnterTime * timeMultiplier;
			Configuration.ExitTime = timeOffset + original.ExitTime * timeMultiplier;
			ApplyPlatformConfiguration();
		}

		public bool WillBeInUse(float time)
		{
			return time > Configuration.EnterTime && time <= Configuration.ExitTime + minTimeBetweenDrops;
		}

		public void PlayerArrivedAtPlatform(DropPlayer player)
		{
			if (Configuration.Type == PlatformType.SPIKES)
			{
				player.GetStuckInSnow();
				animator.SetTrigger("PlayerStuck");
			}
			else if (!visitedPlayers.Contains(player))
			{
				if (!IsFirstPlatform)
				{
					player.LandCount++;
				}
				visitedPlayers.Add(player);
			}
		}

		public void ApplyPlatformConfiguration()
		{
			ApplyPosition();
			ApplyRotation();
			ApplyPlatformType();
			ApplyFences();
		}

		public void ApplyFences()
		{
			FenceNorth.SetActive(Configuration.Fences.North);
			FenceEast.SetActive(Configuration.Fences.East);
			FenceSouth.SetActive(Configuration.Fences.South);
			FenceWest.SetActive(Configuration.Fences.West);
			FencePosts.SetActive(Configuration.Fences.IsAnyFenceEnabled);
		}

		public void ApplyPosition()
		{
			base.transform.localPosition = new Vector3(dropGame.GridSpacing.x * (float)Configuration.GridCoordinates.x, 0f, dropGame.GridSpacing.y * (float)Configuration.GridCoordinates.y);
		}

		public void ApplyPlatformType()
		{
			PlatformType type = Configuration.Type;
			DefaultPlatform.SetActive(type == PlatformType.DEFAULT);
			SpikePlatform.SetActive(type == PlatformType.SPIKES);
			QuickPlatform.SetActive(type == PlatformType.QUICK);
			LongPlatform.SetActive(type == PlatformType.LONG);
		}

		public void ApplyRotation()
		{
			Vector3 localEulerAngles = ((Configuration.Rotation >= 0) ? new Vector3(0f, 90f * (float)Configuration.Rotation, 0f) : ((!UnityEngine.Application.isPlaying) ? new Vector3(0f, 0f, 0f) : new Vector3(0f, (float)UnityEngine.Random.Range(0, 5) * 90f, 0f)));
			for (int i = 0; i < allPlatforms.Count; i++)
			{
				allPlatforms[i].localEulerAngles = localEulerAngles;
			}
			if (StandingLocationParent != null)
			{
				StandingLocationParent.localEulerAngles = new Vector3(0f, (float)UnityEngine.Random.Range(0, 5) * 90f, 0f);
			}
		}

		public bool CanJumpInDirection(Coordinate2D jumpDirection)
		{
			if (jumpDirection.x == 0)
			{
				if (jumpDirection.y > 0)
				{
					return !Configuration.Fences.North;
				}
				if (jumpDirection.y < 0)
				{
					return !Configuration.Fences.South;
				}
			}
			else if (jumpDirection.y == 0)
			{
				if (jumpDirection.x > 0)
				{
					return !Configuration.Fences.East;
				}
				if (jumpDirection.x < 0)
				{
					return !Configuration.Fences.West;
				}
			}
			return true;
		}

		public void OnDrawGizmos()
		{
			if (Configuration != null)
			{
				if (IsInUse)
				{
					Gizmos.color = Color.green;
				}
				else
				{
					Gizmos.color = Color.black;
				}
				Gizmos.DrawWireCube(base.transform.position, new Vector3(2f, 0f, 2f));
				float num = 5f;
				float num2 = Configuration.ExitTime - Configuration.EnterTime;
				if (IsInUse)
				{
					Gizmos.color = new Color(0f, 1f, 1f, 0.5f);
				}
				else
				{
					Gizmos.color = new Color(0f, 0f, 0f, 0.2f);
				}
				Gizmos.DrawCube(base.transform.position + Vector3.up * (Configuration.EnterTime - currentTime + num2 / 2f) * num, new Vector3(1.8f, num2 * num, 1.8f));
				if (IsInUse)
				{
					Gizmos.color = new Color(0f, 1f, 1f, 0.5f);
				}
				else
				{
					Gizmos.color = new Color(1f, 1f, 1f, 0.4f);
				}
				Gizmos.DrawWireCube(base.transform.position + Vector3.up * (Configuration.EnterTime - currentTime + num2 / 2f) * num, new Vector3(1.8f, num2 * num, 1.8f));
				if (UnityEngine.Application.isPlaying)
				{
					Gizmos.color = new Color(0f, 1f, 0f, 0.2f);
					Gizmos.DrawCube(base.transform.position + Vector3.up * (Configuration.EnterTime - EnterWarningTime - currentTime + EnterWarningTime / 2f) * num, new Vector3(0.5f, EnterWarningTime * num, 0.5f));
					Gizmos.color = new Color(1f, 0f, 0f, 0.2f);
					Gizmos.DrawCube(base.transform.position + Vector3.up * (Configuration.ExitTime - ExitWarningTime - currentTime + ExitWarningTime / 2f) * num, new Vector3(0.5f, ExitWarningTime * num, 0.5f));
				}
				if (Configuration.NextPlatform != null)
				{
					Vector3 vector = Vector3.up * 1.2f;
					Gizmos.color = Color.cyan;
					GizmoExtensions.DrawArrowGizmo(base.transform.position + vector, Configuration.NextPlatform.transform.position + vector, 0.2f);
				}
			}
		}

		private void OnGameTimeUpdate(float newTime)
		{
			if (!animator.isInitialized)
			{
				return;
			}
			if (currentTime < Configuration.EnterTime - EnterWarningTime && newTime >= Configuration.EnterTime - EnterWarningTime)
			{
				ApplyPlatformConfiguration();
				animator.SetTrigger("AppearWarning");
				if (Configuration.Type == PlatformType.SPIKES)
				{
					SpikesAappearWarningParticles.Play(true);
					SpikesAappearWarningParticles.transform.localEulerAngles = new Vector3(-90f, UnityEngine.Random.Range(0f, 360f), 0f);
				}
				else
				{
					AppearWarningParticles.Play(true);
					AppearWarningParticles.transform.localEulerAngles = new Vector3(-90f, UnityEngine.Random.Range(0f, 360f), 0f);
				}
				if (OnAnticipation != null)
				{
					OnAnticipation();
				}
				ColumnFadeCollider.gameObject.SetActive(true);
				if (newTime > 0f)
				{
					DropAudio.PlaySound("SFX/Gameplay/Platform/AppearAnticipation");
				}
			}
			if (currentTime < Configuration.EnterTime && newTime >= Configuration.EnterTime)
			{
				animator.SetTrigger("Appear");
				if (OnEnter != null)
				{
					OnEnter();
				}
				if (newTime > 0f)
				{
					DropAudio.PlaySound("SFX/Gameplay/Platform/Appear");
				}
			}
			if (currentTime <= Configuration.ExitTime && newTime > Configuration.ExitTime)
			{
				animator.SetTrigger("Sink");
				if (OnExit != null)
				{
					OnExit();
				}
				ColumnFadeCollider.gameObject.SetActive(false);
				DropAudio.PlaySound("SFX/Gameplay/Platform/Disappear");
			}
			if (currentTime <= ExitTimeWithLateJumpAllowance && newTime > ExitTimeWithLateJumpAllowance && OnKillOccupants != null)
			{
				OnKillOccupants();
			}
			if (currentTime <= Configuration.ExitTime - ExitWarningTime && newTime > Configuration.ExitTime - ExitWarningTime)
			{
				if (dropGame.Player.CurrentPlatform == this)
				{
					animator.SetBool("SinkWarning", true);
					DropAudio.PlaySound("SFX/Gameplay/Platform/DisappearWarning");
				}
				if (OnWarning != null)
				{
					OnWarning();
				}
			}
			SinkRoot.transform.localPosition = GetSinkRootPositionForTime(newTime);
			IceEffectBase.localScale = Vector3.one * GetIceEffectBaseScaleForTime(newTime);
			currentTime = newTime;
		}

		public Vector3 GetStandingPositionForTime(float time, Vector3 startingPosition, bool getGhostPosition = false)
		{
			bool hasChanged = SinkRoot.hasChanged;
			Vector3 localPosition = SinkRoot.transform.localPosition;
			SinkRoot.transform.localPosition = GetSinkRootPositionForTime(time, true);
			Vector3 result = Vector3.zero;
			if (Configuration == null || Configuration.Type != PlatformType.SPIKES)
			{
				result = ((!getGhostPosition) ? StandingPosition : GhostStandingPosition);
			}
			else
			{
				float num = float.MaxValue;
				for (int i = 0; i < TallPlatformSplatLocations.Count; i++)
				{
					float sqrMagnitude = (TallPlatformSplatLocations[i].transform.position - startingPosition).sqrMagnitude;
					if (sqrMagnitude < num)
					{
						result = TallPlatformSplatLocations[i].transform.position;
						num = sqrMagnitude;
					}
				}
			}
			SinkRoot.transform.localPosition = localPosition;
			SinkRoot.transform.hasChanged = hasChanged;
			return result;
		}

		private Vector3 GetSinkRootPositionForTime(float time, bool considerFudge = false)
		{
			Vector3 zero = Vector3.zero;
			if (considerFudge && time < Configuration.EnterTime && time >= EnterTimeWithEarlyJumpAllowance)
			{
				time = Configuration.EnterTime;
			}
			if (time < Configuration.EnterTime || time > Configuration.ExitTime)
			{
				return Vector3.up * MinHeight;
			}
			float num = Configuration.ExitTime - Configuration.EnterTime - (time - Configuration.EnterTime);
			return Vector3.up * Mathf.Min(MaxHeight, MinHeight + HeightOverTime * num);
		}

		private float GetIceEffectBaseScaleForTime(float time)
		{
			float num = Configuration.ExitTime - Configuration.EnterTime - (time - Configuration.EnterTime);
			return Mathf.Min(MaxIceEffectScale, MinIceEffectScale + IceEffectScaleOverTime * num);
		}

		public bool HasPlayerVisitedPlatform(DropPlayer player)
		{
			return visitedPlayers.Contains(player);
		}
	}
}
