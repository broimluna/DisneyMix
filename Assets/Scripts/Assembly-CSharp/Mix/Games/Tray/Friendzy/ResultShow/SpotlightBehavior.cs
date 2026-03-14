using DG.Tweening;
using UnityEngine;

namespace Mix.Games.Tray.Friendzy.ResultShow
{
	public class SpotlightBehavior : MonoBehaviour
	{
		private const float FAKEOUT_DURATION = 0.15f;

		private const float FAKEOUT_DELAY = 0.4f;

		private const float LOCAL_DISPLACEMENT = -0.18175f;

		private const float FOLLOW_RATE = 9f;

		private const float FINAL_SCALE_FACTOR = 5f;

		public Transform[] RightTransforms;

		public Transform[] LeftTransforms;

		public Transform OriginTransform;

		private Vector3[] WayPointsRight;

		private Vector3[] WayPointsLeft;

		private Vector3[] TargetWayPoints;

		private LightingAnimator mResults;

		[SerializeField]
		private Transform mResultsParent;

		public bool IncludeFakeOut;

		private bool mSequenceDone;

		public int NumToHighlight;

		private Transform mTransformForLerp;

		private Vector3 mFinalLocalPos = new Vector3(0f, -0.05f, -0.1875f);

		private Color TRANSPARENT_BLACK = new Color(0f, 0f, 0f, 0f);

		private void Start()
		{
			mSequenceDone = false;
			base.transform.localPosition = OriginTransform.localPosition;
		}

		private void OnEnable()
		{
			SetTransformToFollow();
		}

		private void SetTransformToFollow()
		{
			if (mTransformForLerp == null && mResults != null)
			{
				GameObject gameObject = new GameObject();
				gameObject.name = "SpotlightLerpPoint";
				mTransformForLerp = gameObject.transform;
				mTransformForLerp.SetParent(mResultsParent);
				mTransformForLerp.localPosition = OriginTransform.localPosition;
			}
		}

		public void Initialize(LightingAnimator aLightManager)
		{
			mResults = aLightManager;
			base.transform.localPosition = OriginTransform.localPosition;
			InitializeWayPoints();
			SetTransformToFollow();
			base.gameObject.SetActive(false);
		}

		private void InitializeWayPoints()
		{
			WayPointsRight = CopyWaypointsFromTransforms(RightTransforms);
			WayPointsLeft = CopyWaypointsFromTransforms(LeftTransforms);
			TargetWayPoints = WayPointsLeft;
		}

		private Vector3[] CopyWaypointsFromTransforms(Transform[] aTransforms)
		{
			Vector3[] array = new Vector3[aTransforms.Length];
			for (int i = 0; i < aTransforms.Length; i++)
			{
				array[i] = aTransforms[i].localPosition;
			}
			return array;
		}

		public Sequence SelectionSequence(float aDelay, float aDuration)
		{
			mTransformForLerp.localPosition = OriginTransform.localPosition;
			Sequence sequence = DOTween.Sequence();
			sequence.AppendInterval(aDelay);
			float duration = aDuration / (float)NumToHighlight;
			for (int i = 0; i < NumToHighlight; i++)
			{
				TargetWayPoints = ((TargetWayPoints != WayPointsRight) ? WayPointsRight : WayPointsLeft);
				Vector3 vector = ((i <= NumToHighlight - 2) ? ((i != NumToHighlight - 1 || IncludeFakeOut) ? mResults.GetRandomResult().transform.localPosition : mResults.GetChosenResult().transform.localPosition) : mResults.GetRandomResult(true).transform.localPosition);
				TargetWayPoints[TargetWayPoints.Length - 1] = new Vector3(vector.x, vector.y, vector.z + -0.18175f);
				Tween t = mTransformForLerp.DOLocalPath(TargetWayPoints, duration, PathType.CatmullRom, PathMode.Sidescroller2D, 20, Color.red);
				sequence.Append(t);
			}
			if (IncludeFakeOut)
			{
				sequence.AppendInterval(0.4f);
				Vector3 localPosition = mResults.GetChosenResult().transform.localPosition;
				Tween t2 = ShortcutExtensions.DOLocalMove(endValue: new Vector3(localPosition.x, localPosition.y, localPosition.z + -0.18175f), target: mTransformForLerp, duration: 0.15f);
				sequence.Append(t2);
			}
			sequence.AppendCallback(delegate
			{
				FriendzyGame.PlaySound("ResultPositive", FriendzyGame.SOUND_PREFIX);
			});
			sequence.AppendInterval(0.4f);
			return sequence;
		}

		public Sequence FinalSelectionMove(float aSelectionDuration, float aLightDimDuration)
		{
			Sequence s = DOTween.Sequence();
			SpriteRenderer component = GetComponent<SpriteRenderer>();
			Tween t = mTransformForLerp.DOLocalMove(mFinalLocalPos, aSelectionDuration).OnComplete(delegate
			{
				mSequenceDone = true;
			});
			s.Append(t);
			Tween t2 = base.transform.DOScale(base.transform.lossyScale.x * 5f, aLightDimDuration);
			s.Join(t2);
			Tween t3 = component.DOColor(TRANSPARENT_BLACK, aLightDimDuration);
			return s.Join(t3);
		}

		private void Update()
		{
			if (!mSequenceDone)
			{
				base.transform.position = Vector3.Lerp(base.transform.position, mTransformForLerp.position, Time.deltaTime * 9f);
			}
		}
	}
}
