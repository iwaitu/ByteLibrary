using System;

namespace Byte.Library.Logging.Replay
{
    public class LogEntry
    {
        public Uri Uri { get; set; }
        public string Method { get; set; }
        public string UserAgent { get; set; }
    }
}
