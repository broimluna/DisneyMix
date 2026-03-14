using UnityEngine;

namespace Mix.Games.Tray.Common.Util
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(Renderer))]
	public class SetRenderLayer : MonoBehaviour
	{
		public int SortingLayerId;

		public int OrderInLayer;

		private void OnEnable()
		{
			Renderer component = GetComponent<Renderer>();
			if (component != null)
			{
				component.sortingLayerID = SortingLayerId;
				component.sortingOrder = OrderInLayer;
			}
		}
	}
}
