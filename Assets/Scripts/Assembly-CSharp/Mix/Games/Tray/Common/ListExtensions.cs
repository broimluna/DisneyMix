using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mix.Games.Tray.Common
{
	internal static class ListExtensions
	{
		public static T RandomItem<T>(this IList<T> list)
		{
			return list[UnityEngine.Random.Range(0, list.Count - 1)];
		}

		public static T RandomItem<T>(this IList<T> list, System.Random randomGenerator)
		{
			return list[randomGenerator.Next(list.Count - 1)];
		}

		public static void Shuffle<T>(this IList<T> list)
		{
			for (int i = 0; i < list.Count - 1; i++)
			{
				list.Swap(i, UnityEngine.Random.Range(i, list.Count));
			}
		}

		public static void Shuffle<T>(this IList<T> list, System.Random randomGenerator)
		{
			for (int i = 0; i < list.Count - 1; i++)
			{
				list.Swap(i, randomGenerator.Next(i, list.Count));
			}
		}

		public static void Swap<T>(this IList<T> list, int i, int j)
		{
			T value = list[i];
			list[i] = list[j];
			list[j] = value;
		}
	}
}
