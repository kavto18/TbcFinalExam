using System;
using System.Runtime.CompilerServices;

namespace Common.Helper
{
    public interface ILogger
    {
        void Debug(object message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0);

        void DebugFormat(string format, params object[] args);

        void Info(object message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0);

        void InfoFormat(string format, params object[] args);

        void Warn(object message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0);

        void WarnFormat(string format, params object[] args);

        void Error(object message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0);

        void Error(object message, Exception exception, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0);

        void ErrorFormat(string format, params object[] args);

        void Fatal(object message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0);

        void Fatal(object message, Exception exception, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0);

        void FatalFormat(string format, params object[] args);
    }
}
