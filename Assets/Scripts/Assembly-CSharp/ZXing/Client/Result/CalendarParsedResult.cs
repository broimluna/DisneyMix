using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace ZXing.Client.Result
{
	public sealed class CalendarParsedResult : ParsedResult
	{
		private static readonly Regex RFC2445_DURATION = new Regex("\\A(?:P(?:(\\d+)W)?(?:(\\d+)D)?(?:T(?:(\\d+)H)?(?:(\\d+)M)?(?:(\\d+)S)?)?)\\z");

		private static readonly long[] RFC2445_DURATION_FIELD_UNITS = new long[5] { 604800000L, 86400000L, 3600000L, 60000L, 1000L };

		private static readonly Regex DATE_TIME = new Regex("\\A(?:[0-9]{8}(T[0-9]{6}Z?)?)\\z");

		private readonly string summary;

		private readonly DateTime start;

		private readonly bool startAllDay;

		private readonly DateTime? end;

		private readonly bool endAllDay;

		private readonly string location;

		private readonly string organizer;

		private readonly string[] attendees;

		private readonly string description;

		private readonly double latitude;

		private readonly double longitude;

		public string Summary
		{
			get
			{
				return summary;
			}
		}

		public DateTime Start
		{
			get
			{
				return start;
			}
		}

		public DateTime? End
		{
			get
			{
				return end;
			}
		}

		public bool isEndAllDay
		{
			get
			{
				return endAllDay;
			}
		}

		public string Location
		{
			get
			{
				return location;
			}
		}

		public string Organizer
		{
			get
			{
				return organizer;
			}
		}

		public string[] Attendees
		{
			get
			{
				return attendees;
			}
		}

		public string Description
		{
			get
			{
				return description;
			}
		}

		public double Latitude
		{
			get
			{
				return latitude;
			}
		}

		public double Longitude
		{
			get
			{
				return longitude;
			}
		}

		public CalendarParsedResult(string summary, string startString, string endString, string durationString, string location, string organizer, string[] attendees, string description, double latitude, double longitude)
			: base(ParsedResultType.CALENDAR)
		{
			this.summary = summary;
			try
			{
				start = parseDate(startString);
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.ToString());
			}
			if (endString == null)
			{
				long num = parseDurationMS(durationString);
				DateTime? obj;
				if (num < 0)
				{
					obj = null;
				}
				else
				{
					DateTime? dateTime = start;
					obj = ((!dateTime.HasValue) ? ((DateTime?)null) : new DateTime?(dateTime.Value + new TimeSpan(0, 0, 0, 0, (int)num)));
				}
				end = obj;
			}
			else
			{
				try
				{
					end = parseDate(endString);
				}
				catch (Exception ex2)
				{
					throw new ArgumentException(ex2.ToString());
				}
			}
			startAllDay = startString.Length == 8;
			endAllDay = endString != null && endString.Length == 8;
			this.location = location;
			this.organizer = organizer;
			this.attendees = attendees;
			this.description = description;
			this.latitude = latitude;
			this.longitude = longitude;
			StringBuilder stringBuilder = new StringBuilder(100);
			ParsedResult.maybeAppend(summary, stringBuilder);
			ParsedResult.maybeAppend(format(startAllDay, start), stringBuilder);
			ParsedResult.maybeAppend(format(endAllDay, end), stringBuilder);
			ParsedResult.maybeAppend(location, stringBuilder);
			ParsedResult.maybeAppend(organizer, stringBuilder);
			ParsedResult.maybeAppend(attendees, stringBuilder);
			ParsedResult.maybeAppend(description, stringBuilder);
			displayResultValue = stringBuilder.ToString();
		}

		public bool isStartAllDay()
		{
			return startAllDay;
		}

		private static DateTime parseDate(string when)
		{
			if (!DATE_TIME.Match(when).Success)
			{
				throw new ArgumentException(string.Format("no date format: {0}", when));
			}
			if (when.Length == 8)
			{
				return DateTime.ParseExact(when, buildDateFormat(), CultureInfo.InvariantCulture);
			}
			if (when.Length == 16 && when[15] == 'Z')
			{
				DateTime dateTime = DateTime.ParseExact(when.Substring(0, 15), buildDateTimeFormat(), CultureInfo.InvariantCulture);
				return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Local);
			}
			return DateTime.ParseExact(when, buildDateTimeFormat(), CultureInfo.InvariantCulture);
		}

		private static string format(bool allDay, DateTime? date)
		{
			if (!date.HasValue)
			{
				return null;
			}
			if (allDay)
			{
				return date.Value.ToString("D", CultureInfo.CurrentCulture);
			}
			return date.Value.ToString("F", CultureInfo.CurrentCulture);
		}

		private static long parseDurationMS(string durationString)
		{
			if (durationString == null)
			{
				return -1L;
			}
			Match match = RFC2445_DURATION.Match(durationString);
			if (!match.Success)
			{
				return -1L;
			}
			long num = 0L;
			for (int i = 0; i < RFC2445_DURATION_FIELD_UNITS.Length; i++)
			{
				string value = match.Groups[i + 1].Value;
				if (!string.IsNullOrEmpty(value))
				{
					num += RFC2445_DURATION_FIELD_UNITS[i] * int.Parse(value);
				}
			}
			return num;
		}

		private static string buildDateFormat()
		{
			return "yyyyMMdd";
		}

		private static string buildDateTimeFormat()
		{
			return "yyyyMMdd'T'HHmmss";
		}
	}
}
