using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mix.Games.Tray.Drop
{
	public class DropInputHandler : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IDragHandler
	{
		public Action<PointerEventData> OnTouchBegin = delegate
		{
		};

		public Action<PointerEventData> OnTouchEnd = delegate
		{
		};

		public Action<PointerEventData> OnTouchDrag = delegate
		{
		};

		private void Update()
		{
			base.transform.SetAsFirstSibling();
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			OnTouchBegin(eventData);
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			OnTouchEnd(eventData);
		}

		public void OnDrag(PointerEventData eventData)
		{
			OnTouchDrag(eventData);
		}
	}
}
