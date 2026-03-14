using System.Collections.Generic;
using Mix.Games.Data;
using UnityEngine;

namespace Mix.Games.Tray.Fireworks
{
	public class PlaybackAssembler
	{
		public float mZValue;

		private Vector3 mFireworksOrigin = Vector3.zero;

		private Vector3 mFireworksPlaneRight = Vector3.right;

		public ShowData Parse(FireworksData data)
		{
			ShowData result = default(ShowData);
			mFireworksOrigin = StringToVector3(data.Origin);
			mFireworksPlaneRight = StringToVector3(data.VRight);
			result.mLaunchedFireworks = new Dictionary<float, KeyValuePair<int, Vector3>>();
			for (int i = 0; i < data.NumFireworksLaunched; i++)
			{
				float num = MsToTime(data.TimeStamps[i]);
				Vector3 value = StringToFireworksPos(data.FireworksLocations[i]);
				if (result.mLaunchedFireworks.ContainsKey(num))
				{
					num += 0.0005f;
				}
				result.mLaunchedFireworks.Add(num, new KeyValuePair<int, Vector3>(data.FireworksLaunched[i], value));
			}
			result.mGestures = new List<Gesture>();
			if (data.Gestures != null)
			{
				for (int j = 0; j < data.Gestures.Count; j++)
				{
					Gesture gesture = new Gesture();
					for (int k = 0; k < data.Gestures[j].Time.Count; k++)
					{
						FireworksData.GestureData gestureData = new FireworksData.GestureData();
						gestureData.Time = data.Gestures[j].Time[k];
						gestureData.Pos = data.Gestures[j].Pos[k];
						GesturePoint point = new GesturePoint(gestureData, this);
						gesture.AddNewPoint(point);
					}
					result.mGestures.Add(gesture);
				}
			}
			result.mSelectedScene = Toolbox.Instance.mSceneManager.GetSceneAtIndex(data.Scene);
			result.mMessage = data.Message;
			return result;
		}

		public FireworksData Assemble(ShowData data, FireworksData configData)
		{
			configData.NumFireworksLaunched = data.mLaunchedFireworks.Count;
			FireworkTapPlane tapPlane = data.mSelectedScene.TapPlane;
			mFireworksPlaneRight = tapPlane.GetRightVector();
			configData.VRight = Vector3ToString(mFireworksPlaneRight);
			configData.TimeStamps = new int[configData.NumFireworksLaunched];
			configData.FireworksLaunched = new int[configData.NumFireworksLaunched];
			configData.FireworksLocations = new string[configData.NumFireworksLaunched];
			if (data.mLaunchedFireworks.Count > 0)
			{
				using (Dictionary<float, KeyValuePair<int, Vector3>>.Enumerator enumerator = data.mLaunchedFireworks.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						mFireworksOrigin = enumerator.Current.Value.Value;
					}
				}
			}
			else if (data.mGestures.Count > 0)
			{
				GesturePoint gesturePointAtIndex = data.mGestures[0].GetGesturePointAtIndex(0);
				mFireworksOrigin = gesturePointAtIndex.Point;
			}
			configData.Origin = Vector3ToString(mFireworksOrigin);
			int num = 0;
			foreach (KeyValuePair<float, KeyValuePair<int, Vector3>> mLaunchedFirework in data.mLaunchedFireworks)
			{
				if (num < configData.NumFireworksLaunched)
				{
					configData.TimeStamps[num] = TimeToMs(mLaunchedFirework.Key);
					configData.FireworksLaunched[num] = mLaunchedFirework.Value.Key;
					configData.FireworksLocations[num] = FireworksPosToString(mLaunchedFirework.Value.Value);
					num++;
				}
			}
			configData.Gestures = new List<FireworksData.Gesture>();
			for (int i = 0; i < data.mGestures.Count; i++)
			{
				FireworksData.Gesture gesture = new FireworksData.Gesture();
				for (int j = 0; j < data.mGestures[i].GetPointCount(); j++)
				{
					GesturePoint gesturePointAtIndex2 = data.mGestures[i].GetGesturePointAtIndex(j);
					FireworksData.GestureData gestureData = gesturePointAtIndex2.ToSerializableGestureData(this);
					gesture.Pos.Add(gestureData.Pos);
					gesture.Time.Add(gestureData.Time);
				}
				configData.Gestures.Add(gesture);
			}
			configData.Scene = Toolbox.Instance.mSceneManager.GetSceneIndex(data.mSelectedScene);
			configData.Message = data.mMessage;
			configData = checkDataSize(configData);
			return configData;
		}

		private FireworksData checkDataSize(FireworksData data)
		{
			return data;
		}

		public Vector3 StringToVector3(string vectorString)
		{
			string[] array = vectorString.Split(',');
			float x = float.Parse(array[0]);
			float y = float.Parse(array[1]);
			float z = float.Parse(array[2]);
			return new Vector3(x, y, z);
		}

		public string Vector3ToString(Vector3 aVec)
		{
			return string.Format("{0:F2},{1:F2},{2:F2}", aVec.x, aVec.y, aVec.z);
		}

		public Vector3 StringToFireworksPos(string vectorString)
		{
			string[] array = vectorString.Split(',');
			float xVal = float.Parse(array[0]);
			float yVal = float.Parse(array[1]);
			return PlaneRelativeToWorldSpacePoint(xVal, yVal);
		}

		public string FireworksPosToString(Vector3 aVec)
		{
			Vector2 vector = WorldSpaceToPlaneRelativePoint(aVec);
			return string.Format("{0:F1},{1:F1}", vector.x, vector.y);
		}

		public int TimeToMs(float aTime)
		{
			return Mathf.RoundToInt(aTime * 1000f);
		}

		public float MsToTime(int aMs)
		{
			return (float)aMs * 0.001f;
		}

		public Vector2 WorldSpaceToPlaneRelativePoint(Vector3 aWorldPoint)
		{
			Vector3 lhs = aWorldPoint - mFireworksOrigin;
			float x = Vector3.Dot(lhs, mFireworksPlaneRight);
			return new Vector2(x, lhs.y);
		}

		public Vector3 PlaneRelativeToWorldSpacePoint(float xVal, float yVal)
		{
			return xVal * mFireworksPlaneRight + yVal * Vector3.up + mFireworksOrigin;
		}
	}
}
