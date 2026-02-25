using System;

namespace Mix
{
	public class SdkActions
	{
		public Action<T> CreateAction<T>(Action<T> action)
		{
			return delegate(T arg)
			{
				try
				{
					action(arg);
				}
				catch (Exception ex)
				{
					string text = string.Empty;
					string[] array = ex.StackTrace.Split('\n');
					if (array.Length > 0)
					{
						text = array[0].Split('(')[0];
					}
					Log.Exception("SDK callback threw exception! " + text, ex);
				}
			};
		}

		public Action<T> CreateAction<T, U>(Action<T, U> action, U arg2)
		{
			return delegate(T arg3)
			{
				try
				{
					action(arg3, arg2);
				}
				catch (Exception ex)
				{
					string text = string.Empty;
					string[] array = ex.StackTrace.Split('\n');
					if (array.Length > 0)
					{
						text = array[0].Split('(')[0];
					}
					Log.Exception("SDK callback threw exception! " + text, ex);
				}
			};
		}
	}
}
