using System;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.Drop
{
	public class CommonUI : MonoBehaviour
	{
		public static Action OnMaskOpened;

		public static Action OnMaskClosed;

		public Text TimeText;

		public Animator FullscreenMask;

		public NameMarker NameMarkerPrefab;

		private DropGame dropGame;

		private void Start()
		{
			dropGame = DropGame.Instance;
			DropGame obj = dropGame;
			obj.OnGameTimeUpdated = (Action<float>)Delegate.Combine(obj.OnGameTimeUpdated, new Action<float>(OnGameTimeUpdate));
		}

		private void OnGameTimeUpdate(float time)
		{
			TimeText.text = time.ToString("0.00");
		}

		public void OpenMask()
		{
			FullscreenMask.SetBool("IsOpen", true);
			DropAudio.PlaySound("SFX/UI/MaskOpen");
		}

		public void CloseMask()
		{
			FullscreenMask.SetBool("IsOpen", false);
			DropAudio.PlaySound("SFX/UI/MaskClose");
		}

		public NameMarker CreateNameMarkerForGhost(GhostDropPlayer ghost)
		{
			NameMarker nameMarker = UnityEngine.Object.Instantiate(NameMarkerPrefab);
			nameMarker.transform.SetParent(base.transform, false);
			nameMarker.NameText.text = ghost.Game.GameController.GetGhostName(ghost.GhostResponse.PlayerSwid);
			nameMarker.TargetPlayer = ghost;
			nameMarker.MainPlayer = ghost.Game.Player;
			return nameMarker;
		}
	}
}
