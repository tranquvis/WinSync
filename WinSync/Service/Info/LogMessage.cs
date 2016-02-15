using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSync.Service
{
    public class LogMessage
    {
        public LogType Type { get; set; }
        public string Message { get; set; }

        public Exception Exception { get; set; }

        public LogMessage(LogType type, string message, Exception exception)
        {
            Type = type;
            Message = message;
            Exception = exception;
        }
    }
}
