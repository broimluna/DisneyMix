using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Fabric.ModularSynth
{
	public abstract class Module
	{
		private class ConnectionHolder
		{
			public Module module;

			private int refCount;

			public void Increment()
			{
				refCount++;
			}
		}

		private List<ConnectionHolder> connections = new List<ConnectionHolder>();

		private List<PinBase> pins = new List<PinBase>();

		private Dictionary<int, PinBase> properties = new Dictionary<int, PinBase>();

		private List<Parameter> parameters = new List<Parameter>();

		private int currentFrameID = -1;

		private int registeredPinID;

		private Stopwatch watch = new Stopwatch();

		private double prevElapsed;

		[CompilerGenerated]
		private string _003Cguid_003Ek__BackingField;

		[CompilerGenerated]
		private string _003Cname_003Ek__BackingField;

		[CompilerGenerated]
		private bool _003CIsBypassed_003Ek__BackingField;

		[CompilerGenerated]
		private double _003CCPUUsageExcl_003Ek__BackingField;

		public string guid
		{
			[CompilerGenerated]
			set
			{
				_003Cguid_003Ek__BackingField = value;
			}
		}

		public string name
		{
			[CompilerGenerated]
			set
			{
				_003Cname_003Ek__BackingField = value;
			}
		}

		public ModuleType Type { get; protected set; }

		public bool IsBypassed
		{
			[CompilerGenerated]
			get
			{
				return _003CIsBypassed_003Ek__BackingField;
			}
		}

		public double CPUUsage { get; private set; }

		private double CPUUsageExcl
		{
			[CompilerGenerated]
			set
			{
				_003CCPUUsageExcl_003Ek__BackingField = value;
			}
		}

		public virtual void OnCreate()
		{
		}

		public virtual void OnPlay()
		{
		}

		public virtual void OnStop()
		{
		}

		public virtual void OnNumChannelsChange()
		{
		}

		public virtual void OnBlockSizeChange()
		{
		}

		public virtual void OnUpdate()
		{
		}

		public virtual void OnAudioPinsUpdate()
		{
		}

		public virtual void OnControlPinsUpdated()
		{
		}

		protected void RegisterPin(PinBase pin, string name)
		{
			pin.ID = registeredPinID++;
			pin.ParentModule = this;
			pin.Name = name;
			pins.Add(pin);
			if (pin.Type == ePinType.Control)
			{
				int hashCode = name.GetHashCode();
				properties.Add(hashCode, pin);
			}
		}

		public void RefreshPins()
		{
			List<RefreshPinData> list = new List<RefreshPinData>();
			foreach (KeyValuePair<int, PinBase> property in properties)
			{
				ControlOutputPin controlOutputPin = property.Value as ControlOutputPin;
				if (controlOutputPin != null)
				{
					int hashCode = controlOutputPin.Name.GetHashCode();
					if (property.Key != hashCode)
					{
						list.Add(new RefreshPinData
						{
							oldKey = property.Key,
							newKey = hashCode
						});
					}
				}
			}
			foreach (RefreshPinData item in list)
			{
				ControlOutputPin controlOutputPin2 = properties[item.oldKey] as ControlOutputPin;
				if (controlOutputPin2 != null)
				{
					properties.Remove(item.oldKey);
					properties[item.newKey] = controlOutputPin2;
				}
			}
		}

		public PinBase GetPin(int id)
		{
			for (int i = 0; i < pins.Count; i++)
			{
				PinBase pinBase = pins[i];
				if (pinBase.ID == id)
				{
					return pinBase;
				}
			}
			return null;
		}

		public void SetProperty(string propertyName, object value)
		{
			int hashCode = propertyName.GetHashCode();
			if (properties.ContainsKey(hashCode))
			{
				ControlOutputPin controlOutputPin = properties[hashCode] as ControlOutputPin;
				if (controlOutputPin != null)
				{
					controlOutputPin.Value = value;
				}
			}
		}

		public ControlOutputPin[] GetPropertiesArray()
		{
			List<ControlOutputPin> list = new List<ControlOutputPin>();
			foreach (KeyValuePair<int, PinBase> property in properties)
			{
				list.Add(property.Value as ControlOutputPin);
			}
			return list.ToArray();
		}

		public void RegisterParameter(object data, string name, object min = null, object max = null)
		{
			Parameter parameter = new Parameter();
			parameter._data = data;
			parameter._name = name;
			parameter._min = min;
			parameter._max = max;
			parameters.Add(parameter);
		}

		public void SetParameter(string Name, object data)
		{
			for (int i = 0; i < parameters.Count; i++)
			{
				Parameter parameter = parameters[i];
				if (parameter._name == Name)
				{
					parameter._data = data;
				}
			}
		}

		public object GetParameter(string Name)
		{
			for (int i = 0; i < parameters.Count; i++)
			{
				Parameter parameter = parameters[i];
				if (parameter._name == Name)
				{
					return parameter._data;
				}
			}
			return null;
		}

		private bool CanConnect(eControlType input, eControlType output)
		{
			if ((output == eControlType.IntConstant && input == eControlType.IntSlider) || (output == eControlType.IntSlider && input == eControlType.IntConstant))
			{
				return true;
			}
			if ((output == eControlType.FloatConstant && input == eControlType.FloatSlider) || (output == eControlType.FloatSlider && input == eControlType.FloatConstant))
			{
				return true;
			}
			if ((output == eControlType.Button && input == eControlType.Button) || (output == eControlType.Switch && input == eControlType.Switch) || (output == eControlType.Button && input == eControlType.Switch) || (output == eControlType.Switch && input == eControlType.Button))
			{
				return true;
			}
			if (input == output)
			{
				return true;
			}
			return false;
		}

		public bool AddConnection(Module module, int inputPinID, int outputPinID)
		{
			ConnectionHolder connectionHolder = null;
			for (int i = 0; i < connections.Count; i++)
			{
				connectionHolder = connections[i];
				if (connectionHolder.module == module)
				{
					if (ConnectionAlreadyExists(inputPinID, outputPinID))
					{
						return false;
					}
					connectionHolder.Increment();
					break;
				}
				connectionHolder = null;
			}
			if (connectionHolder == null)
			{
				connectionHolder = new ConnectionHolder();
				connectionHolder.module = module;
				connectionHolder.Increment();
				connections.Add(connectionHolder);
			}
			PinBase pin = GetPin(inputPinID);
			PinBase pin2 = module.GetPin(outputPinID);
			bool result = false;
			if (pin != null && pin.Direction == ePinDirection.Input)
			{
				InputPin inputPin = pin as InputPin;
				if (pin2 != null && pin2.Direction == ePinDirection.Output)
				{
					OutputPin outputPin = pin2 as OutputPin;
					ControlInputPin controlInputPin = inputPin as ControlInputPin;
					ControlOutputPin controlOutputPin = outputPin as ControlOutputPin;
					bool flag = true;
					if (controlInputPin != null && controlOutputPin != null && !CanConnect(controlOutputPin.Type, controlInputPin.Type))
					{
						flag = false;
					}
					if (inputPin.Type == outputPin.Type && flag)
					{
						inputPin._outputPin = outputPin;
						outputPin._inputPin = inputPin;
						if (controlOutputPin != null)
						{
							outputPin.Name = inputPin.Name;
							controlOutputPin.ParentModule.RefreshPins();
						}
						result = true;
					}
				}
			}
			return result;
		}

		public void Update(int frameID = -1, bool ignoreConnections = false)
		{
			if (Type == ModuleType.Panel)
			{
				return;
			}
			if (!watch.IsRunning)
			{
				watch.Start();
			}
			if (frameID == currentFrameID)
			{
				return;
			}
			currentFrameID = frameID;
			double num = 0.0;
			if (!ignoreConnections)
			{
				for (int i = 0; i < connections.Count; i++)
				{
					Module module = connections[i].module;
					module.Update(frameID);
					num += module.CPUUsage;
				}
			}
			if (ControlPinsAreDiry())
			{
				OnControlPinsUpdated();
			}
			if (AudioBuffersAreValid())
			{
				if (!IsBypassed)
				{
					OnAudioPinsUpdate();
				}
				else
				{
					AudioInputPin audioInputPin = null;
					AudioOutputPin audioOutputPin = null;
					for (int j = 0; j < pins.Count; j++)
					{
						if (audioInputPin == null)
						{
							audioInputPin = pins[j] as AudioInputPin;
						}
						if (audioOutputPin == null)
						{
							audioOutputPin = pins[j] as AudioOutputPin;
						}
						if (audioInputPin != null && audioOutputPin != null)
						{
							AudioBuffer buffer = audioInputPin.GetBuffer();
							AudioBuffer buffer2 = audioOutputPin.GetBuffer();
							if (buffer != null && buffer2 != null)
							{
								buffer.CopyToBuffer(buffer2);
							}
							break;
						}
					}
				}
			}
			ResetControlPinDirtyFlags();
			watch.Stop();
			CPUUsage = ((double)watch.ElapsedTicks - prevElapsed) / (double)Stopwatch.Frequency * 1000.0;
			CPUUsageExcl = CPUUsage - num;
			prevElapsed = watch.ElapsedTicks;
		}

		public void SetNumChannels(int numChannels)
		{
			for (int i = 0; i < pins.Count; i++)
			{
				PinBase pinBase = pins[i];
				if (pinBase.Type == ePinType.Audio)
				{
					AudioOutputPin audioOutputPin = pinBase as AudioOutputPin;
					if (audioOutputPin != null && audioOutputPin.HasBuffer())
					{
						audioOutputPin.GetBuffer().Resize(audioOutputPin.GetBuffer().Size, numChannels);
					}
				}
			}
			OnNumChannelsChange();
		}

		public void SetBlockSize(int blockSize)
		{
			for (int i = 0; i < pins.Count; i++)
			{
				PinBase pinBase = pins[i];
				if (pinBase.Type == ePinType.Audio)
				{
					AudioOutputPin audioOutputPin = pinBase as AudioOutputPin;
					if (audioOutputPin != null && audioOutputPin.HasBuffer())
					{
						audioOutputPin.GetBuffer().Resize(blockSize, audioOutputPin.GetBuffer().NumChannels);
					}
				}
			}
			OnBlockSizeChange();
		}

		private bool ConnectionAlreadyExists(int inputPinID, int outputPinID)
		{
			PinBase pin = GetPin(inputPinID);
			if (pin != null && pin.Direction == ePinDirection.Input)
			{
				InputPin inputPin = pin as InputPin;
				if (inputPin != null)
				{
					OutputPin outputPin = inputPin._outputPin;
					if (outputPin != null && outputPin.ID == outputPinID)
					{
						return true;
					}
				}
			}
			return false;
		}

		private bool AudioBuffersAreValid()
		{
			return true;
		}

		private bool ControlPinsAreDiry()
		{
			for (int i = 0; i < pins.Count; i++)
			{
				PinBase pinBase = pins[i];
				if (pinBase.Direction == ePinDirection.Input && pinBase.Type == ePinType.Control)
				{
					ControlInputPin controlInputPin = pinBase as ControlInputPin;
					if (controlInputPin != null && controlInputPin.IsDirty())
					{
						return true;
					}
				}
			}
			return false;
		}

		private void ResetControlPinDirtyFlags()
		{
			for (int i = 0; i < pins.Count; i++)
			{
				PinBase pinBase = pins[i];
				if (pinBase.Direction == ePinDirection.Input && pinBase.Type == ePinType.Control)
				{
					ControlInputPin controlInputPin = pinBase as ControlInputPin;
					if (controlInputPin != null && controlInputPin.IsDirty())
					{
						controlInputPin.ResetDirtyFlag();
					}
				}
			}
		}
	}
}
