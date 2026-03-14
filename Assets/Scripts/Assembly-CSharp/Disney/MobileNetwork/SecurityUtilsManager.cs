using UnityEngine;

namespace Disney.MobileNetwork
{
	public class SecurityUtilsManager : MonoBehaviour
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

		public virtual bool IsDebuggerAttached()
		{
			return Application.genuineCheckAvailable && Application.genuine;
		}
	}
}
