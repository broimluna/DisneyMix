using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	public class ScoreboardScene : MonoBehaviour
	{
		public GameObject AvatarMesh;

		public Animator[] AllAnimators;

		public ParticleSystem SnowShakeParticles;

		protected DropGame mGame;

		public void Init(DropGame aGame)
		{
			mGame = aGame;
			DropGameController gameController = mGame.GameController;
			gameController.LoadFriend(AvatarMesh.gameObject, gameController.PlayerId, true);
		}

		public void Show(bool aIsHighScore)
		{
			base.gameObject.SetActive(true);
			mGame.FollowCamera.gameObject.SetActive(false);
			SnowShakeParticles.Play(true);
		}

		public void Hide()
		{
			int trigger = Animator.StringToHash("Reset");
			for (int i = 0; i < AllAnimators.Length; i++)
			{
				AllAnimators[i].SetTrigger(trigger);
			}
			base.gameObject.SetActive(false);
			mGame.FollowCamera.gameObject.SetActive(true);
		}
	}
}
