using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Fabric.ModularSynth
{
	public class DSPUnit
	{
		private Dictionary<int, Module> modules = new Dictionary<int, Module>();

		private Dictionary<string, ControlOutputPin> globalParameters = new Dictionary<string, ControlOutputPin>();

		private bool isPlaying;

		private Input inputModule;

		private Output outputModule;

		private List<Module> moduleList = new List<Module>();

		[CompilerGenerated]
		private int _003CBlockSize_003Ek__BackingField;

		[CompilerGenerated]
		private float _003CSampleRate_003Ek__BackingField;

		[CompilerGenerated]
		private int _003CNumChannels_003Ek__BackingField;

		public int BlockSize
		{
			[CompilerGenerated]
			set
			{
				_003CBlockSize_003Ek__BackingField = value;
			}
		}

		public float SampleRate
		{
			[CompilerGenerated]
			set
			{
				_003CSampleRate_003Ek__BackingField = value;
			}
		}

		public int NumChannels
		{
			[CompilerGenerated]
			set
			{
				_003CNumChannels_003Ek__BackingField = value;
			}
		}

		public DSPUnit()
		{
			BlockSize = 4096;
			SampleRate = 44100f;
			NumChannels = 2;
		}

		public void Create()
		{
			foreach (KeyValuePair<int, Module> module in modules)
			{
				module.Value.OnCreate();
			}
			CollectGlobalProperties();
		}

		public void Play()
		{
			foreach (KeyValuePair<int, Module> module in modules)
			{
				module.Value.OnPlay();
			}
			isPlaying = true;
		}

		public void Stop()
		{
			foreach (KeyValuePair<int, Module> module in modules)
			{
				module.Value.OnStop();
			}
			isPlaying = false;
		}

		public ControlOutputPin[] CollectGlobalProperties()
		{
			globalParameters.Clear();
			List<ControlOutputPin> list = new List<ControlOutputPin>();
			foreach (KeyValuePair<int, Module> module in modules)
			{
				ControlOutputPin[] propertiesArray = module.Value.GetPropertiesArray();
				for (int i = 0; i < propertiesArray.Length; i++)
				{
					ControlOutputPin controlOutputPin = propertiesArray[i];
					if (controlOutputPin != null && !globalParameters.ContainsKey(propertiesArray[i].Name))
					{
						globalParameters.Add(propertiesArray[i].Name, propertiesArray[i]);
						list.Add(propertiesArray[i]);
					}
				}
			}
			return list.ToArray();
		}

		public void SetNumChannels(int NumChannels)
		{
			foreach (KeyValuePair<int, Module> module in modules)
			{
				module.Value.SetNumChannels(NumChannels);
			}
		}

		public void SetBlockSize(int BlockSize)
		{
			foreach (KeyValuePair<int, Module> module in modules)
			{
				module.Value.SetBlockSize(BlockSize);
			}
		}

		public void Update()
		{
			foreach (KeyValuePair<int, Module> module in modules)
			{
				module.Value.OnUpdate();
			}
		}

		public void Process(float[] inputs, int numOfInputChannels, float[] outputs, int numOfOutputChannels, int numOfSamples)
		{
			if (!isPlaying)
			{
				return;
			}
			if (inputModule != null)
			{
				inputModule.Process(inputs, numOfInputChannels, numOfSamples);
			}
			if (outputModule != null)
			{
				if (moduleList.Count > 0)
				{
					outputModule.ProcessModuleList(outputs, numOfOutputChannels, numOfSamples, ref moduleList);
				}
				else
				{
					outputModule.Process(outputs, numOfOutputChannels, numOfSamples);
				}
			}
		}

		public void Clear()
		{
			modules.Clear();
			outputModule = null;
			inputModule = null;
		}

		public Module CreateModule(string name, string guid)
		{
			Module module = FindModule(guid);
			if (module != null)
			{
				return module;
			}
			module = ModularSynthManager.Instance.CreateModule(name);
			if (module != null)
			{
				int hashCode = guid.GetHashCode();
				module.guid = guid;
				if (!modules.ContainsKey(hashCode))
				{
					modules.Add(hashCode, module);
				}
				if (name == "IO/Output")
				{
					if (outputModule != null)
					{
						return null;
					}
					outputModule = module as Output;
				}
				if (name == "IO/Input")
				{
					if (inputModule != null)
					{
						return null;
					}
					inputModule = module as Input;
				}
			}
			return module;
		}

		public bool LinkModules(string fromModule, int outputPin, string toModule, int inputPin)
		{
			Module module = FindModule(fromModule);
			Module module2 = FindModule(toModule);
			if (module != null && module2 != null)
			{
				return module2.AddConnection(module, inputPin, outputPin);
			}
			return false;
		}

		public void SetModuleProperty(string guid, string property, object value)
		{
			Module module = FindModule(guid);
			if (module != null)
			{
				module.SetProperty(property, value);
			}
		}

		public void SetModuleParameter(string guid, string parameter, object value)
		{
			Module module = FindModule(guid);
			if (module != null)
			{
				module.SetParameter(parameter, value);
			}
		}

		public Module FindModule(string guid)
		{
			int hashCode = guid.GetHashCode();
			if (modules.ContainsKey(hashCode))
			{
				return modules[hashCode];
			}
			return null;
		}
	}
}
