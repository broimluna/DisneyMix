using System;

namespace Disney.Native
{
	public class NativeUtilitiesHeadPhoneJackChangedEventArgs : EventArgs
	{
		public const string HEAD_PHONE_JACK_REMOVED = "headPhoneJackRemoved";

		public const string HEAD_PHONE_JACK_INSERTED = "headPhoneJackInserted";

		public string HeadPhoneJackAction { get; set; }

		public NativeUtilitiesHeadPhoneJackChangedEventArgs(string aHeadPhoneJackAction)
		{
			HeadPhoneJackAction = aHeadPhoneJackAction;
		}
	}
}
