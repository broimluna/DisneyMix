using System.Collections.Generic;
using LitJson;
using Mix.Games.Chat;
using Mix.Games.Data;
using UnityEngine;

namespace Mix.Games.Tray.SecretWord
{
	public class SecretWordGamePostController : BaseGameChatController
	{
		private List<GameObject> mLetterHolders;

		protected SecretWordData mSecretWordData;

		public GameObject LetterHolder;

		public GameObject LetterHolderParent;

		protected List<GameObject> LetterHolders
		{
			get
			{
				if (mLetterHolders == null)
				{
					mLetterHolders = new List<GameObject>();
				}
				return mLetterHolders;
			}
		}

		public override void SetupGameData(string aGameDataJson)
		{
			mGameData = JsonMapper.ToObject<SecretWordData>(aGameDataJson);
			mResponses = (mGameData as IMixGameData).GetResponses();
		}

		protected override void SetupView()
		{
			base.SetupView();
			mSecretWordData = (SecretWordData)mGameData;
			if (LetterHolders.Count == 0)
			{
				LetterHolder.SetActive(false);
				string word = mSecretWordData.Word;
				for (int i = 0; i < word.Length; i++)
				{
					GameObject gameObject = Object.Instantiate(LetterHolder);
					gameObject.SetActive(true);
					gameObject.transform.SetParent(LetterHolderParent.transform, false);
					LetterHolders.Add(gameObject);
				}
				float num = 1f;
				for (int j = 0; j < LetterHolders.Count; j++)
				{
					GameObject gameObject2 = LetterHolders[j];
					gameObject2.transform.localRotation = Quaternion.Euler(0f, 0f, num * (float)Random.Range(1, 7));
					num *= -1f;
				}
			}
		}
	}
}
