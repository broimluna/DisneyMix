using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Mix.Games.Tray.FortuneCookie
{
	public class FortuneGenerator : MonoBehaviour
	{
		public TextAsset master;

		public List<TextAsset> data;

		public int excludeListLength = 5;

		private List<string> mStructures;

		private Dictionary<string, List<string>> mFragments;

		private Dictionary<string, List<int>> mExcludeList;

		private string structuresExcludeListKey = "Structures";

		private void Awake()
		{
			mFragments = new Dictionary<string, List<string>>();
			mExcludeList = new Dictionary<string, List<int>>();
			foreach (TextAsset datum in data)
			{
				List<string> value = new List<string>(datum.text.Split(new string[2] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
				mFragments.Add(datum.name.ToLower(), value);
				mExcludeList.Add(datum.name.ToLower(), new List<int>());
			}
			mStructures = new List<string>(master.text.Split(new string[2] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
			mExcludeList.Add(structuresExcludeListKey, new List<int>());
		}

		public string GenerateRandomFortune()
		{
			int num = UnityEngine.Random.Range(0, mStructures.Count);
			int num2 = 5;
			while (mExcludeList[structuresExcludeListKey].Contains(num) && num2 > 0)
			{
				num = UnityEngine.Random.Range(0, mStructures.Count);
				num2--;
			}
			mExcludeList[structuresExcludeListKey].Add(num);
			string input = ExpandFragments(mStructures[num]);
			input = Regex.Replace(input, "(\\ba (a|e|i|u|o|A|E|I|O|U))+", "an $2");
			if (!input.EndsWith(",") && !input.EndsWith(".") && !input.EndsWith("?") && !input.EndsWith("!"))
			{
				input += ".";
			}
			return char.ToUpper(input[0]) + input.Substring(1);
		}

		public string ExpandFragments(string fragment)
		{
			string text = string.Empty;
			List<string> list = new List<string>(Regex.Split(fragment, "(?=[ .,;?!])"));
			foreach (string item in list)
			{
				text = ((item.Length <= 0 || item.Trim()[0] != '^') ? (text + item) : (text + " " + ExpandFragments(GetRandomFragmentWithKeyword(item.Substring(item.IndexOf('^') + 1).ToLower()))));
			}
			return text.Trim();
		}

		public string GetRandomFragmentWithKeyword(string keyword)
		{
			if (mFragments.ContainsKey(keyword))
			{
				int num = UnityEngine.Random.Range(0, mFragments[keyword].Count);
				string text = mFragments[keyword][num];
				int num2 = 5;
				while (text.Length == 0 || (mExcludeList[keyword].Contains(num) && num2 > 0))
				{
					num = UnityEngine.Random.Range(0, mFragments[keyword].Count);
					text = mFragments[keyword][num];
					num2--;
				}
				mExcludeList[keyword].Add(num);
				if (mExcludeList[keyword].Count > excludeListLength)
				{
					mExcludeList[keyword].RemoveAt(0);
				}
				return text;
			}
			return "(ERROR: " + keyword + ")";
		}
	}
}
