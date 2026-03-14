using UnityEngine;
using UnityEngine.EventSystems;

public class ChatsBtn : MonoBehaviour, IEventSystemHandler, IDragHandler
{
	public Animator chatBtnAnimator;

	public GameObject secondary;

	public GameObject inviteChat;

	public GameObject conversations;

	private Vector3 secondaryStartPos;

	private bool shouldSnapBack = true;

	private bool goBackHome;

	private float amountMoved;

	private void Start()
	{
		secondaryStartPos = secondary.transform.localPosition;
		Debug.Log(inviteChat.transform.localPosition);
		Debug.Log(conversations.transform.localPosition);
	}

	private void Update()
	{
		if (!shouldSnapBack)
		{
			inviteChat.transform.localPosition = Vector3.Lerp(inviteChat.transform.localPosition, new Vector3(0f, 0f), 0.5f);
			conversations.transform.localPosition = Vector3.Lerp(conversations.transform.localPosition, new Vector3(0f, 1000f), 0.5f);
		}
		if (goBackHome)
		{
			inviteChat.transform.localPosition = Vector3.Lerp(inviteChat.transform.localPosition, new Vector3(0f, -1000f), 0.5f);
			conversations.transform.localPosition = Vector3.Lerp(conversations.transform.localPosition, new Vector3(0f, 0f), 0.5f);
		}
	}

	public void OnPointerDown()
	{
		chatBtnAnimator.SetBool("Pressed", true);
	}

	public void OnPointerUp()
	{
		chatBtnAnimator.SetBool("Pressed", false);
		secondary.transform.localPosition = secondaryStartPos;
	}

	public void OnDrag(PointerEventData eventData)
	{
		Vector3 localPosition = secondary.transform.localPosition;
		Vector3 localPosition2 = inviteChat.transform.localPosition;
		Vector3 localPosition3 = conversations.transform.localPosition;
		localPosition.y += eventData.delta.y;
		amountMoved += eventData.delta.y;
		if (shouldSnapBack)
		{
			localPosition2.y += eventData.delta.y;
			inviteChat.transform.localPosition = localPosition2;
			localPosition3.y += eventData.delta.y;
			conversations.transform.localPosition = localPosition3;
		}
		if (amountMoved >= 150f)
		{
			shouldSnapBack = false;
		}
	}

	public void onCancelBtnClicked()
	{
		goBackHome = true;
	}
}
