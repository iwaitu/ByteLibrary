using System;
using System.Linq;

namespace Byte.Library.Logging.Replay
{
    public class LineParser
    {
        private ILogDefinition definition;

        private static readonly string[] validMethods = 
            new string[] { "GET", "HEAD", "PUT", "DELETE", "POST" };

        public LineParser(ILogDefinition definition)
        {
            if (definition == null)
            {
                throw new ArgumentNullException("definition");
            }

            this.definition = definition;
        }

        public LogEntry GetLogEntry(string line)
        {
            if (line == null)
            {
                throw new ArgumentNullException("line");
            }

            if (!this.definition.LineDiscriminator(line))
            {
                return null;
            }

            var logEntry = new LogEntry();

            string[] tokens = line.Split(
                new char[] { this.definition.FieldSeperator }, 
                StringSplitOptions.RemoveEmptyEntries);

            int uriStemIdx;
            int uriQueryIdx;
            if (this.definition.FieldMap.TryGetValue(LogField.UriStem, out uriStemIdx) &&
                this.definition.FieldMap.TryGetValue(LogField.UriQuery, out uriQueryIdx))
            {
                string uriStemToken = this.ParseToken(tokens[uriStemIdx]);
                string uriQueryToken = this.ParseToken(tokens[uriQueryIdx]);

                string uriStemAndQuery = string.Format("{0}?{1}", uriStemToken, uriQueryToken);

                Uri uri = new Uri(uriStemAndQuery, UriKind.Relative);

                logEntry.Uri = uri;
            }

            int methodIdx;
            if (this.definition.FieldMap.TryGetValue(LogField.Method, out methodIdx))
            {
                string methodTokenUpper = tokens[methodIdx].ToUpper();
                if (validMethods.Contains(methodTokenUpper, StringComparer.OrdinalIgnoreCase))
                {
                    logEntry.Method = methodTokenUpper;
                }
            }

            int userAgentIdx;
            if (this.definition.FieldMap.TryGetValue(LogField.UserAgent, out userAgentIdx))
            {
                string userAgentToken = this.ParseToken(tokens[userAgentIdx]);
                logEntry.UserAgent = userAgentToken;
            }

            return logEntry;
        }

        private string ParseToken(string token)
        {
            if (token == this.definition.EmptyFieldDefinition)
            {
                token = String.Empty;
            }
            return token;
        }
    }
}
