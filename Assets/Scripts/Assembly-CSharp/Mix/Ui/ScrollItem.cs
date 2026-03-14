using UnityEngine;

namespace Mix.Ui
{
	public class ScrollItem
	{
		public GameObject ItemGameObject { get; set; }

		public long Id { get; set; }

		public float Height { get; set; }

		public IScrollItem Generator { get; set; }

		public float Position { get; set; }

		public ScrollItem(IScrollItem aGenerator, long aId)
		{
			Generator = aGenerator;
			Id = aId;
		}

		public void Generate(bool aGenerateForHeightOnly)
		{
			ItemGameObject = Generator.GenerateGameObject(aGenerateForHeightOnly);
			Height = ItemGameObject.GetComponent<RectTransform>().rect.height;
		}

		public void Update()
		{
			Position = Mathf.Abs(ItemGameObject.transform.localPosition.y);
			Height = ItemGameObject.GetComponent<RectTransform>().rect.height;
		}

		public void Destroy()
		{
			Generator.Destroy();
		}
	}
}
