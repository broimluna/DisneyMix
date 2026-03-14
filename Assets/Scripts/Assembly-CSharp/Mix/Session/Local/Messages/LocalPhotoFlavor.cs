using System;
using System.IO;
using Disney.Mix.SDK;
using Disney.Mix.SDK.Internal;

namespace Mix.Session.Local.Messages
{
	public class LocalPhotoFlavor : IPhotoFlavor
	{
		private string photoPath;

		public int Height { get; private set; }

		public int Width { get; private set; }

		public PhotoEncoding Encoding { get; private set; }

		public LocalPhotoFlavor(string filePath, PhotoEncoding encoding, int width, int height, string messageId)
		{
			Encoding = encoding;
			Width = width;
			Height = height;
			photoPath = GetEncryptedFilePath() + messageId;
			if (!File.Exists(photoPath))
			{
				byte[] bytes = File.ReadAllBytes(filePath);
				MixEncryptor mixEncryptor = new MixEncryptor();
				KeychainData keychainData = new KeychainData();
				byte[] bytes2 = mixEncryptor.Encrypt(bytes, keychainData.LocalStorageKey);
				if (!Directory.Exists(GetEncryptedFilePath()))
				{
					Directory.CreateDirectory(GetEncryptedFilePath());
				}
				File.WriteAllBytes(photoPath, bytes2);
				File.Delete(filePath);
			}
		}

		public void GetFile(Action<IGetPhotoFlavorFileResult> callback)
		{
			callback(new LocalGetPhotoFlavorFileResult(photoPath));
		}

		private string GetEncryptedFilePath()
		{
			return Application.PersistentDataPath + "/fakefriendmedia/";
		}
	}
}
