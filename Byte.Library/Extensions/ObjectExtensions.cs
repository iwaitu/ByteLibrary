using System;

namespace Byte.Library.Extensions
{
    public static class ObjectExtensions
    {
        public static void AsIf<T>(this object value, Action<T> action) where T : class
        {
            T typed = value as T;

            if (typed != null)
            {
                action(typed);
            }
        }
    }
}
