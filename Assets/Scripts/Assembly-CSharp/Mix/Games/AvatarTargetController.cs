using System;
using Avatar;
using Disney.Mix.SDK;
using Mix.Avatar;
using Mix.Session;
using Mix.Session.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games
{
	public class AvatarTargetController : MonoBehaviour
	{
		public GameObject AvatarOutline;

		public GameObject AvatarShadow;

		public Image AvatarIcon;

		public GameObject[] ResultState;

		protected string mPlayerId;

		private Action mAvatarCleanup;

		public string PlayerId
		{
			get
			{
				return mPlayerId;
			}
		}

		private void OnDestroy()
		{
			if (mAvatarCleanup != null && MonoSingleton<AvatarManager>.Instance != null)
			{
				mAvatarCleanup();
			}
		}

		protected void CreateAvatarImage(string aSwid, Transform aParent)
		{
			SnapshotCallback snapshotCallback = delegate(bool success, Sprite sprite)
			{
				mAvatarCleanup = null;
				if (success)
				{
					ParentSnapshotAndApplySprite(aSwid, AvatarIcon, aParent, sprite);
				}
			};
			IAvatarHolder avatarHolder = null;
			if (MixSession.User.Id.Equals(aSwid))
			{
				avatarHolder = new AvatarHolder(MixSession.User);
			}
			else if (MixChat.FindFriend(aSwid) != null)
			{
				avatarHolder = new AvatarHolder(MixChat.FindFriend(aSwid));
			}
			IAvatar avatar2;
			if (avatarHolder == null)
			{
				IAvatar avatar = null;
				avatar2 = avatar;
			}
			else
			{
				avatar2 = avatarHolder.Avatar;
			}
			IAvatar dna = avatar2;
			MonoSingleton<AvatarManager>.Instance.GetSnapshotFromDna(dna, (AvatarFlags)0, (int)AvatarIcon.GetComponent<RectTransform>().rect.width, snapshotCallback);
			if (AvatarOutline != null)
			{
				AvatarOutline.SetActive(false);
			}
			if (AvatarShadow != null)
			{
				AvatarShadow.SetActive(true);
			}
			AvatarIcon.gameObject.SetActive(true);
		}

		public void LoadPlayer(string aSwid, Transform aParent)
		{
			mPlayerId = aSwid;
			CreateAvatarImage(aSwid, aParent);
		}

		public void SetResultState(int aIndex)
		{
			GameObject[] resultState = ResultState;
			foreach (GameObject gameObject in resultState)
			{
				gameObject.SetActive(false);
			}
			ResultState[aIndex].SetActive(true);
		}

		protected void ParentSnapshotAndApplySprite(string aSwid, Image aImage, Transform aParent, Sprite aSprite)
		{
			aImage.transform.SetParent(aParent, false);
			aImage.sprite = aSprite;
		}
	}
}
