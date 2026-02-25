using System.Text;

namespace ZXing.Client.Result
{
	public sealed class AddressBookParsedResult : ParsedResult
	{
		private readonly string[] names;

		private readonly string[] nicknames;

		private readonly string pronunciation;

		private readonly string[] phoneNumbers;

		private readonly string[] phoneTypes;

		private readonly string[] emails;

		private readonly string[] emailTypes;

		private readonly string instantMessenger;

		private readonly string note;

		private readonly string[] addresses;

		private readonly string[] addressTypes;

		private readonly string org;

		private readonly string birthday;

		private readonly string title;

		private readonly string[] urls;

		private readonly string[] geo;

		public string[] Names
		{
			get
			{
				return names;
			}
		}

		public string[] Nicknames
		{
			get
			{
				return nicknames;
			}
		}

		public string Pronunciation
		{
			get
			{
				return pronunciation;
			}
		}

		public string[] PhoneNumbers
		{
			get
			{
				return phoneNumbers;
			}
		}

		public string[] PhoneTypes
		{
			get
			{
				return phoneTypes;
			}
		}

		public string[] Emails
		{
			get
			{
				return emails;
			}
		}

		public string[] EmailTypes
		{
			get
			{
				return emailTypes;
			}
		}

		public string InstantMessenger
		{
			get
			{
				return instantMessenger;
			}
		}

		public string Note
		{
			get
			{
				return note;
			}
		}

		public string[] Addresses
		{
			get
			{
				return addresses;
			}
		}

		public string[] AddressTypes
		{
			get
			{
				return addressTypes;
			}
		}

		public string Title
		{
			get
			{
				return title;
			}
		}

		public string Org
		{
			get
			{
				return org;
			}
		}

		public string[] URLs
		{
			get
			{
				return urls;
			}
		}

		public string Birthday
		{
			get
			{
				return birthday;
			}
		}

		public string[] Geo
		{
			get
			{
				return geo;
			}
		}

		public AddressBookParsedResult(string[] names, string[] phoneNumbers, string[] phoneTypes, string[] emails, string[] emailTypes, string[] addresses, string[] addressTypes)
			: this(names, null, null, phoneNumbers, phoneTypes, emails, emailTypes, null, null, addresses, addressTypes, null, null, null, null, null)
		{
		}

		public AddressBookParsedResult(string[] names, string[] nicknames, string pronunciation, string[] phoneNumbers, string[] phoneTypes, string[] emails, string[] emailTypes, string instantMessenger, string note, string[] addresses, string[] addressTypes, string org, string birthday, string title, string[] urls, string[] geo)
			: base(ParsedResultType.ADDRESSBOOK)
		{
			this.names = names;
			this.nicknames = nicknames;
			this.pronunciation = pronunciation;
			this.phoneNumbers = phoneNumbers;
			this.phoneTypes = phoneTypes;
			this.emails = emails;
			this.emailTypes = emailTypes;
			this.instantMessenger = instantMessenger;
			this.note = note;
			this.addresses = addresses;
			this.addressTypes = addressTypes;
			this.org = org;
			this.birthday = birthday;
			this.title = title;
			this.urls = urls;
			this.geo = geo;
			displayResultValue = getDisplayResult();
		}

		private string getDisplayResult()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			ParsedResult.maybeAppend(names, stringBuilder);
			ParsedResult.maybeAppend(nicknames, stringBuilder);
			ParsedResult.maybeAppend(pronunciation, stringBuilder);
			ParsedResult.maybeAppend(title, stringBuilder);
			ParsedResult.maybeAppend(org, stringBuilder);
			ParsedResult.maybeAppend(addresses, stringBuilder);
			ParsedResult.maybeAppend(phoneNumbers, stringBuilder);
			ParsedResult.maybeAppend(emails, stringBuilder);
			ParsedResult.maybeAppend(instantMessenger, stringBuilder);
			ParsedResult.maybeAppend(urls, stringBuilder);
			ParsedResult.maybeAppend(birthday, stringBuilder);
			ParsedResult.maybeAppend(geo, stringBuilder);
			ParsedResult.maybeAppend(note, stringBuilder);
			return stringBuilder.ToString();
		}
	}
}
