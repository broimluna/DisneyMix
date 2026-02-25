using Mix.Session;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class ContactCustomerSupportPanel : BasePanel
	{
		public interface IContactCustomerSupportPanelListener
		{
			void ContactCustomerSupport();
		}

		public Text Email;

		public Button ContactCustomerSupport;

		private IContactCustomerSupportPanelListener currentListener;

		private void Start()
		{
		}

		public void Init(IContactCustomerSupportPanelListener aListener)
		{
			currentListener = aListener;
			Email.text = MixSession.User.RegistrationProfile.Email;
		}

		public void OnContactCustomerSupport()
		{
			currentListener.ContactCustomerSupport();
		}
	}
}
