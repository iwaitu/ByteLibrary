using System;
using System.Collections.Generic;
using System.IO;

namespace Byte.Library.Logging.Replay
{
    public class IisDefinition : ILogDefinition
    {
        private static readonly string uriStemField = "cs-uri-stem";
        private static readonly string uriQueryField = "cs-uri-query";
        private static readonly string methodQueryField = "cs-method";
        private static readonly string userAgentField = "cs(User-Agent)";

        private string filePath;
        public IDictionary<LogField, int> FieldMap { get; private set; }
        public char FieldSeperator { get; private set; }
        public Func<string, bool> LineDiscriminator { get; private set; }
        public string EmptyFieldDefinition { get; private set; }

        public IisDefinition(string filePath)
        {
            this.filePath = filePath;
            this.FieldMap = new Dictionary<LogField, int>();
            this.FieldSeperator = ' ';
            this.LineDiscriminator = (line) => !line.StartsWith("#");
            this.EmptyFieldDefinition = "-";
        }

        public void Initialize()
        {
            foreach (var line in File.ReadLines(this.filePath))
            {
                if (line.StartsWith("#Fields:"))
                {
                    this.MapFields(line);
                    break;
                }
            }
        }

        private void MapFields(string fieldLine)
        {
            string[] tokens = fieldLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 1; i < tokens.Length; i++)
            {
                if (tokens[i].Equals(uriStemField, StringComparison.OrdinalIgnoreCase))
                {
                    this.FieldMap.Add(LogField.UriStem, i-1);
                }
                else if (tokens[i].Equals(uriQueryField, StringComparison.OrdinalIgnoreCase))
                {
                    this.FieldMap.Add(LogField.UriQuery, i-1);
                }
                else if (tokens[i].Equals(methodQueryField, StringComparison.OrdinalIgnoreCase))
                {
                    this.FieldMap.Add(LogField.Method, i-1);
                }
                else if (tokens[i].Equals(userAgentField, StringComparison.OrdinalIgnoreCase))
                {
                    this.FieldMap.Add(LogField.UserAgent, i-1);
                }
            }
        }
    }
}
