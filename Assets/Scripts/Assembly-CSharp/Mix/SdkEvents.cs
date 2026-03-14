using System;
using System.Collections.Generic;

namespace Mix
{
	public class SdkEvents
	{
		public Dictionary<KeyValuePair<object, object>, object> events = new Dictionary<KeyValuePair<object, object>, object>();

		public EventHandler<T> AddEventHandler<T>(object o, EventHandler<T> eventHandler) where T : EventArgs
		{
			KeyValuePair<object, object> key = new KeyValuePair<object, object>(o, eventHandler);
			if (events.ContainsKey(key))
			{
				if (events[key] is EventHandler<T>)
				{
					return (EventHandler<T>)events[key];
				}
				events.Remove(key);
			}
			EventHandler<T> eventHandler2 = delegate(object s, T arg)
			{
				try
				{
					eventHandler(s, arg);
				}
				catch (Exception ex)
				{
					string text = string.Empty;
					string[] array = ex.StackTrace.Split('\n');
					if (array.Length > 0)
					{
						text = array[0].Split('(')[0];
					}
					Log.Exception("Event registered with SDK threw an exception!" + text, ex);
				}
			};
			events.Add(key, eventHandler2);
			return eventHandler2;
		}

		public EventHandler<T> GetEventHandler<T>(object o, EventHandler<T> eventHandler) where T : EventArgs
		{
			KeyValuePair<object, object> key = new KeyValuePair<object, object>(o, eventHandler);
			if (events.ContainsKey(key) && events[key] is EventHandler<T>)
			{
				return (EventHandler<T>)events[key];
			}
			return null;
		}
	}
}
