using System;

namespace Assets.Abstractions.Shared.Core
{
	public interface ILogger
	{
		bool IsLogEnabled { get; set; }

		bool IsErrorEnabled { get; set; }

		bool IsFatalEnabled { get; set; }

		bool IsInfoEnabled { get; set; }

		bool IsWarnEnabled { get; set; }

		void Error(object message);

		void ErrorFormat(string format, params object[] args);

		void ErrorFormat(IFormatProvider provider, string format, params object[] args);

		void Fatal(object message);

		void FatalFormat(string format, params object[] args);

		void FatalFormat(IFormatProvider provider, string format, params object[] args);

		void Info(object message);

		void InfoFormat(string format, params object[] args);

		void InfoFormat(IFormatProvider provider, string format, params object[] args);

		void Warn(object message);

		void WarnFormat(string format, params object[] args);

		void WarnFormat(IFormatProvider provider, string format, params object[] args);
	}
}
