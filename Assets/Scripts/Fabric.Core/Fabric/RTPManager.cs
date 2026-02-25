using System;
using UnityEngine;

namespace Fabric
{
	[Serializable]
	public class RTPManager
	{
		[SerializeField]
		public RTPParameterToProperty[] _parameters;

		[SerializeField]
		public Component _reference;

		private float[] _cachedValues = new float[50];

		public void Init(Component component)
		{
			if (_parameters != null)
			{
				for (int i = 0; i < _parameters.Length; i++)
				{
					_parameters[i]._parameter.Init();
				}
			}
			SetupParameterNames(component);
		}

		private void SetupParameterNames(Component component)
		{
			for (int i = 0; i < _parameters.Length; i++)
			{
				RTPParameterToProperty rTPParameterToProperty = _parameters[i];
				bool flag = false;
				string[] array = rTPParameterToProperty._property._name.Split('/');
				if (array.Length == 1)
				{
					switch ((Component.RTPPropertyEnum)rTPParameterToProperty._property._property)
					{
					case Component.RTPPropertyEnum.Volume:
						rTPParameterToProperty._property._componentName = "Component";
						flag = true;
						break;
					case Component.RTPPropertyEnum.Pitch:
						rTPParameterToProperty._property._componentName = "Component";
						flag = true;
						break;
					case Component.RTPPropertyEnum.Pan2D:
						rTPParameterToProperty._property._componentName = "Component";
						flag = true;
						break;
					case Component.RTPPropertyEnum.PanLevel:
						rTPParameterToProperty._property._componentName = "Component";
						flag = true;
						break;
					case Component.RTPPropertyEnum.SpreadLevel:
						rTPParameterToProperty._property._componentName = "Component";
						flag = true;
						break;
					case Component.RTPPropertyEnum.DopplerLevel:
						rTPParameterToProperty._property._componentName = "Component";
						flag = true;
						break;
					case Component.RTPPropertyEnum.Priority:
						rTPParameterToProperty._property._componentName = "Component";
						flag = true;
						break;
					case Component.RTPPropertyEnum.ReverbZoneMix:
						rTPParameterToProperty._property._componentName = "Component";
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					for (int j = 0; j < component._dspComponents.Length; j++)
					{
						DSPComponent dSPComponent = component._dspComponents[j];
						if (!(array[0] == dSPComponent.GetTypeByName()))
						{
							continue;
						}
						for (int k = 0; k < dSPComponent.GetNumberOfParameters(); k++)
						{
							if (array[1] == dSPComponent.GetParameterNameByIndex(k))
							{
								rTPParameterToProperty._property._componentName = dSPComponent.GetTypeByName();
								rTPParameterToProperty._property._propertyName = array[1];
								flag = true;
								break;
							}
						}
					}
				}
				if (flag || component._sideChainComponents == null)
				{
					continue;
				}
				for (int l = 0; l < component._sideChainComponents.Length; l++)
				{
					SideChain sideChain = component._sideChainComponents[l];
					if ((bool)sideChain && array[0] == "SideChain")
					{
						rTPParameterToProperty._property._componentName = "SideChain";
						flag = true;
					}
				}
			}
		}

		public void Reset()
		{
			if (_parameters != null)
			{
				for (int i = 0; i < _parameters.Length; i++)
				{
					_parameters[i]._parameter.Reset();
				}
			}
		}

		public EventStatus SetParameter(Event zEvent)
		{
			EventStatus result = EventStatus.Failed_Uknown;
			if (_parameters == null)
			{
				return result;
			}
			for (int i = 0; i < _parameters.Length; i++)
			{
				RTPParameterToProperty rTPParameterToProperty = _parameters[i];
				ParameterData parameterData = (ParameterData)zEvent._parameter;
				if (rTPParameterToProperty._parameter != null && rTPParameterToProperty._parameter.Name == parameterData._parameter)
				{
					rTPParameterToProperty._parameter.SetValue(parameterData._value);
					result = EventStatus.Handled;
				}
			}
			return result;
		}

		public EventStatus SetMarker(Event zEvent)
		{
			EventStatus result = EventStatus.Failed_Uknown;
			if (_parameters == null)
			{
				return result;
			}
			for (int i = 0; i < _parameters.Length; i++)
			{
				RTPParameterToProperty rTPParameterToProperty = _parameters[i];
				string label = (string)zEvent._parameter;
				RTPMarker marker = rTPParameterToProperty._parameter._markers.GetMarker(label);
				if (marker != null)
				{
					rTPParameterToProperty._parameter.SetNormalisedValue(marker._value);
					result = EventStatus.Handled;
				}
			}
			return result;
		}

		public EventStatus KeyOffMarker(Event zEvent)
		{
			EventStatus result = EventStatus.Failed_Uknown;
			if (_parameters == null)
			{
				return result;
			}
			for (int i = 0; i < _parameters.Length; i++)
			{
				RTPParameterToProperty rTPParameterToProperty = _parameters[i];
				string text = (string)zEvent._parameter;
				rTPParameterToProperty._parameter._markers.KeyOffMarker();
				result = EventStatus.Handled;
			}
			return result;
		}

		public void Update(Component component)
		{
			if (_parameters == null)
			{
				return;
			}
			for (int i = 0; i < _cachedValues.Length; i++)
			{
				_cachedValues[i] = 1f;
			}
			for (int j = 0; j < _parameters.Length; j++)
			{
				RTPParameterToProperty rTPParameterToProperty = _parameters[j];
				rTPParameterToProperty._parameter.Update();
				float num = 1f;
				float num2 = 1f;
				float min = rTPParameterToProperty._property._min;
				float max = rTPParameterToProperty._property._max;
				if (rTPParameterToProperty._type == RTPParameterType.Distance || rTPParameterToProperty._parameter.Name == "Distance")
				{
					num = 1f / (rTPParameterToProperty._parameter._max - rTPParameterToProperty._parameter._min);
					if (component.ParentGameObject != null)
					{
						if (FabricManager.Instance._audioListener != null)
						{
							num2 = Vector3.Distance(component.ParentGameObject.transform.position, FabricManager.Instance._audioListener.transform.position);
						}
						else if (Camera.main != null)
						{
							num2 = Vector3.Distance(component.ParentGameObject.transform.position, Camera.main.transform.position);
						}
						rTPParameterToProperty._parameter.SetValue(num2);
						num2 *= num;
						num = max - min + min;
						num2 = rTPParameterToProperty._envelope.Calculate_y(num2) * num;
					}
				}
				else if (rTPParameterToProperty._type == RTPParameterType.Modulator)
				{
					RTPModulator rtpModulator = rTPParameterToProperty._rtpModulator;
					if (rtpModulator != null)
					{
						num2 = rtpModulator.GetValue(Time.time);
						rTPParameterToProperty._parameter.SetValue(num2);
						num2 *= num;
						num = max - min;
						num2 = rTPParameterToProperty._envelope.Calculate_y(num2) * num;
					}
				}
				else if (rTPParameterToProperty._type == RTPParameterType.Listener_Angle)
				{
					if (component.ParentGameObject != null)
					{
						Vector3 vector = default(Vector3);
						Vector3 to = default(Vector3);
						if (FabricManager.Instance._audioListener != null)
						{
							vector = component.ParentGameObject.transform.position - FabricManager.Instance._audioListener.transform.position;
							to = FabricManager.Instance._audioListener.transform.forward;
						}
						else if (Camera.main != null)
						{
							vector = component.ParentGameObject.transform.position - Camera.main.transform.position;
							to = Camera.main.transform.forward;
						}
						num2 = Vector3.Angle(vector, to);
						rTPParameterToProperty._parameter.SetValue(num2);
						num2 *= num;
						num = max - min;
						num2 = rTPParameterToProperty._envelope.Calculate_y(num2) * num;
					}
				}
				else if (rTPParameterToProperty._type == RTPParameterType.Component_Angle)
				{
					if (component.ParentGameObject != null)
					{
						Vector3 vector2 = default(Vector3);
						Vector3 to2 = default(Vector3);
						if (FabricManager.Instance._audioListener != null)
						{
							vector2 = component.ParentGameObject.transform.position - FabricManager.Instance._audioListener.transform.position;
							to2 = component.ParentGameObject.transform.forward;
						}
						else if (Camera.main != null)
						{
							vector2 = component.ParentGameObject.transform.position - Camera.main.transform.position;
							to2 = component.ParentGameObject.transform.forward;
						}
						num2 = Vector3.Angle(vector2, to2);
						rTPParameterToProperty._parameter.SetValue(num2);
						num2 *= num;
						num = max - min;
						num2 = rTPParameterToProperty._envelope.Calculate_y(num2) * num;
					}
				}
				else if (rTPParameterToProperty._type == RTPParameterType.Component_Velocity)
				{
					if (component.ParentGameObject != null)
					{
						num2 = (component.ParentGameObject.transform.position - rTPParameterToProperty._previousPosition).magnitude / FabricTimer.GetRealtimeDelta();
						rTPParameterToProperty._parameter.SetValue(num2);
						num2 *= num;
						num = max - min;
						num2 = rTPParameterToProperty._envelope.Calculate_y(num2) * num;
						rTPParameterToProperty._previousPosition = component.ParentGameObject.transform.position;
					}
				}
				else if (rTPParameterToProperty._type == RTPParameterType.Listener_Velocity)
				{
					if (FabricManager.Instance._audioListener != null)
					{
						num2 = (FabricManager.Instance._audioListener.transform.position - rTPParameterToProperty._previousPosition).magnitude / FabricTimer.GetRealtimeDelta();
						rTPParameterToProperty._previousPosition = FabricManager.Instance._audioListener.transform.position;
					}
					else if (Camera.main != null)
					{
						num2 = (Camera.main.transform.position - rTPParameterToProperty._previousPosition).magnitude / FabricTimer.GetRealtimeDelta();
						rTPParameterToProperty._previousPosition = Camera.main.transform.position;
					}
					rTPParameterToProperty._parameter.SetValue(num2);
					num2 *= num;
					num = max - min;
					num2 = rTPParameterToProperty._envelope.Calculate_y(num2) * num;
				}
				else if (rTPParameterToProperty._type == RTPParameterType.Volume_Meter)
				{
					VolumeMeter volumeMeter = rTPParameterToProperty._volumeMeter;
					if (volumeMeter != null)
					{
						num2 = volumeMeter.volumeMeterState.mRMS;
						rTPParameterToProperty._parameter.SetValue(num2);
						num2 *= num;
						num = max - min;
						num2 = rTPParameterToProperty._envelope.Calculate_y(num2) * num;
					}
				}
				else if (rTPParameterToProperty._type == RTPParameterType.Global_Parameter)
				{
					GlobalParameter globalParameter = EventManager.Instance._globalParameterManager._globalRTParameters.FindItem(rTPParameterToProperty._globalParameterName);
					if (globalParameter != null)
					{
						num = max - min;
						num2 = rTPParameterToProperty._envelope.Calculate_y(globalParameter.GetNormalisedCurrentValue());
						num2 *= num;
						rTPParameterToProperty._parameter.SetValue(num2);
					}
				}
				else
				{
					num = max - min;
					num2 = rTPParameterToProperty._envelope.Calculate_y(rTPParameterToProperty._parameter.GetNormalisedCurrentValue());
					num2 *= num;
				}
				if ((object)component != null)
				{
					int property = rTPParameterToProperty._property._property;
					_cachedValues[rTPParameterToProperty._property._property] *= num2;
					((IRTPPropertyListener)component).UpdateProperty(rTPParameterToProperty._property, _cachedValues[rTPParameterToProperty._property._property], rTPParameterToProperty._propertyType);
				}
			}
		}
	}
}
