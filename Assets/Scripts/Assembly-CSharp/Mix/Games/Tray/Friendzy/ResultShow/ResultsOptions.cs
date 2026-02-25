using System.Collections.Generic;
using Mix.Games.Tray.Friendzy.Data;
using UnityEngine;

namespace Mix.Games.Tray.Friendzy.ResultShow
{
	public class ResultsOptions : MonoBehaviour
	{
		public SpriteRenderer ChosenImage;

		public SpriteRenderer[] ResultOptionImages;

		private float mTargetWidth = 0.1024f;

		private float mTargetHeight = 0.128f;

		public void SetResultSprites(Picture[] aPics)
		{
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			for (int i = 0; i < aPics.Length; i++)
			{
				list.Add(i);
			}
			for (int j = 0; j < ResultOptionImages.Length; j++)
			{
				int index = Random.Range(0, list.Count);
				Picture picture = aPics[list[index]];
				ResultOptionImages[j].sprite = picture.GetPicture();
				ScaleSprite(ResultOptionImages[j]);
				list2.Add(list[index]);
				list.Remove(list[index]);
				if (list.Count == 0)
				{
					List<int> list3 = list;
					list = list2;
					list2 = list3;
				}
			}
		}

		public void SetChosenSprite(Picture aChosenPic)
		{
			ChosenImage.sprite = aChosenPic.GetPicture();
			ScaleSprite(ChosenImage);
		}

		private void ScaleSprite(SpriteRenderer aSpriteRenderer)
		{
			Bounds bounds = aSpriteRenderer.sprite.bounds;
			float num = mTargetWidth / bounds.size.x;
			float y = mTargetHeight / bounds.size.y;
			aSpriteRenderer.transform.localScale = new Vector3(num, y, num);
		}
	}
}
