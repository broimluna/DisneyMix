using DG.Tweening;
using Fabric;
using Mix.Games.Data;
using Mix.Games.Tray.Common;
using UnityEngine;

namespace Mix.Games.Tray.SecretWord
{
	public class SecretWordGame : MonoBehaviour, IMixGame
	{
		private const string MUSIC_CUE_NAME = "SecretWord/MUS/Music";

		private bool mDidTipTextFade;

		public SecretWordGameController gameController;

		public TileGrid tileGrid;

		public WordArea wordArea;

		public SecretWordCreateWordPanel createWordPanel;

		public SecretWordGuessWordPanel guessPanel;

		public float gameOverDelay = 3f;

		public float tipTextFadeOutTime = 0.25f;

		[Space(10f)]
		public int maxWordLength = 8;

		public int maxHearts = 5;

		private int numLettersLeft;

		private int numLives = 7;

		public SecretWordGameStates GameState { get; set; }

		public string Word { get; set; }

		public string Hint { get; set; }

		public bool DisableInput { get; set; }

		void IMixGame.Initialize(MixGameData aData)
		{
			if (DebugSceneIndicator.IsMainScene)
			{
				if (aData == null)
				{
					EnterCreateState();
					return;
				}
				EnterGuessState();
				Hint = gameController.GetGameData<SecretWordData>().Hint;
				SetupWord(gameController.GetGameData<SecretWordData>().Word);
			}
		}

		void IMixGame.Pause()
		{
			BaseGameController.Instance.Session.SessionSounds.PauseSoundEvent("SecretWord/MUS/Music", base.gameObject);
			DOTween.PauseAll();
		}

		void IMixGame.Play()
		{
			DOVirtual.DelayedCall(0.1f, delegate
			{
				BaseGameController.Instance.Session.SessionSounds.PlayMusic("SecretWord/MUS/Music", base.gameObject);
			});
		}

		void IMixGame.Quit()
		{
			BaseGameController.Instance.Session.SessionSounds.StopSoundEvent("SecretWord/MUS/Music", base.gameObject);
			DOTween.KillAll();
		}

		void IMixGame.Resume()
		{
			DOVirtual.DelayedCall(0.1f, delegate
			{
				BaseGameController.Instance.Session.SessionSounds.UnpauseSoundEvent("SecretWord/MUS/Music", base.gameObject);
			});
			DOTween.PlayAll();
			if (GameState != SecretWordGameStates.GUESS)
			{
				createWordPanel.GetWordSuggestions();
			}
		}

		private void OnEnable()
		{
			TrayGamePhysics.ResetAllGameLayers();
		}

		private void Start()
		{
			DisableInput = false;
		}

		private void EnterCreateState()
		{
			GameState = SecretWordGameStates.CREATE;
			createWordPanel.gameObject.SetActive(true);
			guessPanel.gameObject.SetActive(false);
			wordArea.SetupForWordCreate(maxWordLength);
		}

		private void EnterGuessState()
		{
			GameState = SecretWordGameStates.GUESS;
			createWordPanel.gameObject.SetActive(false);
			guessPanel.gameObject.SetActive(true);
		}

		public void SendDataAndQuit()
		{
			SecretWordData gameData = gameController.GetGameData<SecretWordData>();
			gameData.Word = Word;
			gameData.Hint = Hint;
			gameController.GameOver(gameData);
		}

		private void GameOver(bool aResult)
		{
			DisableInput = true;
			guessPanel.ShowGameOverPanel(aResult);
			if (aResult)
			{
				wordArea.CelebrateAllTiles();
			}
			if (DebugSceneIndicator.IsMainScene)
			{
				SecretWordData data = gameController.GetGameData<SecretWordData>();
				SecretWordResponse response = data.GetMyResponse(data.Responses, gameController.PlayerId);
				response.Success = aResult;
				response.Attempts++;
				DOVirtual.DelayedCall(gameOverDelay, delegate
				{
					FinishGameOver(data, response);
				}, false);
			}
		}

		private void FinishGameOver(SecretWordData data, SecretWordResponse response)
		{
			gameController.GameOver(response);
		}

		public void PlayerGuessWordSuccess()
		{
			GameOver(true);
		}

		public void PlayerGuessWordFail()
		{
			GameOver(false);
		}

		public void OnTileClicked(Tile aTile)
		{
			if (DisableInput)
			{
				aTile.PunchScale();
			}
			else if (GameState == SecretWordGameStates.CREATE)
			{
				if (aTile.IsInGrid())
				{
					int firstFreeSlot = wordArea.GetFirstFreeSlot();
					if (firstFreeSlot >= 0)
					{
						TileSlot slotByIndex = wordArea.GetSlotByIndex(firstFreeSlot);
						wordArea.InsertTileAtIndex(firstFreeSlot, aTile);
						BaseGameController.Instance.Session.SessionSounds.PlaySoundEvent("SecretWord/SFX/TapLetterBlock");
						aTile.FlyToSlot(slotByIndex);
					}
					wordArea.SendTileCount();
				}
				else if (aTile.IsInSlot())
				{
					aTile.Explode();
				}
			}
			else if (aTile.IsInGrid())
			{
				string text = aTile.letter.text.ToUpper();
				if (Word.Contains(text))
				{
					AnimateGuesses(aTile, text[0]);
					return;
				}
				BaseGameController.Instance.Session.SessionSounds.PlaySoundEvent("SecretWord/SFX/IncorrectLetterGuessed");
				numLives--;
				guessPanel.LoseHeart(numLives);
				aTile.Explode(true);
				if (numLives == 0)
				{
					PlayerGuessWordFail();
				}
			}
			else
			{
				aTile.PunchScale();
			}
		}

		private void AnimateGuesses(Tile aTile, char aLetter)
		{
			Vector3 startPos = aTile.homePosition;
			Quaternion startRot = aTile.transform.rotation;
			int num = Word.IndexOf(aLetter, 0);
			AnimateOneGuess(aTile, num);
			numLettersLeft--;
			Sequence s = DOTween.Sequence();
			while (true)
			{
				s.AppendInterval(0.25f);
				num = Word.IndexOf(aLetter, num + 1);
				if (num > 0)
				{
					int index = num;
					s.AppendCallback(delegate
					{
						AnimateOneGuess(aLetter, startPos, startRot, index);
					});
					numLettersLeft--;
					continue;
				}
				break;
			}
			if (numLettersLeft <= 0)
			{
				s.AppendCallback(PlayerGuessWordSuccess);
			}
		}

		private void AnimateOneGuess(char aLetter, Vector3 startPos, Quaternion startRot, int slotIndex)
		{
			Tile aTile = CreateNewTileWithPositionAndRotation(tileGrid.transform, aLetter, startPos, startRot, false);
			AnimateOneGuess(aTile, slotIndex);
		}

		private void AnimateOneGuess(Tile aTile, int charIndex)
		{
			wordArea.InsertTileAtIndex(charIndex, aTile);
			TileSlot aTarget = wordArea.m_slots[charIndex];
			aTile.FlyToSlot(aTarget);
			EventManager.Instance.PostEvent("SecretWord/SFX/CorrectLetterGuessed", aTile.gameObject);
		}

		public Tile CreateNewTile(Transform aParentTransform, char aLetter)
		{
			return CreateNewTileWithPositionAndRotation(aParentTransform, aLetter, aParentTransform.position, aParentTransform.rotation, true);
		}

		public Tile CreateNewTileWithPositionAndRotation(Transform aParent, char aLetter, Vector3 aPosition, Quaternion aRotation, bool aKeepWorldPosition)
		{
			GameObject gameObject = Object.Instantiate(tileGrid.tilePrefab, aPosition, aRotation) as GameObject;
			Transform transform = gameObject.transform;
			transform.SetParent(aParent, aKeepWorldPosition);
			Tile component = gameObject.GetComponent<Tile>();
			component.Setup(this, tileGrid, wordArea, aLetter);
			component.homePosition = aPosition;
			return component;
		}

		private void SetupWord(string theWord)
		{
			Word = theWord.ToUpper();
			numLettersLeft = theWord.Length;
			wordArea.SetupForWordGuess(numLettersLeft);
			numLives = maxHearts;
			guessPanel.Init(numLives);
		}
	}
}
