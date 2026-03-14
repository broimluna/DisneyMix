using System;
using System.Collections.Generic;
using DG.Tweening;
using Mix.Games.Session;
using Mix.Games.Tray.Hints;
using Mix.Games.Tray.ObjectPool;
using UnityEngine;

namespace Mix.Games.Tray.HighFive
{
	public class HighFivePlayMode : MonoBehaviour
	{
		protected const string COUNTDOWN_MARKER = "# Countdown";

		protected const float INTRO_ANIMATION_TIME = 2.5f;

		protected const float MIN_MISS_TIME = 0.5f;

		public HighFiveGameController GameController;

		public Camera GameCamera;

		public Camera HitMarkerCamera;

		public Transform EffectSpawnPlane;

		public GameObject[] HitMarkers;

		public GameObject[] HitVfx;

		public float ShakeTime = 0.25f;

		public float[] ShakePosMagnitude;

		[Header("Character")]
		public HighFiveCharacter Head;

		public HighFiveTarget[] HitTargets;

		[Header("UI")]
		public HighFiveStats GameStats;

		public UICounter ScoreCounter;

		public UICounter StreakCounter;

		public HighFiveUiMultiplier MultiplierUi;

		public GenericGameTooltip TopHint;

		public GenericGameTooltip BottomHint;

		public GameObject GameplayUIPanel;

		public RectTransform GameplayUITransform;

		public HighFiveGameOverPanel GameOverPanel;

		public HighFiveBackground Background;

		public Color AwesomeBGColor = Color.yellow;

		public Color GoodBGColor = Color.green;

		public Transform GameOverCameraTransform;

		public float GameOverCameraTime = 0.5f;

		protected RaycastHit2D[] mHitResults = new RaycastHit2D[4];

		protected float cameraZDist;

		protected float missTimeStamp = -0.5f;

		protected int mRandomSeed;

		protected System.Random mRandomGenerator;

		protected bool mIsFirstPlayer;

		public TextAsset CommandFile;

		protected List<string> mCommandList;

		protected int mCurCommandIndex = -1;

		protected Dictionary<string, string> mCommandVars;

		protected int mBeatsExpected;

		protected int mBeatsCompleted;

		protected List<int> mScratchCandidateList = new List<int>(4);

		protected float mCurWindupTime = 0.25f;

		protected float mCurGrowTime = 0.5f;

		protected float mCurShrinkTime = 0.25f;

		protected float mCurPerBeatDelay = 0.25f;

		protected bool mAutoHideTarget = true;

		protected float mAverageScoreScale;

		protected int mCountdownCommand;

		protected bool mShouldPlayCountdown;

		protected int mLastPlayedCommand;

		protected bool mIsGameOver;

		public void Init(int aSeed, bool aIsFirstPlayer)
		{
			if (aIsFirstPlayer)
			{
				string text = Time.realtimeSinceStartup.ToString();
				mRandomSeed = text.GetHashCode();
			}
			else
			{
				mRandomSeed = aSeed;
			}
			GameStats.BestScore = 0;
			mIsFirstPlayer = aIsFirstPlayer;
			InitCommands();
		}

		protected void Start()
		{
			for (int i = 0; i < HitTargets.Length; i++)
			{
				HitTargets[i].Init();
			}
			for (int j = 0; j < HitMarkers.Length; j++)
			{
				ObjectPoolManager.CreatePool(HitMarkers[j], 6);
			}
			HitMarkerCamera.orthographicSize = Mathf.Tan((float)Math.PI / 360f * GameCamera.fieldOfView) * (1f - HitMarkerCamera.transform.position.z);
			cameraZDist = EffectSpawnPlane.position.z - GameCamera.transform.position.z;
			mAverageScoreScale = 1f / (float)HighFiveStats.PointVals[2];
			Head.SetAverageScore(1f);
			if (mCommandList == null)
			{
				Init(0, true);
			}
			StartGame();
		}

		protected void StartGame()
		{
			mRandomGenerator = new System.Random(mRandomSeed);
			GameplayUIPanel.SetActive(false);
			GameOverPanel.Hide();
			GameStats = new HighFiveStats();
			GameStats.Reset();
			Sequence s = DOTween.Sequence();
			s.AppendInterval(1f);
			s.AppendCallback(delegate
			{
				GameController.AudioManager.PlaySound("IntroMove");
			});
			s.AppendInterval(1f);
			s.AppendCallback(ShowUI);
			s.AppendInterval(0.75f);
			s.AppendCallback(StartGameSequence);
		}

		protected void SpawnHitMarker(HitType aType, Vector3 aPos, int aPointVal, bool aConvertToWorldSpace)
		{
			if (aConvertToWorldSpace)
			{
				aPos.z = cameraZDist;
				aPos = GameCamera.ScreenToWorldPoint(aPos);
			}
			GameObject aPrefab = HitMarkers[(int)aType];
			GameObject gameObject = ObjectPoolManager.InstantiatePooledObj(aPrefab, aPos, Quaternion.identity);
			if (aType != HitType.Miss)
			{
				gameObject.GetComponent<HighFiveHitMarker>().SetPointValue(aPointVal);
			}
			gameObject.transform.SetParent(EffectSpawnPlane);
			GameObject gameObject2 = HitVfx[(int)aType];
			if (gameObject2 != null)
			{
				GameObject gameObject3 = UnityEngine.Object.Instantiate(gameObject2, aPos, Quaternion.identity) as GameObject;
				gameObject3.transform.SetParent(EffectSpawnPlane);
			}
		}

		protected int ProcessHit(HitType aHitType)
		{
			if (aHitType == HitType.TooFast || aHitType == HitType.TooSlow)
			{
				GameController.AudioManager.PlaySound("Hit_OK");
			}
			switch (aHitType)
			{
			case HitType.Awesome:
				Background.DoFlash(AwesomeBGColor);
				GameController.AudioManager.PlaySound("Hit_Great");
				break;
			case HitType.Good:
				Background.DoFlash(GoodBGColor);
				GameController.AudioManager.PlaySound("Hit_Good");
				break;
			}
			int result = GameStats.RecordOneHit(aHitType);
			if (GameStats.TotalHits == 1 && !StreakCounter.gameObject.activeSelf)
			{
				StreakCounter.gameObject.SetActive(true);
			}
			StreakCounter.CountToTargetValue(GameStats.TotalHits, 0.25f);
			ScoreCounter.CountToTargetValue(GameStats.CurScore, 0.25f);
			float averageScore = mAverageScoreScale * GameStats.GetCurrentRunningAverage();
			Head.SetAverageScore(averageScore);
			if (aHitType != HitType.Miss)
			{
				Head.OnHitTarget();
				DOVirtual.DelayedCall(0.25f, delegate
				{
					((HighFiveGameController)BaseGameController.Instance).AudioManager.PlaySound("RightHandMeterUp");
				});
			}
			return result;
		}

		protected void StartGameSequence()
		{
			UpdateHitTargetEvents(true);
			DoNextCommand();
		}

		protected void InitCommands()
		{
			string[] array = CommandFile.text.Split(new string[2] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			mCommandList = new List<string>(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i][0] != '#')
				{
					mCommandList.Add(array[i]);
				}
				if (string.Equals(array[i], "# Countdown"))
				{
					mCountdownCommand = mCommandList.Count;
				}
			}
			mCommandVars = new Dictionary<string, string>();
		}

		protected void DoNextCommand()
		{
			mCurCommandIndex++;
			if (mShouldPlayCountdown && mCurCommandIndex >= mCountdownCommand && mCurCommandIndex <= mCommandList.Count)
			{
				DoCountdownCommand();
			}
			else
			{
				DoGameCommand();
			}
		}

		protected void DoCountdownCommand()
		{
			if (mCurCommandIndex == mCommandList.Count)
			{
				mShouldPlayCountdown = false;
				mCurCommandIndex = mLastPlayedCommand - 1;
				DoNextCommand();
				return;
			}
			mBeatsCompleted = 0;
			mBeatsExpected = ParseCommand(mCommandList[mCurCommandIndex]);
			if (mBeatsExpected == 0)
			{
				DoNextCommand();
			}
		}

		protected void DoGameCommand()
		{
			if (mCommandList != null && mCurCommandIndex < mCommandList.Count - 1 && mCurCommandIndex >= 0 && mCurCommandIndex < mCountdownCommand)
			{
				mBeatsCompleted = 0;
				mBeatsExpected = ParseCommand(mCommandList[mCurCommandIndex]);
				if (mBeatsExpected == 0)
				{
					DoNextCommand();
				}
			}
			else
			{
				GameOver();
			}
		}

		protected int ParseCommand(string aCmd)
		{
			int num = 0;
			switch (aCmd[0])
			{
			case '?':
				num = (int)char.GetNumericValue(aCmd, 1);
				DoRandomChord(num, ref mScratchCandidateList);
				break;
			case '+':
				num = DoChord(aCmd);
				break;
			case ',':
				num = DoSequence(aCmd);
				break;
			case '|':
				num = DoSelection(aCmd);
				break;
			case '^':
				mCurWindupTime = float.Parse(aCmd.Substring(1));
				break;
			case '<':
				mCurGrowTime = float.Parse(aCmd.Substring(1));
				break;
			case '>':
				mCurShrinkTime = float.Parse(aCmd.Substring(1));
				break;
			case '=':
				mCurPerBeatDelay = float.Parse(aCmd.Substring(1));
				break;
			case '.':
			{
				float delay = float.Parse(aCmd.Substring(1));
				num = 1;
				DOVirtual.DelayedCall(delay, DoNextCommand);
				break;
			}
			case '$':
			{
				string text = aCmd.Substring(1);
				string value;
				if (mCommandVars.TryGetValue(text, out value))
				{
					num = ParseCommand(value);
					break;
				}
				Debug.LogWarningFormat("Could not find definition for command variable '{0}'", text);
				break;
			}
			case '{':
				UpdateTooltip(TopHint, aCmd);
				Head.ResetLookAt();
				break;
			case '[':
				UpdateTooltip(BottomHint, aCmd);
				Head.ResetLookAt();
				break;
			case '!':
			{
				int num3 = int.Parse(aCmd.Substring(1));
				mAutoHideTarget = num3 == 0;
				break;
			}
			case '%':
				num = DoSelectionByScore(aCmd);
				break;
			default:
				if (char.IsNumber(aCmd[0]))
				{
					num = 1;
					int num2 = (int)char.GetNumericValue(aCmd, 0);
					ShowTargetAndLook(HitTargets[num2]);
				}
				else if (char.IsLetter(aCmd[0]))
				{
					AddVariable(aCmd);
				}
				else
				{
					Debug.LogWarningFormat("Unknown command: {0}", aCmd);
				}
				break;
			}
			return num;
		}

		protected void DoRandomChord(int aNumTargets, ref List<int> aCandidates)
		{
			SelectPermutation(aNumTargets, HitTargets.Length, ref aCandidates);
			for (int i = 0; i < aCandidates.Count; i++)
			{
				ShowTarget(HitTargets[aCandidates[i]]);
			}
			LookAtAverageTarget(aCandidates);
		}

		protected int DoChord(string aCmdStr)
		{
			List<int> list = new List<int>(aCmdStr.Length - 1);
			for (int i = 1; i < aCmdStr.Length; i++)
			{
				int num = (int)char.GetNumericValue(aCmdStr, i);
				list.Add(num);
				ShowTarget(HitTargets[num]);
			}
			LookAtAverageTarget(list);
			return aCmdStr.Length - 1;
		}

		protected int DoSequence(string aCmdStr)
		{
			float num = Mathf.Max(0.25f, mCurGrowTime);
			Sequence s = DOTween.Sequence();
			for (int i = 1; i < aCmdStr.Length; i++)
			{
				int index = (int)char.GetNumericValue(aCmdStr, i);
				s.InsertCallback(num * (float)(i - 1), delegate
				{
					ShowTargetAndLook(HitTargets[index]);
				});
			}
			return aCmdStr.Length - 1;
		}

		protected int DoSelection(string aCmdStr)
		{
			string[] array = aCmdStr.Split(new char[1] { '|' }, StringSplitOptions.RemoveEmptyEntries);
			int num = mRandomGenerator.Next(array.Length);
			return ParseCommand(array[num]);
		}

		protected void AddVariable(string aCmdStr)
		{
			string[] array = aCmdStr.Split('=');
			if (array.Length == 2)
			{
				string key = array[0].Trim();
				string value = array[1].Trim();
				mCommandVars.Add(key, value);
			}
			else
			{
				Debug.LogWarningFormat("Trying to create variable '{0}', but it does not match the expected format 'VARNAME=<commandlist>'");
			}
		}

		protected void SelectPermutation(int aNumSamples, int aSetSize, ref List<int> aResults)
		{
			aResults.Clear();
			for (int i = aSetSize - aNumSamples; i < aSetSize; i++)
			{
				int item = mRandomGenerator.Next(0, i);
				if (!aResults.Contains(item))
				{
					aResults.Add(item);
				}
				else
				{
					aResults.Add(i);
				}
			}
		}

		protected void UpdateTooltip(GenericGameTooltip aTip, string aCmd)
		{
			GameController.AudioManager.PlaySound("TextBubble");
			if (aCmd.Length > 2)
			{
				string text = aCmd.Substring(1, aCmd.Length - 2);
				if (aTip.IsFullyShowing)
				{
					aTip.SetTextWithBounce(text);
					return;
				}
				aTip.text = text;
				aTip.Show();
				if (mAutoHideTarget)
				{
					Head.StartConversation();
				}
			}
			else
			{
				aTip.Hide(true);
				if (mAutoHideTarget)
				{
					Head.StopConversation();
				}
			}
		}

		protected int DoSelectionByScore(string aCmdStr)
		{
			string[] array = aCmdStr.Split(new char[1] { '%' }, StringSplitOptions.RemoveEmptyEntries);
			float num = mAverageScoreScale * (float)GameStats.CurScore / (float)GameStats.TotalBeats;
			int value = 4;
			if (num <= 2f)
			{
				num = 0f;
			}
			else if (num <= 4f)
			{
				value = 1;
			}
			else if (num <= 8f)
			{
				value = 2;
			}
			else if (num <= 12f)
			{
				value = 3;
			}
			value = Mathf.Clamp(value, 0, array.Length - 1);
			return ParseCommand(array[value]);
		}

		protected void TargetHit(HighFiveTarget aTarget, HitType aHitType, Vector3 aHitLocation)
		{
			int aPointVal = ProcessHit(aHitType);
			SpawnHitMarker(aHitType, aTarget.GetDefaultHitMarkerWorldPos(), aPointVal, false);
			if (aHitType != HitType.Miss)
			{
				GameCamera.DOShakePosition(ShakeTime, ShakePosMagnitude[(int)aHitType]);
			}
		}

		protected void TargetComplete(HighFiveTarget aTarget, HitType aHitType)
		{
			mBeatsCompleted++;
			if (mBeatsCompleted == mBeatsExpected)
			{
				DOVirtual.DelayedCall(mCurShrinkTime + mCurPerBeatDelay, delegate
				{
					DoNextCommand();
				});
			}
		}

		protected void ShowTarget(HighFiveTarget aTarget)
		{
			aTarget.ShowHand(mCurWindupTime, mCurGrowTime, mCurShrinkTime, mAutoHideTarget);
		}

		protected void ShowTargetAndLook(HighFiveTarget aTarget)
		{
			aTarget.ShowHand(mCurWindupTime, mCurGrowTime, mCurShrinkTime, mAutoHideTarget);
			Head.LookAtTarget(aTarget.transform.position);
		}

		protected void LookAtAverageTarget(List<int> aTargetList)
		{
			Vector3 zero = Vector3.zero;
			for (int i = 0; i < aTargetList.Count; i++)
			{
				zero += HitTargets[aTargetList[i]].transform.position;
			}
			zero /= (float)aTargetList.Count;
			Head.LookAtTarget(zero);
		}

		protected void ShowUI()
		{
			GameController.AudioManager.PlaySound("DropDown");
			GameplayUIPanel.SetActive(true);
			GameplayUITransform.DOLocalMoveY(GameplayUITransform.localPosition.y + 40f, 0.5f).From().SetEase(Ease.OutBack);
			StreakCounter.JumpToValue(0, false);
			StreakCounter.gameObject.SetActive(false);
			ScoreCounter.JumpToValue(0, false);
			MultiplierUi.Show();
			HighFiveStats gameStats = GameStats;
			gameStats.OnMultiplierChanged = (Action<int>)Delegate.Combine(gameStats.OnMultiplierChanged, new Action<int>(MultiplierUi.UpdateMultiplier));
			HighFiveStats gameStats2 = GameStats;
			gameStats2.OnMultiplierProgressChanged = (Action<float>)Delegate.Combine(gameStats2.OnMultiplierProgressChanged, new Action<float>(MultiplierUi.UpdateProgress));
			TopHint.Hide();
			BottomHint.Hide();
			Animator component = GameCamera.GetComponent<Animator>();
			if (component != null)
			{
				component.enabled = false;
			}
		}

		public void GameOver()
		{
			mIsGameOver = true;
			Head.LookAtTarget(GameOverCameraTransform.position);
			GameplayUIPanel.SetActive(false);
			float rating = GameStats.GetRating();
			if (rating < 1f)
			{
				rating = 0.7f;
			}
			Sequence s = DOTween.Sequence();
			s.Append(GameCamera.transform.DOLocalMove(GameOverCameraTransform.localPosition, GameOverCameraTime).SetEase(Ease.InOutCubic));
			s.AppendInterval(0.5f);
			s.AppendCallback(delegate
			{
				Head.LookAtTarget(GameOverCameraTransform.position + 0.125f * Vector3.up);
				Head.SetAverageScore(rating);
				Head.OnHitTarget();
			});
			GameOverPanel.Show(0, GameStats);
		}

		public void SendData()
		{
			GameController.LogEvent(GameLogEventType.ACTION, "high_five_score", GameStats.CurScore.ToString());
			if (mIsFirstPlayer)
			{
				GameController.GameOverPost(GameStats, mRandomSeed);
			}
			else
			{
				GameController.GameOverResponse(GameStats);
			}
		}

		public void Pause()
		{
			if (!mIsGameOver)
			{
				UpdateHitTargetEvents(false);
				mShouldPlayCountdown = true;
				mLastPlayedCommand = mCurCommandIndex - 1;
				mCurCommandIndex = mCountdownCommand - 1;
				TopHint.Hide(true);
				BottomHint.Hide(true);
			}
		}

		public void Resume()
		{
			if (!mIsGameOver)
			{
				UpdateHitTargetEvents(true);
				DoNextCommand();
			}
		}

		protected void UpdateHitTargetEvents(bool shouldAdd)
		{
			for (int i = 0; i < HitTargets.Length; i++)
			{
				if (shouldAdd)
				{
					HitTargets[i].OnHit += TargetHit;
					HitTargets[i].OnComplete += TargetComplete;
				}
				else
				{
					HitTargets[i].OnHit -= TargetHit;
					HitTargets[i].OnComplete -= TargetComplete;
				}
			}
		}
	}
}
