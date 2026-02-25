namespace Mix.Games.Tray.Fireworks
{
	public class FireworkF : Firework
	{
		public FontFirework FontFirework;

		public override void Explode()
		{
			base.Explode();
			FontFirework.ExplodeD();
		}

		public override void Return()
		{
			base.Return();
			FontFirework.Return();
		}
	}
}
