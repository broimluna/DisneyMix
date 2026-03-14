using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Mix.Games.Tray.Friendzy.Menu
{
	[RequireComponent(typeof(Collider2D))]
	[RequireComponent(typeof(FlipPanel))]
	public class MenuButton : MonoBehaviour
	{
		private const float DRAG_TOLERANCE = 15f;

		private const float DEPRESSED_AMOUNT = 60f;

		private bool ClickedDown;

		private Vector2 currentPos;

		private Vector2 prevPos;

		public UnityEvent onClick;

		private FlipPanel mFlipPanel;

		public Color DEPRESSED_COLOR;

		public Color NORMAL_COLOR;

		private void Awake()
		{
			mFlipPanel = GetComponent<FlipPanel>();
			InitButtonBehavior();
		}

		private void InitButtonBehavior()
		{
			if (onClick == null)
			{
				onClick = new UnityEvent();
			}
			ClickedDown = false;
		}

		private void OnMouseDown()
		{
			if (MenuAnimator.InputEnabled)
			{
				ClickedDown = true;
				currentPos = (prevPos = Input.mousePosition);
				DepressFlipBar(60f);
			}
		}

		private void DepressFlipBar(float aAmount)
		{
			Color aColor;
			if (aAmount > 1f)
			{
				aColor = DEPRESSED_COLOR;
				FriendzyGame.PlaySound("ClickIn", FriendzyGame.SOUND_PREFIX);
			}
			else
			{
				aColor = NORMAL_COLOR;
				FriendzyGame.PlaySound("ClickOut", FriendzyGame.SOUND_PREFIX);
			}
			AlterFrontPanelColor(aColor);
			mFlipPanel.transformToFollow.DOLocalMoveZ(aAmount, 0.15f);
		}

		private void OnMouseDrag()
		{
			if (ClickedDown)
			{
				currentPos = Input.mousePosition;
				if (!((currentPos - prevPos).sqrMagnitude > 15f))
				{
				}
			}
		}

		private void OnMouseUpAsButton()
		{
			if (ClickedDown)
			{
				ClickedDown = false;
				DepressFlipBar(0f);
				onClick.Invoke();
			}
		}

		private void OnMouseExit()
		{
			ClickedDown = false;
			DepressFlipBar(0f);
		}

		public void AlterFrontPanelColor(Color aColor)
		{
			mFlipPanel.AlterFrontPanelColor(aColor);
		}
	}
}
