using System.Collections.Generic;
using Mix;
using UnityEngine;
using UnityEngine.UI;

public class PrototypeController : MonoBehaviour, IFlickCallback
{
	public static float DRAG_MAX = 0.2f;

	public Canvas ParentCanvas;

	public Animator ChatButtonAnimator;

	public Animator FriendsButtonAnimator;

	public Animator ProfileButtonAnimator;

	public FlickController ChatFlickController;

	public FlickController FriendsFlickController;

	public FlickController ProfileFlickController;

	public SimpleAnimator SimpleAnimator;

	public RectTransform BottomNav;

	public RectTransform FriendsScreen;

	public RectTransform ProfileScreen;

	public RectTransform ConvoScreen;

	public RectTransform StartChatScreen;

	public RectTransform AddFriendScreen;

	public RectTransform EditAvatarScreen;

	private FlickController currentFlicking;

	private RectTransform currentScreen;

	private ColorBlock offBlock;

	private ColorBlock onBlock;

	void IFlickCallback.OnDragging(float aDelta)
	{
		Debug.Log("Dragging");
		List<RectTransform> list = new List<RectTransform>();
		if (currentFlicking.Equals(FriendsFlickController))
		{
			list.Add(currentScreen);
			list.Add(BottomNav);
			list.Add(AddFriendScreen);
		}
		else if (currentFlicking.Equals(ChatFlickController))
		{
			list.Add(currentScreen);
			list.Add(BottomNav);
			list.Add(StartChatScreen);
		}
		else if (currentFlicking.Equals(ProfileFlickController))
		{
			list.Add(currentScreen);
			list.Add(BottomNav);
			list.Add(EditAvatarScreen);
		}
		for (int i = 0; i < list.Count; i++)
		{
			Vector2 anchoredPosition = list[i].anchoredPosition;
			Vector2 anchoredPosition2 = new Vector2(anchoredPosition.x, anchoredPosition.y += aDelta);
			list[i].anchoredPosition = anchoredPosition2;
		}
	}

	void IFlickCallback.OnFlicked(bool aFlicked)
	{
		Debug.Log("Flicked: " + aFlicked);
		RectTransform rectTransform = null;
		if (currentFlicking.Equals(FriendsFlickController))
		{
			rectTransform = AddFriendScreen;
		}
		else if (currentFlicking.Equals(ChatFlickController))
		{
			rectTransform = StartChatScreen;
		}
		else if (currentFlicking.Equals(ProfileFlickController))
		{
			rectTransform = EditAvatarScreen;
		}
		if (aFlicked)
		{
			SimpleAnimator.StartAnimation(new RectTransform[2] { currentScreen, BottomNav }, new RectTransform[1] { rectTransform }, SimpleAnimator.AnimationDirection.Up, 0.07f);
		}
		else
		{
			SimpleAnimator.StartAnimation(new RectTransform[2] { currentScreen, BottomNav }, new RectTransform[1] { rectTransform }, SimpleAnimator.AnimationDirection.Down, 0.15f);
		}
	}

	public void Start()
	{
		MixConstants.CANVAS_HEIGHT = ParentCanvas.GetComponent<RectTransform>().rect.height;
		MixConstants.CANVAS_WIDTH = ParentCanvas.GetComponent<RectTransform>().rect.width;
		StartChatScreen.anchoredPosition = new Vector2(0f, 0f - MixConstants.CANVAS_HEIGHT);
		AddFriendScreen.anchoredPosition = new Vector2(0f, 0f - MixConstants.CANVAS_HEIGHT);
		EditAvatarScreen.anchoredPosition = new Vector2(0f, 0f - MixConstants.CANVAS_HEIGHT);
		FriendsScreen.anchoredPosition = new Vector2(MixConstants.CANVAS_WIDTH, 0f);
		ProfileScreen.anchoredPosition = new Vector2(0f - MixConstants.CANVAS_WIDTH, 0f);
		offBlock = ProfileButtonAnimator.GetComponent<Button>().colors;
		onBlock = ChatButtonAnimator.GetComponent<Button>().colors;
		currentScreen = ConvoScreen;
	}

	public void OnCloseStartChat()
	{
		SimpleAnimator.StartAnimation(new RectTransform[2] { currentScreen, BottomNav }, new RectTransform[1] { StartChatScreen }, SimpleAnimator.AnimationDirection.Down, 0.15f);
	}

	public void OnCloseAddFriend()
	{
		SimpleAnimator.StartAnimation(new RectTransform[2] { currentScreen, BottomNav }, new RectTransform[1] { AddFriendScreen }, SimpleAnimator.AnimationDirection.Down, 0.15f);
	}

	public void OnCloseEditAvatar()
	{
		SimpleAnimator.StartAnimation(new RectTransform[2] { currentScreen, BottomNav }, new RectTransform[1] { EditAvatarScreen }, SimpleAnimator.AnimationDirection.Down, 0.15f);
	}

	public void OnFriendsButtonDown()
	{
		FriendsButtonAnimator.SetBool("Pressed", true);
		Invoke("OnFriendsButtonDownDelay", 0.15f);
	}

	public void OnFriendsButtonUp()
	{
		if (FriendsFlickController.Detecting)
		{
			FriendsFlickController.Finish(Mathf.Abs(ConvoScreen.anchoredPosition.y) > DRAG_MAX * MixConstants.CANVAS_HEIGHT);
		}
		else
		{
			TintButton(FriendsButtonAnimator);
			FriendsScreen.anchoredPosition = new Vector2(MixConstants.CANVAS_WIDTH, 0f);
			SimpleAnimator.StartAnimation(new RectTransform[1] { currentScreen }, new RectTransform[1] { FriendsScreen }, SimpleAnimator.AnimationDirection.Left, 0.15f);
			currentScreen = FriendsScreen;
		}
		FriendsButtonAnimator.SetBool("Pressed", false);
	}

	private void OnFriendsButtonDownDelay()
	{
		if (FriendsButtonAnimator.GetBool("Pressed"))
		{
			FriendsButtonAnimator.Play("Reveal");
			FriendsFlickController.Detect(this);
			currentFlicking = FriendsFlickController;
		}
	}

	public void OnProfileButtonDown()
	{
		ProfileButtonAnimator.SetBool("Pressed", true);
		Invoke("OnProfileButtonDownDelay", 0.15f);
	}

	public void OnProfileButtonUp()
	{
		if (ProfileFlickController.Detecting)
		{
			ProfileFlickController.Finish(Mathf.Abs(ConvoScreen.anchoredPosition.y) > DRAG_MAX * MixConstants.CANVAS_HEIGHT);
		}
		else
		{
			TintButton(ProfileButtonAnimator);
			ProfileScreen.anchoredPosition = new Vector2(0f - MixConstants.CANVAS_WIDTH, 0f);
			SimpleAnimator.StartAnimation(new RectTransform[1] { currentScreen }, new RectTransform[1] { ProfileScreen }, SimpleAnimator.AnimationDirection.Right, 0.15f);
			currentScreen = ProfileScreen;
		}
		ProfileButtonAnimator.SetBool("Pressed", false);
	}

	private void OnProfileButtonDownDelay()
	{
		if (ProfileButtonAnimator.GetBool("Pressed"))
		{
			ProfileButtonAnimator.Play("Reveal");
			ProfileFlickController.Detect(this);
			currentFlicking = ProfileFlickController;
		}
	}

	public void OnChatButtonDown()
	{
		ChatButtonAnimator.SetBool("Pressed", true);
		Invoke("OnChatButtonDownDelay", 0.15f);
	}

	public void OnChatButtonUp()
	{
		if (ChatFlickController.Detecting)
		{
			ChatFlickController.Finish(Mathf.Abs(ConvoScreen.anchoredPosition.y) > DRAG_MAX * MixConstants.CANVAS_HEIGHT);
		}
		else
		{
			TintButton(ChatButtonAnimator);
			SimpleAnimator.AnimationDirection aDirection = SimpleAnimator.AnimationDirection.Right;
			if (currentScreen.Equals(FriendsScreen))
			{
				ConvoScreen.anchoredPosition = new Vector2(0f - MixConstants.CANVAS_WIDTH, 0f);
				aDirection = SimpleAnimator.AnimationDirection.Right;
			}
			else if (currentScreen.Equals(ProfileScreen))
			{
				ConvoScreen.anchoredPosition = new Vector2(MixConstants.CANVAS_WIDTH, 0f);
				aDirection = SimpleAnimator.AnimationDirection.Left;
			}
			SimpleAnimator.StartAnimation(new RectTransform[1] { currentScreen }, new RectTransform[1] { ConvoScreen }, aDirection, 0.15f);
			currentScreen = ConvoScreen;
		}
		ChatButtonAnimator.SetBool("Pressed", false);
	}

	private void OnChatButtonDownDelay()
	{
		if (ChatButtonAnimator.GetBool("Pressed"))
		{
			ChatButtonAnimator.Play("Reveal");
			ChatFlickController.Detect(this);
			currentFlicking = ChatFlickController;
		}
	}

	private void TintButton(Animator aButtonAnimator)
	{
		FriendsButtonAnimator.GetComponent<Button>().colors = offBlock;
		ChatButtonAnimator.GetComponent<Button>().colors = offBlock;
		ProfileButtonAnimator.GetComponent<Button>().colors = offBlock;
		aButtonAnimator.GetComponent<Button>().colors = onBlock;
	}
}
