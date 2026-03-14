using Mix.Games.Tray.Hints;
using UnityEngine;

namespace Mix.Games.Tray.SecretWord
{
	public class SecretWordGuessWordPanel : MonoBehaviour
	{
		public const int NUM_HEARTS = 7;

		private const float BLINK_TIME = 0.5f;

		private string mHint;

		public GameObject heartCounterPrefab;

		public Transform heartCounterHolder;

		public Sprite heartIcon;

		public float heartTextMinWidth;

		public SecretWordGameController gameController;

		public SecretWordGame game;

		public Tile tile;

		public GameObject[] hearts;

		public SecretWordGameOverPanel gameOverPanel;

		public SecretWordSuccessPanel successPanel;

		public SecretWordHints hints;

		private GenericGameTooltip mHeartCounterText;

		private void Awake()
		{
			GameObject gameObject = Object.Instantiate(heartCounterPrefab);
			mHeartCounterText = gameObject.GetComponent<GenericGameTooltip>();
			mHeartCounterText.transform.SetParent(heartCounterHolder, false);
			mHeartCounterText.SetAnchor(AnchorUIElement.AnchorStyle.TOP_RIGHT);
			mHeartCounterText.leftIcon.sprite = heartIcon;
			mHeartCounterText.leftIcon.gameObject.SetActive(true);
			mHeartCounterText.Show(true);
			mHeartCounterText.textLayoutElement.minWidth = heartTextMinWidth;
			mHeartCounterText.leftSpacer.SetActive(false);
		}

		private void Start()
		{
			mHint = string.Format(BaseGameController.Instance.Session.GetLocalizedString("customtokens.secretword.guess_hint"), game.Hint);
			hints.HintText = mHint;
			hints.secretWordHint.Show();
		}

		public void Init(int numHearts)
		{
			base.gameObject.SetActive(true);
			mHeartCounterText.text = numHearts.ToString();
		}

		public void LoseHeart(int newHeartCount)
		{
			mHeartCounterText.SetTextWithBounce(newHeartCount.ToString());
		}

		public void ShowGameOverPanel(bool didPlayerWin)
		{
			if (didPlayerWin)
			{
				BaseGameController.Instance.Session.SessionSounds.PlaySoundEvent("SecretWord/SFX/GuessWord");
				successPanel.Show();
			}
			else
			{
				BaseGameController.Instance.Session.SessionSounds.PlaySoundEvent("SecretWord/SFX/FailToGuessWord");
				gameOverPanel.Show();
			}
		}
	}
}
