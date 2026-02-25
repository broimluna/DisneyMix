namespace Mix.Games.Tray.Friendzy.Data
{
	public class Category
	{
		public string Name;

		private Picture logoPicture;

		public readonly string LogoURL;

		private Picture logoBackgroundPicture;

		public readonly string LogoBackground;

		private Picture quizBackgroundPicture;

		public readonly string QuizBackground;

		private Picture categoryPalettePicture;

		public readonly string CategoryPalette;

		public readonly string MainColorHex;

		public readonly string AccentColorHex;

		public readonly string QuestionFontColorHex;

		public readonly string AnswerFontColorHex;

		public readonly string QuestionBgColorHex;

		public readonly string AnswerBgColorHex;

		public readonly Quiz[] Quizzes;

		public readonly string uid;

		public Picture GetLogoPicture()
		{
			if (logoPicture == null)
			{
				logoPicture = new Picture();
			}
			return logoPicture;
		}

		public Picture LoadLogoPicture()
		{
			Picture picture = GetLogoPicture();
			picture.URLToImage = LogoURL;
			picture.SetPictureType(PictureType.SPRITE);
			return picture;
		}

		public Picture GetLogoBackgroundPicture()
		{
			if (logoBackgroundPicture == null)
			{
				logoBackgroundPicture = new Picture();
			}
			return logoBackgroundPicture;
		}

		public Picture LoadLogoBackgroundPicture()
		{
			Picture picture = GetLogoBackgroundPicture();
			picture.URLToImage = LogoBackground;
			picture.SetPictureType(PictureType.TEXTURE);
			return picture;
		}

		public Picture GetQuizBackgroundPicture()
		{
			if (quizBackgroundPicture == null)
			{
				quizBackgroundPicture = new Picture();
			}
			return quizBackgroundPicture;
		}

		public Picture LoadQuizBackgroundPicture()
		{
			Picture picture = GetQuizBackgroundPicture();
			picture.URLToImage = QuizBackground;
			picture.SetPictureType(PictureType.SPRITE);
			return picture;
		}

		public Picture GetCategoryPalettePicture()
		{
			if (categoryPalettePicture == null)
			{
				categoryPalettePicture = new Picture();
			}
			return categoryPalettePicture;
		}

		public Picture LoadCategoryPalettePicture()
		{
			Picture picture = GetCategoryPalettePicture();
			picture.URLToImage = CategoryPalette;
			picture.SetPictureType(PictureType.TEXTURE);
			return picture;
		}
	}
}
