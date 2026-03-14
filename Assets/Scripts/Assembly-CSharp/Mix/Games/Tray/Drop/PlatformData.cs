namespace Mix.Games.Tray.Drop
{
	public class PlatformData
	{
		public bool is_decoy { get; set; }

		public Coordinate2D grid_coordinate { get; set; }

		public int rotation { get; set; }

		public PlatformType type { get; set; }

		public double enter_time { get; set; }

		public double duration { get; set; }

		public string fences { get; set; }

		public int path_order { get; set; }

		public PlatformData()
		{
		}

		public PlatformData(PlatformInfo info, bool isDecoy)
		{
			is_decoy = isDecoy;
			grid_coordinate = info.GridCoordinates;
			rotation = info.Rotation;
			type = info.Type;
			enter_time = info.EnterTime;
			duration = info.Duration;
			fences = info.Fences.GetBits();
			path_order = info.PathOrder;
		}

		public PlatformInfo ToPlatformInfo()
		{
			PlatformInfo platformInfo = new PlatformInfo();
			platformInfo.GridCoordinates = grid_coordinate;
			platformInfo.Rotation = rotation;
			platformInfo.Type = type;
			platformInfo.EnterTime = (float)enter_time;
			platformInfo.Duration = (float)duration;
			platformInfo.Fences.SetFromBits(fences);
			platformInfo.PathOrder = path_order;
			return platformInfo;
		}
	}
}
