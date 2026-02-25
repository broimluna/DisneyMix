using System.Collections.Generic;

namespace Fabric.ModularSynth
{
	public class ModularSynthManager
	{
		private List<ModuleFactory> moduleFactories = new List<ModuleFactory>();

		private static ModularSynthManager instance;

		public static ModularSynthManager Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new ModularSynthManager();
				}
				return instance;
			}
		}

		public ModularSynthManager()
		{
			RegisterComponentFactory(new DevideFactory());
			RegisterComponentFactory(new AddFactory());
			RegisterComponentFactory(new SubtractFactory());
			RegisterComponentFactory(new MultiplyFactory());
			RegisterComponentFactory(new ReciprocalFactory());
			RegisterComponentFactory(new SingleDelayFactory());
			RegisterComponentFactory(new ScopeFactory());
			RegisterComponentFactory(new LevelMeterFactory());
			RegisterComponentFactory(new ADSRFactory());
			RegisterComponentFactory(new LowPassFilterFactory());
			RegisterComponentFactory(new HighPassFilterFactory());
			RegisterComponentFactory(new BandPassFilterFactory());
			RegisterComponentFactory(new AllPassFilterFactory());
			RegisterComponentFactory(new NotchFilterFactory());
			RegisterComponentFactory(new PeakingEQFilterFactory());
			RegisterComponentFactory(new LowShelfFilterFactory());
			RegisterComponentFactory(new HighShelfFilterFactory());
			RegisterComponentFactory(new InputFactory());
			RegisterComponentFactory(new OutputFactory());
			RegisterComponentFactory(new LFOFactory());
			RegisterComponentFactory(new OscillatorFactory());
			RegisterComponentFactory(new FloatSliderFactory());
			RegisterComponentFactory(new FloatConstantFactory());
			RegisterComponentFactory(new IntSliderFactory());
			RegisterComponentFactory(new IntConstantFactory());
			RegisterComponentFactory(new TextFactory());
			RegisterComponentFactory(new SwitchFactory());
			RegisterComponentFactory(new ButtonFactory());
			RegisterComponentFactory(new ListFactory());
			RegisterComponentFactory(new SamplePlayerFactory());
			RegisterComponentFactory(new MultiplierFactory());
			RegisterComponentFactory(new MixerFactory());
			RegisterComponentFactory(new CrossfadeFactory());
			RegisterComponentFactory(new PitchShifterFactory());
			RegisterComponentFactory(new AudioToControlFactory());
			RegisterComponentFactory(new OnStartTriggerFactory());
		}

		public Module CreateModule(string name)
		{
			Module module = null;
			for (int i = 0; i < moduleFactories.Count; i++)
			{
				ModuleFactory moduleFactory = moduleFactories[i];
				if (moduleFactory.Name() == name)
				{
					module = moduleFactory.CreateInstance();
					module.name = moduleFactory.Name();
					break;
				}
			}
			return module;
		}

		public void RegisterComponentFactory(ModuleFactory moduleFactory)
		{
			if (!moduleFactories.Contains(moduleFactory))
			{
				moduleFactories.Add(moduleFactory);
			}
		}
	}
}
