namespace Mix.Games.Tray.Fireworks
{
	public class FireworkT : Firework
	{
		public TextureFirework TextureFirework;

		public override void Explode()
		{
			base.Explode();
			TextureFirework.Explode();
		}

		public override void Return()
		{
			base.Return();
			TextureFirework.Return();
		}
	}
}
