using Mix.Games.Tray.Friendzy.Data;
using UnityEngine.UI;

namespace Mix.Games.Tray.Friendzy.Menu
{
	public class FlipPanelHeader : FlipPanel
	{
		public Image QuizImage;

		public override void ToggleOppositeSide(bool aToggle)
		{
			if (mIsOnCategorySide)
			{
				QuizImage.enabled = aToggle;
			}
			else
			{
				CategoryImage.enabled = aToggle;
			}
		}

		public override string GetFrontPanelText()
		{
			return string.Empty;
		}

		public override void SetCategory(Category aCategory)
		{
			QuizImage.sprite = aCategory.GetLogoPicture().GetPicture();
			FlipBarMaterialTarget.material.mainTexture = aCategory.GetLogoBackgroundPicture().GetTexture();
		}
	}
}
