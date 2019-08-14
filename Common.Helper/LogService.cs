using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository;
using log4net.Repository.Hierarchy;

namespace Common.Helper
{
    public class LogService : ILogger
    {
        private volatile ILog _log;
        private readonly string _defaultLogPattern;
        private readonly bool _useFileAppender;

        public static ILoggerRepository Repository = LogManager.CreateRepository("AppLog");

        public LogService([CallerFilePath]string filePath = "", string defaultLogPattern = null, bool useFileAppender = true)
        {
            _defaultLogPattern = defaultLogPattern ?? "%utcdate %level %logger - %message%newline%exception";
            _useFileAppender = useFileAppender;
            ConfigLogger(GetPathName(filePath));
        }

        private void ConfigLogger(string fileName)
        {
            // If configuration was not found in app.config setup default config
            if (!Repository.Configured)
            {
                var hierarchy = (Hierarchy)Repository;
                if (_useFileAppender)
                {
                    hierarchy.Root.AddAppender(GetRollingFileAppender());
                }
#if DEBUG
                hierarchy.Root.Level = Level.Debug;
#else
                hierarchy.Root.Level = Level.Info;
#endif
                hierarchy.Configured = true;
            }
            _log = LogManager.GetLogger("AppLog", fileName);
        }

        public void Debug(object message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            _log.Debug($"|{GetPathName(filePath)}@{memberName}@{lineNumber}@{Thread.CurrentThread.ManagedThreadId}| - {message}");
        }

        public void DebugFormat(string format, params object[] args)
        {
            _log.DebugFormat(format, args);
        }

        public void Info(object message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            _log.Info($"|{GetPathName(filePath)}@{memberName}@{lineNumber}@{Thread.CurrentThread.ManagedThreadId}| - {message}");
        }

        public void InfoFormat(string format, params object[] args)
        {
            _log.InfoFormat(format, args);
        }

        public void Warn(object message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            _log.Warn($"|{GetPathName(filePath)}@{memberName}@{lineNumber}@{Thread.CurrentThread.ManagedThreadId}| - {message}");
        }

        public void WarnFormat(string format, params object[] args)
        {
            _log.WarnFormat(format, args);
        }

        public void Error(object message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            _log.Error($"|{GetPathName(filePath)}@{memberName}@{lineNumber}@{Thread.CurrentThread.ManagedThreadId}| - {message}");
        }

        public void Error(object message, Exception exception, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            _log.Error($"|{GetPathName(filePath)}@{memberName}@{lineNumber}@{Thread.CurrentThread.ManagedThreadId}| - {message}", exception);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            _log.ErrorFormat(format, args);
        }

        public void Fatal(object message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            _log.Fatal($"|{GetPathName(filePath)}@{memberName}@{lineNumber}@{Thread.CurrentThread.ManagedThreadId}| - {message}");
        }

        public void Fatal(object message, Exception exception, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            _log.Fatal($"|{GetPathName(filePath)}@{memberName}@{lineNumber}@{Thread.CurrentThread.ManagedThreadId}| - {message}", exception);
        }

        public void FatalFormat(string format, params object[] args)
        {
            _log.FatalFormat(format, args);
        }

        public string GetPathName(string filePath)
        {
            try
            {
                return Path.GetFileNameWithoutExtension(filePath);
            }
            catch (Exception e)
            {
                _log.Fatal($"Invalid failPath - {filePath}", e);
                return "UnknowFileName";
            }
        }

        private IAppender GetRollingFileAppender(string logPattern = null)
        {
            var patternLayout = new PatternLayout
            {
                ConversionPattern = logPattern ?? _defaultLogPattern
            };
            patternLayout.ActivateOptions();
            var roller = new RollingFileAppender
            {
                AppendToFile = true,
                File = "AppLogs/",
                Layout = patternLayout,
                MaxSizeRollBackups = -1,
                MaximumFileSize = "50MB",
                RollingStyle = RollingFileAppender.RollingMode.Composite,
                StaticLogFileName = false,
                PreserveLogFileNameExtension = true,
                DatePattern = "yyyy/MM/dd/'Logs.log'"
            };
            roller.ActivateOptions();
            return roller;
        }
    }
}
