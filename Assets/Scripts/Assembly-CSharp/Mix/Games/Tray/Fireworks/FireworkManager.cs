using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.Fireworks
{
	public class FireworkManager
	{
		public const int mMAX_NUM_OF_AVAILABLE_FIREWORKS = 6;

		private int mMaxNumOfParticularFirework;

		private int[] mInstanceIndex = new int[6];

		private Firework[,] mSpecificFireworks;

		public FireworkGesture mGestureFirework;

		public FireworksController mController;

		private bool mIsLoaded;

		public void SetMaxNumberofParticularFireworks(int amountAllowed)
		{
			mMaxNumOfParticularFirework = amountAllowed;
			mSpecificFireworks = new Firework[6, mMaxNumOfParticularFirework];
		}

		public void Purge()
		{
			if (!mIsLoaded)
			{
				return;
			}
			for (int i = 0; i < mSpecificFireworks.GetLength(0); i++)
			{
				for (int j = 0; j < mSpecificFireworks.GetLength(1); j++)
				{
					Object.Destroy(mSpecificFireworks[i, j].gameObject);
					mSpecificFireworks[i, j] = null;
				}
			}
			for (int k = 0; k < mInstanceIndex.Length; k++)
			{
				mInstanceIndex[k] = 0;
			}
			if ((bool)mGestureFirework)
			{
				Object.Destroy(mGestureFirework.gameObject);
			}
			mIsLoaded = false;
		}

		public void Load(Scene currentScene, FireworksGame.FireworksStage gameStage = FireworksGame.FireworksStage.End, FireworksGame game = null)
		{
			int num = 0;
			List<Firework> list = new List<Firework>();
			GameObject[] buttonFireworks = currentScene.ButtonFireworks;
			foreach (GameObject gameObject in buttonFireworks)
			{
				for (int j = 0; j < mMaxNumOfParticularFirework; j++)
				{
					GameObject gameObject2;
					if (num == 4 && currentScene.Alt5thFireworks.Count == mMaxNumOfParticularFirework)
					{
						gameObject2 = Object.Instantiate(currentScene.Alt5thFireworks[j]);
						gameObject2.name = currentScene.Alt5thFireworks[j].name + " " + j;
					}
					else
					{
						gameObject2 = Object.Instantiate(gameObject);
						gameObject2.name = gameObject.name + " " + j;
					}
					Firework component = gameObject2.GetComponent<Firework>();
					component.transform.SetParent(mController.transform.GetChild(0));
					component.OffScreenLocation = new Vector3(0f, -15f, 0f);
					gameObject2.GetComponent<RandomizeColors>().Colors = currentScene.FireworkColors;
					mSpecificFireworks[num, j] = component;
					mSpecificFireworks[num, j].transform.localPosition = component.OffScreenLocation;
					if (j == 0)
					{
						Firework item = mSpecificFireworks[num, j];
						list.Add(item);
					}
				}
				num++;
			}
			for (int k = 0; k < mMaxNumOfParticularFirework; k++)
			{
				GameObject gameObject3 = Object.Instantiate(currentScene.TapFirework);
				gameObject3.name = currentScene.TapFirework.name + " " + k;
				Firework component2 = gameObject3.GetComponent<Firework>();
				component2.transform.SetParent(mController.transform.GetChild(0));
				component2.OffScreenLocation = new Vector3(0f, -15f, 0f);
				component2.transform.localPosition = component2.OffScreenLocation;
				component2.gameObject.GetComponent<RandomizeColors>().Colors = currentScene.FireworkColors;
				mSpecificFireworks[num, k] = component2;
			}
			GameObject gameObject4 = Object.Instantiate(currentScene.GestureFirework);
			FireworkGesture component3 = gameObject4.GetComponent<FireworkGesture>();
			component3.transform.SetParent(mController.transform.GetChild(0));
			mGestureFirework = component3;
			mGestureFirework.transform.localPosition = Vector3.zero;
			if (game != null && gameStage == FireworksGame.FireworksStage.Creation)
			{
				for (int l = 0; l < list.Count; l++)
				{
					GameObject gameObject5 = Object.Instantiate(game.FireworkButtonPrefab);
					FireworkButton component4 = gameObject5.transform.GetComponent<FireworkButton>();
					component4.mFireworksGame = game;
					component4.mFireworkIndex = l;
					gameObject5.transform.GetChild(0).GetComponent<Image>().sprite = list[l].ButtonImage;
					gameObject5.transform.SetParent(game.FireworkButtonContainer.transform, false);
					for (int m = 0; m < mMaxNumOfParticularFirework; m++)
					{
						mSpecificFireworks[l, m].GetComponent<Firework>().LaunchButton = gameObject5.GetComponent<Button>();
					}
				}
			}
			mIsLoaded = true;
		}

		public bool LaunchFirework(int fireworkIndex, Vector3 location)
		{
			bool result = false;
			int num = 0;
			GameObject gameObject;
			if (fireworkIndex >= 6)
			{
				gameObject = mGestureFirework.gameObject;
				result = true;
				gameObject.transform.localPosition = location;
				gameObject.gameObject.SetActive(true);
			}
			else
			{
				num = mInstanceIndex[fireworkIndex];
				gameObject = mSpecificFireworks[fireworkIndex, num].gameObject;
			}
			if (gameObject != mGestureFirework.gameObject)
			{
				gameObject.GetComponent<Firework>().Launch(location);
				num++;
				if (num >= mMaxNumOfParticularFirework)
				{
					num = 0;
				}
				mInstanceIndex[fireworkIndex] = num;
			}
			if (Toolbox.Instance.mFireworkGame.mGameStage == FireworksGame.FireworksStage.Creation)
			{
				Firework component = mSpecificFireworks[fireworkIndex, num].GetComponent<Firework>();
				if (fireworkIndex < 5 && component.ButtonImage != component.LaunchButton.transform.GetChild(0).GetComponent<Image>().sprite)
				{
					component.LaunchButton.transform.GetChild(0).GetComponent<Image>().sprite = component.ButtonImage;
				}
			}
			FireworksGame fireworksGame = (FireworksGame)mController.MixGame;
			if (fireworkIndex < 5 && fireworksGame.mGameStage == FireworksGame.FireworksStage.Creation && AllFireworksAtIndexUsed(fireworkIndex))
			{
				gameObject.GetComponent<Firework>().LaunchButton.interactable = false;
			}
			return result;
		}

		private bool AllFireworksAtIndexUsed(int fireworkIndex)
		{
			bool result = true;
			for (int i = 0; i < mMaxNumOfParticularFirework; i++)
			{
				if (!mSpecificFireworks[fireworkIndex, i].GetComponent<Firework>().mIsPlaying)
				{
					result = false;
					break;
				}
			}
			return result;
		}

		public bool FireworksFinished()
		{
			bool result = true;
			for (int i = 0; i < 6; i++)
			{
				int num = mInstanceIndex[i];
				num = ((num != 0) ? (num - 1) : (mMaxNumOfParticularFirework - 1));
				Firework component = mSpecificFireworks[i, num].transform.GetComponent<Firework>();
				if (component.mIsPlaying)
				{
					result = false;
					break;
				}
			}
			return result;
		}

		public Vector3 GetTouchOnTapPlane()
		{
			return Toolbox.Instance.mSceneManager.mCurrentScene.TapPlane.GetTouchOnTapPlane(Toolbox.Instance.mFireworkGame.GameController.MixGameCamera);
		}

		public Vector3 GetRandomFireworkLocation()
		{
			return Toolbox.Instance.mSceneManager.mCurrentScene.GenerationArea.GetRandomFireworkLocation();
		}
	}
}
