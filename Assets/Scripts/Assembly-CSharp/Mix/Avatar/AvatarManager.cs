using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Avatar;
using Avatar.DataTypes;
using Disney.Mix.SDK;
using Mix.Assets;
using Mix.DeviceDb;
using Mix.Entitlements;
using Mix.Session;
using Mix.Session.Extensions;
using Mix.Tracking;
using Mix.Ui;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Avatar
{
	public class AvatarManager : MonoSingleton<AvatarManager>, IAvatarSnapshotManagerDependencies, IAvatarSnapshotCallDependencies
	{
		public sealed class GenerateSnapshotFromDna_003Ec__AnonStorey22A
		{
            public sealed class GenerateSnapshotFromDna_003Ec__AnonStorey22B
			{
				internal bool success;

				internal GenerateSnapshotFromDna_003Ec__AnonStorey22A _003C_003Ef__ref_0024554;

				internal void _003C_003Em__466(AvatarSnapshotData snapshotData)
				{
					if (!_003C_003Ef__ref_0024554.cancelled)
					{
						_003C_003Ef__ref_0024554.callback(new AvatarSnapshotResult(success && snapshotData != null, _003C_003Ef__ref_0024554.snapshotId, snapshotData));
					}
					_003C_003Ef__ref_0024554._003C_003Ef__this.skinnerWork.Remove(_003C_003Ef__ref_0024554.doWork);
					_003C_003Ef__ref_0024554._003C_003Ef__this.iterateThroughSkinnerQueue();
				}
			}

			public sealed class GenerateSnapshotFromDna_003Ec__AnonStorey22C
			{
				internal bool compositeResult;

				internal GenerateSnapshotFromDna_003Ec__AnonStorey22A _003C_003Ef__ref_0024554;

				internal void _003C_003Em__468(bool aIsSuccess, string id)
				{
					_003C_003Ef__ref_0024554.finalRender(compositeResult);
				}
			}

			internal bool cancelled;

			internal Action doWork;

			internal GameObject cubeRigGameObject;

			internal int size;

			internal AvatarFlags flags;

			internal Vector3 avatarRotation;

			internal Vector3 avatarOffset;

			internal bool hasGeo;

			internal Action<AvatarSnapshotResult> callback;

			internal string snapshotId;

			internal bool started;

			internal Action compositeCancel;

			internal Action<bool> finalRender;

			internal IAvatar dna;

			internal AvatarManager _003C_003Ef__this;

			internal void _003C_003Em__454(bool success)
			{
				GenerateSnapshotFromDna_003Ec__AnonStorey22B CS_0024_003C_003E8__locals9 = new GenerateSnapshotFromDna_003Ec__AnonStorey22B();
				CS_0024_003C_003E8__locals9._003C_003Ef__ref_0024554 = this;
				CS_0024_003C_003E8__locals9.success = success;
				if (cancelled)
				{
					_003C_003Ef__this.skinnerWork.Remove(doWork);
					_003C_003Ef__this.iterateThroughSkinnerQueue();
					return;
				}
				_003C_003Ef__this.RenderAvatarSnapshot(cubeRigGameObject, size, flags, delegate(AvatarSnapshotData snapshotData)
				{
					if (!CS_0024_003C_003E8__locals9._003C_003Ef__ref_0024554.cancelled)
					{
						CS_0024_003C_003E8__locals9._003C_003Ef__ref_0024554.callback(new AvatarSnapshotResult(CS_0024_003C_003E8__locals9.success && snapshotData != null, CS_0024_003C_003E8__locals9._003C_003Ef__ref_0024554.snapshotId, snapshotData));
					}
					CS_0024_003C_003E8__locals9._003C_003Ef__ref_0024554._003C_003Ef__this.skinnerWork.Remove(CS_0024_003C_003E8__locals9._003C_003Ef__ref_0024554.doWork);
					CS_0024_003C_003E8__locals9._003C_003Ef__ref_0024554._003C_003Ef__this.iterateThroughSkinnerQueue();
				}, avatarRotation, avatarOffset, hasGeo);
			}

			internal void _003C_003Em__455()
			{
				if (!started)
				{
					if (doWork != null)
					{
						_003C_003Ef__this.skinnerWork.Remove(doWork);
					}
				}
				else if (compositeCancel != null)
				{
					compositeCancel();
					cancelled = true;
					finalRender(false);
				}
				else
				{
					cancelled = true;
				}
			}

			internal void _003C_003Em__456()
			{
				started = true;
				Singleton<TechAnalytics>.Instance.TrackTimeToCompositeAvatarStart(_003C_003Ef__this.snapshotScene.gameObject);
				compositeCancel = _003C_003Ef__this.SkinAvatar(cubeRigGameObject, dna, flags, delegate(bool compositeResult, string cacheId)
				{
					GenerateSnapshotFromDna_003Ec__AnonStorey22C CS_0024_003C_003E8__locals5 = new GenerateSnapshotFromDna_003Ec__AnonStorey22C();
					CS_0024_003C_003E8__locals5._003C_003Ef__ref_0024554 = this;
					CS_0024_003C_003E8__locals5.compositeResult = compositeResult;
					compositeCancel = null;
					_003C_003Ef__this.HandleApiCallback(cubeRigGameObject, CS_0024_003C_003E8__locals5.compositeResult, cacheId, delegate
					{
						CS_0024_003C_003E8__locals5._003C_003Ef__ref_0024554.finalRender(CS_0024_003C_003E8__locals5.compositeResult);
					});
				});
			}

			internal void _003C_003Em__467(bool compositeResult, string cacheId)
			{
				GenerateSnapshotFromDna_003Ec__AnonStorey22C CS_0024_003C_003E8__locals5 = new GenerateSnapshotFromDna_003Ec__AnonStorey22C();
				CS_0024_003C_003E8__locals5._003C_003Ef__ref_0024554 = this;
				CS_0024_003C_003E8__locals5.compositeResult = compositeResult;
				compositeCancel = null;
				_003C_003Ef__this.HandleApiCallback(cubeRigGameObject, CS_0024_003C_003E8__locals5.compositeResult, cacheId, delegate
				{
					CS_0024_003C_003E8__locals5._003C_003Ef__ref_0024554.finalRender(CS_0024_003C_003E8__locals5.compositeResult);
				});
			}
		}

		private sealed class LoadSnapshots_003Ec__AnonStorey22D
		{
			internal LoadSnapshotCallback callback;

			internal AvatarSnapshotData ret;

			internal void _003C_003Em__457(Texture2D texture)
			{
				if (texture == null)
				{
					callback(null);
					return;
				}
				Sprite snapshot = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f);
				ret.snapshot = snapshot;
				ret.loadPercentage = 1f;
				callback(ret);
			}
		}

		public AvatarApi api;

		private GameObject avatarPreloader;

		private GameObject snapshotScene;

		private GameObject snapshotSceneGeo;

		private AvatarSnapshotCache snapshotCache;

		private AvatarAssetFramework assetFramework;

		private AsyncHelper<AvatarSnapshotResult> snapshotHelper;

		private List<Action> skinnerWork;

		private SdkActions actionGenerator = new SdkActions();

		private SdkEvents eventGenerator = new SdkEvents();

		private ClientAvatar clientAvatar;

		private static readonly Vector3 AVATAR_OFFSET = new Vector3(0f, -0.505f, 3.35f);

		private static readonly Vector3 AVATAR_ROTATION = new Vector3(0f, 180f, 0f);

		private bool isSnapshotAvailable = true;

		private bool isPreloaderAvailable = true;

		private bool canUseAntiAliasing = true;

		public IAvatar CurrentDna
		{
			get
			{
				IAvatar result;
				if (clientAvatar != null)
				{
					IAvatar avatar = clientAvatar;
					result = avatar;
				}
				else if (MixSession.User == null)
				{
					IAvatar avatar = null;
					result = avatar;
				}
				else
				{
					result = MixSession.User.Avatar;
				}
				return result;
			}
		}

		void IAvatarSnapshotManagerDependencies.LoadSnapshots(AvatarSnapshotDocument snapshotInfo, LoadSnapshotCallback callback)
		{
			LoadSnapshots_003Ec__AnonStorey22D CS_0024_003C_003E8__locals7 = new LoadSnapshots_003Ec__AnonStorey22D();
			CS_0024_003C_003E8__locals7.callback = callback;
			CS_0024_003C_003E8__locals7.ret = new AvatarSnapshotData();
			int size = snapshotInfo.size;
			Texture2D image = new Texture2D(size, size, TextureFormat.ARGB32, false);
			MonoSingleton<AssetManager>.Instance.LoadImage(snapshotInfo.path, ref image, delegate(Texture2D texture)
			{
				if (texture == null)
				{
					CS_0024_003C_003E8__locals7.callback(null);
				}
				else
				{
					Sprite snapshot = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f);
					CS_0024_003C_003E8__locals7.ret.snapshot = snapshot;
					CS_0024_003C_003E8__locals7.ret.loadPercentage = 1f;
					CS_0024_003C_003E8__locals7.callback(CS_0024_003C_003E8__locals7.ret);
				}
			});
		}

		void IAvatarSnapshotManagerDependencies.SaveSnapshots(AvatarSnapshotDocument snapshotInfo, AvatarSnapshotData snapshot, Action<bool> callback)
		{
			if (!string.IsNullOrEmpty(snapshotInfo.path) && snapshot.snapshot.texture != null && callback != null)
			{
				MonoSingleton<AssetManager>.Instance.SaveImage(snapshotInfo.path, snapshot.snapshot.texture, callback);
			}
		}

		Action IAvatarSnapshotCallDependencies.GenerateSnapshotFromDna(IAvatar dna, AvatarFlags flags, int size, Action<AvatarSnapshotResult> callback, Vector3 avatarRotation, Vector3 avatarOffset)
		{
			GenerateSnapshotFromDna_003Ec__AnonStorey22A CS_0024_003C_003E8__locals69 = new GenerateSnapshotFromDna_003Ec__AnonStorey22A();
			CS_0024_003C_003E8__locals69.size = size;
			CS_0024_003C_003E8__locals69.flags = flags;
			CS_0024_003C_003E8__locals69.avatarRotation = avatarRotation;
			CS_0024_003C_003E8__locals69.avatarOffset = avatarOffset;
			CS_0024_003C_003E8__locals69.callback = callback;
			CS_0024_003C_003E8__locals69.dna = dna;
			CS_0024_003C_003E8__locals69._003C_003Ef__this = this;
			CS_0024_003C_003E8__locals69.started = false;
			CS_0024_003C_003E8__locals69.compositeCancel = null;
			CS_0024_003C_003E8__locals69.cancelled = false;
			CS_0024_003C_003E8__locals69.doWork = null;
			CS_0024_003C_003E8__locals69.snapshotId = CreateSnapshotId(CS_0024_003C_003E8__locals69.dna, CS_0024_003C_003E8__locals69.flags, CS_0024_003C_003E8__locals69.size, CS_0024_003C_003E8__locals69.avatarRotation, CS_0024_003C_003E8__locals69.avatarOffset);
			CS_0024_003C_003E8__locals69.hasGeo = AvatarHasGeo(CS_0024_003C_003E8__locals69.dna);
			GameObject gameObject = ((!CS_0024_003C_003E8__locals69.hasGeo) ? snapshotScene : snapshotSceneGeo);
			Transform transform = ((!(gameObject != null)) ? null : gameObject.transform.Find("cube_rig"));
			CS_0024_003C_003E8__locals69.cubeRigGameObject = ((!(transform != null)) ? null : transform.gameObject);
			CS_0024_003C_003E8__locals69.finalRender = delegate(bool success)
			{
				GenerateSnapshotFromDna_003Ec__AnonStorey22A.GenerateSnapshotFromDna_003Ec__AnonStorey22B CS_0024_003C_003E8__locals62 = new GenerateSnapshotFromDna_003Ec__AnonStorey22A.GenerateSnapshotFromDna_003Ec__AnonStorey22B();
				CS_0024_003C_003E8__locals62._003C_003Ef__ref_0024554 = CS_0024_003C_003E8__locals69;
				CS_0024_003C_003E8__locals62.success = success;
				if (CS_0024_003C_003E8__locals69.cancelled)
				{
					CS_0024_003C_003E8__locals69._003C_003Ef__this.skinnerWork.Remove(CS_0024_003C_003E8__locals69.doWork);
					CS_0024_003C_003E8__locals69._003C_003Ef__this.iterateThroughSkinnerQueue();
				}
				else
				{
					CS_0024_003C_003E8__locals69._003C_003Ef__this.RenderAvatarSnapshot(CS_0024_003C_003E8__locals69.cubeRigGameObject, CS_0024_003C_003E8__locals69.size, CS_0024_003C_003E8__locals69.flags, delegate(AvatarSnapshotData snapshotData)
					{
						if (!CS_0024_003C_003E8__locals62._003C_003Ef__ref_0024554.cancelled)
						{
							CS_0024_003C_003E8__locals62._003C_003Ef__ref_0024554.callback(new AvatarSnapshotResult(CS_0024_003C_003E8__locals62.success && snapshotData != null, CS_0024_003C_003E8__locals62._003C_003Ef__ref_0024554.snapshotId, snapshotData));
						}
						CS_0024_003C_003E8__locals62._003C_003Ef__ref_0024554._003C_003Ef__this.skinnerWork.Remove(CS_0024_003C_003E8__locals62._003C_003Ef__ref_0024554.doWork);
						CS_0024_003C_003E8__locals62._003C_003Ef__ref_0024554._003C_003Ef__this.iterateThroughSkinnerQueue();
					}, CS_0024_003C_003E8__locals69.avatarRotation, CS_0024_003C_003E8__locals69.avatarOffset, CS_0024_003C_003E8__locals69.hasGeo);
				}
			};
			Action result = delegate
			{
				if (!CS_0024_003C_003E8__locals69.started)
				{
					if (CS_0024_003C_003E8__locals69.doWork != null)
					{
						CS_0024_003C_003E8__locals69._003C_003Ef__this.skinnerWork.Remove(CS_0024_003C_003E8__locals69.doWork);
					}
				}
				else if (CS_0024_003C_003E8__locals69.compositeCancel != null)
				{
					CS_0024_003C_003E8__locals69.compositeCancel();
					CS_0024_003C_003E8__locals69.cancelled = true;
					CS_0024_003C_003E8__locals69.finalRender(false);
				}
				else
				{
					CS_0024_003C_003E8__locals69.cancelled = true;
				}
			};
			CS_0024_003C_003E8__locals69.doWork = delegate
			{
				CS_0024_003C_003E8__locals69.started = true;
				Singleton<TechAnalytics>.Instance.TrackTimeToCompositeAvatarStart(CS_0024_003C_003E8__locals69._003C_003Ef__this.snapshotScene.gameObject);
				CS_0024_003C_003E8__locals69.compositeCancel = CS_0024_003C_003E8__locals69._003C_003Ef__this.SkinAvatar(CS_0024_003C_003E8__locals69.cubeRigGameObject, CS_0024_003C_003E8__locals69.dna, CS_0024_003C_003E8__locals69.flags, delegate(bool compositeResult, string cacheId)
				{
					GenerateSnapshotFromDna_003Ec__AnonStorey22A.GenerateSnapshotFromDna_003Ec__AnonStorey22C CS_0024_003C_003E8__locals72 = new GenerateSnapshotFromDna_003Ec__AnonStorey22A.GenerateSnapshotFromDna_003Ec__AnonStorey22C();
					CS_0024_003C_003E8__locals72._003C_003Ef__ref_0024554 = CS_0024_003C_003E8__locals69;
					CS_0024_003C_003E8__locals72.compositeResult = compositeResult;
					CS_0024_003C_003E8__locals69.compositeCancel = null;
					CS_0024_003C_003E8__locals69._003C_003Ef__this.HandleApiCallback(CS_0024_003C_003E8__locals69.cubeRigGameObject, CS_0024_003C_003E8__locals72.compositeResult, cacheId, delegate
					{
						CS_0024_003C_003E8__locals72._003C_003Ef__ref_0024554.finalRender(CS_0024_003C_003E8__locals72.compositeResult);
					});
				});
			};
			skinnerWork.Add(CS_0024_003C_003E8__locals69.doWork);
			if (skinnerWork.Count == 1)
			{
				iterateThroughSkinnerQueue();
			}
			return result;
		}

		public void Init(IAvatarManagerInitListener aInitListener)
		{
			snapshotHelper = new AsyncHelper<AvatarSnapshotResult>();
			skinnerWork = new List<Action>();
			snapshotCache = new AvatarSnapshotCache(this, Singleton<MixDocumentCollections>.Instance.avatarSnapshotDocumentCollectionApi);
			avatarPreloader = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefabs/Avatar/avatar_mp_preloader"));
			UnityEngine.Object.DontDestroyOnLoad(avatarPreloader);
			if ((bool)avatarPreloader)
			{
				avatarPreloader.name = "AvatarPreloader";
				avatarPreloader.transform.Translate(-1000f, -1000f, 0f);
				Animator component = avatarPreloader.transform.Find("cube_rig").gameObject.GetComponent<Animator>();
				UnityEngine.Object.Destroy(component);
			}
			snapshotScene = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefabs/Avatar/AvatarSnapshotCamera_Default"));
			UnityEngine.Object.DontDestroyOnLoad(snapshotScene);
			if ((bool)snapshotScene)
			{
				snapshotScene.transform.Translate(-1000f, -1100f, 0f);
				AvatarObjectSpawner component2 = snapshotScene.GetComponent<AvatarObjectSpawner>();
				GameObject gameObject = component2.Init();
				UnityEngine.Object.DestroyObject(gameObject.transform.parent.gameObject);
				gameObject.transform.SetParent(snapshotScene.transform);
				snapshotScene.SetActive(false);
			}
			snapshotSceneGeo = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefabs/Avatar/AvatarSnapshotCamera_Geo"));
			UnityEngine.Object.DontDestroyOnLoad(snapshotSceneGeo);
			if ((bool)snapshotSceneGeo)
			{
				snapshotSceneGeo.transform.Translate(-1000f, -1100f, 0f);
				AvatarObjectSpawner component3 = snapshotSceneGeo.GetComponent<AvatarObjectSpawner>();
				GameObject gameObject2 = component3.Init();
				UnityEngine.Object.DestroyObject(gameObject2.transform.parent.gameObject);
				gameObject2.transform.SetParent(snapshotSceneGeo.transform);
				snapshotSceneGeo.SetActive(false);
			}
			GenerateFolder("/Avatar/cache/");
			api = new AvatarApi(this);
			assetFramework = new AvatarAssetFramework(MonoSingleton<AssetManager>.Instance, Singleton<EntitlementsManager>.Instance);
			api.InitializeAvatars(assetFramework, null, aInitListener.OnShadersLoaded);
			if (UnityEngine.Application.platform == RuntimePlatform.Android)
			{
				string text = SystemInfo.deviceModel.ToLower();
				if (text.Contains("sm-n91") || text.Contains("motorola nexus 6"))
				{
					canUseAntiAliasing = false;
				}
			}
		}

		[Conditional("LOGGING_ENABLED")]
		public void LogAvatar(string msg, AvatarAlertLevel lvl)
		{
			switch (lvl)
			{
			case AvatarAlertLevel.DEBUG:
				break;
			case AvatarAlertLevel.WARNING:
				break;
			case AvatarAlertLevel.ERROR:
				break;
			case AvatarAlertLevel.TIMING:
				break;
			}
		}

		public new void OnDestroy()
		{
			base.OnDestroy();
		}

		public void saveCurrentUsersDna()
		{
			if (!MixSession.IsValidSession)
			{
				return;
			}
			MixSession.User.SetAvatar(CurrentDna, actionGenerator.CreateAction(delegate(ISetAvatarResult results)
			{
				if (!results.Success)
				{
					MixSession.AddOfflineItem(new AvatarQueueItem(api.SerializeAvatar(CurrentDna)));
				}
				else
				{
					clientAvatar = null;
				}
			}));
			PreloadAvatar(CurrentDna, (AvatarFlags)0);
		}

		public void setCurrentUsersDna(IAvatar dna)
		{
			if (dna != null)
			{
				clientAvatar = new ClientAvatar(dna);
			}
		}

		public void SetPriority(bool isCompositing)
		{
			api.SetProcessing(isCompositing);
		}

		public IAvatar GenerateRandomDna()
		{
			return api.GenerateRandomDna();
		}

		private IEnumerator LockedPreload(Action<Action> work)
		{
			while (!isPreloaderAvailable || MonoSingleton<NavigationManager>.Instance.FindCurrentRequest() != null)
			{
				yield return new WaitForSeconds(0.1f);
			}
			isPreloaderAvailable = false;
			yield return new WaitForEndOfFrame();
			work(delegate
			{
				isPreloaderAvailable = true;
			});
		}

		public void PreloadAvatar(IAvatar avatar, AvatarFlags flags)
		{
			StartCoroutine(LockedPreload(delegate(Action callback)
			{
				if (AvatarApi.ValidateAvatar(avatar) && !avatarPreloader.IsNullOrDisposed())
				{
					GameObject avatarObj = avatarPreloader.transform.Find("cube_rig").gameObject;
					api.CompositeTextures(avatar, avatarObj, flags, delegate(bool success, string cacheId)
					{
						if (success)
						{
						}
						callback();
					});
				}
			}));
		}

		public Action GetSnapshotFromDna(IAvatar dna, AvatarFlags flags, int size, SnapshotCallback snapshotCallback)
		{
			return GetSnapshotFromDna(dna, flags, size, snapshotCallback, AVATAR_ROTATION, AVATAR_OFFSET);
		}

		public Action GetSnapshotFromDna(IAvatar dna, AvatarFlags flags, int size, SnapshotCallback snapshotCallback, Vector3 avatarRotation)
		{
			return GetSnapshotFromDna(dna, flags, size, snapshotCallback, avatarRotation, AVATAR_OFFSET);
		}

		public Action GetSnapshotFromDna(IAvatar dna, AvatarFlags flags, int size, SnapshotCallback snapshotCallback, Vector3 avatarRotation, Vector3 avatarOffset)
		{
			if (!AvatarApi.ValidateAvatar(dna))
			{
				dna = null;
			}
			Action cancellableAsync = null;
			bool cancelled = false;
			Action result = delegate
			{
				cancelled = true;
				if (cancellableAsync != null)
				{
					cancellableAsync();
				}
			};
			string id = CreateSnapshotId(dna, flags, size, avatarRotation, avatarOffset);
			snapshotCache.GetCacheData(id, delegate(bool success, AvatarSnapshotData cacheSnapshot)
			{
				if (!this.IsNullOrDisposed() && !cancelled)
				{
					if (success && cacheSnapshot != null)
					{
						if (snapshotCallback != null)
						{
							snapshotCallback(success, cacheSnapshot.snapshot);
						}
					}
					else if (snapshotHelper != null)
					{
						cancellableAsync = snapshotHelper.AddAsyncCall(new AvatarSnapshotCall(dna, flags, size, avatarRotation, avatarOffset, this), delegate(AvatarSnapshotResult snapshotResults)
						{
							cancellableAsync = null;
							if (snapshotResults == null)
							{
								if (snapshotCallback != null)
								{
									snapshotCallback(false, null);
								}
							}
							else if (snapshotResults.success && snapshotCache != null)
							{
								snapshotCache.SetCacheData(snapshotResults.cacheId, snapshotResults.data, delegate(bool cacheResult)
								{
									if (snapshotCallback != null)
									{
										snapshotCallback(snapshotResults.success && cacheResult, (snapshotResults.data != null) ? snapshotResults.data.snapshot : null);
									}
								});
							}
							else if (snapshotCallback != null)
							{
								snapshotCallback(snapshotResults.success, (snapshotResults.data != null) ? snapshotResults.data.snapshot : null);
							}
						});
					}
				}
			});
			return result;
		}

		private IEnumerator SnapshotLock(Action work)
		{
			while (!isSnapshotAvailable || MonoSingleton<NavigationManager>.Instance.FindCurrentRequest() != null)
			{
				yield return new WaitForSeconds(0.1f);
			}
			isSnapshotAvailable = false;
			yield return new WaitForEndOfFrame();
			work();
			isSnapshotAvailable = true;
		}

		private void RenderAvatarSnapshot(GameObject avatarTarget, int size, AvatarFlags flags, Action<AvatarSnapshotData> result, Vector3 avatarRotation, Vector3 avatarOffset, bool hasGeo)
		{
			if (avatarTarget != null)
			{
				StartCoroutine(SnapshotLock(delegate
				{
					Transform parent = avatarTarget.transform.parent;
					if (parent != null)
					{
						GameObject gameObject = ((!hasGeo) ? snapshotScene : snapshotSceneGeo);
						gameObject.SetActive(true);
						Camera component = gameObject.GetComponent<Camera>();
						component.cullingMask = 1 << avatarTarget.layer;
						component.targetTexture = new RenderTexture(size, size, 24);
						component.targetTexture.anisoLevel = 8;
						if (hasGeo && canUseAntiAliasing)
						{
							component.targetTexture.antiAliasing = 4;
						}
						Texture2D texture2D = new Texture2D(size, size, TextureFormat.ARGB32, false);
						RenderTexture.active = component.targetTexture;
						GL.Clear(true, true, new Color(0.9f, 0f, 0f, 0f));
						component.Render();
						texture2D.ReadPixels(new Rect(0f, 0f, size, size), 0, 0, false);
						texture2D.Apply(false);
						while (!component.targetTexture.IsCreated())
						{
							component.targetTexture = new RenderTexture(size, size, 24);
							RenderTexture.active = component.targetTexture;
							component.Render();
							texture2D.ReadPixels(new Rect(0f, 0f, size, size), 0, 0, false);
							texture2D.Apply(false);
						}
						Sprite snapshot = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 100f);
						RenderTexture.active = null;
						gameObject.SetActive(false);
						AvatarSnapshotData obj = new AvatarSnapshotData
						{
							isHd = ((flags & AvatarFlags.IsHd) == AvatarFlags.IsHd),
							hasNormals = ((flags & AvatarFlags.WithNormals) == AvatarFlags.WithNormals),
							size = size,
							snapshot = snapshot
						};
						result(obj);
					}
					else
					{
						result(null);
					}
				}));
			}
			else
			{
				result(null);
			}
		}

		public Action SkinAvatar(GameObject avatarObj, IAvatar dna, AvatarFlags flags, SkinnedCallback callback)
		{
			if (!AvatarApi.ValidateAvatar(dna))
			{
				dna = null;
			}
			if (avatarObj.IsNullOrDisposed())
			{
				if (callback != null)
				{
					callback(false, string.Empty);
				}
				return delegate
				{
				};
			}
			if (api == null)
			{
				if (callback != null)
				{
					callback(false, string.Empty);
				}
				return delegate
				{
				};
			}
			if (Singleton<TechAnalytics>.Instance != null)
			{
				Singleton<TechAnalytics>.Instance.TrackTimeToCompositeAvatarStart(avatarObj);
			}
			TextureCallback apiCallback = delegate(bool success, string id)
			{
				HandleApiCallback(avatarObj, success, id, callback);
			};
			Action result = delegate
			{
				api.CancelAvatarComposite(dna, avatarObj, flags, apiCallback);
			};
			api.CompositeTextures(dna, avatarObj, flags, apiCallback);
			return result;
		}

		public void HandleApiCallback(GameObject avatarObj, bool aIsSuccess, string cacheId, SkinnedCallback callback)
		{
			Singleton<TechAnalytics>.Instance.TrackTimeToCompositeAvatarEnd(avatarObj, aIsSuccess);
			if (!aIsSuccess)
			{
				if (callback != null)
				{
					ResetAvatarHead(avatarObj, delegate
					{
						callback(aIsSuccess, cacheId);
					});
				}
				else
				{
					ResetAvatarHead(avatarObj, delegate
					{
					});
				}
			}
			else if (callback != null)
			{
				callback(aIsSuccess, cacheId);
			}
		}

		public void ResetAvatarHead(GameObject avatarObj, Action callback = null)
		{
			if (callback == null)
			{
				callback = delegate
				{
				};
			}
			api.ResetAvatarTextures(avatarObj, "Textures/Avatar/avtr_0000_basic_diff", callback);
		}

		public void GenerateFolder(string relPath)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(Application.PersistentDataPath + "/cache/" + relPath);
			if (!directoryInfo.Exists)
			{
				directoryInfo.Create();
			}
		}

		public bool DoesFileExist(string relPath)
		{
			return MonoSingleton<AssetManager>.Instance.DoesExist(relPath);
		}

		private void iterateThroughSkinnerQueue()
		{
			if (skinnerWork.Count > 0)
			{
				skinnerWork[0]();
			}
		}

		public string CreateSnapshotId(IAvatar dna, AvatarFlags flags, int size, Vector3 avatarRotation, Vector3 avatarOffset)
		{
			return CreateHash(string.Concat(flags, api.SerializeAvatar(dna), size, avatarRotation, avatarOffset));
		}

		public string CreateHash(string input)
		{
			return AssetManager.GetShaString(input);
		}

		public void TintSnapshot(Graphic image, float alpha)
		{
			image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
		}

		public void RenderAvatarSnapshotWithCancel(IAvatarHolder aAvatarHolder, GameObject aNormalTarget, string aTransformSearchString, Action aCancelSnapshot)
		{
			IAvatar avatar = aAvatarHolder.Avatar;
			string transformSearchString = ((!AvatarHasGeo(avatar)) ? aTransformSearchString : (aTransformSearchString + "_Geo"));
			Transform transformTarget = aNormalTarget.transform.Find(transformSearchString);
			int imageTargetSize = (int)transformTarget.GetComponent<RectTransform>().rect.height;
			SnapshotCallback snapshotDelegate = delegate(bool success, Sprite sprite)
			{
				aCancelSnapshot = null;
				if (success && transformTarget != null)
				{
					Image component = transformTarget.GetComponent<Image>();
					if (sprite != null && component != null)
					{
						component.sprite = sprite;
						component.enabled = true;
						transformTarget.gameObject.SetActive(true);
					}
				}
			};
			aCancelSnapshot = GetSnapshotFromDna(avatar, (AvatarFlags)0, imageTargetSize, snapshotDelegate);
			EventHandler<AbstractAvatarChangedEventArgs> eventHandler = delegate(object sender, AbstractAvatarChangedEventArgs args)
			{
				string text = ((!AvatarHasGeo(args.Avatar)) ? aTransformSearchString : (aTransformSearchString + "_Geo"));
				if (!text.Equals(transformSearchString) && !aNormalTarget.IsNullOrDisposed())
				{
					transformSearchString = text;
					transformTarget.gameObject.SetActive(false);
					transformTarget = aNormalTarget.transform.Find(text);
					imageTargetSize = (int)transformTarget.GetComponent<RectTransform>().rect.height;
				}
				if (aCancelSnapshot != null)
				{
					aCancelSnapshot();
				}
				if (transformTarget != null)
				{
					aCancelSnapshot = GetSnapshotFromDna(args.Avatar, (AvatarFlags)0, imageTargetSize, snapshotDelegate);
				}
			};
			aAvatarHolder.OnAvatarChanged += eventGenerator.AddEventHandler(aAvatarHolder, eventHandler);
		}

		public GameObject CreateAvatarObject(bool flipped = false)
		{
			string path = ((!flipped) ? "Prefabs/Avatar/avatar_mp_final" : "Prefabs/Avatar/avatar_mp_final_flipped");
			return UnityEngine.Object.Instantiate(Resources.Load<GameObject>(path));
		}

		public bool AvatarHasGeo(IAvatar avatar)
		{
			if (avatar == null)
			{
				return false;
			}
			return (avatar.Accessory.SelectionKey != "3" || avatar.Hat.SelectionKey != "257") && avatar.Costume.SelectionKey == "15";
		}
	}
}
