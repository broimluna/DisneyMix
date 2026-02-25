using System;
using DG.Tweening;
using Mix.Games.Session;
using Mix.Games.Tray.Friendzy.Camera;
using Mix.Games.Tray.Friendzy.Data;
using Mix.Games.Tray.Friendzy.FSM;
using UnityEngine;

namespace Mix.Games.Tray.Friendzy.Menu
{
	[RequireComponent(typeof(MenuAnimator))]
	[RequireComponent(typeof(FlipBarManager))]
	public class MenuSystem : MonoBehaviour
	{
		public FiniteStateMachine MenuFSM;

		public CategoryIPState CategoryState;

		public QuizIPState QuizState;

		public LoadingMenuState LoadingMenuState;

		public MenuRevealState RevealState;

		private MenuMessage mMessage;

		private MenuController mMenuController;

		private CameraController mCameraController;

		private Category[] mCachedCategory;

		[Header("References to Menu Components")]
		public FlipPanelHeader HeaderFlipBar;

		public BackButton BackBtn;

		private MenuAnimator mMenuAnim;

		private FlipBarManager mFlipBarManager;

		private bool mDataReady;

		public bool MenuReadyToBeOpened;

		private void Awake()
		{
			CategoryState = new CategoryIPState(this);
			QuizState = new QuizIPState(this);
			LoadingMenuState = new LoadingMenuState(this);
			RevealState = new MenuRevealState(this);
			MenuFSM = new FiniteStateMachine();
			if (!mDataReady)
			{
				MenuFSM.CurrentState = LoadingMenuState;
			}
			else
			{
				MenuFSM.CurrentState = RevealState;
			}
			MenuFSM.CurrentState.Enter();
			mMenuAnim = GetComponent<MenuAnimator>();
			mFlipBarManager = GetComponent<FlipBarManager>();
		}

		private void Update()
		{
			MenuFSM.Update();
		}

		public void ReceiveMessage(ref MenuMessage message)
		{
			switch (message.Type)
			{
			case MenuMessageType.IP_QUIZ_DATA_LOADED:
				if (CategoryState != null)
				{
					MenuFSM.ChangeToState(RevealState);
				}
				mDataReady = true;
				break;
			case MenuMessageType.MENU_IMPLODE:
				MenuReadyToBeOpened = true;
				break;
			case MenuMessageType.TURN_OFF_LEVEL_GEOMETRY:
				mMenuController.ReceiveMessage(ref message);
				break;
			}
		}

		public void SetMenuController(MenuController inMenuController)
		{
			mMenuController = inMenuController;
		}

		public Category[] GetCategories()
		{
			if (mCachedCategory == null)
			{
				mCachedCategory = mMenuController.GetCategories();
			}
			return mCachedCategory;
		}

		public string[] GetQuizzesByIP()
		{
			return mMenuController.GetQuizzesByIP(mMessage.Category);
		}

		public void DisplayCategories(Category[] dataToDisplay)
		{
			mFlipBarManager.PopulateCategories(dataToDisplay);
		}

		public void DisplayQuizzes()
		{
			string[] quizzesByIP = GetQuizzesByIP();
			mFlipBarManager.PopulateQuizzes(quizzesByIP);
		}

		public void ReceiveCategoryOrIPNameFromButton(string categoryOrString)
		{
			if (MenuFSM.CurrentState == CategoryState)
			{
				string aGameParameter = string.Format("{0}{1}", "quiz_", categoryOrString);
				BaseGameController.Instance.LogEvent(GameLogEventType.ACTION, aGameParameter, categoryOrString);
				FriendzyGame.PlaySound("SelectTheme", FriendzyGame.SOUND_PREFIX);
				mMessage.Category = categoryOrString;
				MenuFSM.ChangeToState(QuizState);
				Category category = null;
				for (int i = 0; i < mCachedCategory.Length; i++)
				{
					if (string.Equals(mMessage.Category, mCachedCategory[i].Name))
					{
						category = mCachedCategory[i];
						break;
					}
				}
				HeaderFlipBar.SetCategory(category);
				BackBtn.SetColors(GameUtil.HexToColor(category.MainColorHex), GameUtil.HexToColor(category.QuestionBgColorHex));
				mMenuAnim.SetIPTexture(category.GetCategoryPalettePicture().GetTexture());
				mMenuAnim.IPColorSwitch();
				mMenuAnim.FlipAllBars(mFlipBarManager.FlipBars).AppendCallback(BackBtn.Show);
			}
			else
			{
				FriendzyGame.PlaySound("SelectTopic", FriendzyGame.SOUND_PREFIX);
				BackBtn.Hide();
				mMessage.Quiz = categoryOrString;
				mMenuController.ReceiveMessage(ref mMessage);
			}
		}

		public void RandomlySelectQuiz()
		{
			FriendzyGame.PlaySound("SelectTopic", FriendzyGame.SOUND_PREFIX);
			DateTime now = DateTime.Now;
			UnityEngine.Random.InitState(now.Hour * now.Minute + now.Second);
			int num = UnityEngine.Random.Range(0, mCachedCategory.Length);
			mMessage.Category = mCachedCategory[num].Name;
			if (mCachedCategory[num].Quizzes.Length == 0)
			{
				int num2;
				for (num2 = num + 1; num2 != num; num2++)
				{
					num2 %= mCachedCategory.Length;
					if (mCachedCategory[num2].Quizzes.Length > 0)
					{
						mMessage.Category = mCachedCategory[num2].Name;
						break;
					}
				}
			}
			string[] quizzesByIP = GetQuizzesByIP();
			UnityEngine.Random.InitState(now.Hour * now.Minute + now.Second);
			num = UnityEngine.Random.Range(0, quizzesByIP.Length);
			mMessage.Quiz = quizzesByIP[num];
			BackBtn.Hide();
			mMenuController.ReceiveMessage(ref mMessage);
		}

		public void BackButtonClicked()
		{
			FriendzyGame.PlaySound("BackUI", FriendzyGame.SOUND_PREFIX);
			mMenuAnim.FlipAllBars(mFlipBarManager.FlipBars);
			mMenuAnim.IPColorSwitch();
			MenuFSM.ChangeToState(CategoryState);
			BackBtn.Hide();
			MenuMessage message = new MenuMessage(MenuMessageType.BACK_BUTTON_PRESSED, mMessage.Category);
			mMenuController.ReceiveMessage(ref message);
		}

		public void TurnOff()
		{
			mMenuAnim.TurnOff();
		}

		public void TurnOn()
		{
			mMenuAnim.TurnOn();
		}

		public CameraController GetCameraController()
		{
			if (mCameraController == null)
			{
				mCameraController = mMenuController.GetCameraController();
			}
			return mCameraController;
		}
	}
}
