using DG.Tweening;
using Mix.Games.Tray.Friendzy.Camera;
using Mix.Games.Tray.Friendzy.Data;
using Mix.Games.Tray.Friendzy.FSM;
using Mix.Games.Tray.Friendzy.FriendzyQuiz;
using Mix.Games.Tray.Friendzy.ResultShow;
using Mix.Games.Tray.Friendzy.ResultsCanvas;
using UnityEngine;

namespace Mix.Games.Tray.Friendzy.ResultAnimator
{
	public class ResultsAnimator : MonoBehaviour
	{
		private const float BLINK_RATE_RESULTS = 0.15f;

		private const float BLINK_RATE_NORMAL = 1f;

		public CameraController CamController;

		public LightingAnimator LightAnim;

		[Header("Pertaining to Pedestals")]
		public Animator[] PedestalAnimators;

		[Header("Pertaining to Player Avatars")]
		public Animator[] PlayerAvatars;

		[SerializeField]
		[Header("Pertaining to Final Results Displays")]
		private ResultsCanvasData[] ResultsCanvases;

		public FiniteStateMachine mFSM;

		public InitialState Initial;

		public ResultDeterminedState ResultDetermined;

		public PlayerTwoMovementState CameraMovement;

		public DisplayResultsState DisplayResults;

		public ParticleSystem Confetti;

		public GameShowLights GameLights;

		public BackgroundImage Background;

		[HideInInspector]
		public PLAYER Player;

		private string FinalTrigger { get; set; }

		public string ResultSentence { get; set; }

		public string ResultText { get; set; }

		public string ResultDescription { get; set; }

		private void Awake()
		{
			Initial = new InitialState(this);
			ResultDetermined = new ResultDeterminedState(this);
			CameraMovement = new PlayerTwoMovementState(this);
			DisplayResults = new DisplayResultsState(this);
			mFSM = new FiniteStateMachine();
			mFSM.CurrentState = Initial;
			mFSM.CurrentState.Enter();
			LightAnim.SetResultAnimator(this);
		}

		private void Update()
		{
			mFSM.CurrentState.Update();
		}

		public void StartLightingSequence()
		{
			LightAnim.UpdateLayout(true);
			PedestalAction(0, "ResultsZoomIn");
			LightAnim.ShowResults().OnComplete(LightAnim.LightSequence);
		}

		public void PedestalLift()
		{
			PlayerAvatars[0].gameObject.SetActive(true);
			PedestalAction(0, "CurrentPlayer");
		}

		public void PedestalAction(int aPedestalIndex, string aPedestalAction)
		{
			PedestalAnimators[aPedestalIndex].SetTrigger(aPedestalAction);
		}

		public void PlayerReaction(int aPlayerIndex, string aPlayerAction)
		{
			PlayerAvatars[aPlayerIndex].SetTrigger(aPlayerAction);
		}

		public void GetResultAvatarReact()
		{
			PlayerReaction(0, "GetResult");
		}

		public void ActivatePlayerTwo()
		{
			PlayerReaction(0, "WatchP2Enter");
			PlayerAvatars[1].gameObject.SetActive(true);
			SetPlayerReactions();
			PedestalAnimators[1].SetTrigger("RiseUp");
		}

		private void SetPlayerReactions()
		{
			if (FinalTrigger.CompareTo("ERROR") == 0)
			{
				FinalTrigger = "NEUTRAL";
			}
			PlayerReaction(0, FinalTrigger);
			PlayerReaction(1, FinalTrigger);
		}

		public void ResultsZoom()
		{
			CamController.ResultSlowZoom();
		}

		public void ResultZoomOut()
		{
			CamController.ResultZoomOut();
		}

		public void SetResultSprites(Picture[] aPictures)
		{
			Sprite[] array = new Sprite[aPictures.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = aPictures[i].GetPicture();
			}
			LightAnim.SetResultSprites(array);
		}

		public void SetChosenSprite(Picture aChosenPic)
		{
			LightAnim.SetChosenResult(aChosenPic.GetPicture());
		}

		public void ActivateDescriptionCanvas(int aPlayerIndex, bool aToggle)
		{
			ResultsCanvases[aPlayerIndex].PlayerDescription.SetActive(aToggle);
		}

		public void DisplayResultTitle()
		{
			ResultsCanvases[(int)(Player - 1)].SetResultSentence(ResultText);
			DescriptionReact();
		}

		public void DisplayQuizTitle()
		{
			ResultsCanvases[(int)(Player - 1)].SetResultSentence(ResultSentence);
		}

		public void DisplayFullDescription()
		{
			PlayerReaction(0, "ShowDescription");
			ResultsCanvases[(int)(Player - 1)].DisplayFullDescription();
			ResultsCanvases[(int)(Player - 1)].ActionButton.SetActive(true);
		}

		public void DescriptionReact()
		{
			ResultsCanvases[(int)(Player - 1)].DescriptionReact();
		}

		public void ActivateResultScreen(bool aToggle)
		{
			ResultsCanvases[1].ActivateResultScreen(aToggle).OnComplete(DelayedFinalReaction);
		}

		private void DelayedFinalReaction()
		{
			CamController.ComparisonZoomOut();
			PlayReactionSound();
			ActivateConfetti(false);
			ActivateConfetti(true);
		}

		private void PlayReactionSound()
		{
			string empty = string.Empty;
			switch (FinalTrigger)
			{
			default:
				empty = "ResultPositive";
				break;
			case "NEGATIVE":
				empty = "ResultNegative";
				break;
			}
			FriendzyGame.PlaySound(empty, FriendzyGame.SOUND_PREFIX);
		}

		public void EnterResultsState()
		{
			FriendzyGame.PlaySound("ContinueUI", FriendzyGame.SOUND_PREFIX);
			DOVirtual.DelayedCall(0.3f, delegate
			{
				FriendzyGame.PlaySound("Avatar/Sequence3/DropIn", FriendzyGame.SOUND_PREFIX);
			});
			CamController.ComparisonZoomIn();
			mFSM.ChangeToState(DisplayResults);
		}

		public void EnterState(IState aStateToEnter)
		{
			if (aStateToEnter == null)
			{
				aStateToEnter = DisplayResults;
			}
			mFSM.ChangeToState(aStateToEnter);
		}

		public void GetResult()
		{
			ResultZoomOut();
			GetResultAvatarReact();
			ActivateConfetti(true);
			PedestalAction(0, "ResultsZoomOut");
			SetLightBlinkRate(0.15f);
		}

		public void SetLightBlinkRate(float aRate)
		{
			GameLights.BlinkInterval = aRate;
		}

		public void ActivateConfetti(bool aToggle)
		{
			Confetti.gameObject.SetActive(aToggle);
		}

		public void ReceiveMessage(ref ResultMessage aResultMessage)
		{
			switch (aResultMessage.Type)
			{
			case ResultMessageType.START_THE_SHOW:
				Player = aResultMessage.FriendzyResultEntry.PlayerNumber;
				if (Player == PLAYER.PLAYER_2)
				{
					ResultsCanvases[1].SetPlayerResults(0, aResultMessage.FriendzyResultEntry.mResult.Pic.GetPicture(), aResultMessage.FriendzyResultEntry.mResult.Message);
					ResultsCanvases[1].SetPlayerResults(1, aResultMessage.FriendzyOtherPlayerResultPicture.GetPicture(), aResultMessage.FriendzyOtherPlayerResultEntry.mResult.Message);
				}
				FinalTrigger = aResultMessage.FriendzyRelationshipState.ToString();
				ResultSentence = aResultMessage.QuizReference.Title;
				ResultText = aResultMessage.FriendzyResultEntry.mResult.Message;
				ResultDescription = aResultMessage.FriendzyResultEntry.mResult.DescriptionText;
				ResultsCanvases[(int)(Player - 1)].SetCurrentPlayerDescription(ResultSentence, ResultText, ResultDescription);
				CamController.OutroZoomOut();
				break;
			}
		}
	}
}
