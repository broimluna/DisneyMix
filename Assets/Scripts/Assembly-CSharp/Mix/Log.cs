using System;
using System.Collections.Generic;
using System.Diagnostics;
using Disney.Mix.SDK;
using Disney.MobileNetwork;
using UnityEngine;

namespace Mix
{
	public static class Log
	{
		public class MixConsoleLogger : AbstractLogger
		{
			public override void Debug(string message)
			{
			}

			public override void Warning(string message)
			{
			}

			public override void Error(string message)
			{
			}

			public override void Critical(string message)
			{
				if (ShouldLogMessage(message))
				{
					LogToHockey("CRITICAL " + message);
				}
			}

			public override void Fatal(string message)
			{
				if (ShouldLogMessage(message))
				{
					LogToHockey("FATAL " + message);
				}
			}

			private void LogToHockey(string message)
			{
				ReportExceptionToHockey(message, StackTraceUtility.ExtractStackTrace());
			}
		}

		public enum LogLevel
		{
			All = 0,
			Warning = 1,
			Exception = 2,
			Error = 3
		}

		public enum LOG_STATE
		{
			QUIET = 0,
			VERBOSE = 1
		}

		public enum CHANNEL
		{
			GENERAL = 0,
			ASSET_MANAGER = 1,
			ASSET_MANAGER_TRANSACTIONAL = 2,
			ASSET_BUNDLE = 3,
			AVATAR = 4,
			AVATAR_TIMING = 5,
			CHAT = 6,
			CHAT_SERVICE = 7,
			GAMES = 8,
			CPIPE = 9,
			FRIENDS = 10,
			LOCALIZER = 11,
			MOBILE_NETWORKS = 12,
			SINGLETONS = 13,
			MONO_SINGLETONS = 14,
			STARTUP_SEQUENCE = 15,
			THREAD_POOL = 16,
			UI = 17,
			USER_SERVICES = 18,
			WEBSERVICES = 19,
			REG_FLOW = 20,
			PUSH = 21,
			PARENTAL_CONTROL = 22
		}

		public static bool init;

		public static AbstractLogger MixLogger;

		private static Dictionary<CHANNEL, LOG_STATE> logLevels;

		private static List<string> alreadyBeenLogged;

		static Log()
		{
			init = false;
			logLevels = new Dictionary<CHANNEL, LOG_STATE>
			{
				{
					CHANNEL.GENERAL,
					LOG_STATE.VERBOSE
				},
				{
					CHANNEL.ASSET_MANAGER,
					LOG_STATE.QUIET
				},
				{
					CHANNEL.ASSET_MANAGER_TRANSACTIONAL,
					LOG_STATE.QUIET
				},
				{
					CHANNEL.ASSET_BUNDLE,
					LOG_STATE.QUIET
				},
				{
					CHANNEL.AVATAR,
					LOG_STATE.QUIET
				},
				{
					CHANNEL.AVATAR_TIMING,
					LOG_STATE.QUIET
				},
				{
					CHANNEL.CHAT,
					LOG_STATE.QUIET
				},
				{
					CHANNEL.CHAT_SERVICE,
					LOG_STATE.QUIET
				},
				{
					CHANNEL.GAMES,
					LOG_STATE.QUIET
				},
				{
					CHANNEL.CPIPE,
					LOG_STATE.VERBOSE
				},
				{
					CHANNEL.FRIENDS,
					LOG_STATE.QUIET
				},
				{
					CHANNEL.LOCALIZER,
					LOG_STATE.QUIET
				},
				{
					CHANNEL.MOBILE_NETWORKS,
					LOG_STATE.QUIET
				},
				{
					CHANNEL.SINGLETONS,
					LOG_STATE.QUIET
				},
				{
					CHANNEL.MONO_SINGLETONS,
					LOG_STATE.QUIET
				},
				{
					CHANNEL.STARTUP_SEQUENCE,
					LOG_STATE.VERBOSE
				},
				{
					CHANNEL.THREAD_POOL,
					LOG_STATE.QUIET
				},
				{
					CHANNEL.UI,
					LOG_STATE.QUIET
				},
				{
					CHANNEL.USER_SERVICES,
					LOG_STATE.QUIET
				},
				{
					CHANNEL.WEBSERVICES,
					LOG_STATE.QUIET
				},
				{
					CHANNEL.REG_FLOW,
					LOG_STATE.QUIET
				},
				{
					CHANNEL.PUSH,
					LOG_STATE.QUIET
				},
				{
					CHANNEL.PARENTAL_CONTROL,
					LOG_STATE.QUIET
				}
			};
			alreadyBeenLogged = new List<string>();
			MixLogger = new MixConsoleLogger();
		}

		[Conditional("LOGGING_ENABLED")]
		public static void Debug(string msg, CHANNEL channel = CHANNEL.GENERAL)
		{
			if (logLevels[channel] == LOG_STATE.VERBOSE)
			{
				LogInternal(LogLevel.All, msg);
			}
		}

		[Conditional("LOGGING_ENABLED")]
		public static void Warning(string msg, CHANNEL channel = CHANNEL.GENERAL)
		{
			LogInternal(LogLevel.Warning, msg);
		}

		[Conditional("LOGGING_ENABLED")]
		public static void Error(string msg, CHANNEL channel = CHANNEL.GENERAL)
		{
			LogInternal(LogLevel.Error, msg);
		}

		public static void ExceptionOnce(string uniqueId, string msg, Exception exception = null)
		{
			if (!alreadyBeenLogged.Contains(uniqueId))
			{
				alreadyBeenLogged.Add(uniqueId);
				Exception(msg, exception);
			}
		}

		public static void Exception(Exception exception)
		{
			LogInternal(LogLevel.Exception, string.Empty, exception);
		}

		public static void Exception(string msg, Exception exception = null)
		{
			LogInternal(LogLevel.Exception, msg, exception);
		}

		private static void LogInternal(LogLevel level, string aMessage, Exception exception = null)
		{
			if (level != LogLevel.Exception)
			{
				return;
			}
			if (exception != null)
			{
				aMessage = exception.Source + " - " + exception.Message + " - " + aMessage;
				ReportExceptionToHockey(aMessage, exception.StackTrace);
			}
			else
			{
				try
				{
					throw new Exception();
				}
				catch (Exception ex)
				{
					exception = ex;
				}
				ReportExceptionToHockey(aMessage, exception.StackTrace);
			}
		}

		private static void ReportExceptionToHockey(string log, string stackTrace)
		{
			if (Service.IsSet<HockeyAppManager>())
			{
				HockeyAppManager hockeyAppManager = Service.Get<HockeyAppManager>();
				hockeyAppManager.OnHandleLogCallback("[Handled Exception] " + log, stackTrace, LogType.Exception);
			}
		}

		private static void HandleUnityLog(string message, string stackTrace, LogType type)
		{
			if (Service.IsSet<NativeLogger>())
			{
				Service.Get<NativeLogger>().SendLog(message, stackTrace, type);
			}
		}

		private static bool ShouldLogMessage(string message)
		{
			return Application.InternetReachability != 0 && !message.Contains("Other error = ") && !message.Contains("Other web call error = ");
		}
	}
}
