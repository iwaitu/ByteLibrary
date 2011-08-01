using System;
using System.Collections.Generic;

namespace Byte.Library.Collection.Sorting
{
    public class Sorter
    {
        public static void Sort<T>(IList<T> list) where T : IComparable<T>
        {
            Sort(list, SortType.QuickSort);
        }

        public static void Sort<T>(IList<T> list, SortType sortType) where T : IComparable<T>
        {
            SortStrategy<T> strategy = SortStrategyFactory.GetSortStrategy<T>(sortType);
            strategy.Execute(list);
        }
    }
}
