using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	internal static class ListExtensions
	{
		public static T RandomItem<T>(this IList<T> list, System.Random randomGenerator = null)
		{
			if (randomGenerator == null)
			{
				return list[UnityEngine.Random.Range(0, list.Count - 1)];
			}
			return list[randomGenerator.Next(list.Count - 1)];
		}

		public static void Shuffle<T>(this IList<T> list, System.Random randomGenerator = null)
		{
			if (randomGenerator == null)
			{
				for (int i = 0; i < list.Count - 1; i++)
				{
					list.Swap(i, UnityEngine.Random.Range(i, list.Count));
				}
			}
			else
			{
				for (int j = 0; j < list.Count - 1; j++)
				{
					list.Swap(j, randomGenerator.Next(j, list.Count));
				}
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
