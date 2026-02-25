using UnityEngine;
using UnityEngine.EventSystems;

namespace Mix.Games.Tray.InputHandler
{
	public class PanWithInertiaController : MonoBehaviour, IEventSystemHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IInitializePotentialDragHandler
	{
		protected const float FOCUS_ERR_DIST_SQ = 0.016f;

		public Transform Mover;

		public Vector3 Extents;

		public Vector3 Padding;

		public float SpringConstant = 30f;

		[Range(0f, 1f)]
		public float VelocityDecay = 0.07f;

		[Range(0f, 1f)]
		public float DragSpeedMultiplier = 0.1f;

		public string scrollSfx = "WouldYouRather/SFX/SlideCardList";

		public bool UseTouchKit;

		protected float mDampingConstant;

		protected Vector2 mScaledDeltaPos;

		protected Vector2 mVelocity;

		protected Vector2 mAccel;

		protected Vector3 mCenterPos;

		protected Transform mFocusTarget;

		protected Bounds mDraggingBounds;

		protected bool mIsDragging;

		public bool AllowDragging { get; set; }

		public void FocusOn(Transform aTarget)
		{
			mAccel = Vector2.zero;
			mVelocity = Vector2.zero;
			mFocusTarget = aTarget;
		}

		public void ResetCamera()
		{
			mAccel = Vector2.zero;
			mVelocity = Vector2.zero;
			mScaledDeltaPos = Vector2.zero;
			Mover.position = mCenterPos;
		}

		public void ResizeController(Vector2 aExtents, Vector2 aPadding)
		{
			Extents = aExtents;
			Padding = aPadding;
			mDraggingBounds = new Bounds(mCenterPos, Extents);
		}

		public void ResizeControllerX(float aWidth, float aPadding)
		{
			Extents.x = aWidth;
			Padding.x = aPadding;
			mDraggingBounds = new Bounds(mCenterPos, Extents);
		}

		protected void Start()
		{
			mCenterPos = Mover.position;
			mDampingConstant = 2f * Mathf.Sqrt(SpringConstant);
			ResizeController(Extents, Padding);
			if (UseTouchKit)
			{
				InitTouchKit();
			}
			else
			{
				InitUnityEvents();
			}
			AllowDragging = true;
		}

		protected void UpdateSpringKinematics(Vector2 aDisplacement)
		{
			mAccel = (0f - SpringConstant) * aDisplacement - mDampingConstant * mVelocity;
			mVelocity += mAccel * Time.deltaTime;
			if (mVelocity.sqrMagnitude < 0.01f)
			{
				mVelocity = Vector2.zero;
			}
		}

		protected void Update()
		{
			Vector3 vector = Vector3.zero;
			if (mIsDragging)
			{
				if (!mDraggingBounds.Contains(Mover.position))
				{
					Vector3 vector2 = mDraggingBounds.ClosestPoint(Mover.position);
					Vector3 vector3 = Mover.position - vector2;
					if (Padding.x > 0f)
					{
						vector3.x = Mathf.Clamp(Mathf.Abs(vector3.x), 0f, Padding.x);
						mScaledDeltaPos.x *= 1f - vector3.x / Padding.x;
					}
					else
					{
						mScaledDeltaPos.x = 0f;
					}
					if (Padding.y > 0f)
					{
						vector3.y = Mathf.Clamp(Mathf.Abs(vector3.y), 0f, Padding.y);
						mScaledDeltaPos.y *= 1f - vector3.y / Padding.y;
					}
					else
					{
						mScaledDeltaPos.y = 0f;
					}
				}
				vector = mScaledDeltaPos;
			}
			else if (mFocusTarget != null)
			{
				Vector2 aVector = Mover.position - mFocusTarget.position;
				FilterVectorByExtents(ref aVector);
				if (aVector.sqrMagnitude > 0.016f)
				{
					UpdateSpringKinematics(aVector);
					vector = mVelocity * Time.deltaTime;
				}
				else
				{
					mFocusTarget = null;
				}
			}
			else
			{
				if (mDraggingBounds.Contains(Mover.position))
				{
					mVelocity *= Mathf.Pow(VelocityDecay, Time.deltaTime);
				}
				else
				{
					Vector3 vector4 = mDraggingBounds.ClosestPoint(Mover.position);
					UpdateSpringKinematics(Mover.position - vector4);
				}
				vector = mVelocity * Time.deltaTime;
			}
			Mover.position += vector;
		}

		protected void FilterVectorByExtents(ref Vector2 aVector)
		{
			if (Extents.x == 0f)
			{
				aVector.x = 0f;
			}
			if (Extents.y == 0f)
			{
				aVector.y = 0f;
			}
		}

		protected void InitUnityEvents()
		{
			if (base.gameObject.GetComponent<EmptyGraphic>() == null)
			{
				base.gameObject.AddComponent<EmptyGraphic>();
			}
		}

		public void OnInitializePotentialDrag(PointerEventData data)
		{
			_OnInitializePotentialDrag();
		}

		public void OnBeginDrag(PointerEventData data)
		{
			_OnBeginDrag(data.position);
		}

		public void OnEndDrag(PointerEventData data)
		{
			_OnEndDrag();
		}

		public void OnDrag(PointerEventData data)
		{
			_OnDrag(data.delta);
		}

		protected void InitTouchKit()
		{
			TKPanRecognizer tKPanRecognizer = new TKPanRecognizer();
			tKPanRecognizer.gestureRecognizedEvent += OnDrag;
			tKPanRecognizer.gestureCompleteEvent += OnEndDrag;
			TouchKit.addGestureRecognizer(tKPanRecognizer);
		}

		protected void OnDrag(TKPanRecognizer aGesture)
		{
			if (AllowDragging)
			{
				if (mIsDragging)
				{
					_OnDrag(aGesture.deltaTranslation);
				}
				else
				{
					_OnBeginDrag(aGesture.touchLocation());
				}
			}
		}

		protected void OnEndDrag(TKPanRecognizer aGesture)
		{
			_OnEndDrag();
		}

		protected void _OnInitializePotentialDrag()
		{
			if (AllowDragging)
			{
				mVelocity = Vector2.zero;
				mAccel = Vector2.zero;
			}
		}

		protected void _OnBeginDrag(Vector2 aStartPos)
		{
			if (AllowDragging)
			{
				mVelocity = Vector2.zero;
				mAccel = Vector2.zero;
				mIsDragging = true;
			}
		}

		protected void _OnEndDrag()
		{
			mIsDragging = false;
		}

		protected void _OnDrag(Vector2 aDragDelta)
		{
			if (AllowDragging)
			{
				if (Time.deltaTime > 0f)
				{
					mScaledDeltaPos = (0f - DragSpeedMultiplier) * aDragDelta;
					FilterVectorByExtents(ref mScaledDeltaPos);
					mVelocity = mScaledDeltaPos / Time.deltaTime;
				}
				else
				{
					mVelocity = Vector2.zero;
				}
			}
		}

		protected void FilterVectorByExtents(ref Vector3 aVector)
		{
			if (Extents.x == 0f)
			{
				aVector.x = 0f;
			}
			if (Extents.y == 0f)
			{
				aVector.y = 0f;
			}
		}
	}
}
