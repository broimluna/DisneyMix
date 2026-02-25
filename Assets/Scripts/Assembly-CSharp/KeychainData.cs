using System;
using System.Security.Cryptography;
using Disney.Mix.SDK;
using Disney.MobileNetwork;

public class KeychainData : IKeychain
{
	private byte[] localStorageKey = new byte[32];

	public byte[] LocalStorageKey
	{
		get
		{
			return GetOrGenerateLocalStorageKey();
		}
	}

	public byte[] PushNotificationKey
	{
		set
		{
			SetPushNotificationKey(value);
		}
	}

	private byte[] GetOrGenerateLocalStorageKey()
	{
		string text = Service.Get<KeyChainManager>().GetString("SessionUnlockKey");
		if (string.IsNullOrEmpty(text))
		{
			new RNGCryptoServiceProvider().GetBytes(localStorageKey);
			Service.Get<KeyChainManager>().PutString("SessionUnlockKey", Convert.ToBase64String(localStorageKey));
		}
		else
		{
			localStorageKey = Convert.FromBase64String(text);
		}
		return localStorageKey;
	}

	private void SetPushNotificationKey(byte[] aKey)
	{
		if (aKey != null)
		{
			Service.Get<KeyChainManager>().PutString("PushNotificationUnlockKey", Convert.ToBase64String(aKey));
		}
		else
		{
			Service.Get<KeyChainManager>().RemoveString("PushNotificationUnlockKey");
		}
	}
}
