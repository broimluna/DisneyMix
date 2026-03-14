using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	public class DropPlayerOnFloorBehaviour : DropPlayerBehaviour
	{
		public override void OnStateEnterOnce(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (base.Player.OnDie != null)
			{
				base.Player.HitFloorParticles.Play(true);
				base.Player.OnDie();
				base.Player.Active = false;
			}
			DropAudio.PlaySound("SFX/Gameplay/Player/LandOnGround");
		}
	}
}
