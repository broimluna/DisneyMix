using System.Collections;
using Mix.Games.Session;
using Mix.Games.Tray.Friendzy.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.Friendzy.LoadAsset
{
	public class Loader : MonoBehaviour, IGameAsset
	{
		private FriendzyGame mFriendzyGame;

		void IGameAsset.OnGameSessionAssetLoaded(object aUserData)
		{
			Hashtable hashtable = aUserData as Hashtable;
			JobEntry jobEntry = hashtable["Parameter"] as JobEntry;
			if (!BaseGameController.Instance.IsNullOrDisposed())
			{
				GameObject gameObject = (GameObject)BaseGameController.Instance.GetBundleInstance(jobEntry.PictureEntry.GetImageURL());
				gameObject.transform.SetParent(base.transform, false);
				if (jobEntry.PictureEntry.GetPictureType() == PictureType.SPRITE)
				{
					jobEntry.PictureEntry.SetPicture(gameObject.GetComponent<Image>().sprite);
				}
				else if (jobEntry.PictureEntry.GetPictureType() == PictureType.TEXTURE)
				{
					jobEntry.PictureEntry.SetTexture(gameObject.GetComponent<Image>().sprite.texture);
				}
				jobEntry.EntryJob.NumberOfItems--;
				if (jobEntry.EntryJob.NumberOfItems == 0)
				{
					jobEntry.EntryJob.CallBackWhenFinishedJob(jobEntry.EntryJob);
				}
			}
		}

		private void Awake()
		{
			mFriendzyGame = base.gameObject.GetComponent<FriendzyGame>();
		}

		private void Destroy()
		{
			mFriendzyGame.GameController.CancelBundles();
		}

		public void LoadJob(Job job)
		{
			if (job.NumberOfItems == 0)
			{
				job.CallBackWhenFinishedJob(job);
				return;
			}
			for (int i = 0; i < job.ObjectsToLoad.Length; i++)
			{
				Picture pictureEntry = job.ObjectsToLoad[i] as Picture;
				JobEntry jobEntry = new JobEntry();
				jobEntry.PictureEntry = pictureEntry;
				jobEntry.EntryJob = job;
				LoadImage(jobEntry);
			}
		}

		public void LoadImage(JobEntry entry)
		{
			string text = entry.PictureEntry.GetImageURL();
			if (entry.PictureEntry.GetPictureType() == PictureType.SPRITE)
			{
				if (text == string.Empty)
				{
					text = "friendzy_default";
				}
				mFriendzyGame.GameController.LoadAsset(this, text, entry);
			}
			else if (entry.PictureEntry.GetPictureType() == PictureType.TEXTURE)
			{
				if (text == string.Empty)
				{
				}
				mFriendzyGame.GameController.LoadAsset(this, text, entry);
			}
		}
	}
}
