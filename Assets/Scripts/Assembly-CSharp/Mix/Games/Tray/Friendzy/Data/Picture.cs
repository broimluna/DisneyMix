using UnityEngine;

namespace Mix.Games.Tray.Friendzy.Data
{
	public class Picture
	{
		private const string IMAGE_RELATIVE_PATH = "gogs/friendzy/game_assets/en_US/";

		public string URLToImage;

		private Sprite PictureSprite;

		private Texture PictureTexture;

		private PictureType TypeOfPicture;

		public Sprite GetPicture()
		{
			return PictureSprite;
		}

		public Texture GetTexture()
		{
			return PictureTexture;
		}

		public PictureType GetPictureType()
		{
			return TypeOfPicture;
		}

		public void SetPicture(Sprite aSprite)
		{
			PictureSprite = aSprite;
		}

		public void SetPictureType(PictureType aType)
		{
			TypeOfPicture = aType;
		}

		public void SetTexture(Texture aTexture)
		{
			PictureTexture = aTexture;
		}

		public string GetImageURL()
		{
			string empty = string.Empty;
			empty = (URLToImage.EndsWith(".unity3d") ? URLToImage : ((!URLToImage.Contains("logo")) ? string.Format("{0}_hd.unity3d", URLToImage) : string.Format("{0}.unity3d", URLToImage)));
			return "gogs/friendzy/game_assets/en_US/" + empty;
		}
	}
}
