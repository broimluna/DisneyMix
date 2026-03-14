using Mix.Localization;
using UnityEngine;

namespace Mix.Ui
{
	public class AvatarCategoryButton : MonoBehaviour
	{
		public string disableEntitlementId;

		public string CategoryLocToken;

		private void Start()
		{
		}

		private void Update()
		{
		}

		public string GetLocText()
		{
			return Singleton<Localizer>.Instance.getString(CategoryLocToken);
		}
	}
}
