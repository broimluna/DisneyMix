using Disney.MobileNetwork;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class FullScreenPicturePanel : BasePanel
	{
		public Image MainImage;

		public GameObject NotificationBar;

		public SaveMediaController MediaController;

		public bool AllowShowingOfMenu { get; set; }

		public void Init(Sprite aSprite)
		{
			if (aSprite.texture.width > aSprite.texture.height)
			{
				float num = ((!((float)aSprite.texture.width > MixConstants.CANVAS_WIDTH)) ? 1f : (MixConstants.CANVAS_WIDTH / (float)aSprite.texture.width));
				float num2 = (float)aSprite.texture.height * num / 4f;
				MainImage.transform.Rotate(Vector3.forward * 90f);
				MainImage.rectTransform.offsetMax = new Vector2(num2, num2);
				MainImage.rectTransform.offsetMin = new Vector2(0f - num2, 0f - num2);
			}
			MainImage.sprite = aSprite;
			EnvironmentManager.ShowStatusBar(false);
		}

		public override bool OnAndroidBackButton()
		{
			EnvironmentManager.ShowStatusBar(true);
			isPanelCloseClicked = true;
			base.ClosePanel();
			return true;
		}

		public override void ClosePanel()
		{
			if (!MediaController.ContextMenu.activeSelf)
			{
				EnvironmentManager.ShowStatusBar(true);
				base.ClosePanel();
			}
		}

		public void ShowNotification(string aString)
		{
			NotificationBar.SetActive(true);
			NotificationBar.transform.Find("NotificationText").GetComponent<Text>().text = aString;
		}
	}
}
