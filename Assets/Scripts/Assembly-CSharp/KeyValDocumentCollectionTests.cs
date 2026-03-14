using System.Diagnostics;
using Mix;
using Mix.DeviceDb;
using UnityEngine;

public class KeyValDocumentCollectionTests : MonoBehaviour
{
	private void Start()
	{
		Mix.Application.TestingDirectory = "/KeyValDocumentCollectionTests/";
		Singleton<MixDocumentCollections>.Instance.ClearKeyValApiAndCollection();
		KeyValDocumentCollectionApi keyValDocumentCollectionApi = Singleton<MixDocumentCollections>.Instance.keyValDocumentCollectionApi;
		keyValDocumentCollectionApi.SaveDeviceValue("string1key", "string1value");
		keyValDocumentCollectionApi.SaveDeviceValueFromBool("bool1true", true);
		keyValDocumentCollectionApi.SaveDeviceValueFromBool("bool2true", false);
		keyValDocumentCollectionApi.SaveDeviceValueFromFloat("float11.0345", 1.0345f);
		keyValDocumentCollectionApi.SaveDeviceValueFromInt("int11", 11);
		string text = keyValDocumentCollectionApi.LoadDeviceValue("string1key");
		bool flag = keyValDocumentCollectionApi.LoadDeviceValueAsBool("bool1true", false);
		bool flag2 = keyValDocumentCollectionApi.LoadDeviceValueAsBool("bool2true", true);
		float num = keyValDocumentCollectionApi.LoadDeviceValueAsFloat("float11.0345", 0.01f);
		int num2 = keyValDocumentCollectionApi.LoadDeviceValueAsInt("int11", 0);
		bool flag3 = keyValDocumentCollectionApi.LoadDeviceValueAsBool("notthere", true);
		float num3 = keyValDocumentCollectionApi.LoadDeviceValueAsFloat("notthere", 1f);
		int num4 = keyValDocumentCollectionApi.LoadDeviceValueAsInt("notthere", 1);
		if (text != "string1value")
		{
			FailTest("string value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		if (!flag)
		{
			FailTest("bool value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		if (flag2)
		{
			FailTest("bool value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		if (num != 1.0345f)
		{
			FailTest("float value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		if (num2 != 11)
		{
			FailTest("int value not correctn " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		if (!flag3)
		{
			FailTest("default bool value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		if (num3 != 1f)
		{
			FailTest("default float value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		if (num4 != 1)
		{
			FailTest("default int value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		keyValDocumentCollectionApi.SaveDeviceValue("string1key", "string1valueUpdated");
		keyValDocumentCollectionApi.SaveDeviceValueFromBool("bool1true", false);
		keyValDocumentCollectionApi.SaveDeviceValueFromBool("bool2true", true);
		keyValDocumentCollectionApi.SaveDeviceValueFromFloat("float11.0345", 1.2f);
		keyValDocumentCollectionApi.SaveDeviceValueFromInt("int11", 22);
		text = keyValDocumentCollectionApi.LoadDeviceValue("string1key");
		flag = keyValDocumentCollectionApi.LoadDeviceValueAsBool("bool1true", false);
		flag2 = keyValDocumentCollectionApi.LoadDeviceValueAsBool("bool2true", true);
		num = keyValDocumentCollectionApi.LoadDeviceValueAsFloat("float11.0345", 0.1f);
		num2 = keyValDocumentCollectionApi.LoadDeviceValueAsInt("int11", 0);
		flag3 = keyValDocumentCollectionApi.LoadDeviceValueAsBool("notthere", false);
		num3 = keyValDocumentCollectionApi.LoadDeviceValueAsFloat("notthere", 0f);
		num4 = keyValDocumentCollectionApi.LoadDeviceValueAsInt("notthere", 2);
		if (text != "string1valueUpdated")
		{
			FailTest("string value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		if (flag)
		{
			FailTest("bool value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		if (!flag2)
		{
			FailTest("bool value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		if (num != 1.2f)
		{
			FailTest("float value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		if (num2 != 22)
		{
			FailTest("int value not correctn " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		if (flag3)
		{
			FailTest("default bool value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		if (num3 != 0f)
		{
			FailTest("default float value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		if (num4 != 2)
		{
			FailTest("default int value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		keyValDocumentCollectionApi.SaveUserValue("string1key", "string1value");
		keyValDocumentCollectionApi.SaveUserValueFromBool("bool1true", true);
		keyValDocumentCollectionApi.SaveUserValueFromBool("bool2true", false);
		keyValDocumentCollectionApi.SaveUserValueFromFloat("float11.0345", 1.0345f);
		keyValDocumentCollectionApi.SaveUserValueFromInt("int11", 11);
		text = keyValDocumentCollectionApi.LoadUserValue("string1key");
		flag = keyValDocumentCollectionApi.LoadUserValueAsBool("bool1true");
		flag2 = keyValDocumentCollectionApi.LoadUserValueAsBool("bool2true", true);
		num = keyValDocumentCollectionApi.LoadUserValueAsFloat("float11.0345", 0.01f);
		num2 = keyValDocumentCollectionApi.LoadUserValueAsInt("int11");
		flag3 = keyValDocumentCollectionApi.LoadUserValueAsBool("notthere", true);
		num3 = keyValDocumentCollectionApi.LoadUserValueAsFloat("notthere", 1f);
		num4 = keyValDocumentCollectionApi.LoadUserValueAsInt("notthere", 1);
		keyValDocumentCollectionApi.SaveUserValue("string1keyUser2", "string1value");
		keyValDocumentCollectionApi.SaveUserValueFromBool("bool1trueUser2", true);
		keyValDocumentCollectionApi.SaveUserValueFromBool("bool2trueUser2", false);
		keyValDocumentCollectionApi.SaveUserValueFromFloat("float11.0345User2", 1.0345f);
		keyValDocumentCollectionApi.SaveUserValueFromInt("int11User2", 11);
		string text2 = keyValDocumentCollectionApi.LoadUserValue("string1keyUser2");
		bool flag4 = keyValDocumentCollectionApi.LoadUserValueAsBool("bool1trueUser2");
		bool flag5 = keyValDocumentCollectionApi.LoadUserValueAsBool("bool2trueUser2", true);
		float num5 = keyValDocumentCollectionApi.LoadUserValueAsFloat("float11.0345User2", 0.01f);
		int num6 = keyValDocumentCollectionApi.LoadUserValueAsInt("int11User2");
		bool flag6 = keyValDocumentCollectionApi.LoadUserValueAsBool("notthereUser2", true);
		float num7 = keyValDocumentCollectionApi.LoadUserValueAsFloat("notthereUser2", 1f);
		int num8 = keyValDocumentCollectionApi.LoadUserValueAsInt("notthereUser2", 1);
		if (text2 != "string1value")
		{
			FailTest("string value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		if (!flag4)
		{
			FailTest("bool value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		if (flag5)
		{
			FailTest("bool value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		if (num5 != 1.0345f)
		{
			FailTest("float value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		if (num6 != 11)
		{
			FailTest("int value not correctn " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		if (!flag6)
		{
			FailTest("default bool value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		if (num7 != 1f)
		{
			FailTest("default float value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		if (num8 != 1)
		{
			FailTest("default int value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		if (text != "string1value")
		{
			FailTest("string value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		if (!flag)
		{
			FailTest("bool value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		if (flag2)
		{
			FailTest("bool value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		if (num != 1.0345f)
		{
			FailTest("float value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		if (num2 != 11)
		{
			FailTest("int value not correctn " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		if (!flag3)
		{
			FailTest("default bool value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		if (num3 != 1f)
		{
			FailTest("default float value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		if (num4 != 1)
		{
			FailTest("default int value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		keyValDocumentCollectionApi.LogOut();
		if (keyValDocumentCollectionApi.collections.Count != 1)
		{
			FailTest("logout failed.");
			return;
		}
		keyValDocumentCollectionApi.SaveUserValue("rk1", "val");
		string text3 = keyValDocumentCollectionApi.LoadUserValue("rk1");
		if (text3 != "val")
		{
			FailTest("save/load value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		keyValDocumentCollectionApi.RemoveUserKey("rk1");
		text3 = keyValDocumentCollectionApi.LoadUserValue("rk1");
		if (!string.IsNullOrEmpty(text3))
		{
			FailTest("remvoe value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		keyValDocumentCollectionApi.SaveDeviceValue("rk1", "val");
		text3 = keyValDocumentCollectionApi.LoadDeviceValue("rk1");
		if (text3 != "val")
		{
			FailTest("save/load value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			return;
		}
		keyValDocumentCollectionApi.RemoveDeviceKey("rk1");
		text3 = keyValDocumentCollectionApi.LoadDeviceValue("rk1");
		if (!string.IsNullOrEmpty(text3))
		{
			FailTest("remvoe value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
		}
		else
		{
			PassTest();
		}
	}

	private void Update()
	{
	}

	public void FailTest(string reason)
	{
		Singleton<MixDocumentCollections>.Instance.ClearKeyValApiAndCollection();
		Mix.Application.TestingDirectory = string.Empty;
		IntegrationTest.Fail(base.gameObject, reason);
	}

	public void PassTest()
	{
		Singleton<MixDocumentCollections>.Instance.ClearKeyValApiAndCollection();
		Mix.Application.TestingDirectory = string.Empty;
		IntegrationTest.Pass(base.gameObject);
	}
}
