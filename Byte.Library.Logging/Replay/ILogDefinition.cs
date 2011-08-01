using System;
using System.Collections.Generic;

namespace Byte.Library.Logging.Replay
{
    public interface ILogDefinition
    {
        IDictionary<LogField, int> FieldMap { get; }
        char FieldSeperator { get; }
        Func<string, bool> LineDiscriminator { get; }
        string EmptyFieldDefinition { get; }
    }
}
