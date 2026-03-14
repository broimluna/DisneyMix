using UnityEngine;

namespace Disney.MobileNetwork
{
	public class ReferralStoreAndroidManager : ReferralStoreManager
	{
		public override void Show()
		{
			Logger.LogDebug(this, "Referral store show called in Android");
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject androidJavaObject = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.mobilenetwork.referralstore.DMNReferralStoreActivity");
			AndroidJavaObject androidJavaObject2 = new AndroidJavaObject("android.content.Intent", androidJavaObject, androidJavaClass2);
			androidJavaObject.Call("startActivity", androidJavaObject2);
		}
	}
}
