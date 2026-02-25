using System;
using System.Collections.Generic;

namespace Mix
{
	public class AsyncHelper<T>
	{
		public Dictionary<string, IAsyncCall<T>> executedCalls;

		public Dictionary<string, Action<T>> dict;

		public Dictionary<int, Action<T>> actionMapper;

		public AsyncHelper()
		{
			executedCalls = new Dictionary<string, IAsyncCall<T>>();
			dict = new Dictionary<string, Action<T>>();
			actionMapper = new Dictionary<int, Action<T>>();
		}

		public Action AddAsyncCall(IAsyncCall<T> call, Action<T> newCallback)
		{
			actionMapper[newCallback.GetHashCode()] = delegate(T val)
			{
				actionMapper.Remove(newCallback.GetHashCode());
				newCallback(val);
			};
			if (dict.ContainsKey(call.GetIdentifier()))
			{
				Dictionary<string, Action<T>> dictionary2;
				Dictionary<string, Action<T>> dictionary = (dictionary2 = dict);
				string identifier;
				string key = (identifier = call.GetIdentifier());
				Action<T> a = dictionary2[identifier];
				dictionary[key] = (Action<T>)Delegate.Combine(a, actionMapper[newCallback.GetHashCode()]);
			}
			else
			{
				dict.Add(call.GetIdentifier(), actionMapper[newCallback.GetHashCode()]);
				executedCalls[call.GetIdentifier()] = call;
				call.Execute(delegate(T val)
				{
					if (dict.ContainsKey(call.GetIdentifier()))
					{
						Action<T> action = dict[call.GetIdentifier()];
						dict.Remove(call.GetIdentifier());
						action(val);
					}
				});
			}
			return delegate
			{
				if (actionMapper.ContainsKey(newCallback.GetHashCode()) && dict.ContainsKey(call.GetIdentifier()))
				{
					Dictionary<string, Action<T>> dictionary4;
					Dictionary<string, Action<T>> dictionary3 = (dictionary4 = dict);
					string identifier2;
					string key2 = (identifier2 = call.GetIdentifier());
					Action<T> source = dictionary4[identifier2];
					dictionary3[key2] = (Action<T>)Delegate.Remove(source, actionMapper[newCallback.GetHashCode()]);
					if (dict[call.GetIdentifier()] == null || dict[call.GetIdentifier()].GetInvocationList().Length <= 0)
					{
						executedCalls[call.GetIdentifier()].Cancel();
						executedCalls.Remove(call.GetIdentifier());
						dict.Remove(call.GetIdentifier());
					}
				}
			};
		}
	}
}
