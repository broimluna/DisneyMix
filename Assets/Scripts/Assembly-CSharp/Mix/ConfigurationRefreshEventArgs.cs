using System;
using System.Collections.Generic;

namespace Mix
{
	public class ConfigurationRefreshEventArgs : EventArgs
	{
		public Dictionary<string, string> Configuration { get; protected set; }

		public bool Success { get; protected set; }

		public ConfigurationRefreshEventArgs(bool success, Dictionary<string, string> config)
		{
			Success = success;
			Configuration = config;
		}
	}
}
