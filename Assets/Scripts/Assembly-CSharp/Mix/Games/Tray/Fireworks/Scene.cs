using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.Fireworks
{
	public class Scene : MonoBehaviour
	{
		public int SceneID;

		public Button SceneButton;

		public Sprite Background;

		public GameObject[] ButtonFireworks = new GameObject[5];

		public GameObject TapFirework;

		public GameObject GestureFirework;

		public float FinaleTime = 5f;

		public float SongTime;

		public GameObject Foreground3D;

		public List<GameObject> Alt5thFireworks = new List<GameObject>();

		public int UIindex;

		public Color[] FireworkColors;

		public Color[] ForegroundTints;

		public Animator SceneBackgroundAnim;

		public FireworksGame Game;

		public ScrollRectHorizontalSnap SceneScrollRect;

		public ManageSwipeArrows SwipeArrows;

		public FireworkTapPlane TapPlane;

		public FireworkGenerationArea GenerationArea;

		[Header("Bundle Info")]
		public string SceneBundle;

		[Header("Music")]
		public string PreviewSong;

		public string MainSong;

		public void SelectScene()
		{
			if (Game != null && SceneScrollRect != null && SwipeArrows != null)
			{
				SceneScrollRect.JumpToScene(this);
				SwipeArrows.ToggleLeftArrow(SceneScrollRect.HasLeftSelection(this));
				SwipeArrows.ToggleRightArrow(SceneScrollRect.HasRightSelection(this));
				Game.ChangeScene(this);
			}
		}
	}
}
