using System;
using System.Linq.Expressions;
using System.Reflection;
using DeviceDB;

namespace Disney.Mix.SDK.Internal
{
	[Serialized(102, new byte[] { })]
	public class UserDocument : AbstractDocument
	{
		public static readonly string SwidFieldName;

		public static readonly string DisplayNameFieldName;

		[Indexed]
		[Serialized(0, new byte[] { })]
		public string Swid;

		[Indexed]
		[Serialized(1, new byte[] { })]
		public string DisplayName;

		[Serialized(2, new byte[] { })]
		public long AvatarId;

		[Serialized(3, new byte[] { })]
		public string FirstName;

		[Serialized(4, new byte[] { })]
		public string HashedSwid;

		[Serialized(5, new byte[] { })]
		public string Status;

		static UserDocument()
		{
			ParameterExpression parameterExpression = Expression.Parameter(typeof(UserDocument), "f");
			SwidFieldName = FieldNameGetter.Get((UserDocument f) => f.Swid);
			DisplayNameFieldName = FieldNameGetter.Get((UserDocument f) => f.DisplayName);
		}
	}
}
