using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Mix.Games.Tray.Fireworks
{
	[RequireComponent(typeof(EventTrigger))]
	[RequireComponent(typeof(ScrollRect))]
	public class ScrollRectHorizontalSnap : MonoBehaviour
	{
		public float mSnapSpeed = 15f;

		public float mSnapThreshold = 0.0045f;

		public float mInertiaCutoffMagnitude = 200f;

		public Animator selectionPromptAnim;

		private int mNumberOfEntries;

		private float[] mSnapPositions;

		private ScrollRect mScrollRect;

		private float mCurrentSnapPosition;

		private int mCurrentSnapPositionIndex;

		private int mFinalSnapPositionIndex;

		private bool mIsSnapping;

		private bool mIsFirstSnap = true;

		private void Update()
		{
			if (mIsSnapping)
			{
				if (mIsFirstSnap)
				{
					HideSceneSelectPrompt();
					mIsFirstSnap = false;
				}
				mScrollRect.horizontalNormalizedPosition = Mathf.Lerp(mScrollRect.horizontalNormalizedPosition, mCurrentSnapPosition, mSnapSpeed * Time.deltaTime);
				if (Mathf.Abs(mScrollRect.horizontalNormalizedPosition - mCurrentSnapPosition) <= mSnapThreshold)
				{
					mScrollRect.horizontalNormalizedPosition = mCurrentSnapPosition;
					mIsSnapping = false;
					mScrollRect.content.gameObject.transform.GetChild(mFinalSnapPositionIndex).GetComponent<Scene>().SelectScene();
					mCurrentSnapPositionIndex = GetNearestSnapPositionIndex();
				}
			}
		}

		public void OnBeginDrag()
		{
			mCurrentSnapPositionIndex = GetNearestSnapPositionIndex();
		}

		public void OnDrag()
		{
			mIsSnapping = false;
		}

		public void OnEndDrag()
		{
			int num = GetNearestSnapPositionIndex();
			if (num == mCurrentSnapPositionIndex && mScrollRect.velocity.sqrMagnitude > mInertiaCutoffMagnitude * mInertiaCutoffMagnitude)
			{
				if (mScrollRect.velocity.x < 0f)
				{
					num = mCurrentSnapPositionIndex + 1;
				}
				else if (mScrollRect.velocity.x > 1f)
				{
					num = mCurrentSnapPositionIndex - 1;
				}
				num = Mathf.Clamp(num, 0, mSnapPositions.Length - 1);
			}
			mCurrentSnapPosition = mSnapPositions[num];
			mFinalSnapPositionIndex = num;
			mIsSnapping = true;
		}

		private int GetNearestSnapPositionIndex()
		{
			float horizontalNormalizedPosition = mScrollRect.horizontalNormalizedPosition;
			float num = float.PositiveInfinity;
			int result = 0;
			for (int i = 0; i < mSnapPositions.Length; i++)
			{
				if (Mathf.Abs(mSnapPositions[i] - horizontalNormalizedPosition) < num)
				{
					num = Mathf.Abs(mSnapPositions[i] - horizontalNormalizedPosition);
					result = i;
				}
			}
			return result;
		}

		public void Initialize()
		{
			mScrollRect = base.gameObject.GetComponent<ScrollRect>();
			mScrollRect.inertia = true;
			mNumberOfEntries = mScrollRect.content.gameObject.transform.childCount;
			Scene[] componentsInChildren = mScrollRect.content.gameObject.transform.GetComponentsInChildren<Scene>();
			for (int i = 0; i < mNumberOfEntries; i++)
			{
				componentsInChildren[i].UIindex = i;
			}
			if (mNumberOfEntries > 0)
			{
				mSnapPositions = new float[mNumberOfEntries];
				float num = 1f / (float)(mNumberOfEntries - 1);
				for (int j = 0; j < mNumberOfEntries; j++)
				{
					mSnapPositions[j] = (float)j * num;
				}
			}
			else
			{
				mSnapPositions[0] = 0f;
			}
		}

		public void JumpToScene(Scene scene)
		{
			if (mScrollRect != null && scene != null)
			{
				mScrollRect.horizontalNormalizedPosition = mSnapPositions[scene.UIindex];
				mCurrentSnapPositionIndex = GetNearestSnapPositionIndex();
			}
		}

		public void SnapToNextScene(bool isSnapLeft)
		{
			if (isSnapLeft)
			{
				if (mCurrentSnapPositionIndex > 0)
				{
					mCurrentSnapPosition = mSnapPositions[mCurrentSnapPositionIndex - 1];
					mFinalSnapPositionIndex = mCurrentSnapPositionIndex - 1;
					mIsSnapping = true;
				}
			}
			else if (mCurrentSnapPositionIndex < mSnapPositions.Length - 1)
			{
				mCurrentSnapPosition = mSnapPositions[mCurrentSnapPositionIndex + 1];
				mFinalSnapPositionIndex = mCurrentSnapPositionIndex + 1;
				mIsSnapping = true;
			}
		}

		public bool HasLeftSelection(Scene scene)
		{
			bool result = true;
			if (scene != null)
			{
				result = scene.UIindex != 0;
			}
			return result;
		}

		public bool HasRightSelection(Scene scene)
		{
			bool result = true;
			if (scene != null && mSnapPositions != null)
			{
				result = scene.UIindex != mSnapPositions.Length - 1;
			}
			return result;
		}

		private void HideSceneSelectPrompt()
		{
			if (selectionPromptAnim != null)
			{
				selectionPromptAnim.Play("PromptExitAnimation");
			}
		}
	}
}
