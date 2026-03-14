using System;

namespace Mix
{
	public interface IAsyncCall<T>
	{
		void Execute(Action<T> callback);

		void Cancel();

		string GetIdentifier();
	}
}
