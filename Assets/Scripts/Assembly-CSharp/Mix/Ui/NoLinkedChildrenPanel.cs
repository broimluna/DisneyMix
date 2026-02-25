using Mix.Session;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class NoLinkedChildrenPanel : BasePanel
	{
		public Text Email;

		private void Start()
		{
		}

		public void Init()
		{
			Email.text = MixSession.User.RegistrationProfile.Email;
		}
	}
}
