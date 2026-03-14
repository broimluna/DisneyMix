using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Mix
{
	public class EncryptionUtil
	{
		public static string Decrypt(string aValue, byte[] aKey, byte[] aInitVec, bool urlSafe = false)
		{
			try
			{
				byte[] cipherText = ((!urlSafe) ? Convert.FromBase64String(aValue) : Convert.FromBase64String(base64Rfc4686Decode(aValue)));
				return decryptStringFromBytes(cipherText, aKey, aInitVec);
			}
			catch (Exception exception)
			{
				Log.Exception("Unable to decrypt string", exception);
				return string.Empty;
			}
		}

		public static string EncryptSha1(string aValue, byte[] aKey, bool urlSafe = false)
		{
			try
			{
				byte[] bytes = Encoding.UTF8.GetBytes(aValue);
				using (AesManaged aesManaged = new AesManaged())
				{
					aesManaged.Key = aKey;
					aesManaged.Mode = CipherMode.ECB;
					aesManaged.Padding = PaddingMode.PKCS7;
					ICryptoTransform cryptoTransform = aesManaged.CreateEncryptor();
					byte[] inArray = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
					cryptoTransform.Dispose();
					string text = Convert.ToBase64String(inArray);
					if (urlSafe)
					{
						text = base64Rfc4686Encode(text);
					}
					return text;
				}
			}
			catch (Exception exception)
			{
				Log.Exception("Unable to encrypt string", exception);
				return string.Empty;
			}
		}

		private static string decryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
		{
			if (cipherText == null || cipherText.Length <= 0)
			{
				throw new ArgumentNullException("cipherText");
			}
			if (Key == null || Key.Length <= 0)
			{
				throw new ArgumentNullException("Key");
			}
			if (IV == null || IV.Length <= 0)
			{
				throw new ArgumentNullException("IV");
			}
			string result = null;
			using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
			{
				rijndaelManaged.Key = Key;
				rijndaelManaged.IV = IV;
				ICryptoTransform transform = rijndaelManaged.CreateDecryptor(rijndaelManaged.Key, rijndaelManaged.IV);
				using (MemoryStream memoryStream = new MemoryStream(cipherText))
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read))
					{
						using (StreamReader streamReader = new StreamReader(cryptoStream))
						{
							result = streamReader.ReadToEnd();
							streamReader.Dispose();
						}
						cryptoStream.Dispose();
					}
					memoryStream.Dispose();
					return result;
				}
			}
		}

		private static string base64Rfc4686Encode(string aValue)
		{
			if (string.IsNullOrEmpty(aValue))
			{
				return aValue;
			}
			return aValue.Replace("+", "-").Replace("/", "_");
		}

		private static string base64Rfc4686Decode(string aValue)
		{
			if (string.IsNullOrEmpty(aValue))
			{
				return aValue;
			}
			return aValue.Replace("-", "+").Replace("_", "/");
		}
	}
}
