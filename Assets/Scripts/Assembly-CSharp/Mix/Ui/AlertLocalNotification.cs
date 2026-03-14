using System.Collections.Generic;
using Disney.Mix.SDK;
using Mix.Localization;
using Mix.Session;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class AlertLocalNotification : BaseNofitication
	{
		public IAlert Alert;

		public AlertLocalNotification(IAlert aAlert)
		{
			Alert = aAlert;
		}

		public override GameObject GenerateGameObject()
		{
			List<IAlert> list = new List<IAlert>();
			list.Add(Alert);
			MixSession.User.ClearAlerts(list, OnClearAlert);
			GameObject original = Resources.Load<GameObject>("Prefabs/Ui/WarningPanel");
			GameObject gameObject = Object.Instantiate(original);
			gameObject.GetComponentInChildren<Text>().text = Singleton<Localizer>.Instance.getString(AlertTokenMappings.mappings[string.Concat(Alert.Type, "_", Alert.Level)]);
			return gameObject;
		}

		private void OnClearAlert(IClearAlertsResult aResult)
		{
			if (aResult.Success)
			{
			}
		}
	}
}
