using System.Collections.Generic;
using Mix.Games.Session;
using UnityEngine;

namespace Mix.Games.Tray.MemeGenerator
{
	public class MemeImageSelectionController : MonoBehaviour, IMemeImageOptionSelected
	{
		public RectTransform MemeHolder;

		public GameObject MemeImageContainer;

		protected List<MemeImageOption> mImageOptions;

		protected IMemeGeneration mMemeGeneration;

		Object IMemeImageOptionSelected.GetImage(string aPath)
		{
			return mMemeGeneration.GetMemeGeneratorController().GetBundleInstance(aPath);
		}

		void IMemeImageOptionSelected.LoadImage(IGameAsset aSessionAsset, string aPath)
		{
			mMemeGeneration.GetMemeGeneratorController().LoadAsset(aSessionAsset, aPath);
		}

		void IMemeImageOptionSelected.DestroyImageOption(string aPath, Object aObject)
		{
			mMemeGeneration.GetMemeGeneratorController().CancelBundles(aPath);
			mMemeGeneration.GetMemeGeneratorController().DestroyBundleInstance(aPath, aObject);
		}

		void IMemeImageOptionSelected.OnImageSelected(MemeImageOption aOption)
		{
			BaseGameController.Instance.Session.SessionSounds.PlaySoundEvent("MemeGenerator/SFX/SelectMemeImage");
			mMemeGeneration.SetMemeImage(aOption.ImageUrl);
		}

		public void Initialize(IMemeGeneration aMemeGeneration, List<Meme_Generator_Data> aData, string aBasePath)
		{
			mImageOptions = new List<MemeImageOption>();
			mMemeGeneration = aMemeGeneration;
			MemeImageContainer = base.transform.Find("MemeScroller/MemeHolder/meme_item").gameObject;
			aData.Sort(new MemeGeneratorDataSorter());
			for (int i = 0; i < aData.Count; i++)
			{
				GameObject gameObject = Object.Instantiate(MemeImageContainer);
				gameObject.transform.SetParent(MemeHolder, false);
				gameObject.SetActive(true);
				string aImageUrl = aBasePath + aData[i].url;
				string aThumbUrl = aBasePath + aData[i].thumb;
				MemeImageOption component = gameObject.GetComponent<MemeImageOption>();
				component.Initialize(this, aImageUrl, aThumbUrl, aData[i]);
				mImageOptions.Add(component);
			}
		}
	}
}
