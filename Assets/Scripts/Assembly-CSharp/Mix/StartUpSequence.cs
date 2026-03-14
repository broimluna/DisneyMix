using System.Collections.Generic;
using Disney.Mix.SDK;
using Mix.Assets;
using Mix.Connectivity;
using Mix.DeviceDb;
using Mix.GagManagement;
using Mix.Native;
using Mix.SequenceOperations;
using Mix.Session;
using Mix.Threading;
using Mix.Tracking;
using Mix.Ui;
using UnityEngine;

namespace Mix
{
	public class StartUpSequence : MonoSingleton<StartUpSequence>, IOperationCompleteHandler
	{
		public const byte IN = 198;

		public bool StartUpSequenceComplete;

		private OperationStack operationStack;

		private float startTime;

		private Dictionary<SequenceOperation, List<SequenceOperation>> operationDependencyChart;

		public void Init()
		{
			MonoSingleton<LifecycleEventDispatcher>.Instance.Init();
			operationStack = new OperationStack(this, this);
			PreloadDataStartOperation preloadDataStartOperation = new PreloadDataStartOperation(operationStack);
			ExternalLibraryStartOperation item = new ExternalLibraryStartOperation(operationStack);
			KeyChainManagerStartOperation item2 = new KeyChainManagerStartOperation(operationStack);
			DeviceDbOpenOperation deviceDbOpenOperation = new DeviceDbOpenOperation(operationStack);
			StartThreadFramerateThrottlingOperation startThreadFramerateThrottlingOperation = new StartThreadFramerateThrottlingOperation(operationStack);
			CertificateValidationStartOperation item3 = new CertificateValidationStartOperation(operationStack);
			AssetManagerStartOperation assetManagerStartOperation = new AssetManagerStartOperation(operationStack);
			LocalizationStartOperation localizationStartOperation = new LocalizationStartOperation(operationStack, this);
			ForceUpdateStartOperation forceUpdateStartOperation = new ForceUpdateStartOperation(operationStack);
			ExternalConfigurationStartOperation externalConfigurationStartOperation = new ExternalConfigurationStartOperation(operationStack);
			PushNotificationsStartOperation pushNotificationsStartOperation = new PushNotificationsStartOperation(operationStack);
			CpipeUpdateOperation cpipeUpdateOperation = new CpipeUpdateOperation(operationStack);
			EntitlementsStartOperation entitlementsStartOperation = new EntitlementsStartOperation(operationStack);
			CheckDiskSpaceStartOperation checkDiskSpaceStartOperation = new CheckDiskSpaceStartOperation(operationStack);
			AvatarManagerStartOperation avatarManagerStartOperation = new AvatarManagerStartOperation(operationStack);
			GameManagerStartOperation item4 = new GameManagerStartOperation(operationStack);
			SoftLoginOperation softLoginOperation = new SoftLoginOperation(operationStack);
			operationDependencyChart = new Dictionary<SequenceOperation, List<SequenceOperation>>();
			operationDependencyChart[preloadDataStartOperation] = new List<SequenceOperation> { item, deviceDbOpenOperation };
			operationDependencyChart[deviceDbOpenOperation] = new List<SequenceOperation> { item2 };
			operationDependencyChart[startThreadFramerateThrottlingOperation] = new List<SequenceOperation> { deviceDbOpenOperation };
			operationDependencyChart[assetManagerStartOperation] = new List<SequenceOperation> { item, deviceDbOpenOperation };
			operationDependencyChart[localizationStartOperation] = new List<SequenceOperation> { assetManagerStartOperation };
			operationDependencyChart[externalConfigurationStartOperation] = new List<SequenceOperation> { assetManagerStartOperation };
			operationDependencyChart[cpipeUpdateOperation] = new List<SequenceOperation> { assetManagerStartOperation, externalConfigurationStartOperation };
			operationDependencyChart[entitlementsStartOperation] = new List<SequenceOperation> { assetManagerStartOperation };
			operationDependencyChart[checkDiskSpaceStartOperation] = new List<SequenceOperation> { assetManagerStartOperation };
			operationDependencyChart[pushNotificationsStartOperation] = new List<SequenceOperation> { externalConfigurationStartOperation };
			operationDependencyChart[avatarManagerStartOperation] = new List<SequenceOperation> { deviceDbOpenOperation };
			operationDependencyChart[forceUpdateStartOperation] = new List<SequenceOperation> { cpipeUpdateOperation, item, externalConfigurationStartOperation, localizationStartOperation, assetManagerStartOperation };
			operationDependencyChart[softLoginOperation] = new List<SequenceOperation>
			{
				forceUpdateStartOperation, startThreadFramerateThrottlingOperation, item3, localizationStartOperation, externalConfigurationStartOperation, pushNotificationsStartOperation, cpipeUpdateOperation, entitlementsStartOperation, checkDiskSpaceStartOperation, avatarManagerStartOperation,
				item4
			};
			operationStack.OperationDependencyChart = operationDependencyChart;
			operationStack.Add(preloadDataStartOperation);
			operationStack.Add(item2);
			operationStack.Add(deviceDbOpenOperation);
			operationStack.Add(startThreadFramerateThrottlingOperation);
			operationStack.Add(item);
			operationStack.Add(assetManagerStartOperation);
			operationStack.Add(localizationStartOperation);
			operationStack.Add(forceUpdateStartOperation);
			operationStack.Add(externalConfigurationStartOperation);
			operationStack.Add(cpipeUpdateOperation);
			operationStack.Add(entitlementsStartOperation);
			operationStack.Add(item3);
			operationStack.Add(checkDiskSpaceStartOperation);
			operationStack.Add(avatarManagerStartOperation);
			operationStack.Add(pushNotificationsStartOperation);
			operationStack.Add(item4);
			operationStack.Add(softLoginOperation);
			startTime = Time.realtimeSinceStartup;
			operationStack.StartNextOperation();
		}

		private void Update()
		{
			if (operationStack != null)
			{
				operationStack.StartNextOperation();
			}
		}

		public void OnOperationComplete(SequenceOperation aOperation)
		{
			bool flag = true;
			foreach (SequenceOperation item in operationStack)
			{
				if (item.status != OperationStatus.STATUS_SUCCESSFUL && item.status != OperationStatus.STATUS_SUCCESSFUL_STILL_FINALIZING)
				{
					flag = false;
				}
			}
			if (!StartUpSequenceComplete && flag)
			{
				Singleton<ThreadFramerateThrottler>.Instance.ExitThrottlingSection();
			}
			StartUpSequenceComplete = flag;
		}

		public void OnApplicationPause(bool goingToBackground)
		{
			if (Singleton<SettingsManager>.Instance != null)
			{
				Singleton<SettingsManager>.Instance.SetUserSettings();
			}
			if (MonoSingleton<GagManager>.Instance != null && goingToBackground)
			{
				MonoSingleton<GagManager>.Instance.ClearGags();
			}
			if (MonoSingleton<ConnectionManager>.Instance != null)
			{
				MonoSingleton<ConnectionManager>.Instance.OnApplicationPausing(goingToBackground);
			}
			if (MonoSingleton<AssetManager>.Instance != null)
			{
			}
			MonoSingleton<NativeKeyboardManager>.Instance.Keyboard.Hide();
			if (!MixSession.IsValidSession)
			{
				return;
			}
			if (goingToBackground)
			{
				MixSession.PauseSession();
				if (MonoSingleton<AssetManager>.Instance != null)
				{
					MonoSingleton<AssetManager>.Instance.OnLowMemoryEvent();
				}
			}
			else
			{
				Singleton<TechAnalytics>.Instance.TrackTimeFromBackgroundStart();
				MixSession.UnPauseSession(delegate
				{
					if (MixSession.IsValidSession)
					{
						Singleton<TechAnalytics>.Instance.TrackTimeFromBackgroundEnd();
						DisplayNameProposedStatus status = MixSession.Session.LocalUser.RegistrationProfile.DisplayNameProposedStatus;
						if (status != DisplayNameProposedStatus.Accepted)
						{
							Singleton<MixDocumentCollections>.Instance.keyValDocumentCollectionApi.SaveUserValueFromInt("displayname.approved.seen", 0);
						}
						MixSession.Session.LocalUser.RefreshProfile(delegate(IRefreshProfileResult result)
						{
							if (result.Success && MixSession.Session != null && !MixSession.Session.IsDisposed && Singleton<PanelManager>.Instance != null && status != MixSession.Session.LocalUser.RegistrationProfile.DisplayNameProposedStatus)
							{
								BasePanel basePanel = Singleton<PanelManager>.Instance.FindPanel(typeof(DisplayNamePanel));
								if (!basePanel.IsNullOrDisposed())
								{
									basePanel.ClosePanel();
								}
								ProfileController.ShowDisplayNamePanels(null);
							}
						});
					}
				});
			}
			if (goingToBackground)
			{
				Singleton<MixDocumentCollections>.Instance.keyValDocumentCollectionApi.SaveUserValueFromInt("bi.unreadMessageCount", (int)MixChat.GetTotalUnreadMessageCount());
			}
			else
			{
				Analytics.LogUnreadChats();
			}
		}

		private void OnApplicationQuit()
		{
		}
	}
}
