using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.Friendzy.Menu
{
	public class MenuAnimator : MonoBehaviour
	{
		private const float CASCADE_RATE = 0.05f;

		private const float ZOOM_DURATION = 2f;

		private const float INTRO_DURATION = 0.75f;

		private const float EXIT_DURATION = 1.25f;

		private const float COLOR_LERP_DURATION = 0.75f;

		[Header("References to Menu Components")]
		public FlipPanelHeader HeaderFlipBar;

		public Transform HeaderSnapPoint;

		public Transform MenuLeftSide;

		public Transform MenuRightSide;

		public Transform FlipBarParent;

		public Image Fader;

		public Image Logo;

		public ScrollRect SnappingPointScroll;

		private Vector3 LocalHeaderPosOut = new Vector3(0f, 0.4f, -0.12f);

		private Vector3 LocalHeaderPosIn = new Vector3(0f, 0.2f, -0.12f);

		private Vector3 LocalLeftSidePosOut = new Vector3(-0.3f, -0.0125f, 0f);

		private Vector3 LocalLeftSidePosIn = new Vector3(0f, -0.0125f, 0f);

		private Vector3 LocalRightSidePosOut = new Vector3(0.3f, -0.0125f, 0f);

		private Vector3 LocalRightSidePosIn = new Vector3(0f, -0.0125f, 0f);

		private Vector3 LocalFlipBarsPosOut = new Vector3(0f, -0.7f, 0f);

		private Vector3 LocalFlipBarsPosIn = Vector3.zero;

		private FlipBarManager mFlipBarManager;

		private LerpColorSwitch mColorSwitcher;

		public static bool InputEnabled;

		private void Awake()
		{
			mFlipBarManager = GetComponent<FlipBarManager>();
			mColorSwitcher = GetComponent<LerpColorSwitch>();
			Initialize();
		}

		private void Initialize()
		{
			InputEnabled = false;
			HeaderFlipBar.transform.localPosition = LocalHeaderPosOut;
			MenuLeftSide.localPosition = LocalLeftSidePosOut;
			MenuRightSide.localPosition = LocalRightSidePosOut;
			FlipBarParent.localPosition = LocalFlipBarsPosOut;
			SetElementsActive(false);
		}

		public void SetElementsActive(bool active)
		{
			HeaderSnapPoint.gameObject.SetActive(active);
			MenuLeftSide.gameObject.SetActive(active);
			MenuRightSide.gameObject.SetActive(active);
			FlipBarParent.gameObject.SetActive(active);
		}

		public Sequence FlipAllBars(List<FlipPanel> flipBars)
		{
			Sequence sequence = DOTween.Sequence();
			InputEnabled = false;
			FriendzyGame.PlaySound("MenuFlipBars", FriendzyGame.SOUND_PREFIX);
			sequence.Insert(0f, HeaderFlipBar.Flip180());
			for (int i = 0; i < flipBars.Count; i++)
			{
				Sequence t = flipBars[i].Flip180();
				sequence.Insert((float)(i + 1) * 0.05f, t);
			}
			sequence.AppendCallback(delegate
			{
				InputEnabled = true;
			});
			return sequence;
		}

		public Sequence FlipAllBarsEnd(List<FlipPanel> flipBars)
		{
			Sequence sequence = DOTween.Sequence();
			InputEnabled = false;
			FriendzyGame.PlaySound("MenuFlipBars", FriendzyGame.SOUND_PREFIX);
			HeaderFlipBar.Flip180End();
			for (int i = 0; i < flipBars.Count; i++)
			{
				Sequence t = flipBars[i].Flip180End();
				sequence.Insert(0f, t);
			}
			return sequence;
		}

		public void SetIPTexture(Texture aIPTexture)
		{
			mColorSwitcher.SetIPTexture(aIPTexture);
			Texture2D texture2D = aIPTexture as Texture2D;
			if (texture2D != null)
			{
				mColorSwitcher.SetIPColor(texture2D.GetPixel(0, 0));
			}
		}

		public Tween IPColorSwitch()
		{
			return mColorSwitcher.BlendColors(0.75f);
		}

		public void TurnOn()
		{
			Transform HeaderFlipBarParent = HeaderFlipBar.transform.parent;
			HeaderFlipBar.transform.SetParent(HeaderSnapPoint);
			Sequence s = DOTween.Sequence();
			s.Append(MenuEnter(0.75f)).OnComplete(delegate
			{
				InputEnabled = true;
				Logo.gameObject.SetActive(false);
				HeaderFlipBar.transform.SetParent(HeaderFlipBarParent);
			});
		}

		private Sequence MenuEnter(float aDuration)
		{
			HeaderFlipBar.transform.SetParent(HeaderSnapPoint);
			SetElementsActive(true);
			Sequence sequence = DOTween.Sequence();
			sequence.Insert(0.1f, TweenScrollRectPosition(0f, 0f));
			sequence.InsertCallback(0.1f, delegate
			{
				FriendzyGame.PlaySound("MenuSlideIn", FriendzyGame.SOUND_PREFIX);
			});
			sequence.Append(HeaderSnapPoint.DOLocalMove(LocalHeaderPosIn, aDuration));
			sequence.Join(MenuLeftSide.DOLocalMove(LocalLeftSidePosIn, aDuration));
			sequence.Join(MenuRightSide.DOLocalMove(LocalRightSidePosIn, aDuration));
			sequence.Join(FlipBarParent.DOLocalMove(LocalFlipBarsPosIn, aDuration));
			sequence.Insert(0.6f, TweenScrollRectPosition(1f, aDuration));
			sequence.InsertCallback(0.6f, delegate
			{
				FriendzyGame.PlaySound("MenuSlideIn", FriendzyGame.SOUND_PREFIX);
			});
			return sequence;
		}

		private Tween TweenScrollRectPosition(float aPosition, float aDuration)
		{
			return DOTween.To(() => SnappingPointScroll.verticalNormalizedPosition, delegate(float x)
			{
				SnappingPointScroll.verticalNormalizedPosition = x;
			}, aPosition, aDuration);
		}

		private Sequence MenuExplode(float aDuration)
		{
			FriendzyGame.PlaySound("MenuSlideIn", FriendzyGame.SOUND_PREFIX);
			Fader.gameObject.SetActive(true);
			HeaderFlipBar.transform.SetParent(HeaderSnapPoint);
			Sequence sequence = DOTween.Sequence();
			sequence.Append(HeaderSnapPoint.DOLocalMove(LocalHeaderPosOut, aDuration));
			sequence.Join(MenuLeftSide.DOLocalMove(LocalLeftSidePosOut, aDuration));
			sequence.Join(MenuRightSide.DOLocalMove(LocalRightSidePosOut, aDuration));
			sequence.Join(FlipBarParent.DOLocalMove(LocalFlipBarsPosOut, aDuration));
			sequence.Join(Fader.DOFade(0f, aDuration).OnComplete(Inactivate));
			return sequence;
		}

		private void Inactivate()
		{
			base.gameObject.SetActive(false);
		}

		public void TurnOff()
		{
			Sequence s = DOTween.Sequence();
			s.Append(FlipAllBarsEnd(mFlipBarManager.FlipBars));
			s.Append(MenuExplode(1.25f));
			s.Join(TweenScrollRectPosition(1f, 1.25f));
		}
	}
}
