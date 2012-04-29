using System.Collections.Generic;

namespace Byte.Library.IO
{
    public interface ICsvFileDefinition<T> where T : struct
    {
        char Separator { get; }
        bool TrimItems { get; }
        IDictionary<T, int> ColumnMap { get; }
    }
}
