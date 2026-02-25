using System;
using System.Text.RegularExpressions;

namespace Mix
{
	public class DataChecker
	{
		private const string emailRegexStr = "[-a-z0-9_+]+(\\.[-a-z0-9_+]+)*@([a-z0-9_][-a-z0-9_]*(\\.[-a-z0-9_]+)*\\.[a-z]{2,}|([0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}))";

		private const string urlRegexStr = "(https?://)?([a-zA-Z0-9_][a-zA-Z0-9-_]*\\.(([a-zA-Z0-9-]+)\\.)?(aero|arpa|asia|biz|cat|com|coop|edu|gov|info|int|jobs|mil|mobi|museum|name|net|org|pro|tel|travel|ac|ad|ae|af|ag|ai|al|am|an|ao|aq|ar|as|at|au|aw|ax|az|ba|bb|bd|be|bf|bg|bh|bi|bj|bm|bn|bo|br|bs|bt|bv|bw|by|bz|ca|cc|cd|cf|cg|ch|ci|ck|cl|cm|cn|co|cr|cu|cv|cx|cy|cz|cz|de|dj|dk|dm|do|dz|ec|ee|eg|er|es|et|eu|fi|fj|fk|fm|fo|fr|ga|gb|gd|ge|gf|gg|gh|gi|gl|gm|gn|gp|gq|gr|gs|gt|gu|gw|gy|hk|hm|hn|hr|ht|hu|id|ie|il|im|in|io|iq|ir|is|it|je|jm|jo|jp|ke|kg|kh|ki|km|kn|kp|kr|kw|ky|kz|la|lb|lc|li|lk|lr|ls|lt|lu|lv|ly|ma|mc|md|me|mg|mh|mk|ml|mn|mn|mo|mp|mr|ms|mt|mu|mv|mw|mx|my|mz|na|nc|ne|nf|ng|ni|nl|no|np|nr|nu|nz|nom|pa|pe|pf|pg|ph|pk|pl|pm|pn|pr|ps|pt|pw|py|qa|re|ra|rs|ru|rw|sa|sb|sc|sd|se|sg|sh|si|sj|sj|sk|sl|sm|sn|so|sr|st|su|sv|sy|sz|tc|td|tf|tg|th|tj|tk|tl|tm|tn|to|tp|tr|tt|tv|tw|tz|ua|ug|uk|us|uy|uz|va|vc|ve|vg|vi|vn|vu|wf|ws|ye|yt|yu|za|zm|zw))(?::\\d{2,5})?((?:/)(?!\\.)(?:([a-zA-Z0-9-_?=&%!][a-zA-Z0-9-_?=&%.!]*)*)+)*";

		public const string IK = "PVP";

		public static bool IsNullEmptyOrJustWhiteSpace(string aString)
		{
			return string.IsNullOrEmpty(aString) || string.Empty.Equals(aString.Trim());
		}

		public static bool IsValidSwid(string aSwid)
		{
			Regex regex = new Regex("\\{[0-9a-fA-F]{8}(-[0-9a-fA-F]{4}){3}-[0-9a-fA-F]{12}\\}");
			return !string.IsNullOrEmpty(aSwid) && regex.IsMatch(aSwid);
		}

		public static bool IsValidEmail(string aEmail)
		{
			Regex regex = new Regex("^[-a-z0-9_+]+(\\.[-a-z0-9_+]+)*@([a-z0-9_][-a-z0-9_]*(\\.[-a-z0-9_]+)*\\.[a-z]{2,}|([0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}))$", RegexOptions.IgnoreCase);
			return !string.IsNullOrEmpty(aEmail) && regex.IsMatch(aEmail);
		}

		public static bool IsValidUsernameNotEmail(string aUsername)
		{
			Regex regex = new Regex("^((?![-@\\.\\s]).)*$");
			return !string.IsNullOrEmpty(aUsername) && regex.IsMatch(aUsername);
		}

		public static bool IsValidUrl(string aUrl)
		{
			Regex regex = new Regex("^(https?://)?([a-zA-Z0-9_][a-zA-Z0-9-_]*\\.(([a-zA-Z0-9-]+)\\.)?(aero|arpa|asia|biz|cat|com|coop|edu|gov|info|int|jobs|mil|mobi|museum|name|net|org|pro|tel|travel|ac|ad|ae|af|ag|ai|al|am|an|ao|aq|ar|as|at|au|aw|ax|az|ba|bb|bd|be|bf|bg|bh|bi|bj|bm|bn|bo|br|bs|bt|bv|bw|by|bz|ca|cc|cd|cf|cg|ch|ci|ck|cl|cm|cn|co|cr|cu|cv|cx|cy|cz|cz|de|dj|dk|dm|do|dz|ec|ee|eg|er|es|et|eu|fi|fj|fk|fm|fo|fr|ga|gb|gd|ge|gf|gg|gh|gi|gl|gm|gn|gp|gq|gr|gs|gt|gu|gw|gy|hk|hm|hn|hr|ht|hu|id|ie|il|im|in|io|iq|ir|is|it|je|jm|jo|jp|ke|kg|kh|ki|km|kn|kp|kr|kw|ky|kz|la|lb|lc|li|lk|lr|ls|lt|lu|lv|ly|ma|mc|md|me|mg|mh|mk|ml|mn|mn|mo|mp|mr|ms|mt|mu|mv|mw|mx|my|mz|na|nc|ne|nf|ng|ni|nl|no|np|nr|nu|nz|nom|pa|pe|pf|pg|ph|pk|pl|pm|pn|pr|ps|pt|pw|py|qa|re|ra|rs|ru|rw|sa|sb|sc|sd|se|sg|sh|si|sj|sj|sk|sl|sm|sn|so|sr|st|su|sv|sy|sz|tc|td|tf|tg|th|tj|tk|tl|tm|tn|to|tp|tr|tt|tv|tw|tz|ua|ug|uk|us|uy|uz|va|vc|ve|vg|vi|vn|vu|wf|ws|ye|yt|yu|za|zm|zw))(?::\\d{2,5})?((?:/)(?!\\.)(?:([a-zA-Z0-9-_?=&%!][a-zA-Z0-9-_?=&%.!]*)*)+)*$");
			return !string.IsNullOrEmpty(aUrl) && regex.IsMatch(aUrl);
		}

		public static MatchCollection GetEmailsAndUrls(string aText)
		{
			return Regex.Matches(aText, "[-a-z0-9_+]+(\\.[-a-z0-9_+]+)*@([a-z0-9_][-a-z0-9_]*(\\.[-a-z0-9_]+)*\\.[a-z]{2,}|([0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}))|(https?://)?([a-zA-Z0-9_][a-zA-Z0-9-_]*\\.(([a-zA-Z0-9-]+)\\.)?(aero|arpa|asia|biz|cat|com|coop|edu|gov|info|int|jobs|mil|mobi|museum|name|net|org|pro|tel|travel|ac|ad|ae|af|ag|ai|al|am|an|ao|aq|ar|as|at|au|aw|ax|az|ba|bb|bd|be|bf|bg|bh|bi|bj|bm|bn|bo|br|bs|bt|bv|bw|by|bz|ca|cc|cd|cf|cg|ch|ci|ck|cl|cm|cn|co|cr|cu|cv|cx|cy|cz|cz|de|dj|dk|dm|do|dz|ec|ee|eg|er|es|et|eu|fi|fj|fk|fm|fo|fr|ga|gb|gd|ge|gf|gg|gh|gi|gl|gm|gn|gp|gq|gr|gs|gt|gu|gw|gy|hk|hm|hn|hr|ht|hu|id|ie|il|im|in|io|iq|ir|is|it|je|jm|jo|jp|ke|kg|kh|ki|km|kn|kp|kr|kw|ky|kz|la|lb|lc|li|lk|lr|ls|lt|lu|lv|ly|ma|mc|md|me|mg|mh|mk|ml|mn|mn|mo|mp|mr|ms|mt|mu|mv|mw|mx|my|mz|na|nc|ne|nf|ng|ni|nl|no|np|nr|nu|nz|nom|pa|pe|pf|pg|ph|pk|pl|pm|pn|pr|ps|pt|pw|py|qa|re|ra|rs|ru|rw|sa|sb|sc|sd|se|sg|sh|si|sj|sj|sk|sl|sm|sn|so|sr|st|su|sv|sy|sz|tc|td|tf|tg|th|tj|tk|tl|tm|tn|to|tp|tr|tt|tv|tw|tz|ua|ug|uk|us|uy|uz|va|vc|ve|vg|vi|vn|vu|wf|ws|ye|yt|yu|za|zm|zw))(?::\\d{2,5})?((?:/)(?!\\.)(?:([a-zA-Z0-9-_?=&%!][a-zA-Z0-9-_?=&%.!]*)*)+)*");
		}

		public static long ToUnixTime(DateTime aDate, bool aUtc = false)
		{
			DateTime dateTime = ((!aUtc) ? new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local) : new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
			return Convert.ToInt64((aDate.ToUniversalTime() - dateTime).TotalSeconds);
		}

		public static DateTime FromUnixTime(long aUnixTime, bool aUtc = false)
		{
			return ((!aUtc) ? new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local) : new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddSeconds(aUnixTime);
		}
	}
}
