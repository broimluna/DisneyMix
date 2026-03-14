using Mix.Games.Chat;
using UnityEngine;

namespace Mix.Ui.Chat
{
	public class GamePostItemController : MonoBehaviour
	{
		public GameObject ContextualLoader;

		public Transform ItemTarget;

		public GameObject AvatarContainer;

		public GameObject TapToPlay;

		public GameObject AlreadyPlayed;

		public void OnPlayClicked()
		{
			ItemTarget.GetComponentInChildren<BaseGameChatController>().Play();
		}
	}
}
