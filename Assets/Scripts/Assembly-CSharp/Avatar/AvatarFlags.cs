using System;

namespace Avatar
{
	[Flags]
	public enum AvatarFlags
	{
		IsHd = 1,
		WithNormals = 2,
		WithoutCaching = 4,
		WithoutCostume = 8,
		WithoutGeo = 0x10
	}
}
