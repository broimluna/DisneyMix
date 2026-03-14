using Mix.Games.Data;
using UnityEngine;

namespace Mix.Games.Tray.Fireworks
{
	public class GesturePoint
	{
		public float Timestamp { get; set; }

		public Vector3 Point { get; set; }

		public GesturePoint(float timestamp, Vector3 point)
		{
			Timestamp = timestamp;
			Point = point;
		}

		public GesturePoint(GesturePoint other)
		{
			Timestamp = other.Timestamp;
			Point = other.Point;
		}

		public GesturePoint(FireworksData.GestureData data, PlaybackAssembler aPlaybackAssembler)
		{
			ParseSerializableGestureData(data, aPlaybackAssembler);
		}

		public FireworksData.GestureData ToSerializableGestureData(PlaybackAssembler aPlaybackAssembler)
		{
			FireworksData.GestureData gestureData = new FireworksData.GestureData();
			gestureData.Time = aPlaybackAssembler.TimeToMs(Timestamp);
			gestureData.Pos = aPlaybackAssembler.FireworksPosToString(Point);
			return gestureData;
		}

		public override string ToString()
		{
			return string.Format("[{0}] {1}", Timestamp, Point.ToString());
		}

		public void ParseSerializableGestureData(FireworksData.GestureData data, PlaybackAssembler aPlaybackAssembler)
		{
			Timestamp = aPlaybackAssembler.MsToTime(data.Time);
			Point = aPlaybackAssembler.StringToFireworksPos(data.Pos);
		}
	}
}
