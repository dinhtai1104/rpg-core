using System;
using UnityEngine;

namespace Assets.Abstractions.Shared.Core
{
	public sealed class UnityLogger : ILogger
	{
		private static readonly UnityEngine.ILogger Logger = Debug.unityLogger;

		public bool IsLogEnabled { set; get; } = true;
		public bool IsErrorEnabled { set; get; } = true;
		public bool IsFatalEnabled { set; get; } = true;
		public bool IsInfoEnabled { set; get; } = true;
		public bool IsWarnEnabled { set; get; } = true;

		public void Error(object message)
		{
			if (!IsLogEnabled || !IsErrorEnabled)
			{
				return;
			}

			Logger.Log(LogType.Error, message);
		}

		public void ErrorFormat(string format, params object[] args)
		{
			if (!IsLogEnabled || !IsErrorEnabled)
			{
				return;
			}

			Logger.Log(LogType.Error, string.Format(format, args));
		}

		public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
		{
			if (!IsLogEnabled || !IsErrorEnabled)
			{
				return;
			}

			Logger.Log(LogType.Error, string.Format(provider, format, args));
		}

		public void Fatal(object message)
		{
			if (!IsLogEnabled || !IsFatalEnabled)
			{
				return;
			}

			Logger.Log(LogType.Exception, message);
		}

		public void FatalFormat(string format, params object[] args)
		{
			if (!IsLogEnabled || !IsFatalEnabled)
			{
				return;
			}

			Logger.Log(LogType.Exception, string.Format(format, args));
		}

		public void FatalFormat(IFormatProvider provider, string format, params object[] args)
		{
			if (!IsLogEnabled || !IsFatalEnabled)
			{
				return;
			}

			Logger.Log(LogType.Exception, string.Format(provider, format, args));
		}

		public void Info(object message)
		{
			if (!IsLogEnabled || !IsInfoEnabled)
			{
				return;
			}

			Logger.Log(LogType.Log, message);
		}

		public void InfoFormat(string format, params object[] args)
		{
			if (!IsLogEnabled || !IsInfoEnabled)
			{
				return;
			}

			Logger.Log(LogType.Log, string.Format(format, args));
		}

		public void InfoFormat(IFormatProvider provider, string format, params object[] args)
		{
			if (!IsLogEnabled || !IsInfoEnabled)
			{
				return;
			}

			Logger.Log(LogType.Log, string.Format(provider, format, args));
		}

		public void Warn(object message)
		{
			if (!IsLogEnabled || !IsWarnEnabled)
			{
				return;
			}

			Logger.Log(LogType.Warning, message);
		}

		public void WarnFormat(string format, params object[] args)
		{
			if (!IsLogEnabled || !IsWarnEnabled)
			{
				return;
			}

			Logger.Log(LogType.Warning, string.Format(format, args));
		}

		public void WarnFormat(IFormatProvider provider, string format, params object[] args)
		{
			if (!IsLogEnabled || !IsWarnEnabled)
			{
				return;
			}

			Logger.Log(LogType.Warning, string.Format(provider, format, args));
		}
	}
}
