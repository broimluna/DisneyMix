using System;
using System.Collections.Generic;
using UnityEngine;

namespace Disney.Native
{
	public class NativeUtilities : MonoBehaviour
	{
		public event EventHandler<NativeUtilitiesHeadPhoneJackChangedEventArgs> OnNativeUtilitiesHeadPhoneJackChanged = delegate
		{
		};

		public virtual void InitNativeUtilities()
		{
		}

		public virtual void GotoApplicationSettings()
		{
		}

		public virtual void UnGZipFile(string aSourcePath, string aDestPath)
		{
		}

		public virtual bool HasPermissions(List<string> aPermnissions)
		{
			return true;
		}

		public virtual bool AskForPermissions(List<string> aPermnissions, Action<NativeUtilitiesPermissionResult> aCallback)
		{
			return false;
		}

		public void HeadPhonesChanged(string aHeadPhoneAction)
		{
			string aHeadPhoneJackAction = "unknown";
			if (aHeadPhoneAction == "inserted")
			{
				aHeadPhoneJackAction = "headPhoneJackInserted";
			}
			else if (aHeadPhoneAction == "removed")
			{
				aHeadPhoneJackAction = "headPhoneJackRemoved";
			}
			this.OnNativeUtilitiesHeadPhoneJackChanged(this, new NativeUtilitiesHeadPhoneJackChangedEventArgs(aHeadPhoneJackAction));
		}
	}
}
