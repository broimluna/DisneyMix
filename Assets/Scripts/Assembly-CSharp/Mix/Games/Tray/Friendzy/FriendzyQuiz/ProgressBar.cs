using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.Friendzy.FriendzyQuiz
{
	[RequireComponent(typeof(CanvasGroup))]
	[RequireComponent(typeof(Slider))]
	public class ProgressBar : MonoBehaviour
	{
		private const int INITIAL_NUM_IN_POOL = 6;

		private const int PROGRESS_BAR_LENGTH = 450;

		private const string mFinalProgressBarSound = "Progress5/Complete";

		private Slider mSlider;

		private CanvasGroup mCanvasGroup;

		private float mIncrement;

		[HideInInspector]
		public float SegmentSize;

		public Image DividerPrefab;

		public List<Image> Dividers = new List<Image>(6);

		private int mNumDividers;

		private float[] mDividerPositions;

		public Image Border;

		public Image FillImage;

		public Image Background;

		public bool HasBounciness;

		public Transform StartOfBar;

		public Transform EndOfBar;

		private string[] mProgressBarSounds = new string[4] { "Progress1", "Progress2", "Progress3", "Progress4" };

		private float mRatioOfSoundsToBars;

		public int CurrentDividerPosIndex { get; set; }

		private void Awake()
		{
			mSlider = GetComponent<Slider>();
			mCanvasGroup = GetComponent<CanvasGroup>();
			CurrentDividerPosIndex = -1;
		}

		public void InitializeDividerPool()
		{
			for (int i = 0; i < 6; i++)
			{
				SpawnDivider();
			}
		}

		public void SetNumQuestions(int aNumQuestions)
		{
			mNumDividers = aNumQuestions - 1;
			mRatioOfSoundsToBars = (float)mNumDividers / (float)mProgressBarSounds.Length;
			while (mNumDividers > Dividers.Count)
			{
				SpawnDivider();
			}
			mIncrement = 1f / (float)aNumQuestions;
			SegmentSize = (EndOfBar.position.x - StartOfBar.position.x) / (float)aNumQuestions;
			mDividerPositions = new float[aNumQuestions];
			for (int i = 0; i < mNumDividers; i++)
			{
				mDividerPositions[i] = mIncrement * (float)(i + 1);
				Dividers[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(mDividerPositions[i] * 450f, 0f);
				Dividers[i].gameObject.SetActive(true);
			}
			mDividerPositions[mNumDividers] = 1f;
		}

		protected void SpawnDivider()
		{
			Image image = Object.Instantiate(DividerPrefab);
			image.transform.SetParent(base.transform);
			image.transform.localScale = base.transform.localScale;
			Dividers.Add(image);
			image.gameObject.SetActive(false);
		}

		public void SetIPColors(Color aMainColor, Color aAccentColor, Color aBackgroundColor)
		{
			Border.color = aMainColor;
			FillImage.color = aAccentColor;
			for (int i = 0; i < Dividers.Count; i++)
			{
				Dividers[i].color = aMainColor;
			}
			Background.color = aBackgroundColor;
		}

		public Sequence FlashColor(Color aColor)
		{
			float duration = 0.5f;
			Sequence sequence = DOTween.Sequence();
			Color color = FillImage.color;
			sequence.Append(FillImage.DOColor(aColor, duration));
			sequence.Append(FillImage.DOColor(color, duration));
			return sequence;
		}

		public Sequence ProgressToQuestion(int aQuestionIndex)
		{
			CurrentDividerPosIndex = aQuestionIndex - 1;
			float aDuration = 0.8f;
			float aDuration2 = 0.125f;
			float num = mIncrement / 3f;
			Sequence sequence = DOTween.Sequence();
			if (HasBounciness)
			{
				sequence.Append(TweenLerpValue(mDividerPositions[CurrentDividerPosIndex] + num, aDuration2));
			}
			else
			{
				aDuration = 0.1f;
			}
			sequence.Append(TweenLerpValue(mDividerPositions[CurrentDividerPosIndex], aDuration));
			if (aQuestionIndex > mNumDividers)
			{
				sequence.PrependCallback(delegate
				{
					FriendzyGame.PlaySound(mProgressBarSounds[mProgressBarSounds.Length - 1], FriendzyGame.SOUND_PREFIX);
				});
				sequence.AppendCallback(delegate
				{
					FriendzyGame.PlaySound("Progress5/Complete", FriendzyGame.SOUND_PREFIX);
				});
				sequence.Append(base.transform.DOPunchScale(Vector3.one * 0.3f, 0.5f, 0, 0.5f));
			}
			else
			{
				sequence.PrependCallback(delegate
				{
					FriendzyGame.PlaySound(mProgressBarSounds[(int)((float)(aQuestionIndex - 1) / mRatioOfSoundsToBars)], FriendzyGame.SOUND_PREFIX);
				});
			}
			return sequence;
		}

		private Tween TweenLerpValue(float aLerpPosition, float aDuration)
		{
			return DOTween.To(() => mSlider.value, delegate(float x)
			{
				mSlider.value = x;
			}, aLerpPosition, aDuration);
		}

		public Tween FadeIn()
		{
			return mCanvasGroup.DOFade(1f, 1.5f);
		}

		public Tween FadeOut()
		{
			return mCanvasGroup.DOFade(0f, 0.5f);
		}
	}
}
