using UnityEngine;

namespace Disney.MobileNetwork
{
	public class NativeLogger : MonoBehaviour
	{
		public void Awake()
		{
			Service.Set(this);
			Init();
			Object.DontDestroyOnLoad(base.gameObject);
		}

		protected virtual void Init()
		{
		}

		public virtual void SendLog(string message, string stackTrace, LogType type)
		{
		}
	}
}
