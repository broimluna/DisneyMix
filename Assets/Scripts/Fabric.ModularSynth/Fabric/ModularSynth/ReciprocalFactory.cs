namespace Fabric.ModularSynth
{
	internal class ReciprocalFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new Reciprocal();
		}

		public override string Name()
		{
			return "Math/Reciprocal";
		}
	}
}
