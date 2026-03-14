using System.Diagnostics;

namespace Disney.Mix.SDK
{
	public abstract class AbstractLogger
	{
		[Conditional("__MIX_SDK_INTERNAL_DISABLE_LOG_DEBUG__")]
		public abstract void Debug(string message);

		[Conditional("__MIX_SDK_INTERNAL_DISABLE_LOG_WARNING__")]
		public abstract void Warning(string message);

		[Conditional("__MIX_SDK_INTERNAL_DISABLE_LOG_ERROR__")]
		public abstract void Error(string message);

		public abstract void Critical(string message);

		public abstract void Fatal(string message);
	}
}
