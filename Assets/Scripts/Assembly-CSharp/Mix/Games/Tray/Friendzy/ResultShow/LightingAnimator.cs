using System.Collections.Generic;
using DG.Tweening;
using Mix.Games.Tray.Common;
using Mix.Games.Tray.Friendzy.ResultAnimator;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.Friendzy.ResultShow
{
	public class LightingAnimator : MonoBehaviour
	{
		private const float RESULT_CASCADE_RATE = 0.15f;

		private const float ROW_SPACING = 10f;

		private const float COL_SPACING = 20f;

		private const float IN_BETWEEN_TIME = 1f;

		private const float FLASHING_TIME = 1f;

		private const float DARKEN_AND_ROTATE = 0.5f;

		private const float SHOW_SPRITE_DURATION = 0.125f;

		private const float LIGHT_DIM_DURATION = 1f;

		private const float SPOTLIGHT_DELAY = 0.5f;

		private const float SPOTLIGHT_DURATION = 4f;

		private const float SPOTLIGHT_SCALE_FACTOR = 1f;

		private const float SELECTION_DURATION = 0.5f;

		private const float RESULT_LINGER_DURATION = 1.5f;

		private const int INITIAL_SIZE_OF_LIST = 6;

		public GameObject ResultPrefab;

		private List<ResultCardBehavior> mResultCards;

		private List<ResultCardBehavior> mIncorrectResultCards;

		private ResultCardBehavior mChosenResult;

		private ResultsAnimator mResultAnim;

		public Transform Row1PosReference;

		public Transform Row2PosReference;

		public Transform PosReference;

		public Transform ResultsParent;

		private Vector2 RESULT_SPACE = new Vector2(600f, 450f);

		private Vector2 mTargetWorldSize;

		public int NumOfResults;

		public bool UseSameNumAsResults = true;

		private int mRowBreakpoint = 3;

		private Color TRANSPARENT_BLACK = new Color(0f, 0f, 0f, 0f);

		private Color FADED_BLACK = new Color(0f, 0f, 0f, 0.722656f);

		public Image ResultsFader;

		public SpotlightBehavior SpotlightVignette;

		public SpriteRenderer SpotlightAvatar;

		public bool SequenceStarted { get; set; }

		public bool SequenceEnded { get; set; }

		public bool SequenceSkippable { get; set; }

		private void Awake()
		{
			Initialize();
		}

		private void Initialize()
		{
			InitializeLights();
			mResultCards = new List<ResultCardBehavior>(6);
			mIncorrectResultCards = new List<ResultCardBehavior>(6);
			mTargetWorldSize = new Vector2(1f, 1f);
			Vector2 size = ResultsParent.parent.GetComponent<RectTransform>().rect.size;
			RESULT_SPACE = size;
		}

		public void SetResultAnimator(ResultsAnimator aResultAnim)
		{
			mResultAnim = aResultAnim;
		}

		public void UpdateLayout(bool aImmediate = false)
		{
			NumOfResults = mResultCards.Count;
			bool flag = NumOfResults > mRowBreakpoint;
			if (flag)
			{
				int num = NumOfResults / 2;
				int num2 = NumOfResults - num;
				mTargetWorldSize = CalculateTargetWorldSize(flag, num2);
				UpdatePlacementAndScale(aImmediate, num2, Row1PosReference.localPosition, 0, num2);
				UpdatePlacementAndScale(aImmediate, num, Row2PosReference.localPosition, num2, mResultCards.Count);
			}
			else
			{
				mTargetWorldSize = CalculateTargetWorldSize(flag, NumOfResults);
				UpdatePlacementAndScale(aImmediate, NumOfResults, PosReference.localPosition, 0, mResultCards.Count);
			}
			ResizeSpotlight();
		}

		private Vector2 CalculateTargetWorldSize(bool aMoreThanBreakPoint, int aNumInRow)
		{
			float num = RESULT_SPACE.x / (float)aNumInRow - 20f * (float)(aNumInRow - 1);
			float num2 = ((!aMoreThanBreakPoint) ? RESULT_SPACE.y : (RESULT_SPACE.y / 2f - 10f));
			if (num > num2)
			{
				num = num2;
			}
			return new Vector2(num, num);
		}

		private void UpdatePlacementAndScale(bool aSetImmediate, int aNumInRow, Vector3 aPos, int aStartIndex, int aEndIndex)
		{
			float num = mTargetWorldSize.x * (float)aNumInRow + 20f * (float)(aNumInRow - 1);
			float num2 = num / 2f;
			float num3 = mTargetWorldSize.x / 2f;
			float num4 = base.transform.localPosition.x - num2 + num3;
			for (int i = aStartIndex; i < aEndIndex; i++)
			{
				ResultCardBehavior resultCardBehavior = mResultCards[i];
				resultCardBehavior.SetLerpPoint(new Vector3(num4, aPos.y, aPos.z), aSetImmediate);
				num4 += mTargetWorldSize.x + 20f;
				resultCardBehavior.ResetTargetScale(mTargetWorldSize, aSetImmediate);
			}
		}

		public void SetChosenResult(Sprite aChosenImage)
		{
			int num = Random.Range(0, mResultCards.Count);
			Sprite aOtherSprite = SetChosenSprite(num, aChosenImage);
			SwapSprites(aOtherSprite, num);
			mIncorrectResultCards.Remove(mChosenResult);
		}

		private Sprite SetChosenSprite(int aRandom, Sprite aChosenSprite)
		{
			mChosenResult = null;
			for (int i = 0; i < mResultCards.Count; i++)
			{
				if (mResultCards[i].ResultSprite.name.CompareTo(aChosenSprite.name) == 0)
				{
					mChosenResult = mResultCards[i];
					break;
				}
			}
			if (mChosenResult == null)
			{
				mChosenResult = mResultCards[aRandom];
			}
			Sprite resultSprite = mChosenResult.ResultSprite;
			mChosenResult.ResultSprite = aChosenSprite;
			mChosenResult.name = "Chosen" + mChosenResult.name;
			mChosenResult.ResetTargetScale(mTargetWorldSize, true);
			return resultSprite;
		}

		public void SetResultSprites(Sprite[] aSprites)
		{
			if (NumOfResults < aSprites.Length || UseSameNumAsResults)
			{
				NumOfResults = aSprites.Length;
			}
			List<int> list = new List<int>();
			for (int i = 0; i < NumOfResults; i++)
			{
				list.Add(i);
			}
			list.Shuffle();
			for (int j = 0; j < NumOfResults; j++)
			{
				GameObject gameObject = Object.Instantiate(ResultPrefab, base.transform.position, base.transform.rotation) as GameObject;
				gameObject.name = ResultPrefab.name;
				gameObject.transform.SetParent(ResultsParent, false);
				ResultCardBehavior component = gameObject.GetComponent<ResultCardBehavior>();
				mResultCards.Add(component);
				mIncorrectResultCards.Add(component);
				component.ResultSprite = aSprites[list[j] % aSprites.Length];
				component.ResetTargetScale(mTargetWorldSize, true);
				gameObject.SetActive(true);
			}
			mResultCards.Shuffle();
			NumOfResults = mResultCards.Count;
			if (NumOfResults > mRowBreakpoint)
			{
				mRowBreakpoint = NumOfResults - NumOfResults / 2;
			}
		}

		private void SwapSprites(Sprite aOtherSprite, int aRandomIndex)
		{
			ResultCardBehavior resultCardBehavior = mResultCards[(aRandomIndex + 2) % mResultCards.Count];
			for (int i = 0; i < mResultCards.Count; i++)
			{
				if (mResultCards[i].ResultSprite.name.CompareTo(aOtherSprite.name) == 0)
				{
					resultCardBehavior = mResultCards[i];
					break;
				}
			}
			resultCardBehavior.ResultSprite = aOtherSprite;
		}

		public Sequence ShowResults()
		{
			Sequence sequence = DOTween.Sequence();
			for (int i = 0; i < mResultCards.Count; i++)
			{
				FriendzyGame.PlaySound("ImageAppears", FriendzyGame.SOUND_PREFIX);
				sequence.Append(mResultCards[i].ShowNormalSprite(0.125f));
			}
			return sequence;
		}

		private void DestroyOtherChoices()
		{
			for (int i = 0; i < mIncorrectResultCards.Count; i++)
			{
				mResultCards.Remove(mIncorrectResultCards[i]);
				mIncorrectResultCards[i].gameObject.SetActive(false);
			}
			mIncorrectResultCards.Clear();
		}

		public ResultCardBehavior GetRandomResult(bool aOnlyIncorrectChoices = false)
		{
			int num = 0;
			if (aOnlyIncorrectChoices)
			{
				num = Random.Range(0, mIncorrectResultCards.Count);
				return mIncorrectResultCards[num];
			}
			num = Random.Range(0, mResultCards.Count);
			return mResultCards[num];
		}

		public ResultCardBehavior GetChosenResult()
		{
			return mChosenResult;
		}

		private Tween SelectChoice(ResultCardBehavior aResult)
		{
			return aResult.SelectChoice(0.5f, PosReference.localPosition);
		}

		private void InitializeLights()
		{
			SequenceStarted = false;
			SequenceEnded = false;
			SequenceSkippable = true;
			SpotlightVignette.Initialize(this);
			ResultsFader.color = TRANSPARENT_BLACK;
			SpotlightAvatar.gameObject.SetActive(false);
			ResultsFader.gameObject.SetActive(false);
		}

		public void LightSequence()
		{
			Sequence s = DOTween.Sequence();
			ResultsFader.gameObject.SetActive(true);
			mResultAnim.DisplayQuizTitle();
			Tween t = ResultsFader.DOColor(FADED_BLACK, 1f).OnComplete(TurnOnSpotlights);
			s.Append(t);
			s.AppendCallback(BeginSequence);
			s.Append(SelectionSequence(0.5f, 4f));
			s.AppendCallback(NotSkippable);
			s.AppendInterval(0.5f).OnComplete(SelectResult);
		}

		private void BeginSequence()
		{
			SequenceStarted = true;
		}

		private void NotSkippable()
		{
			SequenceSkippable = false;
		}

		private void TurnOnSpotlights()
		{
			ResultsFader.gameObject.SetActive(false);
			SpotlightAvatar.gameObject.SetActive(true);
			SpotlightVignette.gameObject.SetActive(true);
			FriendzyGame.PlaySound("Drumroll", FriendzyGame.SOUND_PREFIX);
			FriendzyGame.SetVolumeEvent(FriendzyGame.MUSIC_CUE_NAME, 0f);
		}

		private Sequence SelectionSequence(float aDelay, float aDuration)
		{
			Sequence sequence = DOTween.Sequence();
			sequence.Insert(0f, SpotlightVignette.SelectionSequence(aDelay, aDuration));
			return sequence;
		}

		public void SelectResult()
		{
			DestroyOtherChoices();
			UpdateLayout();
			mResultAnim.GetResult();
			FriendzyGame.StopSound("Drumroll", FriendzyGame.SOUND_PREFIX);
			FriendzyGame.SetVolumeEvent(FriendzyGame.MUSIC_CUE_NAME, 0.5f);
			FriendzyGame.PlaySound("Cheer", FriendzyGame.SOUND_PREFIX);
			Sequence s = DOTween.Sequence();
			s.AppendCallback(delegate
			{
				mResultAnim.DisplayResultTitle();
			});
			s.Append(TurnOffSpotlights());
			s.Join(SelectChoice(mChosenResult));
			s.AppendInterval(1.5f).OnComplete(delegate
			{
				Object.Destroy(mChosenResult.gameObject);
				SequenceEnded = true;
			});
		}

		private Tween ResizeSpotlight(bool aSetImmediate = false)
		{
			float num = 1f * (mTargetWorldSize.y / ResultPrefab.GetComponent<RectTransform>().rect.size.y) * SpotlightVignette.transform.localScale.y;
			Vector3 vector = num * Vector3.one;
			if (aSetImmediate)
			{
				SpotlightVignette.transform.localScale = vector;
			}
			return SpotlightVignette.transform.DOScale(vector, 0.5f);
		}

		private Sequence TurnOffSpotlights()
		{
			SpotlightAvatar.gameObject.SetActive(false);
			ResultsFader.gameObject.SetActive(false);
			return SpotlightVignette.FinalSelectionMove(0.5f, 1f);
		}
	}
}
