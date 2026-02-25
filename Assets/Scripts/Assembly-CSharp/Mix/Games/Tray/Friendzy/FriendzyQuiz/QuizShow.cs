using DG.Tweening;
using Mix.Games.Tray.Friendzy.Data;
using Mix.Games.Tray.Friendzy.FSM;
using Mix.Games.Tray.Friendzy.LoadAsset;
using Mix.Games.Tray.Friendzy.Render;
using UnityEngine;

namespace Mix.Games.Tray.Friendzy.FriendzyQuiz
{
	public class QuizShow : MonoBehaviour
	{
		private const float LOAD_SPLASH_SCREEN_WAIT = 3f;

		private QuizController mQuizController;

		private Quiz mQuiz;

		private Category mCategory;

		private int mCurrentQuizIndex;

		private int[] mSumOfResult;

		public FiniteStateMachine QuizShowFSM;

		private QuizInitializationAndLoadState mLoadState;

		public QuizTakingState QuizTakingState;

		public bool DataLoadedAndReady = true;

		public GameObject[] Renderers;

		private IQuestionRenderer[] mRenderers;

		public QuestionOption QuestionPanel;

		public ProgressBar ProgressBarScript;

		public SelectionParticle SelectionParticleEffect;

		public SplashScreen QuizLogoScreen;

		public BackgroundImage QuizBackgroundImage;

		public ParticleSystem[] AmbientParticles;

		private void Awake()
		{
			mLoadState = new QuizInitializationAndLoadState(this);
			QuizTakingState = new QuizTakingState(this);
			QuizShowFSM = new FiniteStateMachine();
			mRenderers = new IQuestionRenderer[Renderers.Length];
			for (int i = 0; i < Renderers.Length; i++)
			{
				mRenderers[i] = Renderers[i].GetComponent<IQuestionRenderer>();
			}
			mCurrentQuizIndex = 0;
		}

		public void SetQuizController(QuizController inQuizController)
		{
			mQuizController = inQuizController;
		}

		public void ShowNextQuestion()
		{
			ShouldLoadResultAssets();
			LoadQuestion(mCurrentQuizIndex + 1);
			if (mCurrentQuizIndex < mQuiz.Questions.Length)
			{
				RenderQuestion(mQuiz.Questions[mCurrentQuizIndex]);
			}
			else
			{
				ProgressBarScript.FadeOut().OnComplete(ProgressToResults).SetDelay(0.35f);
			}
		}

		private void ProgressToResults()
		{
			QuizMessage inMessage = new QuizMessage(QuizMessageType.QUIZ_FINISHED, GetResultEntry(CalculateResultIndex()));
			mQuizController.ReceiveMessage(ref inMessage);
		}

		private void RenderQuestion(Question inQuestion)
		{
			int outIndexAtWhichRendererWasFound = 0;
			GameObject questionRenderer = GetQuestionRenderer(inQuestion.Type, out outIndexAtWhichRendererWasFound);
			questionRenderer.gameObject.SetActive(true);
			IQuestionRenderer questionRenderer2 = mRenderers[outIndexAtWhichRendererWasFound];
			questionRenderer2.RenderQuestion(inQuestion);
		}

		private GameObject GetQuestionRenderer(QUESTION_TYPE inType, out int outIndexAtWhichRendererWasFound)
		{
			for (int i = 0; i < Renderers.Length; i++)
			{
				if (inType == mRenderers[i].GetRenderType())
				{
					outIndexAtWhichRendererWasFound = i;
					return Renderers[i];
				}
			}
			outIndexAtWhichRendererWasFound = -1;
			return null;
		}

		public void ReceiveAnswer(int indexOfAnswer)
		{
			int[] array = mQuiz.Questions[mCurrentQuizIndex].IndexToResult[indexOfAnswer];
			for (int i = 0; i < array.Length; i++)
			{
				mSumOfResult[i] += array[i];
			}
			TransitionQuestions(indexOfAnswer);
		}

		private void TransitionQuestions(int indexOfAnswer)
		{
			Sequence s = DOTween.Sequence();
			int outIndexAtWhichRendererWasFound = 0;
			GameObject obj = GetQuestionRenderer(mQuiz.Questions[mCurrentQuizIndex++].Type, out outIndexAtWhichRendererWasFound);
			s.Append(obj.GetComponent<TransitionAnimator>().SelectionSequence(indexOfAnswer, ProgressBarScript));
			s.AppendCallback(delegate
			{
				obj.gameObject.SetActive(false);
			});
			s.AppendInterval(0.3f);
			s.AppendCallback(AdvanceOrLoadQuestion);
			s.Append(ProgressBarScript.ProgressToQuestion(mCurrentQuizIndex));
		}

		private void AdvanceOrLoadQuestion()
		{
			if (DataLoadedAndReady)
			{
				ShowNextQuestion();
			}
			else
			{
				QuizShowFSM.ChangeToState(mLoadState);
			}
		}

		private void SetAllParticlesColorOverLifetime(Color aColor)
		{
			for (int i = 0; i < AmbientParticles.Length; i++)
			{
				SetParticleColorOverLifetime(AmbientParticles[i], aColor);
			}
		}

		private void SetParticleColorOverLifetime(ParticleSystem aSys, Color aColor)
		{
			ParticleSystem.ColorOverLifetimeModule colorOverLifetime = aSys.colorOverLifetime;
			colorOverLifetime.enabled = true;
			Gradient gradient = new Gradient();
			GradientColorKey[] colorKeys = new GradientColorKey[2]
			{
				new GradientColorKey(aColor, 0f),
				new GradientColorKey(aColor, 1f)
			};
			GradientAlphaKey[] alphaKeys = new GradientAlphaKey[4]
			{
				new GradientAlphaKey(0f, 0f),
				new GradientAlphaKey(1f, 0.15f),
				new GradientAlphaKey(1f, 0.6f),
				new GradientAlphaKey(0f, 1f)
			};
			gradient.SetKeys(colorKeys, alphaKeys);
			colorOverLifetime.color = new ParticleSystem.MinMaxGradient(gradient);
		}

		public void ReceiveMessage(ref QuizMessage message)
		{
			if (message.Type == QuizMessageType.QUIZ_INITIALIZATION)
			{
				mSumOfResult = new int[message.QuizMessageQuiz.ResultTable.Length];
				mQuiz = message.QuizMessageQuiz;
				mCategory = message.QuizMessageCategory;
				ProgressBarScript.SetNumQuestions(mQuiz.Questions.Length);
				Color color = GameUtil.HexToColor(mCategory.MainColorHex);
				Color color2 = GameUtil.HexToColor(mCategory.AccentColorHex);
				Color color3 = GameUtil.HexToColor(mCategory.QuestionBgColorHex);
				ProgressBarScript.SetIPColors(color, color2, color3);
				SelectionParticleEffect.SetParticleEffectColor(color2);
				SetAllParticlesColorOverLifetime(color2);
				Color color4 = GameUtil.HexToColor(mCategory.QuestionFontColorHex);
				Color answerFontColor = GameUtil.HexToColor(mCategory.AnswerFontColorHex);
				QuestionPanel.SetColors(color, color4, color3);
				for (int i = 0; i < mRenderers.Length; i++)
				{
					mRenderers[i].SetColors(color, color4, answerFontColor, GameUtil.HexToColor(mCategory.AnswerBgColorHex));
				}
				QuizLogoScreen.SetLogoImage(mCategory.GetLogoPicture().GetPicture());
				QuizLogoScreen.SetQuizTitle(mQuiz.Title);
				QuizShowFSM.CurrentState = mLoadState;
				Sequence s = DOTween.Sequence();
				s.InsertCallback(3f, ReadyToMoveOn);
				QuizShowFSM.CurrentState.Enter();
			}
		}

		private ResultEntry GetResultEntry(int inIndex)
		{
			ResultEntry resultEntry = new ResultEntry();
			resultEntry.mIP = mCategory.Name;
			resultEntry.mQuiz = mQuiz.Title;
			resultEntry.mResult = mQuiz.ResultTable[inIndex];
			resultEntry.QuizTitle = mQuiz.Title;
			return resultEntry;
		}

		private int CalculateResultIndex()
		{
			int result = -1;
			int num = -1;
			for (int i = 0; i < mSumOfResult.Length; i++)
			{
				if (mSumOfResult[i] > num)
				{
					result = i;
					num = mSumOfResult[i];
				}
			}
			return result;
		}

		public void InitialLoadQuiz()
		{
			Job job = new Job();
			job.CallBackWhenFinishedJob = InitialLoadQuizFinished;
			QuizMessage inMessage = new QuizMessage(QuizMessageType.LOAD_QUESTION, job);
			int num = 0;
			if (mQuiz.Questions[num].GetQuestionGenre() == QUESTION_TYPE.QUESTION_TWO_VERT_PIC)
			{
				job.NumberOfItems += mQuiz.Questions[num].Pictures.Length;
			}
			job.ObjectsToLoad = new object[job.NumberOfItems + 1];
			int i = 0;
			if (mQuiz.Questions[num].GetQuestionGenre() == QUESTION_TYPE.QUESTION_TWO_VERT_PIC)
			{
				for (; i < mQuiz.Questions[num].Pictures.Length; i++)
				{
					mQuiz.Questions[num].Pictures[i].SetPictureType(PictureType.SPRITE);
					job.ObjectsToLoad[i] = mQuiz.Questions[num].Pictures[i];
				}
			}
			job.NumberOfItems++;
			job.ObjectsToLoad[i] = mCategory.LoadQuizBackgroundPicture();
			mQuizController.ReceiveMessage(ref inMessage);
		}

		private void InitialLoadQuizFinished(Job aJob)
		{
			QuizBackgroundImage.ToggleEnable(true);
			QuizBackgroundImage.ToggleFade(false, 3f);
			QuizBackgroundImage.SetSprite(mCategory.GetQuizBackgroundPicture().GetPicture());
			QuizLogoScreen.DataForQuizLoaded = true;
		}

		private void MidQuizLoadFinished(Job aJob)
		{
			DataLoadedAndReady = true;
		}

		private void ShouldLoadResultAssets()
		{
			if (mCurrentQuizIndex == mQuiz.Questions.Length / 2)
			{
				QuizMessage inMessage = new QuizMessage(QuizMessageType.LOAD_RESULT_ASSETS, mCategory, mQuiz);
				mQuizController.ReceiveMessage(ref inMessage);
			}
		}

		private void LoadQuestion(int aIndexOfQuestionToLoad)
		{
			if (aIndexOfQuestionToLoad >= mQuiz.Questions.Length)
			{
				return;
			}
			Job job = new Job();
			job.CallBackWhenFinishedJob = MidQuizLoadFinished;
			QuizMessage inMessage = new QuizMessage(QuizMessageType.LOAD_QUESTION, job);
			if (mQuiz.Questions[aIndexOfQuestionToLoad].GetQuestionGenre() == QUESTION_TYPE.QUESTION_TWO_VERT_PIC)
			{
				job.NumberOfItems += mQuiz.Questions[aIndexOfQuestionToLoad].Pictures.Length;
			}
			if (job.NumberOfItems != 0)
			{
				job.ObjectsToLoad = new object[job.NumberOfItems];
				for (int i = 0; i < mQuiz.Questions[aIndexOfQuestionToLoad].Pictures.Length; i++)
				{
					mQuiz.Questions[aIndexOfQuestionToLoad].Pictures[i].SetPictureType(PictureType.SPRITE);
					job.ObjectsToLoad[i] = mQuiz.Questions[aIndexOfQuestionToLoad].Pictures[i];
				}
			}
			mQuizController.ReceiveMessage(ref inMessage);
		}

		public void ReadyToMoveOn()
		{
			if (QuizLogoScreen.DataForQuizLoaded)
			{
				QuizLogoScreen.Disappear().OnComplete(delegate
				{
					QuizLogoScreen.gameObject.SetActive(false);
					ProgressBarScript.gameObject.SetActive(true);
					ProgressBarScript.FadeIn();
					QuizMessage inMessage = new QuizMessage(QuizMessageType.TURN_OFF_LEVEL_GEOMETRY);
					mQuizController.ReceiveMessage(ref inMessage);
					QuizShowFSM.ChangeToState(QuizTakingState);
				});
			}
			else
			{
				Sequence s = DOTween.Sequence();
				s.InsertCallback(1f, ReadyToMoveOn);
			}
		}
	}
}
