using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Mix.Games.Tray.Fireworks
{
	public class SceneManager
	{
		private const float INTRO_MUSIC_DELAY = 0.1f;

		private List<Scene> mScenes = new List<Scene>();

		private int mCurrentSceneIndex;

		public Scene mCurrentScene;

		public void Initialize()
		{
			GameObject sceneParent = Toolbox.Instance.mFireworkGame.SceneParent;
			mScenes.Clear();
			mCurrentScene = null;
			if ((bool)sceneParent)
			{
				for (int i = 0; i < sceneParent.transform.childCount; i++)
				{
					mScenes.Add(sceneParent.transform.GetChild(i).GetComponent<Scene>());
				}
			}
		}

		public void ChangeScene(bool isLeft)
		{
			int num = mCurrentSceneIndex;
			if (isLeft)
			{
				mCurrentSceneIndex--;
				if (mCurrentSceneIndex < 0)
				{
					mCurrentSceneIndex = mScenes.Count - 1;
				}
			}
			else
			{
				mCurrentSceneIndex++;
				if (mCurrentSceneIndex >= mScenes.Count)
				{
					mCurrentSceneIndex = 0;
				}
			}
			if (mCurrentSceneIndex == num)
			{
				return;
			}
			if (mCurrentScene != null)
			{
				if (mCurrentScene.SceneButton != null)
				{
					mCurrentScene.SceneButton.interactable = false;
				}
				BaseGameController.Instance.Session.SessionSounds.StopSoundEvent(mCurrentScene.PreviewSong);
			}
			mCurrentScene = mScenes[mCurrentSceneIndex];
			if (mCurrentScene.SceneButton != null)
			{
				mCurrentScene.SceneButton.interactable = true;
			}
			BaseGameController.Instance.Session.SessionSounds.PlayMusic(mCurrentScene.PreviewSong);
		}

		public void ChangeScene(Scene scene)
		{
			if (!(scene != mCurrentScene))
			{
				return;
			}
			if (mCurrentScene != null)
			{
				if (mCurrentScene.SceneButton != null)
				{
					mCurrentScene.SceneButton.interactable = false;
				}
				BaseGameController.Instance.Session.SessionSounds.StopSoundEvent(mCurrentScene.PreviewSong);
			}
			mCurrentScene = scene;
			if (mCurrentScene.SceneButton != null)
			{
				mCurrentScene.SceneButton.interactable = true;
			}
			mCurrentSceneIndex = GetSceneIndex(scene);
			DOVirtual.DelayedCall(0.1f, delegate
			{
				BaseGameController.Instance.Session.SessionSounds.PlayMusic(mCurrentScene.PreviewSong);
			});
		}

		public void ChooseFirstScene()
		{
			if (mScenes != null && mScenes.Count >= 1)
			{
				ChangeScene(mScenes[0]);
			}
		}

		public Scene GetSceneAtIndex(int index)
		{
			return mScenes.Where((Scene x) => x.SceneID == index).FirstOrDefault();
		}

		public int GetSceneIndex(Scene scene)
		{
			return scene.SceneID;
		}

		public void Purge()
		{
			ChangeScene(mScenes[0]);
		}
	}
}
