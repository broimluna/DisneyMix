using System.Collections.Generic;
using DG.Tweening;
using Mix.Games.Data;
using Mix.Games.Session;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.WouldYouRather
{
	[SelectionBase]
	public class WouldYouRatherCard : MonoBehaviour, IGameAsset
	{
		public float randomRotationAmount = 5f;

		[Header("Internal References")]
		public GameObject cardParent;

		public SelectedEffect glow;

		public Text choiceText;

		public Image choiceImage;

		public ParticleSystem finalParticles;

		[Header("Drag & Flick Settings")]
		public float selectedLiftAmount = 0.1f;

		public float selectedLiftTime = 0.3f;

		[Header("Start Animation")]
		public float cardStartAnimationApexHeight = 1f;

		public float cardStartAnimationFlyTime = 4f;

		public float cardStartAnimationRotateTime = 2f;

		public float cardStartAnimationRotateDelay = 1f;

		[Header("Select / Deselect Animation")]
		public float cardSelectAnimationApexHeight;

		public float cardSelectAnimationFlyTime;

		public float cardSelectAnimationFlyDelay;

		[Header("End Animation")]
		public float cardEndAnimationScaleDownTime = 0.5f;

		public float cardEndAnimationExplodeDelay = 1f;

		public float cardEndAnimationDelayBeforeGameOver = 3f;

		public float cardEndAnimationChosenScale = 1.2f;

		public float cardEndAnimationNotChosenScale = 0.5f;

		public float cardEndAnimationChosenScaleTime = 0.3f;

		[Header("Shuffle Animation")]
		public float cardShuffleTime = 0.5f;

		public Vector3 cardShuffleFlyVectorMin;

		public Vector3 cardShuffleFlyVectorMax;

		[Header("Selected Animation")]
		public Vector3 selectedPositionWobbleAmount;

		public float selectedPositionWobbleFrequencey;

		public Vector3 selectedRotationWobbleAmount;

		public float selectedRotationWobbleFrequencey;

		[Header("Text Fitting")]
		public bool ManuallyFitText = true;

		public int MaxFontSize = 10;

		public int MinFontSize = 6;

		[HideInInspector]
		public WouldYouRatherGame WYRGame;

		[HideInInspector]
		public int choiceIndex;

		private List<WouldYouRatherChoice> mChoices;

		private GameObject mImage;

		private int mPositionInStack;

		private Collider mCollider;

		private Transform mTransform;

		private Vector3 mLiftAmount = Vector3.zero;

		private Vector3 mLiftVelocity = Vector3.zero;

		private Vector3 mLiftTarget = Vector3.zero;

		private float mWobbleAmount;

		private float mWobbleChangeVelocity;

		private float mWobbleAmountTarget;

		private bool mIsSelected;

		public WouldYouRatherResponse.WouldYouRatherResponseType responseChoice { get; set; }

		public int positionInStack
		{
			get
			{
				return mPositionInStack;
			}
			set
			{
				mPositionInStack = value;
			}
		}

		public bool isSelected
		{
			get
			{
				return mIsSelected;
			}
			set
			{
				if (value != mIsSelected && WYRGame.AllowCardSelection)
				{
					mIsSelected = value;
					if (mIsSelected)
					{
						WYRGame.CardSelected(this);
						glow.Activate();
						mLiftTarget = Vector3.up * selectedLiftAmount;
						mWobbleAmountTarget = 1f;
					}
					else
					{
						WYRGame.CardDeselected(this);
						glow.Deactivate();
						mLiftTarget = Vector3.zero;
						mWobbleAmountTarget = 0f;
					}
				}
			}
		}

		public WouldYouRatherGame.CardStackLocation CardLocation { get; set; }

		public WouldYouRatherChoice CurrentChoice { get; private set; }

		void IGameAsset.OnGameSessionAssetLoaded(object aUserData)
		{
			if (!this.IsNullOrDisposed() && !(base.transform == null) && !(WYRGame == null) && CurrentChoice != null)
			{
				if (!mImage.IsNullOrDisposed())
				{
					Object.Destroy(mImage);
				}
				mImage = WYRGame.GameController.GetBundleInstance(CurrentChoice.ImageURL) as GameObject;
				if (mImage != null)
				{
					mImage.transform.SetParent(base.transform, false);
					choiceImage.sprite = mImage.GetComponent<Image>().sprite;
					mImage.SetActive(false);
				}
			}
		}

		private void Awake()
		{
			mCollider = GetComponent<Collider>();
			mTransform = base.transform;
			mLiftTarget = Vector3.zero;
		}

		private void Start()
		{
			cardParent.transform.localEulerAngles = new Vector3(0f, Random.Range(0f - randomRotationAmount, randomRotationAmount), 0f);
		}

		private void Update()
		{
			mLiftAmount = Vector3.SmoothDamp(mLiftAmount, mLiftTarget, ref mLiftVelocity, selectedLiftTime);
			mWobbleAmount = Mathf.SmoothDamp(mWobbleAmount, mWobbleAmountTarget, ref mWobbleChangeVelocity, 0.4f);
			cardParent.transform.localPosition = mLiftAmount + Mathf.Sin(Time.time * selectedPositionWobbleFrequencey) * mWobbleAmount * selectedPositionWobbleAmount;
			cardParent.transform.localEulerAngles = Mathf.Sin(Time.time * selectedRotationWobbleFrequencey) * mWobbleAmount * selectedRotationWobbleAmount;
		}

		private void OnDestroy()
		{
			if (DebugSceneIndicator.IsMainScene && CurrentChoice != null)
			{
				WYRGame.GameController.CancelBundles(CurrentChoice.ImageURL);
				WYRGame.GameController.DestroyBundleInstance(CurrentChoice.ImageURL, mImage);
			}
		}

		public void ReceiveChoices(List<WouldYouRatherChoice> choices)
		{
			mChoices = choices;
			if (mChoices != null && choiceIndex >= 0 && choiceIndex < mChoices.Count)
			{
				UpdateChoice(mChoices[choiceIndex]);
			}
		}

		public void UpdateChoice(WouldYouRatherChoice choice)
		{
			CurrentChoice = choice;
			choiceText.text = choice.Text;
			if (ManuallyFitText)
			{
				FitTextToBox();
			}
			WYRGame.GameController.LoadAsset(this, choice.ImageURL);
		}

		private void FitTextToBox()
		{
			choiceText.fontSize = MaxFontSize;
			while (LayoutUtility.GetPreferredHeight(choiceText.rectTransform) > choiceText.rectTransform.rect.height && choiceText.fontSize > MinFontSize)
			{
				choiceText.fontSize--;
			}
		}

		public void AppearToBeSelected()
		{
			glow.Activate();
			mWobbleAmountTarget = 1f;
		}

		public void DisableInput()
		{
			mCollider.enabled = false;
		}

		public void BeginEndGameAnimation(bool wasChosen)
		{
			Sequence s = DOTween.Sequence();
			glow.Deactivate();
			if (!WYRGame.IsFirstPlayer)
			{
				if (wasChosen)
				{
					Tweener t = base.transform.DOScale(Vector3.one * cardEndAnimationChosenScale, cardEndAnimationChosenScaleTime).SetEase(Ease.InCubic);
					s.Append(t);
				}
				else
				{
					Tweener t2 = base.transform.DOScale(Vector3.one * cardEndAnimationNotChosenScale, cardEndAnimationChosenScaleTime).SetEase(Ease.InCubic);
					s.Append(t2);
				}
			}
			s.AppendInterval(cardEndAnimationExplodeDelay);
			s.AppendCallback(ExplodeCardParticles);
			Tweener t3 = cardParent.transform.DOScale(Vector3.zero, cardEndAnimationScaleDownTime).SetEase(Ease.InCubic);
			s.Append(t3);
			s.AppendInterval(cardEndAnimationDelayBeforeGameOver);
			s.AppendCallback(EndGameAnimationComplete);
		}

		private void ExplodeCardParticles()
		{
			glow.Deactivate();
			finalParticles.Play(true);
		}

		public Sequence BeginStartGameAnimation()
		{
			Vector3 positionForCardInStack = WYRGame.GetPositionForCardInStack(CardLocation, positionInStack);
			List<Vector3> list = new List<Vector3>();
			list.Add(base.transform.position);
			list.Add(Vector3.Lerp(base.transform.position, positionForCardInStack, 0.5f) + Vector3.up * cardStartAnimationApexHeight);
			list.Add(positionForCardInStack);
			Sequence sequence = DOTween.Sequence();
			Tweener t = mTransform.DOPath(list.ToArray(), cardStartAnimationFlyTime).SetEase(Ease.InOutCubic);
			sequence.Insert(0f, t);
			t = mTransform.DORotate(Vector3.zero, cardStartAnimationRotateTime).SetEase(Ease.InOutSine);
			sequence.Insert(cardStartAnimationRotateDelay, t);
			return sequence;
		}

		public void FlyToLocation(Vector3 location)
		{
			List<Vector3> list = new List<Vector3>();
			list.Add(base.transform.position);
			list.Add(Vector3.Lerp(base.transform.position, location, 0.5f) + Vector3.up * cardSelectAnimationApexHeight);
			list.Add(location);
			mTransform.DOPath(list.ToArray(), cardSelectAnimationFlyTime).SetEase(Ease.InOutCubic).SetDelay(cardSelectAnimationFlyDelay);
		}

		public Tweener StartShuffleAnimation()
		{
			if (mTransform == null)
			{
				mTransform = base.transform;
			}
			Vector3 position = mTransform.position;
			position += new Vector3(Random.Range(cardShuffleFlyVectorMin.x, cardShuffleFlyVectorMax.x), Random.Range(cardShuffleFlyVectorMin.y, cardShuffleFlyVectorMax.y), Random.Range(cardShuffleFlyVectorMin.z, cardShuffleFlyVectorMax.z));
			return mTransform.DOMove(position, cardShuffleTime).SetEase(Ease.InCubic).OnComplete(ShuffleAnimationComplete);
		}

		public void ToggleCard()
		{
			isSelected = !isSelected;
		}

		public void MoveToStartPositionComplete()
		{
		}

		public void EndGameAnimationComplete()
		{
			WYRGame.GameOver();
		}

		public void ShuffleAnimationComplete()
		{
			Object.Destroy(base.gameObject);
		}
	}
}
