using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mix.Games.Tray.SecretWord
{
	public class Tile : MonoBehaviour
	{
		[Header("Internal References")]
		public TextMesh letter;

		public GameObject offsetParent;

		public GameObject visuals;

		public TileColliderHelper colliderHelper;

		public Collider touchCollider;

		[Header("Particle Systems")]
		public ParticleSystem dropParticles;

		public ParticleSystem explodeParticles;

		public ParticleSystem celebrateParticles;

		public ParticleSystem loseHeartParticles;

		[Header("Animation Variables")]
		public float dragLiftTime = 0.2f;

		public Ease dragLiftEasing = Ease.OutBack;

		[Space(5f)]
		public float dragDropTime = 0.2f;

		public Ease dragDropEasing = Ease.OutBounce;

		[Range(0f, 1f)]
		public float dropParticleTime = 0.4f;

		[Space(5f)]
		public float flyLiftTime = 0.1f;

		public float flyTravelTime = 0.35f;

		public float dragRaiseAmount = 0.5f;

		public float dragScaleAmount = 1.5f;

		public float appearTime = 0.25f;

		public float appearDelay = 0.25f;

		public float dragDampTime = 0.2f;

		[HideInInspector]
		public Vector3 homePosition;

		[HideInInspector]
		public bool isAppearing;

		[Header("Drag Bounce Settings")]
		public float punchScaleAmount = 1.5f;

		public float punchScaleDelay = 0.1f;

		public float punchScaleTime = 0.5f;

		[Header("Drag Offset Settings")]
		public float dragOffsetTime = 0.2f;

		public float dragOffsetMultiplier = 1.25f;

		[Header("Celebration Animation")]
		public float celebrationJumpAmount = 1f;

		public float celebrationJumpFrequency = 5f;

		public float celebrationJumpOffset = 2f;

		[HideInInspector]
		public int celebrationJumpIndex;

		private bool m_allowDrag;

		private bool m_allowClick;

		private SecretWordGame m_game;

		private TileGrid m_tileGrid;

		private WordArea m_wordArea;

		private bool m_canGoHome = true;

		private FSM<TileStates> m_states = new FSM<TileStates>();

		private Vector3 m_inSlotVelocity = Vector3.zero;

		private Vector3 m_dragOffset;

		private bool m_canDisplaceTiles;

		private float m_randomCelebrationTimer;

		private bool m_shouldShowLoseHeartEffect;

		private Vector3 m_dragTarget;

		private Vector3 m_dragDampVelocity;

		private Transform m_transform;

		private TileSlot m_targetSlot;

		private float m_targetSlotTimer;

		public TileSlot currentSlot
		{
			get
			{
				return m_wordArea.GetSlotContainingTile(this);
			}
		}

		public TileSlot myCurrentSlot { get; set; }

		public bool IsInGrid()
		{
			return m_transform.parent == m_tileGrid.transform;
		}

		public bool IsInSlot()
		{
			return m_states.state == TileStates.IN_SLOT || myCurrentSlot != null;
		}

		private void Awake()
		{
			m_allowClick = true;
			m_states.AddState(TileStates.IDLE, null, null, null);
			m_states.AddState(TileStates.DRAGGING, Enter_Dragging, Update_Dragging, null);
			m_states.AddState(TileStates.SNAP_AFTER_DRAG, Enter_SnapAfterDrag, null, Exit_SnapAfterDrag);
			m_states.AddState(TileStates.IN_SLOT, Enter_InSlot, Update_InSlot, null);
			m_states.AddState(TileStates.FLY_TO_SLOT, Enter_FlyToSlot, Update_FlyToSlot, Exit_FlyToSlot);
			m_states.AddState(TileStates.EXPLODE, Enter_Explode, Update_Explode, null);
			m_states.AddState(TileStates.CELEBRATING, Enter_Celebrating, Update_Celebrating, null);
			m_states.state = TileStates.IDLE;
			m_states.Start();
			m_transform = base.transform;
			m_canDisplaceTiles = false;
		}

		private void Start()
		{
			if (m_game.GameState == SecretWordGameStates.CREATE)
			{
				m_allowDrag = true;
			}
		}

		private void OnEnable()
		{
			if (m_states.state == TileStates.DRAGGING)
			{
				OnEndDrag(null);
			}
			else if (m_states.state == TileStates.SNAP_AFTER_DRAG)
			{
				dropParticles.Play();
			}
			else if (m_states.state == TileStates.EXPLODE)
			{
				explodeParticles.Play();
			}
		}

		private void Update()
		{
			m_states.Update();
		}

		public void Setup(SecretWordGame game, TileGrid grid, WordArea area, char character)
		{
			m_game = game;
			m_tileGrid = grid;
			m_wordArea = area;
			letter.text = character.ToString().ToUpper();
			base.name = "Tile - " + character;
		}

		public void Explode(bool showLoseHeartEffect = false)
		{
			if (m_states.state == TileStates.FLY_TO_SLOT || m_states.state == TileStates.DRAGGING || m_states.state == TileStates.SNAP_AFTER_DRAG)
			{
				m_tileGrid.CreateTile(homePosition, letter.text[0], 0f);
			}
			m_states.state = TileStates.EXPLODE;
			m_shouldShowLoseHeartEffect = showLoseHeartEffect;
		}

		public Tween AppearInSlot(TileSlot aTarget)
		{
			SetState(TileStates.IN_SLOT);
			m_transform.localScale = Vector3.zero;
			Tween tween = m_transform.DOScale(Vector3.one, appearTime).SetEase(Ease.OutBack);
			tween.OnComplete(delegate
			{
				BaseGameController.Instance.Session.SessionSounds.PlaySoundEvent("SecretWord/SFX/TapLetterBlock");
			});
			myCurrentSlot = aTarget;
			return tween;
		}

		public void PunchScale()
		{
			m_allowClick = false;
			m_transform.DOPunchScale(new Vector3(punchScaleAmount, punchScaleAmount, punchScaleAmount), punchScaleTime, 5, 0f).OnComplete(delegate
			{
				m_allowClick = true;
			});
		}

		public void FlyToSlot(TileSlot aTarget)
		{
			m_allowClick = false;
			SetState(TileStates.FLY_TO_SLOT);
			myCurrentSlot = aTarget;
		}

		public void DropInPlace()
		{
			BaseGameController.Instance.Session.SessionSounds.PlaySoundEvent("SecretWord/SFX/DropLetter");
			offsetParent.transform.DOLocalMove(Vector3.zero, dragDropTime).SetEase(dragDropEasing);
			m_transform.DOLocalRotate(Vector3.zero, dragDropTime).SetEase(Ease.InOutCubic);
			m_transform.DOScale(Vector3.one, dragDropTime).SetEase(Ease.OutBack);
			DOVirtual.DelayedCall(dragDropTime * 0.4f, PlayDropParticles);
		}

		private void PlayDropParticles()
		{
			dropParticles.Stop();
			dropParticles.Play(true);
			m_allowClick = true;
		}

		public void SetState(TileStates state)
		{
			m_states.state = state;
		}

		private void Enter_Dragging()
		{
			offsetParent.transform.DOLocalMove(new Vector3(0f, dragRaiseAmount, 0f), dragLiftTime).SetEase(dragLiftEasing);
			m_transform.DOScale(new Vector3(dragScaleAmount, dragScaleAmount, dragScaleAmount), dragLiftTime).SetEase(Ease.OutBack);
		}

		private void Update_Dragging()
		{
			if (!m_canDisplaceTiles)
			{
				if (m_targetSlot != null && m_targetSlot != currentSlot && m_targetSlotTimer > 0f)
				{
					m_targetSlotTimer -= Time.deltaTime;
					if (m_targetSlotTimer <= 0f)
					{
						if (m_targetSlot.tile != null)
						{
							BaseGameController.Instance.Session.SessionSounds.PlaySoundEvent("SecretWord/SFX/LettersRearranged");
						}
						if (currentSlot != null && (float)Mathf.Abs(currentSlot.slotNumber - m_targetSlot.slotNumber) <= 1f)
						{
							m_wordArea.SwapTilesInSlots(m_targetSlot.slotNumber, currentSlot.slotNumber);
						}
						else
						{
							m_wordArea.InsertTileAtIndex(m_targetSlot.slotNumber, this);
						}
						m_canDisplaceTiles = true;
					}
				}
			}
			else if (currentSlot == null)
			{
				m_canDisplaceTiles = false;
				m_targetSlotTimer = m_wordArea.displacementTimeout;
			}
			base.transform.position = Vector3.SmoothDamp(base.transform.position, m_dragTarget, ref m_dragDampVelocity, dragDampTime);
		}

		private void Enter_SnapAfterDrag()
		{
			touchCollider.enabled = false;
			if (currentSlot != null && currentSlot.tile == this)
			{
				m_transform.DOMove(currentSlot.visual.transform.position, 0.5f).SetEase(Ease.InOutCubic).OnComplete(delegate
				{
					SetState(TileStates.IN_SLOT);
				});
				m_canDisplaceTiles = true;
				return;
			}
			m_canDisplaceTiles = false;
			if (m_canGoHome)
			{
				base.transform.parent = m_tileGrid.transform;
				m_transform.DOLocalMove(homePosition, 0.5f).SetEase(Ease.InOutCubic).OnComplete(delegate
				{
					SetState(TileStates.IDLE);
				});
			}
			else
			{
				m_states.state = TileStates.EXPLODE;
			}
		}

		private void Exit_SnapAfterDrag()
		{
			if (m_states.state != TileStates.EXPLODE)
			{
				DropInPlace();
				touchCollider.enabled = true;
			}
		}

		private void Enter_InSlot()
		{
			m_inSlotVelocity = Vector3.zero;
			if (m_canGoHome)
			{
				m_canGoHome = false;
				if (m_game.GameState == SecretWordGameStates.CREATE && !isAppearing)
				{
					m_tileGrid.CreateTile(homePosition, letter.text[0], 0f);
				}
			}
		}

		private void Update_InSlot()
		{
			if (m_wordArea.GetSlotContainingTile(this) != null)
			{
				base.transform.position = Vector3.SmoothDamp(m_transform.position, m_wordArea.GetSlotContainingTile(this).visual.transform.position, ref m_inSlotVelocity, 0.1f);
			}
		}

		private void Enter_FlyToSlot()
		{
			TileSlot slotContainingTile = m_wordArea.GetSlotContainingTile(this);
			touchCollider.enabled = false;
			if (slotContainingTile != null)
			{
				offsetParent.transform.DOLocalMove(new Vector3(0f, dragRaiseAmount, 0f), flyLiftTime).SetEase(dragLiftEasing);
				m_transform.DOMove(slotContainingTile.visual.transform.position, flyTravelTime).SetEase(Ease.InOutCubic);
				m_canDisplaceTiles = true;
			}
		}

		private void Update_FlyToSlot()
		{
			if (m_states.timeInCurrentState > flyTravelTime)
			{
				m_states.state = TileStates.IN_SLOT;
			}
		}

		private void Exit_FlyToSlot()
		{
			if (m_states.state != TileStates.EXPLODE)
			{
				DropInPlace();
				touchCollider.enabled = true;
			}
		}

		private void Enter_Explode()
		{
			BaseGameController.Instance.Session.SessionSounds.PlaySoundEvent("SecretWord/SFX/DeleteLetter");
			explodeParticles.Stop();
			explodeParticles.Play(true);
			visuals.SetActive(false);
			touchCollider.enabled = false;
			if (m_shouldShowLoseHeartEffect)
			{
				m_shouldShowLoseHeartEffect = false;
				loseHeartParticles.Play(true);
			}
			Sequence s = DOTween.Sequence();
			s.AppendInterval(0.2f);
			s.AppendCallback(ExplodeRemoveTileFromSlot);
		}

		private void Update_Explode()
		{
			if (!explodeParticles.IsAlive())
			{
				if (!m_shouldShowLoseHeartEffect)
				{
					Object.Destroy(base.gameObject);
				}
				else if (!loseHeartParticles.IsAlive())
				{
					Object.Destroy(base.gameObject);
				}
			}
		}

		private void ExplodeRemoveTileFromSlot()
		{
			TileSlot slotContainingTile = m_wordArea.GetSlotContainingTile(this);
			if (slotContainingTile != null)
			{
				m_wordArea.RemoveTileFromSlot(slotContainingTile.slotNumber);
			}
		}

		private void Enter_Celebrating()
		{
			m_randomCelebrationTimer = 0f;
			celebrateParticles.Play(true);
			touchCollider.enabled = false;
		}

		private void Update_Celebrating()
		{
			m_randomCelebrationTimer += Time.deltaTime;
			offsetParent.transform.localPosition = new Vector3(0f, (Mathf.Sin(m_randomCelebrationTimer * celebrationJumpFrequency + (float)celebrationJumpIndex * celebrationJumpOffset) + 1f) / 2f * celebrationJumpAmount, 0f);
		}

		public void OnBeginDrag(BaseEventData eventData)
		{
			if (!m_allowDrag || m_game.DisableInput)
			{
				PunchScale();
				return;
			}
			m_dragOffset = new Vector3(0f, 0f, 0.5f * dragScaleAmount);
			m_dragTarget = base.transform.position;
			m_states.state = TileStates.DRAGGING;
			BaseGameController.Instance.Session.SessionSounds.PlaySoundEvent("SecretWord/SFX/DragLetter");
		}

		public void OnEndDrag(BaseEventData eventData)
		{
			if (m_allowDrag && !m_game.DisableInput)
			{
				m_wordArea.EliminateGapsBetweenTiles();
				if (m_states.state == TileStates.DRAGGING)
				{
					m_states.state = TileStates.SNAP_AFTER_DRAG;
				}
			}
		}

		public void OnDrag(BaseEventData eventData)
		{
			if (!m_allowDrag || m_game.DisableInput)
			{
				return;
			}
			PointerEventData pointerEventData = eventData as PointerEventData;
			m_dragTarget = GetPositionOnDragPlane(pointerEventData.position) + m_dragOffset;
			Vector3 tileVisualPositionOnDragPlane = GetTileVisualPositionOnDragPlane();
			TileSlot closestSlotToPosition = m_wordArea.GetClosestSlotToPosition(tileVisualPositionOnDragPlane, this, m_canDisplaceTiles);
			if (!m_canDisplaceTiles)
			{
				TileSlot closestSlotToPosition2 = m_wordArea.GetClosestSlotToPosition(tileVisualPositionOnDragPlane, this, true);
				if (closestSlotToPosition != m_targetSlot)
				{
					m_targetSlotTimer = m_wordArea.displacementTimeout;
				}
				m_targetSlot = closestSlotToPosition2;
			}
			if (closestSlotToPosition != null)
			{
				if (closestSlotToPosition != currentSlot)
				{
					if (closestSlotToPosition.tile != null)
					{
						BaseGameController.Instance.Session.SessionSounds.PlaySoundEvent("SecretWord/SFX/LettersRearranged");
					}
					if (currentSlot != null)
					{
						m_wordArea.SwapTilesInSlots(closestSlotToPosition.slotNumber, currentSlot.slotNumber);
					}
					else
					{
						m_wordArea.InsertTileAtIndex(closestSlotToPosition.slotNumber, this);
					}
				}
			}
			else if (currentSlot != null)
			{
				m_wordArea.RemoveTileFromSlot(currentSlot.slotNumber);
				m_transform.parent = m_tileGrid.tileParent;
			}
		}

		public void OnClickDown(BaseEventData eventData)
		{
		}

		public void OnClick(BaseEventData eventData)
		{
			if (m_allowClick)
			{
				m_game.OnTileClicked(this);
			}
		}

		private Vector3 GetPositionOnDragPlane(Vector3 screenPoint)
		{
			Plane plane = new Plane(Vector3.up, m_transform.position);
			Ray ray = m_game.gameController.MixGameCamera.ScreenPointToRay(screenPoint);
			float enter;
			if (plane.Raycast(ray, out enter))
			{
				return ray.GetPoint(enter);
			}
			return Vector3.zero;
		}

		private Vector3 GetTileVisualPositionOnDragPlane()
		{
			Vector3 screenPoint = m_game.gameController.MixGameCamera.WorldToScreenPoint(visuals.transform.position);
			return GetPositionOnDragPlane(screenPoint);
		}

		private void OnDrawGizmos()
		{
			if (UnityEngine.Application.isPlaying)
			{
				Gizmos.color = Color.green;
				TileSlot bestTouchingSlot = colliderHelper.GetBestTouchingSlot();
				if (bestTouchingSlot != null)
				{
					BoxCollider component = bestTouchingSlot.GetComponent<BoxCollider>();
					Gizmos.DrawWireCube(component.transform.position, component.bounds.size);
				}
			}
		}
	}
}
