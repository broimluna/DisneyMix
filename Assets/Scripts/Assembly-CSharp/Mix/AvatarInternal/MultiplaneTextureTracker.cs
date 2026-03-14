using System;
using System.Collections.Generic;
using Mix.Assets;
using UnityEngine;

namespace Mix.AvatarInternal
{
	public class MultiplaneTextureTracker : MonoBehaviour
	{
		public Dictionary<Material, MultiplaneMaterialTextureInfo> matsTextureInfos = new Dictionary<Material, MultiplaneMaterialTextureInfo>();

		public Action glassesToCleanup;

		public Action hatToCleanup;

		public int x;

		public Dictionary<string, string> currentGeoIds = new Dictionary<string, string>();

		private Dictionary<string, GameObject> geoJoints = new Dictionary<string, GameObject>();

		private void Start()
		{
			base.gameObject.SetActive(true);
		}

		private void Update()
		{
		}

		private void OnDestroy()
		{
			foreach (KeyValuePair<Material, MultiplaneMaterialTextureInfo> matsTextureInfo in matsTextureInfos)
			{
				matsTextureInfo.Value.Cleanup();
			}
			matsTextureInfos.Clear();
			if (glassesToCleanup != null)
			{
				glassesToCleanup();
				glassesToCleanup = null;
			}
			if (hatToCleanup != null)
			{
				hatToCleanup();
				hatToCleanup = null;
			}
		}

		public void CleanAllTextures()
		{
			OnDestroy();
		}

		public void CleanCurrentTexture(Material mat, string prop)
		{
			if (matsTextureInfos.ContainsKey(mat))
			{
				matsTextureInfos[mat].CleanupProp(prop);
			}
		}

		public void SetAndTrackTexture(Material mat, string prop, string id, Texture2D texture)
		{
			MultiplaneMaterialTextureInfo multiplaneMaterialTextureInfo;
			if (matsTextureInfos.ContainsKey(mat))
			{
				multiplaneMaterialTextureInfo = matsTextureInfos[mat];
			}
			else
			{
				multiplaneMaterialTextureInfo = new MultiplaneMaterialTextureInfo(mat);
				matsTextureInfos.Add(mat, multiplaneMaterialTextureInfo);
			}
			multiplaneMaterialTextureInfo.AddProp(prop, texture, id);
			mat.SetTexture(prop, texture);
		}

		public void CleanupGeo()
		{
			if (glassesToCleanup != null)
			{
				glassesToCleanup();
				glassesToCleanup = null;
			}
			if (hatToCleanup != null)
			{
				hatToCleanup();
				hatToCleanup = null;
			}
		}

		public bool AttachAndTrackGeoAccessory(string bundlePath, GameObject parent, string propertyName, ref Material geoMaterial)
		{
			if (propertyName == "Accessory")
			{
				if (glassesToCleanup != null)
				{
					glassesToCleanup();
					glassesToCleanup = null;
				}
			}
			else if (propertyName == "Hat" && hatToCleanup != null)
			{
				hatToCleanup();
				hatToCleanup = null;
			}
			GameObject bundleInstance = (GameObject)MonoSingleton<AssetManager>.Instance.GetBundleInstance(bundlePath);
			if (bundleInstance == null)
			{
				geoMaterial = null;
				currentGeoIds[propertyName] = null;
				return false;
			}
			AvatarGeoComponent geoComponent = bundleInstance.GetComponent<AvatarGeoComponent>();
			if (geoComponent != null && geoComponent.MeshObject != null)
			{
				if (!geoJoints.ContainsKey(propertyName))
				{
					geoJoints.Add(propertyName, parent);
				}
				else
				{
					geoJoints[propertyName] = parent;
				}
				geoComponent.MeshObject.layer = parent.layer;
				geoComponent.MeshObject.transform.localScale = new Vector3(1f, 1f, 1f);
				geoComponent.MeshObject.transform.SetParent(parent.transform, false);
				UnityEngine.Object.Destroy(bundleInstance);
				if (string.Compare(geoComponent.JointName, "grp_offset/def_front") == 0)
				{
					geoComponent.MeshObject.transform.Rotate(0f, 180f, 0f);
				}
				MeshRenderer component = geoComponent.MeshObject.GetComponent<MeshRenderer>();
				geoMaterial = component.material;
				Shader shader = Shader.Find("Custom/gradientlightalphaZWrite");
				component.material.shader = shader;
				if (propertyName == "Accessory")
				{
					glassesToCleanup = delegate
					{
						UnityEngine.Object.Destroy(geoComponent.MeshObject);
						if (MonoSingleton<AssetManager>.Instance != null)
						{
							MonoSingleton<AssetManager>.Instance.DestroyBundleInstance(bundlePath, bundleInstance);
						}
						geoJoints[propertyName] = null;
						currentGeoIds[propertyName] = null;
					};
				}
				else if (propertyName == "Hat")
				{
					hatToCleanup = delegate
					{
						UnityEngine.Object.Destroy(geoComponent.MeshObject);
						if (MonoSingleton<AssetManager>.Instance != null)
						{
							MonoSingleton<AssetManager>.Instance.DestroyBundleInstance(bundlePath, bundleInstance);
						}
						geoJoints[propertyName] = null;
						currentGeoIds[propertyName] = null;
					};
				}
			}
			else
			{
				MultiplaneMaterials.EmptyGeoPath = bundlePath;
				currentGeoIds[propertyName] = null;
				MonoSingleton<AssetManager>.Instance.DestroyBundleInstance(bundlePath, bundleInstance);
				geoMaterial = null;
			}
			return true;
		}

		public void DisableActiveGeoJoint(string propertyName)
		{
			if (geoJoints.ContainsKey(propertyName) && !geoJoints[propertyName].IsNullOrDisposed())
			{
				geoJoints[propertyName].SetActive(false);
			}
		}

		public void EnableActiveGeoJoint(string propertyName)
		{
			if (geoJoints.ContainsKey(propertyName) && !geoJoints[propertyName].IsNullOrDisposed())
			{
				geoJoints[propertyName].SetActive(true);
			}
		}

		public GameObject GetActiveGeoJoint(string propertyName)
		{
			if (geoJoints.ContainsKey(propertyName) && !geoJoints[propertyName].IsNullOrDisposed())
			{
				return geoJoints[propertyName];
			}
			return null;
		}
	}
}
