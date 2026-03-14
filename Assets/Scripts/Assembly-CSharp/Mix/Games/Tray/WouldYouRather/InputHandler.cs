using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mix.Games.Tray.WouldYouRather
{
	public class InputHandler : MonoBehaviour, IEventSystemHandler, IDragHandler, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IInitializePotentialDragHandler
	{
		private const float FOCUS_ERR_DIST = 0.4f;

		private const float GIZMO_RADIUS = 0.5f;

		public LineSlider lineSliderObject;

		public WouldYouRatherGame game;

		public float velocityDecay;

		public GameObject dragSliderShuttle;

		[Range(0f, 360f)]
		public float screenDragAngle;

		[Range(0f, 1f)]
		public float dragSpeedMultiplier = 0.1f;

		public bool isCameraDraggingEnabled = true;

		public bool isCardTappingEnabled = true;

		public float springConstant = 30f;

		public float overflowWidth = 1f;

		private float dampingConstant;

		private float mLinVelocity;

		private float mLinAccel;

		private float mDragLinDelta;

		private float mCurOffset;

		private Vector2 mDragLastPos;

		private Vector3 mCenterPos;

		private float mLineHalfWidth;

		private float mMaxLineHalfWidth;

		private bool mIsDragging;

		private List<float> mDragSegmentLengths;

		private float mTotalDragLineLength;

		private Transform mFocusTarget;

		private Vector2 screenDragVector
		{
			get
			{
				return Quaternion.AngleAxis(screenDragAngle, Vector3.back) * Vector3.right;
			}
		}

		private void Awake()
		{
			mDragSegmentLengths = new List<float>();
			mTotalDragLineLength = 0f;
			for (int i = 0; i < lineSliderObject.linePoints.Count - 1; i++)
			{
				float num = Vector3.Distance(lineSliderObject.GetPointAtIndex(i), lineSliderObject.GetPointAtIndex(i + 1));
				mDragSegmentLengths.Add(num);
				mTotalDragLineLength += num;
			}
			mLineHalfWidth = mTotalDragLineLength * 0.5f;
			mMaxLineHalfWidth = mLineHalfWidth + overflowWidth;
			mCenterPos = lineSliderObject.transform.localPosition;
			dampingConstant = 2f * Mathf.Sqrt(springConstant);
		}

		private void Start()
		{
			mIsDragging = false;
			mDragLinDelta = 0f;
			ResetGameplayCamera();
		}

		private float ModulateDragOffset(float rawOffset)
		{
			float num = Mathf.Abs(rawOffset);
			if (num < mLineHalfWidth)
			{
				return rawOffset;
			}
			return Mathf.Clamp(rawOffset, 0f - mMaxLineHalfWidth, mMaxLineHalfWidth);
		}

		private void UpdateSpringKinematics(float curDisplacement)
		{
			mLinAccel = (0f - springConstant) * curDisplacement - dampingConstant * mLinVelocity;
			mLinVelocity += mLinAccel * Time.deltaTime;
			if (Mathf.Abs(mLinVelocity) < 0.1f)
			{
				mLinVelocity = 0f;
			}
		}

		private void Update()
		{
			if (isCameraDraggingEnabled)
			{
				if (mIsDragging)
				{
					if (mCurOffset > mLineHalfWidth)
					{
						mDragLinDelta *= 1f - (mCurOffset - mLineHalfWidth) / overflowWidth;
					}
					else if (mCurOffset < 0f - mLineHalfWidth)
					{
						mDragLinDelta *= 1f - (mCurOffset + mLineHalfWidth) / overflowWidth;
					}
					mCurOffset = Mathf.Lerp(mCurOffset, mCurOffset + mDragLinDelta, 0.75f);
					mCurOffset = Mathf.Clamp(mCurOffset, 0f - mMaxLineHalfWidth, mMaxLineHalfWidth);
				}
				else if (mFocusTarget != null)
				{
					Vector3 lhs = dragSliderShuttle.transform.position - mFocusTarget.position;
					float num = Vector3.Dot(lhs, Vector3.right);
					if (Mathf.Abs(num) > 0.4f)
					{
						UpdateSpringKinematics(num);
						mCurOffset += mLinVelocity * Time.deltaTime;
					}
					else
					{
						mFocusTarget = null;
					}
				}
				else
				{
					if (mCurOffset > mLineHalfWidth)
					{
						UpdateSpringKinematics(mCurOffset - mLineHalfWidth);
					}
					else if (mCurOffset < 0f - mLineHalfWidth)
					{
						UpdateSpringKinematics(mCurOffset + mLineHalfWidth);
					}
					else
					{
						mLinVelocity *= Mathf.Pow(velocityDecay, Time.deltaTime);
					}
					mCurOffset += mLinVelocity * Time.deltaTime;
				}
				dragSliderShuttle.transform.localPosition = mCenterPos + mCurOffset * Vector3.right;
			}
			else
			{
				mLinVelocity = 0f;
				mLinAccel = 0f;
			}
		}

		public void OnBeginDrag(PointerEventData data)
		{
			if (isCameraDraggingEnabled)
			{
				BaseGameController.Instance.Session.SessionSounds.PlaySoundEvent("WouldYouRather/SFX/SlideCardList");
				mIsDragging = true;
				mDragLastPos = data.position;
			}
		}

		public void OnEndDrag(PointerEventData data)
		{
			BaseGameController.Instance.Session.SessionSounds.StopSoundEvent("WouldYouRather/SFX/SlideCardList");
			mIsDragging = false;
		}

		public void OnInitializePotentialDrag(PointerEventData data)
		{
			if (isCameraDraggingEnabled)
			{
				mLinVelocity = 0f;
				mLinAccel = 0f;
			}
		}

		public void OnDrag(PointerEventData data)
		{
			if (isCameraDraggingEnabled)
			{
				Vector2 lhs = data.position - mDragLastPos;
				mDragLastPos = data.position;
				if (Time.deltaTime > 0f)
				{
					mDragLinDelta = Vector2.Dot(lhs, screenDragVector) * (0f - dragSpeedMultiplier);
					mLinVelocity = mDragLinDelta / Time.deltaTime;
				}
				else
				{
					mLinVelocity = 0f;
				}
			}
		}

		public void OnPointerClick(PointerEventData data)
		{
			if (!isCardTappingEnabled || data.dragging)
			{
				return;
			}
			Ray ray = game.GameController.MixGameCamera.ScreenPointToRay(data.position);
			RaycastHit[] array = Physics.RaycastAll(ray, 10f);
			for (int i = 0; i < array.Length; i++)
			{
				WouldYouRatherCard component = array[i].collider.GetComponent<WouldYouRatherCard>();
				if (!component)
				{
					continue;
				}
				if (game.IsMakingFinalDecision())
				{
					if (!game.IsFirstPlayer)
					{
						game.selectedResponse = component.responseChoice;
						game.FinalDecisionMade();
					}
					continue;
				}
				component.ToggleCard();
				if (game.ShouldGameplayCameraFocusOnCard())
				{
					mLinAccel = 0f;
					mLinVelocity = 0f;
					mFocusTarget = component.transform;
				}
				break;
			}
		}

		public void ResetGameplayCamera()
		{
			mLinAccel = 0f;
			mLinVelocity = 0f;
			mDragLinDelta = 0f;
			mCurOffset = 0f;
			dragSliderShuttle.transform.localPosition = mCenterPos;
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.white;
			Gizmos.DrawSphere(mCenterPos, 0.5f);
			Gizmos.color = Color.yellow;
			Gizmos.DrawSphere(mCenterPos + mLineHalfWidth * Vector3.right, 0.5f);
			Gizmos.DrawSphere(mCenterPos - mLineHalfWidth * Vector3.right, 0.5f);
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(mCenterPos + mMaxLineHalfWidth * Vector3.right, 0.5f);
			Gizmos.DrawSphere(mCenterPos - mMaxLineHalfWidth * Vector3.right, 0.5f);
		}
	}
}
