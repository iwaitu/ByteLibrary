using System;
using System.Collections.Generic;
using System.Linq;

namespace ByteLibrary
{
    public static class EnumUtilities
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
