using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class KeyboardTray : MonoBehaviour
	{
		public ChatBar ChatBar;

		public void UpdateState(bool aState)
		{
			base.gameObject.SetActive(aState);
		}

		public void SetSize(float aHeight)
		{
			GetComponent<LayoutElement>().minHeight = aHeight;
		}
	}
}
