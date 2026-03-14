namespace Fabric.ModularSynth
{
	public class Point
	{
		public float _x;

		public float _y;

		public CurveTypes _curveType = CurveTypes.Linear;

		public Point(float x, float y, CurveTypes curveType)
		{
			_x = x;
			_y = y;
			_curveType = curveType;
		}
	}
}
