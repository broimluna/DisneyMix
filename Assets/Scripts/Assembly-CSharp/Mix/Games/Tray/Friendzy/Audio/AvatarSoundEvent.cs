using UnityEngine;

namespace Mix.Games.Tray.Friendzy.Audio
{
	public class AvatarSoundEvent : MonoBehaviour
	{
		public void PlaySound(string aSoundWithPrefix)
		{
			FriendzyGame.PlaySound(aSoundWithPrefix);
		}
	}
}
