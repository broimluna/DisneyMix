namespace Mix.Games.Tray.Drop
{
	public class DropDirection
	{
		public int X { get; set; }

		public int Y { get; set; }

		public int JumpCount { get; set; }

		public DropDirection()
		{
			JumpCount = 0;
			X = 0;
			Y = 0;
		}

		public DropDirection(int jumpCount, int x, int y)
		{
			JumpCount = jumpCount;
			X = x;
			Y = y;
		}
	}
}
