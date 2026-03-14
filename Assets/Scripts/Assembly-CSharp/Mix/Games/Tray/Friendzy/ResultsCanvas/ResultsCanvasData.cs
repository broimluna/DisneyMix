using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.Friendzy.ResultsCanvas
{
	public class ResultsCanvasData : MonoBehaviour
	{
		private const float INITIAL_PLAYER_DELAY = 1.5f;

		private const float REVEAL_DELAY = 3.5f;

		[SerializeField]
		private Text mRelationshipSentence;

		public PlayerResultsData[] PlayerResults;

		public GameObject PlayerDescription;

		public Text ResultSentenceText;

		public Text PlayerDescriptionText;

		public GameObject Results;

		public GameObject ActionButton;

		public GameObject DoneButton;

		public string ResultSentence { get; set; }

		public string ResultText { get; set; }

		public string ResultDescription { get; set; }

		public void SetRelationshipSentence(string aSentence)
		{
			mRelationshipSentence.text = aSentence;
		}

		public Sequence ActivateResultScreen(bool aToggle)
		{
			Results.SetActive(aToggle);
			FadeInPlayerPicture(0);
			Sequence sequence = DOTween.Sequence();
			sequence.InsertCallback(1.5f, delegate
			{
				FadeInPlayerPicture(1);
			});
			sequence.InsertCallback(3.5f, ResultSentenceReact);
			return sequence;
		}

		public void SetPlayerResults(int aPlayerIndex, Sprite aImage, string aMessage)
		{
			PlayerResults[aPlayerIndex].SetResultsImage(aImage);
			PlayerResults[aPlayerIndex].SetResultsMessage(aMessage);
		}

		public void FadeInPlayerPicture(int aPlayerIndex)
		{
			PlayerResults[aPlayerIndex].FadeInImage();
		}

		public void SetCurrentPlayerDescription(string aResultSentence, string aResultText, string aDescription)
		{
			ResultSentence = aResultSentence;
			ResultText = aResultText;
			ResultDescription = aDescription;
		}

		public void SetResultSentence(string aResultSentence)
		{
			ResultSentenceText.text = aResultSentence;
		}

		public void SetCurrentPlayerDescription(string aDescription)
		{
			PlayerDescriptionText.text = aDescription;
		}

		public void DisplayFullDescription()
		{
			ResultSentenceText.text = ResultText;
			PlayerDescriptionText.text = ResultDescription;
		}

		public void DescriptionReact()
		{
			base.transform.DOShakeScale(1f, 0.0001f, 0, 0.5f);
			base.transform.DOShakeRotation(1f, 0.0001f, 0, 0.5f);
		}

		private void ResultSentenceReact()
		{
			DoneButton.SetActive(true);
			mRelationshipSentence.enabled = true;
			mRelationshipSentence.transform.DOShakeScale(1f, 0.3f, 2, 2f);
			mRelationshipSentence.transform.DOShakeRotation(1f, 5f, 2, 1f);
		}
	}
}
