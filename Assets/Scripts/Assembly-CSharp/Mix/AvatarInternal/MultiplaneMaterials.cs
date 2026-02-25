using System;
using System.Collections.Generic;
using System.Text;
using Avatar;
using Avatar.DataTypes;
using Avatar.Interfaces;
using Disney.Mix.SDK;
using LitJson;
using Mix.Assets;
using UnityEngine;

namespace Mix.AvatarInternal
{
	public class MultiplaneMaterials
	{
		public static string EmptyCostumePath = string.Empty;

		public static string EmptyGeoPath = string.Empty;

		private MonoBehaviour monoEngine;

		private MultiplaneCompositor faceComp;

		private MultiplaneCompositor eyeComp;

		private MultiplaneCompositor eyebrowComp;

		private MultiplaneCompositor mouthComp;

		private MultiplaneCompositor glassesComp;

		private MultiplaneCompositor hatComp;

		private Material glassesMaterial;

		private Material hatMaterial;

		private Material faceMat;

		private Material leftEyeMat;

		private Material rightEyeMat;

		private Material leftEyebrowMat;

		private Material rightEyebrowMat;

		private Material mouthMat;

		private GameObject leftEyeObj;

		private GameObject rightEyeObj;

		private GameObject leftEyebrowObj;

		private GameObject rightEyebrowObj;

		private GameObject mouthObj;

		private GameObject faceObj;

		private GameObject baseAvatarObject;

		private readonly MultiplaneTextureTracker textureTracker;

		private global::Avatar.Interfaces.IAssetManager assetManager;

		public bool hasErrored;

		public MultiplaneMaterials(MonoBehaviour aMonoEngine, global::Avatar.Interfaces.IAssetManager aAssetManager, GameObject aAvatarObj)
		{
			if (aAvatarObj.IsNullOrDisposed())
			{
				hasErrored = true;
				return;
			}
			monoEngine = aMonoEngine;
			assetManager = aAssetManager;
			try
			{
				leftEyeObj = aAvatarObj.transform.Find("grp_offset/face_eye_left").gameObject;
				rightEyeObj = aAvatarObj.transform.Find("grp_offset/face_eye_right").gameObject;
				leftEyebrowObj = aAvatarObj.transform.Find("grp_offset/face_brow_left").gameObject;
				rightEyebrowObj = aAvatarObj.transform.Find("grp_offset/face_brow_right").gameObject;
				mouthObj = aAvatarObj.transform.Find("grp_offset/face_mouth").gameObject;
				faceObj = aAvatarObj.transform.Find("grp_offset/head").gameObject;
			}
			catch (Exception)
			{
				hasErrored = true;
				return;
			}
			baseAvatarObject = aAvatarObj;
			faceMat = RetrieveMaterial(faceObj);
			leftEyeMat = RetrieveMaterial(leftEyeObj);
			rightEyeMat = RetrieveMaterial(rightEyeObj);
			leftEyebrowMat = RetrieveMaterial(leftEyebrowObj);
			rightEyebrowMat = RetrieveMaterial(rightEyebrowObj);
			mouthMat = RetrieveMaterial(mouthObj);
			faceComp = new MultiplaneCompositor(monoEngine, 512, TextureFormat.ARGB32, "NewAvatarHair");
			eyebrowComp = new MultiplaneCompositor(monoEngine, 64, TextureFormat.ARGB32, "NewAvatarBrow");
			eyeComp = new MultiplaneCompositor(monoEngine, 256, TextureFormat.ARGB32, "NewAvatarEyes");
			mouthComp = new MultiplaneCompositor(monoEngine, 256, TextureFormat.ARGB32, "NewAvatarMouth");
			glassesComp = new MultiplaneCompositor(monoEngine, 128, TextureFormat.ARGB32, "NewAvatarGeo");
			hatComp = new MultiplaneCompositor(monoEngine, 256, TextureFormat.ARGB32, "NewAvatarGeo");
			Transform transform = aAvatarObj.transform.Find("grp_offset/tex_l_eye");
			Transform transform2 = aAvatarObj.transform.Find("grp_offset/tex_r_eye");
			Transform transform3 = aAvatarObj.transform.Find("grp_offset/tex_mouth");
			if (transform == null || transform2 == null || transform3 == null)
			{
				hasErrored = true;
				return;
			}
			AddTextureControllerToObj(leftEyeObj, transform.gameObject, 0.25f, 0.25f);
			AddTextureControllerToObj(rightEyeObj, transform2.gameObject, 0.25f, 0.25f);
			AddTextureControllerToObj(mouthObj, transform3.gameObject, 0.33f, 0.25f);
			textureTracker = aAvatarObj.GetComponent<MultiplaneTextureTracker>() ?? aAvatarObj.AddComponent<MultiplaneTextureTracker>();
		}

		public Material RetrieveMaterial(GameObject go)
		{
			Renderer component = go.GetComponent<Renderer>();
			if (component == null)
			{
				hasErrored = true;
				return null;
			}
			return component.material;
		}

		public void CleanupCompositors()
		{
			if (!baseAvatarObject.IsNullOrDisposed())
			{
				faceComp.CleanLayers();
				eyebrowComp.CleanLayers();
				eyeComp.CleanLayers();
				mouthComp.CleanLayers();
				glassesComp.CleanLayers();
				hatComp.CleanLayers();
			}
		}

		public void ResetAvatarHeadToDefault(string texturePath, Action callback)
		{
			if (baseAvatarObject.IsNullOrDisposed() || textureTracker.IsNullOrDisposed() || faceMat.IsNullOrDisposed())
			{
				callback();
				return;
			}
			Texture texture = (Texture)Resources.Load(texturePath, typeof(Texture));
			if (texture != null)
			{
				SetAvatarPlanesVisibility(false);
				textureTracker.CleanAllTextures();
				faceMat.SetTexture("_MainTex", texture);
				callback();
			}
			else
			{
				callback();
			}
		}

		private void SetAvatarPlanesVisibility(bool isShown)
		{
			if (!baseAvatarObject.IsNullOrDisposed() && !leftEyeObj.IsNullOrDisposed() && !rightEyeObj.IsNullOrDisposed() && !leftEyebrowObj.IsNullOrDisposed() && !rightEyebrowObj.IsNullOrDisposed() && !mouthObj.IsNullOrDisposed() && !textureTracker.IsNullOrDisposed())
			{
				leftEyeObj.SetActive(isShown);
				rightEyeObj.SetActive(isShown);
				leftEyebrowObj.SetActive(isShown);
				rightEyebrowObj.SetActive(isShown);
				mouthObj.SetActive(isShown);
				if (!isShown)
				{
					textureTracker.CleanupGeo();
				}
			}
		}

		private void setBlendValues(GameObject go, float yValue, bool hasXOffset = false, float xValue = 0f)
		{
			if (go.IsNullOrDisposed())
			{
				return;
			}
			SkinnedMeshRenderer component = go.GetComponent<SkinnedMeshRenderer>();
			if (!component.IsNullOrDisposed() && component.sharedMesh != null && component.sharedMesh.blendShapeCount > 0)
			{
				if (hasXOffset)
				{
					component.SetBlendShapeWeight(0, xValue + 50f);
					component.SetBlendShapeWeight(1, yValue + 50f);
				}
				else
				{
					component.SetBlendShapeWeight(0, yValue + 50f);
				}
			}
		}

		private void AddTextureControllerToObj(GameObject go, GameObject jointObject, float xOffsetRatio = 0.333f, float yOffsetRatio = 0.333f)
		{
			if (!go.IsNullOrDisposed())
			{
				AnimatedTextureController animatedTextureController = go.GetComponent<AnimatedTextureController>() ?? go.AddComponent<AnimatedTextureController>();
				animatedTextureController.jointController = jointObject;
				animatedTextureController.offsetRatioX = xOffsetRatio;
				animatedTextureController.offsetRatioY = yOffsetRatio;
			}
		}

		public void LoadAvatarTextures(IAvatar dna, AvatarFlags flags, Action<bool> callback)
		{
			if (hasErrored)
			{
				callback(false);
				return;
			}
			CompositeCostume(dna, flags, delegate(bool hasCostume)
			{
				if (hasErrored)
				{
					CleanupCompositors();
					callback(false);
				}
				else
				{
					SetAvatarPlanesVisibility(!hasCostume);
					if (!hasCostume)
					{
						DelegateQueuer delegateQueuer = new DelegateQueuer(delegate
						{
							CleanupCompositors();
							callback(!hasErrored);
						});
						CompositeSkinAndHair(dna, flags, delegateQueuer.EnqueueAction());
						CompositeBrow(dna, flags, delegateQueuer.EnqueueAction());
						CompositeEyes(dna, flags, delegateQueuer.EnqueueAction());
						CompositeMouth(dna, flags, delegateQueuer.EnqueueAction());
						CompositeAccessory(dna, flags, delegateQueuer.EnqueueAction(), dna.Accessory, "Accessory", glassesComp);
						if (dna.Hat != null)
						{
							CompositeAccessory(dna, flags, delegateQueuer.EnqueueAction(), dna.Hat, "Hat", hatComp);
						}
						delegateQueuer.FinishedEnqueuing();
					}
					else
					{
						callback(true);
					}
				}
			});
		}

		private void LoadAccessoryGeometry(IAvatarProperty geoProperty, string propertyName, Action<bool> callback)
		{
			if (geoProperty == null)
			{
				callback(false);
				return;
			}
			AvatarElement avatarData = assetManager.GetAvatarData(geoProperty.SelectionKey);
			if (avatarData == null || string.IsNullOrEmpty(avatarData.Hd))
			{
				callback(false);
				return;
			}
			string bundlePath = avatarData.Hd.Replace("_hd", "_geo_hd");
			if (!string.IsNullOrEmpty(EmptyGeoPath) && bundlePath == EmptyGeoPath)
			{
				callback(false);
				return;
			}
			assetManager.LoadABundle(delegate(UnityEngine.Object systemObject, object userdata)
			{
				GeoLoadedCallback(systemObject, bundlePath, propertyName, callback);
			}, bundlePath, null, string.Empty, false, false, true);
		}

		private void LoadTestGeoAccessory(IAvatarProperty geoProperty, MultiplaneCompositor comper, string propertyName, Action<bool> callback)
		{
			if (geoProperty == null)
			{
				callback(false);
				return;
			}
			AvatarElement avatarData = assetManager.GetAvatarData(geoProperty.SelectionKey);
			if (avatarData == null || string.IsNullOrEmpty(avatarData.Hd) || comper == null)
			{
				callback(false);
				return;
			}
			comper.AddLayerInfo("Accessory", geoProperty, avatarData);
			string bundlePath = avatarData.Hd;
			assetManager.LoadABundle(delegate(UnityEngine.Object systemObject, object userdata)
			{
				BundleLoadedCallback(systemObject, comper, bundlePath, geoProperty.SelectionKey, callback);
			}, bundlePath, null, string.Empty, false, false, true);
		}

		private void GeoLoadedCallback(UnityEngine.Object aGameObject, string bundlePath, string propertyName, Action<bool> callback)
		{
			GameObject gameObject = null;
			if (!aGameObject.IsNullOrDisposed())
			{
				GameObject gameObject2 = aGameObject as GameObject;
				AvatarGeoComponent component = gameObject2.GetComponent<AvatarGeoComponent>();
				if (!baseAvatarObject.IsNullOrDisposed())
				{
					Transform transform = baseAvatarObject.transform.Find(component.JointName);
					if (!transform.IsNullOrDisposed())
					{
						gameObject = transform.gameObject;
					}
				}
				else
				{
					hasErrored = true;
					callback(false);
				}
			}
			if (MonoSingleton<AssetManager>.Instance != null && !gameObject.IsNullOrDisposed() && !textureTracker.IsNullOrDisposed())
			{
				if (propertyName == "Hat")
				{
					bool obj = textureTracker.AttachAndTrackGeoAccessory(bundlePath, gameObject, propertyName, ref hatMaterial);
					if (hatMaterial != null)
					{
						textureTracker.DisableActiveGeoJoint(propertyName);
					}
					callback(obj);
				}
				else
				{
					bool obj2 = textureTracker.AttachAndTrackGeoAccessory(bundlePath, gameObject, propertyName, ref glassesMaterial);
					if (glassesMaterial != null)
					{
						textureTracker.DisableActiveGeoJoint(propertyName);
					}
					callback(obj2);
				}
			}
			else
			{
				hasErrored = true;
				callback(false);
			}
		}

		private void BundleLoadedCallback(UnityEngine.Object aGameObject, MultiplaneCompositor comper, string bundlePath, string key, Action<bool> callback)
		{
			if (MonoSingleton<AssetManager>.Instance != null)
			{
				GameObject gameObject = (GameObject)MonoSingleton<AssetManager>.Instance.GetBundleInstance(bundlePath);
				if (gameObject == null)
				{
					if (callback != null)
					{
						callback(false);
					}
					return;
				}
				Action bundleCleanup = AddTextureLayersFromComponent(gameObject, comper, bundlePath, key);
				comper.AddBundleCleanupCall(bundleCleanup);
				if (callback != null)
				{
					callback(true);
				}
			}
			else
			{
				callback(false);
			}
		}

		private Action AddTextureLayersFromComponent(GameObject bundleInstance, MultiplaneCompositor comper, string bundlePath, string key)
		{
			AvatarComponent component = bundleInstance.GetComponent<AvatarComponent>();
			if (!component.IsNullOrDisposed())
			{
				AvatarComponentLayer[] layers = component.Layers;
				foreach (AvatarComponentLayer avatarComponentLayer in layers)
				{
					if (avatarComponentLayer != null)
					{
						AvatarTextureTracker.AddTextureReference(avatarComponentLayer.Texture);
						AvatarTextureTracker.AddTextureReference(avatarComponentLayer.NormalMap);
					}
				}
				comper.AddComponent(key, component);
				return delegate
				{
					if (!component.IsNullOrDisposed())
					{
						if (component != null && component.Layers != null)
						{
							for (int j = 0; j < component.Layers.Length; j++)
							{
								if (component.Layers[j].Texture != null)
								{
									AvatarTextureTracker.DecrementTextureReference(component.Layers[j].Texture);
								}
								if (component.Layers[j].NormalMap != null)
								{
									AvatarTextureTracker.DecrementTextureReference(component.Layers[j].NormalMap);
								}
							}
						}
						if (!string.IsNullOrEmpty(bundlePath) && bundleInstance != null)
						{
							component = null;
							MonoSingleton<AssetManager>.Instance.DestroyBundleInstance(bundlePath, bundleInstance);
							bundleInstance = null;
						}
					}
				};
			}
			return delegate
			{
			};
		}

		private void AddBundleLoad(string name, IAvatarProperty property, MultiplaneCompositor comp, AvatarFlags flags, Action<bool> callback)
		{
			AvatarElement avatarData = assetManager.GetAvatarData(property.SelectionKey);
			if (avatarData == null)
			{
				callback(false);
				return;
			}
			comp.AddLayerInfo(name, property, avatarData);
			string bundlePath = avatarData.Hd;
			assetManager.LoadABundle(delegate(UnityEngine.Object systemObject, object userdata)
			{
				BundleLoadedCallback(systemObject, comp, bundlePath, property.SelectionKey, callback);
			}, bundlePath, null, string.Empty, false, false, true);
		}

		private Action<Texture2D> SetTextures(string prop, Material mat, string path, Action callback)
		{
			return delegate(Texture2D mainTex)
			{
				if (!string.IsNullOrEmpty(path))
				{
					assetManager.SaveCachedImage(mainTex, path, delegate(bool imageSaved)
					{
						hasErrored = !imageSaved;
						if (!hasErrored && !textureTracker.IsNullOrDisposed())
						{
							textureTracker.SetAndTrackTexture(mat, prop, path, mainTex);
						}
						callback();
					});
				}
				else
				{
					if (!textureTracker.IsNullOrDisposed())
					{
						textureTracker.SetAndTrackTexture(mat, prop, path, mainTex);
					}
					callback();
				}
			};
		}

		private string GenerateFilenameFromProps(KeyValuePair<IAvatarProperty, string>[] propInfos, bool ignoreOffsets = false)
		{
			StringBuilder stringBuilder = new StringBuilder();
			JsonWriter jsonWriter = new JsonWriter(stringBuilder);
			jsonWriter.WriteArrayStart();
			for (int i = 0; i < propInfos.Length; i++)
			{
				KeyValuePair<IAvatarProperty, string> keyValuePair = propInfos[i];
				SerializeProperty(jsonWriter, keyValuePair.Key, keyValuePair.Value, ignoreOffsets);
			}
			jsonWriter.WriteArrayEnd();
			return assetManager.GetShaString(stringBuilder.ToString());
		}

		private void SerializeProperty(JsonWriter writer, IAvatarProperty prop, string propName, bool ignoreOffsets)
		{
			if (prop == null)
			{
				prop = new ClientAvatarProperty(string.Empty, 0, 0.0, 0.0);
			}
			writer.WriteObjectStart();
			writer.WritePropertyName(propName);
			writer.WriteObjectStart();
			writer.WritePropertyName("SelectionKey");
			writer.Write(prop.SelectionKey);
			writer.WritePropertyName("TintIndex");
			writer.Write(prop.TintIndex);
			if (!ignoreOffsets)
			{
				writer.WritePropertyName("XOffset");
				writer.Write(prop.XOffset);
				writer.WritePropertyName("YOffset");
				writer.Write(prop.YOffset);
			}
			if (assetManager != null)
			{
				writer.WritePropertyName("VersionInfo");
				writer.Write(assetManager.GetAvatarElementVersionInfo(assetManager.GetAvatarData(prop.SelectionKey)));
			}
			writer.WriteObjectEnd();
			writer.WriteObjectEnd();
		}

		private void CompositeCostume(IAvatar dna, AvatarFlags flags, Action<bool> callback)
		{
			if ((flags & AvatarFlags.WithoutCostume) == AvatarFlags.WithoutCostume)
			{
				callback(false);
				return;
			}
			KeyValuePair<IAvatarProperty, string>[] propInfos = new KeyValuePair<IAvatarProperty, string>[1]
			{
				new KeyValuePair<IAvatarProperty, string>(dna.Costume, "Costume")
			};
			string path = GenerateFilenameFromProps(propInfos) + ".png";
			Texture2D texture = MultiplaneMemCache.GetTexture(path);
			if (texture != null)
			{
				SetTextures("_MainTex", faceMat, string.Empty, delegate
				{
					callback(true);
				})(texture);
				return;
			}
			if (!string.IsNullOrEmpty(EmptyCostumePath) && EmptyCostumePath == path)
			{
				callback(false);
				return;
			}
			assetManager.LoadCachedImage(path, delegate(bool success, Texture2D texture2D)
			{
				if (!success)
				{
					AddBundleLoad("Costume", dna.Costume, faceComp, flags, delegate(bool bundleSuccess)
					{
						if (!bundleSuccess)
						{
							hasErrored = true;
							callback(true);
						}
						else
						{
							bool hasCostume = !faceComp.IsComponentIgnored(dna.Costume.SelectionKey);
							if (hasCostume)
							{
								faceComp.Composite(flags, delegate(Texture2D newTexture)
								{
									MultiplaneMemCache.AddTexture(path, newTexture, delegate
									{
										UnityEngine.Object.Destroy(newTexture);
									});
									SetTextures("_MainTex", faceMat, path, delegate
									{
										callback(hasCostume);
									})(newTexture);
								});
							}
							else
							{
								EmptyCostumePath = path;
								callback(hasCostume);
							}
						}
					});
				}
				else
				{
					MultiplaneMemCache.AddTexture(path, texture2D, delegate
					{
						UnityEngine.Object.Destroy(texture2D);
					});
					SetTextures("_MainTex", faceMat, string.Empty, delegate
					{
						callback(true);
					})(texture2D);
				}
			});
		}

		private void CompositeSkinAndHair(IAvatar dna, AvatarFlags flags, Action callback)
		{
			if (hasErrored)
			{
				callback();
				return;
			}
			KeyValuePair<IAvatarProperty, string>[] propInfos = new KeyValuePair<IAvatarProperty, string>[3]
			{
				new KeyValuePair<IAvatarProperty, string>(dna.Skin, "Skin"),
				new KeyValuePair<IAvatarProperty, string>(dna.Nose, "Nose"),
				new KeyValuePair<IAvatarProperty, string>(dna.Hair, "Hair")
			};
			string path = GenerateFilenameFromProps(propInfos) + ".png";
			Texture2D texture = MultiplaneMemCache.GetTexture(path);
			if (texture != null)
			{
				SetTextures("_MainTex", faceMat, string.Empty, callback)(texture);
				return;
			}
			assetManager.LoadCachedImage(path, delegate(bool success, Texture2D texture2D)
			{
				if (!success)
				{
					CompositeSkinAndHairFromScratch(dna, flags, path, callback);
				}
				else
				{
					MultiplaneMemCache.AddTexture(path, texture2D, delegate
					{
						UnityEngine.Object.Destroy(texture2D);
					});
					SetTextures("_MainTex", faceMat, string.Empty, callback)(texture2D);
				}
			});
		}

		private void CompositeHairAndNormals(IAvatar dna, AvatarFlags flags, Action callback)
		{
			if (hasErrored)
			{
				callback();
			}
		}

		private Action<bool> HairEnquerHelper(DelegateQueuer queuer)
		{
			Action callback = queuer.EnqueueAction();
			return delegate(bool success)
			{
				hasErrored = !success;
				callback();
			};
		}

		private void CompositeSkinAndHairFromScratch(IAvatar dna, AvatarFlags flags, string basePath, Action callback)
		{
			DelegateQueuer delegateQueuer = new DelegateQueuer(delegate
			{
				if (hasErrored)
				{
					callback();
				}
				else
				{
					faceComp.Composite(flags, delegate(Texture2D mainTex)
					{
						if (!textureTracker.IsNullOrDisposed())
						{
							MultiplaneMemCache.AddTexture(basePath, mainTex, delegate
							{
								UnityEngine.Object.Destroy(mainTex);
							});
							textureTracker.SetAndTrackTexture(faceMat, "_MainTex", basePath, mainTex);
							assetManager.SaveCachedImage(mainTex, basePath, delegate
							{
								callback();
							});
						}
						else
						{
							hasErrored = true;
							callback();
						}
					});
				}
			});
			AddBundleLoad("Skin", dna.Skin, faceComp, flags, HairEnquerHelper(delegateQueuer));
			AddBundleLoad("Nose", dna.Nose, faceComp, flags, HairEnquerHelper(delegateQueuer));
			AddBundleLoad("Hair", dna.Hair, faceComp, flags, HairEnquerHelper(delegateQueuer));
			delegateQueuer.FinishedEnqueuing();
		}

		private void CompositeBrow(IAvatar dna, AvatarFlags flags, Action callback)
		{
			if (hasErrored || textureTracker.IsNullOrDisposed())
			{
				callback();
				return;
			}
			ClientAvatarProperty browDupe = new ClientAvatarProperty(dna.Brow.SelectionKey, dna.Hair.TintIndex, dna.Brow.XOffset, dna.Brow.YOffset);
			KeyValuePair<IAvatarProperty, string>[] propInfos = new KeyValuePair<IAvatarProperty, string>[1]
			{
				new KeyValuePair<IAvatarProperty, string>(browDupe, "Brow")
			};
			string path = GenerateFilenameFromProps(propInfos, true) + ".png";
			setBlendValues(leftEyebrowObj, (float)browDupe.YOffset);
			setBlendValues(rightEyebrowObj, (float)browDupe.YOffset);
			Texture2D texture = MultiplaneMemCache.GetTexture(path);
			if (texture != null)
			{
				MultiplaneMemCache.GetTexture(path);
				textureTracker.SetAndTrackTexture(leftEyebrowMat, "_MainTex", path, texture);
				textureTracker.SetAndTrackTexture(rightEyebrowMat, "_MainTex", path, texture);
				callback();
				return;
			}
			assetManager.LoadCachedImage(path, delegate(bool success, Texture2D texture2D)
			{
				if (!success)
				{
					AddBundleLoad("Brow", browDupe, eyebrowComp, flags, delegate(bool bundleSuccess)
					{
						if (!bundleSuccess)
						{
							hasErrored = true;
							callback();
						}
						else
						{
							eyebrowComp.Composite(flags, delegate(Texture2D mainTex)
							{
								if (!textureTracker.IsNullOrDisposed())
								{
									MultiplaneMemCache.AddTexture(path, mainTex, delegate
									{
										UnityEngine.Object.Destroy(mainTex);
									});
									MultiplaneMemCache.GetTexture(path);
									textureTracker.SetAndTrackTexture(leftEyebrowMat, "_MainTex", path, mainTex);
									textureTracker.SetAndTrackTexture(rightEyebrowMat, "_MainTex", path, mainTex);
									assetManager.SaveCachedImage(mainTex, path, delegate
									{
										callback();
									});
								}
								else
								{
									hasErrored = true;
									callback();
								}
							});
						}
					});
				}
				else
				{
					if (!textureTracker.IsNullOrDisposed())
					{
						MultiplaneMemCache.AddTexture(path, texture2D, delegate
						{
							UnityEngine.Object.Destroy(texture2D);
						});
						MultiplaneMemCache.GetTexture(path);
						textureTracker.SetAndTrackTexture(leftEyebrowMat, "_MainTex", path, texture2D);
						textureTracker.SetAndTrackTexture(rightEyebrowMat, "_MainTex", path, texture2D);
					}
					callback();
				}
			});
		}

		private void CompositeAccessory(IAvatar dna, AvatarFlags flags, Action callback, IAvatarProperty accessoryProperty, string propertyName, MultiplaneCompositor compositor)
		{
			if (dna == null || textureTracker.IsNullOrDisposed())
			{
				hasErrored = true;
				callback();
				return;
			}
			if ((flags & AvatarFlags.WithoutGeo) == AvatarFlags.WithoutGeo)
			{
				textureTracker.DisableActiveGeoJoint(propertyName);
				callback();
				return;
			}
			offsetAccessory((float)accessoryProperty.YOffset, propertyName);
			KeyValuePair<IAvatarProperty, string>[] propInfos = new KeyValuePair<IAvatarProperty, string>[1]
			{
				new KeyValuePair<IAvatarProperty, string>(accessoryProperty, propertyName)
			};
			string id = GenerateFilenameFromProps(propInfos, true);
			if (textureTracker.currentGeoIds.ContainsKey(propertyName) && textureTracker.currentGeoIds[propertyName] == id)
			{
				callback();
				return;
			}
			string path = id + ".png";
			LoadAccessoryGeometry(accessoryProperty, propertyName, delegate(bool geoSuccess)
			{
				if (geoSuccess)
				{
					assetManager.LoadCachedImage(path, delegate(bool cacheSuccess, Texture2D texture)
					{
						if (!cacheSuccess)
						{
							LoadTestGeoAccessory(accessoryProperty, compositor, propertyName, delegate(bool textureSuccess)
							{
								if (textureSuccess && !getGeoMaterial(propertyName).IsNullOrDisposed())
								{
									compositor.Composite(flags, delegate(Texture2D mainTex)
									{
										if (!textureTracker.IsNullOrDisposed())
										{
											textureTracker.SetAndTrackTexture(getGeoMaterial(propertyName), "_MainTex", path, mainTex);
											textureTracker.EnableActiveGeoJoint(propertyName);
											offsetAccessory((float)accessoryProperty.YOffset, propertyName);
											if (textureTracker.currentGeoIds.ContainsKey(propertyName))
											{
												textureTracker.currentGeoIds[propertyName] = id;
											}
											else
											{
												textureTracker.currentGeoIds.Add(propertyName, id);
											}
											assetManager.SaveCachedImage(mainTex, path, delegate
											{
												callback();
											});
										}
									});
								}
								else
								{
									textureTracker.DisableActiveGeoJoint(propertyName);
									textureTracker.currentGeoIds[propertyName] = null;
									callback();
								}
							});
						}
						else
						{
							if (!textureTracker.IsNullOrDisposed())
							{
								MultiplaneMemCache.AddTexture(path, texture, delegate
								{
									UnityEngine.Object.Destroy(texture);
								});
								textureTracker.SetAndTrackTexture(getGeoMaterial(propertyName), "_MainTex", path, texture);
								textureTracker.EnableActiveGeoJoint(propertyName);
								offsetAccessory((float)accessoryProperty.YOffset, propertyName);
								if (textureTracker.currentGeoIds.ContainsKey(propertyName))
								{
									textureTracker.currentGeoIds[propertyName] = id;
								}
								else
								{
									textureTracker.currentGeoIds.Add(propertyName, id);
								}
							}
							callback();
						}
					});
				}
				else
				{
					textureTracker.DisableActiveGeoJoint(propertyName);
					textureTracker.currentGeoIds[propertyName] = null;
					callback();
				}
			});
		}

		private Material getGeoMaterial(string propertyName)
		{
			if (propertyName == "Hat")
			{
				return hatMaterial;
			}
			return glassesMaterial;
		}

		private void CompositeEyes(IAvatar dna, AvatarFlags flags, Action callback)
		{
			if (hasErrored || textureTracker.IsNullOrDisposed())
			{
				callback();
				return;
			}
			KeyValuePair<IAvatarProperty, string>[] propInfos = new KeyValuePair<IAvatarProperty, string>[1]
			{
				new KeyValuePair<IAvatarProperty, string>(dna.Eyes, "Eyes")
			};
			string path = GenerateFilenameFromProps(propInfos, true) + ".png";
			setBlendValues(leftEyeObj, (float)dna.Eyes.YOffset);
			setBlendValues(rightEyeObj, (float)dna.Eyes.YOffset);
			Texture2D texture = MultiplaneMemCache.GetTexture(path);
			if (texture != null)
			{
				MultiplaneMemCache.GetTexture(path);
				textureTracker.SetAndTrackTexture(leftEyeMat, "_MainTex", path, texture);
				textureTracker.SetAndTrackTexture(rightEyeMat, "_MainTex", path, texture);
				callback();
				return;
			}
			assetManager.LoadCachedImage(path, delegate(bool success, Texture2D texture2D)
			{
				if (!success)
				{
					AddBundleLoad("Eyes", dna.Eyes, eyeComp, flags, delegate(bool bundleSuccess)
					{
						if (!bundleSuccess)
						{
							hasErrored = true;
							callback();
						}
						else
						{
							eyeComp.Composite(flags, delegate(Texture2D mainTex)
							{
								if (!textureTracker.IsNullOrDisposed())
								{
									MultiplaneMemCache.AddTexture(path, mainTex, delegate
									{
										UnityEngine.Object.Destroy(mainTex);
									});
									MultiplaneMemCache.GetTexture(path);
									textureTracker.SetAndTrackTexture(leftEyeMat, "_MainTex", path, mainTex);
									textureTracker.SetAndTrackTexture(rightEyeMat, "_MainTex", path, mainTex);
									assetManager.SaveCachedImage(mainTex, path, delegate
									{
										callback();
									});
								}
								else
								{
									hasErrored = true;
									callback();
								}
							});
						}
					});
				}
				else
				{
					if (!textureTracker.IsNullOrDisposed())
					{
						MultiplaneMemCache.AddTexture(path, texture2D, delegate
						{
							UnityEngine.Object.Destroy(texture2D);
						});
						MultiplaneMemCache.GetTexture(path);
						textureTracker.SetAndTrackTexture(leftEyeMat, "_MainTex", path, texture2D);
						textureTracker.SetAndTrackTexture(rightEyeMat, "_MainTex", path, texture2D);
					}
					callback();
				}
			});
		}

		private void CompositeMouth(IAvatar dna, AvatarFlags flags, Action callback)
		{
			if (hasErrored)
			{
				callback();
				return;
			}
			ClientAvatarProperty mouthPropDupe = new ClientAvatarProperty(dna.Mouth.SelectionKey, dna.Skin.TintIndex, dna.Mouth.XOffset, dna.Mouth.YOffset);
			KeyValuePair<IAvatarProperty, string>[] propInfos = new KeyValuePair<IAvatarProperty, string>[1]
			{
				new KeyValuePair<IAvatarProperty, string>(mouthPropDupe, "Mouth")
			};
			string path = GenerateFilenameFromProps(propInfos, true) + ".png";
			setBlendValues(mouthObj, (float)dna.Mouth.YOffset, true, (float)dna.Mouth.XOffset);
			Texture2D texture = MultiplaneMemCache.GetTexture(path);
			if (texture != null)
			{
				SetTextures("_MainTex", mouthMat, string.Empty, callback)(texture);
				return;
			}
			assetManager.LoadCachedImage(path, delegate(bool success, Texture2D texture2D)
			{
				if (!success)
				{
					AddBundleLoad("Mouth", mouthPropDupe, mouthComp, flags, delegate(bool bundleSuccess)
					{
						if (!bundleSuccess)
						{
							hasErrored = true;
							callback();
						}
						else
						{
							mouthComp.Composite(flags, delegate(Texture2D newTexture)
							{
								MultiplaneMemCache.AddTexture(path, newTexture, delegate
								{
									UnityEngine.Object.Destroy(newTexture);
								});
								SetTextures("_MainTex", mouthMat, path, callback)(newTexture);
							});
						}
					});
				}
				else
				{
					MultiplaneMemCache.AddTexture(path, texture2D, delegate
					{
						UnityEngine.Object.Destroy(texture2D);
					});
					SetTextures("_MainTex", mouthMat, string.Empty, callback)(texture2D);
				}
			});
		}

		private void offsetAccessory(float aYOffset, string aPropertyName)
		{
			GameObject activeGeoJoint = textureTracker.GetActiveGeoJoint(aPropertyName);
			if (!activeGeoJoint.IsNullOrDisposed())
			{
				Transform child = activeGeoJoint.transform.GetChild(0);
				if (!child.IsNullOrDisposed())
				{
					float num = 22.81f;
					float num2 = (aYOffset + 50f) / 100f;
					float y = num * num2 + -9.7f;
					child.transform.localPosition = new Vector3(0f, y, 0f);
				}
			}
		}
	}
}
