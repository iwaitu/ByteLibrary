using System.Collections.Generic;

namespace ByteLibrary.IO
{
    public interface ICsvFileDefinition<T> where T : struct
    {
        char Separator { get; }
        bool TrimItems { get; }
        IDictionary<T, int> ColumnMap { get; }
    }
}
