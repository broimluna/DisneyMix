using Mix.Games.Chat;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Ui
{
	public class GameChatItem : MonoBehaviour
	{
		public GameObject ContextualLoader;

		public GameObject AvatarContainer;

		public Transform ItemTarget;

		public Image AvatarImage;

		public Image FooterImage;

		public Image TailImage;

		public Text AvatarName;

		public GameObject Footer
		{
			get
			{
				if (FooterImage != null)
				{
					return FooterImage.transform.parent.gameObject;
				}
				return null;
			}
		}

		public GameObject Tail
		{
			get
			{
				if (TailImage != null)
				{
					return TailImage.transform.parent.gameObject;
				}
				return null;
			}
		}

		public void OnPlayClicked()
		{
			BaseGameChatController componentInChildren = ItemTarget.GetComponentInChildren<BaseGameChatController>();
			if (!(componentInChildren == null))
			{
				componentInChildren.Play();
			}
		}
	}
}
