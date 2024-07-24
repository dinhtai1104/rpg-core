#define LOGS_DEVELOPMENT
#define LOGS_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using CallerName = System.Runtime.CompilerServices.CallerMemberNameAttribute;
using CallerPath = System.Runtime.CompilerServices.CallerFilePathAttribute;
using CallerLine = System.Runtime.CompilerServices.CallerLineNumberAttribute;
using Debug = UnityEngine.Debug;

namespace Assets.Abstractions.Shared.Foundation
{
    /// <summary> Log level. </summary>
    public enum LogLevel : byte
    {
        // Info.
        Info,

        // Warnings.
        Warning,

        // Errors.
        Error,

        // No message.
        Off
    }

    /// <summary> Log messages. </summary>
    public static class Log
    {
        /// <summary> Log level. </summary>
        public static LogLevel Level { get; set; } = Settings.Log.DefaultLevel;

#if UNITY_EDITOR
        private static void LogInfo(string source, string message) =>
            Debug.Log($"{source} <color={Settings.Log.InfoColor.ToHex()}>{message}</color>");

        private static void LogWarning(string source, string message) =>
            Debug.LogWarning($"{source} <color={Settings.Log.WarningColor.ToHex()}>{message}</color>");

        private static void LogError(string source, string message) =>
            Debug.LogError($"{source} <color={Settings.Log.ErrorColor.ToHex()}>{message}</color>");

#else
        private static void LogInfo(string source, string message) => Debug.Log($"{source} {message}");
        private static void LogWarning(string source, string message) => Debug.LogWarning($"{source} {message}");
        private static void LogError(string source, string message) => Debug.LogError($"{source} {message}");
#endif

        /// <summary> Information message. </summary>
        /// <param name="message">Message</param>
        [DebuggerStepThrough]
#if LOGS_DEVELOPMENT
        [Conditional("DEVELOPMENT")]
#endif
#if LOGS_EDITOR
        [Conditional("UNITY_EDITOR")]
#endif
        public static void Info(string message, [CallerName] string member = "",
            [CallerPath] string sourceFile = "",
            [CallerLine] int line = 0)
        {
            if (Level <= LogLevel.Info)
                LogInfo($"[{Path.GetFileNameWithoutExtension(sourceFile)}:{member}:{line}]", message);
        }

        /// <summary> Warning message. </summary>
        /// <param name="message">Message</param>
        [DebuggerStepThrough]
#if LOGS_DEVELOPMENT
        [Conditional("DEVELOPMENT")]
#endif
#if LOGS_EDITOR
        [Conditional("UNITY_EDITOR")]
#endif
        public static void Warning(string message, [CallerName] string member = "",
            [CallerPath] string sourceFile = "",
            [CallerLine] int line = 0)
        {
            if (Level <= LogLevel.Warning)
                LogWarning($"[{Path.GetFileNameWithoutExtension(sourceFile)}:{member}:{line}]", message);
        }

        /// <summary> Error message. </summary>
        /// <param name="message">Message</param>
        [DebuggerStepThrough]
#if LOGS_DEVELOPMENT
        [Conditional("DEVELOPMENT")]
#endif
#if LOGS_EDITOR
        [Conditional("UNITY_EDITOR")]
#endif
        public static void Error(string message, [CallerName] string member = "",
            [CallerPath] string sourceFile = "",
            [CallerLine] int line = 0)
        {
            if (Level <= LogLevel.Error)
                LogError($"[{Path.GetFileNameWithoutExtension(sourceFile)}:{member}:{line}]", message);
        }

        /// <summary> Throw exception. </summary>
        /// <param name="message">Message</param>
        /// <param name="e">Exception</param>
        [DebuggerStepThrough]
#if LOGS_DEVELOPMENT
        [Conditional("DEVELOPMENT")]
#endif
#if LOGS_EDITOR
        [Conditional("UNITY_EDITOR")]
#endif
        public static void Exception(string message, Exception e = null, [CallerName] string member = "",
            [CallerPath] string sourceFile = "",
            [CallerLine] int line = 0)
        {
            e ??= new Exception($"[{Path.GetFileNameWithoutExtension(sourceFile)}:{member}:{line}] {message}");
            Debug.LogException(e);

            throw e;
        }

        /// <summary> Argument exception and stack trace. </summary>
        /// <param name="message">Message</param>
        [DebuggerStepThrough]
#if LOGS_DEVELOPMENT
        [Conditional("DEVELOPMENT")]
#endif
#if LOGS_EDITOR
        [Conditional("UNITY_EDITOR")]
#endif
        public static void ExceptionArgument(string message, [CallerName] string member = "",
            [CallerPath] string sourceFile = "",
            [CallerLine] int line = 0) =>
            Exception(message, new ArgumentException(), member, sourceFile, line);

        /// <summary> ArgumentOutOfRange exception and stack trace. </summary>
        /// <param name="message">Message</param>
        [DebuggerStepThrough]
#if LOGS_DEVELOPMENT
        [Conditional("DEVELOPMENT")]
#endif
#if LOGS_EDITOR
        [Conditional("UNITY_EDITOR")]
#endif
        public static void ExceptionArgumentOutOfRange(string message, [CallerName] string member = "",
            [CallerPath] string sourceFile = "",
            [CallerLine] int line = 0) =>
            Exception(message, new ArgumentOutOfRangeException(), member, sourceFile, line);

        /// <summary> IndexOutOfRange exception and stack trace. </summary>
        /// <param name="message">Message</param>
        [DebuggerStepThrough]
#if LOGS_DEVELOPMENT
        [Conditional("DEVELOPMENT")]
#endif
#if LOGS_EDITOR
        [Conditional("UNITY_EDITOR")]
#endif
        public static void ExceptionIndexOutOfRange(string message, [CallerName] string member = "",
            [CallerPath] string sourceFile = "",
            [CallerLine] int line = 0) =>
            Exception(message, new IndexOutOfRangeException(), member, sourceFile, line);

        /// <summary> ArgumentNull exception and stack trace. </summary>
        /// <param name="message">Message</param>
        [DebuggerStepThrough]
#if LOGS_DEVELOPMENT
        [Conditional("DEVELOPMENT")]
#endif
#if LOGS_EDITOR
        [Conditional("UNITY_EDITOR")]
#endif
        public static void ExceptionArgumentNull(string message, [CallerName] string member = "",
            [CallerPath] string sourceFile = "",
            [CallerLine] int line = 0) =>
            Exception(message, new ArgumentNullException(), member, sourceFile, line);

        /// <summary> Key not found exception and stack trace. </summary>
        /// <param name="message">Message</param>
        [DebuggerStepThrough]
#if LOGS_DEVELOPMENT
        [Conditional("DEVELOPMENT")]
#endif
#if LOGS_EDITOR
        [Conditional("UNITY_EDITOR")]
#endif
        public static void ExceptionKeyNotFound(string message, [CallerName] string member = "",
            [CallerPath] string sourceFile = "",
            [CallerLine] int line = 0) =>
            Exception(message, new KeyNotFoundException(), member, sourceFile, line);
    }
}