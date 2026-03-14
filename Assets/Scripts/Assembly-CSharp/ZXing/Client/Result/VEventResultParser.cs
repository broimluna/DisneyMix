using System;
using System.Collections.Generic;
using System.Globalization;

namespace ZXing.Client.Result
{
	internal sealed class VEventResultParser : ResultParser
	{
		public override ParsedResult parse(ZXing.Result result)
		{
			string text = result.Text;
			if (text == null)
			{
				return null;
			}
			int num = text.IndexOf("BEGIN:VEVENT");
			if (num < 0)
			{
				return null;
			}
			string summary = matchSingleVCardPrefixedField("SUMMARY", text, true);
			string text2 = matchSingleVCardPrefixedField("DTSTART", text, true);
			if (text2 == null)
			{
				return null;
			}
			string endString = matchSingleVCardPrefixedField("DTEND", text, true);
			string durationString = matchSingleVCardPrefixedField("DURATION", text, true);
			string location = matchSingleVCardPrefixedField("LOCATION", text, true);
			string organizer = stripMailto(matchSingleVCardPrefixedField("ORGANIZER", text, true));
			string[] array = matchVCardPrefixedField("ATTENDEE", text, true);
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = stripMailto(array[i]);
				}
			}
			string description = matchSingleVCardPrefixedField("DESCRIPTION", text, true);
			string text3 = matchSingleVCardPrefixedField("GEO", text, true);
			double result2;
			double result3;
			if (text3 == null)
			{
				result2 = double.NaN;
				result3 = double.NaN;
			}
			else
			{
				int num2 = text3.IndexOf(';');
				if (num2 < 0)
				{
					return null;
				}
				if (!double.TryParse(text3.Substring(0, num2), NumberStyles.Float, CultureInfo.InvariantCulture, out result2))
				{
					return null;
				}
				if (!double.TryParse(text3.Substring(num2 + 1), NumberStyles.Float, CultureInfo.InvariantCulture, out result3))
				{
					return null;
				}
			}
			try
			{
				return new CalendarParsedResult(summary, text2, endString, durationString, location, organizer, array, description, result2, result3);
			}
			catch (ArgumentException)
			{
				return null;
			}
		}

		private static string matchSingleVCardPrefixedField(string prefix, string rawText, bool trim)
		{
			List<string> list = VCardResultParser.matchSingleVCardPrefixedField(prefix, rawText, trim, false);
			return (list != null && list.Count != 0) ? list[0] : null;
		}

		private static string[] matchVCardPrefixedField(string prefix, string rawText, bool trim)
		{
			List<List<string>> list = VCardResultParser.matchVCardPrefixedField(prefix, rawText, trim, false);
			if (list == null || list.Count == 0)
			{
				return null;
			}
			int count = list.Count;
			string[] array = new string[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = list[i][0];
			}
			return array;
		}

		private static string stripMailto(string s)
		{
			if (s != null && (s.StartsWith("mailto:") || s.StartsWith("MAILTO:")))
			{
				s = s.Substring(7);
			}
			return s;
		}
	}
}
