using Mix.Games.Chat;
using UnityEngine;

namespace Mix.Ui.Chat
{
	public class GameResponseItemController : MonoBehaviour
	{
		public GameObject ContextualLoader;

		public Transform ItemTarget;

		public GameObject AvatarContainer;

		public void OnPlayClicked()
		{
			ItemTarget.GetComponentInChildren<BaseGameChatController>().Play();
		}
	}
}
