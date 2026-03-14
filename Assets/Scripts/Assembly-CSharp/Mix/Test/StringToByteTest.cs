using Mix.Assets.Worker;
using UnityEngine;

namespace Mix.Test
{
	public class StringToByteTest : MonoBehaviour, IStringToByte
	{
		private void Start()
		{
			TestingUtils.ClearAllCache();
			STB();
		}

		private void Update()
		{
		}

		public void STB()
		{
			string s = "my string";
			new StringToByte(this, s);
		}

		public void OnStringToByte(byte[] bytes, object aUserData)
		{
			if (bytes != null && bytes.Length > 0)
			{
				IntegrationTest.Pass(base.gameObject);
			}
			else
			{
				IntegrationTest.Fail(base.gameObject);
			}
		}
	}
}
