using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.Fireworks
{
	public class SceneButtonLoader : MonoBehaviour
	{
		public Button sceneButton;

		public GameObject buttonSpinner;

		public GameObject buttonPlayIcon;

		public FireworksGame fireworksGame;

		public Scene scene;

		public ScrollRectHorizontalSnap scrollSnap;

		public ManageSwipeArrows arrowsManager;

		public PlayMenuTransitionAnimations menuAnimManager;

		public void OnPressed()
		{
			if (!IsLoading())
			{
				EnableSpinner(true);
				BaseGameController.Instance.Session.SessionSounds.PlaySoundEvent("Fireworks/SFX/UIButton");
				if (scrollSnap != null && scene != null)
				{
					scrollSnap.JumpToScene(scene);
				}
				if (fireworksGame != null)
				{
					fireworksGame.SaveSelectedSceneandSong();
				}
				if (arrowsManager != null)
				{
					arrowsManager.gameObject.SetActive(false);
				}
			}
		}

		private void EnableSpinner(bool doSpin)
		{
			if (!(buttonSpinner == null) && !(sceneButton == null) && !(buttonPlayIcon == null))
			{
				buttonPlayIcon.SetActive(!doSpin);
				buttonSpinner.SetActive(doSpin);
				sceneButton.interactable = !doSpin;
			}
		}

		private bool IsLoading()
		{
			bool result = false;
			if (sceneButton != null)
			{
				result = !sceneButton.IsInteractable();
			}
			return result;
		}

		public void TransitionToCreationMode()
		{
			if (menuAnimManager != null)
			{
				EnableSpinner(false);
				sceneButton.gameObject.SetActive(false);
				menuAnimManager.PlayExitAnimation();
			}
		}
	}
}
