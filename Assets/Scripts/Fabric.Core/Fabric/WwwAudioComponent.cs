using System.Collections;
using UnityEngine;

namespace Fabric
{
	[AddComponentMenu("Fabric/Components/WwwAudioComponent")]
	public class WwwAudioComponent : AudioComponent
	{
		[SerializeField]
		[HideInInspector]
		public wwwFileLocation _fileLocation;

		[HideInInspector]
		[SerializeField]
		public AudioType _audioType = AudioType.WAV;

		[HideInInspector]
		public bool _is3D;

		[HideInInspector]
		[SerializeField]
		public bool _isStreaming = true;

		[SerializeField]
		[HideInInspector]
		public bool _languageSupported;

		[HideInInspector]
		[SerializeField]
		public bool _loadOnStart;

		[HideInInspector]
		[SerializeField]
		public bool _ignoreUnloadUnusedAssets;

		[SerializeField]
		[HideInInspector]
		private string _audioClipReference;

		private WWW www;

		public string AudioClipReference
		{
			get
			{
				return _audioClipReference;
			}
			set
			{
				_audioClipReference = value;
			}
		}

		protected override void OnInitialise(bool inPreviewMode = false)
		{
			AudioSource component = base.gameObject.GetComponent<AudioSource>();
			if ((bool)component)
			{
				Debug.LogWarning("Fabric: Adding an AudioSource and AudioComponent [" + base.name + "] in the same gameObject will impact performance, move AudioSource in a new gameObject underneath");
			}
			base.OnInitialise(inPreviewMode);
			if (_loadOnStart)
			{
				Load();
			}
		}

		private void Load()
		{
			string text = GetAudioClipReferenceFilename();
			if (_languageSupported)
			{
				int languageIndex = FabricManager.Instance.GetLanguageIndex();
				if (languageIndex >= 0)
				{
					LanguageProperties languagePropertiesByIndex = FabricManager.Instance.GetLanguagePropertiesByIndex(languageIndex);
					if (languagePropertiesByIndex != null)
					{
						text = text.Replace("LANGUAGE", languagePropertiesByIndex._languageFolder);
					}
				}
			}
			www = new WWW(text);
			base.AudioClip = www.GetAudioClip(_is3D, _isStreaming, _audioType);
		}

		private IEnumerator LoadPlayCoroutine(ComponentInstance zComponentInstance, float target, float curve, bool dontPlayComponents)
		{
			string filename = GetAudioClipReferenceFilename();
			if (_languageSupported)
			{
				int languageIndex = FabricManager.Instance.GetLanguageIndex();
				if (languageIndex >= 0)
				{
					LanguageProperties languagePropertiesByIndex = FabricManager.Instance.GetLanguagePropertiesByIndex(languageIndex);
					if (languagePropertiesByIndex != null)
					{
						filename = filename.Replace("LANGUAGE", languagePropertiesByIndex._languageFolder);
					}
				}
			}
			www = new WWW(filename);
			while (!www.isDone)
			{
				yield return new WaitForSeconds(0.1f);
			}
			base.AudioClip = www.GetAudioClip(_is3D, _isStreaming, _audioType);
			PlayInternalWait(zComponentInstance, target, curve, dontPlayComponents);
		}

		protected bool UnLoad()
		{
			base.AudioClip = null;
			if (base.AudioSource != null)
			{
				base.AudioSource.clip = null;
			}
			if (www != null)
			{
				www.Dispose();
			}
			if (!_ignoreUnloadUnusedAssets)
			{
				Resources.UnloadUnusedAssets();
			}
			return true;
		}

		private string GetAudioClipReferenceFilename()
		{
			string text = "";
			if (_fileLocation == wwwFileLocation.DataPath)
			{
				text = GetDataPath();
			}
			else if (_fileLocation == wwwFileLocation.PersistentDataPath)
			{
				text = GetPersistentPath();
			}
			else if (_fileLocation == wwwFileLocation.StreamingAssetsPath)
			{
				text = GetStreamingPath();
			}
			else if (_fileLocation == wwwFileLocation.Http)
			{
				return _audioClipReference;
			}
			return text + "//" + _audioClipReference;
		}

		private string GetPersistentPath()
		{
			return "file://" + Application.persistentDataPath;
		}

		private string GetStreamingPath()
		{
			return "file://" + Application.streamingAssetsPath;
		}

		private string GetDataPath()
		{
			return "file://" + Application.dataPath;
		}

		internal override void PlayInternal(ComponentInstance zComponentInstance, float target, float curve, bool dontPlayComponents)
		{
			if (!_loadOnStart)
			{
				SetComponentActive(true);
				_currentState = AudioComponentState.WaitingToPlay;
				StartCoroutine(LoadPlayCoroutine(zComponentInstance, target, curve, dontPlayComponents));
			}
			else
			{
				PlayInternalWait(zComponentInstance, target, curve, dontPlayComponents);
			}
		}

		protected override void StopAudioComponent()
		{
			base.StopAudioComponent();
			if (!_loadOnStart)
			{
				UnLoad();
			}
		}

		internal override void StopInternal(bool stopInstances, bool forceStop, float target, float curve, double scheduledEnd)
		{
			base.StopInternal(stopInstances, forceStop, target, curve, scheduledEnd);
			if (forceStop && !_loadOnStart)
			{
				UnLoad();
			}
		}

		public override void SetAudioClip(AudioClip audioClip, GameObject parentGameObject)
		{
			if (!_loadOnStart)
			{
				UnLoad();
			}
			base.SetAudioClip(audioClip, parentGameObject);
		}
	}
}
