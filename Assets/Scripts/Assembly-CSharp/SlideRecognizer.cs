using System.Collections.Generic;
using Mix;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlideRecognizer : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IPointerClickHandler
{
	public delegate void OnSlide(GameObject inst, Vector2 aMoveVector);

	public delegate void OnSlideComplete(GameObject inst);

	public delegate void OnClick(PointerEventData pd);

	public delegate void OnDown(PointerEventData pd);

	public delegate void OnUp(PointerEventData pd);

	public OnSlide onSlide;

	public OnSlideComplete onSlideComplete;

	public OnClick onClick;

	public OnDown onDown;

	public OnUp onUp;

	private Vector2 downAt;

	private Vector2 upAt;

	private TKPanRecognizer recognizer;

	private Camera worldCamera;

	private bool slideStarted;

	private bool downOnObject;

	public void SwitchControlSwipeOpen()
	{
		if (onSlide != null)
		{
			onSlide((GameObject)recognizer.userData, new Vector2(16f, 0f));
		}
		if (onSlideComplete != null)
		{
			onSlideComplete(new GameObject("open"));
		}
	}

	public void SwitchControlSwipeClose()
	{
		if (onSlide != null)
		{
			onSlide((GameObject)recognizer.userData, new Vector2(0f, 0f));
		}
		if (onSlideComplete != null)
		{
			onSlideComplete(new GameObject("close"));
		}
	}

	private void Start()
	{
		recognizer = new TKPanRecognizer();
		recognizer.minimumNumberOfTouches = 1;
		recognizer.userData = base.gameObject;
		UpdateTKRect();
		recognizer.gestureRecognizedEvent += delegate(TKPanRecognizer r)
		{
			if (!slideStarted)
			{
				PointerEventData eventData = new PointerEventData(EventSystem.current)
				{
					position = new Vector2(r.touchLocation().x, r.touchLocation().y)
				};
				List<RaycastResult> list = new List<RaycastResult>();
				EventSystem.current.RaycastAll(eventData, list);
				if (list.Count > 0)
				{
					if (list[0].gameObject.transform.IsChildOf(base.gameObject.transform))
					{
						downOnObject = true;
					}
				}
				else
				{
					downOnObject = false;
				}
				slideStarted = true;
			}
			if (onSlide != null && downOnObject)
			{
				Vector2 aMoveVector = new Vector2(r.deltaTranslation.x * Singleton<SettingsManager>.Instance.GetWidthScale(), r.deltaTranslation.y * Singleton<SettingsManager>.Instance.GetHeightScale());
				onSlide((GameObject)r.userData, aMoveVector);
			}
		};
		recognizer.gestureCompleteEvent += delegate(TKPanRecognizer r)
		{
			if (onSlideComplete != null && downOnObject)
			{
				onSlideComplete((GameObject)r.userData);
			}
			slideStarted = false;
			downOnObject = false;
		};
		TouchKit.addGestureRecognizer(recognizer);
	}

	private void Update()
	{
		UpdateTKRect();
	}

	private void OnDestroy()
	{
		if (recognizer != null)
		{
			TouchKit.removeGestureRecognizer(recognizer);
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (onDown != null)
		{
			onDown(eventData);
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (onUp != null)
		{
			onUp(eventData);
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (onClick != null)
		{
			onClick(eventData);
		}
	}

	private void UpdateTKRect()
	{
		if (worldCamera == null)
		{
			Canvas parentCanvas = Util.GetParentCanvas(base.gameObject);
			if (parentCanvas == null)
			{
				return;
			}
			worldCamera = parentCanvas.worldCamera;
		}
		RectTransform component = GetComponent<RectTransform>();
		if (recognizer != null && component.hasChanged)
		{
			component.hasChanged = false;
			Vector3[] array = new Vector3[4];
			component.GetWorldCorners(array);
			Vector2 vector = RectTransformUtility.WorldToScreenPoint(worldCamera, array[0]);
			Vector2 vector2 = RectTransformUtility.WorldToScreenPoint(worldCamera, array[2]);
			float width = vector2.x - vector.x;
			float height = vector2.y - vector.y;
			float x = vector.x;
			float y = vector.y;
			recognizer.boundaryFrame = new TKRect(x, y, width, height);
		}
	}
}
