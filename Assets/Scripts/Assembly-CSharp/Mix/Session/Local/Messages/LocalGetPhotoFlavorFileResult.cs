using System.IO;
using Disney.Mix.SDK;
using Disney.Mix.SDK.Internal;

namespace Mix.Session.Local.Messages
{
	public class LocalGetPhotoFlavorFileResult : IGetPhotoFlavorFileResult
	{
		private string filePath;

		private bool success;

		public bool Success
		{
			get
			{
				return success;
			}
		}

		public byte[] File
		{
			get
			{
				MixEncryptor mixEncryptor = new MixEncryptor();
				KeychainData keychainData = new KeychainData();
				try
				{
					return mixEncryptor.Decrypt(System.IO.File.ReadAllBytes(filePath), keychainData.LocalStorageKey);
				}
				catch (IOException)
				{
					return null;
				}
			}
		}

		public LocalGetPhotoFlavorFileResult(string filePath)
		{
			success = System.IO.File.Exists(filePath);
			this.filePath = filePath;
		}
	}
}
