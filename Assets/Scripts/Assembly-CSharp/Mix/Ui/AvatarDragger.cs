using System.Collections.Generic;
using Disney.Mix.SDK;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class AvatarDragger : MonoBehaviour
	{
		private const int DraggerOpen = -2;

		public List<AvatarClosetItem> slotList;

		public GameObject editButton;

		public GameObject deleteButton;

		public GameObject mainAvatar;

		public float avatarYOffset;

		public AvatarClosetPanel costumePanel;

		public Animator AvatarAnimationController;

		private int slot = -1;

		private IAvatar dna;

		private GameObject m_DraggingIcon;

		private RectTransform m_DraggingPlane;

		private float yOffset;

		private int currentDraggerSlot = -2;

		private void Start()
		{
		}

		private void Update()
		{
		}

		public bool OnBeginDrag(int aSlot, IAvatar aDna, Sprite sprite, PointerEventData eventData)
		{
			if (currentDraggerSlot != -2)
			{
				return false;
			}
			float num = 640f / (float)Screen.height;
			yOffset = avatarYOffset / num;
			slot = aSlot;
			dna = aDna;
			if (slot >= 0)
			{
				AvatarAnimationController.Play("ShowDelete");
			}
			Canvas canvas = FindCanvasInParents(base.gameObject);
			if (canvas == null)
			{
				return false;
			}
			m_DraggingIcon = new GameObject("AvatarIcon");
			m_DraggingIcon.transform.SetParent(canvas.transform, false);
			m_DraggingIcon.transform.SetAsLastSibling();
			Image image = m_DraggingIcon.AddComponent<Image>();
			image.sprite = sprite;
			image.SetNativeSize();
			currentDraggerSlot = aSlot;
			m_DraggingPlane = canvas.transform as RectTransform;
			SetDraggedPosition(eventData);
			return true;
		}

		public void OnDrag(int aSlot, PointerEventData data)
		{
			if (currentDraggerSlot == aSlot)
			{
				SetDraggedPosition(data);
			}
		}

		public void OnEndDrag(int aSlot, PointerEventData data)
		{
			if (currentDraggerSlot != aSlot)
			{
				return;
			}
			currentDraggerSlot = -2;
			bool flag = false;
			if (m_DraggingIcon != null)
			{
				Object.Destroy(m_DraggingIcon);
			}
			if (data == null)
			{
				return;
			}
			if (costumePanel.showTooltip)
			{
				if (Util.GetRectInScreenSpace(slotList[0].GetBoundingBox()).Contains(data.position))
				{
					slotList[0].OnAvatarDragged(0, dna);
					costumePanel.SetTooltip(false);
				}
				return;
			}
			for (int i = 0; i < slotList.Count; i++)
			{
				Rect rectInScreenSpace = Util.GetRectInScreenSpace(slotList[i].GetBoundingBox());
				if (!Util.ExpandRect(rectInScreenSpace, rectInScreenSpace.width / 3f).Contains(data.position))
				{
					continue;
				}
				if (slotList[i].IsOpenSlot)
				{
					slotList[i].OnAvatarDragged(i, dna);
					flag = true;
					if (slot >= 0)
					{
						slotList[slot].DeleteSlot(slot);
					}
				}
				else
				{
					if (slot >= 0)
					{
						slotList[slot].OnAvatarDragged(slot, slotList[i].slotDna);
					}
					slotList[i].OnAvatarDragged(i, dna);
				}
			}
			if (slot >= 0)
			{
				Rect rectInScreenSpace2 = Util.GetRectInScreenSpace(deleteButton.gameObject.GetComponent<RectTransform>());
				if (Util.ExpandRect(rectInScreenSpace2, 0f, 0f, 0f, rectInScreenSpace2.height / 2f).Contains(data.position))
				{
					Analytics.LogAvatarOutfitDeletedAction();
					slotList[slot].DeleteSlot(slot);
					flag = true;
				}
				if (Util.GetRectInScreenSpace(mainAvatar.gameObject.GetComponent<RectTransform>()).Contains(data.position))
				{
					costumePanel.OnClosetSlotClicked(slotList[slot]);
				}
				if (!flag)
				{
					slotList[slot].OnAvatarReturned();
				}
				AvatarAnimationController.Play("HideDelete");
			}
			slot = -1;
			dna = null;
		}

		private void SetDraggedPosition(PointerEventData data)
		{
			RectTransform component = m_DraggingIcon.GetComponent<RectTransform>();
			Vector3 worldPoint;
			if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, new Vector2(data.position.x, data.position.y + yOffset), data.pressEventCamera, out worldPoint))
			{
				component.position = worldPoint;
				component.rotation = m_DraggingPlane.rotation;
			}
		}

		public void UpdateDragIcon(Sprite sprite)
		{
			if (!m_DraggingIcon.IsNullOrDisposed())
			{
				Image component = m_DraggingIcon.GetComponent<Image>();
				if (!component.IsNullOrDisposed())
				{
					component.sprite = sprite;
				}
			}
		}

		public bool IsBusy()
		{
			return currentDraggerSlot != -2;
		}

		public Canvas FindCanvasInParents(GameObject go)
		{
			if (go == null)
			{
				return null;
			}
			Canvas component = go.GetComponent<Canvas>();
			if (component != null)
			{
				return component;
			}
			Transform parent = go.transform.parent;
			while (parent != null && component == null)
			{
				component = parent.gameObject.GetComponent<Canvas>();
				parent = parent.parent;
			}
			return component;
		}
	}
}
