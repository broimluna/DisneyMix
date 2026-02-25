using System;
using System.Collections.Generic;
using DG.Tweening;
using LitJson;
using Mix.Games.Session;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.SecretWord
{
	public class SecretWordCreateWordPanel : MonoBehaviour, IMixGameDataRequest, IGameModerationResult
	{
		public delegate void ValidateTextCallback(string aText, bool isTextValid);

		private string mWord;

		private string mHint;

		private List<Secret_Word_Data> mWordSuggestions;

		private string mLastSuggestion;

		public SecretWordGameController gameController;

		public SecretWordGame game;

		public Button nextButton;

		public Button randomButton;

		public GameObject nextButtonSpinner;

		public GameObject nextButtonIcon;

		public SecretWordCreateHintPanel enterHintPanel;

		public SecretWordHints hints;

		public float warningMessageDuration = 3f;

		public float delayBeforeShowingHint = 0.5f;

		private Tween mWarningTween;

		void IGameModerationResult.OnModerationResult(bool aIsModerated, string aModeratedText, object aUserData)
		{
			if (this.IsNullOrDisposed() || base.gameObject.IsNullOrDisposed())
			{
				return;
			}
			EnableValidationSpinner(false);
			if (!aIsModerated)
			{
				if (mWord != mLastSuggestion)
				{
					mHint = string.Empty;
				}
				game.Word = mWord;
				game.Hint = mHint;
				ShowHintPanel();
			}
			else
			{
				game.wordArea.ExplodeTiles();
				randomButton.interactable = true;
				ShowInvalidWordWarning();
			}
		}

		void IGameModerationResult.OnModerationError(object aUserData)
		{
			if (!this.IsNullOrDisposed() && !base.gameObject.IsNullOrDisposed() && DebugSceneIndicator.IsMainScene)
			{
				EnableValidationSpinner(false);
				game.gameController.PauseOnNetworkError();
			}
		}

		private void Start()
		{
			game.wordArea.TilesUpdated += HandleTilesUpdated;
			hints.HintText = GetObjectiveHint();
			GetWordSuggestions();
			if (hints != null && hints.secretWordHint != null)
			{
				hints.secretWordHint.Hide(true);
				DOVirtual.DelayedCall(delayBeforeShowingHint, delegate
				{
					hints.secretWordHint.Show();
				});
			}
			nextButton.interactable = false;
			ShakeDetector.StartShakeDetection(base.gameObject, OnShake, 0.25f);
		}

		private string GetObjectiveHint()
		{
			if (DebugSceneIndicator.IsMainScene)
			{
				if (gameController.IsGroupSession)
				{
					return BaseGameController.Instance.Session.GetLocalizedString("customtokens.secretword.create_word_group");
				}
				string friendName = gameController.FriendName;
				return string.Format(BaseGameController.Instance.Session.GetLocalizedString("customtokens.secretword.create_word"), friendName);
			}
			return string.Format(BaseGameController.Instance.Session.GetLocalizedString("customtokens.secretword.create_word"), "Honey Badger");
		}

		public void GetWordSuggestions()
		{
			if (mWordSuggestions == null)
			{
				if (DebugSceneIndicator.IsMainScene)
				{
					gameController.LoadData(this, "data/Writers_Block_Data/Writers_Block_Data.gz", "Writers_Block_Data.txt", ParseJsonData);
				}
				else
				{
					mWordSuggestions = GetDebugWritersBlockData();
				}
			}
		}

		public void ShowInvalidWordWarning()
		{
			hints.secretWordHint.SetTextWithBounce(BaseGameController.Instance.Session.GetLocalizedString("customtokens.secretword.invalid_word_warning"));
			if (mWarningTween != null)
			{
				mWarningTween.Complete();
			}
			mWarningTween = DOVirtual.DelayedCall(warningMessageDuration, delegate
			{
				hints.secretWordHint.SetTextWithBounce(GetObjectiveHint());
			});
		}

		private void HandleTilesUpdated(int numTiles)
		{
			nextButton.interactable = numTiles >= 3 && randomButton.IsInteractable();
		}

		public void OnNextPressed()
		{
			EnableValidationSpinner(true);
			randomButton.interactable = false;
			mWord = game.wordArea.GetWord();
			BaseGameController.Instance.Session.SessionSounds.PlaySoundEvent("SecretWord/SFX/ButtonUI");
			game.gameController.ModerateText(mWord, this, null);
		}

		public void OnHintCancelled()
		{
			BaseGameController.Instance.Session.SessionSounds.PlaySoundEvent("SecretWord/SFX/ButtonUI");
			HideHintPanel();
			randomButton.interactable = true;
		}

		public void OnRandomPressed()
		{
			if (!BaseGameController.Instance.IsNullOrDisposed() && BaseGameController.Instance.Session != null && BaseGameController.Instance.Session.SessionSounds != null)
			{
				BaseGameController.Instance.Session.SessionSounds.PlaySoundEvent("SecretWord/SFX/ButtonUI");
			}
			if (!randomButton.IsNullOrDisposed())
			{
				randomButton.interactable = false;
			}
			if (!game.IsNullOrDisposed() && !game.wordArea.IsNullOrDisposed() && mWordSuggestions != null)
			{
				game.wordArea.ExplodeTiles();
				int num = UnityEngine.Random.Range(0, mWordSuggestions.Count);
				string text = mWordSuggestions[num].word.ToUpper();
				string hint = mWordSuggestions[num].hint;
				if (text == mLastSuggestion)
				{
					num = (num + 1) % mWordSuggestions.Count;
					text = mWordSuggestions[num].word.ToUpper();
					hint = mWordSuggestions[num].hint;
				}
				mLastSuggestion = text;
				mWord = text;
				mHint = hint;
				game.wordArea.PopInWord(text, FinishRandomize);
			}
		}

		private void FinishRandomize()
		{
			nextButton.interactable = true;
			randomButton.interactable = true;
		}

		private void EnableValidationSpinner(bool enable)
		{
			if (!this.IsNullOrDisposed() && !base.gameObject.IsNullOrDisposed())
			{
				nextButtonSpinner.SetActive(enable);
				nextButtonIcon.SetActive(!enable);
				randomButton.interactable = !enable;
				game.DisableInput = enable;
			}
		}

		private void OnShake()
		{
			if (randomButton.IsInteractable())
			{
				randomButton.interactable = false;
				DOVirtual.DelayedCall(0.25f, OnRandomPressed);
			}
		}

		private void ShowHintPanel()
		{
			enterHintPanel.word = mWord;
			enterHintPanel.ShowPanel();
			gameController.MixGameCamera.gameObject.SetActive(false);
		}

		private void HideHintPanel()
		{
			gameController.MixGameCamera.gameObject.SetActive(true);
			enterHintPanel.HidePanel();
		}

		public List<Secret_Word_Data> GetDebugWritersBlockData()
		{
			List<Secret_Word_Data> list = new List<Secret_Word_Data>();
			list.Add(new Secret_Word_Data
			{
				uid = "writers_block_1",
				word = "ALADDIN",
				hint = "Three wishes and a monkey"
			});
			list.Add(new Secret_Word_Data
			{
				uid = "writers_block_2",
				word = "ARIEL",
				hint = "Dreams about going up there"
			});
			list.Add(new Secret_Word_Data
			{
				uid = "writers_block_3",
				word = "AURORA",
				hint = "Sleepy princess"
			});
			list.Add(new Secret_Word_Data
			{
				uid = "writers_block_4",
				word = "BAMBI",
				hint = "Prince of the forest"
			});
			return list;
		}

		public void OnDataLoaded(object aObject, object aUserData)
		{
			ContentObj contentObj = aObject as ContentObj;
			try
			{
				if (contentObj.content.objects.wbd.Count == 0)
				{
					gameController.PauseOnNetworkError();
					return;
				}
				mWordSuggestions = contentObj.content.objects.wbd;
				randomButton.interactable = true;
			}
			catch (Exception)
			{
				gameController.PauseOnNetworkError();
			}
		}

		public object ParseJsonData(string json)
		{
			return JsonMapper.ToObject<ContentObj>(json);
		}
	}
}
