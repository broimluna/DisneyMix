using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.Friendzy.Menu
{
	[RequireComponent(typeof(RectTransform))]
	public class ContentFitter : MonoBehaviour
	{
		public HorizontalOrVerticalLayoutGroup layoutGroup;

		public RectTransform RectTrans;

		public float WidthOfElement;

		public float HeightOfElement;

		private float NumOfHorizElements;

		private float NumOfVertElements;

		private float HorizOffset;

		private float VertOffset;

		private void Awake()
		{
			HorizOffset = layoutGroup.padding.left + layoutGroup.padding.right;
			VertOffset = layoutGroup.padding.top + layoutGroup.padding.bottom;
		}

		public void UpdateSize(Vector2 aSizeDelta)
		{
			RectTrans.sizeDelta = aSizeDelta;
		}

		public void UpdateSize(int aNumOfHorizElements, int aNumOfVertElements)
		{
			NumOfHorizElements = aNumOfHorizElements;
			NumOfVertElements = aNumOfVertElements;
			UpdateSize(new Vector2(WidthOfElement * NumOfHorizElements + layoutGroup.spacing * (NumOfHorizElements - 1f) + HorizOffset, HeightOfElement * NumOfVertElements + layoutGroup.spacing * (NumOfVertElements - 1f) + VertOffset));
		}

		public void UpdateSize(float aSizeOfElement, int aNumOfHorizElements, int aNumOfVertElements)
		{
			HeightOfElement = (WidthOfElement = aSizeOfElement);
			UpdateSize(aNumOfHorizElements, aNumOfVertElements);
		}

		public void UpdateSize(float aWidthOfElement, float aHeightOfElement, int aNumOfHorizElements, int aNumOfVertElements)
		{
			WidthOfElement = aWidthOfElement;
			HeightOfElement = aHeightOfElement;
			UpdateSize(aNumOfHorizElements, aNumOfVertElements);
		}
	}
}
