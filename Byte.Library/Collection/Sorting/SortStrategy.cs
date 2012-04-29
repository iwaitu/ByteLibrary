using System.Collections.Generic;

namespace Byte.Library.Collection.Sorting
{
    internal abstract class SortStrategy<T>
    {
        public abstract void Execute(IList<T> list);

        protected virtual void Swap(IList<T> list, int leftIdx, int rightIdx)
        {
            T temp = list[leftIdx];
            list[leftIdx] = list[rightIdx];
            list[rightIdx] = temp;
        }
    }
}
