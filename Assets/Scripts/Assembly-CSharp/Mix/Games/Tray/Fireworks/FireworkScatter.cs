namespace Mix.Games.Tray.Fireworks
{
	public class FireworkScatter : Firework
	{
		public SnakeScatter snakeScatter;

		public override void Explode()
		{
			base.Explode();
			snakeScatter.Explode();
		}

		public override void Return()
		{
			base.Return();
			snakeScatter.Return();
		}
	}
}
