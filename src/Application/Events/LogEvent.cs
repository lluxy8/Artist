using Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Events
{
    public class LogEvent
    {
        public LogType Type { get; }
        public string Author { get; }
        public string Message { get; }

        public LogEvent(string author, string message, LogType type)
        {
            Author = author;
            Message = message;
            Type = type;
        }
    }
}
