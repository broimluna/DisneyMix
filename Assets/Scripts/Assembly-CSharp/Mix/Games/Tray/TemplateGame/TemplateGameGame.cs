using DG.Tweening;
using Disney.LaunchPad.Packages.EventSystem;
using Disney.LaunchPad.Packages.FiniteStateMachine;
using Mix.Games.Data;
using Mix.Games.Tray.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.TemplateGame
{
	public class TemplateGameGame : MonoBehaviour, IMixGame
	{
		private const string MUSIC_CUE_NAME = "TemplateGame/MUS/Music";

		private TemplateGameGameController mGameController;

		private FiniteStateMachine mStateMachine;

		public bool TestPlayer1 = true;

		private bool mIsFirstRound = true;

		public Button optionA;

		public Button optionB;

		public Text optionA_Text;

		public Text optionB_Text;

		public Text titleText;

		public Text subtitleText;

		private string mSelectedString;

		private bool mSelectedOption;

		public TemplateGameGameController GameController
		{
			get
			{
				return mGameController;
			}
		}

		public EventDispatcher GameEventDispatcher { get; private set; }

		void IMixGame.Initialize(MixGameData aData)
		{
			if (DebugSceneIndicator.IsMainScene)
			{
				if (aData == null)
				{
					GameEventDispatcher.DispatchEvent(new GamePlayEvent());
					return;
				}
				TemplateGameData gameData = GameController.GetGameData<TemplateGameData>();
				mSelectedString = gameData.TestString;
				GameEventDispatcher.DispatchEvent(new GameContinueEvent());
			}
		}

		void IMixGame.Pause()
		{
			BaseGameController.Instance.Session.SessionSounds.PauseSoundEvent("TemplateGame/MUS/Music", base.gameObject);
			DOTween.PauseAll();
		}

		void IMixGame.Play()
		{
			DOVirtual.DelayedCall(0.1f, delegate
			{
				BaseGameController.Instance.Session.SessionSounds.PlayMusic("TemplateGame/MUS/Music", base.gameObject);
			});
		}

		void IMixGame.Quit()
		{
			BaseGameController.Instance.Session.SessionSounds.StopSoundEvent("TemplateGame/MUS/Music", base.gameObject);
			DOTween.KillAll();
		}

		void IMixGame.Resume()
		{
			DOVirtual.DelayedCall(0.1f, delegate
			{
				BaseGameController.Instance.Session.SessionSounds.UnpauseSoundEvent("TemplateGame/MUS/Music", base.gameObject);
			});
			DOTween.PlayAll();
		}

		private void OnEnable()
		{
			TrayGamePhysics.ResetAllGameLayers();
		}

		private void Awake()
		{
			mGameController = base.transform.parent.gameObject.GetComponent<TemplateGameGameController>();
		}

		private void Start()
		{
			CreateStateMachine();
		}

		public void SetupGame()
		{
			titleText.text = "Welcome to TemplateGame!";
		}

		public void PlayGame()
		{
			mIsFirstRound = true;
			subtitleText.text = "Pick a conversation topic:";
			optionA_Text.text = "Puppies";
			optionB_Text.text = "Ice Cream";
			optionA.onClick.AddListener(delegate
			{
				PlayOptionSelected(optionA_Text.text);
			});
			optionB.onClick.AddListener(delegate
			{
				PlayOptionSelected(optionB_Text.text);
			});
		}

		public void ContinueGame()
		{
			mIsFirstRound = false;
			titleText.color = Color.yellow;
			subtitleText.text = string.Format("Do you like {0}?", mSelectedString);
			optionA_Text.text = "Yes";
			optionB_Text.text = "No";
			optionA.onClick.AddListener(delegate
			{
				ContinueOptionSelected(true);
			});
			optionB.onClick.AddListener(delegate
			{
				ContinueOptionSelected(false);
			});
		}

		public void SendAndQuit()
		{
			if (mIsFirstRound)
			{
				GameOverPost(mSelectedString);
			}
			else
			{
				GameOverResponse(mSelectedOption);
			}
		}

		private void GameOverPost(string aString)
		{
			if (DebugSceneIndicator.IsMainScene)
			{
				TemplateGameData gameData = GameController.GetGameData<TemplateGameData>();
				gameData.TestString = aString;
				GameController.GameOver(gameData);
			}
		}

		private void GameOverResponse(bool aResult)
		{
			if (DebugSceneIndicator.IsMainScene)
			{
				TemplateGameData gameData = GameController.GetGameData<TemplateGameData>();
				TemplateGameResponse myResponse = gameData.GetMyResponse(gameData.Responses, GameController.PlayerId);
				myResponse.Result = aResult;
				myResponse.Attempts++;
				GameController.GameOver(myResponse);
			}
		}

		private void PlayOptionSelected(string aSelection)
		{
			mSelectedString = aSelection;
			GameEventDispatcher.DispatchEvent(new GameSendEvent());
		}

		private void ContinueOptionSelected(bool aSelection)
		{
			mSelectedOption = aSelection;
			GameEventDispatcher.DispatchEvent(new GameSendEvent());
		}

		private void CreateStateMachine()
		{
			GameEventDispatcher = new EventDispatcher();
			mStateMachine = FiniteStateMachine.CreateFiniteStateMachine(base.transform, "Game State Machine");
			mStateMachine.eventDispatcher = GameEventDispatcher;
			mStateMachine.defaultTransition = mStateMachine.CreateTransition<ImmediateTransition>(string.Empty);
			State state = mStateMachine.CreateState("Loading");
			State state2 = CreateState<ContinueGameState>("Continue");
			State state3 = CreateState<PlayGameState>("Play");
			State endState = CreateState<SendState>("Send");
			mStateMachine.initialState = state;
			mStateMachine.CreateEventSignal<GamePlayEvent>(state, state3);
			mStateMachine.CreateEventSignal<GameContinueEvent>(state, state2);
			mStateMachine.CreateEventSignal<GameSendEvent>(state3, endState);
			mStateMachine.CreateEventSignal<GameSendEvent>(state2, endState);
			mStateMachine.SetActive(true);
		}

		private State CreateState<T>(string aStateName) where T : BaseGameState
		{
			State state = mStateMachine.CreateState<T>(aStateName);
			BaseGameState component = state.GetComponent<BaseGameState>();
			component.mGame = this;
			return state;
		}
	}
}
