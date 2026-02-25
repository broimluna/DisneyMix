using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	public class DropPlayerStuckInSnowBehaviour : DropPlayerBehaviour
	{
		public override void OnStateEnterOnce(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (base.Player.OnDie != null)
			{
				base.Player.HitSnowParticles.Play(true);
				base.Player.OnDie();
				base.Player.Active = false;
			}
			DropAudio.PlaySound("SFX/Gameplay/Player/LandOnObstacle");
		}
	}
}
